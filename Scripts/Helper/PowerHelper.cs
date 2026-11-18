using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
public static class PowerHelper
{
    // DICTIONARY CACHE: Lưu kết quả của từng nguồn/loại vật phẩm (Key: Tên nguồn, Value: UserPowers)
    public static Dictionary<string, UserPowers> _powerCache = new Dictionary<string, UserPowers>();
    // Thêm cờ đánh dấu trạng thái đã khởi tạo hay chưa
    private static bool _isInitialized = false;
    private static Task _initializingTask = null; // Chống gọi trùng lặp nếu nhiều nơi gọi cùng lúc
    public static void RecalculatePower(this UserPowers userPower)
    {
        userPower.Power = CalculatePower(
            userPower.Health,
            userPower.PhysicalAttack, userPower.PhysicalDefense,
            userPower.MagicalAttack, userPower.MagicalDefense,
            userPower.ChemicalAttack, userPower.ChemicalDefense,
            userPower.AtomicAttack, userPower.AtomicDefense,
            userPower.MentalAttack, userPower.MentalDefense,
            userPower.Speed,
            userPower.CriticalDamageRate, userPower.CriticalRate, userPower.CriticalResistanceRate, userPower.IgnoreCriticalRate,
            userPower.PenetrationRate, userPower.PenetrationResistanceRate, userPower.EvasionRate,
            userPower.DamageAbsorptionRate, userPower.IgnoreDamageAbsorptionRate, userPower.AbsorbedDamageRate,
            userPower.VitalityRegenerationRate, userPower.VitalityRegenerationResistanceRate,
            userPower.AccuracyRate, userPower.LifestealRate,
            userPower.ShieldStrength, userPower.Tenacity, userPower.ResistanceRate,
            userPower.ComboRate, userPower.IgnoreComboRate, userPower.ComboDamageRate, userPower.ComboResistanceRate,
            userPower.StunRate, userPower.IgnoreStunRate,
            userPower.ReflectionRate, userPower.IgnoreReflectionRate, userPower.ReflectionDamageRate, userPower.ReflectionResistanceRate,
            userPower.Mana, userPower.ManaRegenerationRate,
            userPower.DamageToDifferentFactionRate, userPower.ResistanceToDifferentFactionRate,
            userPower.DamageToSameFactionRate, userPower.ResistanceToSameFactionRate,
            userPower.NormalDamageRate, userPower.NormalResistanceRate,
            userPower.SkillDamageRate, userPower.SkillResistanceRate
        );
    }
    public static double CalculatePower(
        double health, double physicalAttack, double physicalDefense, double magicalAttack, double magicalDefense,
        double chemicalAttack, double chemicalDefense, double atomicAttack, double atomicDefense, double mentalAttack, double mentalDefense,
        double speed, double criticalDamageRate, double criticalRate, double criticalResistanceRate, double ignoreCriticalRate,
        double penetrationRate, double penetrationResistanceRate, double evasionRate,
        double damageAbsorptionRate, double ignoreDamageAbsorptionRate, double absorbedDamageRate,
        double vitalityRegenerationRate, double vitalityRegenerationResistanceRate, double accuracyRate, double lifestealRate,
        double shieldStrength, double tenacity, double resistanceRate, double comboRate, double ignoreComboRate, double comboDamageRate, double comboResistanceRate,
        double stunRate, double ignoreStunRate, double reflectionRate, double ignoreReflectionRate, double reflectionDamageRate, double reflectionResistanceRate,
        double mana, double manaRegenerationRate,
        double damageToDifferentFactionRate, double resistanceToDifferentFactionRate,
        double damageToSameFactionRate, double resistanceToSameFactionRate,
        double normalDamageRate, double normalResistanceRate,
        double skillDamageRate, double skillResistanceRate
    )
    {
        double weight = 0.5;

        double totalAttack = (physicalAttack + magicalAttack + chemicalAttack + atomicAttack + mentalAttack) * weight;
        double totalDefense = (physicalDefense + magicalDefense + chemicalDefense + atomicDefense + mentalDefense) * weight + shieldStrength * weight;

        // Điều chỉnh các chỉ số tỷ lệ
        double adjusted_critical_rate = (criticalRate / 100) * totalAttack / 100;
        double adjusted_critical_damage = (criticalDamageRate / 100) * totalAttack / 100;
        double adjusted_critical_resistance_rate = (criticalResistanceRate / 100) * totalDefense / 100;
        double adjusted_ignore_critical_rate = (ignoreCriticalRate / 100) * totalAttack / 100;
        double adjusted_penetration = (penetrationRate / 100) * totalAttack / 100;
        double adjusted_penetration_resistance_rate = (penetrationResistanceRate / 100) * totalDefense / 100;
        double adjusted_evasion = (evasionRate / 100) * totalDefense / 100;
        double adjusted_absorption = (damageAbsorptionRate / 100) * totalDefense / 100;
        double adjusted_ignore_damage_absorption_rate = (ignoreDamageAbsorptionRate / 100) * totalDefense / 100;
        double adjusted_absorbed_damage_rate = (absorbedDamageRate / 100) * totalDefense / 100;
        double adjusted_regeneration = (vitalityRegenerationRate / 100) * health / 100;
        double adjusted_vitality_regeneration_resistance_rate = (vitalityRegenerationResistanceRate / 100) * health / 100;
        double adjusted_accuracy = (accuracyRate / 100) * totalAttack / 100;
        double adjusted_lifesteal = (lifestealRate / 100) * totalAttack / 100;
        double adjusted_tenacity = (tenacity / 100) * totalDefense / 100;
        double adjusted_resistance = (resistanceRate / 100) * (totalDefense + health * 0.5) / 100;
        double adjusted_combo = (comboRate / 100) * totalAttack / 100;
        double adjusted_ignore_combo_rate = (ignoreComboRate / 100) * totalAttack / 100;
        double adjusted_combo_damage_rate = (comboDamageRate / 100) * totalAttack / 100;
        double adjusted_combo_resistance_rate = (comboResistanceRate / 100) * totalDefense / 100;
        double adjusted_stun_rate = (stunRate / 100) * totalAttack / 100;
        double adjusted_ignore_stun_rate = (ignoreStunRate / 100) * totalAttack / 100;
        double adjusted_reflection = (reflectionRate / 100) * (totalDefense + health * 0.5) / 100;
        double adjusted_ignore_reflection_rate = (ignoreReflectionRate / 100) * totalAttack / 100;
        double adjusted_reflection_damage_rate = (reflectionDamageRate / 100) * totalAttack / 100;
        double adjusted_reflection_resistance_rate = (reflectionResistanceRate / 100) * totalDefense / 100;

        // Điều chỉnh thuộc tính faction
        double adjusted_damage_to_different_faction = (damageToDifferentFactionRate / 100) * totalAttack / 100;
        double adjusted_resistance_to_different_faction = (resistanceToDifferentFactionRate / 100) * totalDefense / 100;
        double adjusted_damage_to_same_faction = (damageToSameFactionRate / 100) * totalAttack / 100;
        double adjusted_resistance_to_same_faction = (resistanceToSameFactionRate / 100) * totalDefense / 100;

        double adjusted_normal_damage_rate = (normalDamageRate / 100) * totalAttack / 100;
        double adjusted_normal_resistance_rate = (normalResistanceRate / 100) * totalDefense / 100;
        double adjusted_skill_damage_rate = (skillDamageRate / 100) * totalAttack / 100;
        double adjusted_skill_resistance_rate = (skillResistanceRate / 100) * totalDefense / 100;

        // Điều chỉnh mana
        double adjustedMana = mana * 0.5;
        double adjustedManaRegeneration = (manaRegenerationRate / 100) * mana;

        // Tổng sức mạnh
        return (health * weight + totalAttack + totalDefense + speed * weight +
        adjusted_critical_rate + adjusted_critical_damage + adjusted_critical_resistance_rate + adjusted_ignore_critical_rate +
        adjusted_penetration + adjusted_penetration_resistance_rate +
        adjusted_evasion + adjusted_absorption + adjusted_ignore_damage_absorption_rate + adjusted_absorbed_damage_rate +
        adjusted_regeneration + adjusted_vitality_regeneration_resistance_rate + adjusted_accuracy +
        adjusted_lifesteal + adjusted_tenacity + adjusted_resistance +
        adjusted_combo + adjusted_ignore_combo_rate + adjusted_combo_damage_rate + adjusted_combo_resistance_rate +
        adjusted_stun_rate + adjusted_ignore_stun_rate +
        adjusted_reflection + adjusted_ignore_reflection_rate + adjusted_reflection_damage_rate + adjusted_reflection_resistance_rate +
        adjusted_damage_to_different_faction + adjusted_resistance_to_different_faction +
        adjusted_damage_to_same_faction + adjusted_resistance_to_same_faction +
        adjustedMana + adjustedManaRegeneration +
        adjusted_normal_damage_rate + adjusted_normal_resistance_rate +
        adjusted_skill_damage_rate + adjusted_skill_resistance_rate) / 10;
    }
    /// <summary>
    /// Hàm khởi tạo gọi 1 LẦN DUY NHẤT khi vào game:
    /// Gọi 3 hàm phân loại (lưu vào dictionary), sau đó sum lại thành tổng lực chiến.
    /// </summary>
    public static async Task InitializeGamePowersAsync(string userId)
    {
        // Nếu đang có một tiến trình khởi tạo chạy rồi thì chờ nó chạy xong, không chạy đè
        if (_initializingTask != null && !_initializingTask.IsCompleted)
        {
            await _initializingTask;
            return;
        }

        _initializingTask = InternalInitializeAsync(userId);
        await _initializingTask;
    }

