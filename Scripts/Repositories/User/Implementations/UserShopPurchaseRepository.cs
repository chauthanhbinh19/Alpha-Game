using System;
using System.Threading.Tasks;
using MySqlConnector;
using UnityEngine;

public class UserShopPurchaseRepository : IUserShopPurchaseRepository
{
    /// <summary>
    /// Kiểm tra trạng thái người dùng có hợp lệ để giao dịch hay không.
    /// </summary>
    /// <returns>Trả về Success nếu hợp lệ, ngược lại trả về Failure kèm thông báo lỗi.</returns>
    public async Task<InsertOrUpdateResult<bool>> CheckUserStatusAsync(string userId, MySqlConnection connection, MySqlTransaction transaction = null)
    {
        string checkUserSQL = @"
        SELECT is_active, is_deleted 
        FROM users 
        WHERE id = @user_id 
        FOR UPDATE;"; // Khóa dòng nếu gọi trong Transaction

        await using MySqlCommand cmd = new MySqlCommand(checkUserSQL, connection, transaction);
        cmd.Parameters.AddWithValue("@user_id", userId);

        await using MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return InsertOrUpdateResult<bool>.Failure(MessageConstants.USER_NOT_FOUND);
        }

        bool isActive = Convert.ToBoolean(reader["is_active"]);
        bool isDeleted = Convert.ToBoolean(reader["is_deleted"]);

        if (isDeleted)
        {
            return InsertOrUpdateResult<bool>.Failure(MessageConstants.USER_DELETED);
        }

        if (!isActive)
        {
            return InsertOrUpdateResult<bool>.Failure(MessageConstants.USER_INACTIVE);
        }

