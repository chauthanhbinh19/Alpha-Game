using System.Collections.Generic;

public class PowerManagerService : IPowerManagerService
{
    private readonly IPowerManagerRepository _powerManagerRepository;

    public PowerManagerService(IPowerManagerRepository powerManagerRepository)
    {
        _powerManagerRepository = powerManagerRepository;
    }

    public static PowerManagerService Create()
    {
        return new PowerManagerService(new PowerManagerRepository());
    }

    public void InsertUserStats(string user_id)
    {
        PowerManager powerManager = CalculateTotalPower();
        _powerManagerRepository.InsertUserStats(user_id, powerManager);
    }
    public void UpdateUserStats(string user_id)
    {
        PowerManager powerManager = CalculateTotalPower();
        _powerManagerRepository.UpdateUserStats(user_id, powerManager);
    }
    public PowerManager GetUserStats(string user_id)
    {
        return _powerManagerRepository.GetUserStats(user_id);
    }
    public PowerManager CalculateTotalPower()
    {
        PowerManager totalPower = new PowerManager();

        PowerManager achievementsPower = GetAchievementsPower();
        AddPower(totalPower, achievementsPower);

        // Lấy sức mạnh từ Avatars
        PowerManager avatarsPower = GetAvatarsPower();
        AddPower(totalPower, avatarsPower);

        // Lấy sức mạnh từ Books
        PowerManager booksPower = GetBooksPower();
        AddPower(totalPower, booksPower);

        // Lấy sức mạnh từ Borders
        PowerManager bordersPower = GetBordersPower();
        AddPower(totalPower, bordersPower);

        // Lấy sức mạnh từ Card Heroes
        PowerManager cardHeroesPower = GetCardHeroesPower();
        AddPower(totalPower, cardHeroesPower);

        // Lấy sức mạnh từ Card Captains
        PowerManager cardCaptainsPower = GetCardCaptainsPower();
        AddPower(totalPower, cardCaptainsPower);

        // Lấy sức mạnh từ Card Colonels
        PowerManager cardColonelsPower = GetCardColonelsPower();
        AddPower(totalPower, cardColonelsPower);

        // Lấy sức mạnh từ Card Generals
        PowerManager cardGeneralsPower = GetCardGeneralsPower();
        AddPower(totalPower, cardGeneralsPower);

        // Lấy sức mạnh từ Card Admirals
        PowerManager cardAdmiralsPower = GetCardAdmiralsPower();
        AddPower(totalPower, cardAdmiralsPower);

        // Lấy sức mạnh từ Card Monsters
        PowerManager cardMonstersPower = GetCardMonstersPower();
        AddPower(totalPower, cardMonstersPower);

        // Lấy sức mạnh từ Card Military
        PowerManager cardMilitaryPower = GetCardMilitaryPower();
        AddPower(totalPower, cardMilitaryPower);

        // Lấy sức mạnh từ Card Spell
        PowerManager cardSpellPower = GetCardSpellPower();
        AddPower(totalPower, cardSpellPower);

        // Lấy sức mạnh từ Collaborations
        PowerManager collaborationsPower = GetCollaborationsPower();
        AddPower(totalPower, collaborationsPower);

        // Lấy sức mạnh từ Collaboration Equipments
        PowerManager collaborationEquipmentsPower = GetCollaborationEquipmentsPower();
        AddPower(totalPower, collaborationEquipmentsPower);

        // Lấy sức mạnh từ Equipments
        PowerManager equipmentsPower = GetEquipmentsPower();
        AddPower(totalPower, equipmentsPower);

        // Lấy sức mạnh từ Magic Formation Circle
        PowerManager magicFormationCirclePower = GetMagicFormationCirclePower();
        AddPower(totalPower, magicFormationCirclePower);

        // Lấy sức mạnh từ Relics
        PowerManager relicsPower = GetRelicsPower();
        AddPower(totalPower, relicsPower);

        // Lấy sức mạnh từ Medals
        PowerManager medalsPower = GetMedalsPower();
        AddPower(totalPower, medalsPower);

        // Lấy sức mạnh từ Pets
        PowerManager petsPower = GetPetsPower();
        AddPower(totalPower, petsPower);

        // Lấy sức mạnh từ Symbols
        PowerManager symbolsPower = GetSymbolsPower();
        AddPower(totalPower, symbolsPower);

        // Lấy sức mạnh từ Skills
        PowerManager skillsPower = GetSkillsPower();
        AddPower(totalPower, skillsPower);

        // Lấy sức mạnh từ Titles
        PowerManager titlesPower = GetTitlesPower();
        AddPower(totalPower, titlesPower);

        // Lấy sức mạnh từ Talisman
        PowerManager talismanPower = GetTalismanPower();
        AddPower(totalPower, talismanPower);

        // Lấy sức mạnh từ Puppet
        PowerManager puppetPower = GetPuppetPower();
        AddPower(totalPower, puppetPower);

        // Lấy sức mạnh từ Alchemy
        PowerManager alchemyPower = GetAlchemyPower();
        AddPower(totalPower, alchemyPower);

        // Lấy sức mạnh từ Forge
        PowerManager forgePower = GetForgePower();
        AddPower(totalPower, forgePower);

        // Lấy sức mạnh từ Card Life
        PowerManager cardLifePower = GetCardLifePower();
        AddPower(totalPower, cardLifePower);

        return totalPower;
    }
    private void AddPower(PowerManager target, PowerManager source)
    {
        target.power += source.power;
        target.health += source.health;
        target.physical_attack += source.physical_attack;
        target.physical_defense += source.physical_defense;
        target.magical_attack += source.magical_attack;
        target.magical_defense += source.magical_defense;
        target.chemical_attack += source.chemical_attack;
        target.chemical_defense += source.chemical_defense;
        target.atomic_attack += source.atomic_attack;
        target.atomic_defense += source.atomic_defense;
        target.mental_attack += source.mental_attack;
        target.mental_defense += source.mental_defense;
        target.speed += source.speed;
        target.critical_damage_rate += source.critical_damage_rate;
        target.critical_rate += source.critical_rate;
        target.penetration_rate += source.penetration_rate;
        target.evasion_rate += source.evasion_rate;
        target.damage_absorption_rate += source.damage_absorption_rate;
        target.vitality_regeneration_rate += source.vitality_regeneration_rate;
        target.accuracy_rate += source.accuracy_rate;
        target.lifesteal_rate += source.lifesteal_rate;
        target.shield_strength += source.shield_strength;
        target.tenacity += source.tenacity;
        target.resistance_rate += source.resistance_rate;
        target.combo_rate += source.combo_rate;
        target.reflection_rate += source.reflection_rate;
        target.mana += source.mana;
        target.mana_regeneration_rate += source.mana_regeneration_rate;
        target.damage_to_different_faction_rate += source.damage_to_different_faction_rate;
        target.resistance_to_different_faction_rate += source.resistance_to_different_faction_rate;
        target.damage_to_same_faction_rate += source.damage_to_same_faction_rate;
        target.resistance_to_same_faction_rate += source.resistance_to_same_faction_rate;

        target.percent_all_health += source.percent_all_health;
        target.percent_all_physical_attack += source.percent_all_physical_attack;
        target.percent_all_physical_defense += source.percent_all_physical_defense;
        target.percent_all_magical_attack += source.percent_all_magical_attack;
        target.percent_all_magical_defense += source.percent_all_magical_defense;
        target.percent_all_chemical_attack += source.percent_all_chemical_attack;
        target.percent_all_chemical_defense += source.percent_all_chemical_defense;
        target.percent_all_atomic_attack += source.percent_all_atomic_attack;
        target.percent_all_atomic_defense += source.percent_all_atomic_defense;
        target.percent_all_mental_attack += source.percent_all_mental_attack;
        target.percent_all_mental_defense += source.percent_all_mental_defense;
    }
    public PowerManager GetAchievementsPower()
    {
        PowerManager powerManager = new PowerManager();

        // User Achievements
        IUserAchievementsRepository userAchievementsRepository = new UserAchievementsRepository();
        UserAchievementsService userAchievementsService = new UserAchievementsService(userAchievementsRepository);
        Achievements userAchievements = userAchievementsService.SumPowerUserAchievements();

        powerManager.power += userAchievements.power;
        powerManager.health += userAchievements.health;
        powerManager.physical_attack += userAchievements.physical_attack;
        powerManager.physical_defense += userAchievements.physical_defense;
        powerManager.magical_attack += userAchievements.magical_attack;
        powerManager.magical_defense += userAchievements.magical_defense;
        powerManager.chemical_attack += userAchievements.chemical_attack;
        powerManager.chemical_defense += userAchievements.chemical_defense;
        powerManager.atomic_attack += userAchievements.atomic_attack;
        powerManager.atomic_defense += userAchievements.atomic_defense;
        powerManager.mental_attack += userAchievements.mental_attack;
        powerManager.mental_defense += userAchievements.mental_defense;
        powerManager.speed += userAchievements.speed;
        powerManager.critical_damage_rate += userAchievements.critical_damage_rate;
        powerManager.critical_rate += userAchievements.critical_rate;
        powerManager.penetration_rate += userAchievements.penetration_rate;
        powerManager.evasion_rate += userAchievements.evasion_rate;
        powerManager.damage_absorption_rate += userAchievements.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += userAchievements.vitality_regeneration_rate;
        powerManager.accuracy_rate += userAchievements.accuracy_rate;
        powerManager.lifesteal_rate += userAchievements.lifesteal_rate;
        powerManager.shield_strength += userAchievements.shield_strength;
        powerManager.tenacity += userAchievements.tenacity;
        powerManager.resistance_rate += userAchievements.resistance_rate;
        powerManager.combo_rate += userAchievements.combo_rate;
        powerManager.reflection_rate += userAchievements.reflection_rate;
        powerManager.mana += userAchievements.mana;
        powerManager.mana_regeneration_rate += userAchievements.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += userAchievements.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += userAchievements.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += userAchievements.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += userAchievements.resistance_to_same_faction_rate;

        // Percent Achievements
        IAchievementsRepository achievementsRepository = new AchievementsRepository();
        AchievementsService achievementsService = new AchievementsService(achievementsRepository);
        Achievements percentAchievements = achievementsService.SumPowerAchievementsPercent();

        powerManager.percent_all_health += percentAchievements.percent_all_health;
        powerManager.percent_all_physical_attack += percentAchievements.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += percentAchievements.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += percentAchievements.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += percentAchievements.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += percentAchievements.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += percentAchievements.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += percentAchievements.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += percentAchievements.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += percentAchievements.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += percentAchievements.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetAvatarsPower()
    {
        PowerManager powerManager = new PowerManager();

        // Gallery Avatars
        IAvatarsGalleryRepository avatarsGalleryRepository = new AvatarsGalleryRepository();
        AvatarsGalleryService avatarsGalleryService = new AvatarsGalleryService(avatarsGalleryRepository);
        Avatars galleryAvatars = avatarsGalleryService.SumPowerAvatarsGallery();

        powerManager.power += galleryAvatars.power;
        powerManager.health += galleryAvatars.health;
        powerManager.physical_attack += galleryAvatars.physical_attack;
        powerManager.physical_defense += galleryAvatars.physical_defense;
        powerManager.magical_attack += galleryAvatars.magical_attack;
        powerManager.magical_defense += galleryAvatars.magical_defense;
        powerManager.chemical_attack += galleryAvatars.chemical_attack;
        powerManager.chemical_defense += galleryAvatars.chemical_defense;
        powerManager.atomic_attack += galleryAvatars.atomic_attack;
        powerManager.atomic_defense += galleryAvatars.atomic_defense;
        powerManager.mental_attack += galleryAvatars.mental_attack;
        powerManager.mental_defense += galleryAvatars.mental_defense;
        powerManager.speed += galleryAvatars.speed;
        powerManager.critical_damage_rate += galleryAvatars.critical_damage_rate;
        powerManager.critical_rate += galleryAvatars.critical_rate;
        powerManager.penetration_rate += galleryAvatars.penetration_rate;
        powerManager.evasion_rate += galleryAvatars.evasion_rate;
        powerManager.damage_absorption_rate += galleryAvatars.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += galleryAvatars.vitality_regeneration_rate;
        powerManager.accuracy_rate += galleryAvatars.accuracy_rate;
        powerManager.lifesteal_rate += galleryAvatars.lifesteal_rate;
        powerManager.shield_strength += galleryAvatars.shield_strength;
        powerManager.tenacity += galleryAvatars.tenacity;
        powerManager.resistance_rate += galleryAvatars.resistance_rate;
        powerManager.combo_rate += galleryAvatars.combo_rate;
        powerManager.reflection_rate += galleryAvatars.reflection_rate;
        powerManager.mana += galleryAvatars.mana;
        powerManager.mana_regeneration_rate += galleryAvatars.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += galleryAvatars.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += galleryAvatars.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += galleryAvatars.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += galleryAvatars.resistance_to_same_faction_rate;

        powerManager.percent_all_health += galleryAvatars.percent_all_health;
        powerManager.percent_all_physical_attack += galleryAvatars.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += galleryAvatars.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += galleryAvatars.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += galleryAvatars.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += galleryAvatars.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += galleryAvatars.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += galleryAvatars.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += galleryAvatars.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += galleryAvatars.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += galleryAvatars.percent_all_mental_defense;

        // User Avatars
        IUserAvatarsRepository userAvatarsRepository = new UserAvatarsRepository();
        UserAvatarsService userAvatarsService = new UserAvatarsService(userAvatarsRepository);
        Avatars userAvatars = userAvatarsService.SumPowerUserAvatars();

        powerManager.power += userAvatars.power;
        powerManager.health += userAvatars.health;
        powerManager.physical_attack += userAvatars.physical_attack;
        powerManager.physical_defense += userAvatars.physical_defense;
        powerManager.magical_attack += userAvatars.magical_attack;
        powerManager.magical_defense += userAvatars.magical_defense;
        powerManager.chemical_attack += userAvatars.chemical_attack;
        powerManager.chemical_defense += userAvatars.chemical_defense;
        powerManager.atomic_attack += userAvatars.atomic_attack;
        powerManager.atomic_defense += userAvatars.atomic_defense;
        powerManager.mental_attack += userAvatars.mental_attack;
        powerManager.mental_defense += userAvatars.mental_defense;
        powerManager.speed += userAvatars.speed;
        powerManager.critical_damage_rate += userAvatars.critical_damage_rate;
        powerManager.critical_rate += userAvatars.critical_rate;
        powerManager.penetration_rate += userAvatars.penetration_rate;
        powerManager.evasion_rate += userAvatars.evasion_rate;
        powerManager.damage_absorption_rate += userAvatars.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += userAvatars.vitality_regeneration_rate;
        powerManager.accuracy_rate += userAvatars.accuracy_rate;
        powerManager.lifesteal_rate += userAvatars.lifesteal_rate;
        powerManager.shield_strength += userAvatars.shield_strength;
        powerManager.tenacity += userAvatars.tenacity;
        powerManager.resistance_rate += userAvatars.resistance_rate;
        powerManager.combo_rate += userAvatars.combo_rate;
        powerManager.reflection_rate += userAvatars.reflection_rate;
        powerManager.mana += userAvatars.mana;
        powerManager.mana_regeneration_rate += userAvatars.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += userAvatars.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += userAvatars.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += userAvatars.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += userAvatars.resistance_to_same_faction_rate;

        // Percent Avatars
        IAvatarsRepository avatarsRepository = new AvatarsRepository();
        AvatarsService avatarsService = new AvatarsService(avatarsRepository);
        Avatars percentAvatars = avatarsService.SumPowerAvatarsPercent();

        powerManager.percent_all_health += percentAvatars.percent_all_health;
        powerManager.percent_all_physical_attack += percentAvatars.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += percentAvatars.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += percentAvatars.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += percentAvatars.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += percentAvatars.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += percentAvatars.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += percentAvatars.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += percentAvatars.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += percentAvatars.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += percentAvatars.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetBordersPower()
    {
        PowerManager powerManager = new PowerManager();

        // Gallery Borders
        IBordersGalleryRepository bordersGalleryRepository = new BordersGalleryRepository(); // Note: This was named booksGalleryRepository in the original. Corrected for clarity.
        BordersGalleryService bordersGalleryService = new BordersGalleryService(bordersGalleryRepository); // Note: This was named booksGalleryService in the original. Corrected for clarity.
        Borders galleryBorders = bordersGalleryService.SumPowerBordersGallery();

        powerManager.power += galleryBorders.power;
        powerManager.health += galleryBorders.health;
        powerManager.physical_attack += galleryBorders.physical_attack;
        powerManager.physical_defense += galleryBorders.physical_defense;
        powerManager.magical_attack += galleryBorders.magical_attack;
        powerManager.magical_defense += galleryBorders.magical_defense;
        powerManager.chemical_attack += galleryBorders.chemical_attack;
        powerManager.chemical_defense += galleryBorders.chemical_defense;
        powerManager.atomic_attack += galleryBorders.atomic_attack;
        powerManager.atomic_defense += galleryBorders.atomic_defense;
        powerManager.mental_attack += galleryBorders.mental_attack;
        powerManager.mental_defense += galleryBorders.mental_defense;
        powerManager.speed += galleryBorders.speed;
        powerManager.critical_damage_rate += galleryBorders.critical_damage_rate;
        powerManager.critical_rate += galleryBorders.critical_rate;
        powerManager.penetration_rate += galleryBorders.penetration_rate;
        powerManager.evasion_rate += galleryBorders.evasion_rate;
        powerManager.damage_absorption_rate += galleryBorders.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += galleryBorders.vitality_regeneration_rate;
        powerManager.accuracy_rate += galleryBorders.accuracy_rate;
        powerManager.lifesteal_rate += galleryBorders.lifesteal_rate;
        powerManager.shield_strength += galleryBorders.shield_strength;
        powerManager.tenacity += galleryBorders.tenacity;
        powerManager.resistance_rate += galleryBorders.resistance_rate;
        powerManager.combo_rate += galleryBorders.combo_rate;
        powerManager.reflection_rate += galleryBorders.reflection_rate;
        powerManager.mana += galleryBorders.mana;
        powerManager.mana_regeneration_rate += galleryBorders.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += galleryBorders.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += galleryBorders.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += galleryBorders.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += galleryBorders.resistance_to_same_faction_rate;

        powerManager.percent_all_health += galleryBorders.percent_all_health;
        powerManager.percent_all_physical_attack += galleryBorders.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += galleryBorders.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += galleryBorders.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += galleryBorders.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += galleryBorders.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += galleryBorders.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += galleryBorders.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += galleryBorders.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += galleryBorders.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += galleryBorders.percent_all_mental_defense;

        // User Borders
        IUserBordersRepository userBordersRepository = new UserBordersRepository();
        UserBordersService userBordersService = new UserBordersService(userBordersRepository);
        Borders userBorders = userBordersService.SumPowerUserBorders();

        powerManager.power += userBorders.power;
        powerManager.health += userBorders.health;
        powerManager.physical_attack += userBorders.physical_attack;
        powerManager.physical_defense += userBorders.physical_defense;
        powerManager.magical_attack += userBorders.magical_attack;
        powerManager.magical_defense += userBorders.magical_defense;
        powerManager.chemical_attack += userBorders.chemical_attack;
        powerManager.chemical_defense += userBorders.chemical_defense;
        powerManager.atomic_attack += userBorders.atomic_attack;
        powerManager.atomic_defense += userBorders.atomic_defense;
        powerManager.mental_attack += userBorders.mental_attack;
        powerManager.mental_defense += userBorders.mental_defense;
        powerManager.speed += userBorders.speed;
        powerManager.critical_damage_rate += userBorders.critical_damage_rate;
        powerManager.critical_rate += userBorders.critical_rate;
        powerManager.penetration_rate += userBorders.penetration_rate;
        powerManager.evasion_rate += userBorders.evasion_rate;
        powerManager.damage_absorption_rate += userBorders.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += userBorders.vitality_regeneration_rate;
        powerManager.accuracy_rate += userBorders.accuracy_rate;
        powerManager.lifesteal_rate += userBorders.lifesteal_rate;
        powerManager.shield_strength += userBorders.shield_strength;
        powerManager.tenacity += userBorders.tenacity;
        powerManager.resistance_rate += userBorders.resistance_rate;
        powerManager.combo_rate += userBorders.combo_rate;
        powerManager.reflection_rate += userBorders.reflection_rate;
        powerManager.mana += userBorders.mana;
        powerManager.mana_regeneration_rate += userBorders.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += userBorders.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += userBorders.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += userBorders.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += userBorders.resistance_to_same_faction_rate;

        // Percent Borders
        IBordersRepository bordersRepository = new BordersRepository();
        BordersService bordersService = new BordersService(bordersRepository);
        Borders percentBorders = bordersService.SumPowerBordersPercent();

        powerManager.percent_all_health += percentBorders.percent_all_health;
        powerManager.percent_all_physical_attack += percentBorders.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += percentBorders.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += percentBorders.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += percentBorders.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += percentBorders.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += percentBorders.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += percentBorders.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += percentBorders.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += percentBorders.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += percentBorders.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetBooksPower()
    {
        PowerManager powerManager = new PowerManager();

        IBooksGalleryRepository booksGalleryRepository = new BooksGalleryRepository();
        BooksGalleryService booksGalleryService = new BooksGalleryService(booksGalleryRepository);

        //Gallery
        Books books = booksGalleryService.SumPowerBooksGallery();

        powerManager.power += books.power;
        powerManager.health += books.health;
        powerManager.physical_attack += books.physical_attack;
        powerManager.physical_defense += books.physical_defense;
        powerManager.magical_attack += books.magical_attack;
        powerManager.magical_defense += books.magical_defense;
        powerManager.chemical_attack += books.chemical_attack;
        powerManager.chemical_defense += books.chemical_defense;
        powerManager.atomic_attack += books.atomic_attack;
        powerManager.atomic_defense += books.atomic_defense;
        powerManager.mental_attack += books.mental_attack;
        powerManager.mental_defense += books.mental_defense;
        powerManager.speed += books.speed;
        powerManager.critical_damage_rate += books.critical_damage_rate;
        powerManager.critical_rate += books.critical_rate;
        powerManager.penetration_rate += books.penetration_rate;
        powerManager.evasion_rate += books.evasion_rate;
        powerManager.damage_absorption_rate += books.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += books.vitality_regeneration_rate;
        powerManager.accuracy_rate += books.accuracy_rate;
        powerManager.lifesteal_rate += books.lifesteal_rate;
        powerManager.shield_strength += books.shield_strength;
        powerManager.tenacity += books.tenacity;
        powerManager.resistance_rate += books.resistance_rate;
        powerManager.combo_rate += books.combo_rate;
        powerManager.reflection_rate += books.reflection_rate;
        powerManager.mana += books.mana;
        powerManager.mana_regeneration_rate += books.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += books.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += books.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += books.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += books.resistance_to_same_faction_rate;

        powerManager.percent_all_health += books.percent_all_health;
        powerManager.percent_all_physical_attack += books.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += books.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += books.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += books.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += books.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += books.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += books.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += books.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += books.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += books.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardHeroesPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardHeroesGalleryRepository cardHeroesGalleryRepository = new CardHeroesGalleryRepository();
        CardHeroesGalleryService cardHeroesGalleryService = new CardHeroesGalleryService(cardHeroesGalleryRepository);

        // Gallery
        CardHeroes cardHeroes = cardHeroesGalleryService.SumPowerCardHeroesGallery();

        powerManager.power += cardHeroes.power;
        powerManager.health += cardHeroes.health;
        powerManager.physical_attack += cardHeroes.physical_attack;
        powerManager.physical_defense += cardHeroes.physical_defense;
        powerManager.magical_attack += cardHeroes.magical_attack;
        powerManager.magical_defense += cardHeroes.magical_defense;
        powerManager.chemical_attack += cardHeroes.chemical_attack;
        powerManager.chemical_defense += cardHeroes.chemical_defense;
        powerManager.atomic_attack += cardHeroes.atomic_attack;
        powerManager.atomic_defense += cardHeroes.atomic_defense;
        powerManager.mental_attack += cardHeroes.mental_attack;
        powerManager.mental_defense += cardHeroes.mental_defense;
        powerManager.speed += cardHeroes.speed;
        powerManager.critical_damage_rate += cardHeroes.critical_damage_rate;
        powerManager.critical_rate += cardHeroes.critical_rate;
        powerManager.penetration_rate += cardHeroes.penetration_rate;
        powerManager.evasion_rate += cardHeroes.evasion_rate;
        powerManager.damage_absorption_rate += cardHeroes.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardHeroes.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardHeroes.accuracy_rate;
        powerManager.lifesteal_rate += cardHeroes.lifesteal_rate;
        powerManager.shield_strength += cardHeroes.shield_strength;
        powerManager.tenacity += cardHeroes.tenacity;
        powerManager.resistance_rate += cardHeroes.resistance_rate;
        powerManager.combo_rate += cardHeroes.combo_rate;
        powerManager.reflection_rate += cardHeroes.reflection_rate;
        powerManager.mana += cardHeroes.mana;
        powerManager.mana_regeneration_rate += cardHeroes.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardHeroes.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardHeroes.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardHeroes.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardHeroes.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardHeroes.percent_all_health;
        powerManager.percent_all_physical_attack += cardHeroes.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardHeroes.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardHeroes.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardHeroes.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardHeroes.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardHeroes.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardHeroes.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardHeroes.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardHeroes.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardHeroes.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardCaptainsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardCaptainsGalleryRepository cardCaptainsGalleryRepository = new CardCaptainsGalleryRepository();
        CardCaptainsGalleryService cardCaptainsGalleryService = new CardCaptainsGalleryService(cardCaptainsGalleryRepository);

        // Gallery
        CardCaptains cardCaptains = cardCaptainsGalleryService.SumPowerCardCaptainsGallery();

        powerManager.power += cardCaptains.power;
        powerManager.health += cardCaptains.health;
        powerManager.physical_attack += cardCaptains.physical_attack;
        powerManager.physical_defense += cardCaptains.physical_defense;
        powerManager.magical_attack += cardCaptains.magical_attack;
        powerManager.magical_defense += cardCaptains.magical_defense;
        powerManager.chemical_attack += cardCaptains.chemical_attack;
        powerManager.chemical_defense += cardCaptains.chemical_defense;
        powerManager.atomic_attack += cardCaptains.atomic_attack;
        powerManager.atomic_defense += cardCaptains.atomic_defense;
        powerManager.mental_attack += cardCaptains.mental_attack;
        powerManager.mental_defense += cardCaptains.mental_defense;
        powerManager.speed += cardCaptains.speed;
        powerManager.critical_damage_rate += cardCaptains.critical_damage_rate;
        powerManager.critical_rate += cardCaptains.critical_rate;
        powerManager.penetration_rate += cardCaptains.penetration_rate;
        powerManager.evasion_rate += cardCaptains.evasion_rate;
        powerManager.damage_absorption_rate += cardCaptains.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardCaptains.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardCaptains.accuracy_rate;
        powerManager.lifesteal_rate += cardCaptains.lifesteal_rate;
        powerManager.shield_strength += cardCaptains.shield_strength;
        powerManager.tenacity += cardCaptains.tenacity;
        powerManager.resistance_rate += cardCaptains.resistance_rate;
        powerManager.combo_rate += cardCaptains.combo_rate;
        powerManager.reflection_rate += cardCaptains.reflection_rate;
        powerManager.mana += cardCaptains.mana;
        powerManager.mana_regeneration_rate += cardCaptains.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardCaptains.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardCaptains.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardCaptains.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardCaptains.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardCaptains.percent_all_health;
        powerManager.percent_all_physical_attack += cardCaptains.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardCaptains.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardCaptains.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardCaptains.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardCaptains.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardCaptains.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardCaptains.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardCaptains.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardCaptains.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardCaptains.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardColonelsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardColonelsGalleryRepository cardColonelsGalleryRepository = new CardColonelsGalleryRepository();
        CardColonelsGalleryService cardColonelsGalleryService = new CardColonelsGalleryService(cardColonelsGalleryRepository);

        //Gallery
        CardColonels cardColonels = cardColonelsGalleryService.SumPowerCardColonelsGallery();

        powerManager.power += cardColonels.power;
        powerManager.health += cardColonels.health;
        powerManager.physical_attack += cardColonels.physical_attack;
        powerManager.physical_defense += cardColonels.physical_defense;
        powerManager.magical_attack += cardColonels.magical_attack;
        powerManager.magical_defense += cardColonels.magical_defense;
        powerManager.chemical_attack += cardColonels.chemical_attack;
        powerManager.chemical_defense += cardColonels.chemical_defense;
        powerManager.atomic_attack += cardColonels.atomic_attack;
        powerManager.atomic_defense += cardColonels.atomic_defense;
        powerManager.mental_attack += cardColonels.mental_attack;
        powerManager.mental_defense += cardColonels.mental_defense;
        powerManager.speed += cardColonels.speed;
        powerManager.critical_damage_rate += cardColonels.critical_damage_rate;
        powerManager.critical_rate += cardColonels.critical_rate;
        powerManager.penetration_rate += cardColonels.penetration_rate;
        powerManager.evasion_rate += cardColonels.evasion_rate;
        powerManager.damage_absorption_rate += cardColonels.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardColonels.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardColonels.accuracy_rate;
        powerManager.lifesteal_rate += cardColonels.lifesteal_rate;
        powerManager.shield_strength += cardColonels.shield_strength;
        powerManager.tenacity += cardColonels.tenacity;
        powerManager.resistance_rate += cardColonels.resistance_rate;
        powerManager.combo_rate += cardColonels.combo_rate;
        powerManager.reflection_rate += cardColonels.reflection_rate;
        powerManager.mana += cardColonels.mana;
        powerManager.mana_regeneration_rate += cardColonels.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardColonels.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardColonels.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardColonels.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardColonels.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardColonels.percent_all_health;
        powerManager.percent_all_physical_attack += cardColonels.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardColonels.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardColonels.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardColonels.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardColonels.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardColonels.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardColonels.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardColonels.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardColonels.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardColonels.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardGeneralsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardGeneralsGalleryRepository cardGeneralsGalleryRepository = new CardGeneralsGalleryRepository();
        CardGeneralsGalleryService cardGeneralsGalleryService = new CardGeneralsGalleryService(cardGeneralsGalleryRepository);

        //Gallery
        CardGenerals cardGenerals = cardGeneralsGalleryService.SumPowerCardGeneralsGallery();

        powerManager.power += cardGenerals.power;
        powerManager.health += cardGenerals.health;
        powerManager.physical_attack += cardGenerals.physical_attack;
        powerManager.physical_defense += cardGenerals.physical_defense;
        powerManager.magical_attack += cardGenerals.magical_attack;
        powerManager.magical_defense += cardGenerals.magical_defense;
        powerManager.chemical_attack += cardGenerals.chemical_attack;
        powerManager.chemical_defense += cardGenerals.chemical_defense;
        powerManager.atomic_attack += cardGenerals.atomic_attack;
        powerManager.atomic_defense += cardGenerals.atomic_defense;
        powerManager.mental_attack += cardGenerals.mental_attack;
        powerManager.mental_defense += cardGenerals.mental_defense;
        powerManager.speed += cardGenerals.speed;
        powerManager.critical_damage_rate += cardGenerals.critical_damage_rate;
        powerManager.critical_rate += cardGenerals.critical_rate;
        powerManager.penetration_rate += cardGenerals.penetration_rate;
        powerManager.evasion_rate += cardGenerals.evasion_rate;
        powerManager.damage_absorption_rate += cardGenerals.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardGenerals.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardGenerals.accuracy_rate;
        powerManager.lifesteal_rate += cardGenerals.lifesteal_rate;
        powerManager.shield_strength += cardGenerals.shield_strength;
        powerManager.tenacity += cardGenerals.tenacity;
        powerManager.resistance_rate += cardGenerals.resistance_rate;
        powerManager.combo_rate += cardGenerals.combo_rate;
        powerManager.reflection_rate += cardGenerals.reflection_rate;
        powerManager.mana += cardGenerals.mana;
        powerManager.mana_regeneration_rate += cardGenerals.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardGenerals.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardGenerals.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardGenerals.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardGenerals.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardGenerals.percent_all_health;
        powerManager.percent_all_physical_attack += cardGenerals.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardGenerals.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardGenerals.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardGenerals.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardGenerals.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardGenerals.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardGenerals.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardGenerals.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardGenerals.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardGenerals.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardAdmiralsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardAdmiralsGalleryRepository cardAdmiralsGalleryRepository = new CardAdmiralsGalleryRepository();
        CardAdmiralsGalleryService cardAdmiralsGalleryService = new CardAdmiralsGalleryService(cardAdmiralsGalleryRepository);

        //Gallery
        CardAdmirals cardAdmirals = cardAdmiralsGalleryService.SumPowerCardCaptainsGallery();

        powerManager.power += cardAdmirals.power;
        powerManager.health += cardAdmirals.health;
        powerManager.physical_attack += cardAdmirals.physical_attack;
        powerManager.physical_defense += cardAdmirals.physical_defense;
        powerManager.magical_attack += cardAdmirals.magical_attack;
        powerManager.magical_defense += cardAdmirals.magical_defense;
        powerManager.chemical_attack += cardAdmirals.chemical_attack;
        powerManager.chemical_defense += cardAdmirals.chemical_defense;
        powerManager.atomic_attack += cardAdmirals.atomic_attack;
        powerManager.atomic_defense += cardAdmirals.atomic_defense;
        powerManager.mental_attack += cardAdmirals.mental_attack;
        powerManager.mental_defense += cardAdmirals.mental_defense;
        powerManager.speed += cardAdmirals.speed;
        powerManager.critical_damage_rate += cardAdmirals.critical_damage_rate;
        powerManager.critical_rate += cardAdmirals.critical_rate;
        powerManager.penetration_rate += cardAdmirals.penetration_rate;
        powerManager.evasion_rate += cardAdmirals.evasion_rate;
        powerManager.damage_absorption_rate += cardAdmirals.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardAdmirals.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardAdmirals.accuracy_rate;
        powerManager.lifesteal_rate += cardAdmirals.lifesteal_rate;
        powerManager.shield_strength += cardAdmirals.shield_strength;
        powerManager.tenacity += cardAdmirals.tenacity;
        powerManager.resistance_rate += cardAdmirals.resistance_rate;
        powerManager.combo_rate += cardAdmirals.combo_rate;
        powerManager.reflection_rate += cardAdmirals.reflection_rate;
        powerManager.mana += cardAdmirals.mana;
        powerManager.mana_regeneration_rate += cardAdmirals.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardAdmirals.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardAdmirals.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardAdmirals.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardAdmirals.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardAdmirals.percent_all_health;
        powerManager.percent_all_physical_attack += cardAdmirals.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardAdmirals.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardAdmirals.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardAdmirals.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardAdmirals.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardAdmirals.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardAdmirals.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardAdmirals.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardAdmirals.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardAdmirals.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardMonstersPower()
    {
        PowerManager powerManager = new PowerManager();
        // CardMonsters cardMonsters = new CardMonsters();
        ICardMonstersGalleryRepository cardMonstersGalleryRepository = new CardMonstersGalleryRepository();
        CardMonstersGalleryService cardMonstersGalleryService = new CardMonstersGalleryService(cardMonstersGalleryRepository);
        //Gallery
        CardMonsters cardMonsters = cardMonstersGalleryService.SumPowerCardMonstersGallery();
        powerManager.power += cardMonsters.power;
        powerManager.health += cardMonsters.health;
        powerManager.physical_attack += cardMonsters.physical_attack;
        powerManager.physical_defense += cardMonsters.physical_defense;
        powerManager.magical_attack += cardMonsters.magical_attack;
        powerManager.magical_defense += cardMonsters.magical_defense;
        powerManager.chemical_attack += cardMonsters.chemical_attack;
        powerManager.chemical_defense += cardMonsters.chemical_defense;
        powerManager.atomic_attack += cardMonsters.atomic_attack;
        powerManager.atomic_defense += cardMonsters.atomic_defense;
        powerManager.mental_attack += cardMonsters.mental_attack;
        powerManager.mental_defense += cardMonsters.mental_defense;
        powerManager.speed += cardMonsters.speed;
        powerManager.critical_damage_rate += cardMonsters.critical_damage_rate;
        powerManager.critical_rate += cardMonsters.critical_rate;
        powerManager.penetration_rate += cardMonsters.penetration_rate;
        powerManager.evasion_rate += cardMonsters.evasion_rate;
        powerManager.damage_absorption_rate += cardMonsters.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardMonsters.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardMonsters.accuracy_rate;
        powerManager.lifesteal_rate += cardMonsters.lifesteal_rate;
        powerManager.shield_strength += cardMonsters.shield_strength;
        powerManager.tenacity += cardMonsters.tenacity;
        powerManager.resistance_rate += cardMonsters.resistance_rate;
        powerManager.combo_rate += cardMonsters.combo_rate;
        powerManager.reflection_rate += cardMonsters.reflection_rate;
        powerManager.mana += cardMonsters.mana;
        powerManager.mana_regeneration_rate += cardMonsters.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardMonsters.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardMonsters.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardMonsters.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardMonsters.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardMonsters.percent_all_health;
        powerManager.percent_all_physical_attack += cardMonsters.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardMonsters.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardMonsters.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardMonsters.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardMonsters.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardMonsters.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardMonsters.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardMonsters.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardMonsters.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardMonsters.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardMilitaryPower()
    {
        PowerManager powerManager = new PowerManager();

        // CardMilitary cardMilitary = new CardMilitary();
        ICardMilitaryGalleryRepository cardMilitaryGalleryRepository = new CardMilitaryGalleryRepository();
        CardMilitaryGalleryService cardMilitaryGalleryService = new CardMilitaryGalleryService(cardMilitaryGalleryRepository);
        //Gallery
        CardMilitary cardMilitary = cardMilitaryGalleryService.SumPowerCardMilitaryGallery();

        powerManager.power += cardMilitary.power;
        powerManager.health += cardMilitary.health;
        powerManager.physical_attack += cardMilitary.physical_attack;
        powerManager.physical_defense += cardMilitary.physical_defense;
        powerManager.magical_attack += cardMilitary.magical_attack;
        powerManager.magical_defense += cardMilitary.magical_defense;
        powerManager.chemical_attack += cardMilitary.chemical_attack;
        powerManager.chemical_defense += cardMilitary.chemical_defense;
        powerManager.atomic_attack += cardMilitary.atomic_attack;
        powerManager.atomic_defense += cardMilitary.atomic_defense;
        powerManager.mental_attack += cardMilitary.mental_attack;
        powerManager.mental_defense += cardMilitary.mental_defense;
        powerManager.speed += cardMilitary.speed;
        powerManager.critical_damage_rate += cardMilitary.critical_damage_rate;
        powerManager.critical_rate += cardMilitary.critical_rate;
        powerManager.penetration_rate += cardMilitary.penetration_rate;
        powerManager.evasion_rate += cardMilitary.evasion_rate;
        powerManager.damage_absorption_rate += cardMilitary.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardMilitary.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardMilitary.accuracy_rate;
        powerManager.lifesteal_rate += cardMilitary.lifesteal_rate;
        powerManager.shield_strength += cardMilitary.shield_strength;
        powerManager.tenacity += cardMilitary.tenacity;
        powerManager.resistance_rate += cardMilitary.resistance_rate;
        powerManager.combo_rate += cardMilitary.combo_rate;
        powerManager.reflection_rate += cardMilitary.reflection_rate;
        powerManager.mana += cardMilitary.mana;
        powerManager.mana_regeneration_rate += cardMilitary.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardMilitary.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardMilitary.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardMilitary.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardMilitary.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardMilitary.percent_all_health;
        powerManager.percent_all_physical_attack += cardMilitary.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardMilitary.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardMilitary.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardMilitary.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardMilitary.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardMilitary.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardMilitary.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardMilitary.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardMilitary.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardMilitary.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCardSpellPower()
    {
        PowerManager powerManager = new PowerManager();

        // CardSpell cardSpell = new CardSpell();
        ICardSpellGalleryRepository cardSpellGalleryRepository = new CardSpellGalleryRepository();
        CardSpellGalleryService cardSpellGalleryService = new CardSpellGalleryService(cardSpellGalleryRepository);
        //Gallery
        CardSpell cardSpell = cardSpellGalleryService.SumPowerCardSpellGallery();

        powerManager.power += cardSpell.power;
        powerManager.health += cardSpell.health;
        powerManager.physical_attack += cardSpell.physical_attack;
        powerManager.physical_defense += cardSpell.physical_defense;
        powerManager.magical_attack += cardSpell.magical_attack;
        powerManager.magical_defense += cardSpell.magical_defense;
        powerManager.chemical_attack += cardSpell.chemical_attack;
        powerManager.chemical_defense += cardSpell.chemical_defense;
        powerManager.atomic_attack += cardSpell.atomic_attack;
        powerManager.atomic_defense += cardSpell.atomic_defense;
        powerManager.mental_attack += cardSpell.mental_attack;
        powerManager.mental_defense += cardSpell.mental_defense;
        powerManager.speed += cardSpell.speed;
        powerManager.critical_damage_rate += cardSpell.critical_damage_rate;
        powerManager.critical_rate += cardSpell.critical_rate;
        powerManager.penetration_rate += cardSpell.penetration_rate;
        powerManager.evasion_rate += cardSpell.evasion_rate;
        powerManager.damage_absorption_rate += cardSpell.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardSpell.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardSpell.accuracy_rate;
        powerManager.lifesteal_rate += cardSpell.lifesteal_rate;
        powerManager.shield_strength += cardSpell.shield_strength;
        powerManager.tenacity += cardSpell.tenacity;
        powerManager.resistance_rate += cardSpell.resistance_rate;
        powerManager.combo_rate += cardSpell.combo_rate;
        powerManager.reflection_rate += cardSpell.reflection_rate;
        powerManager.mana += cardSpell.mana;
        powerManager.mana_regeneration_rate += cardSpell.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardSpell.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardSpell.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardSpell.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardSpell.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardSpell.percent_all_health;
        powerManager.percent_all_physical_attack += cardSpell.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardSpell.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardSpell.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardSpell.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardSpell.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardSpell.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardSpell.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardSpell.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardSpell.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardSpell.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCollaborationsPower()
    {
        PowerManager powerManager = new PowerManager();

        // Collaboration collaboration = new Collaboration();
        ICollaborationGalleryRepository collaborationGalleryRepository = new CollaborationGalleryRepository();
        CollaborationGalleryService collaborationGalleryService = new CollaborationGalleryService(collaborationGalleryRepository);
        // Gallery tổng hợp sức mạnh từ Collaboration Gallery
        Collaboration collaboration = collaborationGalleryService.SumPowerCollaborationsGallery();
        powerManager.power += collaboration.power;
        powerManager.health += collaboration.health;
        powerManager.physical_attack += collaboration.physical_attack;
        powerManager.physical_defense += collaboration.physical_defense;
        powerManager.magical_attack += collaboration.magical_attack;
        powerManager.magical_defense += collaboration.magical_defense;
        powerManager.chemical_attack += collaboration.chemical_attack;
        powerManager.chemical_defense += collaboration.chemical_defense;
        powerManager.atomic_attack += collaboration.atomic_attack;
        powerManager.atomic_defense += collaboration.atomic_defense;
        powerManager.mental_attack += collaboration.mental_attack;
        powerManager.mental_defense += collaboration.mental_defense;
        powerManager.speed += collaboration.speed;
        powerManager.critical_damage_rate += collaboration.critical_damage_rate;
        powerManager.critical_rate += collaboration.critical_rate;
        powerManager.penetration_rate += collaboration.penetration_rate;
        powerManager.evasion_rate += collaboration.evasion_rate;
        powerManager.damage_absorption_rate += collaboration.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += collaboration.vitality_regeneration_rate;
        powerManager.accuracy_rate += collaboration.accuracy_rate;
        powerManager.lifesteal_rate += collaboration.lifesteal_rate;
        powerManager.shield_strength += collaboration.shield_strength;
        powerManager.tenacity += collaboration.tenacity;
        powerManager.resistance_rate += collaboration.resistance_rate;
        powerManager.combo_rate += collaboration.combo_rate;
        powerManager.reflection_rate += collaboration.reflection_rate;
        powerManager.mana += collaboration.mana;
        powerManager.mana_regeneration_rate += collaboration.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += collaboration.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += collaboration.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += collaboration.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += collaboration.resistance_to_same_faction_rate;

        powerManager.percent_all_health += collaboration.percent_all_health;
        powerManager.percent_all_physical_attack += collaboration.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += collaboration.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += collaboration.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += collaboration.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += collaboration.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += collaboration.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += collaboration.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += collaboration.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += collaboration.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += collaboration.percent_all_mental_defense;

        IUserCollaborationRepository userCollaborationRepository = new UserCollaborationRepository();
        UserCollaborationService userCollaborationService = new UserCollaborationService(userCollaborationRepository);
        // Gallery tổng hợp sức mạnh từ User Collaborations
        collaboration = userCollaborationService.SumPowerUserCollaborations();
        powerManager.power += collaboration.power;
        powerManager.health += collaboration.health;
        powerManager.physical_attack += collaboration.physical_attack;
        powerManager.physical_defense += collaboration.physical_defense;
        powerManager.magical_attack += collaboration.magical_attack;
        powerManager.magical_defense += collaboration.magical_defense;
        powerManager.chemical_attack += collaboration.chemical_attack;
        powerManager.chemical_defense += collaboration.chemical_defense;
        powerManager.atomic_attack += collaboration.atomic_attack;
        powerManager.atomic_defense += collaboration.atomic_defense;
        powerManager.mental_attack += collaboration.mental_attack;
        powerManager.mental_defense += collaboration.mental_defense;
        powerManager.speed += collaboration.speed;
        powerManager.critical_damage_rate += collaboration.critical_damage_rate;
        powerManager.critical_rate += collaboration.critical_rate;
        powerManager.penetration_rate += collaboration.penetration_rate;
        powerManager.evasion_rate += collaboration.evasion_rate;
        powerManager.damage_absorption_rate += collaboration.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += collaboration.vitality_regeneration_rate;
        powerManager.accuracy_rate += collaboration.accuracy_rate;
        powerManager.lifesteal_rate += collaboration.lifesteal_rate;
        powerManager.shield_strength += collaboration.shield_strength;
        powerManager.tenacity += collaboration.tenacity;
        powerManager.resistance_rate += collaboration.resistance_rate;
        powerManager.combo_rate += collaboration.combo_rate;
        powerManager.reflection_rate += collaboration.reflection_rate;
        powerManager.mana += collaboration.mana;
        powerManager.mana_regeneration_rate += collaboration.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += collaboration.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += collaboration.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += collaboration.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += collaboration.resistance_to_same_faction_rate;

        ICollaborationRepository collaborationRepository = new CollaborationRepository();
        CollaborationService collaborationService = new CollaborationService(collaborationRepository);
        // Phần cộng dồn percent từ Collaboration Percent
        collaboration = collaborationService.SumPowerCollaborationsPercent();
        powerManager.percent_all_health += collaboration.percent_all_health;
        powerManager.percent_all_physical_attack += collaboration.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += collaboration.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += collaboration.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += collaboration.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += collaboration.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += collaboration.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += collaboration.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += collaboration.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += collaboration.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += collaboration.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetCollaborationEquipmentsPower()
    {
        PowerManager powerManager = new PowerManager();
        // CollaborationEquipment collaborationEquipment = new CollaborationEquipment();
        ICollaborationEquipmentGalleryRepository collaborationEquipmentGalleryRepository = new CollaborationEquipmentGalleryRepository();
        CollaborationEquipmentGalleryService collaborationEquipmentGalleryService = new CollaborationEquipmentGalleryService(collaborationEquipmentGalleryRepository);
        // Sum power from Gallery Equipments
        CollaborationEquipment collaborationEquipment = collaborationEquipmentGalleryService.SumPowerCollaborationEquipmentsGallery();
        powerManager.power += collaborationEquipment.power;
        powerManager.health += collaborationEquipment.health;
        powerManager.physical_attack += collaborationEquipment.physical_attack;
        powerManager.physical_defense += collaborationEquipment.physical_defense;
        powerManager.magical_attack += collaborationEquipment.magical_attack;
        powerManager.magical_defense += collaborationEquipment.magical_defense;
        powerManager.chemical_attack += collaborationEquipment.chemical_attack;
        powerManager.chemical_defense += collaborationEquipment.chemical_defense;
        powerManager.atomic_attack += collaborationEquipment.atomic_attack;
        powerManager.atomic_defense += collaborationEquipment.atomic_defense;
        powerManager.mental_attack += collaborationEquipment.mental_attack;
        powerManager.mental_defense += collaborationEquipment.mental_defense;
        powerManager.speed += collaborationEquipment.speed;
        powerManager.critical_damage_rate += collaborationEquipment.critical_damage_rate;
        powerManager.critical_rate += collaborationEquipment.critical_rate;
        powerManager.penetration_rate += collaborationEquipment.penetration_rate;
        powerManager.evasion_rate += collaborationEquipment.evasion_rate;
        powerManager.damage_absorption_rate += collaborationEquipment.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += collaborationEquipment.vitality_regeneration_rate;
        powerManager.accuracy_rate += collaborationEquipment.accuracy_rate;
        powerManager.lifesteal_rate += collaborationEquipment.lifesteal_rate;
        powerManager.shield_strength += collaborationEquipment.shield_strength;
        powerManager.tenacity += collaborationEquipment.tenacity;
        powerManager.resistance_rate += collaborationEquipment.resistance_rate;
        powerManager.combo_rate += collaborationEquipment.combo_rate;
        powerManager.reflection_rate += collaborationEquipment.reflection_rate;
        powerManager.mana += collaborationEquipment.mana;
        powerManager.mana_regeneration_rate += collaborationEquipment.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += collaborationEquipment.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += collaborationEquipment.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += collaborationEquipment.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += collaborationEquipment.resistance_to_same_faction_rate;

        powerManager.percent_all_health += collaborationEquipment.percent_all_health;
        powerManager.percent_all_physical_attack += collaborationEquipment.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += collaborationEquipment.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += collaborationEquipment.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += collaborationEquipment.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += collaborationEquipment.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += collaborationEquipment.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += collaborationEquipment.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += collaborationEquipment.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += collaborationEquipment.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += collaborationEquipment.percent_all_mental_defense;

        IUserCollaborationEquipmentRepository userCollaborationEquipmentRepository = new UserCollaborationEquipmentRepository();
        UserCollaborationEquipmentService userCollaborationEquipmentService = new UserCollaborationEquipmentService(userCollaborationEquipmentRepository);
        // Sum power from User Collaboration Equipments
        collaborationEquipment = userCollaborationEquipmentService.SumPowerUserCollaborationEquipments();
        powerManager.power += collaborationEquipment.power;
        powerManager.health += collaborationEquipment.health;
        powerManager.physical_attack += collaborationEquipment.physical_attack;
        powerManager.physical_defense += collaborationEquipment.physical_defense;
        powerManager.magical_attack += collaborationEquipment.magical_attack;
        powerManager.magical_defense += collaborationEquipment.magical_defense;
        powerManager.chemical_attack += collaborationEquipment.chemical_attack;
        powerManager.chemical_defense += collaborationEquipment.chemical_defense;
        powerManager.atomic_attack += collaborationEquipment.atomic_attack;
        powerManager.atomic_defense += collaborationEquipment.atomic_defense;
        powerManager.mental_attack += collaborationEquipment.mental_attack;
        powerManager.mental_defense += collaborationEquipment.mental_defense;
        powerManager.speed += collaborationEquipment.speed;
        powerManager.critical_damage_rate += collaborationEquipment.critical_damage_rate;
        powerManager.critical_rate += collaborationEquipment.critical_rate;
        powerManager.penetration_rate += collaborationEquipment.penetration_rate;
        powerManager.evasion_rate += collaborationEquipment.evasion_rate;
        powerManager.damage_absorption_rate += collaborationEquipment.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += collaborationEquipment.vitality_regeneration_rate;
        powerManager.accuracy_rate += collaborationEquipment.accuracy_rate;
        powerManager.lifesteal_rate += collaborationEquipment.lifesteal_rate;
        powerManager.shield_strength += collaborationEquipment.shield_strength;
        powerManager.tenacity += collaborationEquipment.tenacity;
        powerManager.resistance_rate += collaborationEquipment.resistance_rate;
        powerManager.combo_rate += collaborationEquipment.combo_rate;
        powerManager.reflection_rate += collaborationEquipment.reflection_rate;
        powerManager.mana += collaborationEquipment.mana;
        powerManager.mana_regeneration_rate += collaborationEquipment.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += collaborationEquipment.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += collaborationEquipment.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += collaborationEquipment.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += collaborationEquipment.resistance_to_same_faction_rate;

        return powerManager;
    }
    public PowerManager GetEquipmentsPower()
    {
        PowerManager powerManager = new PowerManager();
        // Equipments equipments = new Equipments();
        IEquipmentsGalleryRepository equipmentsGalleryRepository = new EquipmentsGalleryRepository();
        EquipmentsGalleryService equipmentsGalleryService = new EquipmentsGalleryService(equipmentsGalleryRepository);
        // Gallery Equipments power sum
        Equipments equipments = equipmentsGalleryService.SumPowerEquipmentsGallery();

        powerManager.power += equipments.power;
        powerManager.health += equipments.health;
        powerManager.physical_attack += equipments.physical_attack;
        powerManager.physical_defense += equipments.physical_defense;
        powerManager.magical_attack += equipments.magical_attack;
        powerManager.magical_defense += equipments.magical_defense;
        powerManager.chemical_attack += equipments.chemical_attack;
        powerManager.chemical_defense += equipments.chemical_defense;
        powerManager.atomic_attack += equipments.atomic_attack;
        powerManager.atomic_defense += equipments.atomic_defense;
        powerManager.mental_attack += equipments.mental_attack;
        powerManager.mental_defense += equipments.mental_defense;
        powerManager.speed += equipments.speed;
        powerManager.critical_damage_rate += equipments.critical_damage_rate;
        powerManager.critical_rate += equipments.critical_rate;
        powerManager.penetration_rate += equipments.penetration_rate;
        powerManager.evasion_rate += equipments.evasion_rate;
        powerManager.damage_absorption_rate += equipments.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += equipments.vitality_regeneration_rate;
        powerManager.accuracy_rate += equipments.accuracy_rate;
        powerManager.lifesteal_rate += equipments.lifesteal_rate;
        powerManager.shield_strength += equipments.shield_strength;
        powerManager.tenacity += equipments.tenacity;
        powerManager.resistance_rate += equipments.resistance_rate;
        powerManager.combo_rate += equipments.combo_rate;
        powerManager.reflection_rate += equipments.reflection_rate;
        powerManager.mana += equipments.mana;
        powerManager.mana_regeneration_rate += equipments.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += equipments.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += equipments.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += equipments.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += equipments.resistance_to_same_faction_rate;

        powerManager.percent_all_health += equipments.percent_all_health;
        powerManager.percent_all_physical_attack += equipments.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += equipments.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += equipments.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += equipments.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += equipments.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += equipments.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += equipments.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += equipments.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += equipments.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += equipments.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetMagicFormationCirclePower()
    {
        PowerManager powerManager = new PowerManager();
        // MagicFormationCircle magicFormationCircle = new MagicFormationCircle();
        IMagicFormationCircleGalleryRepository magicFormationCircleGalleryRepository = new MagicFormationCircleGalleryRepository();
        MagicFormationCircleGalleryService magicFormationCircleGalleryService = new MagicFormationCircleGalleryService(magicFormationCircleGalleryRepository);
        // Gallery
        MagicFormationCircle magicFormationCircle = magicFormationCircleGalleryService.SumPowerMagicFormationCircleGallery();
        powerManager.power += magicFormationCircle.power;
        powerManager.health += magicFormationCircle.health;
        powerManager.physical_attack += magicFormationCircle.physical_attack;
        powerManager.physical_defense += magicFormationCircle.physical_defense;
        powerManager.magical_attack += magicFormationCircle.magical_attack;
        powerManager.magical_defense += magicFormationCircle.magical_defense;
        powerManager.chemical_attack += magicFormationCircle.chemical_attack;
        powerManager.chemical_defense += magicFormationCircle.chemical_defense;
        powerManager.atomic_attack += magicFormationCircle.atomic_attack;
        powerManager.atomic_defense += magicFormationCircle.atomic_defense;
        powerManager.mental_attack += magicFormationCircle.mental_attack;
        powerManager.mental_defense += magicFormationCircle.mental_defense;
        powerManager.speed += magicFormationCircle.speed;
        powerManager.critical_damage_rate += magicFormationCircle.critical_damage_rate;
        powerManager.critical_rate += magicFormationCircle.critical_rate;
        powerManager.penetration_rate += magicFormationCircle.penetration_rate;
        powerManager.evasion_rate += magicFormationCircle.evasion_rate;
        powerManager.damage_absorption_rate += magicFormationCircle.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += magicFormationCircle.vitality_regeneration_rate;
        powerManager.accuracy_rate += magicFormationCircle.accuracy_rate;
        powerManager.lifesteal_rate += magicFormationCircle.lifesteal_rate;
        powerManager.shield_strength += magicFormationCircle.shield_strength;
        powerManager.tenacity += magicFormationCircle.tenacity;
        powerManager.resistance_rate += magicFormationCircle.resistance_rate;
        powerManager.combo_rate += magicFormationCircle.combo_rate;
        powerManager.reflection_rate += magicFormationCircle.reflection_rate;
        powerManager.mana += magicFormationCircle.mana;
        powerManager.mana_regeneration_rate += magicFormationCircle.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += magicFormationCircle.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += magicFormationCircle.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += magicFormationCircle.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += magicFormationCircle.resistance_to_same_faction_rate;

        powerManager.percent_all_health += magicFormationCircle.percent_all_health;
        powerManager.percent_all_physical_attack += magicFormationCircle.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += magicFormationCircle.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += magicFormationCircle.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += magicFormationCircle.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += magicFormationCircle.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += magicFormationCircle.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += magicFormationCircle.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += magicFormationCircle.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += magicFormationCircle.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += magicFormationCircle.percent_all_mental_defense;

        IUserMagicFormationCircleRepository userMagicFormationCircleRepository = new UserMagicFormationCirlceRepository();
        UserMagicFormationCircleService userMagicFormationCircleService = new UserMagicFormationCircleService(userMagicFormationCircleRepository);
        // User
        magicFormationCircle = userMagicFormationCircleService.SumPowerUserMagicFormationCircle();
        powerManager.power += magicFormationCircle.power;
        powerManager.health += magicFormationCircle.health;
        powerManager.physical_attack += magicFormationCircle.physical_attack;
        powerManager.physical_defense += magicFormationCircle.physical_defense;
        powerManager.magical_attack += magicFormationCircle.magical_attack;
        powerManager.magical_defense += magicFormationCircle.magical_defense;
        powerManager.chemical_attack += magicFormationCircle.chemical_attack;
        powerManager.chemical_defense += magicFormationCircle.chemical_defense;
        powerManager.atomic_attack += magicFormationCircle.atomic_attack;
        powerManager.atomic_defense += magicFormationCircle.atomic_defense;
        powerManager.mental_attack += magicFormationCircle.mental_attack;
        powerManager.mental_defense += magicFormationCircle.mental_defense;
        powerManager.speed += magicFormationCircle.speed;
        powerManager.critical_damage_rate += magicFormationCircle.critical_damage_rate;
        powerManager.critical_rate += magicFormationCircle.critical_rate;
        powerManager.penetration_rate += magicFormationCircle.penetration_rate;
        powerManager.evasion_rate += magicFormationCircle.evasion_rate;
        powerManager.damage_absorption_rate += magicFormationCircle.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += magicFormationCircle.vitality_regeneration_rate;
        powerManager.accuracy_rate += magicFormationCircle.accuracy_rate;
        powerManager.lifesteal_rate += magicFormationCircle.lifesteal_rate;
        powerManager.shield_strength += magicFormationCircle.shield_strength;
        powerManager.tenacity += magicFormationCircle.tenacity;
        powerManager.resistance_rate += magicFormationCircle.resistance_rate;
        powerManager.combo_rate += magicFormationCircle.combo_rate;
        powerManager.reflection_rate += magicFormationCircle.reflection_rate;
        powerManager.mana += magicFormationCircle.mana;
        powerManager.mana_regeneration_rate += magicFormationCircle.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += magicFormationCircle.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += magicFormationCircle.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += magicFormationCircle.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += magicFormationCircle.resistance_to_same_faction_rate;

        IMagicFormationCircleRepository magicFormationCircleRepository = new MagicFormationCircleRepository();
        MagicFormationCircleService magicFormationCircleService = new MagicFormationCircleService(magicFormationCircleRepository);
        // Percent
        magicFormationCircle = magicFormationCircleService.SumPowerMagicFormationCirclePercent();
        powerManager.percent_all_health += magicFormationCircle.percent_all_health;
        powerManager.percent_all_physical_attack += magicFormationCircle.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += magicFormationCircle.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += magicFormationCircle.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += magicFormationCircle.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += magicFormationCircle.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += magicFormationCircle.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += magicFormationCircle.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += magicFormationCircle.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += magicFormationCircle.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += magicFormationCircle.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetRelicsPower()
    {
        // Relics relics = new Relics();
        PowerManager powerManager = new PowerManager();

        IRelicsGalleryRepository relicsGalleryRepository = new RelicsGalleryRepository();
        RelicsGalleryService relicsGalleryService = new RelicsGalleryService(relicsGalleryRepository);
        // Gallery
        Relics relics = relicsGalleryService.SumPowerRelicsGallery();
        powerManager.power += relics.power;
        powerManager.health += relics.health;
        powerManager.physical_attack += relics.physical_attack;
        powerManager.physical_defense += relics.physical_defense;
        powerManager.magical_attack += relics.magical_attack;
        powerManager.magical_defense += relics.magical_defense;
        powerManager.chemical_attack += relics.chemical_attack;
        powerManager.chemical_defense += relics.chemical_defense;
        powerManager.atomic_attack += relics.atomic_attack;
        powerManager.atomic_defense += relics.atomic_defense;
        powerManager.mental_attack += relics.mental_attack;
        powerManager.mental_defense += relics.mental_defense;
        powerManager.speed += relics.speed;
        powerManager.critical_damage_rate += relics.critical_damage_rate;
        powerManager.critical_rate += relics.critical_rate;
        powerManager.penetration_rate += relics.penetration_rate;
        powerManager.evasion_rate += relics.evasion_rate;
        powerManager.damage_absorption_rate += relics.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += relics.vitality_regeneration_rate;
        powerManager.accuracy_rate += relics.accuracy_rate;
        powerManager.lifesteal_rate += relics.lifesteal_rate;
        powerManager.shield_strength += relics.shield_strength;
        powerManager.tenacity += relics.tenacity;
        powerManager.resistance_rate += relics.resistance_rate;
        powerManager.combo_rate += relics.combo_rate;
        powerManager.reflection_rate += relics.reflection_rate;
        powerManager.mana += relics.mana;
        powerManager.mana_regeneration_rate += relics.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += relics.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += relics.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += relics.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += relics.resistance_to_same_faction_rate;

        powerManager.percent_all_health += relics.percent_all_health;
        powerManager.percent_all_physical_attack += relics.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += relics.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += relics.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += relics.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += relics.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += relics.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += relics.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += relics.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += relics.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += relics.percent_all_mental_defense;

        IUserRelicsRepository userRelicsRepository = new UserRelicsRepository();
        UserRelicsService userRelicsService = new UserRelicsService(userRelicsRepository);
        // User
        relics = userRelicsService.SumPowerUserRelics();
        powerManager.power += relics.power;
        powerManager.health += relics.health;
        powerManager.physical_attack += relics.physical_attack;
        powerManager.physical_defense += relics.physical_defense;
        powerManager.magical_attack += relics.magical_attack;
        powerManager.magical_defense += relics.magical_defense;
        powerManager.chemical_attack += relics.chemical_attack;
        powerManager.chemical_defense += relics.chemical_defense;
        powerManager.atomic_attack += relics.atomic_attack;
        powerManager.atomic_defense += relics.atomic_defense;
        powerManager.mental_attack += relics.mental_attack;
        powerManager.mental_defense += relics.mental_defense;
        powerManager.speed += relics.speed;
        powerManager.critical_damage_rate += relics.critical_damage_rate;
        powerManager.critical_rate += relics.critical_rate;
        powerManager.penetration_rate += relics.penetration_rate;
        powerManager.evasion_rate += relics.evasion_rate;
        powerManager.damage_absorption_rate += relics.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += relics.vitality_regeneration_rate;
        powerManager.accuracy_rate += relics.accuracy_rate;
        powerManager.lifesteal_rate += relics.lifesteal_rate;
        powerManager.shield_strength += relics.shield_strength;
        powerManager.tenacity += relics.tenacity;
        powerManager.resistance_rate += relics.resistance_rate;
        powerManager.combo_rate += relics.combo_rate;
        powerManager.reflection_rate += relics.reflection_rate;
        powerManager.mana += relics.mana;
        powerManager.mana_regeneration_rate += relics.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += relics.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += relics.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += relics.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += relics.resistance_to_same_faction_rate;

        IRelicsRepository relicsRepository = new RelicsRepository();
        RelicsService relicsService = new RelicsService(relicsRepository);
        // Percent
        relics = relicsService.SumPowerRelicsPercent();
        powerManager.percent_all_health += relics.percent_all_health;
        powerManager.percent_all_physical_attack += relics.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += relics.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += relics.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += relics.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += relics.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += relics.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += relics.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += relics.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += relics.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += relics.percent_all_mental_defense;

        return powerManager;
    }
    public PowerManager GetMedalsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Medals medals = new Medals();
        IMedalsGalleryRepository medalsGalleryRepository = new MedalsGalleryRepository();
        MedalsGalleryService medalsGalleryService = new MedalsGalleryService(medalsGalleryRepository);
        // Gallery
        Medals medals = medalsGalleryService.SumPowerMedalsGallery();
        powerManager.power += medals.power;
        powerManager.health += medals.health;
        powerManager.physical_attack += medals.physical_attack;
        powerManager.physical_defense += medals.physical_defense;
        powerManager.magical_attack += medals.magical_attack;
        powerManager.magical_defense += medals.magical_defense;
        powerManager.chemical_attack += medals.chemical_attack;
        powerManager.chemical_defense += medals.chemical_defense;
        powerManager.atomic_attack += medals.atomic_attack;
        powerManager.atomic_defense += medals.atomic_defense;
        powerManager.mental_attack += medals.mental_attack;
        powerManager.mental_defense += medals.mental_defense;
        powerManager.speed += medals.speed;
        powerManager.critical_damage_rate += medals.critical_damage_rate;
        powerManager.critical_rate += medals.critical_rate;
        powerManager.penetration_rate += medals.penetration_rate;
        powerManager.evasion_rate += medals.evasion_rate;
        powerManager.damage_absorption_rate += medals.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += medals.vitality_regeneration_rate;
        powerManager.accuracy_rate += medals.accuracy_rate;
        powerManager.lifesteal_rate += medals.lifesteal_rate;
        powerManager.shield_strength += medals.shield_strength;
        powerManager.tenacity += medals.tenacity;
        powerManager.resistance_rate += medals.resistance_rate;
        powerManager.combo_rate += medals.combo_rate;
        powerManager.reflection_rate += medals.reflection_rate;
        powerManager.mana += medals.mana;
        powerManager.mana_regeneration_rate += medals.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += medals.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += medals.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += medals.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += medals.resistance_to_same_faction_rate;

        powerManager.percent_all_health += medals.percent_all_health;
        powerManager.percent_all_physical_attack += medals.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += medals.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += medals.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += medals.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += medals.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += medals.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += medals.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += medals.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += medals.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += medals.percent_all_mental_defense;

        IUserMedalsRepository userMedalsRepository = new UserMedalsRepository();
        UserMedalsService userMedalsService = new UserMedalsService(userMedalsRepository);
        // User Medals (Gallery)
        medals = userMedalsService.SumPowerUserMedals(); // Giả định SumPowerUserMedals cũng trả về một đối tượng Medals mới hoặc đã được reset
        powerManager.power += medals.power;
        powerManager.health += medals.health;
        powerManager.physical_attack += medals.physical_attack;
        powerManager.physical_defense += medals.physical_defense;
        powerManager.magical_attack += medals.magical_attack;
        powerManager.magical_defense += medals.magical_defense;
        powerManager.chemical_attack += medals.chemical_attack;
        powerManager.chemical_defense += medals.chemical_defense;
        powerManager.atomic_attack += medals.atomic_attack;
        powerManager.atomic_defense += medals.atomic_defense;
        powerManager.mental_attack += medals.mental_attack;
        powerManager.mental_defense += medals.mental_defense;
        powerManager.speed += medals.speed;
        powerManager.critical_damage_rate += medals.critical_damage_rate;
        powerManager.critical_rate += medals.critical_rate;
        powerManager.penetration_rate += medals.penetration_rate;
        powerManager.evasion_rate += medals.evasion_rate;
        powerManager.damage_absorption_rate += medals.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += medals.vitality_regeneration_rate;
        powerManager.accuracy_rate += medals.accuracy_rate;
        powerManager.lifesteal_rate += medals.lifesteal_rate;
        powerManager.shield_strength += medals.shield_strength;
        powerManager.tenacity += medals.tenacity;
        powerManager.resistance_rate += medals.resistance_rate;
        powerManager.combo_rate += medals.combo_rate;
        powerManager.reflection_rate += medals.reflection_rate;
        powerManager.mana += medals.mana;
        powerManager.mana_regeneration_rate += medals.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += medals.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += medals.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += medals.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += medals.resistance_to_same_faction_rate;

        IMedalsRepository medalsRepository = new MedalsRepository();
        MedalsService medalsService = new MedalsService(medalsRepository);
        // Percent
        medals = medalsService.SumPowerMedalsPercent(); // Giả định SumPowerMedalsPercent cũng trả về một đối tượng Medals mới hoặc đã được reset
        powerManager.percent_all_health += medals.percent_all_health;
        powerManager.percent_all_physical_attack += medals.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += medals.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += medals.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += medals.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += medals.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += medals.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += medals.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += medals.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += medals.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += medals.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetPetsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Pets pets = new Pets();
        IPetsGalleryRepository petsGalleryRepository = new PetsGalleryRepository();
        PetsGalleryService petsGalleryService = new PetsGalleryService(petsGalleryRepository);
        // Gallery
        Pets pets = petsGalleryService.SumPowerPetsGallery();
        powerManager.power += pets.power;
        powerManager.health += pets.health;
        powerManager.physical_attack += pets.physical_attack;
        powerManager.physical_defense += pets.physical_defense;
        powerManager.magical_attack += pets.magical_attack;
        powerManager.magical_defense += pets.magical_defense;
        powerManager.chemical_attack += pets.chemical_attack;
        powerManager.chemical_defense += pets.chemical_defense;
        powerManager.atomic_attack += pets.atomic_attack;
        powerManager.atomic_defense += pets.atomic_defense;
        powerManager.mental_attack += pets.mental_attack;
        powerManager.mental_defense += pets.mental_defense;
        powerManager.speed += pets.speed;
        powerManager.critical_damage_rate += pets.critical_damage_rate;
        powerManager.critical_rate += pets.critical_rate;
        powerManager.penetration_rate += pets.penetration_rate;
        powerManager.evasion_rate += pets.evasion_rate;
        powerManager.damage_absorption_rate += pets.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += pets.vitality_regeneration_rate;
        powerManager.accuracy_rate += pets.accuracy_rate;
        powerManager.lifesteal_rate += pets.lifesteal_rate;
        powerManager.shield_strength += pets.shield_strength;
        powerManager.tenacity += pets.tenacity;
        powerManager.resistance_rate += pets.resistance_rate;
        powerManager.combo_rate += pets.combo_rate;
        powerManager.reflection_rate += pets.reflection_rate;
        powerManager.mana += pets.mana;
        powerManager.mana_regeneration_rate += pets.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += pets.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += pets.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += pets.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += pets.resistance_to_same_faction_rate;

        powerManager.percent_all_health += pets.percent_all_health;
        powerManager.percent_all_physical_attack += pets.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += pets.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += pets.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += pets.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += pets.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += pets.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += pets.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += pets.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += pets.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += pets.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetSymbolsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Symbols symbols = new Symbols();
        ISymbolsGalleryRepository symbolsGalleryRepository = new SymbolsGalleryRepository();
        SymbolsGalleryService symbolsGalleryService = new SymbolsGalleryService(symbolsGalleryRepository);
        // Gallery
        Symbols symbols = symbolsGalleryService.SumPowerSymbolsGallery();
        powerManager.power += symbols.power;
        powerManager.health += symbols.health;
        powerManager.physical_attack += symbols.physical_attack;
        powerManager.physical_defense += symbols.physical_defense;
        powerManager.magical_attack += symbols.magical_attack;
        powerManager.magical_defense += symbols.magical_defense;
        powerManager.chemical_attack += symbols.chemical_attack;
        powerManager.chemical_defense += symbols.chemical_defense;
        powerManager.atomic_attack += symbols.atomic_attack;
        powerManager.atomic_defense += symbols.atomic_defense;
        powerManager.mental_attack += symbols.mental_attack;
        powerManager.mental_defense += symbols.mental_defense;
        powerManager.speed += symbols.speed;
        powerManager.critical_damage_rate += symbols.critical_damage_rate;
        powerManager.critical_rate += symbols.critical_rate;
        powerManager.penetration_rate += symbols.penetration_rate;
        powerManager.evasion_rate += symbols.evasion_rate;
        powerManager.damage_absorption_rate += symbols.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += symbols.vitality_regeneration_rate;
        powerManager.accuracy_rate += symbols.accuracy_rate;
        powerManager.lifesteal_rate += symbols.lifesteal_rate;
        powerManager.shield_strength += symbols.shield_strength;
        powerManager.tenacity += symbols.tenacity;
        powerManager.resistance_rate += symbols.resistance_rate;
        powerManager.combo_rate += symbols.combo_rate;
        powerManager.reflection_rate += symbols.reflection_rate;
        powerManager.mana += symbols.mana;
        powerManager.mana_regeneration_rate += symbols.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += symbols.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += symbols.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += symbols.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += symbols.resistance_to_same_faction_rate;

        powerManager.percent_all_health += symbols.percent_all_health;
        powerManager.percent_all_physical_attack += symbols.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += symbols.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += symbols.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += symbols.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += symbols.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += symbols.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += symbols.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += symbols.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += symbols.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += symbols.percent_all_mental_defense;

        IUserSymbolsRepository userSymbolsRepository = new UserSymbolsRepository();
        UserSymbolsService userSymbolsService = new UserSymbolsService(userSymbolsRepository);
        // User Symbols (Gallery)
        symbols = userSymbolsService.SumPowerUserSymbols(); // Giả định SumPowerUserSymbols cũng trả về một đối tượng Symbols mới hoặc đã được reset
        powerManager.power += symbols.power;
        powerManager.health += symbols.health;
        powerManager.physical_attack += symbols.physical_attack;
        powerManager.physical_defense += symbols.physical_defense;
        powerManager.magical_attack += symbols.magical_attack;
        powerManager.magical_defense += symbols.magical_defense;
        powerManager.chemical_attack += symbols.chemical_attack;
        powerManager.chemical_defense += symbols.chemical_defense;
        powerManager.atomic_attack += symbols.atomic_attack;
        powerManager.atomic_defense += symbols.atomic_defense;
        powerManager.mental_attack += symbols.mental_attack;
        powerManager.mental_defense += symbols.mental_defense;
        powerManager.speed += symbols.speed;
        powerManager.critical_damage_rate += symbols.critical_damage_rate;
        powerManager.critical_rate += symbols.critical_rate;
        powerManager.penetration_rate += symbols.penetration_rate;
        powerManager.evasion_rate += symbols.evasion_rate;
        powerManager.damage_absorption_rate += symbols.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += symbols.vitality_regeneration_rate;
        powerManager.accuracy_rate += symbols.accuracy_rate;
        powerManager.lifesteal_rate += symbols.lifesteal_rate;
        powerManager.shield_strength += symbols.shield_strength;
        powerManager.tenacity += symbols.tenacity;
        powerManager.resistance_rate += symbols.resistance_rate;
        powerManager.combo_rate += symbols.combo_rate;
        powerManager.reflection_rate += symbols.reflection_rate;
        powerManager.mana += symbols.mana;
        powerManager.mana_regeneration_rate += symbols.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += symbols.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += symbols.resistance_to_different_faction_rate;
        powerManager.resistance_to_same_faction_rate += symbols.resistance_to_same_faction_rate;
        powerManager.damage_to_same_faction_rate += symbols.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += symbols.resistance_to_same_faction_rate;

        ISymbolsRepository symbolsRepository = new SymbolsRepository();
        SymbolsService symbolsService = new SymbolsService(symbolsRepository);
        // Percent
        symbols = symbolsService.SumPowerSymbolsPercent(); // Giả định SumPowerSymbolsPercent cũng trả về một đối tượng Symbols mới hoặc đã được reset
        powerManager.percent_all_health += symbols.percent_all_health;
        powerManager.percent_all_physical_attack += symbols.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += symbols.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += symbols.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += symbols.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += symbols.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += symbols.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += symbols.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += symbols.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += symbols.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += symbols.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetSkillsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Skills skills = new Skills();
        ISkillsGalleryRepository skillsGalleryRepository = new SkillsGalleryRepository();
        SkillsGalleryService skillsGalleryService = new SkillsGalleryService(skillsGalleryRepository);
        // Gallery
        Skills skills = skillsGalleryService.SumPowerSkillsGallery();
        powerManager.power += skills.power;
        powerManager.health += skills.health;
        powerManager.physical_attack += skills.physical_attack;
        powerManager.physical_defense += skills.physical_defense;
        powerManager.magical_attack += skills.magical_attack;
        powerManager.magical_defense += skills.magical_defense;
        powerManager.chemical_attack += skills.chemical_attack;
        powerManager.chemical_defense += skills.chemical_defense;
        powerManager.atomic_attack += skills.atomic_attack;
        powerManager.atomic_defense += skills.atomic_defense;
        powerManager.mental_attack += skills.mental_attack;
        powerManager.mental_defense += skills.mental_defense;
        powerManager.speed += skills.speed;
        powerManager.critical_damage_rate += skills.critical_damage_rate;
        powerManager.critical_rate += skills.critical_rate;
        powerManager.penetration_rate += skills.penetration_rate;
        powerManager.evasion_rate += skills.evasion_rate;
        powerManager.damage_absorption_rate += skills.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += skills.vitality_regeneration_rate;
        powerManager.accuracy_rate += skills.accuracy_rate;
        powerManager.lifesteal_rate += skills.lifesteal_rate;
        powerManager.shield_strength += skills.shield_strength;
        powerManager.tenacity += skills.tenacity;
        powerManager.resistance_rate += skills.resistance_rate;
        powerManager.combo_rate += skills.combo_rate;
        powerManager.reflection_rate += skills.reflection_rate;
        powerManager.mana += skills.mana;
        powerManager.mana_regeneration_rate += skills.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += skills.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += skills.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += skills.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += skills.resistance_to_same_faction_rate;

        powerManager.percent_all_health += skills.percent_all_health;
        powerManager.percent_all_physical_attack += skills.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += skills.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += skills.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += skills.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += skills.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += skills.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += skills.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += skills.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += skills.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += skills.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetTitlesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ITitlesGalleryRepository titlesGalleryRepository = new TitlesGalleryRepository();
        TitlesGalleryService titlesGalleryService = new TitlesGalleryService(titlesGalleryRepository);
        // Gallery
        Titles titles = titlesGalleryService.SumPowerTitlesGallery();
        powerManager.power += titles.power;
        powerManager.health += titles.health;
        powerManager.physical_attack += titles.physical_attack;
        powerManager.physical_defense += titles.physical_defense;
        powerManager.magical_attack += titles.magical_attack;
        powerManager.magical_defense += titles.magical_defense;
        powerManager.chemical_attack += titles.chemical_attack;
        powerManager.chemical_defense += titles.chemical_defense;
        powerManager.atomic_attack += titles.atomic_attack;
        powerManager.atomic_defense += titles.atomic_defense;
        powerManager.mental_attack += titles.mental_attack;
        powerManager.mental_defense += titles.mental_defense;
        powerManager.speed += titles.speed;
        powerManager.critical_damage_rate += titles.critical_damage_rate;
        powerManager.critical_rate += titles.critical_rate;
        powerManager.penetration_rate += titles.penetration_rate;
        powerManager.evasion_rate += titles.evasion_rate;
        powerManager.damage_absorption_rate += titles.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += titles.vitality_regeneration_rate;
        powerManager.accuracy_rate += titles.accuracy_rate;
        powerManager.lifesteal_rate += titles.lifesteal_rate;
        powerManager.shield_strength += titles.shield_strength;
        powerManager.tenacity += titles.tenacity;
        powerManager.resistance_rate += titles.resistance_rate;
        powerManager.combo_rate += titles.combo_rate;
        powerManager.reflection_rate += titles.reflection_rate;
        powerManager.mana += titles.mana;
        powerManager.mana_regeneration_rate += titles.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += titles.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += titles.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += titles.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += titles.resistance_to_same_faction_rate;

        powerManager.percent_all_health += titles.percent_all_health;
        powerManager.percent_all_physical_attack += titles.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += titles.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += titles.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += titles.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += titles.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += titles.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += titles.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += titles.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += titles.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += titles.percent_all_mental_defense;

        IUserTitlesRepository userTitlesRepository = new UserTitlesRepository();
        UserTitlesService userTitlesService = new UserTitlesService(userTitlesRepository);
        // User Titles (Gallery)
        titles = userTitlesService.SumPowerUserTitles(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.power += titles.power;
        powerManager.health += titles.health;
        powerManager.physical_attack += titles.physical_attack;
        powerManager.physical_defense += titles.physical_defense;
        powerManager.magical_attack += titles.magical_attack;
        powerManager.magical_defense += titles.magical_defense;
        powerManager.chemical_attack += titles.chemical_attack;
        powerManager.chemical_defense += titles.chemical_defense;
        powerManager.atomic_attack += titles.atomic_attack;
        powerManager.atomic_defense += titles.atomic_defense;
        powerManager.mental_attack += titles.mental_attack;
        powerManager.mental_defense += titles.mental_defense;
        powerManager.speed += titles.speed;
        powerManager.critical_damage_rate += titles.critical_damage_rate;
        powerManager.critical_rate += titles.critical_rate;
        powerManager.penetration_rate += titles.penetration_rate;
        powerManager.evasion_rate += titles.evasion_rate;
        powerManager.damage_absorption_rate += titles.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += titles.vitality_regeneration_rate;
        powerManager.accuracy_rate += titles.accuracy_rate;
        powerManager.lifesteal_rate += titles.lifesteal_rate;
        powerManager.shield_strength += titles.shield_strength;
        powerManager.tenacity += titles.tenacity;
        powerManager.resistance_rate += titles.resistance_rate;
        powerManager.combo_rate += titles.combo_rate;
        powerManager.reflection_rate += titles.reflection_rate;
        powerManager.mana += titles.mana;
        powerManager.mana_regeneration_rate += titles.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += titles.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += titles.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += titles.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += titles.resistance_to_same_faction_rate;

        ITitlesRepository titlesRepository = new TitlesRepository();
        TitlesService titlesService = new TitlesService(titlesRepository);
        // Percent
        titles = titlesService.SumPowerTitlesPercent(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.percent_all_health += titles.percent_all_health;
        powerManager.percent_all_physical_attack += titles.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += titles.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += titles.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += titles.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += titles.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += titles.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += titles.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += titles.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += titles.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += titles.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetTalismanPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Talisman talisman = new Talisman();
        ITalismanGalleryRepository talismanGalleryRepository = new TalismanGalleryRepository();
        TalismanGalleryService talismanGalleryService = new TalismanGalleryService(talismanGalleryRepository);
        // Gallery
        Talisman talisman = talismanGalleryService.SumPowerTalismanGallery();
        powerManager.power += talisman.power;
        powerManager.health += talisman.health;
        powerManager.physical_attack += talisman.physical_attack;
        powerManager.physical_defense += talisman.physical_defense;
        powerManager.magical_attack += talisman.magical_attack;
        powerManager.magical_defense += talisman.magical_defense;
        powerManager.chemical_attack += talisman.chemical_attack;
        powerManager.chemical_defense += talisman.chemical_defense;
        powerManager.atomic_attack += talisman.atomic_attack;
        powerManager.atomic_defense += talisman.atomic_defense;
        powerManager.mental_attack += talisman.mental_attack;
        powerManager.mental_defense += talisman.mental_defense;
        powerManager.speed += talisman.speed;
        powerManager.critical_damage_rate += talisman.critical_damage_rate;
        powerManager.critical_rate += talisman.critical_rate;
        powerManager.penetration_rate += talisman.penetration_rate;
        powerManager.evasion_rate += talisman.evasion_rate;
        powerManager.damage_absorption_rate += talisman.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += talisman.vitality_regeneration_rate;
        powerManager.accuracy_rate += talisman.accuracy_rate;
        powerManager.lifesteal_rate += talisman.lifesteal_rate;
        powerManager.shield_strength += talisman.shield_strength;
        powerManager.tenacity += talisman.tenacity;
        powerManager.resistance_rate += talisman.resistance_rate;
        powerManager.combo_rate += talisman.combo_rate;
        powerManager.reflection_rate += talisman.reflection_rate;
        powerManager.mana += talisman.mana;
        powerManager.mana_regeneration_rate += talisman.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += talisman.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += talisman.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += talisman.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += talisman.resistance_to_same_faction_rate;

        powerManager.percent_all_health += talisman.percent_all_health;
        powerManager.percent_all_physical_attack += talisman.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += talisman.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += talisman.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += talisman.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += talisman.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += talisman.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += talisman.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += talisman.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += talisman.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += talisman.percent_all_mental_defense;

        IUserTalismanRepository userTalismanRepository = new UserTalismanRepository();
        UserTalismanService userTalismanService = new UserTalismanService(userTalismanRepository);
        // User
        talisman = userTalismanService.SumPowerUserTalisman(); // Giả định SumPowerUserTalisman cũng trả về một đối tượng Talisman mới hoặc đã được reset
        powerManager.power += talisman.power;
        powerManager.health += talisman.health;
        powerManager.physical_attack += talisman.physical_attack;
        powerManager.physical_defense += talisman.physical_defense;
        powerManager.magical_attack += talisman.magical_attack;
        powerManager.magical_defense += talisman.magical_defense;
        powerManager.chemical_attack += talisman.chemical_attack;
        powerManager.chemical_defense += talisman.chemical_defense;
        powerManager.atomic_attack += talisman.atomic_attack;
        powerManager.atomic_defense += talisman.atomic_defense;
        powerManager.mental_attack += talisman.mental_attack;
        powerManager.mental_defense += talisman.mental_defense;
        powerManager.speed += talisman.speed;
        powerManager.critical_damage_rate += talisman.critical_damage_rate;
        powerManager.critical_rate += talisman.critical_rate;
        powerManager.penetration_rate += talisman.penetration_rate;
        powerManager.evasion_rate += talisman.evasion_rate;
        powerManager.damage_absorption_rate += talisman.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += talisman.vitality_regeneration_rate;
        powerManager.accuracy_rate += talisman.accuracy_rate;
        powerManager.lifesteal_rate += talisman.lifesteal_rate;
        powerManager.shield_strength += talisman.shield_strength;
        powerManager.tenacity += talisman.tenacity;
        powerManager.resistance_rate += talisman.resistance_rate;
        powerManager.combo_rate += talisman.combo_rate;
        powerManager.reflection_rate += talisman.reflection_rate;
        powerManager.mana += talisman.mana;
        powerManager.mana_regeneration_rate += talisman.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += talisman.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += talisman.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += talisman.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += talisman.resistance_to_same_faction_rate;

        ITalismanRepository talismanRepository = new TalismanRepository();
        TalismanService talismanService = new TalismanService(talismanRepository);
        // Percent
        talisman = talismanService.SumPowerTalismanPercent(); // Giả định SumPowerTalismanPercent cũng trả về một đối tượng Talisman mới hoặc đã được reset
        powerManager.percent_all_health += talisman.percent_all_health;
        powerManager.percent_all_physical_attack += talisman.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += talisman.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += talisman.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += talisman.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += talisman.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += talisman.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += talisman.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += talisman.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += talisman.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += talisman.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetPuppetPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Puppet puppet = new Puppet();
        IPuppetGalleryRepository puppetGalleryRepository = new PuppetGalleryRepository();
        PuppetGalleryService puppetGalleryService = new PuppetGalleryService(puppetGalleryRepository);
        // Gallery
        Puppet puppet = puppetGalleryService.SumPowerPuppetGallery();
        powerManager.power += puppet.power;
        powerManager.health += puppet.health;
        powerManager.physical_attack += puppet.physical_attack;
        powerManager.physical_defense += puppet.physical_defense;
        powerManager.magical_attack += puppet.magical_attack;
        powerManager.magical_defense += puppet.magical_defense;
        powerManager.chemical_attack += puppet.chemical_attack;
        powerManager.chemical_defense += puppet.chemical_defense;
        powerManager.atomic_attack += puppet.atomic_attack;
        powerManager.atomic_defense += puppet.atomic_defense;
        powerManager.mental_attack += puppet.mental_attack;
        powerManager.mental_defense += puppet.mental_defense;
        powerManager.speed += puppet.speed;
        powerManager.critical_damage_rate += puppet.critical_damage_rate;
        powerManager.critical_rate += puppet.critical_rate;
        powerManager.penetration_rate += puppet.penetration_rate;
        powerManager.evasion_rate += puppet.evasion_rate;
        powerManager.damage_absorption_rate += puppet.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += puppet.vitality_regeneration_rate;
        powerManager.accuracy_rate += puppet.accuracy_rate;
        powerManager.lifesteal_rate += puppet.lifesteal_rate;
        powerManager.shield_strength += puppet.shield_strength;
        powerManager.tenacity += puppet.tenacity;
        powerManager.resistance_rate += puppet.resistance_rate;
        powerManager.combo_rate += puppet.combo_rate;
        powerManager.reflection_rate += puppet.reflection_rate;
        powerManager.mana += puppet.mana;
        powerManager.mana_regeneration_rate += puppet.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += puppet.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += puppet.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += puppet.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += puppet.resistance_to_same_faction_rate;

        powerManager.percent_all_health += puppet.percent_all_health;
        powerManager.percent_all_physical_attack += puppet.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += puppet.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += puppet.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += puppet.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += puppet.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += puppet.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += puppet.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += puppet.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += puppet.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += puppet.percent_all_mental_defense;

        IUserPuppetRepository userPuppetRepository = new UserPuppetRepository();
        UserPuppetService userPuppetService = new UserPuppetService(userPuppetRepository);
        // User
        puppet = userPuppetService.SumPowerUserPuppet(); // Giả định SumPowerUserPuppet cũng trả về một đối tượng Puppet mới hoặc đã được reset
        powerManager.power += puppet.power;
        powerManager.health += puppet.health;
        powerManager.physical_attack += puppet.physical_attack;
        powerManager.physical_defense += puppet.physical_defense;
        powerManager.magical_attack += puppet.magical_attack;
        powerManager.magical_defense += puppet.magical_defense;
        powerManager.chemical_attack += puppet.chemical_attack;
        powerManager.chemical_defense += puppet.chemical_defense;
        powerManager.atomic_attack += puppet.atomic_attack;
        powerManager.atomic_defense += puppet.atomic_defense;
        powerManager.mental_attack += puppet.mental_attack;
        powerManager.mental_defense += puppet.mental_defense;
        powerManager.speed += puppet.speed;
        powerManager.critical_damage_rate += puppet.critical_damage_rate;
        powerManager.critical_rate += puppet.critical_rate;
        powerManager.penetration_rate += puppet.penetration_rate;
        powerManager.evasion_rate += puppet.evasion_rate;
        powerManager.damage_absorption_rate += puppet.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += puppet.vitality_regeneration_rate;
        powerManager.accuracy_rate += puppet.accuracy_rate;
        powerManager.lifesteal_rate += puppet.lifesteal_rate;
        powerManager.shield_strength += puppet.shield_strength;
        powerManager.tenacity += puppet.tenacity;
        powerManager.resistance_rate += puppet.resistance_rate;
        powerManager.combo_rate += puppet.combo_rate;
        powerManager.reflection_rate += puppet.reflection_rate;
        powerManager.mana += puppet.mana;
        powerManager.mana_regeneration_rate += puppet.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += puppet.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += puppet.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += puppet.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += puppet.resistance_to_same_faction_rate;

        IPuppetRepository puppetRepository = new PuppetRepository();
        PuppetService puppetService = new PuppetService(puppetRepository);
        // Percent
        puppet = puppetService.SumPowerPuppetPercent(); // Giả định SumPowerPuppetPercent cũng trả về một đối tượng Puppet mới hoặc đã được reset
        powerManager.percent_all_health += puppet.percent_all_health;
        powerManager.percent_all_physical_attack += puppet.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += puppet.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += puppet.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += puppet.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += puppet.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += puppet.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += puppet.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += puppet.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += puppet.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += puppet.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetAlchemyPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Alchemy alchemy = new Alchemy();
        IAlchemyGalleryRepository alchemyGalleryRepository = new AlchemyGalleryRepository();
        AlchemyGalleryService alchemyGalleryService = new AlchemyGalleryService(alchemyGalleryRepository);
        // Gallery
        Alchemy alchemy = alchemyGalleryService.SumPowerAlchemyGallery();
        powerManager.power += alchemy.power;
        powerManager.health += alchemy.health;
        powerManager.physical_attack += alchemy.physical_attack;
        powerManager.physical_defense += alchemy.physical_defense;
        powerManager.magical_attack += alchemy.magical_attack;
        powerManager.magical_defense += alchemy.magical_defense;
        powerManager.chemical_attack += alchemy.chemical_attack;
        powerManager.chemical_defense += alchemy.chemical_defense;
        powerManager.atomic_attack += alchemy.atomic_attack;
        powerManager.atomic_defense += alchemy.atomic_defense;
        powerManager.mental_attack += alchemy.mental_attack;
        powerManager.mental_defense += alchemy.mental_defense;
        powerManager.speed += alchemy.speed;
        powerManager.critical_damage_rate += alchemy.critical_damage_rate;
        powerManager.critical_rate += alchemy.critical_rate;
        powerManager.penetration_rate += alchemy.penetration_rate;
        powerManager.evasion_rate += alchemy.evasion_rate;
        powerManager.damage_absorption_rate += alchemy.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += alchemy.vitality_regeneration_rate;
        powerManager.accuracy_rate += alchemy.accuracy_rate;
        powerManager.lifesteal_rate += alchemy.lifesteal_rate;
        powerManager.shield_strength += alchemy.shield_strength;
        powerManager.tenacity += alchemy.tenacity;
        powerManager.resistance_rate += alchemy.resistance_rate;
        powerManager.combo_rate += alchemy.combo_rate;
        powerManager.reflection_rate += alchemy.reflection_rate;
        powerManager.mana += alchemy.mana;
        powerManager.mana_regeneration_rate += alchemy.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += alchemy.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += alchemy.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += alchemy.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += alchemy.resistance_to_same_faction_rate;

        powerManager.percent_all_health += alchemy.percent_all_health;
        powerManager.percent_all_physical_attack += alchemy.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += alchemy.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += alchemy.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += alchemy.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += alchemy.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += alchemy.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += alchemy.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += alchemy.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += alchemy.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += alchemy.percent_all_mental_defense;

        IUserAlchemyRepository userAlchemyRepository = new UserAlchemyRepository();
        UserAlchemyService userAlchemyService = new UserAlchemyService(userAlchemyRepository);
        // User
        alchemy = userAlchemyService.SumPowerUserAlchemy(); // Giả định SumPowerUserAlchemy cũng trả về một đối tượng Alchemy mới hoặc đã được reset
        powerManager.power += alchemy.power;
        powerManager.health += alchemy.health;
        powerManager.physical_attack += alchemy.physical_attack;
        powerManager.physical_defense += alchemy.physical_defense;
        powerManager.magical_attack += alchemy.magical_attack;
        powerManager.magical_defense += alchemy.magical_defense;
        powerManager.chemical_attack += alchemy.chemical_attack;
        powerManager.chemical_defense += alchemy.chemical_defense;
        powerManager.atomic_attack += alchemy.atomic_attack;
        powerManager.atomic_defense += alchemy.atomic_defense;
        powerManager.mental_attack += alchemy.mental_attack;
        powerManager.mental_defense += alchemy.mental_defense;
        powerManager.speed += alchemy.speed;
        powerManager.critical_damage_rate += alchemy.critical_damage_rate;
        powerManager.critical_rate += alchemy.critical_rate;
        powerManager.penetration_rate += alchemy.penetration_rate;
        powerManager.evasion_rate += alchemy.evasion_rate;
        powerManager.damage_absorption_rate += alchemy.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += alchemy.vitality_regeneration_rate;
        powerManager.accuracy_rate += alchemy.accuracy_rate;
        powerManager.lifesteal_rate += alchemy.lifesteal_rate;
        powerManager.shield_strength += alchemy.shield_strength;
        powerManager.tenacity += alchemy.tenacity;
        powerManager.resistance_rate += alchemy.resistance_rate;
        powerManager.combo_rate += alchemy.combo_rate;
        powerManager.reflection_rate += alchemy.reflection_rate;
        powerManager.mana += alchemy.mana;
        powerManager.mana_regeneration_rate += alchemy.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += alchemy.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += alchemy.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += alchemy.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += alchemy.resistance_to_same_faction_rate;

        IAlchemyRepository alchemyRepository = new AlchemyRepository();
        AlchemyService alchemyService = new AlchemyService(alchemyRepository);
        // Percent
        alchemy = alchemyService.SumPowerAlchemyPercent(); // Giả định SumPowerAlchemyPercent cũng trả về một đối tượng Alchemy mới hoặc đã được reset
        powerManager.percent_all_health += alchemy.percent_all_health;
        powerManager.percent_all_physical_attack += alchemy.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += alchemy.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += alchemy.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += alchemy.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += alchemy.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += alchemy.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += alchemy.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += alchemy.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += alchemy.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += alchemy.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetForgePower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Forge forge = new Forge();
        IForgeGalleryRepository forgeGalleryRepository = new ForgeGalleryRepository();
        ForgeGalleryService forgeGalleryService = new ForgeGalleryService(forgeGalleryRepository);
        // Gallery
        Forge forge = forgeGalleryService.SumPowerForgeGallery();
        powerManager.power += forge.power;
        powerManager.health += forge.health;
        powerManager.physical_attack += forge.physical_attack;
        powerManager.physical_defense += forge.physical_defense;
        powerManager.magical_attack += forge.magical_attack;
        powerManager.magical_defense += forge.magical_defense;
        powerManager.chemical_attack += forge.chemical_attack;
        powerManager.chemical_defense += forge.chemical_defense;
        powerManager.atomic_attack += forge.atomic_attack;
        powerManager.atomic_defense += forge.atomic_defense;
        powerManager.mental_attack += forge.mental_attack;
        powerManager.mental_defense += forge.mental_defense;
        powerManager.speed += forge.speed;
        powerManager.critical_damage_rate += forge.critical_damage_rate;
        powerManager.critical_rate += forge.critical_rate;
        powerManager.penetration_rate += forge.penetration_rate;
        powerManager.evasion_rate += forge.evasion_rate;
        powerManager.damage_absorption_rate += forge.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += forge.vitality_regeneration_rate;
        powerManager.accuracy_rate += forge.accuracy_rate;
        powerManager.lifesteal_rate += forge.lifesteal_rate;
        powerManager.shield_strength += forge.shield_strength;
        powerManager.tenacity += forge.tenacity;
        powerManager.resistance_rate += forge.resistance_rate;
        powerManager.combo_rate += forge.combo_rate;
        powerManager.reflection_rate += forge.reflection_rate;
        powerManager.mana += forge.mana;
        powerManager.mana_regeneration_rate += forge.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += forge.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += forge.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += forge.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += forge.resistance_to_same_faction_rate;

        powerManager.percent_all_health += forge.percent_all_health;
        powerManager.percent_all_physical_attack += forge.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += forge.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += forge.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += forge.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += forge.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += forge.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += forge.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += forge.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += forge.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += forge.percent_all_mental_defense;

        IUserForgeRepository userForgeRepository = new UserForgeRepository();
        UserForgeService userForgeService = new UserForgeService(userForgeRepository);
        // User
        forge = userForgeService.SumPowerUserForge(); // Giả định SumPowerUserForge cũng trả về một đối tượng Forge mới hoặc đã được reset
        powerManager.power += forge.power;
        powerManager.health += forge.health;
        powerManager.physical_attack += forge.physical_attack;
        powerManager.physical_defense += forge.physical_defense;
        powerManager.magical_attack += forge.magical_attack;
        powerManager.magical_defense += forge.magical_defense;
        powerManager.chemical_attack += forge.chemical_attack;
        powerManager.chemical_defense += forge.chemical_defense;
        powerManager.atomic_attack += forge.atomic_attack;
        powerManager.atomic_defense += forge.atomic_defense;
        powerManager.mental_attack += forge.mental_attack;
        powerManager.mental_defense += forge.mental_defense;
        powerManager.speed += forge.speed;
        powerManager.critical_damage_rate += forge.critical_damage_rate;
        powerManager.critical_rate += forge.critical_rate;
        powerManager.penetration_rate += forge.penetration_rate;
        powerManager.evasion_rate += forge.evasion_rate;
        powerManager.damage_absorption_rate += forge.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += forge.vitality_regeneration_rate;
        powerManager.accuracy_rate += forge.accuracy_rate;
        powerManager.lifesteal_rate += forge.lifesteal_rate;
        powerManager.shield_strength += forge.shield_strength;
        powerManager.tenacity += forge.tenacity;
        powerManager.resistance_rate += forge.resistance_rate;
        powerManager.combo_rate += forge.combo_rate;
        powerManager.reflection_rate += forge.reflection_rate;
        powerManager.mana += forge.mana;
        powerManager.mana_regeneration_rate += forge.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += forge.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += forge.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += forge.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += forge.resistance_to_same_faction_rate;

        IForgeRepository forgeRepository = new ForgeRepository();
        ForgeService forgeService = new ForgeService(forgeRepository);
        // Percent
        forge = forgeService.SumPowerForgePercent(); // Giả định SumPowerForgePercent cũng trả về một đối tượng Forge mới hoặc đã được reset
        powerManager.percent_all_health += forge.percent_all_health;
        powerManager.percent_all_physical_attack += forge.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += forge.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += forge.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += forge.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += forge.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += forge.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += forge.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += forge.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += forge.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += forge.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetCardLifePower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // CardLife cardLife = new CardLife();
        ICardLifeGalleryRepository cardLifeGalleryRepository = new CardLifeGalleryRepository();
        CardLifeGalleryService cardLifeGalleryService = new CardLifeGalleryService(cardLifeGalleryRepository);
        // Gallery
        CardLife cardLife = cardLifeGalleryService.SumPowerCardLifeGallery();
        powerManager.power += cardLife.power;
        powerManager.health += cardLife.health;
        powerManager.physical_attack += cardLife.physical_attack;
        powerManager.physical_defense += cardLife.physical_defense;
        powerManager.magical_attack += cardLife.magical_attack;
        powerManager.magical_defense += cardLife.magical_defense;
        powerManager.chemical_attack += cardLife.chemical_attack;
        powerManager.chemical_defense += cardLife.chemical_defense;
        powerManager.atomic_attack += cardLife.atomic_attack;
        powerManager.atomic_defense += cardLife.atomic_defense;
        powerManager.mental_attack += cardLife.mental_attack;
        powerManager.mental_defense += cardLife.mental_defense;
        powerManager.speed += cardLife.speed;
        powerManager.critical_damage_rate += cardLife.critical_damage_rate;
        powerManager.critical_rate += cardLife.critical_rate;
        powerManager.penetration_rate += cardLife.penetration_rate;
        powerManager.evasion_rate += cardLife.evasion_rate;
        powerManager.damage_absorption_rate += cardLife.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardLife.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardLife.accuracy_rate;
        powerManager.lifesteal_rate += cardLife.lifesteal_rate;
        powerManager.shield_strength += cardLife.shield_strength;
        powerManager.tenacity += cardLife.tenacity;
        powerManager.resistance_rate += cardLife.resistance_rate;
        powerManager.combo_rate += cardLife.combo_rate;
        powerManager.reflection_rate += cardLife.reflection_rate;
        powerManager.mana += cardLife.mana;
        powerManager.mana_regeneration_rate += cardLife.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardLife.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardLife.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardLife.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardLife.resistance_to_same_faction_rate;

        powerManager.percent_all_health += cardLife.percent_all_health;
        powerManager.percent_all_physical_attack += cardLife.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardLife.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardLife.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardLife.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardLife.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardLife.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardLife.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardLife.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardLife.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardLife.percent_all_mental_defense;

        IUserCardLifeRepository userCardLifeRepository = new UserCardLifeRepository();
        UserCardLifeService userCardLifeService = new UserCardLifeService(userCardLifeRepository);
        // User
        cardLife = userCardLifeService.SumPowerUserCardLife(); // Giả định SumPowerUserCardLife cũng trả về một đối tượng CardLife mới hoặc đã được reset
        powerManager.power += cardLife.power;
        powerManager.health += cardLife.health;
        powerManager.physical_attack += cardLife.physical_attack;
        powerManager.physical_defense += cardLife.physical_defense;
        powerManager.magical_attack += cardLife.magical_attack;
        powerManager.magical_defense += cardLife.magical_defense;
        powerManager.chemical_attack += cardLife.chemical_attack;
        powerManager.chemical_defense += cardLife.chemical_defense;
        powerManager.atomic_attack += cardLife.atomic_attack;
        powerManager.atomic_defense += cardLife.atomic_defense;
        powerManager.mental_attack += cardLife.mental_attack;
        powerManager.mental_defense += cardLife.mental_defense;
        powerManager.speed += cardLife.speed;
        powerManager.critical_damage_rate += cardLife.critical_damage_rate;
        powerManager.critical_rate += cardLife.critical_rate;
        powerManager.penetration_rate += cardLife.penetration_rate;
        powerManager.evasion_rate += cardLife.evasion_rate;
        powerManager.damage_absorption_rate += cardLife.damage_absorption_rate;
        powerManager.vitality_regeneration_rate += cardLife.vitality_regeneration_rate;
        powerManager.accuracy_rate += cardLife.accuracy_rate;
        powerManager.lifesteal_rate += cardLife.lifesteal_rate;
        powerManager.shield_strength += cardLife.shield_strength;
        powerManager.tenacity += cardLife.tenacity;
        powerManager.resistance_rate += cardLife.resistance_rate;
        powerManager.combo_rate += cardLife.combo_rate;
        powerManager.reflection_rate += cardLife.reflection_rate;
        powerManager.mana += cardLife.mana;
        powerManager.mana_regeneration_rate += cardLife.mana_regeneration_rate;
        powerManager.damage_to_different_faction_rate += cardLife.damage_to_different_faction_rate;
        powerManager.resistance_to_different_faction_rate += cardLife.resistance_to_different_faction_rate;
        powerManager.damage_to_same_faction_rate += cardLife.damage_to_same_faction_rate;
        powerManager.resistance_to_same_faction_rate += cardLife.resistance_to_same_faction_rate;

        ICardLifeRepository cardLifeRepository = new CardLifeRepository();
        CardLifeService cardLifeService = new CardLifeService(cardLifeRepository);
        // Percent
        cardLife = cardLifeService.SumPowerCardLifePercent(); // Giả định SumPowerCardLifePercent cũng trả về một đối tượng CardLife mới hoặc đã được reset
        powerManager.percent_all_health += cardLife.percent_all_health;
        powerManager.percent_all_physical_attack += cardLife.percent_all_physical_attack;
        powerManager.percent_all_physical_defense += cardLife.percent_all_physical_defense;
        powerManager.percent_all_magical_attack += cardLife.percent_all_magical_attack;
        powerManager.percent_all_magical_defense += cardLife.percent_all_magical_defense;
        powerManager.percent_all_chemical_attack += cardLife.percent_all_chemical_attack;
        powerManager.percent_all_chemical_defense += cardLife.percent_all_chemical_defense;
        powerManager.percent_all_atomic_attack += cardLife.percent_all_atomic_attack;
        powerManager.percent_all_atomic_defense += cardLife.percent_all_atomic_defense;
        powerManager.percent_all_mental_attack += cardLife.percent_all_mental_attack;
        powerManager.percent_all_mental_defense += cardLife.percent_all_mental_defense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }


}