    private static async Task InternalInitializeAsync(string userId)
    {
        _powerCache.Clear();

        // QUAN TRỌNG: Phải reset User.UserPower về object trắng trước khi Sum để tránh cộng dồn đè lên dữ liệu cũ
        User.UserPower = new UserPowers();

        await Task.WhenAll(
            CalculatePercentPowerAsync(userId),
            CalculateGalleryPowerAsync(userId),
            CalculateUserObjectPowerAsync(userId),
            CalculateUserExtraPowerAsync(userId)
        );

        SumPowersFromCache();
        User.UserPower.RecalculatePower();

        _isInitialized = true;
    }

    /// <summary>
    /// 1. Hàm CLEAR: Dọn dẹp toàn bộ Dictionary cache và reset tổng lực chiến về 0.
    /// (Thường gọi khi người chơi đăng xuất hoặc đổi nhân vật).
    /// </summary>
    public static void Clear()
    {
        _powerCache.Clear();
        _isInitialized = false; // Reset lại cờ khi Clear (đăng xuất / đổi nhân vật)
        _initializingTask = null;
        User.UserPower = new UserPowers();
    }

    /// <summary>
    /// Overload cũ cho 1 nguồn (Giữ nguyên tương thích ngược)
    /// </summary>
    public static Task<PowerResultDTO> RefreshAndSyncTotalPowerAsync(
        string cacheKey,
        Func<string, Task<UserPowers>> fetchFunc)
    {
        return RefreshAndSyncTotalPowerAsync((cacheKey, fetchFunc));
    }

    /// <summary>
    /// Hàm mới: Refresh đồng thời nhiều nguồn chỉ số, gom lại tính RAM 1 lần và lưu DB 1 lần
    /// </summary>
    public static async Task<PowerResultDTO> RefreshAndSyncTotalPowerAsync(
        params (string cacheKey, Func<string, Task<UserPowers>> fetchFunc)[] sources)
    {
        if (sources == null || sources.Length == 0)
        {
            return new PowerResultDTO
            {
                HasChanged = false,
                CurrentPower = User.CurrentUserPower,
                NewPower = User.CurrentUserPower
            };
        }

        string userId = User.CurrentUserId;
        double oldPower = User.CurrentUserPower;

        // 1. Nếu chưa khởi tạo cache lần nào thì khởi tạo toàn bộ (đã bao gồm tất cả nguồn)
        if (!_isInitialized)
        {
            await InitializeGamePowersAsync(userId);
        }
        else
        {
            // Chạy song song tất cả các query lấy chỉ số mới của các nguồn bị ảnh hưởng
            var fetchTasks = new Task<UserPowers>[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                fetchTasks[i] = sources[i].fetchFunc(userId);
            }

            UserPowers[] results = await Task.WhenAll(fetchTasks);

            // Cập nhật Delta cho từng nguồn vào User.UserPower
            bool anySourceChanged = false;
            for (int i = 0; i < sources.Length; i++)
            {
                string key = sources[i].cacheKey;
                UserPowers updatedPower = results[i];
                if (updatedPower == null) continue;

                bool hasChanged = true;
                if (_powerCache.TryGetValue(key, out UserPowers oldSourcePower))
                {
                    UserPowers delta = CalculateDelta(oldSourcePower, updatedPower);
                    hasChanged = !IsDeltaZero(delta);
                }

                if (hasChanged)
                {
                    UpdateAndApplyDelta(key, updatedPower);
                    anySourceChanged = true;
                }
            }

            // Nếu không có nguồn nào thay đổi chỉ số -> Thoát sớm, không tính lại thẻ hay ghi DB
            if (!anySourceChanged)
            {
                return new PowerResultDTO
                {
                    HasChanged = false,
                    CurrentPower = oldPower,
                    NewPower = oldPower
                };
            }
        }

        // 2. Tính lại chỉ số & lực chiến đội hình trên RAM đúng 1 lần duy nhất
        var teamsService = TeamsService.Create();
        teamsService.RecalculateTeamStats();

        // 3. Sum tổng lực chiến Team & lưu DB đúng 1 lần duy nhất
        double newPower = await teamsService.GetTeamsPowerAsync(userId);

        return new PowerResultDTO
        {
            HasChanged = Math.Abs(newPower - oldPower) > 0.0001,
            CurrentPower = oldPower,
            NewPower = newPower
        };
    }

    private static async Task<bool> RefreshPowerSourceAsync(
        string cacheKey,
        Func<string, Task<UserPowers>> fetchFunc,
        string userId)
    {
        if (!_isInitialized)
        {
            await InitializeGamePowersAsync(userId);
            return true;
        }

        UserPowers updatedPower = await fetchFunc(userId);

        bool hasChanged = true;
        if (_powerCache.TryGetValue(cacheKey, out UserPowers oldPower))
        {
            UserPowers delta = CalculateDelta(oldPower, updatedPower);
            hasChanged = !IsDeltaZero(delta);
        }

        if (hasChanged)
        {
            UpdateAndApplyDelta(cacheKey, updatedPower);
        }

        return hasChanged;
    }

    private static bool IsDeltaZero(UserPowers delta)
    {
        return Math.Abs(delta.Power) < 0.0001 &&
               Math.Abs(delta.Health) < 0.0001 &&
               Math.Abs(delta.PhysicalAttack) < 0.0001 &&
               Math.Abs(delta.MagicalAttack) < 0.0001 &&
               Math.Abs(delta.PercentAllHealth) < 0.0001 &&
               Math.Abs(delta.PercentAllPhysicalAttack) < 0.0001;
    }