        return InsertOrUpdateResult<bool>.Success(true);
    }
    public async Task<InsertOrUpdateResult<CardHeroes>> InsertOrUpdateUserCardHeroAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardHeroes>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardHeroes>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardHeroes>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardHeroes>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardHero gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardHeroSQL = @"
            SELECT * FROM card_heroes 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardHeroes cardHero = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardHeroSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardHero = MappingExtensionsHelper.MapCardHeroFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardHero.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardHero == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardHeroes>.Failure(MessageConstants.CARD_HEROES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_heroes (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_heroes (
                user_id, card_hero_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardHero.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardHeroParameters(updateOrInsertObjectCommand, userId, cardHero);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_HEROES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardHeroes>.Inserted(cardHero);
            }
            else
            {
                return InsertOrUpdateResult<CardHeroes>.Updated(cardHero);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardHeroes>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Achievements>> InsertOrUpdateUserAchievementAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Achievements>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Achievements>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Achievements>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Achievements>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Achievement gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectAchievementSQL = @"
            SELECT * FROM achievements 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Achievements achievement = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectAchievementSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    achievement = MappingExtensionsHelper.MapAchievementFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    achievement.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (achievement == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Achievements>.Failure(MessageConstants.ACHIEVEMENTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_achievements (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_achievements (
                user_id, achievement_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                achievement.Quantity = purchaseCount;
                MappingExtensionsHelper.AddAchievementParameters(updateOrInsertObjectCommand, userId, achievement);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.ACHIEVEMENTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Achievements>.Inserted(achievement);
            }
            else
            {
                return InsertOrUpdateResult<Achievements>.Updated(achievement);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Achievements>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Books>> InsertOrUpdateUserBookAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Books>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Books>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Books>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Books>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Book gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectBookSQL = @"
            SELECT * FROM books 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Books book = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectBookSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    book = MappingExtensionsHelper.MapBookFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    book.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (book == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Books>.Failure(MessageConstants.BOOKS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_books (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_books (
                user_id, book_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                book.Quantity = purchaseCount;
                MappingExtensionsHelper.AddBookParameters(updateOrInsertObjectCommand, userId, book);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.BOOKS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Books>.Inserted(book);
            }
            else
            {
                return InsertOrUpdateResult<Books>.Updated(book);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Books>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Pets>> InsertOrUpdateUserPetAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Pets>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Pets>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Pets>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Pets>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Pet gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectPetSQL = @"
            SELECT * FROM pets 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Pets pet = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectPetSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    pet = MappingExtensionsHelper.MapPetFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    pet.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (pet == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Pets>.Failure(MessageConstants.PETS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_pets (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_pets (
                user_id, pet_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                pet.Quantity = purchaseCount;
                MappingExtensionsHelper.AddPetParameters(updateOrInsertObjectCommand, userId, pet);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.PETS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Pets>.Inserted(pet);
            }
            else
            {
                return InsertOrUpdateResult<Pets>.Updated(pet);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Pets>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardCaptains>> InsertOrUpdateUserCardCaptainAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardCaptains>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardCaptains>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardCaptains>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardCaptains>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardCaptain gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardCaptainSQL = @"
            SELECT * FROM card_captains 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardCaptains cardCaptain = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardCaptainSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardCaptain = MappingExtensionsHelper.MapCardCaptainFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardCaptain.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardCaptain == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardCaptains>.Failure(MessageConstants.CARD_CAPTAINS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_captains (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_captains (
                user_id, card_captain_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardCaptain.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardCaptainParameters(updateOrInsertObjectCommand, userId, cardCaptain);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_CAPTAINS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardCaptains>.Inserted(cardCaptain);
            }
            else
            {
                return InsertOrUpdateResult<CardCaptains>.Updated(cardCaptain);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardCaptains>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CollaborationEquipments>> InsertOrUpdateUserCollaborationEquipmentAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CollaborationEquipments>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CollaborationEquipments>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CollaborationEquipments>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CollaborationEquipments>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CollaborationEquipment gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCollaborationEquipmentSQL = @"
            SELECT * FROM collaboration_equipments 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CollaborationEquipments collaborationEquipment = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCollaborationEquipmentSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    collaborationEquipment = MappingExtensionsHelper.MapCollaborationEquipmentFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    collaborationEquipment.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (collaborationEquipment == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CollaborationEquipments>.Failure(MessageConstants.COLLABORATION_EQUIPMENTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_collaboration_equipments (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_collaboration_equipments (
                user_id, collaboration_equipment_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                collaborationEquipment.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCollaborationEquipmentParameters(updateOrInsertObjectCommand, userId, collaborationEquipment);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.COLLABORATION_EQUIPMENTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CollaborationEquipments>.Inserted(collaborationEquipment);
            }
            else
            {
                return InsertOrUpdateResult<CollaborationEquipments>.Updated(collaborationEquipment);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CollaborationEquipments>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardMilitaries>> InsertOrUpdateUserCardMilitaryAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMilitaries>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMilitaries>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMilitaries>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMilitaries>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardMilitary gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardMilitarySQL = @"
            SELECT * FROM card_militaries 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardMilitaries cardMilitary = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardMilitarySQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardMilitary = MappingExtensionsHelper.MapCardMilitaryFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardMilitary.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardMilitary == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMilitaries>.Failure(MessageConstants.CARD_MILITARIES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_militaries (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_militaries (
                user_id, card_militarie_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardMilitary.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardMilitaryParameters(updateOrInsertObjectCommand, userId, cardMilitary);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_MILITARIES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardMilitaries>.Inserted(cardMilitary);
            }
            else
            {
                return InsertOrUpdateResult<CardMilitaries>.Updated(cardMilitary);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardMilitaries>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardSpells>> InsertOrUpdateUserCardSpellAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSpells>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSpells>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSpells>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSpells>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardSpell gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardSpellSQL = @"
            SELECT * FROM card_spells 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardSpells cardSpell = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardSpellSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardSpell = MappingExtensionsHelper.MapCardSpellFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardSpell.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardSpell == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSpells>.Failure(MessageConstants.CARD_SPELLS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_spells (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_spells (
                user_id, card_spell_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardSpell.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardSpellParameters(updateOrInsertObjectCommand, userId, cardSpell);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_SPELLS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardSpells>.Inserted(cardSpell);
            }
            else
            {
                return InsertOrUpdateResult<CardSpells>.Updated(cardSpell);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardSpells>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Collaborations>> InsertOrUpdateUserCollaborationAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Collaborations>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Collaborations>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Collaborations>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Collaborations>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Collaboration gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCollaborationSQL = @"
            SELECT * FROM collaborations 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Collaborations collaboration = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCollaborationSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    collaboration = MappingExtensionsHelper.MapCollaborationFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    collaboration.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (collaboration == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Collaborations>.Failure(MessageConstants.COLLABORATIONS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_collaborations (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_collaborations (
                user_id, collaboration_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                collaboration.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCollaborationParameters(updateOrInsertObjectCommand, userId, collaboration);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.COLLABORATIONS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Collaborations>.Inserted(collaboration);
            }
            else
            {
                return InsertOrUpdateResult<Collaborations>.Updated(collaboration);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Collaborations>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardMonsters>> InsertOrUpdateUserCardMonsterAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMonsters>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMonsters>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMonsters>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMonsters>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardMonster gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardMonsterSQL = @"
            SELECT * FROM card_monsters 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardMonsters cardMonster = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardMonsterSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardMonster = MappingExtensionsHelper.MapCardMonsterFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardMonster.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardMonster == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardMonsters>.Failure(MessageConstants.CARD_MONSTERS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_monsters (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_monsters (
                user_id, card_monster_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardMonster.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardMonsterParameters(updateOrInsertObjectCommand, userId, cardMonster);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_MONSTERS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardMonsters>.Inserted(cardMonster);
            }
            else
            {
                return InsertOrUpdateResult<CardMonsters>.Updated(cardMonster);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardMonsters>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Equipments>> InsertOrUpdateUserEquipmentAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Equipments>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Equipments>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Equipments>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Equipments>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Equipment gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectEquipmentSQL = @"
            SELECT * FROM equipments 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Equipments equipment = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectEquipmentSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    equipment = MappingExtensionsHelper.MapEquipmentFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    equipment.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (equipment == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Equipments>.Failure(MessageConstants.EQUIPMENTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_equipments (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_equipments (
                user_id, equipment_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                equipment.Quantity = purchaseCount;
                MappingExtensionsHelper.AddEquipmentParameters(updateOrInsertObjectCommand, userId, equipment);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.EQUIPMENTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Equipments>.Inserted(equipment);
            }
            else
            {
                return InsertOrUpdateResult<Equipments>.Updated(equipment);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Equipments>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Medals>> InsertOrUpdateUserMedalAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Medals>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Medals>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Medals>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Medals>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Medal gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectMedalSQL = @"
            SELECT * FROM medals 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Medals medal = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectMedalSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    medal = MappingExtensionsHelper.MapMedalFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    medal.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (medal == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Medals>.Failure(MessageConstants.MEDALS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_medals (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_medals (
                user_id, medal_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                medal.Quantity = purchaseCount;
                MappingExtensionsHelper.AddMedalParameters(updateOrInsertObjectCommand, userId, medal);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.MEDALS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Medals>.Inserted(medal);
            }
            else
            {
                return InsertOrUpdateResult<Medals>.Updated(medal);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Medals>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Skills>> InsertOrUpdateUserSkillAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Skills>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Skills>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Skills>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Skills>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Skill gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectSkillSQL = @"
            SELECT * FROM skills 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Skills skill = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectSkillSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    skill = MappingExtensionsHelper.MapSkillFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    skill.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (skill == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Skills>.Failure(MessageConstants.SKILLS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_skills (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_skills (
                user_id, skill_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                skill.Quantity = purchaseCount;
                MappingExtensionsHelper.AddSkillParameters(updateOrInsertObjectCommand, userId, skill);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.SKILLS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Skills>.Inserted(skill);
            }
            else
            {
                return InsertOrUpdateResult<Skills>.Updated(skill);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Skills>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Symbols>> InsertOrUpdateUserSymbolAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Symbols>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Symbols>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Symbols>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Symbols>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Symbol gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectSymbolSQL = @"
            SELECT * FROM symbols 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Symbols symbol = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectSymbolSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    symbol = MappingExtensionsHelper.MapSymbolFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    symbol.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (symbol == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Symbols>.Failure(MessageConstants.SYMBOLS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_symbols (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_symbols (
                user_id, symbol_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                symbol.Quantity = purchaseCount;
                MappingExtensionsHelper.AddSymbolParameters(updateOrInsertObjectCommand, userId, symbol);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.SYMBOLS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Symbols>.Inserted(symbol);
            }
            else
            {
                return InsertOrUpdateResult<Symbols>.Updated(symbol);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Symbols>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Titles>> InsertOrUpdateUserTitleAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Titles>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Titles>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Titles>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Titles>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Title gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectTitleSQL = @"
            SELECT * FROM titles 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Titles title = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectTitleSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    title = MappingExtensionsHelper.MapTitleFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    title.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (title == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Titles>.Failure(MessageConstants.TITLES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_titles (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_titles (
                user_id, title_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                title.Quantity = purchaseCount;
                MappingExtensionsHelper.AddTitleParameters(updateOrInsertObjectCommand, userId, title);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.TITLES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Titles>.Inserted(title);
            }
            else
            {
                return InsertOrUpdateResult<Titles>.Updated(title);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Titles>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<MagicFormationCircles>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MagicFormationCircles>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MagicFormationCircles>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MagicFormationCircles>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MagicFormationCircles>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin MagicFormationCircle gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectMagicFormationCircleSQL = @"
            SELECT * FROM magic_formation_circles 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            MagicFormationCircles magicFormationCircle = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectMagicFormationCircleSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    magicFormationCircle = MappingExtensionsHelper.MapMagicFormationCircleFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    magicFormationCircle.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (magicFormationCircle == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MagicFormationCircles>.Failure(MessageConstants.MAGIC_FORMATION_CIRCLES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_magic_formation_circles (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_magic_formation_circles (
                user_id, magic_formation_circle_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                magicFormationCircle.Quantity = purchaseCount;
                MappingExtensionsHelper.AddMagicFormationCircleParameters(updateOrInsertObjectCommand, userId, magicFormationCircle);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<MagicFormationCircles>.Inserted(magicFormationCircle);
            }
            else
            {
                return InsertOrUpdateResult<MagicFormationCircles>.Updated(magicFormationCircle);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<MagicFormationCircles>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Relics>> InsertOrUpdateUserRelicAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Relics>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Relics>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Relics>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Relics>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Relic gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectRelicSQL = @"
            SELECT * FROM relics 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Relics relic = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectRelicSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    relic = MappingExtensionsHelper.MapRelicFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    relic.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (relic == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Relics>.Failure(MessageConstants.RELICS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_relics (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_relics (
                user_id, relic_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                relic.Quantity = purchaseCount;
                MappingExtensionsHelper.AddRelicParameters(updateOrInsertObjectCommand, userId, relic);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.RELICS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Relics>.Inserted(relic);
            }
            else
            {
                return InsertOrUpdateResult<Relics>.Updated(relic);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Relics>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardColonels>> InsertOrUpdateUserCardColonelAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardColonels>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardColonels>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardColonels>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardColonels>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardColonel gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardColonelSQL = @"
            SELECT * FROM card_colonels 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardColonels cardColonel = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardColonelSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardColonel = MappingExtensionsHelper.MapCardColonelFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardColonel.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardColonel == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardColonels>.Failure(MessageConstants.CARD_COLONELS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_colonels (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_colonels (
                user_id, card_colonel_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardColonel.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardColonelParameters(updateOrInsertObjectCommand, userId, cardColonel);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_COLONELS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardColonels>.Inserted(cardColonel);
            }
            else
            {
                return InsertOrUpdateResult<CardColonels>.Updated(cardColonel);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardColonels>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardGenerals>> InsertOrUpdateUserCardGeneralAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardGenerals>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardGenerals>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardGenerals>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardGenerals>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardGeneral gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardGeneralSQL = @"
            SELECT * FROM card_generals 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardGenerals cardGeneral = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardGeneralSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardGeneral = MappingExtensionsHelper.MapCardGeneralFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardGeneral.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardGeneral == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardGenerals>.Failure(MessageConstants.CARD_GENERALS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_generals (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_generals (
                user_id, card_general_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardGeneral.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardGeneralParameters(updateOrInsertObjectCommand, userId, cardGeneral);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_GENERALS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardGenerals>.Inserted(cardGeneral);
            }
            else
            {
                return InsertOrUpdateResult<CardGenerals>.Updated(cardGeneral);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardGenerals>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardAdmirals>> InsertOrUpdateUserCardAdmiralAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardAdmirals>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardAdmirals>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardAdmirals>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardAdmirals>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardAdmiral gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardAdmiralSQL = @"
            SELECT * FROM card_admirals 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardAdmirals cardAdmiral = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardAdmiralSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardAdmiral = MappingExtensionsHelper.MapCardAdmiralFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardAdmiral.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardAdmiral == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardAdmirals>.Failure(MessageConstants.CARD_ADMIRALS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_admirals (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_admirals (
                user_id, card_admiral_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardAdmiral.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardAdmiralParameters(updateOrInsertObjectCommand, userId, cardAdmiral);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_ADMIRALS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardAdmirals>.Inserted(cardAdmiral);
            }
            else
            {
                return InsertOrUpdateResult<CardAdmirals>.Updated(cardAdmiral);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardAdmirals>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardSoldiers>> InsertOrUpdateUserCardSoldierAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSoldiers>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSoldiers>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSoldiers>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSoldiers>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardSoldier gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardSoldierSQL = @"
            SELECT * FROM card_soldiers 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardSoldiers cardSoldier = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardSoldierSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardSoldier = MappingExtensionsHelper.MapCardSoldierFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardSoldier.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardSoldier == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardSoldiers>.Failure(MessageConstants.CARD_SOLDIERS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_soldiers (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_soldiers (
                user_id, card_soldier_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardSoldier.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardSoldierParameters(updateOrInsertObjectCommand, userId, cardSoldier);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_SOLDIERS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardSoldiers>.Inserted(cardSoldier);
            }
            else
            {
                return InsertOrUpdateResult<CardSoldiers>.Updated(cardSoldier);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardSoldiers>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Borders>> InsertOrUpdateUserBorderAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Borders>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Borders>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Borders>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Borders>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Border gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectBorderSQL = @"
            SELECT * FROM borders 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Borders border = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectBorderSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    border = MappingExtensionsHelper.MapBorderFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    border.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (border == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Borders>.Failure(MessageConstants.BORDERS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_borders (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_borders (
                user_id, border_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                border.Quantity = purchaseCount;
                MappingExtensionsHelper.AddBorderParameters(updateOrInsertObjectCommand, userId, border);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.BORDERS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Borders>.Inserted(border);
            }
            else
            {
                return InsertOrUpdateResult<Borders>.Updated(border);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Borders>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Talismans>> InsertOrUpdateUserTalismanAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Talismans>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Talismans>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Talismans>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Talismans>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Talisman gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectTalismanSQL = @"
            SELECT * FROM talismans 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Talismans talisman = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectTalismanSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    talisman = MappingExtensionsHelper.MapTalismanFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    talisman.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (talisman == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Talismans>.Failure(MessageConstants.TALISMANS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_talismans (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_talismans (
                user_id, talisman_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                talisman.Quantity = purchaseCount;
                MappingExtensionsHelper.AddTalismanParameters(updateOrInsertObjectCommand, userId, talisman);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.TALISMANS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Talismans>.Inserted(talisman);
            }
            else
            {
                return InsertOrUpdateResult<Talismans>.Updated(talisman);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Talismans>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Puppets>> InsertOrUpdateUserPuppetAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Puppets>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Puppets>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Puppets>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Puppets>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Puppet gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectPuppetSQL = @"
            SELECT * FROM puppets 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Puppets puppet = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectPuppetSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    puppet = MappingExtensionsHelper.MapPuppetFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    puppet.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (puppet == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Puppets>.Failure(MessageConstants.PUPPETS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_puppets (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_puppets (
                user_id, puppet_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                puppet.Quantity = purchaseCount;
                MappingExtensionsHelper.AddPuppetParameters(updateOrInsertObjectCommand, userId, puppet);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.PUPPETS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Puppets>.Inserted(puppet);
            }
            else
            {
                return InsertOrUpdateResult<Puppets>.Updated(puppet);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Puppets>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Alchemies>> InsertOrUpdateUserAlchemyAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Alchemies>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Alchemies>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Alchemies>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Alchemies>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Alchemy gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectAlchemySQL = @"
            SELECT * FROM alchemies 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Alchemies alchemy = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectAlchemySQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    alchemy = MappingExtensionsHelper.MapAlchemyFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    alchemy.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (alchemy == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Alchemies>.Failure(MessageConstants.ALCHEMIES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_alchemies (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_alchemies (
                user_id, alchemie_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                alchemy.Quantity = purchaseCount;
                MappingExtensionsHelper.AddAlchemyParameters(updateOrInsertObjectCommand, userId, alchemy);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.ALCHEMIES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Alchemies>.Inserted(alchemy);
            }
            else
            {
                return InsertOrUpdateResult<Alchemies>.Updated(alchemy);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Alchemies>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Forges>> InsertOrUpdateUserForgeAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Forges>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Forges>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Forges>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Forges>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Forge gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectForgeSQL = @"
            SELECT * FROM forges 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Forges forge = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectForgeSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    forge = MappingExtensionsHelper.MapForgeFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    forge.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (forge == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Forges>.Failure(MessageConstants.FORGES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_forges (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_forges (
                user_id, forge_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                forge.Quantity = purchaseCount;
                MappingExtensionsHelper.AddForgeParameters(updateOrInsertObjectCommand, userId, forge);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.FORGES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Forges>.Inserted(forge);
            }
            else
            {
                return InsertOrUpdateResult<Forges>.Updated(forge);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Forges>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CardLives>> InsertOrUpdateUserCardLifeAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardLives>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardLives>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardLives>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardLives>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin CardLife gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCardLifeSQL = @"
            SELECT * FROM card_lives 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            CardLives cardLife = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCardLifeSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    cardLife = MappingExtensionsHelper.MapCardLifeFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    cardLife.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (cardLife == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<CardLives>.Failure(MessageConstants.CARD_LIVES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_card_lives (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_card_lives (
                user_id, card_live_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                cardLife.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCardLifeParameters(updateOrInsertObjectCommand, userId, cardLife);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CARD_LIVES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<CardLives>.Inserted(cardLife);
            }
            else
            {
                return InsertOrUpdateResult<CardLives>.Updated(cardLife);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<CardLives>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Artworks>> InsertOrUpdateUserArtworkAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artworks>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artworks>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artworks>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artworks>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Artwork gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectArtworkSQL = @"
            SELECT * FROM artworks 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Artworks artwork = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectArtworkSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    artwork = MappingExtensionsHelper.MapArtworkFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    artwork.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (artwork == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artworks>.Failure(MessageConstants.ARTWORKS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_artworks (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_artworks (
                user_id, artwork_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                artwork.Quantity = purchaseCount;
                MappingExtensionsHelper.AddArtworkParameters(updateOrInsertObjectCommand, userId, artwork);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.ARTWORKS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Artworks>.Inserted(artwork);
            }
            else
            {
                return InsertOrUpdateResult<Artworks>.Updated(artwork);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Artworks>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<SpiritBeasts>> InsertOrUpdateUserSpiritBeastAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritBeasts>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritBeasts>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritBeasts>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritBeasts>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin SpiritBeast gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectSpiritBeastSQL = @"
            SELECT * FROM spirit_beasts 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            SpiritBeasts spiritBeast = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectSpiritBeastSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    spiritBeast = MappingExtensionsHelper.MapSpiritBeastFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    spiritBeast.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (spiritBeast == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritBeasts>.Failure(MessageConstants.SPIRIT_BEASTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_spirit_beasts (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_spirit_beasts (
                user_id, spirit_beast_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                spiritBeast.Quantity = purchaseCount;
                MappingExtensionsHelper.AddSpiritBeastParameters(updateOrInsertObjectCommand, userId, spiritBeast);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.SPIRIT_BEASTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<SpiritBeasts>.Inserted(spiritBeast);
            }
            else
            {
                return InsertOrUpdateResult<SpiritBeasts>.Updated(spiritBeast);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<SpiritBeasts>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Avatars>> InsertOrUpdateUserAvatarAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Avatars>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Avatars>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Avatars>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Avatars>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Avatar gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectAvatarSQL = @"
            SELECT * FROM avatars 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Avatars avatar = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectAvatarSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    avatar = MappingExtensionsHelper.MapAvatarFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    avatar.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (avatar == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Avatars>.Failure(MessageConstants.AVATARS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_avatars (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_avatars (
                user_id, avatar_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                avatar.Quantity = purchaseCount;
                MappingExtensionsHelper.AddAvatarParameters(updateOrInsertObjectCommand, userId, avatar);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.AVATARS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Avatars>.Inserted(avatar);
            }
            else
            {
                return InsertOrUpdateResult<Avatars>.Updated(avatar);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Avatars>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<SpiritCards>> InsertOrUpdateUserSpiritCardAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritCards>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritCards>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritCards>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritCards>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin SpiritCard gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectSpiritCardSQL = @"
            SELECT * FROM spirit_cards 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            SpiritCards spiritCard = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectSpiritCardSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    spiritCard = MappingExtensionsHelper.MapSpiritCardFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    spiritCard.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (spiritCard == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<SpiritCards>.Failure(MessageConstants.SPIRIT_CARDS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_spirit_cards (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_spirit_cards (
                user_id, spirit_card_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                spiritCard.Quantity = purchaseCount;
                MappingExtensionsHelper.AddSpiritCardParameters(updateOrInsertObjectCommand, userId, spiritCard);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.SPIRIT_CARDS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<SpiritCards>.Inserted(spiritCard);
            }
            else
            {
                return InsertOrUpdateResult<SpiritCards>.Updated(spiritCard);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<SpiritCards>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Artifacts>> InsertOrUpdateUserArtifactAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artifacts>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artifacts>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artifacts>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artifacts>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Artifact gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectArtifactSQL = @"
            SELECT * FROM artifacts 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Artifacts artifact = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectArtifactSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    artifact = MappingExtensionsHelper.MapArtifactFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    artifact.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (artifact == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Artifacts>.Failure(MessageConstants.ARTIFACTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_artifacts (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_artifacts (
                user_id, artifact_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                artifact.Quantity = purchaseCount;
                MappingExtensionsHelper.AddArtifactParameters(updateOrInsertObjectCommand, userId, artifact);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.ARTIFACTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Artifacts>.Inserted(artifact);
            }
            else
            {
                return InsertOrUpdateResult<Artifacts>.Updated(artifact);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Artifacts>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Architectures>> InsertOrUpdateUserArchitectureAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Architectures>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Architectures>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Architectures>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Architectures>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Architecture gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectArchitectureSQL = @"
            SELECT * FROM architectures 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Architectures architecture = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectArchitectureSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    architecture = MappingExtensionsHelper.MapArchitectureFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    architecture.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (architecture == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Architectures>.Failure(MessageConstants.ARCHITECTURES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_architectures (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_architectures (
                user_id, architecture_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                architecture.Quantity = purchaseCount;
                MappingExtensionsHelper.AddArchitectureParameters(updateOrInsertObjectCommand, userId, architecture);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.ARCHITECTURES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Architectures>.Inserted(architecture);
            }
            else
            {
                return InsertOrUpdateResult<Architectures>.Updated(architecture);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Architectures>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Technologies>> InsertOrUpdateUserTechnologyAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Technologies>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Technologies>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Technologies>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Technologies>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Technology gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectTechnologySQL = @"
            SELECT * FROM technologies 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Technologies technology = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectTechnologySQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    technology = MappingExtensionsHelper.MapTechnologyFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    technology.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (technology == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Technologies>.Failure(MessageConstants.TECHNOLOGIES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_technologies (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_technologies (
                user_id, technologie_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                technology.Quantity = purchaseCount;
                MappingExtensionsHelper.AddTechnologyParameters(updateOrInsertObjectCommand, userId, technology);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.TECHNOLOGIES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Technologies>.Inserted(technology);
            }
            else
            {
                return InsertOrUpdateResult<Technologies>.Updated(technology);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Technologies>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Vehicles>> InsertOrUpdateUserVehicleAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Vehicles>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Vehicles>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Vehicles>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Vehicles>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Vehicle gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectVehicleSQL = @"
            SELECT * FROM vehicles 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Vehicles vehicle = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectVehicleSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    vehicle = MappingExtensionsHelper.MapVehicleFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    vehicle.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (vehicle == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Vehicles>.Failure(MessageConstants.VEHICLES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_vehicles (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_vehicles (
                user_id, vehicle_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                vehicle.Quantity = purchaseCount;
                MappingExtensionsHelper.AddVehicleParameters(updateOrInsertObjectCommand, userId, vehicle);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.VEHICLES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Vehicles>.Inserted(vehicle);
            }
            else
            {
                return InsertOrUpdateResult<Vehicles>.Updated(vehicle);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Vehicles>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Cores>> InsertOrUpdateUserCoreAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Cores>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Cores>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Cores>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Cores>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Core gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectCoreSQL = @"
            SELECT * FROM cores 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Cores core = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectCoreSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    core = MappingExtensionsHelper.MapCoreFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    core.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (core == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Cores>.Failure(MessageConstants.CORES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_cores (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_cores (
                user_id, core_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                core.Quantity = purchaseCount;
                MappingExtensionsHelper.AddCoreParameters(updateOrInsertObjectCommand, userId, core);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.CORES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Cores>.Inserted(core);
            }
            else
            {
                return InsertOrUpdateResult<Cores>.Updated(core);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Cores>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Weapons>> InsertOrUpdateUserWeaponAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Weapons>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Weapons>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Weapons>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Weapons>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Weapon gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectWeaponSQL = @"
            SELECT * FROM weapons 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Weapons weapon = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectWeaponSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    weapon = MappingExtensionsHelper.MapWeaponFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    weapon.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (weapon == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Weapons>.Failure(MessageConstants.WEAPONS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_weapons (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_weapons (
                user_id, weapon_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                weapon.Quantity = purchaseCount;
                MappingExtensionsHelper.AddWeaponParameters(updateOrInsertObjectCommand, userId, weapon);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.WEAPONS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Weapons>.Inserted(weapon);
            }
            else
            {
                return InsertOrUpdateResult<Weapons>.Updated(weapon);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Weapons>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Robots>> InsertOrUpdateUserRobotAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Robots>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Robots>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Robots>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Robots>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Robot gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectRobotSQL = @"
            SELECT * FROM robots 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Robots robot = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectRobotSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    robot = MappingExtensionsHelper.MapRobotFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    robot.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (robot == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Robots>.Failure(MessageConstants.ROBOTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_robots (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_robots (
                user_id, robot_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                robot.Quantity = purchaseCount;
                MappingExtensionsHelper.AddRobotParameters(updateOrInsertObjectCommand, userId, robot);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.ROBOTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Robots>.Inserted(robot);
            }
            else
            {
                return InsertOrUpdateResult<Robots>.Updated(robot);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Robots>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Badges>> InsertOrUpdateUserBadgeAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Badges>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Badges>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Badges>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Badges>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Badge gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectBadgeSQL = @"
            SELECT * FROM badges 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Badges badge = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectBadgeSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    badge = MappingExtensionsHelper.MapBadgeFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    badge.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (badge == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Badges>.Failure(MessageConstants.BADGES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_badges (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_badges (
                user_id, badge_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                badge.Quantity = purchaseCount;
                MappingExtensionsHelper.AddBadgeParameters(updateOrInsertObjectCommand, userId, badge);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.BADGES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Badges>.Inserted(badge);
            }
            else
            {
                return InsertOrUpdateResult<Badges>.Updated(badge);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Badges>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<MechaBeasts>> InsertOrUpdateUserMechaBeastAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MechaBeasts>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MechaBeasts>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MechaBeasts>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MechaBeasts>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin MechaBeast gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectMechaBeastSQL = @"
            SELECT * FROM mecha_beasts 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            MechaBeasts mechaBeast = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectMechaBeastSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    mechaBeast = MappingExtensionsHelper.MapMechaBeastFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    mechaBeast.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (mechaBeast == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<MechaBeasts>.Failure(MessageConstants.MECHA_BEASTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_mecha_beasts (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_mecha_beasts (
                user_id, mecha_beast_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                mechaBeast.Quantity = purchaseCount;
                MappingExtensionsHelper.AddMechaBeastParameters(updateOrInsertObjectCommand, userId, mechaBeast);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.MECHA_BEASTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<MechaBeasts>.Inserted(mechaBeast);
            }
            else
            {
                return InsertOrUpdateResult<MechaBeasts>.Updated(mechaBeast);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<MechaBeasts>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Runes>> InsertOrUpdateUserRuneAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Runes>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Runes>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Runes>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Runes>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Rune gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectRuneSQL = @"
            SELECT * FROM runes 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Runes rune = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectRuneSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    rune = MappingExtensionsHelper.MapRuneFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    rune.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (rune == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Runes>.Failure(MessageConstants.RUNES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_runes (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_runes (
                user_id, rune_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                rune.Quantity = purchaseCount;
                MappingExtensionsHelper.AddRuneParameters(updateOrInsertObjectCommand, userId, rune);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.RUNES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Runes>.Inserted(rune);
            }
            else
            {
                return InsertOrUpdateResult<Runes>.Updated(rune);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Runes>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Furnitures>> InsertOrUpdateUserFurnitureAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Furnitures>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Furnitures>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Furnitures>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Furnitures>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Furniture gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectFurnitureSQL = @"
            SELECT * FROM furnitures 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Furnitures furniture = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectFurnitureSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    furniture = MappingExtensionsHelper.MapFurnitureFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    furniture.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (furniture == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Furnitures>.Failure(MessageConstants.FURNITURES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_furnitures (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_furnitures (
                user_id, furniture_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                furniture.Quantity = purchaseCount;
                MappingExtensionsHelper.AddFurnitureParameters(updateOrInsertObjectCommand, userId, furniture);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.FURNITURES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Furnitures>.Inserted(furniture);
            }
            else
            {
                return InsertOrUpdateResult<Furnitures>.Updated(furniture);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Furnitures>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Foods>> InsertOrUpdateUserFoodAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Foods>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Foods>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Foods>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Foods>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Food gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectFoodSQL = @"
            SELECT * FROM foods 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Foods food = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectFoodSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    food = MappingExtensionsHelper.MapFoodFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    food.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (food == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Foods>.Failure(MessageConstants.FOODS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_foods (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_foods (
                user_id, food_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                food.Quantity = purchaseCount;
                MappingExtensionsHelper.AddFoodParameters(updateOrInsertObjectCommand, userId, food);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.FOODS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Foods>.Inserted(food);
            }
            else
            {
                return InsertOrUpdateResult<Foods>.Updated(food);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Foods>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Beverages>> InsertOrUpdateUserBeverageAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Beverages>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Beverages>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Beverages>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Beverages>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Beverage gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectBeverageSQL = @"
            SELECT * FROM beverages 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Beverages beverage = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectBeverageSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    beverage = MappingExtensionsHelper.MapBeverageFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    beverage.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (beverage == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Beverages>.Failure(MessageConstants.BEVERAGES_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_beverages (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_beverages (
                user_id, beverage_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                beverage.Quantity = purchaseCount;
                MappingExtensionsHelper.AddBeverageParameters(updateOrInsertObjectCommand, userId, beverage);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.BEVERAGES);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Beverages>.Inserted(beverage);
            }
            else
            {
                return InsertOrUpdateResult<Beverages>.Updated(beverage);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Beverages>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Buildings>> InsertOrUpdateUserBuildingAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Buildings>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Buildings>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Buildings>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Buildings>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Building gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectBuildingSQL = @"
            SELECT * FROM buildings 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Buildings building = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectBuildingSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    building = MappingExtensionsHelper.MapBuildingFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    building.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (building == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Buildings>.Failure(MessageConstants.BUILDINGS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_buildings (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_buildings (
                user_id, building_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                building.Quantity = purchaseCount;
                MappingExtensionsHelper.AddBuildingParameters(updateOrInsertObjectCommand, userId, building);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.BUILDINGS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Buildings>.Inserted(building);
            }
            else
            {
                return InsertOrUpdateResult<Buildings>.Updated(building);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Buildings>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Plants>> InsertOrUpdateUserPlantAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Plants>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Plants>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Plants>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Plants>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Plant gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectPlantSQL = @"
            SELECT * FROM plants 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Plants plant = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectPlantSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    plant = MappingExtensionsHelper.MapPlantFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    plant.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (plant == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Plants>.Failure(MessageConstants.PLANTS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_plants (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_plants (
                user_id, plant_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                plant.Quantity = purchaseCount;
                MappingExtensionsHelper.AddPlantParameters(updateOrInsertObjectCommand, userId, plant);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.PLANTS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Plants>.Inserted(plant);
            }
            else
            {
                return InsertOrUpdateResult<Plants>.Updated(plant);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Plants>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Fashions>> InsertOrUpdateUserFashionAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Fashions>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Fashions>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Fashions>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Fashions>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Fashion gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectFashionSQL = @"
            SELECT * FROM fashions 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Fashions fashion = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectFashionSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    fashion = MappingExtensionsHelper.MapFashionFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    fashion.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (fashion == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Fashions>.Failure(MessageConstants.FASHIONS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_fashions (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_fashions (
                user_id, fashion_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                fashion.Quantity = purchaseCount;
                MappingExtensionsHelper.AddFashionParameters(updateOrInsertObjectCommand, userId, fashion);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.FASHIONS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Fashions>.Inserted(fashion);
            }
            else
            {
                return InsertOrUpdateResult<Fashions>.Updated(fashion);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Fashions>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Emojis>> InsertOrUpdateUserEmojiAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Emojis>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Emojis>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Emojis>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Emojis>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Emoji gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectEmojiSQL = @"
            SELECT * FROM emojis 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Emojis emoji = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectEmojiSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    emoji = MappingExtensionsHelper.MapEmojiFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    emoji.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (emoji == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Emojis>.Failure(MessageConstants.EMOJIS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_emojis (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_emojis (
                user_id, emoji_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                emoji.Quantity = purchaseCount;
                MappingExtensionsHelper.AddEmojiParameters(updateOrInsertObjectCommand, userId, emoji);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.EMOJIS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Emojis>.Inserted(emoji);
            }
            else
            {
                return InsertOrUpdateResult<Emojis>.Updated(emoji);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Emojis>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Outfits>> InsertOrUpdateUserOutfitAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // 1. Khởi tạo Transaction
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1.5. Kiểm tra trạng thái User
            var userCheckResult = await CheckUserStatusAsync(userId, connection, transaction);
            if (!userCheckResult.IsSuccess)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Outfits>.Failure(userCheckResult.Message);
            }

            // 2. Kiểm tra Shop và Vật phẩm trong Shop (Shop_ID, ObjectId, Active = true, Deleted = false)
            // Đồng thời LEFT JOIN tới user_shop_purchase để kiểm tra hạn mức mua (Buy Limit)
            string checkShopItemSQL = @"
            SELECT 
                s.shop_id AS shop_id,
                si.buy_limit_per_user,
                COALESCE(usp.purchase_count, 0) AS total_purchased
            FROM shops s
            INNER JOIN shop_details si ON s.shop_id = si.shop_id
            LEFT JOIN user_shop_purchase usp ON usp.user_id = @user_id 
                AND usp.shop_id = si.shop_id 
                AND usp.object_id = si.object_id
            WHERE s.shop_id = @shop_id 
                AND si.object_id = @object_id
                AND s.is_active = TRUE AND s.is_deleted = FALSE
                AND si.is_active = TRUE AND si.is_deleted = FALSE
            FOR UPDATE;"; // Khóa dòng tránh Race Condition khi giao dịch đồng thời

            int buyLimit = 0;
            int totalPurchased = 0;
            bool shopItemExists = false;

            await using (MySqlCommand checkShopCommand = new MySqlCommand(checkShopItemSQL, connection, transaction))
            {
                checkShopCommand.Parameters.AddWithValue("@user_id", userId);
                checkShopCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                checkShopCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkShopCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    shopItemExists = true;
                    buyLimit = reader.GetInt32("buy_limit_per_user");
                    totalPurchased = reader.GetInt32("total_purchased");
                }
            }

            if (!shopItemExists)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Outfits>.Failure(MessageConstants.ITEM_NOT_FOUND_OR_INACTIVE);
            }

            // Kiểm tra giới hạn mua của người dùng
            if (buyLimit > 0 && (totalPurchased + purchaseCount) > buyLimit)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Outfits>.Failure($"Đã vượt quá giới hạn mua ({totalPurchased}/{buyLimit}).");
            }

            // 3. Kiểm tra số dư tiền tệ của User (Dùng FOR UPDATE để khóa ví tiền)
            double totalCost = shopDTO.ShopDetail.Price * purchaseCount;
            string checkBalanceSQL = @"
            SELECT quantity 
            FROM user_currencies 
            WHERE user_id = @user_id AND currency_id = @currency_id 
            FOR UPDATE;";

            double userBalance = 0;
            bool hasCurrencyRecord = false;

            await using (MySqlCommand checkBalanceCommand = new MySqlCommand(checkBalanceSQL, connection, transaction))
            {
                checkBalanceCommand.Parameters.AddWithValue("@user_id", userId);
                checkBalanceCommand.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);

                await using MySqlDataReader reader = (MySqlDataReader)await checkBalanceCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    hasCurrencyRecord = true;
                    userBalance = Convert.ToDouble(reader["quantity"]);
                }
            }

            if (!hasCurrencyRecord || userBalance < totalCost)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Outfits>.Failure(MessageConstants.INSUFFICIENT_BALANCE);
            }

            // 4. Trừ tiền của User
            string deductCurrencySQL = @"
            UPDATE user_currencies 
            SET quantity = quantity - @total_cost 
            WHERE user_id = @user_id AND currency_id = @currency_id;";

            await using (MySqlCommand deductCmd = new MySqlCommand(deductCurrencySQL, connection, transaction))
            {
                deductCmd.Parameters.AddWithValue("@total_cost", totalCost);
                deductCmd.Parameters.AddWithValue("@user_id", userId);
                deductCmd.Parameters.AddWithValue("@currency_id", shopDTO.ShopDetail.CurrencyId);
                await deductCmd.ExecuteNonQueryAsync();
            }

            // 5. Query thông tin Outfit gốc và gán số lượng nhận được (ObjectQuantity * purchaseCount)
            string selectOutfitSQL = @"
            SELECT * FROM outfits 
            WHERE id = @object_id AND is_active = TRUE AND is_deleted = FALSE;";

            Outfits outfit = null;

            await using (MySqlCommand selectCardCmd = new MySqlCommand(selectOutfitSQL, connection, transaction))
            {
                selectCardCmd.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);

                await using MySqlDataReader reader = (MySqlDataReader)await selectCardCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    outfit = MappingExtensionsHelper.MapOutfitFromReader(reader);
                    // Số lượng thẻ thực nhận = ObjectQuantity thiết lập trong ShopDetail * Số lần mua
                    outfit.Quantity = Convert.ToInt32(shopDTO.ShopDetail.ObjectQuantity * purchaseCount);
                }
            }

            if (outfit == null)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<Outfits>.Failure(MessageConstants.OUTFITS_NOT_FOUND);
            }

            // 6. UPSERT vào bảng user_outfits (Cộng dồn số lượng)
            string updateOrInsertUserObjectSQL = @"
            INSERT INTO user_outfits (
                user_id, outfit_id, rare, level, experience, star, quality, block, quantity,
                power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate,
                evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate,
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                mana, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate,
                skill_damage_rate, skill_resistance_rate
            ) VALUES (
                @user_id, @object_id, @rare, 0, 0, 0, @quality, false, @quantity,
                @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate,
                @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate,
                @skill_damage_rate, @skill_resistance_rate
            )
            ON DUPLICATE KEY UPDATE 
                quantity = quantity + VALUES(quantity);";

            bool isInserted = false;

            await using (MySqlCommand updateOrInsertObjectCommand = new MySqlCommand(updateOrInsertUserObjectSQL, connection, transaction))
            {
                outfit.Quantity = purchaseCount;
                MappingExtensionsHelper.AddOutfitParameters(updateOrInsertObjectCommand, userId, outfit);

                // Đọc số dòng bị ảnh hưởng bởi câu lệnh UPSERT
                int rowsAffected = await updateOrInsertObjectCommand.ExecuteNonQueryAsync();

                // Nếu rowsAffected == 1 => Mới chèn dòng mới (Insert)
                // Nếu rowsAffected == 2 => Đã tồn tại và được cập nhật (Update)
                isInserted = (rowsAffected == 1);
            }

            // 7. Cập nhật lượt mua trong user_shop_purchase
            string updateOrInsertPurchaseSQL = @"
            INSERT INTO user_shop_purchase (user_id, shop_id, object_id, object_type, purchase_count)
            VALUES (@user_id, @shop_id, @object_id, @object_type, @purchase_count)
            ON DUPLICATE KEY UPDATE 
                purchase_count = purchase_count + VALUES(purchase_count);";

            await using (MySqlCommand updateOrInsertPurchaseCommand = new MySqlCommand(updateOrInsertPurchaseSQL, connection, transaction))
            {
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@user_id", userId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@shop_id", shopDTO.ShopId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_id", shopDTO.ShopDetail.ObjectId);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@object_type", shopDTO.ShopDetail.ObjectType ?? AppConstants.ObjectType.OUTFITS);
                updateOrInsertPurchaseCommand.Parameters.AddWithValue("@purchase_count", purchaseCount);
                await updateOrInsertPurchaseCommand.ExecuteNonQueryAsync();
            }

            // 8. Commit toàn bộ giao dịch
            await transaction.CommitAsync();

            if (isInserted)
            {
                return InsertOrUpdateResult<Outfits>.Inserted(outfit);
            }
            else
            {
                return InsertOrUpdateResult<Outfits>.Updated(outfit);
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError("Database Transaction Error: " + ex.Message);
            return InsertOrUpdateResult<Outfits>.Failure(ex.Message);
        }
    }
}