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

        // Lấy sức mạnh từ Artwork
        PowerManager ArtworkPower = GetArtworkPower();
        AddPower(totalPower, ArtworkPower);

        // Lấy sức mạnh từ Spirit Beast
        PowerManager SpiritBeastPower = GetSpiritBeastPower();
        AddPower(totalPower, SpiritBeastPower);

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

        target.PercentAllHealth += source.PercentAllHealth;
        target.PercentAllPhysicalAttack += source.PercentAllPhysicalAttack;
        target.PercentAllPhysicalDefense += source.PercentAllPhysicalDefense;
        target.PercentAllMagicalAttack += source.PercentAllMagicalAttack;
        target.PercentAllMagicalDefense += source.PercentAllMagicalDefense;
        target.PercentAllChemicalAttack += source.PercentAllChemicalAttack;
        target.PercentAllChemicalDefense += source.PercentAllChemicalDefense;
        target.PercentAllAtomicAttack += source.PercentAllAtomicAttack;
        target.PercentAllAtomicDefense += source.PercentAllAtomicDefense;
        target.PercentAllMentalAttack += source.PercentAllMentalAttack;
        target.PercentAllMentalDefense += source.PercentAllMentalDefense;
    }
    public PowerManager GetAchievementsPower()
    {
        PowerManager powerManager = new PowerManager();

        // User Achievements
        IUserAchievementsRepository userAchievementsRepository = new UserAchievementsRepository();
        UserAchievementsService userAchievementsService = new UserAchievementsService(userAchievementsRepository);
        Achievements userAchievements = userAchievementsService.SumPowerUserAchievements();

        powerManager.power += userAchievements.Power;
        powerManager.health += userAchievements.Health;
        powerManager.physical_attack += userAchievements.PhysicalAttack;
        powerManager.physical_defense += userAchievements.PhysicalDefense;
        powerManager.magical_attack += userAchievements.MagicalAttack;
        powerManager.magical_defense += userAchievements.MagicalDefense;
        powerManager.chemical_attack += userAchievements.ChemicalAttack;
        powerManager.chemical_defense += userAchievements.ChemicalDefense;
        powerManager.atomic_attack += userAchievements.AtomicAttack;
        powerManager.atomic_defense += userAchievements.AtomicDefense;
        powerManager.mental_attack += userAchievements.MentalAttack;
        powerManager.mental_defense += userAchievements.MentalDefense;
        powerManager.speed += userAchievements.Speed;
        powerManager.critical_damage_rate += userAchievements.CriticalDamageRate;
        powerManager.critical_rate += userAchievements.CriticalRate;
        powerManager.penetration_rate += userAchievements.PenetrationRate;
        powerManager.evasion_rate += userAchievements.EvasionRate;
        powerManager.damage_absorption_rate += userAchievements.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += userAchievements.VitalityRegenerationRate;
        powerManager.accuracy_rate += userAchievements.AccuracyRate;
        powerManager.lifesteal_rate += userAchievements.LifestealRate;
        powerManager.shield_strength += userAchievements.ShieldStrength;
        powerManager.tenacity += userAchievements.Tenacity;
        powerManager.resistance_rate += userAchievements.ResistanceRate;
        powerManager.combo_rate += userAchievements.ComboRate;
        powerManager.reflection_rate += userAchievements.ReflectionRate;
        powerManager.mana += userAchievements.Mana;
        powerManager.mana_regeneration_rate += userAchievements.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += userAchievements.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += userAchievements.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += userAchievements.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += userAchievements.ResistanceToSameFactionRate;

        // Percent Achievements
        IAchievementsRepository achievementsRepository = new AchievementsRepository();
        AchievementsService achievementsService = new AchievementsService(achievementsRepository);
        Achievements percentAchievements = achievementsService.SumPowerAchievementsPercent();

        powerManager.PercentAllHealth += percentAchievements.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += percentAchievements.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += percentAchievements.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += percentAchievements.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += percentAchievements.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += percentAchievements.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += percentAchievements.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += percentAchievements.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += percentAchievements.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += percentAchievements.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += percentAchievements.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetAvatarsPower()
    {
        PowerManager powerManager = new PowerManager();

        // Gallery Avatars
        IAvatarsGalleryRepository avatarsGalleryRepository = new AvatarsGalleryRepository();
        AvatarsGalleryService avatarsGalleryService = new AvatarsGalleryService(avatarsGalleryRepository);
        Avatars galleryAvatars = avatarsGalleryService.SumPowerAvatarsGallery();

        powerManager.power += galleryAvatars.Power;
        powerManager.health += galleryAvatars.Health;
        powerManager.physical_attack += galleryAvatars.PhysicalAttack;
        powerManager.physical_defense += galleryAvatars.PhysicalDefense;
        powerManager.magical_attack += galleryAvatars.MagicalAttack;
        powerManager.magical_defense += galleryAvatars.MagicalDefense;
        powerManager.chemical_attack += galleryAvatars.ChemicalAttack;
        powerManager.chemical_defense += galleryAvatars.ChemicalDefense;
        powerManager.atomic_attack += galleryAvatars.AtomicAttack;
        powerManager.atomic_defense += galleryAvatars.AtomicDefense;
        powerManager.mental_attack += galleryAvatars.MentalAttack;
        powerManager.mental_defense += galleryAvatars.MentalDefense;
        powerManager.speed += galleryAvatars.Speed;
        powerManager.critical_damage_rate += galleryAvatars.CriticalDamageRate;
        powerManager.critical_rate += galleryAvatars.CriticalRate;
        powerManager.penetration_rate += galleryAvatars.PenetrationRate;
        powerManager.evasion_rate += galleryAvatars.EvasionRate;
        powerManager.damage_absorption_rate += galleryAvatars.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += galleryAvatars.VitalityRegenerationRate;
        powerManager.accuracy_rate += galleryAvatars.AccuracyRate;
        powerManager.lifesteal_rate += galleryAvatars.LifestealRate;
        powerManager.shield_strength += galleryAvatars.ShieldStrength;
        powerManager.tenacity += galleryAvatars.Tenacity;
        powerManager.resistance_rate += galleryAvatars.ResistanceRate;
        powerManager.combo_rate += galleryAvatars.ComboRate;
        powerManager.reflection_rate += galleryAvatars.ReflectionRate;
        powerManager.mana += galleryAvatars.Mana;
        powerManager.mana_regeneration_rate += galleryAvatars.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += galleryAvatars.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += galleryAvatars.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += galleryAvatars.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += galleryAvatars.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += galleryAvatars.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += galleryAvatars.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += galleryAvatars.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += galleryAvatars.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += galleryAvatars.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += galleryAvatars.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += galleryAvatars.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += galleryAvatars.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += galleryAvatars.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += galleryAvatars.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += galleryAvatars.PercentAllMentalDefense;

        // User Avatars
        IUserAvatarsRepository userAvatarsRepository = new UserAvatarsRepository();
        UserAvatarsService userAvatarsService = new UserAvatarsService(userAvatarsRepository);
        Avatars userAvatars = userAvatarsService.SumPowerUserAvatars();

        powerManager.power += userAvatars.Power;
        powerManager.health += userAvatars.Health;
        powerManager.physical_attack += userAvatars.PhysicalAttack;
        powerManager.physical_defense += userAvatars.PhysicalDefense;
        powerManager.magical_attack += userAvatars.MagicalAttack;
        powerManager.magical_defense += userAvatars.MagicalDefense;
        powerManager.chemical_attack += userAvatars.ChemicalAttack;
        powerManager.chemical_defense += userAvatars.ChemicalDefense;
        powerManager.atomic_attack += userAvatars.AtomicAttack;
        powerManager.atomic_defense += userAvatars.AtomicDefense;
        powerManager.mental_attack += userAvatars.MentalAttack;
        powerManager.mental_defense += userAvatars.MentalDefense;
        powerManager.speed += userAvatars.Speed;
        powerManager.critical_damage_rate += userAvatars.CriticalDamageRate;
        powerManager.critical_rate += userAvatars.CriticalRate;
        powerManager.penetration_rate += userAvatars.PenetrationRate;
        powerManager.evasion_rate += userAvatars.EvasionRate;
        powerManager.damage_absorption_rate += userAvatars.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += userAvatars.VitalityRegenerationRate;
        powerManager.accuracy_rate += userAvatars.AccuracyRate;
        powerManager.lifesteal_rate += userAvatars.LifestealRate;
        powerManager.shield_strength += userAvatars.ShieldStrength;
        powerManager.tenacity += userAvatars.Tenacity;
        powerManager.resistance_rate += userAvatars.ResistanceRate;
        powerManager.combo_rate += userAvatars.ComboRate;
        powerManager.reflection_rate += userAvatars.ReflectionRate;
        powerManager.mana += userAvatars.Mana;
        powerManager.mana_regeneration_rate += userAvatars.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += userAvatars.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += userAvatars.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += userAvatars.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += userAvatars.ResistanceToSameFactionRate;

        // Percent Avatars
        IAvatarsRepository avatarsRepository = new AvatarsRepository();
        AvatarsService avatarsService = new AvatarsService(avatarsRepository);
        Avatars percentAvatars = avatarsService.SumPowerAvatarsPercent();

        powerManager.PercentAllHealth += percentAvatars.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += percentAvatars.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += percentAvatars.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += percentAvatars.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += percentAvatars.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += percentAvatars.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += percentAvatars.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += percentAvatars.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += percentAvatars.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += percentAvatars.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += percentAvatars.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetBordersPower()
    {
        PowerManager powerManager = new PowerManager();

        // Gallery Borders
        IBordersGalleryRepository bordersGalleryRepository = new BordersGalleryRepository(); // Note: This was named booksGalleryRepository in the original. Corrected for clarity.
        BordersGalleryService bordersGalleryService = new BordersGalleryService(bordersGalleryRepository); // Note: This was named booksGalleryService in the original. Corrected for clarity.
        Borders galleryBorders = bordersGalleryService.SumPowerBordersGallery();

        powerManager.power += galleryBorders.Power;
        powerManager.health += galleryBorders.Health;
        powerManager.physical_attack += galleryBorders.PhysicalAttack;
        powerManager.physical_defense += galleryBorders.PhysicalDefense;
        powerManager.magical_attack += galleryBorders.MagicalAttack;
        powerManager.magical_defense += galleryBorders.MagicalDefense;
        powerManager.chemical_attack += galleryBorders.ChemicalAttack;
        powerManager.chemical_defense += galleryBorders.ChemicalDefense;
        powerManager.atomic_attack += galleryBorders.AtomicAttack;
        powerManager.atomic_defense += galleryBorders.AtomicDefense;
        powerManager.mental_attack += galleryBorders.MentalAttack;
        powerManager.mental_defense += galleryBorders.MentalDefense;
        powerManager.speed += galleryBorders.Speed;
        powerManager.critical_damage_rate += galleryBorders.CriticalDamageRate;
        powerManager.critical_rate += galleryBorders.CriticalRate;
        powerManager.penetration_rate += galleryBorders.PenetrationRate;
        powerManager.evasion_rate += galleryBorders.EvasionRate;
        powerManager.damage_absorption_rate += galleryBorders.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += galleryBorders.VitalityRegenerationRate;
        powerManager.accuracy_rate += galleryBorders.AccuracyRate;
        powerManager.lifesteal_rate += galleryBorders.LifestealRate;
        powerManager.shield_strength += galleryBorders.ShieldStrength;
        powerManager.tenacity += galleryBorders.Tenacity;
        powerManager.resistance_rate += galleryBorders.ResistanceRate;
        powerManager.combo_rate += galleryBorders.ComboRate;
        powerManager.reflection_rate += galleryBorders.ReflectionRate;
        powerManager.mana += galleryBorders.Mana;
        powerManager.mana_regeneration_rate += galleryBorders.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += galleryBorders.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += galleryBorders.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += galleryBorders.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += galleryBorders.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += galleryBorders.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += galleryBorders.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += galleryBorders.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += galleryBorders.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += galleryBorders.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += galleryBorders.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += galleryBorders.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += galleryBorders.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += galleryBorders.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += galleryBorders.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += galleryBorders.PercentAllMentalDefense;

        // User Borders
        IUserBordersRepository userBordersRepository = new UserBordersRepository();
        UserBordersService userBordersService = new UserBordersService(userBordersRepository);
        Borders userBorders = userBordersService.SumPowerUserBorders();

        powerManager.power += userBorders.Power;
        powerManager.health += userBorders.Health;
        powerManager.physical_attack += userBorders.PhysicalAttack;
        powerManager.physical_defense += userBorders.PhysicalDefense;
        powerManager.magical_attack += userBorders.MagicalAttack;
        powerManager.magical_defense += userBorders.MagicalDefense;
        powerManager.chemical_attack += userBorders.ChemicalAttack;
        powerManager.chemical_defense += userBorders.ChemicalDefense;
        powerManager.atomic_attack += userBorders.AtomicAttack;
        powerManager.atomic_defense += userBorders.AtomicDefense;
        powerManager.mental_attack += userBorders.MentalAttack;
        powerManager.mental_defense += userBorders.MentalDefense;
        powerManager.speed += userBorders.Speed;
        powerManager.critical_damage_rate += userBorders.CriticalDamageRate;
        powerManager.critical_rate += userBorders.CriticalRate;
        powerManager.penetration_rate += userBorders.PenetrationRate;
        powerManager.evasion_rate += userBorders.EvasionRate;
        powerManager.damage_absorption_rate += userBorders.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += userBorders.VitalityRegenerationRate;
        powerManager.accuracy_rate += userBorders.AccuracyRate;
        powerManager.lifesteal_rate += userBorders.LifestealRate;
        powerManager.shield_strength += userBorders.ShieldStrength;
        powerManager.tenacity += userBorders.Tenacity;
        powerManager.resistance_rate += userBorders.ResistanceRate;
        powerManager.combo_rate += userBorders.ComboRate;
        powerManager.reflection_rate += userBorders.ReflectionRate;
        powerManager.mana += userBorders.Mana;
        powerManager.mana_regeneration_rate += userBorders.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += userBorders.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += userBorders.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += userBorders.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += userBorders.ResistanceToSameFactionRate;

        // Percent Borders
        IBordersRepository bordersRepository = new BordersRepository();
        BordersService bordersService = new BordersService(bordersRepository);
        Borders percentBorders = bordersService.SumPowerBordersPercent();

        powerManager.PercentAllHealth += percentBorders.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += percentBorders.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += percentBorders.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += percentBorders.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += percentBorders.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += percentBorders.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += percentBorders.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += percentBorders.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += percentBorders.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += percentBorders.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += percentBorders.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetBooksPower()
    {
        PowerManager powerManager = new PowerManager();

        IBooksGalleryRepository booksGalleryRepository = new BooksGalleryRepository();
        BooksGalleryService booksGalleryService = new BooksGalleryService(booksGalleryRepository);

        //Gallery
        Books books = booksGalleryService.SumPowerBooksGallery();

        powerManager.power += books.Power;
        powerManager.health += books.Health;
        powerManager.physical_attack += books.PhysicalAttack;
        powerManager.physical_defense += books.PhysicalDefense;
        powerManager.magical_attack += books.MagicalAttack;
        powerManager.magical_defense += books.MagicalDefense;
        powerManager.chemical_attack += books.ChemicalAttack;
        powerManager.chemical_defense += books.ChemicalDefense;
        powerManager.atomic_attack += books.AtomicAttack;
        powerManager.atomic_defense += books.AtomicDefense;
        powerManager.mental_attack += books.MentalAttack;
        powerManager.mental_defense += books.MentalDefense;
        powerManager.speed += books.Speed;
        powerManager.critical_damage_rate += books.CriticalDamageRate;
        powerManager.critical_rate += books.CriticalRate;
        powerManager.penetration_rate += books.PenetrationRate;
        powerManager.evasion_rate += books.EvasionRate;
        powerManager.damage_absorption_rate += books.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += books.VitalityRegenerationRate;
        powerManager.accuracy_rate += books.AccuracyRate;
        powerManager.lifesteal_rate += books.LifestealRate;
        powerManager.shield_strength += books.ShieldStrength;
        powerManager.tenacity += books.Tenacity;
        powerManager.resistance_rate += books.ResistanceRate;
        powerManager.combo_rate += books.ComboRate;
        powerManager.reflection_rate += books.ReflectionRate;
        powerManager.mana += books.Mana;
        powerManager.mana_regeneration_rate += books.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += books.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += books.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += books.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += books.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += books.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += books.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += books.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += books.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += books.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += books.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += books.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += books.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += books.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += books.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += books.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardHeroesPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardHeroesGalleryRepository cardHeroesGalleryRepository = new CardHeroesGalleryRepository();
        CardHeroesGalleryService cardHeroesGalleryService = new CardHeroesGalleryService(cardHeroesGalleryRepository);

        // Gallery
        CardHeroes cardHeroes = cardHeroesGalleryService.SumPowerCardHeroesGallery();

        powerManager.power += cardHeroes.Power;
        powerManager.health += cardHeroes.Health;
        powerManager.physical_attack += cardHeroes.PhysicalAttack;
        powerManager.physical_defense += cardHeroes.PhysicalDefense;
        powerManager.magical_attack += cardHeroes.MagicalAttack;
        powerManager.magical_defense += cardHeroes.MagicalDefense;
        powerManager.chemical_attack += cardHeroes.ChemicalAttack;
        powerManager.chemical_defense += cardHeroes.ChemicalDefense;
        powerManager.atomic_attack += cardHeroes.AtomicAttack;
        powerManager.atomic_defense += cardHeroes.AtomicDefense;
        powerManager.mental_attack += cardHeroes.MentalAttack;
        powerManager.mental_defense += cardHeroes.MentalDefense;
        powerManager.speed += cardHeroes.Speed;
        powerManager.critical_damage_rate += cardHeroes.CriticalDamageRate;
        powerManager.critical_rate += cardHeroes.CriticalRate;
        powerManager.penetration_rate += cardHeroes.PenetrationRate;
        powerManager.evasion_rate += cardHeroes.EvasionRate;
        powerManager.damage_absorption_rate += cardHeroes.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardHeroes.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardHeroes.AccuracyRate;
        powerManager.lifesteal_rate += cardHeroes.LifestealRate;
        powerManager.shield_strength += cardHeroes.ShieldStrength;
        powerManager.tenacity += cardHeroes.Tenacity;
        powerManager.resistance_rate += cardHeroes.ResistanceRate;
        powerManager.combo_rate += cardHeroes.ComboRate;
        powerManager.reflection_rate += cardHeroes.ReflectionRate;
        powerManager.mana += cardHeroes.Mana;
        powerManager.mana_regeneration_rate += cardHeroes.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardHeroes.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardHeroes.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardHeroes.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardHeroes.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardHeroes.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardHeroes.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardHeroes.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardHeroes.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardHeroes.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardHeroes.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardHeroes.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardHeroes.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardHeroes.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardHeroes.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardHeroes.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardCaptainsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardCaptainsGalleryRepository cardCaptainsGalleryRepository = new CardCaptainsGalleryRepository();
        CardCaptainsGalleryService cardCaptainsGalleryService = new CardCaptainsGalleryService(cardCaptainsGalleryRepository);

        // Gallery
        CardCaptains cardCaptains = cardCaptainsGalleryService.SumPowerCardCaptainsGallery();

        powerManager.power += cardCaptains.Power;
        powerManager.health += cardCaptains.Health;
        powerManager.physical_attack += cardCaptains.PhysicalAttack;
        powerManager.physical_defense += cardCaptains.PhysicalDefense;
        powerManager.magical_attack += cardCaptains.MagicalAttack;
        powerManager.magical_defense += cardCaptains.MagicalDefense;
        powerManager.chemical_attack += cardCaptains.ChemicalAttack;
        powerManager.chemical_defense += cardCaptains.ChemicalDefense;
        powerManager.atomic_attack += cardCaptains.AtomicAttack;
        powerManager.atomic_defense += cardCaptains.AtomicDefense;
        powerManager.mental_attack += cardCaptains.MentalAttack;
        powerManager.mental_defense += cardCaptains.MentalDefense;
        powerManager.speed += cardCaptains.Speed;
        powerManager.critical_damage_rate += cardCaptains.CriticalDamageRate;
        powerManager.critical_rate += cardCaptains.CriticalRate;
        powerManager.penetration_rate += cardCaptains.PenetrationRate;
        powerManager.evasion_rate += cardCaptains.EvasionRate;
        powerManager.damage_absorption_rate += cardCaptains.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardCaptains.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardCaptains.AccuracyRate;
        powerManager.lifesteal_rate += cardCaptains.LifestealRate;
        powerManager.shield_strength += cardCaptains.ShieldStrength;
        powerManager.tenacity += cardCaptains.Tenacity;
        powerManager.resistance_rate += cardCaptains.ResistanceRate;
        powerManager.combo_rate += cardCaptains.ComboRate;
        powerManager.reflection_rate += cardCaptains.ReflectionRate;
        powerManager.mana += cardCaptains.Mana;
        powerManager.mana_regeneration_rate += cardCaptains.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardCaptains.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardCaptains.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardCaptains.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardCaptains.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardCaptains.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardCaptains.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardCaptains.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardCaptains.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardCaptains.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardCaptains.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardCaptains.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardCaptains.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardCaptains.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardCaptains.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardCaptains.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardColonelsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardColonelsGalleryRepository cardColonelsGalleryRepository = new CardColonelsGalleryRepository();
        CardColonelsGalleryService cardColonelsGalleryService = new CardColonelsGalleryService(cardColonelsGalleryRepository);

        //Gallery
        CardColonels cardColonels = cardColonelsGalleryService.SumPowerCardColonelsGallery();

        powerManager.power += cardColonels.Power;
        powerManager.health += cardColonels.Health;
        powerManager.physical_attack += cardColonels.PhysicalAttack;
        powerManager.physical_defense += cardColonels.PhysicalDefense;
        powerManager.magical_attack += cardColonels.MagicalAttack;
        powerManager.magical_defense += cardColonels.MagicalDefense;
        powerManager.chemical_attack += cardColonels.ChemicalAttack;
        powerManager.chemical_defense += cardColonels.ChemicalDefense;
        powerManager.atomic_attack += cardColonels.AtomicAttack;
        powerManager.atomic_defense += cardColonels.AtomicDefense;
        powerManager.mental_attack += cardColonels.MentalAttack;
        powerManager.mental_defense += cardColonels.MentalDefense;
        powerManager.speed += cardColonels.Speed;
        powerManager.critical_damage_rate += cardColonels.CriticalDamageRate;
        powerManager.critical_rate += cardColonels.CriticalRate;
        powerManager.penetration_rate += cardColonels.PenetrationRate;
        powerManager.evasion_rate += cardColonels.EvasionRate;
        powerManager.damage_absorption_rate += cardColonels.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardColonels.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardColonels.AccuracyRate;
        powerManager.lifesteal_rate += cardColonels.LifestealRate;
        powerManager.shield_strength += cardColonels.ShieldStrength;
        powerManager.tenacity += cardColonels.Tenacity;
        powerManager.resistance_rate += cardColonels.ResistanceRate;
        powerManager.combo_rate += cardColonels.ComboRate;
        powerManager.reflection_rate += cardColonels.ReflectionRate;
        powerManager.mana += cardColonels.Mana;
        powerManager.mana_regeneration_rate += cardColonels.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardColonels.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardColonels.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardColonels.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardColonels.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardColonels.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardColonels.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardColonels.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardColonels.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardColonels.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardColonels.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardColonels.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardColonels.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardColonels.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardColonels.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardColonels.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardGeneralsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardGeneralsGalleryRepository cardGeneralsGalleryRepository = new CardGeneralsGalleryRepository();
        CardGeneralsGalleryService cardGeneralsGalleryService = new CardGeneralsGalleryService(cardGeneralsGalleryRepository);

        //Gallery
        CardGenerals cardGenerals = cardGeneralsGalleryService.SumPowerCardGeneralsGallery();

        powerManager.power += cardGenerals.Power;
        powerManager.health += cardGenerals.Health;
        powerManager.physical_attack += cardGenerals.PhysicalAttack;
        powerManager.physical_defense += cardGenerals.PhysicalDefense;
        powerManager.magical_attack += cardGenerals.MagicalAttack;
        powerManager.magical_defense += cardGenerals.MagicalDefense;
        powerManager.chemical_attack += cardGenerals.ChemicalAttack;
        powerManager.chemical_defense += cardGenerals.ChemicalDefense;
        powerManager.atomic_attack += cardGenerals.AtomicAttack;
        powerManager.atomic_defense += cardGenerals.AtomicDefense;
        powerManager.mental_attack += cardGenerals.MentalAttack;
        powerManager.mental_defense += cardGenerals.MentalDefense;
        powerManager.speed += cardGenerals.Speed;
        powerManager.critical_damage_rate += cardGenerals.CriticalDamageRate;
        powerManager.critical_rate += cardGenerals.CriticalRate;
        powerManager.penetration_rate += cardGenerals.PenetrationRate;
        powerManager.evasion_rate += cardGenerals.EvasionRate;
        powerManager.damage_absorption_rate += cardGenerals.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardGenerals.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardGenerals.AccuracyRate;
        powerManager.lifesteal_rate += cardGenerals.LifestealRate;
        powerManager.shield_strength += cardGenerals.ShieldStrength;
        powerManager.tenacity += cardGenerals.Tenacity;
        powerManager.resistance_rate += cardGenerals.ResistanceRate;
        powerManager.combo_rate += cardGenerals.ComboRate;
        powerManager.reflection_rate += cardGenerals.ReflectionRate;
        powerManager.mana += cardGenerals.Mana;
        powerManager.mana_regeneration_rate += cardGenerals.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardGenerals.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardGenerals.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardGenerals.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardGenerals.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardGenerals.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardGenerals.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardGenerals.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardGenerals.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardGenerals.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardGenerals.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardGenerals.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardGenerals.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardGenerals.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardGenerals.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardGenerals.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardAdmiralsPower()
    {
        PowerManager powerManager = new PowerManager();

        ICardAdmiralsGalleryRepository cardAdmiralsGalleryRepository = new CardAdmiralsGalleryRepository();
        CardAdmiralsGalleryService cardAdmiralsGalleryService = new CardAdmiralsGalleryService(cardAdmiralsGalleryRepository);

        //Gallery
        CardAdmirals cardAdmirals = cardAdmiralsGalleryService.SumPowerCardCaptainsGallery();

        powerManager.power += cardAdmirals.Power;
        powerManager.health += cardAdmirals.Health;
        powerManager.physical_attack += cardAdmirals.PhysicalAttack;
        powerManager.physical_defense += cardAdmirals.PhysicalDefense;
        powerManager.magical_attack += cardAdmirals.MagicalAttack;
        powerManager.magical_defense += cardAdmirals.MagicalDefense;
        powerManager.chemical_attack += cardAdmirals.ChemicalAttack;
        powerManager.chemical_defense += cardAdmirals.ChemicalDefense;
        powerManager.atomic_attack += cardAdmirals.AtomicAttack;
        powerManager.atomic_defense += cardAdmirals.AtomicDefense;
        powerManager.mental_attack += cardAdmirals.MentalAttack;
        powerManager.mental_defense += cardAdmirals.MentalDefense;
        powerManager.speed += cardAdmirals.Speed;
        powerManager.critical_damage_rate += cardAdmirals.CriticalDamageRate;
        powerManager.critical_rate += cardAdmirals.CriticalRate;
        powerManager.penetration_rate += cardAdmirals.PenetrationRate;
        powerManager.evasion_rate += cardAdmirals.EvasionRate;
        powerManager.damage_absorption_rate += cardAdmirals.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardAdmirals.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardAdmirals.AccuracyRate;
        powerManager.lifesteal_rate += cardAdmirals.LifestealRate;
        powerManager.shield_strength += cardAdmirals.ShieldStrength;
        powerManager.tenacity += cardAdmirals.Tenacity;
        powerManager.resistance_rate += cardAdmirals.ResistanceRate;
        powerManager.combo_rate += cardAdmirals.ComboRate;
        powerManager.reflection_rate += cardAdmirals.ReflectionRate;
        powerManager.mana += cardAdmirals.Mana;
        powerManager.mana_regeneration_rate += cardAdmirals.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardAdmirals.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardAdmirals.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardAdmirals.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardAdmirals.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardAdmirals.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardAdmirals.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardAdmirals.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardAdmirals.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardAdmirals.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardAdmirals.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardAdmirals.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardAdmirals.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardAdmirals.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardAdmirals.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardAdmirals.PercentAllMentalDefense;

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
        powerManager.power += cardMonsters.Power;
        powerManager.health += cardMonsters.Health;
        powerManager.physical_attack += cardMonsters.PhysicalAttack;
        powerManager.physical_defense += cardMonsters.PhysicalDefense;
        powerManager.magical_attack += cardMonsters.MagicalAttack;
        powerManager.magical_defense += cardMonsters.MagicalDefense;
        powerManager.chemical_attack += cardMonsters.ChemicalAttack;
        powerManager.chemical_defense += cardMonsters.ChemicalDefense;
        powerManager.atomic_attack += cardMonsters.AtomicAttack;
        powerManager.atomic_defense += cardMonsters.AtomicDefense;
        powerManager.mental_attack += cardMonsters.MentalAttack;
        powerManager.mental_defense += cardMonsters.MentalDefense;
        powerManager.speed += cardMonsters.Speed;
        powerManager.critical_damage_rate += cardMonsters.CriticalDamageRate;
        powerManager.critical_rate += cardMonsters.CriticalRate;
        powerManager.penetration_rate += cardMonsters.PenetrationRate;
        powerManager.evasion_rate += cardMonsters.EvasionRate;
        powerManager.damage_absorption_rate += cardMonsters.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardMonsters.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardMonsters.AccuracyRate;
        powerManager.lifesteal_rate += cardMonsters.LifestealRate;
        powerManager.shield_strength += cardMonsters.ShieldStrength;
        powerManager.tenacity += cardMonsters.Tenacity;
        powerManager.resistance_rate += cardMonsters.ResistanceRate;
        powerManager.combo_rate += cardMonsters.ComboRate;
        powerManager.reflection_rate += cardMonsters.ReflectionRate;
        powerManager.mana += cardMonsters.Mana;
        powerManager.mana_regeneration_rate += cardMonsters.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardMonsters.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardMonsters.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardMonsters.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardMonsters.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardMonsters.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardMonsters.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardMonsters.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardMonsters.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardMonsters.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardMonsters.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardMonsters.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardMonsters.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardMonsters.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardMonsters.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardMonsters.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardMilitaryPower()
    {
        PowerManager powerManager = new PowerManager();

        // CardMilitary cardMilitary = new CardMilitary();
        ICardMilitaryGalleryRepository cardMilitaryGalleryRepository = new CardMilitaryGalleryRepository();
        CardMilitaryGalleryService cardMilitaryGalleryService = new CardMilitaryGalleryService(cardMilitaryGalleryRepository);
        //Gallery
        CardMilitaries cardMilitary = cardMilitaryGalleryService.SumPowerCardMilitaryGallery();

        powerManager.power += cardMilitary.Power;
        powerManager.health += cardMilitary.Health;
        powerManager.physical_attack += cardMilitary.PhysicalAttack;
        powerManager.physical_defense += cardMilitary.PhysicalDefense;
        powerManager.magical_attack += cardMilitary.MagicalAttack;
        powerManager.magical_defense += cardMilitary.MagicalDefense;
        powerManager.chemical_attack += cardMilitary.ChemicalAttack;
        powerManager.chemical_defense += cardMilitary.ChemicalDefense;
        powerManager.atomic_attack += cardMilitary.AtomicAttack;
        powerManager.atomic_defense += cardMilitary.AtomicDefense;
        powerManager.mental_attack += cardMilitary.MentalAttack;
        powerManager.mental_defense += cardMilitary.MentalDefense;
        powerManager.speed += cardMilitary.Speed;
        powerManager.critical_damage_rate += cardMilitary.CriticalDamageRate;
        powerManager.critical_rate += cardMilitary.CriticalRate;
        powerManager.penetration_rate += cardMilitary.PenetrationRate;
        powerManager.evasion_rate += cardMilitary.EvasionRate;
        powerManager.damage_absorption_rate += cardMilitary.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardMilitary.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardMilitary.AccuracyRate;
        powerManager.lifesteal_rate += cardMilitary.LifestealRate;
        powerManager.shield_strength += cardMilitary.ShieldStrength;
        powerManager.tenacity += cardMilitary.Tenacity;
        powerManager.resistance_rate += cardMilitary.ResistanceRate;
        powerManager.combo_rate += cardMilitary.ComboRate;
        powerManager.reflection_rate += cardMilitary.ReflectionRate;
        powerManager.mana += cardMilitary.Mana;
        powerManager.mana_regeneration_rate += cardMilitary.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardMilitary.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardMilitary.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardMilitary.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardMilitary.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardMilitary.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardMilitary.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardMilitary.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardMilitary.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardMilitary.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardMilitary.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardMilitary.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardMilitary.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardMilitary.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardMilitary.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardMilitary.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCardSpellPower()
    {
        PowerManager powerManager = new PowerManager();

        // CardSpell cardSpell = new CardSpell();
        ICardSpellGalleryRepository cardSpellGalleryRepository = new CardSpellGalleryRepository();
        CardSpellGalleryService cardSpellGalleryService = new CardSpellGalleryService(cardSpellGalleryRepository);
        //Gallery
        CardSpells cardSpell = cardSpellGalleryService.SumPowerCardSpellGallery();

        powerManager.power += cardSpell.Power;
        powerManager.health += cardSpell.Health;
        powerManager.physical_attack += cardSpell.PhysicalAttack;
        powerManager.physical_defense += cardSpell.PhysicalDefense;
        powerManager.magical_attack += cardSpell.MagicalAttack;
        powerManager.magical_defense += cardSpell.MagicalDefense;
        powerManager.chemical_attack += cardSpell.ChemicalAttack;
        powerManager.chemical_defense += cardSpell.ChemicalDefense;
        powerManager.atomic_attack += cardSpell.AtomicAttack;
        powerManager.atomic_defense += cardSpell.AtomicDefense;
        powerManager.mental_attack += cardSpell.MentalAttack;
        powerManager.mental_defense += cardSpell.MentalDefense;
        powerManager.speed += cardSpell.Speed;
        powerManager.critical_damage_rate += cardSpell.CriticalDamageRate;
        powerManager.critical_rate += cardSpell.CriticalRate;
        powerManager.penetration_rate += cardSpell.PenetrationRate;
        powerManager.evasion_rate += cardSpell.EvasionRate;
        powerManager.damage_absorption_rate += cardSpell.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardSpell.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardSpell.AccuracyRate;
        powerManager.lifesteal_rate += cardSpell.LifestealRate;
        powerManager.shield_strength += cardSpell.ShieldStrength;
        powerManager.tenacity += cardSpell.Tenacity;
        powerManager.resistance_rate += cardSpell.ResistanceRate;
        powerManager.combo_rate += cardSpell.ComboRate;
        powerManager.reflection_rate += cardSpell.ReflectionRate;
        powerManager.mana += cardSpell.Mana;
        powerManager.mana_regeneration_rate += cardSpell.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardSpell.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardSpell.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardSpell.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardSpell.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardSpell.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardSpell.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardSpell.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardSpell.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardSpell.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardSpell.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardSpell.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardSpell.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardSpell.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardSpell.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardSpell.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCollaborationsPower()
    {
        PowerManager powerManager = new PowerManager();

        // Collaboration collaboration = new Collaboration();
        ICollaborationGalleryRepository collaborationGalleryRepository = new CollaborationGalleryRepository();
        CollaborationGalleryService collaborationGalleryService = new CollaborationGalleryService(collaborationGalleryRepository);
        // Gallery tổng hợp sức mạnh từ Collaboration Gallery
        Collaborations collaboration = collaborationGalleryService.SumPowerCollaborationsGallery();
        powerManager.power += collaboration.Power;
        powerManager.health += collaboration.Health;
        powerManager.physical_attack += collaboration.PhysicalAttack;
        powerManager.physical_defense += collaboration.PhysicalDefense;
        powerManager.magical_attack += collaboration.MagicalAttack;
        powerManager.magical_defense += collaboration.MagicalDefense;
        powerManager.chemical_attack += collaboration.ChemicalAttack;
        powerManager.chemical_defense += collaboration.ChemicalDefense;
        powerManager.atomic_attack += collaboration.AtomicAttack;
        powerManager.atomic_defense += collaboration.AtomicDefense;
        powerManager.mental_attack += collaboration.MentalAttack;
        powerManager.mental_defense += collaboration.MentalDefense;
        powerManager.speed += collaboration.Speed;
        powerManager.critical_damage_rate += collaboration.CriticalDamageRate;
        powerManager.critical_rate += collaboration.CriticalRate;
        powerManager.penetration_rate += collaboration.PenetrationRate;
        powerManager.evasion_rate += collaboration.EvasionRate;
        powerManager.damage_absorption_rate += collaboration.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += collaboration.VitalityRegenerationRate;
        powerManager.accuracy_rate += collaboration.AccuracyRate;
        powerManager.lifesteal_rate += collaboration.LifestealRate;
        powerManager.shield_strength += collaboration.ShieldStrength;
        powerManager.tenacity += collaboration.Tenacity;
        powerManager.resistance_rate += collaboration.ResistanceRate;
        powerManager.combo_rate += collaboration.ComboRate;
        powerManager.reflection_rate += collaboration.ReflectionRate;
        powerManager.mana += collaboration.Mana;
        powerManager.mana_regeneration_rate += collaboration.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += collaboration.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += collaboration.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += collaboration.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += collaboration.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += collaboration.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += collaboration.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += collaboration.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += collaboration.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += collaboration.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += collaboration.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += collaboration.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += collaboration.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += collaboration.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += collaboration.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += collaboration.PercentAllMentalDefense;

        IUserCollaborationRepository userCollaborationRepository = new UserCollaborationRepository();
        UserCollaborationService userCollaborationService = new UserCollaborationService(userCollaborationRepository);
        // Gallery tổng hợp sức mạnh từ User Collaborations
        collaboration = userCollaborationService.SumPowerUserCollaborations();
        powerManager.power += collaboration.Power;
        powerManager.health += collaboration.Health;
        powerManager.physical_attack += collaboration.PhysicalAttack;
        powerManager.physical_defense += collaboration.PhysicalDefense;
        powerManager.magical_attack += collaboration.MagicalAttack;
        powerManager.magical_defense += collaboration.MagicalDefense;
        powerManager.chemical_attack += collaboration.ChemicalAttack;
        powerManager.chemical_defense += collaboration.ChemicalDefense;
        powerManager.atomic_attack += collaboration.AtomicAttack;
        powerManager.atomic_defense += collaboration.AtomicDefense;
        powerManager.mental_attack += collaboration.MentalAttack;
        powerManager.mental_defense += collaboration.MentalDefense;
        powerManager.speed += collaboration.Speed;
        powerManager.critical_damage_rate += collaboration.CriticalDamageRate;
        powerManager.critical_rate += collaboration.CriticalRate;
        powerManager.penetration_rate += collaboration.PenetrationRate;
        powerManager.evasion_rate += collaboration.EvasionRate;
        powerManager.damage_absorption_rate += collaboration.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += collaboration.VitalityRegenerationRate;
        powerManager.accuracy_rate += collaboration.AccuracyRate;
        powerManager.lifesteal_rate += collaboration.LifestealRate;
        powerManager.shield_strength += collaboration.ShieldStrength;
        powerManager.tenacity += collaboration.Tenacity;
        powerManager.resistance_rate += collaboration.ResistanceRate;
        powerManager.combo_rate += collaboration.ComboRate;
        powerManager.reflection_rate += collaboration.ReflectionRate;
        powerManager.mana += collaboration.Mana;
        powerManager.mana_regeneration_rate += collaboration.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += collaboration.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += collaboration.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += collaboration.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += collaboration.ResistanceToSameFactionRate;

        ICollaborationRepository collaborationRepository = new CollaborationRepository();
        CollaborationService collaborationService = new CollaborationService(collaborationRepository);
        // Phần cộng dồn percent từ Collaboration Percent
        collaboration = collaborationService.SumPowerCollaborationsPercent();
        powerManager.PercentAllHealth += collaboration.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += collaboration.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += collaboration.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += collaboration.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += collaboration.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += collaboration.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += collaboration.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += collaboration.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += collaboration.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += collaboration.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += collaboration.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetCollaborationEquipmentsPower()
    {
        PowerManager powerManager = new PowerManager();
        // CollaborationEquipment collaborationEquipment = new CollaborationEquipment();
        ICollaborationEquipmentGalleryRepository collaborationEquipmentGalleryRepository = new CollaborationEquipmentGalleryRepository();
        CollaborationEquipmentGalleryService collaborationEquipmentGalleryService = new CollaborationEquipmentGalleryService(collaborationEquipmentGalleryRepository);
        // Sum power from Gallery Equipments
        CollaborationEquipments collaborationEquipment = collaborationEquipmentGalleryService.SumPowerCollaborationEquipmentsGallery();
        powerManager.power += collaborationEquipment.Power;
        powerManager.health += collaborationEquipment.Health;
        powerManager.physical_attack += collaborationEquipment.PhysicalAttack;
        powerManager.physical_defense += collaborationEquipment.PhysicalDefense;
        powerManager.magical_attack += collaborationEquipment.MagicalAttack;
        powerManager.magical_defense += collaborationEquipment.MagicalDefense;
        powerManager.chemical_attack += collaborationEquipment.ChemicalAttack;
        powerManager.chemical_defense += collaborationEquipment.ChemicalDefense;
        powerManager.atomic_attack += collaborationEquipment.AtomicAttack;
        powerManager.atomic_defense += collaborationEquipment.AtomicDefense;
        powerManager.mental_attack += collaborationEquipment.MentalAttack;
        powerManager.mental_defense += collaborationEquipment.MentalDefense;
        powerManager.speed += collaborationEquipment.Speed;
        powerManager.critical_damage_rate += collaborationEquipment.CriticalDamageRate;
        powerManager.critical_rate += collaborationEquipment.CriticalRate;
        powerManager.penetration_rate += collaborationEquipment.PenetrationRate;
        powerManager.evasion_rate += collaborationEquipment.EvasionRate;
        powerManager.damage_absorption_rate += collaborationEquipment.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += collaborationEquipment.VitalityRegenerationRate;
        powerManager.accuracy_rate += collaborationEquipment.AccuracyRate;
        powerManager.lifesteal_rate += collaborationEquipment.LifestealRate;
        powerManager.shield_strength += collaborationEquipment.ShieldStrength;
        powerManager.tenacity += collaborationEquipment.Tenacity;
        powerManager.resistance_rate += collaborationEquipment.ResistanceRate;
        powerManager.combo_rate += collaborationEquipment.ComboRate;
        powerManager.reflection_rate += collaborationEquipment.ReflectionRate;
        powerManager.mana += collaborationEquipment.Mana;
        powerManager.mana_regeneration_rate += collaborationEquipment.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += collaborationEquipment.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += collaborationEquipment.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += collaborationEquipment.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += collaborationEquipment.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += collaborationEquipment.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += collaborationEquipment.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += collaborationEquipment.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += collaborationEquipment.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += collaborationEquipment.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += collaborationEquipment.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += collaborationEquipment.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += collaborationEquipment.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += collaborationEquipment.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += collaborationEquipment.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += collaborationEquipment.PercentAllMentalDefense;

        IUserCollaborationEquipmentRepository userCollaborationEquipmentRepository = new UserCollaborationEquipmentRepository();
        UserCollaborationEquipmentService userCollaborationEquipmentService = new UserCollaborationEquipmentService(userCollaborationEquipmentRepository);
        // Sum power from User Collaboration Equipments
        collaborationEquipment = userCollaborationEquipmentService.SumPowerUserCollaborationEquipments();
        powerManager.power += collaborationEquipment.Power;
        powerManager.health += collaborationEquipment.Health;
        powerManager.physical_attack += collaborationEquipment.PhysicalAttack;
        powerManager.physical_defense += collaborationEquipment.PhysicalDefense;
        powerManager.magical_attack += collaborationEquipment.MagicalAttack;
        powerManager.magical_defense += collaborationEquipment.MagicalDefense;
        powerManager.chemical_attack += collaborationEquipment.ChemicalAttack;
        powerManager.chemical_defense += collaborationEquipment.ChemicalDefense;
        powerManager.atomic_attack += collaborationEquipment.AtomicAttack;
        powerManager.atomic_defense += collaborationEquipment.AtomicDefense;
        powerManager.mental_attack += collaborationEquipment.MentalAttack;
        powerManager.mental_defense += collaborationEquipment.MentalDefense;
        powerManager.speed += collaborationEquipment.Speed;
        powerManager.critical_damage_rate += collaborationEquipment.CriticalDamageRate;
        powerManager.critical_rate += collaborationEquipment.CriticalRate;
        powerManager.penetration_rate += collaborationEquipment.PenetrationRate;
        powerManager.evasion_rate += collaborationEquipment.EvasionRate;
        powerManager.damage_absorption_rate += collaborationEquipment.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += collaborationEquipment.VitalityRegenerationRate;
        powerManager.accuracy_rate += collaborationEquipment.AccuracyRate;
        powerManager.lifesteal_rate += collaborationEquipment.LifestealRate;
        powerManager.shield_strength += collaborationEquipment.ShieldStrength;
        powerManager.tenacity += collaborationEquipment.Tenacity;
        powerManager.resistance_rate += collaborationEquipment.ResistanceRate;
        powerManager.combo_rate += collaborationEquipment.ComboRate;
        powerManager.reflection_rate += collaborationEquipment.ReflectionRate;
        powerManager.mana += collaborationEquipment.Mana;
        powerManager.mana_regeneration_rate += collaborationEquipment.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += collaborationEquipment.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += collaborationEquipment.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += collaborationEquipment.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += collaborationEquipment.ResistanceToSameFactionRate;

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

        powerManager.power += equipments.Power;
        powerManager.health += equipments.Health;
        powerManager.physical_attack += equipments.PhysicalAttack;
        powerManager.physical_defense += equipments.PhysicalDefense;
        powerManager.magical_attack += equipments.MagicalAttack;
        powerManager.magical_defense += equipments.MagicalDefense;
        powerManager.chemical_attack += equipments.ChemicalAttack;
        powerManager.chemical_defense += equipments.ChemicalDefense;
        powerManager.atomic_attack += equipments.AtomicAttack;
        powerManager.atomic_defense += equipments.AtomicDefense;
        powerManager.mental_attack += equipments.MentalAttack;
        powerManager.mental_defense += equipments.MentalDefense;
        powerManager.speed += equipments.Speed;
        powerManager.critical_damage_rate += equipments.CriticalDamageRate;
        powerManager.critical_rate += equipments.CriticalRate;
        powerManager.penetration_rate += equipments.PenetrationRate;
        powerManager.evasion_rate += equipments.EvasionRate;
        powerManager.damage_absorption_rate += equipments.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += equipments.VitalityRegenerationRate;
        powerManager.accuracy_rate += equipments.AccuracyRate;
        powerManager.lifesteal_rate += equipments.LifestealRate;
        powerManager.shield_strength += equipments.ShieldStrength;
        powerManager.tenacity += equipments.Tenacity;
        powerManager.resistance_rate += equipments.ResistanceRate;
        powerManager.combo_rate += equipments.ComboRate;
        powerManager.reflection_rate += equipments.ReflectionRate;
        powerManager.mana += equipments.Mana;
        powerManager.mana_regeneration_rate += equipments.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += equipments.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += equipments.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += equipments.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += equipments.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += equipments.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += equipments.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += equipments.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += equipments.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += equipments.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += equipments.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += equipments.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += equipments.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += equipments.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += equipments.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += equipments.PercentAllMentalDefense;

        return powerManager;
    }
    public PowerManager GetMagicFormationCirclePower()
    {
        PowerManager powerManager = new PowerManager();
        // MagicFormationCircle magicFormationCircle = new MagicFormationCircle();
        IMagicFormationCircleGalleryRepository magicFormationCircleGalleryRepository = new MagicFormationCircleGalleryRepository();
        MagicFormationCircleGalleryService magicFormationCircleGalleryService = new MagicFormationCircleGalleryService(magicFormationCircleGalleryRepository);
        // Gallery
        MagicFormationCircles magicFormationCircle = magicFormationCircleGalleryService.SumPowerMagicFormationCircleGallery();
        powerManager.power += magicFormationCircle.Power;
        powerManager.health += magicFormationCircle.Health;
        powerManager.physical_attack += magicFormationCircle.PhysicalAttack;
        powerManager.physical_defense += magicFormationCircle.PhysicalDefense;
        powerManager.magical_attack += magicFormationCircle.MagicalAttack;
        powerManager.magical_defense += magicFormationCircle.MagicalDefense;
        powerManager.chemical_attack += magicFormationCircle.ChemicalAttack;
        powerManager.chemical_defense += magicFormationCircle.ChemicalDefense;
        powerManager.atomic_attack += magicFormationCircle.AtomicAttack;
        powerManager.atomic_defense += magicFormationCircle.AtomicDefense;
        powerManager.mental_attack += magicFormationCircle.MentalAttack;
        powerManager.mental_defense += magicFormationCircle.MentalDefense;
        powerManager.speed += magicFormationCircle.Speed;
        powerManager.critical_damage_rate += magicFormationCircle.CriticalDamageRate;
        powerManager.critical_rate += magicFormationCircle.CriticalRate;
        powerManager.penetration_rate += magicFormationCircle.PenetrationRate;
        powerManager.evasion_rate += magicFormationCircle.EvasionRate;
        powerManager.damage_absorption_rate += magicFormationCircle.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += magicFormationCircle.VitalityRegenerationRate;
        powerManager.accuracy_rate += magicFormationCircle.AccuracyRate;
        powerManager.lifesteal_rate += magicFormationCircle.LifestealRate;
        powerManager.shield_strength += magicFormationCircle.ShieldStrength;
        powerManager.tenacity += magicFormationCircle.Tenacity;
        powerManager.resistance_rate += magicFormationCircle.ResistanceRate;
        powerManager.combo_rate += magicFormationCircle.ComboRate;
        powerManager.reflection_rate += magicFormationCircle.ReflectionRate;
        powerManager.mana += magicFormationCircle.Mana;
        powerManager.mana_regeneration_rate += magicFormationCircle.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += magicFormationCircle.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += magicFormationCircle.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += magicFormationCircle.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += magicFormationCircle.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += magicFormationCircle.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += magicFormationCircle.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += magicFormationCircle.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += magicFormationCircle.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += magicFormationCircle.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += magicFormationCircle.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += magicFormationCircle.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += magicFormationCircle.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += magicFormationCircle.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += magicFormationCircle.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += magicFormationCircle.PercentAllMentalDefense;

        IUserMagicFormationCircleRepository userMagicFormationCircleRepository = new UserMagicFormationCirlceRepository();
        UserMagicFormationCircleService userMagicFormationCircleService = new UserMagicFormationCircleService(userMagicFormationCircleRepository);
        // User
        magicFormationCircle = userMagicFormationCircleService.SumPowerUserMagicFormationCircle();
        powerManager.power += magicFormationCircle.Power;
        powerManager.health += magicFormationCircle.Health;
        powerManager.physical_attack += magicFormationCircle.PhysicalAttack;
        powerManager.physical_defense += magicFormationCircle.PhysicalDefense;
        powerManager.magical_attack += magicFormationCircle.MagicalAttack;
        powerManager.magical_defense += magicFormationCircle.MagicalDefense;
        powerManager.chemical_attack += magicFormationCircle.ChemicalAttack;
        powerManager.chemical_defense += magicFormationCircle.ChemicalDefense;
        powerManager.atomic_attack += magicFormationCircle.AtomicAttack;
        powerManager.atomic_defense += magicFormationCircle.AtomicDefense;
        powerManager.mental_attack += magicFormationCircle.MentalAttack;
        powerManager.mental_defense += magicFormationCircle.MentalDefense;
        powerManager.speed += magicFormationCircle.Speed;
        powerManager.critical_damage_rate += magicFormationCircle.CriticalDamageRate;
        powerManager.critical_rate += magicFormationCircle.CriticalRate;
        powerManager.penetration_rate += magicFormationCircle.PenetrationRate;
        powerManager.evasion_rate += magicFormationCircle.EvasionRate;
        powerManager.damage_absorption_rate += magicFormationCircle.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += magicFormationCircle.VitalityRegenerationRate;
        powerManager.accuracy_rate += magicFormationCircle.AccuracyRate;
        powerManager.lifesteal_rate += magicFormationCircle.LifestealRate;
        powerManager.shield_strength += magicFormationCircle.ShieldStrength;
        powerManager.tenacity += magicFormationCircle.Tenacity;
        powerManager.resistance_rate += magicFormationCircle.ResistanceRate;
        powerManager.combo_rate += magicFormationCircle.ComboRate;
        powerManager.reflection_rate += magicFormationCircle.ReflectionRate;
        powerManager.mana += magicFormationCircle.Mana;
        powerManager.mana_regeneration_rate += magicFormationCircle.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += magicFormationCircle.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += magicFormationCircle.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += magicFormationCircle.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += magicFormationCircle.ResistanceToSameFactionRate;

        IMagicFormationCircleRepository magicFormationCircleRepository = new MagicFormationCircleRepository();
        MagicFormationCircleService magicFormationCircleService = new MagicFormationCircleService(magicFormationCircleRepository);
        // Percent
        magicFormationCircle = magicFormationCircleService.SumPowerMagicFormationCirclePercent();
        powerManager.PercentAllHealth += magicFormationCircle.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += magicFormationCircle.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += magicFormationCircle.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += magicFormationCircle.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += magicFormationCircle.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += magicFormationCircle.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += magicFormationCircle.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += magicFormationCircle.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += magicFormationCircle.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += magicFormationCircle.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += magicFormationCircle.PercentAllMentalDefense;

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
        powerManager.power += relics.Power;
        powerManager.health += relics.Health;
        powerManager.physical_attack += relics.PhysicalAttack;
        powerManager.physical_defense += relics.PhysicalDefense;
        powerManager.magical_attack += relics.MagicalAttack;
        powerManager.magical_defense += relics.MagicalDefense;
        powerManager.chemical_attack += relics.ChemicalAttack;
        powerManager.chemical_defense += relics.ChemicalDefense;
        powerManager.atomic_attack += relics.AtomicAttack;
        powerManager.atomic_defense += relics.AtomicDefense;
        powerManager.mental_attack += relics.MentalAttack;
        powerManager.mental_defense += relics.MentalDefense;
        powerManager.speed += relics.Speed;
        powerManager.critical_damage_rate += relics.CriticalDamageRate;
        powerManager.critical_rate += relics.CriticalRate;
        powerManager.penetration_rate += relics.PenetrationRate;
        powerManager.evasion_rate += relics.EvasionRate;
        powerManager.damage_absorption_rate += relics.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += relics.VitalityRegenerationRate;
        powerManager.accuracy_rate += relics.AccuracyRate;
        powerManager.lifesteal_rate += relics.LifestealRate;
        powerManager.shield_strength += relics.ShieldStrength;
        powerManager.tenacity += relics.Tenacity;
        powerManager.resistance_rate += relics.ResistanceRate;
        powerManager.combo_rate += relics.ComboRate;
        powerManager.reflection_rate += relics.ReflectionRate;
        powerManager.mana += relics.Mana;
        powerManager.mana_regeneration_rate += relics.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += relics.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += relics.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += relics.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += relics.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += relics.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += relics.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += relics.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += relics.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += relics.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += relics.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += relics.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += relics.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += relics.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += relics.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += relics.PercentAllMentalDefense;

        IUserRelicsRepository userRelicsRepository = new UserRelicsRepository();
        UserRelicsService userRelicsService = new UserRelicsService(userRelicsRepository);
        // User
        relics = userRelicsService.SumPowerUserRelics();
        powerManager.power += relics.Power;
        powerManager.health += relics.Health;
        powerManager.physical_attack += relics.PhysicalAttack;
        powerManager.physical_defense += relics.PhysicalDefense;
        powerManager.magical_attack += relics.MagicalAttack;
        powerManager.magical_defense += relics.MagicalDefense;
        powerManager.chemical_attack += relics.ChemicalAttack;
        powerManager.chemical_defense += relics.ChemicalDefense;
        powerManager.atomic_attack += relics.AtomicAttack;
        powerManager.atomic_defense += relics.AtomicDefense;
        powerManager.mental_attack += relics.MentalAttack;
        powerManager.mental_defense += relics.MentalDefense;
        powerManager.speed += relics.Speed;
        powerManager.critical_damage_rate += relics.CriticalDamageRate;
        powerManager.critical_rate += relics.CriticalRate;
        powerManager.penetration_rate += relics.PenetrationRate;
        powerManager.evasion_rate += relics.EvasionRate;
        powerManager.damage_absorption_rate += relics.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += relics.VitalityRegenerationRate;
        powerManager.accuracy_rate += relics.AccuracyRate;
        powerManager.lifesteal_rate += relics.LifestealRate;
        powerManager.shield_strength += relics.ShieldStrength;
        powerManager.tenacity += relics.Tenacity;
        powerManager.resistance_rate += relics.ResistanceRate;
        powerManager.combo_rate += relics.ComboRate;
        powerManager.reflection_rate += relics.ReflectionRate;
        powerManager.mana += relics.Mana;
        powerManager.mana_regeneration_rate += relics.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += relics.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += relics.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += relics.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += relics.ResistanceToSameFactionRate;

        IRelicsRepository relicsRepository = new RelicsRepository();
        RelicsService relicsService = new RelicsService(relicsRepository);
        // Percent
        relics = relicsService.SumPowerRelicsPercent();
        powerManager.PercentAllHealth += relics.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += relics.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += relics.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += relics.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += relics.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += relics.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += relics.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += relics.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += relics.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += relics.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += relics.PercentAllMentalDefense;

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
        powerManager.power += medals.Power;
        powerManager.health += medals.Health;
        powerManager.physical_attack += medals.PhysicalAttack;
        powerManager.physical_defense += medals.PhysicalDefense;
        powerManager.magical_attack += medals.MagicalAttack;
        powerManager.magical_defense += medals.MagicalDefense;
        powerManager.chemical_attack += medals.ChemicalAttack;
        powerManager.chemical_defense += medals.ChemicalDefense;
        powerManager.atomic_attack += medals.AtomicAttack;
        powerManager.atomic_defense += medals.AtomicDefense;
        powerManager.mental_attack += medals.MentalAttack;
        powerManager.mental_defense += medals.MentalDefense;
        powerManager.speed += medals.Speed;
        powerManager.critical_damage_rate += medals.CriticalDamageRate;
        powerManager.critical_rate += medals.CriticalRate;
        powerManager.penetration_rate += medals.PenetrationRate;
        powerManager.evasion_rate += medals.EvasionRate;
        powerManager.damage_absorption_rate += medals.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += medals.VitalityRegenerationRate;
        powerManager.accuracy_rate += medals.AccuracyRate;
        powerManager.lifesteal_rate += medals.LifestealRate;
        powerManager.shield_strength += medals.ShieldStrength;
        powerManager.tenacity += medals.Tenacity;
        powerManager.resistance_rate += medals.ResistanceRate;
        powerManager.combo_rate += medals.ComboRate;
        powerManager.reflection_rate += medals.ReflectionRate;
        powerManager.mana += medals.Mana;
        powerManager.mana_regeneration_rate += medals.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += medals.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += medals.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += medals.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += medals.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += medals.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += medals.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += medals.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += medals.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += medals.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += medals.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += medals.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += medals.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += medals.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += medals.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += medals.PercentAllMentalDefense;

        IUserMedalsRepository userMedalsRepository = new UserMedalsRepository();
        UserMedalsService userMedalsService = new UserMedalsService(userMedalsRepository);
        // User Medals (Gallery)
        medals = userMedalsService.SumPowerUserMedals(); // Giả định SumPowerUserMedals cũng trả về một đối tượng Medals mới hoặc đã được reset
        powerManager.power += medals.Power;
        powerManager.health += medals.Health;
        powerManager.physical_attack += medals.PhysicalAttack;
        powerManager.physical_defense += medals.PhysicalDefense;
        powerManager.magical_attack += medals.MagicalAttack;
        powerManager.magical_defense += medals.MagicalDefense;
        powerManager.chemical_attack += medals.ChemicalAttack;
        powerManager.chemical_defense += medals.ChemicalDefense;
        powerManager.atomic_attack += medals.AtomicAttack;
        powerManager.atomic_defense += medals.AtomicDefense;
        powerManager.mental_attack += medals.MentalAttack;
        powerManager.mental_defense += medals.MentalDefense;
        powerManager.speed += medals.Speed;
        powerManager.critical_damage_rate += medals.CriticalDamageRate;
        powerManager.critical_rate += medals.CriticalRate;
        powerManager.penetration_rate += medals.PenetrationRate;
        powerManager.evasion_rate += medals.EvasionRate;
        powerManager.damage_absorption_rate += medals.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += medals.VitalityRegenerationRate;
        powerManager.accuracy_rate += medals.AccuracyRate;
        powerManager.lifesteal_rate += medals.LifestealRate;
        powerManager.shield_strength += medals.ShieldStrength;
        powerManager.tenacity += medals.Tenacity;
        powerManager.resistance_rate += medals.ResistanceRate;
        powerManager.combo_rate += medals.ComboRate;
        powerManager.reflection_rate += medals.ReflectionRate;
        powerManager.mana += medals.Mana;
        powerManager.mana_regeneration_rate += medals.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += medals.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += medals.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += medals.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += medals.ResistanceToSameFactionRate;

        IMedalsRepository medalsRepository = new MedalsRepository();
        MedalsService medalsService = new MedalsService(medalsRepository);
        // Percent
        medals = medalsService.SumPowerMedalsPercent(); // Giả định SumPowerMedalsPercent cũng trả về một đối tượng Medals mới hoặc đã được reset
        powerManager.PercentAllHealth += medals.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += medals.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += medals.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += medals.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += medals.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += medals.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += medals.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += medals.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += medals.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += medals.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += medals.PercentAllMentalDefense;

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
        powerManager.power += pets.Power;
        powerManager.health += pets.Health;
        powerManager.physical_attack += pets.PhysicalAttack;
        powerManager.physical_defense += pets.PhysicalDefense;
        powerManager.magical_attack += pets.MagicalAttack;
        powerManager.magical_defense += pets.MagicalDefense;
        powerManager.chemical_attack += pets.ChemicalAttack;
        powerManager.chemical_defense += pets.ChemicalDefense;
        powerManager.atomic_attack += pets.AtomicAttack;
        powerManager.atomic_defense += pets.AtomicDefense;
        powerManager.mental_attack += pets.MentalAttack;
        powerManager.mental_defense += pets.MentalDefense;
        powerManager.speed += pets.Speed;
        powerManager.critical_damage_rate += pets.CriticalDamageRate;
        powerManager.critical_rate += pets.CriticalRate;
        powerManager.penetration_rate += pets.PenetrationRate;
        powerManager.evasion_rate += pets.EvasionRate;
        powerManager.damage_absorption_rate += pets.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += pets.VitalityRegenerationRate;
        powerManager.accuracy_rate += pets.AccuracyRate;
        powerManager.lifesteal_rate += pets.LifestealRate;
        powerManager.shield_strength += pets.ShieldStrength;
        powerManager.tenacity += pets.Tenacity;
        powerManager.resistance_rate += pets.ResistanceRate;
        powerManager.combo_rate += pets.ComboRate;
        powerManager.reflection_rate += pets.ReflectionRate;
        powerManager.mana += pets.Mana;
        powerManager.mana_regeneration_rate += pets.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += pets.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += pets.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += pets.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += pets.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += pets.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += pets.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += pets.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += pets.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += pets.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += pets.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += pets.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += pets.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += pets.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += pets.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += pets.PercentAllMentalDefense;

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
        powerManager.power += symbols.Power;
        powerManager.health += symbols.Health;
        powerManager.physical_attack += symbols.PhysicalAttack;
        powerManager.physical_defense += symbols.PhysicalDefense;
        powerManager.magical_attack += symbols.MagicalAttack;
        powerManager.magical_defense += symbols.MagicalDefense;
        powerManager.chemical_attack += symbols.ChemicalAttack;
        powerManager.chemical_defense += symbols.ChemicalDefense;
        powerManager.atomic_attack += symbols.AtomicAttack;
        powerManager.atomic_defense += symbols.AtomicDefense;
        powerManager.mental_attack += symbols.MentalAttack;
        powerManager.mental_defense += symbols.MentalDefense;
        powerManager.speed += symbols.Speed;
        powerManager.critical_damage_rate += symbols.CriticalDamageRate;
        powerManager.critical_rate += symbols.CriticalRate;
        powerManager.penetration_rate += symbols.PenetrationRate;
        powerManager.evasion_rate += symbols.EvasionRate;
        powerManager.damage_absorption_rate += symbols.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += symbols.VitalityRegenerationRate;
        powerManager.accuracy_rate += symbols.AccuracyRate;
        powerManager.lifesteal_rate += symbols.LifestealRate;
        powerManager.shield_strength += symbols.ShieldStrength;
        powerManager.tenacity += symbols.Tenacity;
        powerManager.resistance_rate += symbols.ResistanceRate;
        powerManager.combo_rate += symbols.ComboRate;
        powerManager.reflection_rate += symbols.ReflectionRate;
        powerManager.mana += symbols.Mana;
        powerManager.mana_regeneration_rate += symbols.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += symbols.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += symbols.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += symbols.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += symbols.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += symbols.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += symbols.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += symbols.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += symbols.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += symbols.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += symbols.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += symbols.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += symbols.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += symbols.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += symbols.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += symbols.PercentAllMentalDefense;

        IUserSymbolsRepository userSymbolsRepository = new UserSymbolsRepository();
        UserSymbolsService userSymbolsService = new UserSymbolsService(userSymbolsRepository);
        // User Symbols (Gallery)
        symbols = userSymbolsService.SumPowerUserSymbols(); // Giả định SumPowerUserSymbols cũng trả về một đối tượng Symbols mới hoặc đã được reset
        powerManager.power += symbols.Power;
        powerManager.health += symbols.Health;
        powerManager.physical_attack += symbols.PhysicalAttack;
        powerManager.physical_defense += symbols.PhysicalDefense;
        powerManager.magical_attack += symbols.MagicalAttack;
        powerManager.magical_defense += symbols.MagicalDefense;
        powerManager.chemical_attack += symbols.ChemicalAttack;
        powerManager.chemical_defense += symbols.ChemicalDefense;
        powerManager.atomic_attack += symbols.AtomicAttack;
        powerManager.atomic_defense += symbols.AtomicDefense;
        powerManager.mental_attack += symbols.MentalAttack;
        powerManager.mental_defense += symbols.MentalDefense;
        powerManager.speed += symbols.Speed;
        powerManager.critical_damage_rate += symbols.CriticalDamageRate;
        powerManager.critical_rate += symbols.CriticalRate;
        powerManager.penetration_rate += symbols.PenetrationRate;
        powerManager.evasion_rate += symbols.EvasionRate;
        powerManager.damage_absorption_rate += symbols.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += symbols.VitalityRegenerationRate;
        powerManager.accuracy_rate += symbols.AccuracyRate;
        powerManager.lifesteal_rate += symbols.LifestealRate;
        powerManager.shield_strength += symbols.ShieldStrength;
        powerManager.tenacity += symbols.Tenacity;
        powerManager.resistance_rate += symbols.ResistanceRate;
        powerManager.combo_rate += symbols.ComboRate;
        powerManager.reflection_rate += symbols.ReflectionRate;
        powerManager.mana += symbols.Mana;
        powerManager.mana_regeneration_rate += symbols.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += symbols.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += symbols.ResistanceToDifferentFactionRate;
        powerManager.resistance_to_same_faction_rate += symbols.ResistanceToSameFactionRate;
        powerManager.damage_to_same_faction_rate += symbols.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += symbols.ResistanceToSameFactionRate;

        ISymbolsRepository symbolsRepository = new SymbolsRepository();
        SymbolsService symbolsService = new SymbolsService(symbolsRepository);
        // Percent
        symbols = symbolsService.SumPowerSymbolsPercent(); // Giả định SumPowerSymbolsPercent cũng trả về một đối tượng Symbols mới hoặc đã được reset
        powerManager.PercentAllHealth += symbols.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += symbols.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += symbols.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += symbols.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += symbols.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += symbols.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += symbols.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += symbols.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += symbols.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += symbols.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += symbols.PercentAllMentalDefense;

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
        powerManager.power += skills.Power;
        powerManager.health += skills.Health;
        powerManager.physical_attack += skills.PhysicalAttack;
        powerManager.physical_defense += skills.PhysicalDefense;
        powerManager.magical_attack += skills.MagicalAttack;
        powerManager.magical_defense += skills.MagicalDefense;
        powerManager.chemical_attack += skills.ChemicalAttack;
        powerManager.chemical_defense += skills.ChemicalDefense;
        powerManager.atomic_attack += skills.AtomicAttack;
        powerManager.atomic_defense += skills.AtomicDefense;
        powerManager.mental_attack += skills.MentalAttack;
        powerManager.mental_defense += skills.MentalDefense;
        powerManager.speed += skills.Speed;
        powerManager.critical_damage_rate += skills.CriticalDamageRate;
        powerManager.critical_rate += skills.CriticalRate;
        powerManager.penetration_rate += skills.PenetrationRate;
        powerManager.evasion_rate += skills.EvasionRate;
        powerManager.damage_absorption_rate += skills.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += skills.VitalityRegenerationRate;
        powerManager.accuracy_rate += skills.AccuracyRate;
        powerManager.lifesteal_rate += skills.LifestealRate;
        powerManager.shield_strength += skills.ShieldStrength;
        powerManager.tenacity += skills.Tenacity;
        powerManager.resistance_rate += skills.ResistanceRate;
        powerManager.combo_rate += skills.ComboRate;
        powerManager.reflection_rate += skills.ReflectionRate;
        powerManager.mana += skills.Mana;
        powerManager.mana_regeneration_rate += skills.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += skills.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += skills.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += skills.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += skills.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += skills.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += skills.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += skills.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += skills.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += skills.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += skills.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += skills.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += skills.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += skills.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += skills.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += skills.PercentAllMentalDefense;

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
        powerManager.power += titles.Power;
        powerManager.health += titles.Health;
        powerManager.physical_attack += titles.PhysicalAttack;
        powerManager.physical_defense += titles.PhysicalDefense;
        powerManager.magical_attack += titles.MagicalAttack;
        powerManager.magical_defense += titles.MagicalDefense;
        powerManager.chemical_attack += titles.ChemicalAttack;
        powerManager.chemical_defense += titles.ChemicalDefense;
        powerManager.atomic_attack += titles.AtomicAttack;
        powerManager.atomic_defense += titles.AtomicDefense;
        powerManager.mental_attack += titles.MentalAttack;
        powerManager.mental_defense += titles.MentalDefense;
        powerManager.speed += titles.Speed;
        powerManager.critical_damage_rate += titles.CriticalDamageRate;
        powerManager.critical_rate += titles.CriticalRate;
        powerManager.penetration_rate += titles.PenetrationRate;
        powerManager.evasion_rate += titles.EvasionRate;
        powerManager.damage_absorption_rate += titles.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += titles.VitalityRegenerationRate;
        powerManager.accuracy_rate += titles.AccuracyRate;
        powerManager.lifesteal_rate += titles.LifestealRate;
        powerManager.shield_strength += titles.ShieldStrength;
        powerManager.tenacity += titles.Tenacity;
        powerManager.resistance_rate += titles.ResistanceRate;
        powerManager.combo_rate += titles.ComboRate;
        powerManager.reflection_rate += titles.ReflectionRate;
        powerManager.mana += titles.Mana;
        powerManager.mana_regeneration_rate += titles.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += titles.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += titles.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += titles.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += titles.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += titles.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += titles.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += titles.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += titles.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += titles.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += titles.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += titles.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += titles.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += titles.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += titles.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += titles.PercentAllMentalDefense;

        IUserTitlesRepository userTitlesRepository = new UserTitlesRepository();
        UserTitlesService userTitlesService = new UserTitlesService(userTitlesRepository);
        // User Titles (Gallery)
        titles = userTitlesService.SumPowerUserTitles(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.power += titles.Power;
        powerManager.health += titles.Health;
        powerManager.physical_attack += titles.PhysicalAttack;
        powerManager.physical_defense += titles.PhysicalDefense;
        powerManager.magical_attack += titles.MagicalAttack;
        powerManager.magical_defense += titles.MagicalDefense;
        powerManager.chemical_attack += titles.ChemicalAttack;
        powerManager.chemical_defense += titles.ChemicalDefense;
        powerManager.atomic_attack += titles.AtomicAttack;
        powerManager.atomic_defense += titles.AtomicDefense;
        powerManager.mental_attack += titles.MentalAttack;
        powerManager.mental_defense += titles.MentalDefense;
        powerManager.speed += titles.Speed;
        powerManager.critical_damage_rate += titles.CriticalDamageRate;
        powerManager.critical_rate += titles.CriticalRate;
        powerManager.penetration_rate += titles.PenetrationRate;
        powerManager.evasion_rate += titles.EvasionRate;
        powerManager.damage_absorption_rate += titles.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += titles.VitalityRegenerationRate;
        powerManager.accuracy_rate += titles.AccuracyRate;
        powerManager.lifesteal_rate += titles.LifestealRate;
        powerManager.shield_strength += titles.ShieldStrength;
        powerManager.tenacity += titles.Tenacity;
        powerManager.resistance_rate += titles.ResistanceRate;
        powerManager.combo_rate += titles.ComboRate;
        powerManager.reflection_rate += titles.ReflectionRate;
        powerManager.mana += titles.Mana;
        powerManager.mana_regeneration_rate += titles.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += titles.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += titles.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += titles.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += titles.ResistanceToSameFactionRate;

        ITitlesRepository titlesRepository = new TitlesRepository();
        TitlesService titlesService = new TitlesService(titlesRepository);
        // Percent
        titles = titlesService.SumPowerTitlesPercent(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.PercentAllHealth += titles.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += titles.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += titles.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += titles.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += titles.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += titles.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += titles.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += titles.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += titles.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += titles.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += titles.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetTalismanPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Talisman talisman = new Talisman();
        ITalismanGalleryRepository talismanGalleryRepository = new TalismanGalleryRepository();
        TalismanGalleryService talismanGalleryService = new TalismanGalleryService(talismanGalleryRepository);
        // Gallery
        Talismans talisman = talismanGalleryService.SumPowerTalismanGallery();
        powerManager.power += talisman.Power;
        powerManager.health += talisman.Health;
        powerManager.physical_attack += talisman.PhysicalAttack;
        powerManager.physical_defense += talisman.PhysicalDefense;
        powerManager.magical_attack += talisman.MagicalAttack;
        powerManager.magical_defense += talisman.MagicalDefense;
        powerManager.chemical_attack += talisman.ChemicalAttack;
        powerManager.chemical_defense += talisman.ChemicalDefense;
        powerManager.atomic_attack += talisman.AtomicAttack;
        powerManager.atomic_defense += talisman.AtomicDefense;
        powerManager.mental_attack += talisman.MentalAttack;
        powerManager.mental_defense += talisman.MentalDefense;
        powerManager.speed += talisman.Speed;
        powerManager.critical_damage_rate += talisman.CriticalDamageRate;
        powerManager.critical_rate += talisman.CriticalRate;
        powerManager.penetration_rate += talisman.PenetrationRate;
        powerManager.evasion_rate += talisman.EvasionRate;
        powerManager.damage_absorption_rate += talisman.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += talisman.VitalityRegenerationRate;
        powerManager.accuracy_rate += talisman.AccuracyRate;
        powerManager.lifesteal_rate += talisman.LifestealRate;
        powerManager.shield_strength += talisman.ShieldStrength;
        powerManager.tenacity += talisman.Tenacity;
        powerManager.resistance_rate += talisman.ResistanceRate;
        powerManager.combo_rate += talisman.ComboRate;
        powerManager.reflection_rate += talisman.ReflectionRate;
        powerManager.mana += talisman.Mana;
        powerManager.mana_regeneration_rate += talisman.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += talisman.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += talisman.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += talisman.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += talisman.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += talisman.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += talisman.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += talisman.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += talisman.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += talisman.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += talisman.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += talisman.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += talisman.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += talisman.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += talisman.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += talisman.PercentAllMentalDefense;

        IUserTalismanRepository userTalismanRepository = new UserTalismanRepository();
        UserTalismanService userTalismanService = new UserTalismanService(userTalismanRepository);
        // User
        talisman = userTalismanService.SumPowerUserTalisman(); // Giả định SumPowerUserTalisman cũng trả về một đối tượng Talisman mới hoặc đã được reset
        powerManager.power += talisman.Power;
        powerManager.health += talisman.Health;
        powerManager.physical_attack += talisman.PhysicalAttack;
        powerManager.physical_defense += talisman.PhysicalDefense;
        powerManager.magical_attack += talisman.MagicalAttack;
        powerManager.magical_defense += talisman.MagicalDefense;
        powerManager.chemical_attack += talisman.ChemicalAttack;
        powerManager.chemical_defense += talisman.ChemicalDefense;
        powerManager.atomic_attack += talisman.AtomicAttack;
        powerManager.atomic_defense += talisman.AtomicDefense;
        powerManager.mental_attack += talisman.MentalAttack;
        powerManager.mental_defense += talisman.MentalDefense;
        powerManager.speed += talisman.Speed;
        powerManager.critical_damage_rate += talisman.CriticalDamageRate;
        powerManager.critical_rate += talisman.CriticalRate;
        powerManager.penetration_rate += talisman.PenetrationRate;
        powerManager.evasion_rate += talisman.EvasionRate;
        powerManager.damage_absorption_rate += talisman.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += talisman.VitalityRegenerationRate;
        powerManager.accuracy_rate += talisman.AccuracyRate;
        powerManager.lifesteal_rate += talisman.LifestealRate;
        powerManager.shield_strength += talisman.ShieldStrength;
        powerManager.tenacity += talisman.Tenacity;
        powerManager.resistance_rate += talisman.ResistanceRate;
        powerManager.combo_rate += talisman.ComboRate;
        powerManager.reflection_rate += talisman.ReflectionRate;
        powerManager.mana += talisman.Mana;
        powerManager.mana_regeneration_rate += talisman.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += talisman.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += talisman.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += talisman.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += talisman.ResistanceToSameFactionRate;

        ITalismanRepository talismanRepository = new TalismanRepository();
        TalismanService talismanService = new TalismanService(talismanRepository);
        // Percent
        talisman = talismanService.SumPowerTalismanPercent(); // Giả định SumPowerTalismanPercent cũng trả về một đối tượng Talisman mới hoặc đã được reset
        powerManager.PercentAllHealth += talisman.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += talisman.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += talisman.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += talisman.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += talisman.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += talisman.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += talisman.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += talisman.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += talisman.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += talisman.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += talisman.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetPuppetPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Puppet puppet = new Puppet();
        IPuppetGalleryRepository puppetGalleryRepository = new PuppetGalleryRepository();
        PuppetGalleryService puppetGalleryService = new PuppetGalleryService(puppetGalleryRepository);
        // Gallery
        Puppets puppet = puppetGalleryService.SumPowerPuppetGallery();
        powerManager.power += puppet.Power;
        powerManager.health += puppet.Health;
        powerManager.physical_attack += puppet.PhysicalAttack;
        powerManager.physical_defense += puppet.PhysicalDefense;
        powerManager.magical_attack += puppet.MagicalAttack;
        powerManager.magical_defense += puppet.MagicalDefense;
        powerManager.chemical_attack += puppet.ChemicalAttack;
        powerManager.chemical_defense += puppet.ChemicalDefense;
        powerManager.atomic_attack += puppet.AtomicAttack;
        powerManager.atomic_defense += puppet.AtomicDefense;
        powerManager.mental_attack += puppet.MentalAttack;
        powerManager.mental_defense += puppet.MentalDefense;
        powerManager.speed += puppet.Speed;
        powerManager.critical_damage_rate += puppet.CriticalDamageRate;
        powerManager.critical_rate += puppet.CriticalRate;
        powerManager.penetration_rate += puppet.PenetrationRate;
        powerManager.evasion_rate += puppet.EvasionRate;
        powerManager.damage_absorption_rate += puppet.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += puppet.VitalityRegenerationRate;
        powerManager.accuracy_rate += puppet.AccuracyRate;
        powerManager.lifesteal_rate += puppet.LifestealRate;
        powerManager.shield_strength += puppet.ShieldStrength;
        powerManager.tenacity += puppet.Tenacity;
        powerManager.resistance_rate += puppet.ResistanceRate;
        powerManager.combo_rate += puppet.ComboRate;
        powerManager.reflection_rate += puppet.ReflectionRate;
        powerManager.mana += puppet.Mana;
        powerManager.mana_regeneration_rate += puppet.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += puppet.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += puppet.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += puppet.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += puppet.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += puppet.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += puppet.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += puppet.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += puppet.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += puppet.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += puppet.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += puppet.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += puppet.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += puppet.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += puppet.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += puppet.PercentAllMentalDefense;

        IUserPuppetRepository userPuppetRepository = new UserPuppetRepository();
        UserPuppetService userPuppetService = new UserPuppetService(userPuppetRepository);
        // User
        puppet = userPuppetService.SumPowerUserPuppet(); // Giả định SumPowerUserPuppet cũng trả về một đối tượng Puppet mới hoặc đã được reset
        powerManager.power += puppet.Power;
        powerManager.health += puppet.Health;
        powerManager.physical_attack += puppet.PhysicalAttack;
        powerManager.physical_defense += puppet.PhysicalDefense;
        powerManager.magical_attack += puppet.MagicalAttack;
        powerManager.magical_defense += puppet.MagicalDefense;
        powerManager.chemical_attack += puppet.ChemicalAttack;
        powerManager.chemical_defense += puppet.ChemicalDefense;
        powerManager.atomic_attack += puppet.AtomicAttack;
        powerManager.atomic_defense += puppet.AtomicDefense;
        powerManager.mental_attack += puppet.MentalAttack;
        powerManager.mental_defense += puppet.MentalDefense;
        powerManager.speed += puppet.Speed;
        powerManager.critical_damage_rate += puppet.CriticalDamageRate;
        powerManager.critical_rate += puppet.CriticalRate;
        powerManager.penetration_rate += puppet.PenetrationRate;
        powerManager.evasion_rate += puppet.EvasionRate;
        powerManager.damage_absorption_rate += puppet.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += puppet.VitalityRegenerationRate;
        powerManager.accuracy_rate += puppet.AccuracyRate;
        powerManager.lifesteal_rate += puppet.LifestealRate;
        powerManager.shield_strength += puppet.ShieldStrength;
        powerManager.tenacity += puppet.Tenacity;
        powerManager.resistance_rate += puppet.ResistanceRate;
        powerManager.combo_rate += puppet.ComboRate;
        powerManager.reflection_rate += puppet.ReflectionRate;
        powerManager.mana += puppet.Mana;
        powerManager.mana_regeneration_rate += puppet.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += puppet.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += puppet.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += puppet.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += puppet.ResistanceToSameFactionRate;

        IPuppetRepository puppetRepository = new PuppetRepository();
        PuppetService puppetService = new PuppetService(puppetRepository);
        // Percent
        puppet = puppetService.SumPowerPuppetPercent(); // Giả định SumPowerPuppetPercent cũng trả về một đối tượng Puppet mới hoặc đã được reset
        powerManager.PercentAllHealth += puppet.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += puppet.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += puppet.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += puppet.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += puppet.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += puppet.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += puppet.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += puppet.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += puppet.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += puppet.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += puppet.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetAlchemyPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Alchemy alchemy = new Alchemy();
        IAlchemyGalleryRepository alchemyGalleryRepository = new AlchemyGalleryRepository();
        AlchemyGalleryService alchemyGalleryService = new AlchemyGalleryService(alchemyGalleryRepository);
        // Gallery
        Alchemies alchemy = alchemyGalleryService.SumPowerAlchemyGallery();
        powerManager.power += alchemy.Power;
        powerManager.health += alchemy.Health;
        powerManager.physical_attack += alchemy.PhysicalAttack;
        powerManager.physical_defense += alchemy.PhysicalDefense;
        powerManager.magical_attack += alchemy.MagicalAttack;
        powerManager.magical_defense += alchemy.MagicalDefense;
        powerManager.chemical_attack += alchemy.ChemicalAttack;
        powerManager.chemical_defense += alchemy.ChemicalDefense;
        powerManager.atomic_attack += alchemy.AtomicAttack;
        powerManager.atomic_defense += alchemy.AtomicDefense;
        powerManager.mental_attack += alchemy.MentalAttack;
        powerManager.mental_defense += alchemy.MentalDefense;
        powerManager.speed += alchemy.Speed;
        powerManager.critical_damage_rate += alchemy.CriticalDamageRate;
        powerManager.critical_rate += alchemy.CriticalRate;
        powerManager.penetration_rate += alchemy.PenetrationRate;
        powerManager.evasion_rate += alchemy.EvasionRate;
        powerManager.damage_absorption_rate += alchemy.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += alchemy.VitalityRegenerationRate;
        powerManager.accuracy_rate += alchemy.AccuracyRate;
        powerManager.lifesteal_rate += alchemy.LifestealRate;
        powerManager.shield_strength += alchemy.ShieldStrength;
        powerManager.tenacity += alchemy.Tenacity;
        powerManager.resistance_rate += alchemy.ResistanceRate;
        powerManager.combo_rate += alchemy.ComboRate;
        powerManager.reflection_rate += alchemy.ReflectionRate;
        powerManager.mana += alchemy.Mana;
        powerManager.mana_regeneration_rate += alchemy.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += alchemy.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += alchemy.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += alchemy.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += alchemy.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += alchemy.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += alchemy.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += alchemy.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += alchemy.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += alchemy.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += alchemy.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += alchemy.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += alchemy.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += alchemy.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += alchemy.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += alchemy.PercentAllMentalDefense;

        IUserAlchemyRepository userAlchemyRepository = new UserAlchemyRepository();
        UserAlchemyService userAlchemyService = new UserAlchemyService(userAlchemyRepository);
        // User
        alchemy = userAlchemyService.SumPowerUserAlchemy(); // Giả định SumPowerUserAlchemy cũng trả về một đối tượng Alchemy mới hoặc đã được reset
        powerManager.power += alchemy.Power;
        powerManager.health += alchemy.Health;
        powerManager.physical_attack += alchemy.PhysicalAttack;
        powerManager.physical_defense += alchemy.PhysicalDefense;
        powerManager.magical_attack += alchemy.MagicalAttack;
        powerManager.magical_defense += alchemy.MagicalDefense;
        powerManager.chemical_attack += alchemy.ChemicalAttack;
        powerManager.chemical_defense += alchemy.ChemicalDefense;
        powerManager.atomic_attack += alchemy.AtomicAttack;
        powerManager.atomic_defense += alchemy.AtomicDefense;
        powerManager.mental_attack += alchemy.MentalAttack;
        powerManager.mental_defense += alchemy.MentalDefense;
        powerManager.speed += alchemy.Speed;
        powerManager.critical_damage_rate += alchemy.CriticalDamageRate;
        powerManager.critical_rate += alchemy.CriticalRate;
        powerManager.penetration_rate += alchemy.PenetrationRate;
        powerManager.evasion_rate += alchemy.EvasionRate;
        powerManager.damage_absorption_rate += alchemy.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += alchemy.VitalityRegenerationRate;
        powerManager.accuracy_rate += alchemy.AccuracyRate;
        powerManager.lifesteal_rate += alchemy.LifestealRate;
        powerManager.shield_strength += alchemy.ShieldStrength;
        powerManager.tenacity += alchemy.Tenacity;
        powerManager.resistance_rate += alchemy.ResistanceRate;
        powerManager.combo_rate += alchemy.ComboRate;
        powerManager.reflection_rate += alchemy.ReflectionRate;
        powerManager.mana += alchemy.Mana;
        powerManager.mana_regeneration_rate += alchemy.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += alchemy.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += alchemy.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += alchemy.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += alchemy.ResistanceToSameFactionRate;

        IAlchemyRepository alchemyRepository = new AlchemyRepository();
        AlchemyService alchemyService = new AlchemyService(alchemyRepository);
        // Percent
        alchemy = alchemyService.SumPowerAlchemyPercent(); // Giả định SumPowerAlchemyPercent cũng trả về một đối tượng Alchemy mới hoặc đã được reset
        powerManager.PercentAllHealth += alchemy.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += alchemy.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += alchemy.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += alchemy.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += alchemy.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += alchemy.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += alchemy.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += alchemy.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += alchemy.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += alchemy.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += alchemy.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetForgePower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Forge forge = new Forge();
        IForgeGalleryRepository forgeGalleryRepository = new ForgeGalleryRepository();
        ForgeGalleryService forgeGalleryService = new ForgeGalleryService(forgeGalleryRepository);
        // Gallery
        Forges forge = forgeGalleryService.SumPowerForgeGallery();
        powerManager.power += forge.Power;
        powerManager.health += forge.Health;
        powerManager.physical_attack += forge.PhysicalAttack;
        powerManager.physical_defense += forge.PhysicalDefense;
        powerManager.magical_attack += forge.MagicalAttack;
        powerManager.magical_defense += forge.MagicalDefense;
        powerManager.chemical_attack += forge.ChemicalAttack;
        powerManager.chemical_defense += forge.ChemicalDefense;
        powerManager.atomic_attack += forge.AtomicAttack;
        powerManager.atomic_defense += forge.AtomicDefense;
        powerManager.mental_attack += forge.MentalAttack;
        powerManager.mental_defense += forge.MentalDefense;
        powerManager.speed += forge.Speed;
        powerManager.critical_damage_rate += forge.CriticalDamageRate;
        powerManager.critical_rate += forge.CriticalRate;
        powerManager.penetration_rate += forge.PenetrationRate;
        powerManager.evasion_rate += forge.EvasionRate;
        powerManager.damage_absorption_rate += forge.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += forge.VitalityRegenerationRate;
        powerManager.accuracy_rate += forge.AccuracyRate;
        powerManager.lifesteal_rate += forge.LifestealRate;
        powerManager.shield_strength += forge.ShieldStrength;
        powerManager.tenacity += forge.Tenacity;
        powerManager.resistance_rate += forge.ResistanceRate;
        powerManager.combo_rate += forge.ComboRate;
        powerManager.reflection_rate += forge.ReflectionRate;
        powerManager.mana += forge.Mana;
        powerManager.mana_regeneration_rate += forge.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += forge.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += forge.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += forge.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += forge.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += forge.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += forge.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += forge.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += forge.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += forge.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += forge.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += forge.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += forge.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += forge.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += forge.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += forge.PercentAllMentalDefense;

        IUserForgeRepository userForgeRepository = new UserForgeRepository();
        UserForgeService userForgeService = new UserForgeService(userForgeRepository);
        // User
        forge = userForgeService.SumPowerUserForge(); // Giả định SumPowerUserForge cũng trả về một đối tượng Forge mới hoặc đã được reset
        powerManager.power += forge.Power;
        powerManager.health += forge.Health;
        powerManager.physical_attack += forge.PhysicalAttack;
        powerManager.physical_defense += forge.PhysicalDefense;
        powerManager.magical_attack += forge.MagicalAttack;
        powerManager.magical_defense += forge.MagicalDefense;
        powerManager.chemical_attack += forge.ChemicalAttack;
        powerManager.chemical_defense += forge.ChemicalDefense;
        powerManager.atomic_attack += forge.AtomicAttack;
        powerManager.atomic_defense += forge.AtomicDefense;
        powerManager.mental_attack += forge.MentalAttack;
        powerManager.mental_defense += forge.MentalDefense;
        powerManager.speed += forge.Speed;
        powerManager.critical_damage_rate += forge.CriticalDamageRate;
        powerManager.critical_rate += forge.CriticalRate;
        powerManager.penetration_rate += forge.PenetrationRate;
        powerManager.evasion_rate += forge.EvasionRate;
        powerManager.damage_absorption_rate += forge.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += forge.VitalityRegenerationRate;
        powerManager.accuracy_rate += forge.AccuracyRate;
        powerManager.lifesteal_rate += forge.LifestealRate;
        powerManager.shield_strength += forge.ShieldStrength;
        powerManager.tenacity += forge.Tenacity;
        powerManager.resistance_rate += forge.ResistanceRate;
        powerManager.combo_rate += forge.ComboRate;
        powerManager.reflection_rate += forge.ReflectionRate;
        powerManager.mana += forge.Mana;
        powerManager.mana_regeneration_rate += forge.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += forge.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += forge.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += forge.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += forge.ResistanceToSameFactionRate;

        IForgeRepository forgeRepository = new ForgeRepository();
        ForgeService forgeService = new ForgeService(forgeRepository);
        // Percent
        forge = forgeService.SumPowerForgePercent(); // Giả định SumPowerForgePercent cũng trả về một đối tượng Forge mới hoặc đã được reset
        powerManager.PercentAllHealth += forge.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += forge.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += forge.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += forge.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += forge.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += forge.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += forge.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += forge.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += forge.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += forge.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += forge.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetCardLifePower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // CardLife cardLife = new CardLife();
        ICardLifeGalleryRepository cardLifeGalleryRepository = new CardLifeGalleryRepository();
        CardLifeGalleryService cardLifeGalleryService = new CardLifeGalleryService(cardLifeGalleryRepository);
        // Gallery
        CardLives cardLife = cardLifeGalleryService.SumPowerCardLifeGallery();
        powerManager.power += cardLife.Power;
        powerManager.health += cardLife.Health;
        powerManager.physical_attack += cardLife.PhysicalAttack;
        powerManager.physical_defense += cardLife.PhysicalDefense;
        powerManager.magical_attack += cardLife.MagicalAttack;
        powerManager.magical_defense += cardLife.MagicalDefense;
        powerManager.chemical_attack += cardLife.ChemicalAttack;
        powerManager.chemical_defense += cardLife.ChemicalDefense;
        powerManager.atomic_attack += cardLife.AtomicAttack;
        powerManager.atomic_defense += cardLife.AtomicDefense;
        powerManager.mental_attack += cardLife.MentalAttack;
        powerManager.mental_defense += cardLife.MentalDefense;
        powerManager.speed += cardLife.Speed;
        powerManager.critical_damage_rate += cardLife.CriticalDamageRate;
        powerManager.critical_rate += cardLife.CriticalRate;
        powerManager.penetration_rate += cardLife.PenetrationRate;
        powerManager.evasion_rate += cardLife.EvasionRate;
        powerManager.damage_absorption_rate += cardLife.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardLife.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardLife.AccuracyRate;
        powerManager.lifesteal_rate += cardLife.LifestealRate;
        powerManager.shield_strength += cardLife.ShieldStrength;
        powerManager.tenacity += cardLife.Tenacity;
        powerManager.resistance_rate += cardLife.ResistanceRate;
        powerManager.combo_rate += cardLife.ComboRate;
        powerManager.reflection_rate += cardLife.ReflectionRate;
        powerManager.mana += cardLife.Mana;
        powerManager.mana_regeneration_rate += cardLife.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardLife.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardLife.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardLife.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardLife.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += cardLife.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardLife.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardLife.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardLife.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardLife.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardLife.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardLife.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardLife.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardLife.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardLife.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardLife.PercentAllMentalDefense;

        IUserCardLifeRepository userCardLifeRepository = new UserCardLifeRepository();
        UserCardLifeService userCardLifeService = new UserCardLifeService(userCardLifeRepository);
        // User
        cardLife = userCardLifeService.SumPowerUserCardLife(); // Giả định SumPowerUserCardLife cũng trả về một đối tượng CardLife mới hoặc đã được reset
        powerManager.power += cardLife.Power;
        powerManager.health += cardLife.Health;
        powerManager.physical_attack += cardLife.PhysicalAttack;
        powerManager.physical_defense += cardLife.PhysicalDefense;
        powerManager.magical_attack += cardLife.MagicalAttack;
        powerManager.magical_defense += cardLife.MagicalDefense;
        powerManager.chemical_attack += cardLife.ChemicalAttack;
        powerManager.chemical_defense += cardLife.ChemicalDefense;
        powerManager.atomic_attack += cardLife.AtomicAttack;
        powerManager.atomic_defense += cardLife.AtomicDefense;
        powerManager.mental_attack += cardLife.MentalAttack;
        powerManager.mental_defense += cardLife.MentalDefense;
        powerManager.speed += cardLife.Speed;
        powerManager.critical_damage_rate += cardLife.CriticalDamageRate;
        powerManager.critical_rate += cardLife.CriticalRate;
        powerManager.penetration_rate += cardLife.PenetrationRate;
        powerManager.evasion_rate += cardLife.EvasionRate;
        powerManager.damage_absorption_rate += cardLife.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += cardLife.VitalityRegenerationRate;
        powerManager.accuracy_rate += cardLife.AccuracyRate;
        powerManager.lifesteal_rate += cardLife.LifestealRate;
        powerManager.shield_strength += cardLife.ShieldStrength;
        powerManager.tenacity += cardLife.Tenacity;
        powerManager.resistance_rate += cardLife.ResistanceRate;
        powerManager.combo_rate += cardLife.ComboRate;
        powerManager.reflection_rate += cardLife.ReflectionRate;
        powerManager.mana += cardLife.Mana;
        powerManager.mana_regeneration_rate += cardLife.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += cardLife.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += cardLife.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += cardLife.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += cardLife.ResistanceToSameFactionRate;

        ICardLifeRepository cardLifeRepository = new CardLifeRepository();
        CardLifeService cardLifeService = new CardLifeService(cardLifeRepository);
        // Percent
        cardLife = cardLifeService.SumPowerCardLifePercent(); // Giả định SumPowerCardLifePercent cũng trả về một đối tượng CardLife mới hoặc đã được reset
        powerManager.PercentAllHealth += cardLife.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += cardLife.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += cardLife.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += cardLife.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += cardLife.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += cardLife.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += cardLife.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += cardLife.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += cardLife.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += cardLife.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += cardLife.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetArtworkPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Artwork Artwork = new Artwork();
        IArtworkGalleryRepository ArtworkGalleryRepository = new ArtworkGalleryRepository();
        ArtworkGalleryService ArtworkGalleryService = new ArtworkGalleryService(ArtworkGalleryRepository);
        // Gallery
        Artworks artwork = ArtworkGalleryService.SumPowerArtworkGallery();
        powerManager.power += artwork.Power;
        powerManager.health += artwork.Health;
        powerManager.physical_attack += artwork.PhysicalAttack;
        powerManager.physical_defense += artwork.PhysicalDefense;
        powerManager.magical_attack += artwork.MagicalAttack;
        powerManager.magical_defense += artwork.MagicalDefense;
        powerManager.chemical_attack += artwork.ChemicalAttack;
        powerManager.chemical_defense += artwork.ChemicalDefense;
        powerManager.atomic_attack += artwork.AtomicAttack;
        powerManager.atomic_defense += artwork.AtomicDefense;
        powerManager.mental_attack += artwork.MentalAttack;
        powerManager.mental_defense += artwork.MentalDefense;
        powerManager.speed += artwork.Speed;
        powerManager.critical_damage_rate += artwork.CriticalDamageRate;
        powerManager.critical_rate += artwork.CriticalRate;
        powerManager.penetration_rate += artwork.PenetrationRate;
        powerManager.evasion_rate += artwork.EvasionRate;
        powerManager.damage_absorption_rate += artwork.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += artwork.VitalityRegenerationRate;
        powerManager.accuracy_rate += artwork.AccuracyRate;
        powerManager.lifesteal_rate += artwork.LifestealRate;
        powerManager.shield_strength += artwork.ShieldStrength;
        powerManager.tenacity += artwork.Tenacity;
        powerManager.resistance_rate += artwork.ResistanceRate;
        powerManager.combo_rate += artwork.ComboRate;
        powerManager.reflection_rate += artwork.ReflectionRate;
        powerManager.mana += artwork.Mana;
        powerManager.mana_regeneration_rate += artwork.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += artwork.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += artwork.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += artwork.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += artwork.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += artwork.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += artwork.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += artwork.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += artwork.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += artwork.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += artwork.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += artwork.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += artwork.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += artwork.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += artwork.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += artwork.PercentAllMentalDefense;

        IUserArtworkRepository userArtworkRepository = new UserArtworkRepository();
        UserArtworkService userArtworkService = new UserArtworkService(userArtworkRepository);
        // User
        artwork = userArtworkService.SumPowerUserArtwork(); // Giả định SumPowerUserartwork cũng trả về một đối tượng artwork mới hoặc đã được reset
        powerManager.power += artwork.Power;
        powerManager.health += artwork.Health;
        powerManager.physical_attack += artwork.PhysicalAttack;
        powerManager.physical_defense += artwork.PhysicalDefense;
        powerManager.magical_attack += artwork.MagicalAttack;
        powerManager.magical_defense += artwork.MagicalDefense;
        powerManager.chemical_attack += artwork.ChemicalAttack;
        powerManager.chemical_defense += artwork.ChemicalDefense;
        powerManager.atomic_attack += artwork.AtomicAttack;
        powerManager.atomic_defense += artwork.AtomicDefense;
        powerManager.mental_attack += artwork.MentalAttack;
        powerManager.mental_defense += artwork.MentalDefense;
        powerManager.speed += artwork.Speed;
        powerManager.critical_damage_rate += artwork.CriticalDamageRate;
        powerManager.critical_rate += artwork.CriticalRate;
        powerManager.penetration_rate += artwork.PenetrationRate;
        powerManager.evasion_rate += artwork.EvasionRate;
        powerManager.damage_absorption_rate += artwork.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += artwork.VitalityRegenerationRate;
        powerManager.accuracy_rate += artwork.AccuracyRate;
        powerManager.lifesteal_rate += artwork.LifestealRate;
        powerManager.shield_strength += artwork.ShieldStrength;
        powerManager.tenacity += artwork.Tenacity;
        powerManager.resistance_rate += artwork.ResistanceRate;
        powerManager.combo_rate += artwork.ComboRate;
        powerManager.reflection_rate += artwork.ReflectionRate;
        powerManager.mana += artwork.Mana;
        powerManager.mana_regeneration_rate += artwork.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += artwork.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += artwork.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += artwork.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += artwork.ResistanceToSameFactionRate;

        IArtworkRepository artworkRepository = new ArtworkRepository();
        ArtworkService artworkService = new ArtworkService(artworkRepository);
        // Percent
        artwork = artworkService.SumPowerArtworkPercent(); // Giả định SumPowerartworkPercent cũng trả về một đối tượng artwork mới hoặc đã được reset
        powerManager.PercentAllHealth += artwork.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += artwork.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += artwork.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += artwork.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += artwork.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += artwork.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += artwork.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += artwork.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += artwork.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += artwork.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += artwork.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetSpiritBeastPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        ISpiritBeastGalleryRepository spiritBeastGalleryRepository = new SpiritBeastGalleryRepository();
        SpiritBeastGalleryService SpiritBeastGalleryService = new SpiritBeastGalleryService(spiritBeastGalleryRepository);
        // Gallery
        SpiritBeasts spiritBeast = SpiritBeastGalleryService.SumPowerSpiritBeastGallery();
        powerManager.power += spiritBeast.Power;
        powerManager.health += spiritBeast.Health;
        powerManager.physical_attack += spiritBeast.PhysicalAttack;
        powerManager.physical_defense += spiritBeast.PhysicalDefense;
        powerManager.magical_attack += spiritBeast.MagicalAttack;
        powerManager.magical_defense += spiritBeast.MagicalDefense;
        powerManager.chemical_attack += spiritBeast.ChemicalAttack;
        powerManager.chemical_defense += spiritBeast.ChemicalDefense;
        powerManager.atomic_attack += spiritBeast.AtomicAttack;
        powerManager.atomic_defense += spiritBeast.AtomicDefense;
        powerManager.mental_attack += spiritBeast.MentalAttack;
        powerManager.mental_defense += spiritBeast.MentalDefense;
        powerManager.speed += spiritBeast.Speed;
        powerManager.critical_damage_rate += spiritBeast.CriticalDamageRate;
        powerManager.critical_rate += spiritBeast.CriticalRate;
        powerManager.penetration_rate += spiritBeast.PenetrationRate;
        powerManager.evasion_rate += spiritBeast.EvasionRate;
        powerManager.damage_absorption_rate += spiritBeast.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += spiritBeast.VitalityRegenerationRate;
        powerManager.accuracy_rate += spiritBeast.AccuracyRate;
        powerManager.lifesteal_rate += spiritBeast.LifestealRate;
        powerManager.shield_strength += spiritBeast.ShieldStrength;
        powerManager.tenacity += spiritBeast.Tenacity;
        powerManager.resistance_rate += spiritBeast.ResistanceRate;
        powerManager.combo_rate += spiritBeast.ComboRate;
        powerManager.reflection_rate += spiritBeast.ReflectionRate;
        powerManager.mana += spiritBeast.Mana;
        powerManager.mana_regeneration_rate += spiritBeast.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += spiritBeast.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += spiritBeast.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += spiritBeast.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += spiritBeast.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += spiritBeast.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += spiritBeast.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += spiritBeast.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += spiritBeast.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += spiritBeast.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += spiritBeast.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += spiritBeast.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += spiritBeast.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += spiritBeast.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += spiritBeast.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += spiritBeast.PercentAllMentalDefense;

        IUserSpiritBeastRepository userSpiritBeastRepository = new UserSpiritBeastRepository();
        UserSpiritBeastService userSpiritBeastService = new UserSpiritBeastService(userSpiritBeastRepository);
        // User SpiritBeast (Gallery)
        spiritBeast = userSpiritBeastService.SumPowerUserSpiritBeast(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.power += spiritBeast.Power;
        powerManager.health += spiritBeast.Health;
        powerManager.physical_attack += spiritBeast.PhysicalAttack;
        powerManager.physical_defense += spiritBeast.PhysicalDefense;
        powerManager.magical_attack += spiritBeast.MagicalAttack;
        powerManager.magical_defense += spiritBeast.MagicalDefense;
        powerManager.chemical_attack += spiritBeast.ChemicalAttack;
        powerManager.chemical_defense += spiritBeast.ChemicalDefense;
        powerManager.atomic_attack += spiritBeast.AtomicAttack;
        powerManager.atomic_defense += spiritBeast.AtomicDefense;
        powerManager.mental_attack += spiritBeast.MentalAttack;
        powerManager.mental_defense += spiritBeast.MentalDefense;
        powerManager.speed += spiritBeast.Speed;
        powerManager.critical_damage_rate += spiritBeast.CriticalDamageRate;
        powerManager.critical_rate += spiritBeast.CriticalRate;
        powerManager.penetration_rate += spiritBeast.PenetrationRate;
        powerManager.evasion_rate += spiritBeast.EvasionRate;
        powerManager.damage_absorption_rate += spiritBeast.DamageAbsorptionRate;
        powerManager.vitality_regeneration_rate += spiritBeast.VitalityRegenerationRate;
        powerManager.accuracy_rate += spiritBeast.AccuracyRate;
        powerManager.lifesteal_rate += spiritBeast.LifestealRate;
        powerManager.shield_strength += spiritBeast.ShieldStrength;
        powerManager.tenacity += spiritBeast.Tenacity;
        powerManager.resistance_rate += spiritBeast.ResistanceRate;
        powerManager.combo_rate += spiritBeast.ComboRate;
        powerManager.reflection_rate += spiritBeast.ReflectionRate;
        powerManager.mana += spiritBeast.Mana;
        powerManager.mana_regeneration_rate += spiritBeast.ManaRegenerationRate;
        powerManager.damage_to_different_faction_rate += spiritBeast.DamageToDifferentFactionRate;
        powerManager.resistance_to_different_faction_rate += spiritBeast.ResistanceToDifferentFactionRate;
        powerManager.damage_to_same_faction_rate += spiritBeast.DamageToSameFactionRate;
        powerManager.resistance_to_same_faction_rate += spiritBeast.ResistanceToSameFactionRate;

        ISpiritBeastRepository spiritBeastRepository = new SpiritBeastRepository();
        SpiritBeastService spiritBeastService = new SpiritBeastService(spiritBeastRepository);
        // Percent
        spiritBeast = spiritBeastService.SumPowerSpiritBeastPercent(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
        powerManager.PercentAllHealth += spiritBeast.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += spiritBeast.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += spiritBeast.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += spiritBeast.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += spiritBeast.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += spiritBeast.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += spiritBeast.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += spiritBeast.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += spiritBeast.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += spiritBeast.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += spiritBeast.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }

}