    /// <summary>
    /// 2. Hàm tính DELTA và cập nhật lại power khi có sự thay đổi/chênh lệch dữ liệu của một nguồn.
    /// </summary>
    /// <param name="cacheKey">Khóa trong Dictionary (VD: "User_Books", "Percent_Achievements")</param>
    /// <param name="newPower">Đối tượng UserPowers mới sau khi thay đổi/nâng cấp</param>
    /// <summary>
    /// Cập nhật cache, cộng dồn chênh lệch vào User.UserPower và trả về lượng Lực Chiến (Power) tăng/giảm.
    /// </summary>
    public static double UpdateAndApplyDelta(string cacheKey, UserPowers newPower)
    {
        if (newPower == null) return 0;

        // 1. Lưu lại tổng lực chiến TRƯỚC khi cập nhật
        User.UserPower.RecalculatePower();
        double oldTotalPower = User.UserPower.Power;

        UserPowers deltaPower;

        // 2. So sánh với cache cũ và tính Delta
        if (_powerCache.TryGetValue(cacheKey, out UserPowers oldPower))
        {
            // Tính chênh lệch từng thuộc tính (Mới - Cũ)
            deltaPower = CalculateDelta(oldPower, newPower);

            // Thay thế hoàn toàn object mới vào cache
            _powerCache[cacheKey] = newPower;
        }
        else
        {
            // Chưa có trong cache -> thêm mới
            deltaPower = newPower;
            _powerCache[cacheKey] = newPower;
        }

        // 3. Cộng dồn Delta vào User.UserPower tổng
        AddPower(deltaPower);

        // 4. Tính lại tổng lực chiến SAU khi đã cộng dồn đầy đủ chỉ số (cả chỉ số thường lẫn %)
        User.UserPower.RecalculatePower();
        double newTotalPower = User.UserPower.Power;

        // 5. Trả về chính xác mức tăng (+) hoặc giảm (-) lực chiến để hiển thị UI
        return newTotalPower - oldTotalPower;
    }

