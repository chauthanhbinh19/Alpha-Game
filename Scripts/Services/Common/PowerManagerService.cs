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

        // Lấy sức mạnh từ avatars
        PowerManager avatarsPower = GetAvatarsPower();
        AddPower(totalPower, avatarsPower);

        // Lấy sức mạnh từ books
        PowerManager booksPower = GetBooksPower();
        AddPower(totalPower, booksPower);

        // Lấy sức mạnh từ borders
        PowerManager bordersPower = GetBordersPower();
        AddPower(totalPower, bordersPower);

        // Lấy sức mạnh từ card heroes
        PowerManager cardHeroesPower = GetCardHeroesPower();
        AddPower(totalPower, cardHeroesPower);

        // Lấy sức mạnh từ card captains
        PowerManager cardCaptainsPower = GetCardCaptainsPower();
        AddPower(totalPower, cardCaptainsPower);

        // Lấy sức mạnh từ card colonels
        PowerManager cardColonelsPower = GetCardColonelsPower();
        AddPower(totalPower, cardColonelsPower);

        // Lấy sức mạnh từ card generals
        PowerManager cardGeneralsPower = GetCardGeneralsPower();
        AddPower(totalPower, cardGeneralsPower);

        // Lấy sức mạnh từ card admirals
        PowerManager cardAdmiralsPower = GetCardAdmiralsPower();
        AddPower(totalPower, cardAdmiralsPower);

        // Lấy sức mạnh từ card monsters
        PowerManager cardMonstersPower = GetCardMonstersPower();
        AddPower(totalPower, cardMonstersPower);

        // Lấy sức mạnh từ card militaries
        PowerManager cardMilitariesPower = GetCardMilitariesPower();
        AddPower(totalPower, cardMilitariesPower);

        // Lấy sức mạnh từ card spell
        PowerManager cardSpellsPower = GetCardSpellsPower();
        AddPower(totalPower, cardSpellsPower);

        // Lấy sức mạnh từ collaborations
        PowerManager collaborationsPower = GetCollaborationsPower();
        AddPower(totalPower, collaborationsPower);

        // Lấy sức mạnh từ collaboration equipments
        PowerManager collaborationEquipmentsPower = GetCollaborationEquipmentsPower();
        AddPower(totalPower, collaborationEquipmentsPower);

        // Lấy sức mạnh từ equipments
        PowerManager equipmentsPower = GetEquipmentsPower();
        AddPower(totalPower, equipmentsPower);

        // Lấy sức mạnh từ magic formation circles
        PowerManager magicFormationCirclesPower = GetMagicFormationCirclesPower();
        AddPower(totalPower, magicFormationCirclesPower);

        // Lấy sức mạnh từ relics
        PowerManager relicsPower = GetRelicsPower();
        AddPower(totalPower, relicsPower);

        // Lấy sức mạnh từ medals
        PowerManager medalsPower = GetMedalsPower();
        AddPower(totalPower, medalsPower);

        // Lấy sức mạnh từ pets
        PowerManager petsPower = GetPetsPower();
        AddPower(totalPower, petsPower);

        // Lấy sức mạnh từ symbols
        PowerManager symbolsPower = GetSymbolsPower();
        AddPower(totalPower, symbolsPower);

        // Lấy sức mạnh từ skills
        PowerManager skillsPower = GetSkillsPower();
        AddPower(totalPower, skillsPower);

        // Lấy sức mạnh từ titles
        PowerManager titlesPower = GetTitlesPower();
        AddPower(totalPower, titlesPower);

        // Lấy sức mạnh từ talismans
        PowerManager talismansPower = GetTalismansPower();
        AddPower(totalPower, talismansPower);

        // Lấy sức mạnh từ puppets
        PowerManager puppetsPower = GetPuppetsPower();
        AddPower(totalPower, puppetsPower);

        // Lấy sức mạnh từ alchemies
        PowerManager alchemiesPower = GetAlchemiesPower();
        AddPower(totalPower, alchemiesPower);

        // Lấy sức mạnh từ forges
        PowerManager forgesPower = GetForgesPower();
        AddPower(totalPower, forgesPower);

        // Lấy sức mạnh từ card lives
        PowerManager cardLivesPower = GetCardLivesPower();
        AddPower(totalPower, cardLivesPower);

        // Lấy sức mạnh từ artworks
        PowerManager artworksPower = GetArtworksPower();
        AddPower(totalPower, artworksPower);

        // Lấy sức mạnh từ spirit beasts
        PowerManager spiritBeastsPower = GetSpiritBeastsPower();
        AddPower(totalPower, spiritBeastsPower);

        // Lấy sức mạnh từ spirit cards
        PowerManager spiritCardsPower = GetSpiritCardsPower();
        AddPower(totalPower, spiritCardsPower);

        // Lấy sức mạnh từ vehicles
        PowerManager vehiclesPower = GetVehiclesPower();
        AddPower(totalPower, vehiclesPower);

        // Lấy sức mạnh từ architectures
        PowerManager architecturePower = GetArchitecturesPower();
        AddPower(totalPower, architecturePower);

        // Lấy sức mạnh từ technologies
        PowerManager techonogiesPower = GetTechnologiesPower();
        AddPower(totalPower, techonogiesPower);

        // Lấy sức mạnh từ cards
        PowerManager cardPower = GetCardsPower();
        AddPower(totalPower, cardPower);

        // Lấy sức mạnh từ cores
        PowerManager coresPower = GetCoresPower();
        AddPower(totalPower, coresPower);

        // Lấy sức mạnh từ weapons
        PowerManager weaponsPower = GetWeaponsPower();
        AddPower(totalPower, weaponsPower);

        // Lấy sức mạnh từ robots
        PowerManager robotsPower = GetRobotsPower();
        AddPower(totalPower, robotsPower);

        return totalPower;
    }
    private void AddPower(PowerManager target, PowerManager source)
    {
        target.Power += source.Power;
        target.Health += source.Health;
        target.PhysicalAttack += source.PhysicalAttack;
        target.PhysicalDefense += source.PhysicalDefense;
        target.MagicalAttack += source.MagicalAttack;
        target.MagicalDefense += source.MagicalDefense;
        target.ChemicalAttack += source.ChemicalAttack;
        target.ChemicalDefense += source.ChemicalDefense;
        target.AtomicAttack += source.AtomicAttack;
        target.AtomicDefense += source.AtomicDefense;
        target.MentalAttack += source.MentalAttack;
        target.MentalDefense += source.MentalDefense;
        target.Speed += source.Speed;
        target.CriticalDamageRate += source.CriticalDamageRate;
        target.CriticalRate += source.CriticalRate;
        target.PenetrationRate += source.PenetrationRate;
        target.EvasionRate += source.EvasionRate;
        target.DamageAbsorptionRate += source.DamageAbsorptionRate;
        target.VitalityRegenerationRate += source.VitalityRegenerationRate;
        target.AccuracyRate += source.AccuracyRate;
        target.LifestealRate += source.LifestealRate;
        target.ShieldStrength += source.ShieldStrength;
        target.Tenacity += source.Tenacity;
        target.ResistanceRate += source.ResistanceRate;
        target.ComboRate += source.ComboRate;
        target.ReflectionRate += source.ReflectionRate;
        target.Mana += source.Mana;
        target.ManaRegenerationRate += source.ManaRegenerationRate;
        target.DamageToDifferentFactionRate += source.DamageToDifferentFactionRate;
        target.ResistanceToDifferentFactionRate += source.ResistanceToDifferentFactionRate;
        target.DamageToSameFactionRate += source.DamageToSameFactionRate;
        target.ResistanceToSameFactionRate += source.ResistanceToSameFactionRate;

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

        powerManager.Power += userAchievements.Power;
        powerManager.Health += userAchievements.Health;
        powerManager.PhysicalAttack += userAchievements.PhysicalAttack;
        powerManager.PhysicalDefense += userAchievements.PhysicalDefense;
        powerManager.MagicalAttack += userAchievements.MagicalAttack;
        powerManager.MagicalDefense += userAchievements.MagicalDefense;
        powerManager.ChemicalAttack += userAchievements.ChemicalAttack;
        powerManager.ChemicalDefense += userAchievements.ChemicalDefense;
        powerManager.AtomicAttack += userAchievements.AtomicAttack;
        powerManager.AtomicDefense += userAchievements.AtomicDefense;
        powerManager.MentalAttack += userAchievements.MentalAttack;
        powerManager.MentalDefense += userAchievements.MentalDefense;
        powerManager.Speed += userAchievements.Speed;
        powerManager.CriticalDamageRate += userAchievements.CriticalDamageRate;
        powerManager.CriticalRate += userAchievements.CriticalRate;
        powerManager.PenetrationRate += userAchievements.PenetrationRate;
        powerManager.EvasionRate += userAchievements.EvasionRate;
        powerManager.DamageAbsorptionRate += userAchievements.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += userAchievements.VitalityRegenerationRate;
        powerManager.AccuracyRate += userAchievements.AccuracyRate;
        powerManager.LifestealRate += userAchievements.LifestealRate;
        powerManager.ShieldStrength += userAchievements.ShieldStrength;
        powerManager.Tenacity += userAchievements.Tenacity;
        powerManager.ResistanceRate += userAchievements.ResistanceRate;
        powerManager.ComboRate += userAchievements.ComboRate;
        powerManager.ReflectionRate += userAchievements.ReflectionRate;
        powerManager.Mana += userAchievements.Mana;
        powerManager.ManaRegenerationRate += userAchievements.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += userAchievements.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += userAchievements.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += userAchievements.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += userAchievements.ResistanceToSameFactionRate;

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
        Achievements galleryAvatars = avatarsGalleryService.SumPowerAvatarsGallery();

        powerManager.Power += galleryAvatars.Power;
        powerManager.Health += galleryAvatars.Health;
        powerManager.PhysicalAttack += galleryAvatars.PhysicalAttack;
        powerManager.PhysicalDefense += galleryAvatars.PhysicalDefense;
        powerManager.MagicalAttack += galleryAvatars.MagicalAttack;
        powerManager.MagicalDefense += galleryAvatars.MagicalDefense;
        powerManager.ChemicalAttack += galleryAvatars.ChemicalAttack;
        powerManager.ChemicalDefense += galleryAvatars.ChemicalDefense;
        powerManager.AtomicAttack += galleryAvatars.AtomicAttack;
        powerManager.AtomicDefense += galleryAvatars.AtomicDefense;
        powerManager.MentalAttack += galleryAvatars.MentalAttack;
        powerManager.MentalDefense += galleryAvatars.MentalDefense;
        powerManager.Speed += galleryAvatars.Speed;
        powerManager.CriticalDamageRate += galleryAvatars.CriticalDamageRate;
        powerManager.CriticalRate += galleryAvatars.CriticalRate;
        powerManager.PenetrationRate += galleryAvatars.PenetrationRate;
        powerManager.EvasionRate += galleryAvatars.EvasionRate;
        powerManager.DamageAbsorptionRate += galleryAvatars.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += galleryAvatars.VitalityRegenerationRate;
        powerManager.AccuracyRate += galleryAvatars.AccuracyRate;
        powerManager.LifestealRate += galleryAvatars.LifestealRate;
        powerManager.ShieldStrength += galleryAvatars.ShieldStrength;
        powerManager.Tenacity += galleryAvatars.Tenacity;
        powerManager.ResistanceRate += galleryAvatars.ResistanceRate;
        powerManager.ComboRate += galleryAvatars.ComboRate;
        powerManager.ReflectionRate += galleryAvatars.ReflectionRate;
        powerManager.Mana += galleryAvatars.Mana;
        powerManager.ManaRegenerationRate += galleryAvatars.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += galleryAvatars.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += galleryAvatars.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += galleryAvatars.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += galleryAvatars.ResistanceToSameFactionRate;

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
        Achievements userAvatars = userAvatarsService.SumPowerUserAvatars();

        powerManager.Power += userAvatars.Power;
        powerManager.Health += userAvatars.Health;
        powerManager.PhysicalAttack += userAvatars.PhysicalAttack;
        powerManager.PhysicalDefense += userAvatars.PhysicalDefense;
        powerManager.MagicalAttack += userAvatars.MagicalAttack;
        powerManager.MagicalDefense += userAvatars.MagicalDefense;
        powerManager.ChemicalAttack += userAvatars.ChemicalAttack;
        powerManager.ChemicalDefense += userAvatars.ChemicalDefense;
        powerManager.AtomicAttack += userAvatars.AtomicAttack;
        powerManager.AtomicDefense += userAvatars.AtomicDefense;
        powerManager.MentalAttack += userAvatars.MentalAttack;
        powerManager.MentalDefense += userAvatars.MentalDefense;
        powerManager.Speed += userAvatars.Speed;
        powerManager.CriticalDamageRate += userAvatars.CriticalDamageRate;
        powerManager.CriticalRate += userAvatars.CriticalRate;
        powerManager.PenetrationRate += userAvatars.PenetrationRate;
        powerManager.EvasionRate += userAvatars.EvasionRate;
        powerManager.DamageAbsorptionRate += userAvatars.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += userAvatars.VitalityRegenerationRate;
        powerManager.AccuracyRate += userAvatars.AccuracyRate;
        powerManager.LifestealRate += userAvatars.LifestealRate;
        powerManager.ShieldStrength += userAvatars.ShieldStrength;
        powerManager.Tenacity += userAvatars.Tenacity;
        powerManager.ResistanceRate += userAvatars.ResistanceRate;
        powerManager.ComboRate += userAvatars.ComboRate;
        powerManager.ReflectionRate += userAvatars.ReflectionRate;
        powerManager.Mana += userAvatars.Mana;
        powerManager.ManaRegenerationRate += userAvatars.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += userAvatars.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += userAvatars.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += userAvatars.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += userAvatars.ResistanceToSameFactionRate;

        // Percent Avatars
        IAvatarsRepository avatarsRepository = new AvatarsRepository();
        AvatarsService avatarsService = new AvatarsService(avatarsRepository);
        Achievements percentAvatars = avatarsService.SumPowerAvatarsPercent();

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

        powerManager.Power += galleryBorders.Power;
        powerManager.Health += galleryBorders.Health;
        powerManager.PhysicalAttack += galleryBorders.PhysicalAttack;
        powerManager.PhysicalDefense += galleryBorders.PhysicalDefense;
        powerManager.MagicalAttack += galleryBorders.MagicalAttack;
        powerManager.MagicalDefense += galleryBorders.MagicalDefense;
        powerManager.ChemicalAttack += galleryBorders.ChemicalAttack;
        powerManager.ChemicalDefense += galleryBorders.ChemicalDefense;
        powerManager.AtomicAttack += galleryBorders.AtomicAttack;
        powerManager.AtomicDefense += galleryBorders.AtomicDefense;
        powerManager.MentalAttack += galleryBorders.MentalAttack;
        powerManager.MentalDefense += galleryBorders.MentalDefense;
        powerManager.Speed += galleryBorders.Speed;
        powerManager.CriticalDamageRate += galleryBorders.CriticalDamageRate;
        powerManager.CriticalRate += galleryBorders.CriticalRate;
        powerManager.PenetrationRate += galleryBorders.PenetrationRate;
        powerManager.EvasionRate += galleryBorders.EvasionRate;
        powerManager.DamageAbsorptionRate += galleryBorders.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += galleryBorders.VitalityRegenerationRate;
        powerManager.AccuracyRate += galleryBorders.AccuracyRate;
        powerManager.LifestealRate += galleryBorders.LifestealRate;
        powerManager.ShieldStrength += galleryBorders.ShieldStrength;
        powerManager.Tenacity += galleryBorders.Tenacity;
        powerManager.ResistanceRate += galleryBorders.ResistanceRate;
        powerManager.ComboRate += galleryBorders.ComboRate;
        powerManager.ReflectionRate += galleryBorders.ReflectionRate;
        powerManager.Mana += galleryBorders.Mana;
        powerManager.ManaRegenerationRate += galleryBorders.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += galleryBorders.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += galleryBorders.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += galleryBorders.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += galleryBorders.ResistanceToSameFactionRate;

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

        powerManager.Power += userBorders.Power;
        powerManager.Health += userBorders.Health;
        powerManager.PhysicalAttack += userBorders.PhysicalAttack;
        powerManager.PhysicalDefense += userBorders.PhysicalDefense;
        powerManager.MagicalAttack += userBorders.MagicalAttack;
        powerManager.MagicalDefense += userBorders.MagicalDefense;
        powerManager.ChemicalAttack += userBorders.ChemicalAttack;
        powerManager.ChemicalDefense += userBorders.ChemicalDefense;
        powerManager.AtomicAttack += userBorders.AtomicAttack;
        powerManager.AtomicDefense += userBorders.AtomicDefense;
        powerManager.MentalAttack += userBorders.MentalAttack;
        powerManager.MentalDefense += userBorders.MentalDefense;
        powerManager.Speed += userBorders.Speed;
        powerManager.CriticalDamageRate += userBorders.CriticalDamageRate;
        powerManager.CriticalRate += userBorders.CriticalRate;
        powerManager.PenetrationRate += userBorders.PenetrationRate;
        powerManager.EvasionRate += userBorders.EvasionRate;
        powerManager.DamageAbsorptionRate += userBorders.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += userBorders.VitalityRegenerationRate;
        powerManager.AccuracyRate += userBorders.AccuracyRate;
        powerManager.LifestealRate += userBorders.LifestealRate;
        powerManager.ShieldStrength += userBorders.ShieldStrength;
        powerManager.Tenacity += userBorders.Tenacity;
        powerManager.ResistanceRate += userBorders.ResistanceRate;
        powerManager.ComboRate += userBorders.ComboRate;
        powerManager.ReflectionRate += userBorders.ReflectionRate;
        powerManager.Mana += userBorders.Mana;
        powerManager.ManaRegenerationRate += userBorders.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += userBorders.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += userBorders.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += userBorders.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += userBorders.ResistanceToSameFactionRate;

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

        powerManager.Power += books.Power;
        powerManager.Health += books.Health;
        powerManager.PhysicalAttack += books.PhysicalAttack;
        powerManager.PhysicalDefense += books.PhysicalDefense;
        powerManager.MagicalAttack += books.MagicalAttack;
        powerManager.MagicalDefense += books.MagicalDefense;
        powerManager.ChemicalAttack += books.ChemicalAttack;
        powerManager.ChemicalDefense += books.ChemicalDefense;
        powerManager.AtomicAttack += books.AtomicAttack;
        powerManager.AtomicDefense += books.AtomicDefense;
        powerManager.MentalAttack += books.MentalAttack;
        powerManager.MentalDefense += books.MentalDefense;
        powerManager.Speed += books.Speed;
        powerManager.CriticalDamageRate += books.CriticalDamageRate;
        powerManager.CriticalRate += books.CriticalRate;
        powerManager.PenetrationRate += books.PenetrationRate;
        powerManager.EvasionRate += books.EvasionRate;
        powerManager.DamageAbsorptionRate += books.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += books.VitalityRegenerationRate;
        powerManager.AccuracyRate += books.AccuracyRate;
        powerManager.LifestealRate += books.LifestealRate;
        powerManager.ShieldStrength += books.ShieldStrength;
        powerManager.Tenacity += books.Tenacity;
        powerManager.ResistanceRate += books.ResistanceRate;
        powerManager.ComboRate += books.ComboRate;
        powerManager.ReflectionRate += books.ReflectionRate;
        powerManager.Mana += books.Mana;
        powerManager.ManaRegenerationRate += books.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += books.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += books.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += books.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += books.ResistanceToSameFactionRate;

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

        powerManager.Power += cardHeroes.Power;
        powerManager.Health += cardHeroes.Health;
        powerManager.PhysicalAttack += cardHeroes.PhysicalAttack;
        powerManager.PhysicalDefense += cardHeroes.PhysicalDefense;
        powerManager.MagicalAttack += cardHeroes.MagicalAttack;
        powerManager.MagicalDefense += cardHeroes.MagicalDefense;
        powerManager.ChemicalAttack += cardHeroes.ChemicalAttack;
        powerManager.ChemicalDefense += cardHeroes.ChemicalDefense;
        powerManager.AtomicAttack += cardHeroes.AtomicAttack;
        powerManager.AtomicDefense += cardHeroes.AtomicDefense;
        powerManager.MentalAttack += cardHeroes.MentalAttack;
        powerManager.MentalDefense += cardHeroes.MentalDefense;
        powerManager.Speed += cardHeroes.Speed;
        powerManager.CriticalDamageRate += cardHeroes.CriticalDamageRate;
        powerManager.CriticalRate += cardHeroes.CriticalRate;
        powerManager.PenetrationRate += cardHeroes.PenetrationRate;
        powerManager.EvasionRate += cardHeroes.EvasionRate;
        powerManager.DamageAbsorptionRate += cardHeroes.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardHeroes.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardHeroes.AccuracyRate;
        powerManager.LifestealRate += cardHeroes.LifestealRate;
        powerManager.ShieldStrength += cardHeroes.ShieldStrength;
        powerManager.Tenacity += cardHeroes.Tenacity;
        powerManager.ResistanceRate += cardHeroes.ResistanceRate;
        powerManager.ComboRate += cardHeroes.ComboRate;
        powerManager.ReflectionRate += cardHeroes.ReflectionRate;
        powerManager.Mana += cardHeroes.Mana;
        powerManager.ManaRegenerationRate += cardHeroes.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardHeroes.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardHeroes.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardHeroes.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardHeroes.ResistanceToSameFactionRate;

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

        powerManager.Power += cardCaptains.Power;
        powerManager.Health += cardCaptains.Health;
        powerManager.PhysicalAttack += cardCaptains.PhysicalAttack;
        powerManager.PhysicalDefense += cardCaptains.PhysicalDefense;
        powerManager.MagicalAttack += cardCaptains.MagicalAttack;
        powerManager.MagicalDefense += cardCaptains.MagicalDefense;
        powerManager.ChemicalAttack += cardCaptains.ChemicalAttack;
        powerManager.ChemicalDefense += cardCaptains.ChemicalDefense;
        powerManager.AtomicAttack += cardCaptains.AtomicAttack;
        powerManager.AtomicDefense += cardCaptains.AtomicDefense;
        powerManager.MentalAttack += cardCaptains.MentalAttack;
        powerManager.MentalDefense += cardCaptains.MentalDefense;
        powerManager.Speed += cardCaptains.Speed;
        powerManager.CriticalDamageRate += cardCaptains.CriticalDamageRate;
        powerManager.CriticalRate += cardCaptains.CriticalRate;
        powerManager.PenetrationRate += cardCaptains.PenetrationRate;
        powerManager.EvasionRate += cardCaptains.EvasionRate;
        powerManager.DamageAbsorptionRate += cardCaptains.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardCaptains.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardCaptains.AccuracyRate;
        powerManager.LifestealRate += cardCaptains.LifestealRate;
        powerManager.ShieldStrength += cardCaptains.ShieldStrength;
        powerManager.Tenacity += cardCaptains.Tenacity;
        powerManager.ResistanceRate += cardCaptains.ResistanceRate;
        powerManager.ComboRate += cardCaptains.ComboRate;
        powerManager.ReflectionRate += cardCaptains.ReflectionRate;
        powerManager.Mana += cardCaptains.Mana;
        powerManager.ManaRegenerationRate += cardCaptains.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardCaptains.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardCaptains.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardCaptains.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardCaptains.ResistanceToSameFactionRate;

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

        powerManager.Power += cardColonels.Power;
        powerManager.Health += cardColonels.Health;
        powerManager.PhysicalAttack += cardColonels.PhysicalAttack;
        powerManager.PhysicalDefense += cardColonels.PhysicalDefense;
        powerManager.MagicalAttack += cardColonels.MagicalAttack;
        powerManager.MagicalDefense += cardColonels.MagicalDefense;
        powerManager.ChemicalAttack += cardColonels.ChemicalAttack;
        powerManager.ChemicalDefense += cardColonels.ChemicalDefense;
        powerManager.AtomicAttack += cardColonels.AtomicAttack;
        powerManager.AtomicDefense += cardColonels.AtomicDefense;
        powerManager.MentalAttack += cardColonels.MentalAttack;
        powerManager.MentalDefense += cardColonels.MentalDefense;
        powerManager.Speed += cardColonels.Speed;
        powerManager.CriticalDamageRate += cardColonels.CriticalDamageRate;
        powerManager.CriticalRate += cardColonels.CriticalRate;
        powerManager.PenetrationRate += cardColonels.PenetrationRate;
        powerManager.EvasionRate += cardColonels.EvasionRate;
        powerManager.DamageAbsorptionRate += cardColonels.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardColonels.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardColonels.AccuracyRate;
        powerManager.LifestealRate += cardColonels.LifestealRate;
        powerManager.ShieldStrength += cardColonels.ShieldStrength;
        powerManager.Tenacity += cardColonels.Tenacity;
        powerManager.ResistanceRate += cardColonels.ResistanceRate;
        powerManager.ComboRate += cardColonels.ComboRate;
        powerManager.ReflectionRate += cardColonels.ReflectionRate;
        powerManager.Mana += cardColonels.Mana;
        powerManager.ManaRegenerationRate += cardColonels.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardColonels.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardColonels.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardColonels.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardColonels.ResistanceToSameFactionRate;

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

        powerManager.Power += cardGenerals.Power;
        powerManager.Health += cardGenerals.Health;
        powerManager.PhysicalAttack += cardGenerals.PhysicalAttack;
        powerManager.PhysicalDefense += cardGenerals.PhysicalDefense;
        powerManager.MagicalAttack += cardGenerals.MagicalAttack;
        powerManager.MagicalDefense += cardGenerals.MagicalDefense;
        powerManager.ChemicalAttack += cardGenerals.ChemicalAttack;
        powerManager.ChemicalDefense += cardGenerals.ChemicalDefense;
        powerManager.AtomicAttack += cardGenerals.AtomicAttack;
        powerManager.AtomicDefense += cardGenerals.AtomicDefense;
        powerManager.MentalAttack += cardGenerals.MentalAttack;
        powerManager.MentalDefense += cardGenerals.MentalDefense;
        powerManager.Speed += cardGenerals.Speed;
        powerManager.CriticalDamageRate += cardGenerals.CriticalDamageRate;
        powerManager.CriticalRate += cardGenerals.CriticalRate;
        powerManager.PenetrationRate += cardGenerals.PenetrationRate;
        powerManager.EvasionRate += cardGenerals.EvasionRate;
        powerManager.DamageAbsorptionRate += cardGenerals.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardGenerals.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardGenerals.AccuracyRate;
        powerManager.LifestealRate += cardGenerals.LifestealRate;
        powerManager.ShieldStrength += cardGenerals.ShieldStrength;
        powerManager.Tenacity += cardGenerals.Tenacity;
        powerManager.ResistanceRate += cardGenerals.ResistanceRate;
        powerManager.ComboRate += cardGenerals.ComboRate;
        powerManager.ReflectionRate += cardGenerals.ReflectionRate;
        powerManager.Mana += cardGenerals.Mana;
        powerManager.ManaRegenerationRate += cardGenerals.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardGenerals.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardGenerals.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardGenerals.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardGenerals.ResistanceToSameFactionRate;

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

        powerManager.Power += cardAdmirals.Power;
        powerManager.Health += cardAdmirals.Health;
        powerManager.PhysicalAttack += cardAdmirals.PhysicalAttack;
        powerManager.PhysicalDefense += cardAdmirals.PhysicalDefense;
        powerManager.MagicalAttack += cardAdmirals.MagicalAttack;
        powerManager.MagicalDefense += cardAdmirals.MagicalDefense;
        powerManager.ChemicalAttack += cardAdmirals.ChemicalAttack;
        powerManager.ChemicalDefense += cardAdmirals.ChemicalDefense;
        powerManager.AtomicAttack += cardAdmirals.AtomicAttack;
        powerManager.AtomicDefense += cardAdmirals.AtomicDefense;
        powerManager.MentalAttack += cardAdmirals.MentalAttack;
        powerManager.MentalDefense += cardAdmirals.MentalDefense;
        powerManager.Speed += cardAdmirals.Speed;
        powerManager.CriticalDamageRate += cardAdmirals.CriticalDamageRate;
        powerManager.CriticalRate += cardAdmirals.CriticalRate;
        powerManager.PenetrationRate += cardAdmirals.PenetrationRate;
        powerManager.EvasionRate += cardAdmirals.EvasionRate;
        powerManager.DamageAbsorptionRate += cardAdmirals.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardAdmirals.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardAdmirals.AccuracyRate;
        powerManager.LifestealRate += cardAdmirals.LifestealRate;
        powerManager.ShieldStrength += cardAdmirals.ShieldStrength;
        powerManager.Tenacity += cardAdmirals.Tenacity;
        powerManager.ResistanceRate += cardAdmirals.ResistanceRate;
        powerManager.ComboRate += cardAdmirals.ComboRate;
        powerManager.ReflectionRate += cardAdmirals.ReflectionRate;
        powerManager.Mana += cardAdmirals.Mana;
        powerManager.ManaRegenerationRate += cardAdmirals.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardAdmirals.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardAdmirals.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardAdmirals.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardAdmirals.ResistanceToSameFactionRate;

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
        powerManager.Power += cardMonsters.Power;
        powerManager.Health += cardMonsters.Health;
        powerManager.PhysicalAttack += cardMonsters.PhysicalAttack;
        powerManager.PhysicalDefense += cardMonsters.PhysicalDefense;
        powerManager.MagicalAttack += cardMonsters.MagicalAttack;
        powerManager.MagicalDefense += cardMonsters.MagicalDefense;
        powerManager.ChemicalAttack += cardMonsters.ChemicalAttack;
        powerManager.ChemicalDefense += cardMonsters.ChemicalDefense;
        powerManager.AtomicAttack += cardMonsters.AtomicAttack;
        powerManager.AtomicDefense += cardMonsters.AtomicDefense;
        powerManager.MentalAttack += cardMonsters.MentalAttack;
        powerManager.MentalDefense += cardMonsters.MentalDefense;
        powerManager.Speed += cardMonsters.Speed;
        powerManager.CriticalDamageRate += cardMonsters.CriticalDamageRate;
        powerManager.CriticalRate += cardMonsters.CriticalRate;
        powerManager.PenetrationRate += cardMonsters.PenetrationRate;
        powerManager.EvasionRate += cardMonsters.EvasionRate;
        powerManager.DamageAbsorptionRate += cardMonsters.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardMonsters.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardMonsters.AccuracyRate;
        powerManager.LifestealRate += cardMonsters.LifestealRate;
        powerManager.ShieldStrength += cardMonsters.ShieldStrength;
        powerManager.Tenacity += cardMonsters.Tenacity;
        powerManager.ResistanceRate += cardMonsters.ResistanceRate;
        powerManager.ComboRate += cardMonsters.ComboRate;
        powerManager.ReflectionRate += cardMonsters.ReflectionRate;
        powerManager.Mana += cardMonsters.Mana;
        powerManager.ManaRegenerationRate += cardMonsters.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardMonsters.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardMonsters.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardMonsters.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardMonsters.ResistanceToSameFactionRate;

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
    public PowerManager GetCardMilitariesPower()
    {
        PowerManager powerManager = new PowerManager();

        // CardMilitary cardMilitary = new CardMilitary();
        ICardMilitaryGalleryRepository cardMilitaryGalleryRepository = new CardMilitaryGalleryRepository();
        CardMilitaryGalleryService cardMilitaryGalleryService = new CardMilitaryGalleryService(cardMilitaryGalleryRepository);
        //Gallery
        CardMilitaries cardMilitary = cardMilitaryGalleryService.SumPowerCardMilitaryGallery();

        powerManager.Power += cardMilitary.Power;
        powerManager.Health += cardMilitary.Health;
        powerManager.PhysicalAttack += cardMilitary.PhysicalAttack;
        powerManager.PhysicalDefense += cardMilitary.PhysicalDefense;
        powerManager.MagicalAttack += cardMilitary.MagicalAttack;
        powerManager.MagicalDefense += cardMilitary.MagicalDefense;
        powerManager.ChemicalAttack += cardMilitary.ChemicalAttack;
        powerManager.ChemicalDefense += cardMilitary.ChemicalDefense;
        powerManager.AtomicAttack += cardMilitary.AtomicAttack;
        powerManager.AtomicDefense += cardMilitary.AtomicDefense;
        powerManager.MentalAttack += cardMilitary.MentalAttack;
        powerManager.MentalDefense += cardMilitary.MentalDefense;
        powerManager.Speed += cardMilitary.Speed;
        powerManager.CriticalDamageRate += cardMilitary.CriticalDamageRate;
        powerManager.CriticalRate += cardMilitary.CriticalRate;
        powerManager.PenetrationRate += cardMilitary.PenetrationRate;
        powerManager.EvasionRate += cardMilitary.EvasionRate;
        powerManager.DamageAbsorptionRate += cardMilitary.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardMilitary.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardMilitary.AccuracyRate;
        powerManager.LifestealRate += cardMilitary.LifestealRate;
        powerManager.ShieldStrength += cardMilitary.ShieldStrength;
        powerManager.Tenacity += cardMilitary.Tenacity;
        powerManager.ResistanceRate += cardMilitary.ResistanceRate;
        powerManager.ComboRate += cardMilitary.ComboRate;
        powerManager.ReflectionRate += cardMilitary.ReflectionRate;
        powerManager.Mana += cardMilitary.Mana;
        powerManager.ManaRegenerationRate += cardMilitary.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardMilitary.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardMilitary.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardMilitary.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardMilitary.ResistanceToSameFactionRate;

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
    public PowerManager GetCardSpellsPower()
    {
        PowerManager powerManager = new PowerManager();

        // CardSpell cardSpell = new CardSpell();
        ICardSpellGalleryRepository cardSpellGalleryRepository = new CardSpellGalleryRepository();
        CardSpellGalleryService cardSpellGalleryService = new CardSpellGalleryService(cardSpellGalleryRepository);
        //Gallery
        CardSpells cardSpell = cardSpellGalleryService.SumPowerCardSpellGallery();

        powerManager.Power += cardSpell.Power;
        powerManager.Health += cardSpell.Health;
        powerManager.PhysicalAttack += cardSpell.PhysicalAttack;
        powerManager.PhysicalDefense += cardSpell.PhysicalDefense;
        powerManager.MagicalAttack += cardSpell.MagicalAttack;
        powerManager.MagicalDefense += cardSpell.MagicalDefense;
        powerManager.ChemicalAttack += cardSpell.ChemicalAttack;
        powerManager.ChemicalDefense += cardSpell.ChemicalDefense;
        powerManager.AtomicAttack += cardSpell.AtomicAttack;
        powerManager.AtomicDefense += cardSpell.AtomicDefense;
        powerManager.MentalAttack += cardSpell.MentalAttack;
        powerManager.MentalDefense += cardSpell.MentalDefense;
        powerManager.Speed += cardSpell.Speed;
        powerManager.CriticalDamageRate += cardSpell.CriticalDamageRate;
        powerManager.CriticalRate += cardSpell.CriticalRate;
        powerManager.PenetrationRate += cardSpell.PenetrationRate;
        powerManager.EvasionRate += cardSpell.EvasionRate;
        powerManager.DamageAbsorptionRate += cardSpell.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardSpell.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardSpell.AccuracyRate;
        powerManager.LifestealRate += cardSpell.LifestealRate;
        powerManager.ShieldStrength += cardSpell.ShieldStrength;
        powerManager.Tenacity += cardSpell.Tenacity;
        powerManager.ResistanceRate += cardSpell.ResistanceRate;
        powerManager.ComboRate += cardSpell.ComboRate;
        powerManager.ReflectionRate += cardSpell.ReflectionRate;
        powerManager.Mana += cardSpell.Mana;
        powerManager.ManaRegenerationRate += cardSpell.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardSpell.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardSpell.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardSpell.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardSpell.ResistanceToSameFactionRate;

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
        powerManager.Power += collaboration.Power;
        powerManager.Health += collaboration.Health;
        powerManager.PhysicalAttack += collaboration.PhysicalAttack;
        powerManager.PhysicalDefense += collaboration.PhysicalDefense;
        powerManager.MagicalAttack += collaboration.MagicalAttack;
        powerManager.MagicalDefense += collaboration.MagicalDefense;
        powerManager.ChemicalAttack += collaboration.ChemicalAttack;
        powerManager.ChemicalDefense += collaboration.ChemicalDefense;
        powerManager.AtomicAttack += collaboration.AtomicAttack;
        powerManager.AtomicDefense += collaboration.AtomicDefense;
        powerManager.MentalAttack += collaboration.MentalAttack;
        powerManager.MentalDefense += collaboration.MentalDefense;
        powerManager.Speed += collaboration.Speed;
        powerManager.CriticalDamageRate += collaboration.CriticalDamageRate;
        powerManager.CriticalRate += collaboration.CriticalRate;
        powerManager.PenetrationRate += collaboration.PenetrationRate;
        powerManager.EvasionRate += collaboration.EvasionRate;
        powerManager.DamageAbsorptionRate += collaboration.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += collaboration.VitalityRegenerationRate;
        powerManager.AccuracyRate += collaboration.AccuracyRate;
        powerManager.LifestealRate += collaboration.LifestealRate;
        powerManager.ShieldStrength += collaboration.ShieldStrength;
        powerManager.Tenacity += collaboration.Tenacity;
        powerManager.ResistanceRate += collaboration.ResistanceRate;
        powerManager.ComboRate += collaboration.ComboRate;
        powerManager.ReflectionRate += collaboration.ReflectionRate;
        powerManager.Mana += collaboration.Mana;
        powerManager.ManaRegenerationRate += collaboration.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += collaboration.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += collaboration.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += collaboration.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += collaboration.ResistanceToSameFactionRate;

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
        powerManager.Power += collaboration.Power;
        powerManager.Health += collaboration.Health;
        powerManager.PhysicalAttack += collaboration.PhysicalAttack;
        powerManager.PhysicalDefense += collaboration.PhysicalDefense;
        powerManager.MagicalAttack += collaboration.MagicalAttack;
        powerManager.MagicalDefense += collaboration.MagicalDefense;
        powerManager.ChemicalAttack += collaboration.ChemicalAttack;
        powerManager.ChemicalDefense += collaboration.ChemicalDefense;
        powerManager.AtomicAttack += collaboration.AtomicAttack;
        powerManager.AtomicDefense += collaboration.AtomicDefense;
        powerManager.MentalAttack += collaboration.MentalAttack;
        powerManager.MentalDefense += collaboration.MentalDefense;
        powerManager.Speed += collaboration.Speed;
        powerManager.CriticalDamageRate += collaboration.CriticalDamageRate;
        powerManager.CriticalRate += collaboration.CriticalRate;
        powerManager.PenetrationRate += collaboration.PenetrationRate;
        powerManager.EvasionRate += collaboration.EvasionRate;
        powerManager.DamageAbsorptionRate += collaboration.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += collaboration.VitalityRegenerationRate;
        powerManager.AccuracyRate += collaboration.AccuracyRate;
        powerManager.LifestealRate += collaboration.LifestealRate;
        powerManager.ShieldStrength += collaboration.ShieldStrength;
        powerManager.Tenacity += collaboration.Tenacity;
        powerManager.ResistanceRate += collaboration.ResistanceRate;
        powerManager.ComboRate += collaboration.ComboRate;
        powerManager.ReflectionRate += collaboration.ReflectionRate;
        powerManager.Mana += collaboration.Mana;
        powerManager.ManaRegenerationRate += collaboration.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += collaboration.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += collaboration.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += collaboration.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += collaboration.ResistanceToSameFactionRate;

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
        powerManager.Power += collaborationEquipment.Power;
        powerManager.Health += collaborationEquipment.Health;
        powerManager.PhysicalAttack += collaborationEquipment.PhysicalAttack;
        powerManager.PhysicalDefense += collaborationEquipment.PhysicalDefense;
        powerManager.MagicalAttack += collaborationEquipment.MagicalAttack;
        powerManager.MagicalDefense += collaborationEquipment.MagicalDefense;
        powerManager.ChemicalAttack += collaborationEquipment.ChemicalAttack;
        powerManager.ChemicalDefense += collaborationEquipment.ChemicalDefense;
        powerManager.AtomicAttack += collaborationEquipment.AtomicAttack;
        powerManager.AtomicDefense += collaborationEquipment.AtomicDefense;
        powerManager.MentalAttack += collaborationEquipment.MentalAttack;
        powerManager.MentalDefense += collaborationEquipment.MentalDefense;
        powerManager.Speed += collaborationEquipment.Speed;
        powerManager.CriticalDamageRate += collaborationEquipment.CriticalDamageRate;
        powerManager.CriticalRate += collaborationEquipment.CriticalRate;
        powerManager.PenetrationRate += collaborationEquipment.PenetrationRate;
        powerManager.EvasionRate += collaborationEquipment.EvasionRate;
        powerManager.DamageAbsorptionRate += collaborationEquipment.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += collaborationEquipment.VitalityRegenerationRate;
        powerManager.AccuracyRate += collaborationEquipment.AccuracyRate;
        powerManager.LifestealRate += collaborationEquipment.LifestealRate;
        powerManager.ShieldStrength += collaborationEquipment.ShieldStrength;
        powerManager.Tenacity += collaborationEquipment.Tenacity;
        powerManager.ResistanceRate += collaborationEquipment.ResistanceRate;
        powerManager.ComboRate += collaborationEquipment.ComboRate;
        powerManager.ReflectionRate += collaborationEquipment.ReflectionRate;
        powerManager.Mana += collaborationEquipment.Mana;
        powerManager.ManaRegenerationRate += collaborationEquipment.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += collaborationEquipment.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += collaborationEquipment.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += collaborationEquipment.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += collaborationEquipment.ResistanceToSameFactionRate;

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
        powerManager.Power += collaborationEquipment.Power;
        powerManager.Health += collaborationEquipment.Health;
        powerManager.PhysicalAttack += collaborationEquipment.PhysicalAttack;
        powerManager.PhysicalDefense += collaborationEquipment.PhysicalDefense;
        powerManager.MagicalAttack += collaborationEquipment.MagicalAttack;
        powerManager.MagicalDefense += collaborationEquipment.MagicalDefense;
        powerManager.ChemicalAttack += collaborationEquipment.ChemicalAttack;
        powerManager.ChemicalDefense += collaborationEquipment.ChemicalDefense;
        powerManager.AtomicAttack += collaborationEquipment.AtomicAttack;
        powerManager.AtomicDefense += collaborationEquipment.AtomicDefense;
        powerManager.MentalAttack += collaborationEquipment.MentalAttack;
        powerManager.MentalDefense += collaborationEquipment.MentalDefense;
        powerManager.Speed += collaborationEquipment.Speed;
        powerManager.CriticalDamageRate += collaborationEquipment.CriticalDamageRate;
        powerManager.CriticalRate += collaborationEquipment.CriticalRate;
        powerManager.PenetrationRate += collaborationEquipment.PenetrationRate;
        powerManager.EvasionRate += collaborationEquipment.EvasionRate;
        powerManager.DamageAbsorptionRate += collaborationEquipment.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += collaborationEquipment.VitalityRegenerationRate;
        powerManager.AccuracyRate += collaborationEquipment.AccuracyRate;
        powerManager.LifestealRate += collaborationEquipment.LifestealRate;
        powerManager.ShieldStrength += collaborationEquipment.ShieldStrength;
        powerManager.Tenacity += collaborationEquipment.Tenacity;
        powerManager.ResistanceRate += collaborationEquipment.ResistanceRate;
        powerManager.ComboRate += collaborationEquipment.ComboRate;
        powerManager.ReflectionRate += collaborationEquipment.ReflectionRate;
        powerManager.Mana += collaborationEquipment.Mana;
        powerManager.ManaRegenerationRate += collaborationEquipment.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += collaborationEquipment.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += collaborationEquipment.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += collaborationEquipment.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += collaborationEquipment.ResistanceToSameFactionRate;

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

        powerManager.Power += equipments.Power;
        powerManager.Health += equipments.Health;
        powerManager.PhysicalAttack += equipments.PhysicalAttack;
        powerManager.PhysicalDefense += equipments.PhysicalDefense;
        powerManager.MagicalAttack += equipments.MagicalAttack;
        powerManager.MagicalDefense += equipments.MagicalDefense;
        powerManager.ChemicalAttack += equipments.ChemicalAttack;
        powerManager.ChemicalDefense += equipments.ChemicalDefense;
        powerManager.AtomicAttack += equipments.AtomicAttack;
        powerManager.AtomicDefense += equipments.AtomicDefense;
        powerManager.MentalAttack += equipments.MentalAttack;
        powerManager.MentalDefense += equipments.MentalDefense;
        powerManager.Speed += equipments.Speed;
        powerManager.CriticalDamageRate += equipments.CriticalDamageRate;
        powerManager.CriticalRate += equipments.CriticalRate;
        powerManager.PenetrationRate += equipments.PenetrationRate;
        powerManager.EvasionRate += equipments.EvasionRate;
        powerManager.DamageAbsorptionRate += equipments.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += equipments.VitalityRegenerationRate;
        powerManager.AccuracyRate += equipments.AccuracyRate;
        powerManager.LifestealRate += equipments.LifestealRate;
        powerManager.ShieldStrength += equipments.ShieldStrength;
        powerManager.Tenacity += equipments.Tenacity;
        powerManager.ResistanceRate += equipments.ResistanceRate;
        powerManager.ComboRate += equipments.ComboRate;
        powerManager.ReflectionRate += equipments.ReflectionRate;
        powerManager.Mana += equipments.Mana;
        powerManager.ManaRegenerationRate += equipments.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += equipments.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += equipments.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += equipments.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += equipments.ResistanceToSameFactionRate;

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
    public PowerManager GetMagicFormationCirclesPower()
    {
        PowerManager powerManager = new PowerManager();
        // MagicFormationCircle magicFormationCircle = new MagicFormationCircle();
        IMagicFormationCircleGalleryRepository magicFormationCircleGalleryRepository = new MagicFormationCircleGalleryRepository();
        MagicFormationCircleGalleryService magicFormationCircleGalleryService = new MagicFormationCircleGalleryService(magicFormationCircleGalleryRepository);
        // Gallery
        MagicFormationCircles magicFormationCircle = magicFormationCircleGalleryService.SumPowerMagicFormationCircleGallery();
        powerManager.Power += magicFormationCircle.Power;
        powerManager.Health += magicFormationCircle.Health;
        powerManager.PhysicalAttack += magicFormationCircle.PhysicalAttack;
        powerManager.PhysicalDefense += magicFormationCircle.PhysicalDefense;
        powerManager.MagicalAttack += magicFormationCircle.MagicalAttack;
        powerManager.MagicalDefense += magicFormationCircle.MagicalDefense;
        powerManager.ChemicalAttack += magicFormationCircle.ChemicalAttack;
        powerManager.ChemicalDefense += magicFormationCircle.ChemicalDefense;
        powerManager.AtomicAttack += magicFormationCircle.AtomicAttack;
        powerManager.AtomicDefense += magicFormationCircle.AtomicDefense;
        powerManager.MentalAttack += magicFormationCircle.MentalAttack;
        powerManager.MentalDefense += magicFormationCircle.MentalDefense;
        powerManager.Speed += magicFormationCircle.Speed;
        powerManager.CriticalDamageRate += magicFormationCircle.CriticalDamageRate;
        powerManager.CriticalRate += magicFormationCircle.CriticalRate;
        powerManager.PenetrationRate += magicFormationCircle.PenetrationRate;
        powerManager.EvasionRate += magicFormationCircle.EvasionRate;
        powerManager.DamageAbsorptionRate += magicFormationCircle.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += magicFormationCircle.VitalityRegenerationRate;
        powerManager.AccuracyRate += magicFormationCircle.AccuracyRate;
        powerManager.LifestealRate += magicFormationCircle.LifestealRate;
        powerManager.ShieldStrength += magicFormationCircle.ShieldStrength;
        powerManager.Tenacity += magicFormationCircle.Tenacity;
        powerManager.ResistanceRate += magicFormationCircle.ResistanceRate;
        powerManager.ComboRate += magicFormationCircle.ComboRate;
        powerManager.ReflectionRate += magicFormationCircle.ReflectionRate;
        powerManager.Mana += magicFormationCircle.Mana;
        powerManager.ManaRegenerationRate += magicFormationCircle.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += magicFormationCircle.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += magicFormationCircle.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += magicFormationCircle.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += magicFormationCircle.ResistanceToSameFactionRate;

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
        powerManager.Power += magicFormationCircle.Power;
        powerManager.Health += magicFormationCircle.Health;
        powerManager.PhysicalAttack += magicFormationCircle.PhysicalAttack;
        powerManager.PhysicalDefense += magicFormationCircle.PhysicalDefense;
        powerManager.MagicalAttack += magicFormationCircle.MagicalAttack;
        powerManager.MagicalDefense += magicFormationCircle.MagicalDefense;
        powerManager.ChemicalAttack += magicFormationCircle.ChemicalAttack;
        powerManager.ChemicalDefense += magicFormationCircle.ChemicalDefense;
        powerManager.AtomicAttack += magicFormationCircle.AtomicAttack;
        powerManager.AtomicDefense += magicFormationCircle.AtomicDefense;
        powerManager.MentalAttack += magicFormationCircle.MentalAttack;
        powerManager.MentalDefense += magicFormationCircle.MentalDefense;
        powerManager.Speed += magicFormationCircle.Speed;
        powerManager.CriticalDamageRate += magicFormationCircle.CriticalDamageRate;
        powerManager.CriticalRate += magicFormationCircle.CriticalRate;
        powerManager.PenetrationRate += magicFormationCircle.PenetrationRate;
        powerManager.EvasionRate += magicFormationCircle.EvasionRate;
        powerManager.DamageAbsorptionRate += magicFormationCircle.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += magicFormationCircle.VitalityRegenerationRate;
        powerManager.AccuracyRate += magicFormationCircle.AccuracyRate;
        powerManager.LifestealRate += magicFormationCircle.LifestealRate;
        powerManager.ShieldStrength += magicFormationCircle.ShieldStrength;
        powerManager.Tenacity += magicFormationCircle.Tenacity;
        powerManager.ResistanceRate += magicFormationCircle.ResistanceRate;
        powerManager.ComboRate += magicFormationCircle.ComboRate;
        powerManager.ReflectionRate += magicFormationCircle.ReflectionRate;
        powerManager.Mana += magicFormationCircle.Mana;
        powerManager.ManaRegenerationRate += magicFormationCircle.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += magicFormationCircle.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += magicFormationCircle.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += magicFormationCircle.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += magicFormationCircle.ResistanceToSameFactionRate;

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
        powerManager.Power += relics.Power;
        powerManager.Health += relics.Health;
        powerManager.PhysicalAttack += relics.PhysicalAttack;
        powerManager.PhysicalDefense += relics.PhysicalDefense;
        powerManager.MagicalAttack += relics.MagicalAttack;
        powerManager.MagicalDefense += relics.MagicalDefense;
        powerManager.ChemicalAttack += relics.ChemicalAttack;
        powerManager.ChemicalDefense += relics.ChemicalDefense;
        powerManager.AtomicAttack += relics.AtomicAttack;
        powerManager.AtomicDefense += relics.AtomicDefense;
        powerManager.MentalAttack += relics.MentalAttack;
        powerManager.MentalDefense += relics.MentalDefense;
        powerManager.Speed += relics.Speed;
        powerManager.CriticalDamageRate += relics.CriticalDamageRate;
        powerManager.CriticalRate += relics.CriticalRate;
        powerManager.PenetrationRate += relics.PenetrationRate;
        powerManager.EvasionRate += relics.EvasionRate;
        powerManager.DamageAbsorptionRate += relics.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += relics.VitalityRegenerationRate;
        powerManager.AccuracyRate += relics.AccuracyRate;
        powerManager.LifestealRate += relics.LifestealRate;
        powerManager.ShieldStrength += relics.ShieldStrength;
        powerManager.Tenacity += relics.Tenacity;
        powerManager.ResistanceRate += relics.ResistanceRate;
        powerManager.ComboRate += relics.ComboRate;
        powerManager.ReflectionRate += relics.ReflectionRate;
        powerManager.Mana += relics.Mana;
        powerManager.ManaRegenerationRate += relics.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += relics.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += relics.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += relics.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += relics.ResistanceToSameFactionRate;

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
        powerManager.Power += relics.Power;
        powerManager.Health += relics.Health;
        powerManager.PhysicalAttack += relics.PhysicalAttack;
        powerManager.PhysicalDefense += relics.PhysicalDefense;
        powerManager.MagicalAttack += relics.MagicalAttack;
        powerManager.MagicalDefense += relics.MagicalDefense;
        powerManager.ChemicalAttack += relics.ChemicalAttack;
        powerManager.ChemicalDefense += relics.ChemicalDefense;
        powerManager.AtomicAttack += relics.AtomicAttack;
        powerManager.AtomicDefense += relics.AtomicDefense;
        powerManager.MentalAttack += relics.MentalAttack;
        powerManager.MentalDefense += relics.MentalDefense;
        powerManager.Speed += relics.Speed;
        powerManager.CriticalDamageRate += relics.CriticalDamageRate;
        powerManager.CriticalRate += relics.CriticalRate;
        powerManager.PenetrationRate += relics.PenetrationRate;
        powerManager.EvasionRate += relics.EvasionRate;
        powerManager.DamageAbsorptionRate += relics.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += relics.VitalityRegenerationRate;
        powerManager.AccuracyRate += relics.AccuracyRate;
        powerManager.LifestealRate += relics.LifestealRate;
        powerManager.ShieldStrength += relics.ShieldStrength;
        powerManager.Tenacity += relics.Tenacity;
        powerManager.ResistanceRate += relics.ResistanceRate;
        powerManager.ComboRate += relics.ComboRate;
        powerManager.ReflectionRate += relics.ReflectionRate;
        powerManager.Mana += relics.Mana;
        powerManager.ManaRegenerationRate += relics.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += relics.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += relics.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += relics.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += relics.ResistanceToSameFactionRate;

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
        powerManager.Power += medals.Power;
        powerManager.Health += medals.Health;
        powerManager.PhysicalAttack += medals.PhysicalAttack;
        powerManager.PhysicalDefense += medals.PhysicalDefense;
        powerManager.MagicalAttack += medals.MagicalAttack;
        powerManager.MagicalDefense += medals.MagicalDefense;
        powerManager.ChemicalAttack += medals.ChemicalAttack;
        powerManager.ChemicalDefense += medals.ChemicalDefense;
        powerManager.AtomicAttack += medals.AtomicAttack;
        powerManager.AtomicDefense += medals.AtomicDefense;
        powerManager.MentalAttack += medals.MentalAttack;
        powerManager.MentalDefense += medals.MentalDefense;
        powerManager.Speed += medals.Speed;
        powerManager.CriticalDamageRate += medals.CriticalDamageRate;
        powerManager.CriticalRate += medals.CriticalRate;
        powerManager.PenetrationRate += medals.PenetrationRate;
        powerManager.EvasionRate += medals.EvasionRate;
        powerManager.DamageAbsorptionRate += medals.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += medals.VitalityRegenerationRate;
        powerManager.AccuracyRate += medals.AccuracyRate;
        powerManager.LifestealRate += medals.LifestealRate;
        powerManager.ShieldStrength += medals.ShieldStrength;
        powerManager.Tenacity += medals.Tenacity;
        powerManager.ResistanceRate += medals.ResistanceRate;
        powerManager.ComboRate += medals.ComboRate;
        powerManager.ReflectionRate += medals.ReflectionRate;
        powerManager.Mana += medals.Mana;
        powerManager.ManaRegenerationRate += medals.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += medals.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += medals.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += medals.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += medals.ResistanceToSameFactionRate;

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
        powerManager.Power += medals.Power;
        powerManager.Health += medals.Health;
        powerManager.PhysicalAttack += medals.PhysicalAttack;
        powerManager.PhysicalDefense += medals.PhysicalDefense;
        powerManager.MagicalAttack += medals.MagicalAttack;
        powerManager.MagicalDefense += medals.MagicalDefense;
        powerManager.ChemicalAttack += medals.ChemicalAttack;
        powerManager.ChemicalDefense += medals.ChemicalDefense;
        powerManager.AtomicAttack += medals.AtomicAttack;
        powerManager.AtomicDefense += medals.AtomicDefense;
        powerManager.MentalAttack += medals.MentalAttack;
        powerManager.MentalDefense += medals.MentalDefense;
        powerManager.Speed += medals.Speed;
        powerManager.CriticalDamageRate += medals.CriticalDamageRate;
        powerManager.CriticalRate += medals.CriticalRate;
        powerManager.PenetrationRate += medals.PenetrationRate;
        powerManager.EvasionRate += medals.EvasionRate;
        powerManager.DamageAbsorptionRate += medals.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += medals.VitalityRegenerationRate;
        powerManager.AccuracyRate += medals.AccuracyRate;
        powerManager.LifestealRate += medals.LifestealRate;
        powerManager.ShieldStrength += medals.ShieldStrength;
        powerManager.Tenacity += medals.Tenacity;
        powerManager.ResistanceRate += medals.ResistanceRate;
        powerManager.ComboRate += medals.ComboRate;
        powerManager.ReflectionRate += medals.ReflectionRate;
        powerManager.Mana += medals.Mana;
        powerManager.ManaRegenerationRate += medals.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += medals.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += medals.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += medals.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += medals.ResistanceToSameFactionRate;

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
        powerManager.Power += pets.Power;
        powerManager.Health += pets.Health;
        powerManager.PhysicalAttack += pets.PhysicalAttack;
        powerManager.PhysicalDefense += pets.PhysicalDefense;
        powerManager.MagicalAttack += pets.MagicalAttack;
        powerManager.MagicalDefense += pets.MagicalDefense;
        powerManager.ChemicalAttack += pets.ChemicalAttack;
        powerManager.ChemicalDefense += pets.ChemicalDefense;
        powerManager.AtomicAttack += pets.AtomicAttack;
        powerManager.AtomicDefense += pets.AtomicDefense;
        powerManager.MentalAttack += pets.MentalAttack;
        powerManager.MentalDefense += pets.MentalDefense;
        powerManager.Speed += pets.Speed;
        powerManager.CriticalDamageRate += pets.CriticalDamageRate;
        powerManager.CriticalRate += pets.CriticalRate;
        powerManager.PenetrationRate += pets.PenetrationRate;
        powerManager.EvasionRate += pets.EvasionRate;
        powerManager.DamageAbsorptionRate += pets.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += pets.VitalityRegenerationRate;
        powerManager.AccuracyRate += pets.AccuracyRate;
        powerManager.LifestealRate += pets.LifestealRate;
        powerManager.ShieldStrength += pets.ShieldStrength;
        powerManager.Tenacity += pets.Tenacity;
        powerManager.ResistanceRate += pets.ResistanceRate;
        powerManager.ComboRate += pets.ComboRate;
        powerManager.ReflectionRate += pets.ReflectionRate;
        powerManager.Mana += pets.Mana;
        powerManager.ManaRegenerationRate += pets.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += pets.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += pets.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += pets.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += pets.ResistanceToSameFactionRate;

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
        powerManager.Power += symbols.Power;
        powerManager.Health += symbols.Health;
        powerManager.PhysicalAttack += symbols.PhysicalAttack;
        powerManager.PhysicalDefense += symbols.PhysicalDefense;
        powerManager.MagicalAttack += symbols.MagicalAttack;
        powerManager.MagicalDefense += symbols.MagicalDefense;
        powerManager.ChemicalAttack += symbols.ChemicalAttack;
        powerManager.ChemicalDefense += symbols.ChemicalDefense;
        powerManager.AtomicAttack += symbols.AtomicAttack;
        powerManager.AtomicDefense += symbols.AtomicDefense;
        powerManager.MentalAttack += symbols.MentalAttack;
        powerManager.MentalDefense += symbols.MentalDefense;
        powerManager.Speed += symbols.Speed;
        powerManager.CriticalDamageRate += symbols.CriticalDamageRate;
        powerManager.CriticalRate += symbols.CriticalRate;
        powerManager.PenetrationRate += symbols.PenetrationRate;
        powerManager.EvasionRate += symbols.EvasionRate;
        powerManager.DamageAbsorptionRate += symbols.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += symbols.VitalityRegenerationRate;
        powerManager.AccuracyRate += symbols.AccuracyRate;
        powerManager.LifestealRate += symbols.LifestealRate;
        powerManager.ShieldStrength += symbols.ShieldStrength;
        powerManager.Tenacity += symbols.Tenacity;
        powerManager.ResistanceRate += symbols.ResistanceRate;
        powerManager.ComboRate += symbols.ComboRate;
        powerManager.ReflectionRate += symbols.ReflectionRate;
        powerManager.Mana += symbols.Mana;
        powerManager.ManaRegenerationRate += symbols.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += symbols.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += symbols.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += symbols.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += symbols.ResistanceToSameFactionRate;

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
        powerManager.Power += symbols.Power;
        powerManager.Health += symbols.Health;
        powerManager.PhysicalAttack += symbols.PhysicalAttack;
        powerManager.PhysicalDefense += symbols.PhysicalDefense;
        powerManager.MagicalAttack += symbols.MagicalAttack;
        powerManager.MagicalDefense += symbols.MagicalDefense;
        powerManager.ChemicalAttack += symbols.ChemicalAttack;
        powerManager.ChemicalDefense += symbols.ChemicalDefense;
        powerManager.AtomicAttack += symbols.AtomicAttack;
        powerManager.AtomicDefense += symbols.AtomicDefense;
        powerManager.MentalAttack += symbols.MentalAttack;
        powerManager.MentalDefense += symbols.MentalDefense;
        powerManager.Speed += symbols.Speed;
        powerManager.CriticalDamageRate += symbols.CriticalDamageRate;
        powerManager.CriticalRate += symbols.CriticalRate;
        powerManager.PenetrationRate += symbols.PenetrationRate;
        powerManager.EvasionRate += symbols.EvasionRate;
        powerManager.DamageAbsorptionRate += symbols.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += symbols.VitalityRegenerationRate;
        powerManager.AccuracyRate += symbols.AccuracyRate;
        powerManager.LifestealRate += symbols.LifestealRate;
        powerManager.ShieldStrength += symbols.ShieldStrength;
        powerManager.Tenacity += symbols.Tenacity;
        powerManager.ResistanceRate += symbols.ResistanceRate;
        powerManager.ComboRate += symbols.ComboRate;
        powerManager.ReflectionRate += symbols.ReflectionRate;
        powerManager.Mana += symbols.Mana;
        powerManager.ManaRegenerationRate += symbols.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += symbols.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += symbols.ResistanceToDifferentFactionRate;
        powerManager.ResistanceToSameFactionRate += symbols.ResistanceToSameFactionRate;
        powerManager.DamageToSameFactionRate += symbols.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += symbols.ResistanceToSameFactionRate;

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
        powerManager.Power += skills.Power;
        powerManager.Health += skills.Health;
        powerManager.PhysicalAttack += skills.PhysicalAttack;
        powerManager.PhysicalDefense += skills.PhysicalDefense;
        powerManager.MagicalAttack += skills.MagicalAttack;
        powerManager.MagicalDefense += skills.MagicalDefense;
        powerManager.ChemicalAttack += skills.ChemicalAttack;
        powerManager.ChemicalDefense += skills.ChemicalDefense;
        powerManager.AtomicAttack += skills.AtomicAttack;
        powerManager.AtomicDefense += skills.AtomicDefense;
        powerManager.MentalAttack += skills.MentalAttack;
        powerManager.MentalDefense += skills.MentalDefense;
        powerManager.Speed += skills.Speed;
        powerManager.CriticalDamageRate += skills.CriticalDamageRate;
        powerManager.CriticalRate += skills.CriticalRate;
        powerManager.PenetrationRate += skills.PenetrationRate;
        powerManager.EvasionRate += skills.EvasionRate;
        powerManager.DamageAbsorptionRate += skills.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += skills.VitalityRegenerationRate;
        powerManager.AccuracyRate += skills.AccuracyRate;
        powerManager.LifestealRate += skills.LifestealRate;
        powerManager.ShieldStrength += skills.ShieldStrength;
        powerManager.Tenacity += skills.Tenacity;
        powerManager.ResistanceRate += skills.ResistanceRate;
        powerManager.ComboRate += skills.ComboRate;
        powerManager.ReflectionRate += skills.ReflectionRate;
        powerManager.Mana += skills.Mana;
        powerManager.ManaRegenerationRate += skills.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += skills.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += skills.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += skills.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += skills.ResistanceToSameFactionRate;

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
        powerManager.Power += titles.Power;
        powerManager.Health += titles.Health;
        powerManager.PhysicalAttack += titles.PhysicalAttack;
        powerManager.PhysicalDefense += titles.PhysicalDefense;
        powerManager.MagicalAttack += titles.MagicalAttack;
        powerManager.MagicalDefense += titles.MagicalDefense;
        powerManager.ChemicalAttack += titles.ChemicalAttack;
        powerManager.ChemicalDefense += titles.ChemicalDefense;
        powerManager.AtomicAttack += titles.AtomicAttack;
        powerManager.AtomicDefense += titles.AtomicDefense;
        powerManager.MentalAttack += titles.MentalAttack;
        powerManager.MentalDefense += titles.MentalDefense;
        powerManager.Speed += titles.Speed;
        powerManager.CriticalDamageRate += titles.CriticalDamageRate;
        powerManager.CriticalRate += titles.CriticalRate;
        powerManager.PenetrationRate += titles.PenetrationRate;
        powerManager.EvasionRate += titles.EvasionRate;
        powerManager.DamageAbsorptionRate += titles.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += titles.VitalityRegenerationRate;
        powerManager.AccuracyRate += titles.AccuracyRate;
        powerManager.LifestealRate += titles.LifestealRate;
        powerManager.ShieldStrength += titles.ShieldStrength;
        powerManager.Tenacity += titles.Tenacity;
        powerManager.ResistanceRate += titles.ResistanceRate;
        powerManager.ComboRate += titles.ComboRate;
        powerManager.ReflectionRate += titles.ReflectionRate;
        powerManager.Mana += titles.Mana;
        powerManager.ManaRegenerationRate += titles.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += titles.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += titles.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += titles.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += titles.ResistanceToSameFactionRate;

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
        powerManager.Power += titles.Power;
        powerManager.Health += titles.Health;
        powerManager.PhysicalAttack += titles.PhysicalAttack;
        powerManager.PhysicalDefense += titles.PhysicalDefense;
        powerManager.MagicalAttack += titles.MagicalAttack;
        powerManager.MagicalDefense += titles.MagicalDefense;
        powerManager.ChemicalAttack += titles.ChemicalAttack;
        powerManager.ChemicalDefense += titles.ChemicalDefense;
        powerManager.AtomicAttack += titles.AtomicAttack;
        powerManager.AtomicDefense += titles.AtomicDefense;
        powerManager.MentalAttack += titles.MentalAttack;
        powerManager.MentalDefense += titles.MentalDefense;
        powerManager.Speed += titles.Speed;
        powerManager.CriticalDamageRate += titles.CriticalDamageRate;
        powerManager.CriticalRate += titles.CriticalRate;
        powerManager.PenetrationRate += titles.PenetrationRate;
        powerManager.EvasionRate += titles.EvasionRate;
        powerManager.DamageAbsorptionRate += titles.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += titles.VitalityRegenerationRate;
        powerManager.AccuracyRate += titles.AccuracyRate;
        powerManager.LifestealRate += titles.LifestealRate;
        powerManager.ShieldStrength += titles.ShieldStrength;
        powerManager.Tenacity += titles.Tenacity;
        powerManager.ResistanceRate += titles.ResistanceRate;
        powerManager.ComboRate += titles.ComboRate;
        powerManager.ReflectionRate += titles.ReflectionRate;
        powerManager.Mana += titles.Mana;
        powerManager.ManaRegenerationRate += titles.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += titles.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += titles.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += titles.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += titles.ResistanceToSameFactionRate;

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
    public PowerManager GetTalismansPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Talisman talisman = new Talisman();
        ITalismanGalleryRepository talismanGalleryRepository = new TalismanGalleryRepository();
        TalismanGalleryService talismanGalleryService = new TalismanGalleryService(talismanGalleryRepository);
        // Gallery
        Talismans talisman = talismanGalleryService.SumPowerTalismanGallery();
        powerManager.Power += talisman.Power;
        powerManager.Health += talisman.Health;
        powerManager.PhysicalAttack += talisman.PhysicalAttack;
        powerManager.PhysicalDefense += talisman.PhysicalDefense;
        powerManager.MagicalAttack += talisman.MagicalAttack;
        powerManager.MagicalDefense += talisman.MagicalDefense;
        powerManager.ChemicalAttack += talisman.ChemicalAttack;
        powerManager.ChemicalDefense += talisman.ChemicalDefense;
        powerManager.AtomicAttack += talisman.AtomicAttack;
        powerManager.AtomicDefense += talisman.AtomicDefense;
        powerManager.MentalAttack += talisman.MentalAttack;
        powerManager.MentalDefense += talisman.MentalDefense;
        powerManager.Speed += talisman.Speed;
        powerManager.CriticalDamageRate += talisman.CriticalDamageRate;
        powerManager.CriticalRate += talisman.CriticalRate;
        powerManager.PenetrationRate += talisman.PenetrationRate;
        powerManager.EvasionRate += talisman.EvasionRate;
        powerManager.DamageAbsorptionRate += talisman.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += talisman.VitalityRegenerationRate;
        powerManager.AccuracyRate += talisman.AccuracyRate;
        powerManager.LifestealRate += talisman.LifestealRate;
        powerManager.ShieldStrength += talisman.ShieldStrength;
        powerManager.Tenacity += talisman.Tenacity;
        powerManager.ResistanceRate += talisman.ResistanceRate;
        powerManager.ComboRate += talisman.ComboRate;
        powerManager.ReflectionRate += talisman.ReflectionRate;
        powerManager.Mana += talisman.Mana;
        powerManager.ManaRegenerationRate += talisman.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += talisman.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += talisman.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += talisman.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += talisman.ResistanceToSameFactionRate;

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
        powerManager.Power += talisman.Power;
        powerManager.Health += talisman.Health;
        powerManager.PhysicalAttack += talisman.PhysicalAttack;
        powerManager.PhysicalDefense += talisman.PhysicalDefense;
        powerManager.MagicalAttack += talisman.MagicalAttack;
        powerManager.MagicalDefense += talisman.MagicalDefense;
        powerManager.ChemicalAttack += talisman.ChemicalAttack;
        powerManager.ChemicalDefense += talisman.ChemicalDefense;
        powerManager.AtomicAttack += talisman.AtomicAttack;
        powerManager.AtomicDefense += talisman.AtomicDefense;
        powerManager.MentalAttack += talisman.MentalAttack;
        powerManager.MentalDefense += talisman.MentalDefense;
        powerManager.Speed += talisman.Speed;
        powerManager.CriticalDamageRate += talisman.CriticalDamageRate;
        powerManager.CriticalRate += talisman.CriticalRate;
        powerManager.PenetrationRate += talisman.PenetrationRate;
        powerManager.EvasionRate += talisman.EvasionRate;
        powerManager.DamageAbsorptionRate += talisman.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += talisman.VitalityRegenerationRate;
        powerManager.AccuracyRate += talisman.AccuracyRate;
        powerManager.LifestealRate += talisman.LifestealRate;
        powerManager.ShieldStrength += talisman.ShieldStrength;
        powerManager.Tenacity += talisman.Tenacity;
        powerManager.ResistanceRate += talisman.ResistanceRate;
        powerManager.ComboRate += talisman.ComboRate;
        powerManager.ReflectionRate += talisman.ReflectionRate;
        powerManager.Mana += talisman.Mana;
        powerManager.ManaRegenerationRate += talisman.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += talisman.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += talisman.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += talisman.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += talisman.ResistanceToSameFactionRate;

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
    public PowerManager GetPuppetsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Puppet puppet = new Puppet();
        IPuppetGalleryRepository puppetGalleryRepository = new PuppetGalleryRepository();
        PuppetGalleryService puppetGalleryService = new PuppetGalleryService(puppetGalleryRepository);
        // Gallery
        Puppets puppet = puppetGalleryService.SumPowerPuppetGallery();
        powerManager.Power += puppet.Power;
        powerManager.Health += puppet.Health;
        powerManager.PhysicalAttack += puppet.PhysicalAttack;
        powerManager.PhysicalDefense += puppet.PhysicalDefense;
        powerManager.MagicalAttack += puppet.MagicalAttack;
        powerManager.MagicalDefense += puppet.MagicalDefense;
        powerManager.ChemicalAttack += puppet.ChemicalAttack;
        powerManager.ChemicalDefense += puppet.ChemicalDefense;
        powerManager.AtomicAttack += puppet.AtomicAttack;
        powerManager.AtomicDefense += puppet.AtomicDefense;
        powerManager.MentalAttack += puppet.MentalAttack;
        powerManager.MentalDefense += puppet.MentalDefense;
        powerManager.Speed += puppet.Speed;
        powerManager.CriticalDamageRate += puppet.CriticalDamageRate;
        powerManager.CriticalRate += puppet.CriticalRate;
        powerManager.PenetrationRate += puppet.PenetrationRate;
        powerManager.EvasionRate += puppet.EvasionRate;
        powerManager.DamageAbsorptionRate += puppet.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += puppet.VitalityRegenerationRate;
        powerManager.AccuracyRate += puppet.AccuracyRate;
        powerManager.LifestealRate += puppet.LifestealRate;
        powerManager.ShieldStrength += puppet.ShieldStrength;
        powerManager.Tenacity += puppet.Tenacity;
        powerManager.ResistanceRate += puppet.ResistanceRate;
        powerManager.ComboRate += puppet.ComboRate;
        powerManager.ReflectionRate += puppet.ReflectionRate;
        powerManager.Mana += puppet.Mana;
        powerManager.ManaRegenerationRate += puppet.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += puppet.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += puppet.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += puppet.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += puppet.ResistanceToSameFactionRate;

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
        powerManager.Power += puppet.Power;
        powerManager.Health += puppet.Health;
        powerManager.PhysicalAttack += puppet.PhysicalAttack;
        powerManager.PhysicalDefense += puppet.PhysicalDefense;
        powerManager.MagicalAttack += puppet.MagicalAttack;
        powerManager.MagicalDefense += puppet.MagicalDefense;
        powerManager.ChemicalAttack += puppet.ChemicalAttack;
        powerManager.ChemicalDefense += puppet.ChemicalDefense;
        powerManager.AtomicAttack += puppet.AtomicAttack;
        powerManager.AtomicDefense += puppet.AtomicDefense;
        powerManager.MentalAttack += puppet.MentalAttack;
        powerManager.MentalDefense += puppet.MentalDefense;
        powerManager.Speed += puppet.Speed;
        powerManager.CriticalDamageRate += puppet.CriticalDamageRate;
        powerManager.CriticalRate += puppet.CriticalRate;
        powerManager.PenetrationRate += puppet.PenetrationRate;
        powerManager.EvasionRate += puppet.EvasionRate;
        powerManager.DamageAbsorptionRate += puppet.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += puppet.VitalityRegenerationRate;
        powerManager.AccuracyRate += puppet.AccuracyRate;
        powerManager.LifestealRate += puppet.LifestealRate;
        powerManager.ShieldStrength += puppet.ShieldStrength;
        powerManager.Tenacity += puppet.Tenacity;
        powerManager.ResistanceRate += puppet.ResistanceRate;
        powerManager.ComboRate += puppet.ComboRate;
        powerManager.ReflectionRate += puppet.ReflectionRate;
        powerManager.Mana += puppet.Mana;
        powerManager.ManaRegenerationRate += puppet.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += puppet.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += puppet.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += puppet.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += puppet.ResistanceToSameFactionRate;

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
    public PowerManager GetAlchemiesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Alchemy alchemy = new Alchemy();
        IAlchemyGalleryRepository alchemyGalleryRepository = new AlchemyGalleryRepository();
        AlchemyGalleryService alchemyGalleryService = new AlchemyGalleryService(alchemyGalleryRepository);
        // Gallery
        Alchemies alchemy = alchemyGalleryService.SumPowerAlchemyGallery();
        powerManager.Power += alchemy.Power;
        powerManager.Health += alchemy.Health;
        powerManager.PhysicalAttack += alchemy.PhysicalAttack;
        powerManager.PhysicalDefense += alchemy.PhysicalDefense;
        powerManager.MagicalAttack += alchemy.MagicalAttack;
        powerManager.MagicalDefense += alchemy.MagicalDefense;
        powerManager.ChemicalAttack += alchemy.ChemicalAttack;
        powerManager.ChemicalDefense += alchemy.ChemicalDefense;
        powerManager.AtomicAttack += alchemy.AtomicAttack;
        powerManager.AtomicDefense += alchemy.AtomicDefense;
        powerManager.MentalAttack += alchemy.MentalAttack;
        powerManager.MentalDefense += alchemy.MentalDefense;
        powerManager.Speed += alchemy.Speed;
        powerManager.CriticalDamageRate += alchemy.CriticalDamageRate;
        powerManager.CriticalRate += alchemy.CriticalRate;
        powerManager.PenetrationRate += alchemy.PenetrationRate;
        powerManager.EvasionRate += alchemy.EvasionRate;
        powerManager.DamageAbsorptionRate += alchemy.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += alchemy.VitalityRegenerationRate;
        powerManager.AccuracyRate += alchemy.AccuracyRate;
        powerManager.LifestealRate += alchemy.LifestealRate;
        powerManager.ShieldStrength += alchemy.ShieldStrength;
        powerManager.Tenacity += alchemy.Tenacity;
        powerManager.ResistanceRate += alchemy.ResistanceRate;
        powerManager.ComboRate += alchemy.ComboRate;
        powerManager.ReflectionRate += alchemy.ReflectionRate;
        powerManager.Mana += alchemy.Mana;
        powerManager.ManaRegenerationRate += alchemy.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += alchemy.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += alchemy.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += alchemy.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += alchemy.ResistanceToSameFactionRate;

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
        powerManager.Power += alchemy.Power;
        powerManager.Health += alchemy.Health;
        powerManager.PhysicalAttack += alchemy.PhysicalAttack;
        powerManager.PhysicalDefense += alchemy.PhysicalDefense;
        powerManager.MagicalAttack += alchemy.MagicalAttack;
        powerManager.MagicalDefense += alchemy.MagicalDefense;
        powerManager.ChemicalAttack += alchemy.ChemicalAttack;
        powerManager.ChemicalDefense += alchemy.ChemicalDefense;
        powerManager.AtomicAttack += alchemy.AtomicAttack;
        powerManager.AtomicDefense += alchemy.AtomicDefense;
        powerManager.MentalAttack += alchemy.MentalAttack;
        powerManager.MentalDefense += alchemy.MentalDefense;
        powerManager.Speed += alchemy.Speed;
        powerManager.CriticalDamageRate += alchemy.CriticalDamageRate;
        powerManager.CriticalRate += alchemy.CriticalRate;
        powerManager.PenetrationRate += alchemy.PenetrationRate;
        powerManager.EvasionRate += alchemy.EvasionRate;
        powerManager.DamageAbsorptionRate += alchemy.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += alchemy.VitalityRegenerationRate;
        powerManager.AccuracyRate += alchemy.AccuracyRate;
        powerManager.LifestealRate += alchemy.LifestealRate;
        powerManager.ShieldStrength += alchemy.ShieldStrength;
        powerManager.Tenacity += alchemy.Tenacity;
        powerManager.ResistanceRate += alchemy.ResistanceRate;
        powerManager.ComboRate += alchemy.ComboRate;
        powerManager.ReflectionRate += alchemy.ReflectionRate;
        powerManager.Mana += alchemy.Mana;
        powerManager.ManaRegenerationRate += alchemy.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += alchemy.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += alchemy.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += alchemy.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += alchemy.ResistanceToSameFactionRate;

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
    public PowerManager GetForgesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Forge forge = new Forge();
        IForgeGalleryRepository forgeGalleryRepository = new ForgeGalleryRepository();
        ForgeGalleryService forgeGalleryService = new ForgeGalleryService(forgeGalleryRepository);
        // Gallery
        Forges forge = forgeGalleryService.SumPowerForgeGallery();
        powerManager.Power += forge.Power;
        powerManager.Health += forge.Health;
        powerManager.PhysicalAttack += forge.PhysicalAttack;
        powerManager.PhysicalDefense += forge.PhysicalDefense;
        powerManager.MagicalAttack += forge.MagicalAttack;
        powerManager.MagicalDefense += forge.MagicalDefense;
        powerManager.ChemicalAttack += forge.ChemicalAttack;
        powerManager.ChemicalDefense += forge.ChemicalDefense;
        powerManager.AtomicAttack += forge.AtomicAttack;
        powerManager.AtomicDefense += forge.AtomicDefense;
        powerManager.MentalAttack += forge.MentalAttack;
        powerManager.MentalDefense += forge.MentalDefense;
        powerManager.Speed += forge.Speed;
        powerManager.CriticalDamageRate += forge.CriticalDamageRate;
        powerManager.CriticalRate += forge.CriticalRate;
        powerManager.PenetrationRate += forge.PenetrationRate;
        powerManager.EvasionRate += forge.EvasionRate;
        powerManager.DamageAbsorptionRate += forge.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += forge.VitalityRegenerationRate;
        powerManager.AccuracyRate += forge.AccuracyRate;
        powerManager.LifestealRate += forge.LifestealRate;
        powerManager.ShieldStrength += forge.ShieldStrength;
        powerManager.Tenacity += forge.Tenacity;
        powerManager.ResistanceRate += forge.ResistanceRate;
        powerManager.ComboRate += forge.ComboRate;
        powerManager.ReflectionRate += forge.ReflectionRate;
        powerManager.Mana += forge.Mana;
        powerManager.ManaRegenerationRate += forge.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += forge.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += forge.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += forge.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += forge.ResistanceToSameFactionRate;

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
        powerManager.Power += forge.Power;
        powerManager.Health += forge.Health;
        powerManager.PhysicalAttack += forge.PhysicalAttack;
        powerManager.PhysicalDefense += forge.PhysicalDefense;
        powerManager.MagicalAttack += forge.MagicalAttack;
        powerManager.MagicalDefense += forge.MagicalDefense;
        powerManager.ChemicalAttack += forge.ChemicalAttack;
        powerManager.ChemicalDefense += forge.ChemicalDefense;
        powerManager.AtomicAttack += forge.AtomicAttack;
        powerManager.AtomicDefense += forge.AtomicDefense;
        powerManager.MentalAttack += forge.MentalAttack;
        powerManager.MentalDefense += forge.MentalDefense;
        powerManager.Speed += forge.Speed;
        powerManager.CriticalDamageRate += forge.CriticalDamageRate;
        powerManager.CriticalRate += forge.CriticalRate;
        powerManager.PenetrationRate += forge.PenetrationRate;
        powerManager.EvasionRate += forge.EvasionRate;
        powerManager.DamageAbsorptionRate += forge.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += forge.VitalityRegenerationRate;
        powerManager.AccuracyRate += forge.AccuracyRate;
        powerManager.LifestealRate += forge.LifestealRate;
        powerManager.ShieldStrength += forge.ShieldStrength;
        powerManager.Tenacity += forge.Tenacity;
        powerManager.ResistanceRate += forge.ResistanceRate;
        powerManager.ComboRate += forge.ComboRate;
        powerManager.ReflectionRate += forge.ReflectionRate;
        powerManager.Mana += forge.Mana;
        powerManager.ManaRegenerationRate += forge.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += forge.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += forge.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += forge.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += forge.ResistanceToSameFactionRate;

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
    public PowerManager GetCardLivesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // CardLife cardLife = new CardLife();
        ICardLifeGalleryRepository cardLifeGalleryRepository = new CardLifeGalleryRepository();
        CardLifeGalleryService cardLifeGalleryService = new CardLifeGalleryService(cardLifeGalleryRepository);
        // Gallery
        CardLives cardLife = cardLifeGalleryService.SumPowerCardLifeGallery();
        powerManager.Power += cardLife.Power;
        powerManager.Health += cardLife.Health;
        powerManager.PhysicalAttack += cardLife.PhysicalAttack;
        powerManager.PhysicalDefense += cardLife.PhysicalDefense;
        powerManager.MagicalAttack += cardLife.MagicalAttack;
        powerManager.MagicalDefense += cardLife.MagicalDefense;
        powerManager.ChemicalAttack += cardLife.ChemicalAttack;
        powerManager.ChemicalDefense += cardLife.ChemicalDefense;
        powerManager.AtomicAttack += cardLife.AtomicAttack;
        powerManager.AtomicDefense += cardLife.AtomicDefense;
        powerManager.MentalAttack += cardLife.MentalAttack;
        powerManager.MentalDefense += cardLife.MentalDefense;
        powerManager.Speed += cardLife.Speed;
        powerManager.CriticalDamageRate += cardLife.CriticalDamageRate;
        powerManager.CriticalRate += cardLife.CriticalRate;
        powerManager.PenetrationRate += cardLife.PenetrationRate;
        powerManager.EvasionRate += cardLife.EvasionRate;
        powerManager.DamageAbsorptionRate += cardLife.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardLife.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardLife.AccuracyRate;
        powerManager.LifestealRate += cardLife.LifestealRate;
        powerManager.ShieldStrength += cardLife.ShieldStrength;
        powerManager.Tenacity += cardLife.Tenacity;
        powerManager.ResistanceRate += cardLife.ResistanceRate;
        powerManager.ComboRate += cardLife.ComboRate;
        powerManager.ReflectionRate += cardLife.ReflectionRate;
        powerManager.Mana += cardLife.Mana;
        powerManager.ManaRegenerationRate += cardLife.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardLife.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardLife.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardLife.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardLife.ResistanceToSameFactionRate;

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
        powerManager.Power += cardLife.Power;
        powerManager.Health += cardLife.Health;
        powerManager.PhysicalAttack += cardLife.PhysicalAttack;
        powerManager.PhysicalDefense += cardLife.PhysicalDefense;
        powerManager.MagicalAttack += cardLife.MagicalAttack;
        powerManager.MagicalDefense += cardLife.MagicalDefense;
        powerManager.ChemicalAttack += cardLife.ChemicalAttack;
        powerManager.ChemicalDefense += cardLife.ChemicalDefense;
        powerManager.AtomicAttack += cardLife.AtomicAttack;
        powerManager.AtomicDefense += cardLife.AtomicDefense;
        powerManager.MentalAttack += cardLife.MentalAttack;
        powerManager.MentalDefense += cardLife.MentalDefense;
        powerManager.Speed += cardLife.Speed;
        powerManager.CriticalDamageRate += cardLife.CriticalDamageRate;
        powerManager.CriticalRate += cardLife.CriticalRate;
        powerManager.PenetrationRate += cardLife.PenetrationRate;
        powerManager.EvasionRate += cardLife.EvasionRate;
        powerManager.DamageAbsorptionRate += cardLife.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += cardLife.VitalityRegenerationRate;
        powerManager.AccuracyRate += cardLife.AccuracyRate;
        powerManager.LifestealRate += cardLife.LifestealRate;
        powerManager.ShieldStrength += cardLife.ShieldStrength;
        powerManager.Tenacity += cardLife.Tenacity;
        powerManager.ResistanceRate += cardLife.ResistanceRate;
        powerManager.ComboRate += cardLife.ComboRate;
        powerManager.ReflectionRate += cardLife.ReflectionRate;
        powerManager.Mana += cardLife.Mana;
        powerManager.ManaRegenerationRate += cardLife.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += cardLife.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += cardLife.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += cardLife.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += cardLife.ResistanceToSameFactionRate;

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
    public PowerManager GetArtworksPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Artwork Artwork = new Artwork();
        IArtworkGalleryRepository artworkGalleryRepository = new ArtworkGalleryRepository();
        ArtworkGalleryService artworkGalleryService = new ArtworkGalleryService(artworkGalleryRepository);
        // Gallery
        Artworks artwork = artworkGalleryService.SumPowerArtworkGallery();
        powerManager.Power += artwork.Power;
        powerManager.Health += artwork.Health;
        powerManager.PhysicalAttack += artwork.PhysicalAttack;
        powerManager.PhysicalDefense += artwork.PhysicalDefense;
        powerManager.MagicalAttack += artwork.MagicalAttack;
        powerManager.MagicalDefense += artwork.MagicalDefense;
        powerManager.ChemicalAttack += artwork.ChemicalAttack;
        powerManager.ChemicalDefense += artwork.ChemicalDefense;
        powerManager.AtomicAttack += artwork.AtomicAttack;
        powerManager.AtomicDefense += artwork.AtomicDefense;
        powerManager.MentalAttack += artwork.MentalAttack;
        powerManager.MentalDefense += artwork.MentalDefense;
        powerManager.Speed += artwork.Speed;
        powerManager.CriticalDamageRate += artwork.CriticalDamageRate;
        powerManager.CriticalRate += artwork.CriticalRate;
        powerManager.PenetrationRate += artwork.PenetrationRate;
        powerManager.EvasionRate += artwork.EvasionRate;
        powerManager.DamageAbsorptionRate += artwork.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += artwork.VitalityRegenerationRate;
        powerManager.AccuracyRate += artwork.AccuracyRate;
        powerManager.LifestealRate += artwork.LifestealRate;
        powerManager.ShieldStrength += artwork.ShieldStrength;
        powerManager.Tenacity += artwork.Tenacity;
        powerManager.ResistanceRate += artwork.ResistanceRate;
        powerManager.ComboRate += artwork.ComboRate;
        powerManager.ReflectionRate += artwork.ReflectionRate;
        powerManager.Mana += artwork.Mana;
        powerManager.ManaRegenerationRate += artwork.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += artwork.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += artwork.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += artwork.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += artwork.ResistanceToSameFactionRate;

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
        powerManager.Power += artwork.Power;
        powerManager.Health += artwork.Health;
        powerManager.PhysicalAttack += artwork.PhysicalAttack;
        powerManager.PhysicalDefense += artwork.PhysicalDefense;
        powerManager.MagicalAttack += artwork.MagicalAttack;
        powerManager.MagicalDefense += artwork.MagicalDefense;
        powerManager.ChemicalAttack += artwork.ChemicalAttack;
        powerManager.ChemicalDefense += artwork.ChemicalDefense;
        powerManager.AtomicAttack += artwork.AtomicAttack;
        powerManager.AtomicDefense += artwork.AtomicDefense;
        powerManager.MentalAttack += artwork.MentalAttack;
        powerManager.MentalDefense += artwork.MentalDefense;
        powerManager.Speed += artwork.Speed;
        powerManager.CriticalDamageRate += artwork.CriticalDamageRate;
        powerManager.CriticalRate += artwork.CriticalRate;
        powerManager.PenetrationRate += artwork.PenetrationRate;
        powerManager.EvasionRate += artwork.EvasionRate;
        powerManager.DamageAbsorptionRate += artwork.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += artwork.VitalityRegenerationRate;
        powerManager.AccuracyRate += artwork.AccuracyRate;
        powerManager.LifestealRate += artwork.LifestealRate;
        powerManager.ShieldStrength += artwork.ShieldStrength;
        powerManager.Tenacity += artwork.Tenacity;
        powerManager.ResistanceRate += artwork.ResistanceRate;
        powerManager.ComboRate += artwork.ComboRate;
        powerManager.ReflectionRate += artwork.ReflectionRate;
        powerManager.Mana += artwork.Mana;
        powerManager.ManaRegenerationRate += artwork.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += artwork.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += artwork.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += artwork.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += artwork.ResistanceToSameFactionRate;

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
    public PowerManager GetSpiritBeastsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        ISpiritBeastGalleryRepository spiritBeastGalleryRepository = new SpiritBeastGalleryRepository();
        SpiritBeastGalleryService SpiritBeastGalleryService = new SpiritBeastGalleryService(spiritBeastGalleryRepository);
        // Gallery
        SpiritBeasts spiritBeast = SpiritBeastGalleryService.SumPowerSpiritBeastGallery();
        powerManager.Power += spiritBeast.Power;
        powerManager.Health += spiritBeast.Health;
        powerManager.PhysicalAttack += spiritBeast.PhysicalAttack;
        powerManager.PhysicalDefense += spiritBeast.PhysicalDefense;
        powerManager.MagicalAttack += spiritBeast.MagicalAttack;
        powerManager.MagicalDefense += spiritBeast.MagicalDefense;
        powerManager.ChemicalAttack += spiritBeast.ChemicalAttack;
        powerManager.ChemicalDefense += spiritBeast.ChemicalDefense;
        powerManager.AtomicAttack += spiritBeast.AtomicAttack;
        powerManager.AtomicDefense += spiritBeast.AtomicDefense;
        powerManager.MentalAttack += spiritBeast.MentalAttack;
        powerManager.MentalDefense += spiritBeast.MentalDefense;
        powerManager.Speed += spiritBeast.Speed;
        powerManager.CriticalDamageRate += spiritBeast.CriticalDamageRate;
        powerManager.CriticalRate += spiritBeast.CriticalRate;
        powerManager.PenetrationRate += spiritBeast.PenetrationRate;
        powerManager.EvasionRate += spiritBeast.EvasionRate;
        powerManager.DamageAbsorptionRate += spiritBeast.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += spiritBeast.VitalityRegenerationRate;
        powerManager.AccuracyRate += spiritBeast.AccuracyRate;
        powerManager.LifestealRate += spiritBeast.LifestealRate;
        powerManager.ShieldStrength += spiritBeast.ShieldStrength;
        powerManager.Tenacity += spiritBeast.Tenacity;
        powerManager.ResistanceRate += spiritBeast.ResistanceRate;
        powerManager.ComboRate += spiritBeast.ComboRate;
        powerManager.ReflectionRate += spiritBeast.ReflectionRate;
        powerManager.Mana += spiritBeast.Mana;
        powerManager.ManaRegenerationRate += spiritBeast.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += spiritBeast.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += spiritBeast.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += spiritBeast.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += spiritBeast.ResistanceToSameFactionRate;

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
        powerManager.Power += spiritBeast.Power;
        powerManager.Health += spiritBeast.Health;
        powerManager.PhysicalAttack += spiritBeast.PhysicalAttack;
        powerManager.PhysicalDefense += spiritBeast.PhysicalDefense;
        powerManager.MagicalAttack += spiritBeast.MagicalAttack;
        powerManager.MagicalDefense += spiritBeast.MagicalDefense;
        powerManager.ChemicalAttack += spiritBeast.ChemicalAttack;
        powerManager.ChemicalDefense += spiritBeast.ChemicalDefense;
        powerManager.AtomicAttack += spiritBeast.AtomicAttack;
        powerManager.AtomicDefense += spiritBeast.AtomicDefense;
        powerManager.MentalAttack += spiritBeast.MentalAttack;
        powerManager.MentalDefense += spiritBeast.MentalDefense;
        powerManager.Speed += spiritBeast.Speed;
        powerManager.CriticalDamageRate += spiritBeast.CriticalDamageRate;
        powerManager.CriticalRate += spiritBeast.CriticalRate;
        powerManager.PenetrationRate += spiritBeast.PenetrationRate;
        powerManager.EvasionRate += spiritBeast.EvasionRate;
        powerManager.DamageAbsorptionRate += spiritBeast.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += spiritBeast.VitalityRegenerationRate;
        powerManager.AccuracyRate += spiritBeast.AccuracyRate;
        powerManager.LifestealRate += spiritBeast.LifestealRate;
        powerManager.ShieldStrength += spiritBeast.ShieldStrength;
        powerManager.Tenacity += spiritBeast.Tenacity;
        powerManager.ResistanceRate += spiritBeast.ResistanceRate;
        powerManager.ComboRate += spiritBeast.ComboRate;
        powerManager.ReflectionRate += spiritBeast.ReflectionRate;
        powerManager.Mana += spiritBeast.Mana;
        powerManager.ManaRegenerationRate += spiritBeast.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += spiritBeast.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += spiritBeast.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += spiritBeast.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += spiritBeast.ResistanceToSameFactionRate;

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
    public PowerManager GetSpiritCardsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        ISpiritCardGalleryRepository spiritCardGalleryRepository = new SpiritCardGalleryRepository();
        SpiritCardGalleryService spiritCardGalleryService = new SpiritCardGalleryService(spiritCardGalleryRepository);
        // Gallery
        SpiritCards spiritCard = spiritCardGalleryService.SumPowerSpiritCardGallery();
        powerManager.Power += spiritCard.Power;
        powerManager.Health += spiritCard.Health;
        powerManager.PhysicalAttack += spiritCard.PhysicalAttack;
        powerManager.PhysicalDefense += spiritCard.PhysicalDefense;
        powerManager.MagicalAttack += spiritCard.MagicalAttack;
        powerManager.MagicalDefense += spiritCard.MagicalDefense;
        powerManager.ChemicalAttack += spiritCard.ChemicalAttack;
        powerManager.ChemicalDefense += spiritCard.ChemicalDefense;
        powerManager.AtomicAttack += spiritCard.AtomicAttack;
        powerManager.AtomicDefense += spiritCard.AtomicDefense;
        powerManager.MentalAttack += spiritCard.MentalAttack;
        powerManager.MentalDefense += spiritCard.MentalDefense;
        powerManager.Speed += spiritCard.Speed;
        powerManager.CriticalDamageRate += spiritCard.CriticalDamageRate;
        powerManager.CriticalRate += spiritCard.CriticalRate;
        powerManager.PenetrationRate += spiritCard.PenetrationRate;
        powerManager.EvasionRate += spiritCard.EvasionRate;
        powerManager.DamageAbsorptionRate += spiritCard.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += spiritCard.VitalityRegenerationRate;
        powerManager.AccuracyRate += spiritCard.AccuracyRate;
        powerManager.LifestealRate += spiritCard.LifestealRate;
        powerManager.ShieldStrength += spiritCard.ShieldStrength;
        powerManager.Tenacity += spiritCard.Tenacity;
        powerManager.ResistanceRate += spiritCard.ResistanceRate;
        powerManager.ComboRate += spiritCard.ComboRate;
        powerManager.ReflectionRate += spiritCard.ReflectionRate;
        powerManager.Mana += spiritCard.Mana;
        powerManager.ManaRegenerationRate += spiritCard.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += spiritCard.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += spiritCard.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += spiritCard.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += spiritCard.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += spiritCard.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += spiritCard.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += spiritCard.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += spiritCard.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += spiritCard.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += spiritCard.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += spiritCard.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += spiritCard.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += spiritCard.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += spiritCard.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += spiritCard.PercentAllMentalDefense;

        IUserSpiritCardRepository userSpiritCardRepository = new UserSpiritCardRepository();
        UserSpiritCardService userSpiritCardService = new UserSpiritCardService(userSpiritCardRepository);
        // User SpiritBeast (Gallery)
        spiritCard = userSpiritCardService.SumPowerUserSpiritCard(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.Power += spiritCard.Power;
        powerManager.Health += spiritCard.Health;
        powerManager.PhysicalAttack += spiritCard.PhysicalAttack;
        powerManager.PhysicalDefense += spiritCard.PhysicalDefense;
        powerManager.MagicalAttack += spiritCard.MagicalAttack;
        powerManager.MagicalDefense += spiritCard.MagicalDefense;
        powerManager.ChemicalAttack += spiritCard.ChemicalAttack;
        powerManager.ChemicalDefense += spiritCard.ChemicalDefense;
        powerManager.AtomicAttack += spiritCard.AtomicAttack;
        powerManager.AtomicDefense += spiritCard.AtomicDefense;
        powerManager.MentalAttack += spiritCard.MentalAttack;
        powerManager.MentalDefense += spiritCard.MentalDefense;
        powerManager.Speed += spiritCard.Speed;
        powerManager.CriticalDamageRate += spiritCard.CriticalDamageRate;
        powerManager.CriticalRate += spiritCard.CriticalRate;
        powerManager.PenetrationRate += spiritCard.PenetrationRate;
        powerManager.EvasionRate += spiritCard.EvasionRate;
        powerManager.DamageAbsorptionRate += spiritCard.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += spiritCard.VitalityRegenerationRate;
        powerManager.AccuracyRate += spiritCard.AccuracyRate;
        powerManager.LifestealRate += spiritCard.LifestealRate;
        powerManager.ShieldStrength += spiritCard.ShieldStrength;
        powerManager.Tenacity += spiritCard.Tenacity;
        powerManager.ResistanceRate += spiritCard.ResistanceRate;
        powerManager.ComboRate += spiritCard.ComboRate;
        powerManager.ReflectionRate += spiritCard.ReflectionRate;
        powerManager.Mana += spiritCard.Mana;
        powerManager.ManaRegenerationRate += spiritCard.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += spiritCard.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += spiritCard.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += spiritCard.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += spiritCard.ResistanceToSameFactionRate;

        ISpiritCardRepository spiritCardRepository = new SpiritCardRepository();
        SpiritCardService spiritCardService = new SpiritCardService(spiritCardRepository);
        // Percent
        spiritCard = spiritCardService.SumPowerSpiritCardPercent(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
        powerManager.PercentAllHealth += spiritCard.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += spiritCard.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += spiritCard.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += spiritCard.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += spiritCard.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += spiritCard.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += spiritCard.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += spiritCard.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += spiritCard.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += spiritCard.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += spiritCard.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetVehiclesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        IVehicleGalleryRepository vehicleGalleryRepository = new VehicleGalleryRepository();
        VehicleGalleryService vehicleGalleryService = new VehicleGalleryService(vehicleGalleryRepository);
        // Gallery
        Vehicles vehicle = vehicleGalleryService.SumPowerVehiclesGallery();
        powerManager.Power += vehicle.Power;
        powerManager.Health += vehicle.Health;
        powerManager.PhysicalAttack += vehicle.PhysicalAttack;
        powerManager.PhysicalDefense += vehicle.PhysicalDefense;
        powerManager.MagicalAttack += vehicle.MagicalAttack;
        powerManager.MagicalDefense += vehicle.MagicalDefense;
        powerManager.ChemicalAttack += vehicle.ChemicalAttack;
        powerManager.ChemicalDefense += vehicle.ChemicalDefense;
        powerManager.AtomicAttack += vehicle.AtomicAttack;
        powerManager.AtomicDefense += vehicle.AtomicDefense;
        powerManager.MentalAttack += vehicle.MentalAttack;
        powerManager.MentalDefense += vehicle.MentalDefense;
        powerManager.Speed += vehicle.Speed;
        powerManager.CriticalDamageRate += vehicle.CriticalDamageRate;
        powerManager.CriticalRate += vehicle.CriticalRate;
        powerManager.PenetrationRate += vehicle.PenetrationRate;
        powerManager.EvasionRate += vehicle.EvasionRate;
        powerManager.DamageAbsorptionRate += vehicle.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += vehicle.VitalityRegenerationRate;
        powerManager.AccuracyRate += vehicle.AccuracyRate;
        powerManager.LifestealRate += vehicle.LifestealRate;
        powerManager.ShieldStrength += vehicle.ShieldStrength;
        powerManager.Tenacity += vehicle.Tenacity;
        powerManager.ResistanceRate += vehicle.ResistanceRate;
        powerManager.ComboRate += vehicle.ComboRate;
        powerManager.ReflectionRate += vehicle.ReflectionRate;
        powerManager.Mana += vehicle.Mana;
        powerManager.ManaRegenerationRate += vehicle.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += vehicle.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += vehicle.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += vehicle.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += vehicle.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += vehicle.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += vehicle.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += vehicle.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += vehicle.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += vehicle.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += vehicle.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += vehicle.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += vehicle.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += vehicle.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += vehicle.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += vehicle.PercentAllMentalDefense;

        IUserVehicleRepository userVehicleRepository = new UserVehicleRepository();
        UserVehicleService userVehicleService = new UserVehicleService(userVehicleRepository);
        // User SpiritBeast (Gallery)
        vehicle = userVehicleService.SumPowerUserVehicle(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.Power += vehicle.Power;
        powerManager.Health += vehicle.Health;
        powerManager.PhysicalAttack += vehicle.PhysicalAttack;
        powerManager.PhysicalDefense += vehicle.PhysicalDefense;
        powerManager.MagicalAttack += vehicle.MagicalAttack;
        powerManager.MagicalDefense += vehicle.MagicalDefense;
        powerManager.ChemicalAttack += vehicle.ChemicalAttack;
        powerManager.ChemicalDefense += vehicle.ChemicalDefense;
        powerManager.AtomicAttack += vehicle.AtomicAttack;
        powerManager.AtomicDefense += vehicle.AtomicDefense;
        powerManager.MentalAttack += vehicle.MentalAttack;
        powerManager.MentalDefense += vehicle.MentalDefense;
        powerManager.Speed += vehicle.Speed;
        powerManager.CriticalDamageRate += vehicle.CriticalDamageRate;
        powerManager.CriticalRate += vehicle.CriticalRate;
        powerManager.PenetrationRate += vehicle.PenetrationRate;
        powerManager.EvasionRate += vehicle.EvasionRate;
        powerManager.DamageAbsorptionRate += vehicle.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += vehicle.VitalityRegenerationRate;
        powerManager.AccuracyRate += vehicle.AccuracyRate;
        powerManager.LifestealRate += vehicle.LifestealRate;
        powerManager.ShieldStrength += vehicle.ShieldStrength;
        powerManager.Tenacity += vehicle.Tenacity;
        powerManager.ResistanceRate += vehicle.ResistanceRate;
        powerManager.ComboRate += vehicle.ComboRate;
        powerManager.ReflectionRate += vehicle.ReflectionRate;
        powerManager.Mana += vehicle.Mana;
        powerManager.ManaRegenerationRate += vehicle.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += vehicle.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += vehicle.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += vehicle.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += vehicle.ResistanceToSameFactionRate;

        IVehiclesRepository vehicleRepository = new VehiclesRepository();
        VehiclesService vehiclesService = new VehiclesService(vehicleRepository);
        // Percent
        vehicle = vehiclesService.SumPowerVehiclePercent(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
        powerManager.PercentAllHealth += vehicle.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += vehicle.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += vehicle.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += vehicle.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += vehicle.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += vehicle.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += vehicle.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += vehicle.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += vehicle.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += vehicle.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += vehicle.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetCardsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ICardsGalleryRepository cardsGalleryRepository = new CardsGalleryRepository();
        CardsGalleryService cardsGalleryService = new CardsGalleryService(cardsGalleryRepository);
        // Gallery
        Cards card = cardsGalleryService.SumPowerCardsGallery();
        powerManager.Power += card.Power;
        powerManager.Health += card.Health;
        powerManager.PhysicalAttack += card.PhysicalAttack;
        powerManager.PhysicalDefense += card.PhysicalDefense;
        powerManager.MagicalAttack += card.MagicalAttack;
        powerManager.MagicalDefense += card.MagicalDefense;
        powerManager.ChemicalAttack += card.ChemicalAttack;
        powerManager.ChemicalDefense += card.ChemicalDefense;
        powerManager.AtomicAttack += card.AtomicAttack;
        powerManager.AtomicDefense += card.AtomicDefense;
        powerManager.MentalAttack += card.MentalAttack;
        powerManager.MentalDefense += card.MentalDefense;
        powerManager.Speed += card.Speed;
        powerManager.CriticalDamageRate += card.CriticalDamageRate;
        powerManager.CriticalRate += card.CriticalRate;
        powerManager.PenetrationRate += card.PenetrationRate;
        powerManager.EvasionRate += card.EvasionRate;
        powerManager.DamageAbsorptionRate += card.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += card.VitalityRegenerationRate;
        powerManager.AccuracyRate += card.AccuracyRate;
        powerManager.LifestealRate += card.LifestealRate;
        powerManager.ShieldStrength += card.ShieldStrength;
        powerManager.Tenacity += card.Tenacity;
        powerManager.ResistanceRate += card.ResistanceRate;
        powerManager.ComboRate += card.ComboRate;
        powerManager.ReflectionRate += card.ReflectionRate;
        powerManager.Mana += card.Mana;
        powerManager.ManaRegenerationRate += card.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += card.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += card.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += card.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += card.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += card.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += card.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += card.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += card.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += card.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += card.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += card.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += card.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += card.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += card.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += card.PercentAllMentalDefense;

        IUserCardsRepository userCardsRepository = new UserCardsRepository();
        UserCardsService userCardsService = new UserCardsService(userCardsRepository);
        // User Cards (Gallery)
        card = userCardsService.SumPowerUserCards(); // Giả định SumPowerUserCards cũng trả về một đối tượng Cards mới hoặc đã được reset
        powerManager.Power += card.Power;
        powerManager.Health += card.Health;
        powerManager.PhysicalAttack += card.PhysicalAttack;
        powerManager.PhysicalDefense += card.PhysicalDefense;
        powerManager.MagicalAttack += card.MagicalAttack;
        powerManager.MagicalDefense += card.MagicalDefense;
        powerManager.ChemicalAttack += card.ChemicalAttack;
        powerManager.ChemicalDefense += card.ChemicalDefense;
        powerManager.AtomicAttack += card.AtomicAttack;
        powerManager.AtomicDefense += card.AtomicDefense;
        powerManager.MentalAttack += card.MentalAttack;
        powerManager.MentalDefense += card.MentalDefense;
        powerManager.Speed += card.Speed;
        powerManager.CriticalDamageRate += card.CriticalDamageRate;
        powerManager.CriticalRate += card.CriticalRate;
        powerManager.PenetrationRate += card.PenetrationRate;
        powerManager.EvasionRate += card.EvasionRate;
        powerManager.DamageAbsorptionRate += card.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += card.VitalityRegenerationRate;
        powerManager.AccuracyRate += card.AccuracyRate;
        powerManager.LifestealRate += card.LifestealRate;
        powerManager.ShieldStrength += card.ShieldStrength;
        powerManager.Tenacity += card.Tenacity;
        powerManager.ResistanceRate += card.ResistanceRate;
        powerManager.ComboRate += card.ComboRate;
        powerManager.ReflectionRate += card.ReflectionRate;
        powerManager.Mana += card.Mana;
        powerManager.ManaRegenerationRate += card.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += card.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += card.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += card.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += card.ResistanceToSameFactionRate;

        ICardsRepository cardsRepository = new CardsRepository();
        CardsService cardsService = new CardsService(cardsRepository);
        // Percent
        card = cardsService.SumPowerCardsPercent(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.PercentAllHealth += card.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += card.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += card.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += card.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += card.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += card.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += card.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += card.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += card.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += card.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += card.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetTechnologiesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ITechnologiesGalleryRepository technologiesGalleryRepository = new TechnologiesGalleryRepository();
        TechnologiesGalleryService technologiesGalleryService = new TechnologiesGalleryService(technologiesGalleryRepository);
        // Gallery
        Technologies technology = technologiesGalleryService.SumPowerTechnologiesGallery();
        powerManager.Power += technology.Power;
        powerManager.Health += technology.Health;
        powerManager.PhysicalAttack += technology.PhysicalAttack;
        powerManager.PhysicalDefense += technology.PhysicalDefense;
        powerManager.MagicalAttack += technology.MagicalAttack;
        powerManager.MagicalDefense += technology.MagicalDefense;
        powerManager.ChemicalAttack += technology.ChemicalAttack;
        powerManager.ChemicalDefense += technology.ChemicalDefense;
        powerManager.AtomicAttack += technology.AtomicAttack;
        powerManager.AtomicDefense += technology.AtomicDefense;
        powerManager.MentalAttack += technology.MentalAttack;
        powerManager.MentalDefense += technology.MentalDefense;
        powerManager.Speed += technology.Speed;
        powerManager.CriticalDamageRate += technology.CriticalDamageRate;
        powerManager.CriticalRate += technology.CriticalRate;
        powerManager.PenetrationRate += technology.PenetrationRate;
        powerManager.EvasionRate += technology.EvasionRate;
        powerManager.DamageAbsorptionRate += technology.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += technology.VitalityRegenerationRate;
        powerManager.AccuracyRate += technology.AccuracyRate;
        powerManager.LifestealRate += technology.LifestealRate;
        powerManager.ShieldStrength += technology.ShieldStrength;
        powerManager.Tenacity += technology.Tenacity;
        powerManager.ResistanceRate += technology.ResistanceRate;
        powerManager.ComboRate += technology.ComboRate;
        powerManager.ReflectionRate += technology.ReflectionRate;
        powerManager.Mana += technology.Mana;
        powerManager.ManaRegenerationRate += technology.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += technology.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += technology.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += technology.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += technology.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += technology.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += technology.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += technology.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += technology.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += technology.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += technology.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += technology.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += technology.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += technology.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += technology.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += technology.PercentAllMentalDefense;

        IUserTechnologiesRepository userTechnologiesRepository = new UserTechnologiesRepository();
        UserTechnologiesService userTechnologiesService = new UserTechnologiesService(userTechnologiesRepository);
        // User Technologies (Gallery)
        technology = userTechnologiesService.SumPowerUserTechnologies(); // Giả định SumPowerUserTechnologies cũng trả về một đối tượng Technologies mới hoặc đã được reset
        powerManager.Power += technology.Power;
        powerManager.Health += technology.Health;
        powerManager.PhysicalAttack += technology.PhysicalAttack;
        powerManager.PhysicalDefense += technology.PhysicalDefense;
        powerManager.MagicalAttack += technology.MagicalAttack;
        powerManager.MagicalDefense += technology.MagicalDefense;
        powerManager.ChemicalAttack += technology.ChemicalAttack;
        powerManager.ChemicalDefense += technology.ChemicalDefense;
        powerManager.AtomicAttack += technology.AtomicAttack;
        powerManager.AtomicDefense += technology.AtomicDefense;
        powerManager.MentalAttack += technology.MentalAttack;
        powerManager.MentalDefense += technology.MentalDefense;
        powerManager.Speed += technology.Speed;
        powerManager.CriticalDamageRate += technology.CriticalDamageRate;
        powerManager.CriticalRate += technology.CriticalRate;
        powerManager.PenetrationRate += technology.PenetrationRate;
        powerManager.EvasionRate += technology.EvasionRate;
        powerManager.DamageAbsorptionRate += technology.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += technology.VitalityRegenerationRate;
        powerManager.AccuracyRate += technology.AccuracyRate;
        powerManager.LifestealRate += technology.LifestealRate;
        powerManager.ShieldStrength += technology.ShieldStrength;
        powerManager.Tenacity += technology.Tenacity;
        powerManager.ResistanceRate += technology.ResistanceRate;
        powerManager.ComboRate += technology.ComboRate;
        powerManager.ReflectionRate += technology.ReflectionRate;
        powerManager.Mana += technology.Mana;
        powerManager.ManaRegenerationRate += technology.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += technology.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += technology.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += technology.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += technology.ResistanceToSameFactionRate;

        ITechnologiesRepository technologiesRepository = new TechnologiesRepository();
        TechnologiesService technologiesService = new TechnologiesService(technologiesRepository);
        // Percent
        technology = technologiesService.SumPowerTechnologiesPercent(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.PercentAllHealth += technology.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += technology.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += technology.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += technology.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += technology.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += technology.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += technology.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += technology.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += technology.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += technology.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += technology.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetArchitecturesPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IArchitecturesGalleryRepository architecturesGalleryRepository = new ArchitecturesGalleryRepository();
        ArchitecturesGalleryService architecturesGalleryService = new ArchitecturesGalleryService(architecturesGalleryRepository);
        // Gallery
        Architectures architecture = architecturesGalleryService.SumPowerArchitecturesGallery();
        powerManager.Power += architecture.Power;
        powerManager.Health += architecture.Health;
        powerManager.PhysicalAttack += architecture.PhysicalAttack;
        powerManager.PhysicalDefense += architecture.PhysicalDefense;
        powerManager.MagicalAttack += architecture.MagicalAttack;
        powerManager.MagicalDefense += architecture.MagicalDefense;
        powerManager.ChemicalAttack += architecture.ChemicalAttack;
        powerManager.ChemicalDefense += architecture.ChemicalDefense;
        powerManager.AtomicAttack += architecture.AtomicAttack;
        powerManager.AtomicDefense += architecture.AtomicDefense;
        powerManager.MentalAttack += architecture.MentalAttack;
        powerManager.MentalDefense += architecture.MentalDefense;
        powerManager.Speed += architecture.Speed;
        powerManager.CriticalDamageRate += architecture.CriticalDamageRate;
        powerManager.CriticalRate += architecture.CriticalRate;
        powerManager.PenetrationRate += architecture.PenetrationRate;
        powerManager.EvasionRate += architecture.EvasionRate;
        powerManager.DamageAbsorptionRate += architecture.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += architecture.VitalityRegenerationRate;
        powerManager.AccuracyRate += architecture.AccuracyRate;
        powerManager.LifestealRate += architecture.LifestealRate;
        powerManager.ShieldStrength += architecture.ShieldStrength;
        powerManager.Tenacity += architecture.Tenacity;
        powerManager.ResistanceRate += architecture.ResistanceRate;
        powerManager.ComboRate += architecture.ComboRate;
        powerManager.ReflectionRate += architecture.ReflectionRate;
        powerManager.Mana += architecture.Mana;
        powerManager.ManaRegenerationRate += architecture.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += architecture.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += architecture.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += architecture.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += architecture.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += architecture.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += architecture.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += architecture.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += architecture.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += architecture.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += architecture.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += architecture.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += architecture.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += architecture.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += architecture.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += architecture.PercentAllMentalDefense;

        IUserArchitecturesRepository userArchitecturesRepository = new UserArchitecturesRepository();
        UserArchitecturesService userArchitecturesService = new UserArchitecturesService(userArchitecturesRepository);
        // User Architectures (Gallery)
        architecture = userArchitecturesService.SumPowerUserArchitectures(); // Giả định SumPowerUserArchitectures cũng trả về một đối tượng Architectures mới hoặc đã được reset
        powerManager.Power += architecture.Power;
        powerManager.Health += architecture.Health;
        powerManager.PhysicalAttack += architecture.PhysicalAttack;
        powerManager.PhysicalDefense += architecture.PhysicalDefense;
        powerManager.MagicalAttack += architecture.MagicalAttack;
        powerManager.MagicalDefense += architecture.MagicalDefense;
        powerManager.ChemicalAttack += architecture.ChemicalAttack;
        powerManager.ChemicalDefense += architecture.ChemicalDefense;
        powerManager.AtomicAttack += architecture.AtomicAttack;
        powerManager.AtomicDefense += architecture.AtomicDefense;
        powerManager.MentalAttack += architecture.MentalAttack;
        powerManager.MentalDefense += architecture.MentalDefense;
        powerManager.Speed += architecture.Speed;
        powerManager.CriticalDamageRate += architecture.CriticalDamageRate;
        powerManager.CriticalRate += architecture.CriticalRate;
        powerManager.PenetrationRate += architecture.PenetrationRate;
        powerManager.EvasionRate += architecture.EvasionRate;
        powerManager.DamageAbsorptionRate += architecture.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += architecture.VitalityRegenerationRate;
        powerManager.AccuracyRate += architecture.AccuracyRate;
        powerManager.LifestealRate += architecture.LifestealRate;
        powerManager.ShieldStrength += architecture.ShieldStrength;
        powerManager.Tenacity += architecture.Tenacity;
        powerManager.ResistanceRate += architecture.ResistanceRate;
        powerManager.ComboRate += architecture.ComboRate;
        powerManager.ReflectionRate += architecture.ReflectionRate;
        powerManager.Mana += architecture.Mana;
        powerManager.ManaRegenerationRate += architecture.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += architecture.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += architecture.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += architecture.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += architecture.ResistanceToSameFactionRate;

        IArchitecturesRepository architecturesRepository = new ArchitecturesRepository();
        ArchitecturesService architecturesService = new ArchitecturesService(architecturesRepository);
        // Percent
        architecture = architecturesService.SumPowerArchitecturesPercent(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.PercentAllHealth += architecture.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += architecture.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += architecture.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += architecture.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += architecture.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += architecture.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += architecture.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += architecture.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += architecture.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += architecture.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += architecture.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetCoresPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ICoresGalleryRepository coresGalleryRepository = new CoresGalleryRepository();
        CoresGalleryService coresGalleryService = new CoresGalleryService(coresGalleryRepository);
        // Gallery
        Cores core = coresGalleryService.SumPowerCoresGallery();
        powerManager.Power += core.Power;
        powerManager.Health += core.Health;
        powerManager.PhysicalAttack += core.PhysicalAttack;
        powerManager.PhysicalDefense += core.PhysicalDefense;
        powerManager.MagicalAttack += core.MagicalAttack;
        powerManager.MagicalDefense += core.MagicalDefense;
        powerManager.ChemicalAttack += core.ChemicalAttack;
        powerManager.ChemicalDefense += core.ChemicalDefense;
        powerManager.AtomicAttack += core.AtomicAttack;
        powerManager.AtomicDefense += core.AtomicDefense;
        powerManager.MentalAttack += core.MentalAttack;
        powerManager.MentalDefense += core.MentalDefense;
        powerManager.Speed += core.Speed;
        powerManager.CriticalDamageRate += core.CriticalDamageRate;
        powerManager.CriticalRate += core.CriticalRate;
        powerManager.PenetrationRate += core.PenetrationRate;
        powerManager.EvasionRate += core.EvasionRate;
        powerManager.DamageAbsorptionRate += core.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += core.VitalityRegenerationRate;
        powerManager.AccuracyRate += core.AccuracyRate;
        powerManager.LifestealRate += core.LifestealRate;
        powerManager.ShieldStrength += core.ShieldStrength;
        powerManager.Tenacity += core.Tenacity;
        powerManager.ResistanceRate += core.ResistanceRate;
        powerManager.ComboRate += core.ComboRate;
        powerManager.ReflectionRate += core.ReflectionRate;
        powerManager.Mana += core.Mana;
        powerManager.ManaRegenerationRate += core.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += core.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += core.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += core.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += core.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += core.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += core.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += core.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += core.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += core.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += core.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += core.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += core.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += core.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += core.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += core.PercentAllMentalDefense;

        IUserCoresRepository userCoresRepository = new UserCoresRepository();
        UserCoresService userCoresService = new UserCoresService(userCoresRepository);
        // User Cores (Gallery)
        core = userCoresService.SumPowerUserCores(); // Giả định SumPowerUserCores cũng trả về một đối tượng Cores mới hoặc đã được reset
        powerManager.Power += core.Power;
        powerManager.Health += core.Health;
        powerManager.PhysicalAttack += core.PhysicalAttack;
        powerManager.PhysicalDefense += core.PhysicalDefense;
        powerManager.MagicalAttack += core.MagicalAttack;
        powerManager.MagicalDefense += core.MagicalDefense;
        powerManager.ChemicalAttack += core.ChemicalAttack;
        powerManager.ChemicalDefense += core.ChemicalDefense;
        powerManager.AtomicAttack += core.AtomicAttack;
        powerManager.AtomicDefense += core.AtomicDefense;
        powerManager.MentalAttack += core.MentalAttack;
        powerManager.MentalDefense += core.MentalDefense;
        powerManager.Speed += core.Speed;
        powerManager.CriticalDamageRate += core.CriticalDamageRate;
        powerManager.CriticalRate += core.CriticalRate;
        powerManager.PenetrationRate += core.PenetrationRate;
        powerManager.EvasionRate += core.EvasionRate;
        powerManager.DamageAbsorptionRate += core.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += core.VitalityRegenerationRate;
        powerManager.AccuracyRate += core.AccuracyRate;
        powerManager.LifestealRate += core.LifestealRate;
        powerManager.ShieldStrength += core.ShieldStrength;
        powerManager.Tenacity += core.Tenacity;
        powerManager.ResistanceRate += core.ResistanceRate;
        powerManager.ComboRate += core.ComboRate;
        powerManager.ReflectionRate += core.ReflectionRate;
        powerManager.Mana += core.Mana;
        powerManager.ManaRegenerationRate += core.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += core.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += core.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += core.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += core.ResistanceToSameFactionRate;

        ICoresRepository coresRepository = new CoresRepository();
        CoresService coresService = new CoresService(coresRepository);
        // Percent
        core = coresService.SumPowerCoresPercent(); // Giả định SumPowerCoresPercent cũng trả về một đối tượng Cores mới hoặc đã được reset
        powerManager.PercentAllHealth += core.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += core.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += core.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += core.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += core.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += core.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += core.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += core.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += core.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += core.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += core.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetWeaponsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IWeaponsGalleryRepository weaponsGalleryRepository = new WeaponsGalleryRepository();
        WeaponsGalleryService weaponsGalleryService = new WeaponsGalleryService(weaponsGalleryRepository);
        // Gallery
        Weapons weapon = weaponsGalleryService.SumPowerWeaponsGallery();
        powerManager.Power += weapon.Power;
        powerManager.Health += weapon.Health;
        powerManager.PhysicalAttack += weapon.PhysicalAttack;
        powerManager.PhysicalDefense += weapon.PhysicalDefense;
        powerManager.MagicalAttack += weapon.MagicalAttack;
        powerManager.MagicalDefense += weapon.MagicalDefense;
        powerManager.ChemicalAttack += weapon.ChemicalAttack;
        powerManager.ChemicalDefense += weapon.ChemicalDefense;
        powerManager.AtomicAttack += weapon.AtomicAttack;
        powerManager.AtomicDefense += weapon.AtomicDefense;
        powerManager.MentalAttack += weapon.MentalAttack;
        powerManager.MentalDefense += weapon.MentalDefense;
        powerManager.Speed += weapon.Speed;
        powerManager.CriticalDamageRate += weapon.CriticalDamageRate;
        powerManager.CriticalRate += weapon.CriticalRate;
        powerManager.PenetrationRate += weapon.PenetrationRate;
        powerManager.EvasionRate += weapon.EvasionRate;
        powerManager.DamageAbsorptionRate += weapon.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += weapon.VitalityRegenerationRate;
        powerManager.AccuracyRate += weapon.AccuracyRate;
        powerManager.LifestealRate += weapon.LifestealRate;
        powerManager.ShieldStrength += weapon.ShieldStrength;
        powerManager.Tenacity += weapon.Tenacity;
        powerManager.ResistanceRate += weapon.ResistanceRate;
        powerManager.ComboRate += weapon.ComboRate;
        powerManager.ReflectionRate += weapon.ReflectionRate;
        powerManager.Mana += weapon.Mana;
        powerManager.ManaRegenerationRate += weapon.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += weapon.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += weapon.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += weapon.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += weapon.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += weapon.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += weapon.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += weapon.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += weapon.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += weapon.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += weapon.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += weapon.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += weapon.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += weapon.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += weapon.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += weapon.PercentAllMentalDefense;

        IUserWeaponsRepository userWeaponsRepository = new UserWeaponsRepository();
        UserWeaponsService userWeaponsService = new UserWeaponsService(userWeaponsRepository);
        // User Weapons (Gallery)
        weapon = userWeaponsService.SumPowerUserWeapons(); // Giả định SumPowerUserWeapons cũng trả về một đối tượng Weapons mới hoặc đã được reset
        powerManager.Power += weapon.Power;
        powerManager.Health += weapon.Health;
        powerManager.PhysicalAttack += weapon.PhysicalAttack;
        powerManager.PhysicalDefense += weapon.PhysicalDefense;
        powerManager.MagicalAttack += weapon.MagicalAttack;
        powerManager.MagicalDefense += weapon.MagicalDefense;
        powerManager.ChemicalAttack += weapon.ChemicalAttack;
        powerManager.ChemicalDefense += weapon.ChemicalDefense;
        powerManager.AtomicAttack += weapon.AtomicAttack;
        powerManager.AtomicDefense += weapon.AtomicDefense;
        powerManager.MentalAttack += weapon.MentalAttack;
        powerManager.MentalDefense += weapon.MentalDefense;
        powerManager.Speed += weapon.Speed;
        powerManager.CriticalDamageRate += weapon.CriticalDamageRate;
        powerManager.CriticalRate += weapon.CriticalRate;
        powerManager.PenetrationRate += weapon.PenetrationRate;
        powerManager.EvasionRate += weapon.EvasionRate;
        powerManager.DamageAbsorptionRate += weapon.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += weapon.VitalityRegenerationRate;
        powerManager.AccuracyRate += weapon.AccuracyRate;
        powerManager.LifestealRate += weapon.LifestealRate;
        powerManager.ShieldStrength += weapon.ShieldStrength;
        powerManager.Tenacity += weapon.Tenacity;
        powerManager.ResistanceRate += weapon.ResistanceRate;
        powerManager.ComboRate += weapon.ComboRate;
        powerManager.ReflectionRate += weapon.ReflectionRate;
        powerManager.Mana += weapon.Mana;
        powerManager.ManaRegenerationRate += weapon.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += weapon.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += weapon.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += weapon.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += weapon.ResistanceToSameFactionRate;

        IWeaponsRepository weaponsRepository = new WeaponsRepository();
        WeaponsService weaponsService = new WeaponsService(weaponsRepository);
        // Percent
        weapon = weaponsService.SumPowerWeaponsPercent(); // Giả định SumPowerWeaponsPercent cũng trả về một đối tượng Weapons mới hoặc đã được reset
        powerManager.PercentAllHealth += weapon.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += weapon.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += weapon.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += weapon.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += weapon.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += weapon.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += weapon.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += weapon.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += weapon.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += weapon.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += weapon.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public PowerManager GetRobotsPower()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IRobotsGalleryRepository robotsGalleryRepository = new RobotsGalleryRepository();
        RobotsGalleryService robotsGalleryService = new RobotsGalleryService(robotsGalleryRepository);
        // Gallery
        Robots robot = robotsGalleryService.SumPowerRobotsGallery();
        powerManager.Power += robot.Power;
        powerManager.Health += robot.Health;
        powerManager.PhysicalAttack += robot.PhysicalAttack;
        powerManager.PhysicalDefense += robot.PhysicalDefense;
        powerManager.MagicalAttack += robot.MagicalAttack;
        powerManager.MagicalDefense += robot.MagicalDefense;
        powerManager.ChemicalAttack += robot.ChemicalAttack;
        powerManager.ChemicalDefense += robot.ChemicalDefense;
        powerManager.AtomicAttack += robot.AtomicAttack;
        powerManager.AtomicDefense += robot.AtomicDefense;
        powerManager.MentalAttack += robot.MentalAttack;
        powerManager.MentalDefense += robot.MentalDefense;
        powerManager.Speed += robot.Speed;
        powerManager.CriticalDamageRate += robot.CriticalDamageRate;
        powerManager.CriticalRate += robot.CriticalRate;
        powerManager.PenetrationRate += robot.PenetrationRate;
        powerManager.EvasionRate += robot.EvasionRate;
        powerManager.DamageAbsorptionRate += robot.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += robot.VitalityRegenerationRate;
        powerManager.AccuracyRate += robot.AccuracyRate;
        powerManager.LifestealRate += robot.LifestealRate;
        powerManager.ShieldStrength += robot.ShieldStrength;
        powerManager.Tenacity += robot.Tenacity;
        powerManager.ResistanceRate += robot.ResistanceRate;
        powerManager.ComboRate += robot.ComboRate;
        powerManager.ReflectionRate += robot.ReflectionRate;
        powerManager.Mana += robot.Mana;
        powerManager.ManaRegenerationRate += robot.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += robot.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += robot.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += robot.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += robot.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += robot.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += robot.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += robot.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += robot.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += robot.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += robot.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += robot.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += robot.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += robot.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += robot.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += robot.PercentAllMentalDefense;

        IUserRobotsRepository userRobotsRepository = new UserRobotsRepository();
        UserRobotsService userRobotsService = new UserRobotsService(userRobotsRepository);
        // User Robots (Gallery)
        robot = userRobotsService.SumPowerUserRobots(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += robot.Power;
        powerManager.Health += robot.Health;
        powerManager.PhysicalAttack += robot.PhysicalAttack;
        powerManager.PhysicalDefense += robot.PhysicalDefense;
        powerManager.MagicalAttack += robot.MagicalAttack;
        powerManager.MagicalDefense += robot.MagicalDefense;
        powerManager.ChemicalAttack += robot.ChemicalAttack;
        powerManager.ChemicalDefense += robot.ChemicalDefense;
        powerManager.AtomicAttack += robot.AtomicAttack;
        powerManager.AtomicDefense += robot.AtomicDefense;
        powerManager.MentalAttack += robot.MentalAttack;
        powerManager.MentalDefense += robot.MentalDefense;
        powerManager.Speed += robot.Speed;
        powerManager.CriticalDamageRate += robot.CriticalDamageRate;
        powerManager.CriticalRate += robot.CriticalRate;
        powerManager.PenetrationRate += robot.PenetrationRate;
        powerManager.EvasionRate += robot.EvasionRate;
        powerManager.DamageAbsorptionRate += robot.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += robot.VitalityRegenerationRate;
        powerManager.AccuracyRate += robot.AccuracyRate;
        powerManager.LifestealRate += robot.LifestealRate;
        powerManager.ShieldStrength += robot.ShieldStrength;
        powerManager.Tenacity += robot.Tenacity;
        powerManager.ResistanceRate += robot.ResistanceRate;
        powerManager.ComboRate += robot.ComboRate;
        powerManager.ReflectionRate += robot.ReflectionRate;
        powerManager.Mana += robot.Mana;
        powerManager.ManaRegenerationRate += robot.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += robot.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += robot.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += robot.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += robot.ResistanceToSameFactionRate;

        IRobotsRepository robotsRepository = new RobotsRepository();
        RobotsService robotsService = new RobotsService(robotsRepository);
        // Percent
        robot = robotsService.SumPowerRobotsPercent(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += robot.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += robot.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += robot.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += robot.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += robot.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += robot.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += robot.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += robot.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += robot.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += robot.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += robot.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
}