    /// <summary>
    /// Hàm phụ trợ tính toán độ lệch (Delta) giữa chỉ số cũ và chỉ số mới
    /// </summary>
    private static UserPowers CalculateDelta(UserPowers oldP, UserPowers newP)
    {
        return new UserPowers
        {
            Power = newP.Power - oldP.Power,
            Health = newP.Health - oldP.Health,
            PhysicalAttack = newP.PhysicalAttack - oldP.PhysicalAttack,
            PhysicalDefense = newP.PhysicalDefense - oldP.PhysicalDefense,
            MagicalAttack = newP.MagicalAttack - oldP.MagicalAttack,
            MagicalDefense = newP.MagicalDefense - oldP.MagicalDefense,
            ChemicalAttack = newP.ChemicalAttack - oldP.ChemicalAttack,
            ChemicalDefense = newP.ChemicalDefense - oldP.ChemicalDefense,
            AtomicAttack = newP.AtomicAttack - oldP.AtomicAttack,
            AtomicDefense = newP.AtomicDefense - oldP.AtomicDefense,
            MentalAttack = newP.MentalAttack - oldP.MentalAttack,
            MentalDefense = newP.MentalDefense - oldP.MentalDefense,
            Speed = newP.Speed - oldP.Speed,
            CriticalDamageRate = newP.CriticalDamageRate - oldP.CriticalDamageRate,
            CriticalRate = newP.CriticalRate - oldP.CriticalRate,
            CriticalResistanceRate = newP.CriticalResistanceRate - oldP.CriticalResistanceRate,
            IgnoreCriticalRate = newP.IgnoreCriticalRate - oldP.IgnoreCriticalRate,
            PenetrationRate = newP.PenetrationRate - oldP.PenetrationRate,
            PenetrationResistanceRate = newP.PenetrationResistanceRate - oldP.PenetrationResistanceRate,
            EvasionRate = newP.EvasionRate - oldP.EvasionRate,
            DamageAbsorptionRate = newP.DamageAbsorptionRate - oldP.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = newP.IgnoreDamageAbsorptionRate - oldP.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = newP.AbsorbedDamageRate - oldP.AbsorbedDamageRate,
            VitalityRegenerationRate = newP.VitalityRegenerationRate - oldP.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = newP.VitalityRegenerationResistanceRate - oldP.VitalityRegenerationResistanceRate,
            AccuracyRate = newP.AccuracyRate - oldP.AccuracyRate,
            LifestealRate = newP.LifestealRate - oldP.LifestealRate,
            ShieldStrength = newP.ShieldStrength - oldP.ShieldStrength,
            Tenacity = newP.Tenacity - oldP.Tenacity,
            ResistanceRate = newP.ResistanceRate - oldP.ResistanceRate,
            ComboRate = newP.ComboRate - oldP.ComboRate,
            IgnoreComboRate = newP.IgnoreComboRate - oldP.IgnoreComboRate,
            ComboDamageRate = newP.ComboDamageRate - oldP.ComboDamageRate,
            ComboResistanceRate = newP.ComboResistanceRate - oldP.ComboResistanceRate,
            StunRate = newP.StunRate - oldP.StunRate,
            IgnoreStunRate = newP.IgnoreStunRate - oldP.IgnoreStunRate,
            ReflectionRate = newP.ReflectionRate - oldP.ReflectionRate,
            IgnoreReflectionRate = newP.IgnoreReflectionRate - oldP.IgnoreReflectionRate,
            ReflectionDamageRate = newP.ReflectionDamageRate - oldP.ReflectionDamageRate,
            ReflectionResistanceRate = newP.ReflectionResistanceRate - oldP.ReflectionResistanceRate,
            Mana = newP.Mana - oldP.Mana,
            ManaRegenerationRate = newP.ManaRegenerationRate - oldP.ManaRegenerationRate,
            DamageToDifferentFactionRate = newP.DamageToDifferentFactionRate - oldP.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = newP.ResistanceToDifferentFactionRate - oldP.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = newP.DamageToSameFactionRate - oldP.DamageToSameFactionRate,
            ResistanceToSameFactionRate = newP.ResistanceToSameFactionRate - oldP.ResistanceToSameFactionRate,
            NormalDamageRate = newP.NormalDamageRate - oldP.NormalDamageRate,
            NormalResistanceRate = newP.NormalResistanceRate - oldP.NormalResistanceRate,
            SkillDamageRate = newP.SkillDamageRate - oldP.SkillDamageRate,
            SkillResistanceRate = newP.SkillResistanceRate - oldP.SkillResistanceRate,

            PercentAllHealth = newP.PercentAllHealth - oldP.PercentAllHealth,
            PercentAllPhysicalAttack = newP.PercentAllPhysicalAttack - oldP.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = newP.PercentAllPhysicalDefense - oldP.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = newP.PercentAllMagicalAttack - oldP.PercentAllMagicalAttack,
            PercentAllMagicalDefense = newP.PercentAllMagicalDefense - oldP.PercentAllMagicalDefense,
            PercentAllChemicalAttack = newP.PercentAllChemicalAttack - oldP.PercentAllChemicalAttack,
            PercentAllChemicalDefense = newP.PercentAllChemicalDefense - oldP.PercentAllChemicalDefense,
            PercentAllAtomicAttack = newP.PercentAllAtomicAttack - oldP.PercentAllAtomicAttack,
            PercentAllAtomicDefense = newP.PercentAllAtomicDefense - oldP.PercentAllAtomicDefense,
            PercentAllMentalAttack = newP.PercentAllMentalAttack - oldP.PercentAllMentalAttack,
            PercentAllMentalDefense = newP.PercentAllMentalDefense - oldP.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// 1. Nhóm Percent: Gọi các task lấy chỉ số %, sau đó lưu kết quả vào Dictionary cache
    /// </summary>
    public static async Task CalculatePercentPowerAsync(string userId)
    {
        var percentTasks = new Dictionary<string, Task<UserPowers>>
        {
            { AppConstants.Power.PERCENT_ACHIEVEMENTS, GetPercentAchievementsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_ALCHEMIES, GetPercentAlchemiesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_ARCHITECTURES, GetPercentArchitecturesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_ARTIFACTS, GetPercentArtifactsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_ARTWORKS, GetPercentArtworksPowerAsync(userId) },
            { AppConstants.Power.PERCENT_AVATARS, GetPercentAvatarsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_BADGES, GetPercentBadgesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_BEVERAGES, GetPercentBeveragesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_BOOKS, GetPercentBooksPowerAsync(userId) },
            { AppConstants.Power.PERCENT_BORDERS, GetPercentBordersPowerAsync(userId) },
            { AppConstants.Power.PERCENT_BUILDINGS, GetPercentBuildingsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_CARD_LIVES, GetPercentCardLivesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_COLLABORATION_EQUIPMENTS, GetPercentCollaborationEquipmentsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_COLLABORATIONS, GetPercentCollaborationsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_CORES, GetPercentCoresPowerAsync(userId) },
            { AppConstants.Power.PERCENT_EMOJIS, GetPercentEmojisPowerAsync(userId) },
            { AppConstants.Power.PERCENT_FASHIONS, GetPercentFashionsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_FOODS, GetPercentFoodsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_FORGES, GetPercentForgesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_FURNITURES, GetPercentFurnituresPowerAsync(userId) },
            { AppConstants.Power.PERCENT_MAGIC_FORMATION_CIRCLES, GetPercentMagicFormationCirclesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_MECHA_BEASTS, GetPercentMechaBeastsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_MEDALS, GetPercentMedalsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_OUTFITS, GetPercentOutfitsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_PETS, GetPercentPetsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_PLANTS, GetPercentPlantsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_PUPPETS, GetPercentPuppetsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_RELICS, GetPercentRelicsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_ROBOTS, GetPercentRobotsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_RUNES, GetPercentRunesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_SKILLS, GetPercentSkillsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_SPIRIT_BEASTS, GetPercentSpiritBeastsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_SPIRIT_CARDS, GetPercentSpiritCardsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_SYMBOLS, GetPercentSymbolsPowerAsync(userId) },
            { AppConstants.Power.PERCENT_TALISMANS, GetPercentTalismansPowerAsync(userId) },
            { AppConstants.Power.PERCENT_TECHNOLOGIES, GetPercentTechnologiesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_TITLES, GetPercentTitlesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_VEHICLES, GetPercentVehiclesPowerAsync(userId) },
            { AppConstants.Power.PERCENT_WEAPONS, GetPercentWeaponsPowerAsync(userId) }
        };

        foreach (var kvp in percentTasks)
        {
            var result = await kvp.Value;
            if (result != null)
            {
                // Lưu kết quả vào Dictionary
                _powerCache[kvp.Key] = result;
            }
        }
    }

    /// <summary>
    /// 2. Nhóm Gallery: Gọi các task lấy chỉ số bộ sưu tập, lưu kết quả vào Dictionary cache
    /// </summary>
    public static async Task CalculateGalleryPowerAsync(string userId)
    {
        var galleryTasks = new Dictionary<string, Task<UserPowers>>
        {
            { AppConstants.Power.GALLERY_ACHIEVEMENTS, GetAchievementsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_ALCHEMIES, GetAlchemiesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_ARCHITECTURES, GetArchitecturesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_ARTIFACTS, GetArtifactsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_ARTWORKS, GetArtworksGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_AVATARS, GetAvatarsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_BADGES, GetBadgesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_BEVERAGES, GetBeveragesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_BOOKS, GetBooksGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_BORDERS, GetBordersGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_BUILDINGS, GetBuildingsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_ADMIRALS, GetCardAdmiralsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_CAPTAINS, GetCardCaptainsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_COLONELS, GetCardColonelsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_GENERALS, GetCardGeneralsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_HEROES, GetCardHeroesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_LIVES, GetCardLivesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_MONSTERS, GetCardMonstersGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_MILITARIES, GetCardMilitariesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_SOLDIERS, GetCardSoldiersGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CARD_SPELLS, GetCardSpellsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_COLLABORATION_EQUIPMENTS, GetCollaborationEquipmentsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_COLLABORATIONS, GetCollaborationsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_CORES, GetCoresGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_EMOJIS, GetEmojisGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_EQUIPMENTS, GetEquipmentsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_FASHIONS, GetFashionsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_FOODS, GetFoodsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_FORGES, GetForgesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_FURNITURES, GetFurnituresGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_MAGIC_FORMATION_CIRCLES, GetMagicFormationCirclesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_MECHA_BEASTS, GetMechaBeastsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_MEDALS, GetMedalsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_OUTFITS, GetOutfitsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_PETS, GetPetsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_PLANTS, GetPlantsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_PUPPETS, GetPuppetsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_RELICS, GetRelicsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_ROBOTS, GetRobotsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_RUNES, GetRunesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_SKILLS, GetSkillsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_SPIRIT_BEASTS, GetSpiritBeastsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_SPIRIT_CARDS, GetSpiritCardsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_SYMBOLS, GetSymbolsGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_TALISMANS, GetTalismansGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_TECHNOLOGIES, GetTechnologiesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_TITLES, GetTitlesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_VEHICLES, GetVehiclesGalleryPowerAsync(userId) },
            { AppConstants.Power.GALLERY_WEAPONS, GetWeaponsGalleryPowerAsync(userId) }
        };

        foreach (var kvp in galleryTasks)
        {
            var result = await kvp.Value;
            if (result != null)
            {
                _powerCache[kvp.Key] = result;
            }
        }
    }

    /// <summary>
    /// 3. Nhóm User Object: Gọi các task lấy vật phẩm trực tiếp của user, lưu vào Dictionary cache
    /// </summary>
    public static async Task CalculateUserObjectPowerAsync(string userId)
    {
        var userObjectTasks = new Dictionary<string, Task<UserPowers>>
        {
            { AppConstants.Power.USER_ACHIEVEMENTS, GetUserAchievementsPowerAsync(userId) },
            { AppConstants.Power.USER_ALCHEMIES, GetUserAlchemiesPowerAsync(userId) },
            { AppConstants.Power.USER_ARCHITECTURES, GetUserArchitecturesPowerAsync(userId) },
            { AppConstants.Power.USER_ARTIFACTS, GetUserArtifactsPowerAsync(userId) },
            { AppConstants.Power.USER_ARTWORKS, GetUserArtworksPowerAsync(userId) },
            { AppConstants.Power.USER_AVATARS, GetUserAvatarsPowerAsync(userId) },
            { AppConstants.Power.USER_BADGES, GetUserBadgesPowerAsync(userId) },
            { AppConstants.Power.USER_BEVERAGES, GetUserBeveragesPowerAsync(userId) },
            { AppConstants.Power.USER_BOOKS, GetUserBooksPowerAsync(userId) },
            { AppConstants.Power.USER_BORDERS, GetUserBordersPowerAsync(userId) },
            { AppConstants.Power.USER_BUILDINGS, GetUserBuildingsPowerAsync(userId) },
            { AppConstants.Power.USER_CARD_LIVES, GetUserCardLivesPowerAsync(userId) },
            { AppConstants.Power.USER_COLLABORATION_EQUIPMENTS, GetUserCollaborationEquipmentsPowerAsync(userId) },
            { AppConstants.Power.USER_COLLABORATIONS, GetUserCollaborationsPowerAsync(userId) },
            { AppConstants.Power.USER_CORES, GetUserCoresPowerAsync(userId) },
            { AppConstants.Power.USER_EMOJIS, GetUserEmojisPowerAsync(userId) },
            { AppConstants.Power.USER_FASHIONS, GetUserFashionsPowerAsync(userId) },
            { AppConstants.Power.USER_FOODS, GetUserFoodsPowerAsync(userId) },
            { AppConstants.Power.USER_FORGES, GetUserForgesPowerAsync(userId) },
            { AppConstants.Power.USER_FURNITURES, GetUserFurnituresPowerAsync(userId) },
            { AppConstants.Power.USER_MAGIC_FORMATION_CIRCLES, GetUserMagicFormationCirclesPowerAsync(userId) },
            { AppConstants.Power.USER_MECHA_BEASTS, GetUserMechaBeastsPowerAsync(userId) },
            { AppConstants.Power.USER_MEDALS, GetUserMedalsPowerAsync(userId) },
            { AppConstants.Power.USER_OUTFITS, GetUserOutfitPowerAsync(userId) },
            { AppConstants.Power.USER_PETS, GetUserPetsPowerAsync(userId) },
            { AppConstants.Power.USER_PLANTS, GetUserPlantsPowerAsync(userId) },
            { AppConstants.Power.USER_PUPPETS, GetUserPuppetsPowerAsync(userId) },
            { AppConstants.Power.USER_RELICS, GetUserRelicsPowerAsync(userId) },
            { AppConstants.Power.USER_ROBOTS, GetUserRobotsPowerAsync(userId) },
            { AppConstants.Power.USER_RUNES, GetUserRunesPowerAsync(userId) },
            { AppConstants.Power.USER_SKILLS, GetUserSkillsPowerAsync(userId) },
            { AppConstants.Power.USER_SPIRIT_BEASTS, GetUserSpiritBeastsPowerAsync(userId) },
            { AppConstants.Power.USER_SPIRIT_CARDS, GetUserSpiritCardsPowerAsync(userId) },
            { AppConstants.Power.USER_SYMBOLS, GetUserSymbolsPowerAsync(userId) },
            { AppConstants.Power.USER_TALISMANS, GetUserTalismansPowerAsync(userId) },
            { AppConstants.Power.USER_TECHNOLOGIES, GetUserTechnologiesPowerAsync(userId) },
            { AppConstants.Power.USER_TITLES, GetUserTitlesPowerAsync(userId) },
            { AppConstants.Power.USER_VEHICLES, GetUserVehiclesPowerAsync(userId) },
            { AppConstants.Power.USER_WEAPONS, GetUserWeaponsPowerAsync(userId) }
        };

        foreach (var kvp in userObjectTasks)
        {
            var result = await kvp.Value;
            if (result != null)
            {
                _powerCache[kvp.Key] = result;
            }
        }
    }

    /// <summary>
    /// 4. Nhóm Extra Power: Gọi các task lấy vật phẩm trực tiếp của user, lưu vào Dictionary cache
    /// </summary>
    public static async Task CalculateUserExtraPowerAsync(string userId)
    {
        var userObjectTasks = new Dictionary<string, Task<UserPowers>>
        {
            { AppConstants.Power.ANIME, GetSumUserAnimePowersAsync(userId) },
            { AppConstants.Power.ARCHIVE, GetSumUserArchivePowersAsync(userId) },
            { AppConstants.Power.HICA, GetSumUserHICAPowersAsync(userId) },
            { AppConstants.Power.HICB, GetSumUserHICBPowersAsync(userId) },
            { AppConstants.Power.HIDC, GetSumUserHIDCPowersAsync(userId) },
            { AppConstants.Power.HIEN, GetSumUserHIENPowersAsync(userId) },
            { AppConstants.Power.HIHN, GetSumUserHIHNPowersAsync(userId) },
            { AppConstants.Power.HIIN, GetSumUserHIINPowersAsync(userId) },
            { AppConstants.Power.HIRN, GetSumUserHIRNPowersAsync(userId) },
            { AppConstants.Power.HISN, GetSumUserHISNPowersAsync(userId) },
            { AppConstants.Power.HITN, GetSumUserHITNPowersAsync(userId) },
            { AppConstants.Power.SSWN, GetSumUserSSWNPowersAsync(userId) },
            { AppConstants.Power.RESEARCH, GetSumUserResearchPowersAsync(userId) },
            { AppConstants.Power.SCIENCE_FICTION, GetSumUserScienceFictionPowersAsync(userId) },
            { AppConstants.Power.UNIVERSE, GetSumUserUniversePowersAsync(userId) },
            { AppConstants.Power.COSMIC_IMPRINT, GetSumUserCosmicImprintPowersAsync(userId) },
            { AppConstants.Power.PRIMORDIAL_IMPRINT, GetSumUserPrimordialImprintPowersAsync(userId) },
            { AppConstants.Power.PATH_OF_LORD_IMPRINT, GetSumUserPathOfLordImprintPowersAsync(userId) }
        };

        foreach (var kvp in userObjectTasks)
        {
            var result = await kvp.Value;
            if (result != null)
            {
                _powerCache[kvp.Key] = result;
            }
        }
    }

    /// <summary>
    /// Sum lại tất cả các giá trị từ Dictionary vào biến `userPower` tổng
    /// </summary>
    private static void SumPowersFromCache()
    {
        foreach (var kvp in _powerCache)
        {
            AddPower(kvp.Value);
        }
    }

    /// <summary>
    /// Hàm cộng dồn chỉ số (giữ nguyên logic của bạn)
    /// </summary>
    public static void AddPower(UserPowers source)
    {
        if (source == null) return;

        User.UserPower.Power += source.Power;
        User.UserPower.Health += source.Health;
        User.UserPower.PhysicalAttack += source.PhysicalAttack;
        User.UserPower.PhysicalDefense += source.PhysicalDefense;
        User.UserPower.MagicalAttack += source.MagicalAttack;
        User.UserPower.MagicalDefense += source.MagicalDefense;
        User.UserPower.ChemicalAttack += source.ChemicalAttack;
        User.UserPower.ChemicalDefense += source.ChemicalDefense;
        User.UserPower.AtomicAttack += source.AtomicAttack;
        User.UserPower.AtomicDefense += source.AtomicDefense;
        User.UserPower.MentalAttack += source.MentalAttack;
        User.UserPower.MentalDefense += source.MentalDefense;
        User.UserPower.Speed += source.Speed;
        User.UserPower.CriticalDamageRate += source.CriticalDamageRate;
        User.UserPower.CriticalRate += source.CriticalRate;
        User.UserPower.CriticalResistanceRate += source.CriticalResistanceRate;
        User.UserPower.IgnoreCriticalRate += source.IgnoreCriticalRate;
        User.UserPower.PenetrationRate += source.PenetrationRate;
        User.UserPower.PenetrationResistanceRate += source.PenetrationResistanceRate;
        User.UserPower.EvasionRate += source.EvasionRate;
        User.UserPower.DamageAbsorptionRate += source.DamageAbsorptionRate;
        User.UserPower.IgnoreDamageAbsorptionRate += source.IgnoreDamageAbsorptionRate;
        User.UserPower.AbsorbedDamageRate += source.AbsorbedDamageRate;
        User.UserPower.VitalityRegenerationRate += source.VitalityRegenerationRate;
        User.UserPower.VitalityRegenerationResistanceRate += source.VitalityRegenerationResistanceRate;
        User.UserPower.AccuracyRate += source.AccuracyRate;
        User.UserPower.LifestealRate += source.LifestealRate;
        User.UserPower.ShieldStrength += source.ShieldStrength;
        User.UserPower.Tenacity += source.Tenacity;
        User.UserPower.ResistanceRate += source.ResistanceRate;
        User.UserPower.ComboRate += source.ComboRate;
        User.UserPower.IgnoreComboRate += source.IgnoreComboRate;
        User.UserPower.ComboDamageRate += source.ComboDamageRate;
        User.UserPower.ComboResistanceRate += source.ComboResistanceRate;
        User.UserPower.StunRate += source.StunRate;
        User.UserPower.IgnoreStunRate += source.IgnoreStunRate;
        User.UserPower.ReflectionRate += source.ReflectionRate;
        User.UserPower.IgnoreReflectionRate += source.IgnoreReflectionRate;
        User.UserPower.ReflectionDamageRate += source.ReflectionDamageRate;
        User.UserPower.ReflectionResistanceRate += source.ReflectionResistanceRate;
        User.UserPower.Mana += source.Mana;
        User.UserPower.ManaRegenerationRate += source.ManaRegenerationRate;
        User.UserPower.DamageToDifferentFactionRate += source.DamageToDifferentFactionRate;
        User.UserPower.ResistanceToDifferentFactionRate += source.ResistanceToDifferentFactionRate;
        User.UserPower.DamageToSameFactionRate += source.DamageToSameFactionRate;
        User.UserPower.ResistanceToSameFactionRate += source.ResistanceToSameFactionRate;
        User.UserPower.NormalDamageRate += source.NormalDamageRate;
        User.UserPower.NormalResistanceRate += source.NormalResistanceRate;
        User.UserPower.SkillDamageRate += source.SkillDamageRate;
        User.UserPower.SkillResistanceRate += source.SkillResistanceRate;

        User.UserPower.PercentAllHealth += source.PercentAllHealth;
        User.UserPower.PercentAllPhysicalAttack += source.PercentAllPhysicalAttack;
        User.UserPower.PercentAllPhysicalDefense += source.PercentAllPhysicalDefense;
        User.UserPower.PercentAllMagicalAttack += source.PercentAllMagicalAttack;
        User.UserPower.PercentAllMagicalDefense += source.PercentAllMagicalDefense;
        User.UserPower.PercentAllChemicalAttack += source.PercentAllChemicalAttack;
        User.UserPower.PercentAllChemicalDefense += source.PercentAllChemicalDefense;
        User.UserPower.PercentAllAtomicAttack += source.PercentAllAtomicAttack;
        User.UserPower.PercentAllAtomicDefense += source.PercentAllAtomicDefense;
        User.UserPower.PercentAllMentalAttack += source.PercentAllMentalAttack;
        User.UserPower.PercentAllMentalDefense += source.PercentAllMentalDefense;
    }
    public static async Task<UserPowers> GetSumUserPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserPowers result = await UserPowersService.Create().GetSumUserPowersAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserAnimePowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserAnimes result = await UserAnimesService.Create().GetSumUserAnimesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserArchivePowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserArchives result = await UserArchivesService.Create().GetSumUserArchivesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHICAPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHICAs result = await UserHICAsService.Create().GetSumUserHICAsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHICBPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHICBs result = await UserHICBsService.Create().GetSumUserHICBsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHIDCPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHIDCs result = await UserHIDCsService.Create().GetSumUserHIDCsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHIENPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHIENs result = await UserHIENsService.Create().GetSumUserHIENsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHIHNPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHIHNs result = await UserHIHNsService.Create().GetSumUserHIHNsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHIINPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHIINs result = await UserHIINsService.Create().GetSumUserHIINsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHIRNPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHIRNs result = await UserHIRNsService.Create().GetSumUserHIRNsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHISNPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHISNs result = await UserHISNsService.Create().GetSumUserHISNsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserHITNPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserHITNs result = await UserHITNsService.Create().GetSumUserHITNsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserSSWNPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserSSWNs result = await UserSSWNsService.Create().GetSumUserSSWNsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserResearchPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserResearchs result = await UserResearchsService.Create().GetSumUserResearchsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserScienceFictionPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserScienceFictions result = await UserScienceFictionsService.Create().GetSumUserScienceFictionsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserUniversePowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserUniverses result = await UserUniversesService.Create().GetSumUserUniversesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserCosmicImprintPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserCosmicImprints result = await UserCosmicImprintsService.Create().GetSumUserCosmicImprintsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserPrimordialImprintPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserPrimordialImprints result = await UserPrimordialImprintsService.Create().GetSumUserPrimordialImprintsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSumUserPathOfLordImprintPowersAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        UserPathOfLordImprints result = await UserPathOfLordImprintsService.Create().GetSumUserPathOfLordImprintsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentAchievementsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetAchievementsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Achievements result = await AchievementsGalleryService.Create().SumPowerAchievementsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserAchievementsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentAlchemiesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Alchemies percentAlchemies = await AlchemiesService.Create().SumPowerAlchemiesPercentAsync(userId);

        tempUserPower.PercentAllHealth += percentAlchemies.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += percentAlchemies.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += percentAlchemies.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += percentAlchemies.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += percentAlchemies.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += percentAlchemies.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += percentAlchemies.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += percentAlchemies.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += percentAlchemies.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += percentAlchemies.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += percentAlchemies.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetAlchemiesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Alchemies result = await AlchemiesGalleryService.Create().SumPowerAlchemiesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserAlchemiesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Alchemies result = await UserAlchemiesService.Create().SumPowerUserAlchemiesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentArchitecturesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Architectures result = await ArchitecturesService.Create().SumPowerArchitecturesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetArchitecturesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Architectures result = await ArchitecturesGalleryService.Create().SumPowerArchitecturesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserArchitecturesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Architectures result = await UserArchitecturesService.Create().SumPowerUserArchitecturesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentArtifactsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Artifacts result = await ArtifactsService.Create().SumPowerArtifactsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetArtifactsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Artifacts result = await ArtifactsGalleryService.Create().SumPowerArtifactsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserArtifactsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Artifacts result = await UserArtifactsService.Create().SumPowerUserArtifactsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentArtworksPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Artworks result = await ArtworksService.Create().SumPowerArtworksPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetArtworksGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Artworks result = await ArtworksGalleryService.Create().SumPowerArtworksGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserArtworksPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Artworks result = await UserArtworksService.Create().SumPowerUserArtworksAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentAvatarsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Avatars result = await AvatarsService.Create().SumPowerAvatarsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetAvatarsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Avatars result = await AvatarsGalleryService.Create().SumPowerAvatarsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserAvatarsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Avatars result = await UserAvatarsService.Create().SumPowerUserAvatarsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentBadgesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Badges result = await BadgesService.Create().SumPowerBadgesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetBadgesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Badges result = await BadgesGalleryService.Create().SumPowerBadgesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserBadgesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Badges result = await UserBadgesService.Create().SumPowerUserBadgesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentBeveragesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Beverages result = await BeveragesService.Create().SumPowerBeveragesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetBeveragesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Beverages result = await BeveragesGalleryService.Create().SumPowerBeveragesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserBeveragesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Beverages result = await UserBeveragesService.Create().SumPowerUserBeveragesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentBooksPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Books result = await BooksService.Create().SumPowerBooksPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetBooksGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Books result = await BooksGalleryService.Create().SumPowerBooksGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserBooksPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Books result = await UserBooksService.Create().SumPowerUserBooksAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentBordersPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Borders result = await BordersService.Create().SumPowerBordersPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetBordersGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Borders result = await BordersGalleryService.Create().SumPowerBordersGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserBordersPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Borders result = await UserBordersService.Create().SumPowerUserBordersAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentBuildingsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Buildings result = await BuildingsService.Create().SumPowerBuildingsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetBuildingsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Buildings result = await BuildingsGalleryService.Create().SumPowerBuildingsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserBuildingsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Buildings result = await UserBuildingsService.Create().SumPowerUserBuildingsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetPercentCardAdmiralsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     CardAdmirals result = await CardAdmiralsService.Create().SumPowerCardAdmiralsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardAdmiralsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardAdmirals result = await CardAdmiralsGalleryService.Create().SumPowerCardAdmiralsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardAdmiralsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     CardAdmirals result = await UserCardAdmiralsService.Create().SumPowerUserCardAdmiralsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardCaptainsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     CardCaptains result = await CardCaptainsService.Create().SumPowerCardCaptainsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardCaptainsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardCaptains result = await CardCaptainsGalleryService.Create().SumPowerCardCaptainsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardCaptainsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     CardCaptains result = await UserCardCaptainsService.Create().SumPowerUserCardCaptainsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardColonelsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardColonelsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardColonels result = await CardColonelsGalleryService.Create().SumPowerCardColonelsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardColonelsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardGeneralsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardGeneralsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardGenerals result = await CardGeneralsGalleryService.Create().SumPowerCardGeneralsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardGeneralsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardHeroesPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardHeroesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardHeroes result = await CardHeroesGalleryService.Create().SumPowerCardHeroesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardHeroesPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetPercentCardLivesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardLives result = await CardLivesService.Create().SumPowerCardLivesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetCardLivesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardLives result = await CardLivesGalleryService.Create().SumPowerCardLivesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserCardLivesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardLives result = await UserCardLivesService.Create().SumPowerUserCardLivesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetPercentCardMonstersPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardMonstersGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardMonsters result = await CardMonstersGalleryService.Create().SumPowerCardMonstersGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardMonstersPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardMilitariesPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardMilitariesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardMilitaries result = await CardMilitariesGalleryService.Create().SumPowerCardMilitariesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardMilitariesPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardSoldiersPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardSoldiersGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardSoldiers result = await CardSoldiersGalleryService.Create().SumPowerCardSoldiersGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardSoldiersPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    // public static async Task<UserPowers> GetPercentCardSpellsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await AchievementsService.Create().SumPowerAchievementsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetCardSpellsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CardSpells result = await CardSpellsGalleryService.Create().SumPowerCardSpellsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserCardSpellsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Achievements result = await UserAchievementsService.Create().SumPowerUserAchievementsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetPercentCollaborationEquipmentsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CollaborationEquipments result = await CollaborationEquipmentsService.Create().SumPowerCollaborationEquipmentsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetCollaborationEquipmentsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CollaborationEquipments result = await CollaborationEquipmentsGalleryService.Create().SumPowerCollaborationEquipmentsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserCollaborationEquipmentsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        CollaborationEquipments result = await UserCollaborationEquipmentsService.Create().SumPowerUserCollaborationEquipmentsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentCollaborationsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Collaborations result = await CollaborationsService.Create().SumPowerCollaborationsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetCollaborationsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Collaborations result = await CollaborationsGalleryService.Create().SumPowerCollaborationsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserCollaborationsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Collaborations result = await UserCollaborationsService.Create().SumPowerUserCollaborationsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentCoresPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Cores result = await CoresService.Create().SumPowerCoresPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetCoresGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Cores result = await CoresGalleryService.Create().SumPowerCoresGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserCoresPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Cores result = await UserCoresService.Create().SumPowerUserCoresAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentEmojisPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Emojis result = await EmojisService.Create().SumPowerEmojisPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetEmojisGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Emojis result = await EmojisGalleryService.Create().SumPowerEmojisGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserEmojisPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Emojis result = await UserEmojisService.Create().SumPowerUserEmojisAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetPercentEquipmentsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Equipments result = await EquipmentsService.Create().SumPowerEquipmentsPercentAsync(userId);

    //     tempUserPower.PercentAllHealth += result.PercentAllHealth;
    //     tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
    //     tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
    //     tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
    //     tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
    //     tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
    //     tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
    //     tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
    //     tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
    //     tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
    //     tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetEquipmentsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Equipments result = await EquipmentsGalleryService.Create().SumPowerEquipmentsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    // public static async Task<UserPowers> GetUserEquipmentsPowerAsync(string userId)
    // {
    //     UserPowers tempUserPower = new UserPowers();
    //     Equipments result = await UserEquipmentsService.Create().SumPowerUserEquipmentsAsync(userId);

    //     tempUserPower.Power += result.Power;
    //     tempUserPower.Health += result.Health;
    //     tempUserPower.PhysicalAttack += result.PhysicalAttack;
    //     tempUserPower.PhysicalDefense += result.PhysicalDefense;
    //     tempUserPower.MagicalAttack += result.MagicalAttack;
    //     tempUserPower.MagicalDefense += result.MagicalDefense;
    //     tempUserPower.ChemicalAttack += result.ChemicalAttack;
    //     tempUserPower.ChemicalDefense += result.ChemicalDefense;
    //     tempUserPower.AtomicAttack += result.AtomicAttack;
    //     tempUserPower.AtomicDefense += result.AtomicDefense;
    //     tempUserPower.MentalAttack += result.MentalAttack;
    //     tempUserPower.MentalDefense += result.MentalDefense;
    //     tempUserPower.Speed += result.Speed;
    //     tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
    //     tempUserPower.CriticalRate += result.CriticalRate;
    //     tempUserPower.PenetrationRate += result.PenetrationRate;
    //     tempUserPower.EvasionRate += result.EvasionRate;
    //     tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
    //     tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
    //     tempUserPower.AccuracyRate += result.AccuracyRate;
    //     tempUserPower.LifestealRate += result.LifestealRate;
    //     tempUserPower.ShieldStrength += result.ShieldStrength;
    //     tempUserPower.Tenacity += result.Tenacity;
    //     tempUserPower.ResistanceRate += result.ResistanceRate;
    //     tempUserPower.ComboRate += result.ComboRate;
    //     tempUserPower.ReflectionRate += result.ReflectionRate;
    //     tempUserPower.Mana += result.Mana;
    //     tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
    //     tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
    //     tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
    //     tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
    //     tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

    //     return tempUserPower;
    // }
    public static async Task<UserPowers> GetPercentFashionsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Fashions result = await FashionsService.Create().SumPowerFashionsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetFashionsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Fashions result = await FashionsGalleryService.Create().SumPowerFashionsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserFashionsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Fashions result = await UserFashionsService.Create().SumPowerUserFashionsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentFoodsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Foods result = await FoodsService.Create().SumPowerFoodsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetFoodsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Foods result = await FoodsGalleryService.Create().SumPowerFoodsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserFoodsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Foods result = await UserFoodsService.Create().SumPowerUserFoodsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentForgesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Forges result = await ForgesService.Create().SumPowerForgesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetForgesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Forges result = await ForgesGalleryService.Create().SumPowerForgesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserForgesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Forges result = await UserForgesService.Create().SumPowerUserForgesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentFurnituresPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Furnitures result = await FurnituresService.Create().SumPowerFurnituresPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetFurnituresGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Furnitures result = await FurnituresGalleryService.Create().SumPowerFurnituresGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserFurnituresPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Furnitures result = await UserFurnituresService.Create().SumPowerUserFurnituresAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentMagicFormationCirclesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        MagicFormationCircles result = await MagicFormationCirclesService.Create().SumPowerMagicFormationCirclesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetMagicFormationCirclesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        MagicFormationCircles result = await MagicFormationCirclesGalleryService.Create().SumPowerMagicFormationCirclesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserMagicFormationCirclesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        MagicFormationCircles result = await UserMagicFormationCirclesService.Create().SumPowerUserMagicFormationCirclesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentMechaBeastsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        MechaBeasts result = await MechaBeastsService.Create().SumPowerMechaBeastsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetMechaBeastsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        MechaBeasts result = await MechaBeastsGalleryService.Create().SumPowerMechaBeastsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserMechaBeastsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        MechaBeasts result = await UserMechaBeastsService.Create().SumPowerUserMechaBeastsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentMedalsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Medals result = await MedalsService.Create().SumPowerMedalsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetMedalsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Medals result = await MedalsGalleryService.Create().SumPowerMedalsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserMedalsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Medals result = await UserMedalsService.Create().SumPowerUserMedalsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentOutfitsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Outfits result = await OutfitsService.Create().SumPowerOutfitsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetOutfitsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Outfits result = await OutfitsGalleryService.Create().SumPowerOutfitsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserOutfitPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Outfits result = await UserOutfitsService.Create().SumPowerUserOutfitsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentPetsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Pets result = await PetsService.Create().SumPowerPetsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPetsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Pets result = await PetsGalleryService.Create().SumPowerPetsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserPetsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Pets result = await UserPetsService.Create().SumPowerUserPetsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentPlantsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Plants result = await PlantsService.Create().SumPowerPlantsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPlantsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Plants result = await PlantsGalleryService.Create().SumPowerPlantsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserPlantsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Plants result = await UserPlantsService.Create().SumPowerUserPlantsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentPuppetsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Puppets result = await PuppetsService.Create().SumPowerPuppetsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPuppetsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Puppets result = await PuppetsGalleryService.Create().SumPowerPuppetsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserPuppetsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Puppets result = await UserPuppetsService.Create().SumPowerUserPuppetsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentRelicsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Relics result = await RelicsService.Create().SumPowerRelicsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetRelicsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Relics result = await RelicsGalleryService.Create().SumPowerRelicsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserRelicsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Relics result = await UserRelicsService.Create().SumPowerUserRelicsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentRobotsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Robots result = await RobotsService.Create().SumPowerRobotsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetRobotsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Robots result = await RobotsGalleryService.Create().SumPowerRobotsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserRobotsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Robots result = await UserRobotsService.Create().SumPowerUserRobotsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentRunesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Runes result = await RunesService.Create().SumPowerRunesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetRunesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Runes result = await RunesGalleryService.Create().SumPowerRunesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserRunesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Runes result = await UserRunesService.Create().SumPowerUserRunesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentSkillsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Skills result = await SkillsService.Create().SumPowerSkillsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSkillsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Skills result = await SkillsGalleryService.Create().SumPowerSkillsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserSkillsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Skills result = await UserSkillsService.Create().SumPowerUserSkillsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentSpiritBeastsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        SpiritBeasts result = await SpiritBeastsService.Create().SumPowerSpiritBeastsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSpiritBeastsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        SpiritBeasts result = await SpiritBeastsGalleryService.Create().SumPowerSpiritBeastsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserSpiritBeastsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        SpiritBeasts result = await UserSpiritBeastsService.Create().SumPowerUserSpiritBeastsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentSpiritCardsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        SpiritCards result = await SpiritCardsService.Create().SumPowerSpiritCardsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSpiritCardsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        SpiritCards result = await SpiritCardsGalleryService.Create().SumPowerSpiritCardsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserSpiritCardsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        SpiritCards result = await UserSpiritCardsService.Create().SumPowerUserSpiritCardsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentSymbolsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Symbols result = await SymbolsService.Create().SumPowerSymbolsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetSymbolsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Symbols result = await SymbolsGalleryService.Create().SumPowerSymbolsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserSymbolsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Symbols result = await UserSymbolsService.Create().SumPowerUserSymbolsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentTalismansPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Talismans result = await TalismansService.Create().SumPowerTalismansPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetTalismansGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Talismans result = await TalismansGalleryService.Create().SumPowerTalismansGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserTalismansPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Talismans result = await UserTalismansService.Create().SumPowerUserTalismansAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentTechnologiesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Technologies result = await TechnologiesService.Create().SumPowerTechnologiesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetTechnologiesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Technologies result = await TechnologiesGalleryService.Create().SumPowerTechnologiesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserTechnologiesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Technologies result = await UserTechnologiesService.Create().SumPowerUserTechnologiesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentTitlesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Titles result = await TitlesService.Create().SumPowerTitlesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetTitlesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Titles result = await TitlesGalleryService.Create().SumPowerTitlesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserTitlesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Titles result = await UserTitlesService.Create().SumPowerUserTitlesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentVehiclesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Vehicles result = await VehiclesService.Create().SumPowerVehiclesPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetVehiclesGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Vehicles result = await VehiclesGalleryService.Create().SumPowerVehiclesGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserVehiclesPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Vehicles result = await UserVehiclesService.Create().SumPowerUserVehiclesAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetPercentWeaponsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Weapons result = await WeaponsService.Create().SumPowerWeaponsPercentAsync(userId);

        tempUserPower.PercentAllHealth += result.PercentAllHealth;
        tempUserPower.PercentAllPhysicalAttack += result.PercentAllPhysicalAttack;
        tempUserPower.PercentAllPhysicalDefense += result.PercentAllPhysicalDefense;
        tempUserPower.PercentAllMagicalAttack += result.PercentAllMagicalAttack;
        tempUserPower.PercentAllMagicalDefense += result.PercentAllMagicalDefense;
        tempUserPower.PercentAllChemicalAttack += result.PercentAllChemicalAttack;
        tempUserPower.PercentAllChemicalDefense += result.PercentAllChemicalDefense;
        tempUserPower.PercentAllAtomicAttack += result.PercentAllAtomicAttack;
        tempUserPower.PercentAllAtomicDefense += result.PercentAllAtomicDefense;
        tempUserPower.PercentAllMentalAttack += result.PercentAllMentalAttack;
        tempUserPower.PercentAllMentalDefense += result.PercentAllMentalDefense;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetWeaponsGalleryPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Weapons result = await WeaponsGalleryService.Create().SumPowerWeaponsGalleryAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }
    public static async Task<UserPowers> GetUserWeaponsPowerAsync(string userId)
    {
        UserPowers tempUserPower = new UserPowers();
        Weapons result = await UserWeaponsService.Create().SumPowerUserWeaponsAsync(userId);

        tempUserPower.Power += result.Power;
        tempUserPower.Health += result.Health;
        tempUserPower.PhysicalAttack += result.PhysicalAttack;
        tempUserPower.PhysicalDefense += result.PhysicalDefense;
        tempUserPower.MagicalAttack += result.MagicalAttack;
        tempUserPower.MagicalDefense += result.MagicalDefense;
        tempUserPower.ChemicalAttack += result.ChemicalAttack;
        tempUserPower.ChemicalDefense += result.ChemicalDefense;
        tempUserPower.AtomicAttack += result.AtomicAttack;
        tempUserPower.AtomicDefense += result.AtomicDefense;
        tempUserPower.MentalAttack += result.MentalAttack;
        tempUserPower.MentalDefense += result.MentalDefense;
        tempUserPower.Speed += result.Speed;
        tempUserPower.CriticalDamageRate += result.CriticalDamageRate;
        tempUserPower.CriticalRate += result.CriticalRate;
        tempUserPower.PenetrationRate += result.PenetrationRate;
        tempUserPower.EvasionRate += result.EvasionRate;
        tempUserPower.DamageAbsorptionRate += result.DamageAbsorptionRate;
        tempUserPower.VitalityRegenerationRate += result.VitalityRegenerationRate;
        tempUserPower.AccuracyRate += result.AccuracyRate;
        tempUserPower.LifestealRate += result.LifestealRate;
        tempUserPower.ShieldStrength += result.ShieldStrength;
        tempUserPower.Tenacity += result.Tenacity;
        tempUserPower.ResistanceRate += result.ResistanceRate;
        tempUserPower.ComboRate += result.ComboRate;
        tempUserPower.ReflectionRate += result.ReflectionRate;
        tempUserPower.Mana += result.Mana;
        tempUserPower.ManaRegenerationRate += result.ManaRegenerationRate;
        tempUserPower.DamageToDifferentFactionRate += result.DamageToDifferentFactionRate;
        tempUserPower.ResistanceToDifferentFactionRate += result.ResistanceToDifferentFactionRate;
        tempUserPower.DamageToSameFactionRate += result.DamageToSameFactionRate;
        tempUserPower.ResistanceToSameFactionRate += result.ResistanceToSameFactionRate;

        return tempUserPower;
    }

}