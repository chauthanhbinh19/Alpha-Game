using System.Collections.Generic;
using System.Threading.Tasks;

public class PowerManagerService : IPowerManagerService
{
    private static PowerManagerService _instance;
    private readonly IPowerManagerRepository _powerManagerRepository;

    public PowerManagerService(IPowerManagerRepository powerManagerRepository)
    {
        _powerManagerRepository = powerManagerRepository;
    }

    public static PowerManagerService Create()
    {
        if (_instance == null)
        {
            _instance = new PowerManagerService(new PowerManagerRepository());
        }
        return _instance;
    }

    public async Task InsertUserStatsAsync(string user_id)
    {
        PowerManager powerManager = await CalculateTotalPowerAsync();
        await _powerManagerRepository.InsertUserStatsAsync(user_id, powerManager);
    }
    public async Task UpdateUserStatsAsync(string user_id)
    {
        PowerManager powerManager = await CalculateTotalPowerAsync();
        await _powerManagerRepository.UpdateUserStatsAsync(user_id, powerManager);
    }
    public async Task<PowerManager> GetUserStatsAsync(string user_id)
    {
        return await _powerManagerRepository.GetUserStatsAsync(user_id);
    }
    public async Task<PowerManager> CalculateTotalPowerAsync()
    {
        PowerManager totalPower = new PowerManager();

        PowerManager achievementsPower = await GetAchievementsPowerAsync();
        AddPower(totalPower, achievementsPower);

        // Lấy sức mạnh từ avatars
        PowerManager avatarsPower = await GetAvatarsPowerAsync();
        AddPower(totalPower, avatarsPower);

        // Lấy sức mạnh từ books
        PowerManager booksPower = await GetBooksPowerAsync();
        AddPower(totalPower, booksPower);

        // Lấy sức mạnh từ borders
        PowerManager bordersPower = await GetBordersPowerAsync();
        AddPower(totalPower, bordersPower);

        // Lấy sức mạnh từ card heroes
        PowerManager cardHeroesPower = await GetCardHeroesPowerAsync();
        AddPower(totalPower, cardHeroesPower);

        // Lấy sức mạnh từ card captains
        PowerManager cardCaptainsPower = await GetCardCaptainsPowerAsync();
        AddPower(totalPower, cardCaptainsPower);

        // Lấy sức mạnh từ card colonels
        PowerManager cardColonelsPower = await GetCardColonelsPowerAsync();
        AddPower(totalPower, cardColonelsPower);

        // Lấy sức mạnh từ card generals
        PowerManager cardGeneralsPower = await GetCardGeneralsPowerAsync();
        AddPower(totalPower, cardGeneralsPower);

        // Lấy sức mạnh từ card admirals
        PowerManager cardAdmiralsPower = await GetCardAdmiralsPowerAsync();
        AddPower(totalPower, cardAdmiralsPower);

        // Lấy sức mạnh từ card monsters
        PowerManager cardMonstersPower = await GetCardMonstersPowerAsync();
        AddPower(totalPower, cardMonstersPower);

        // Lấy sức mạnh từ card militaries
        PowerManager cardMilitariesPower = await GetCardMilitariesPowerAsync();
        AddPower(totalPower, cardMilitariesPower);

        // Lấy sức mạnh từ card spell
        PowerManager cardSpellsPower = await GetCardSpellsPowerAsync();
        AddPower(totalPower, cardSpellsPower);

        // Lấy sức mạnh từ collaborations
        PowerManager collaborationsPower = await GetCollaborationsPowerAsync();
        AddPower(totalPower, collaborationsPower);

        // Lấy sức mạnh từ collaboration equipments
        PowerManager collaborationEquipmentsPower = await GetCollaborationEquipmentsPowerAsync();
        AddPower(totalPower, collaborationEquipmentsPower);

        // Lấy sức mạnh từ equipments
        PowerManager equipmentsPower = await GetEquipmentsPowerAsync();
        AddPower(totalPower, equipmentsPower);

        // Lấy sức mạnh từ magic formation circles
        PowerManager magicFormationCirclesPower = await GetMagicFormationCirclesPowerAsync();
        AddPower(totalPower, magicFormationCirclesPower);

        // Lấy sức mạnh từ relics
        PowerManager relicsPower = await GetRelicsPowerAsync();
        AddPower(totalPower, relicsPower);

        // Lấy sức mạnh từ medals
        PowerManager medalsPower = await GetMedalsPowerAsync();
        AddPower(totalPower, medalsPower);

        // Lấy sức mạnh từ pets
        PowerManager petsPower = await GetPetsPowerAsync();
        AddPower(totalPower, petsPower);

        // Lấy sức mạnh từ symbols
        PowerManager symbolsPower = await GetSymbolsPowerAsync();
        AddPower(totalPower, symbolsPower);

        // Lấy sức mạnh từ skills
        PowerManager skillsPower = await GetSkillsPowerAsync();
        AddPower(totalPower, skillsPower);

        // Lấy sức mạnh từ titles
        PowerManager titlesPower = await GetTitlesPowerAsync();
        AddPower(totalPower, titlesPower);

        // Lấy sức mạnh từ talismans
        PowerManager talismansPower = await GetTalismansPowerAsync();
        AddPower(totalPower, talismansPower);

        // Lấy sức mạnh từ puppets
        PowerManager puppetsPower = await GetPuppetsPowerAsync();
        AddPower(totalPower, puppetsPower);

        // Lấy sức mạnh từ alchemies
        PowerManager alchemiesPower = await GetAlchemiesPowerAsync();
        AddPower(totalPower, alchemiesPower);

        // Lấy sức mạnh từ forges
        PowerManager forgesPower = await GetForgesPowerAsync();
        AddPower(totalPower, forgesPower);

        // Lấy sức mạnh từ card lives
        PowerManager cardLivesPower = await GetCardLivesPowerAsync();
        AddPower(totalPower, cardLivesPower);

        // Lấy sức mạnh từ artworks
        PowerManager artworksPower = await GetArtworksPowerAsync();
        AddPower(totalPower, artworksPower);

        // Lấy sức mạnh từ spirit beasts
        PowerManager spiritBeastsPower = await GetSpiritBeastsPowerAsync();
        AddPower(totalPower, spiritBeastsPower);

        // Lấy sức mạnh từ spirit cards
        PowerManager spiritCardsPower = await GetSpiritCardsPowerAsync();
        AddPower(totalPower, spiritCardsPower);

        // Lấy sức mạnh từ vehicles
        PowerManager vehiclesPower = await GetVehiclesPowerAsync();
        AddPower(totalPower, vehiclesPower);

        // Lấy sức mạnh từ architectures
        PowerManager architecturePower = await GetArchitecturesPowerAsync();
        AddPower(totalPower, architecturePower);

        // Lấy sức mạnh từ technologies
        PowerManager techonogiesPower = await GetTechnologiesPowerAsync();
        AddPower(totalPower, techonogiesPower);

        // Lấy sức mạnh từ cards
        PowerManager cardPower = await GetCardsPowerAsync();
        AddPower(totalPower, cardPower);

        // Lấy sức mạnh từ cores
        PowerManager coresPower = await GetCoresPowerAsync();
        AddPower(totalPower, coresPower);

        // Lấy sức mạnh từ weapons
        PowerManager weaponsPower = await GetWeaponsPowerAsync();
        AddPower(totalPower, weaponsPower);

        // Lấy sức mạnh từ robots
        PowerManager robotsPower = await GetRobotsPowerAsync();
        AddPower(totalPower, robotsPower);

        // Lấy sức mạnh từ robots
        PowerManager badgesPower = await GetBadgesPowerAsync();
        AddPower(totalPower, badgesPower);

        // Lấy sức mạnh từ robots
        PowerManager mechaBeastsPower = await GetMechaBeastsPowerAsync();
        AddPower(totalPower, mechaBeastsPower);

        // Lấy sức mạnh từ robots
        PowerManager runesPower = await GetRunesPowerAsync();
        AddPower(totalPower, runesPower);

        // Lấy sức mạnh từ robots
        PowerManager furnituresPower = await GetFurnituresPowerAsync();
        AddPower(totalPower, furnituresPower);

        // Lấy sức mạnh từ robots
        PowerManager foodsPower = await GetFoodsPowerAsync();
        AddPower(totalPower, foodsPower);

        // Lấy sức mạnh từ robots
        PowerManager beveragesPower = await GetBeveragesPowerAsync();
        AddPower(totalPower, beveragesPower);

        // Lấy sức mạnh từ robots
        PowerManager buildingsPower = await GetBuildingsPowerAsync();
        AddPower(totalPower, buildingsPower);

        // Lấy sức mạnh từ robots
        PowerManager plantsPower = await GetPlantsPowerAsync();
        AddPower(totalPower, plantsPower);

        // Lấy sức mạnh từ robots
        PowerManager fashionsPower = await GetFashionsPowerAsync();
        AddPower(totalPower, fashionsPower);

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
    public async Task<PowerManager> GetAchievementsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        // User Achievements
        IUserAchievementsRepository userAchievementsRepository = new UserAchievementsRepository();
        UserAchievementsService userAchievementsService = new UserAchievementsService(userAchievementsRepository);
        Achievements userAchievements = await userAchievementsService.SumPowerUserAchievementsAsync();

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
        Achievements percentAchievements = await achievementsService.SumPowerAchievementsPercentAsync();

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
    public async Task<PowerManager> GetAvatarsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        // Gallery Avatars
        IAvatarsGalleryRepository avatarsGalleryRepository = new AvatarsGalleryRepository();
        AvatarsGalleryService avatarsGalleryService = new AvatarsGalleryService(avatarsGalleryRepository);
        Avatars galleryAvatars = await avatarsGalleryService.SumPowerAvatarsGalleryAsync();

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
        Avatars userAvatars = await userAvatarsService.SumPowerUserAvatarsAsync();

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
        Avatars percentAvatars = await avatarsService.SumPowerAvatarsPercentAsync();

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
    public async Task<PowerManager> GetBordersPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        // Gallery Borders
        IBordersGalleryRepository bordersGalleryRepository = new BordersGalleryRepository(); // Note: This was named booksGalleryRepository in the original. Corrected for clarity.
        BordersGalleryService bordersGalleryService = new BordersGalleryService(bordersGalleryRepository); // Note: This was named booksGalleryService in the original. Corrected for clarity.
        Borders galleryBorders = await bordersGalleryService.SumPowerBordersGalleryAsync();

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
        Borders userBorders = await userBordersService.SumPowerUserBordersAsync();

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
        Borders percentBorders = await bordersService.SumPowerBordersPercentAsync();

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
    public async Task<PowerManager> GetBooksPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        IBooksGalleryRepository booksGalleryRepository = new BooksGalleryRepository();
        BooksGalleryService booksGalleryService = new BooksGalleryService(booksGalleryRepository);

        //Gallery
        Books books = await booksGalleryService.SumPowerBooksGalleryAsync();

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
    public async Task<PowerManager> GetCardHeroesPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        ICardHeroesGalleryRepository cardHeroesGalleryRepository = new CardHeroesGalleryRepository();
        CardHeroesGalleryService cardHeroesGalleryService = new CardHeroesGalleryService(cardHeroesGalleryRepository);

        // Gallery
        CardHeroes cardHeroes = await cardHeroesGalleryService.SumPowerCardHeroesGalleryAsync();

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
    public async Task<PowerManager> GetCardCaptainsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        ICardCaptainsGalleryRepository cardCaptainsGalleryRepository = new CardCaptainsGalleryRepository();
        CardCaptainsGalleryService cardCaptainsGalleryService = new CardCaptainsGalleryService(cardCaptainsGalleryRepository);

        // Gallery
        CardCaptains cardCaptains = await cardCaptainsGalleryService.SumPowerCardCaptainsGalleryAsync();

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
    public async Task<PowerManager> GetCardColonelsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        ICardColonelsGalleryRepository cardColonelsGalleryRepository = new CardColonelsGalleryRepository();
        CardColonelsGalleryService cardColonelsGalleryService = new CardColonelsGalleryService(cardColonelsGalleryRepository);

        //Gallery
        CardColonels cardColonels = await cardColonelsGalleryService.SumPowerCardColonelsGalleryAsync();

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
    public async Task<PowerManager> GetCardGeneralsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        ICardGeneralsGalleryRepository cardGeneralsGalleryRepository = new CardGeneralsGalleryRepository();
        CardGeneralsGalleryService cardGeneralsGalleryService = new CardGeneralsGalleryService(cardGeneralsGalleryRepository);

        //Gallery
        CardGenerals cardGenerals = await cardGeneralsGalleryService.SumPowerCardGeneralsGalleryAsync();

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
    public async Task<PowerManager> GetCardAdmiralsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        ICardAdmiralsGalleryRepository cardAdmiralsGalleryRepository = new CardAdmiralsGalleryRepository();
        CardAdmiralsGalleryService cardAdmiralsGalleryService = new CardAdmiralsGalleryService(cardAdmiralsGalleryRepository);

        //Gallery
        CardAdmirals cardAdmirals = await cardAdmiralsGalleryService.SumPowerCardAdmiralsGalleryAsync();

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
    public async Task<PowerManager> GetCardMonstersPowerAsync()
    {
        PowerManager powerManager = new PowerManager();
        // CardMonsters cardMonsters = new CardMonsters();
        ICardMonstersGalleryRepository cardMonstersGalleryRepository = new CardMonstersGalleryRepository();
        CardMonstersGalleryService cardMonstersGalleryService = new CardMonstersGalleryService(cardMonstersGalleryRepository);
        //Gallery
        CardMonsters cardMonsters = await cardMonstersGalleryService.SumPowerCardMonstersGalleryAsync();
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
    public async Task<PowerManager> GetCardMilitariesPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        // CardMilitary cardMilitary = new CardMilitary();
        ICardMilitariesGalleryRepository cardMilitaryGalleryRepository = new CardMilitariesGalleryRepository();
        CardMilitariesGalleryService cardMilitaryGalleryService = new CardMilitariesGalleryService(cardMilitaryGalleryRepository);
        //Gallery
        CardMilitaries cardMilitary = await cardMilitaryGalleryService.SumPowerCardMilitariesGalleryAsync();

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
    public async Task<PowerManager> GetCardSpellsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        // CardSpell cardSpell = new CardSpell();
        ICardSpellsGalleryRepository cardSpellGalleryRepository = new CardSpellsGalleryRepository();
        CardSpellsGalleryService cardSpellGalleryService = new CardSpellsGalleryService(cardSpellGalleryRepository);
        //Gallery
        CardSpells cardSpell = await cardSpellGalleryService.SumPowerCardSpellsGalleryAsync();

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
    public async Task<PowerManager> GetCollaborationsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();

        // Collaboration collaboration = new Collaboration();
        ICollaborationsGalleryRepository collaborationGalleryRepository = new CollaborationsGalleryRepository();
        CollaborationsGalleryService collaborationGalleryService = new CollaborationsGalleryService(collaborationGalleryRepository);
        // Gallery tổng hợp sức mạnh từ Collaboration Gallery
        Collaborations collaboration = await collaborationGalleryService.SumPowerCollaborationsGalleryAsync();
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

        IUserCollaborationsRepository userCollaborationRepository = new UserCollaborationsRepository();
        UserCollaborationsService userCollaborationService = new UserCollaborationsService(userCollaborationRepository);
        // Gallery tổng hợp sức mạnh từ User Collaborations
        collaboration = await userCollaborationService.SumPowerUserCollaborationsAsync();
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

        ICollaborationsRepository collaborationRepository = new CollaborationsRepository();
        CollaborationsService collaborationService = new CollaborationsService(collaborationRepository);
        // Phần cộng dồn percent từ Collaboration Percent
        collaboration = await collaborationService.SumPowerCollaborationsPercentAsync();
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
    public async Task<PowerManager> GetCollaborationEquipmentsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();
        // CollaborationEquipment collaborationEquipment = new CollaborationEquipment();
        ICollaborationEquipmentsGalleryRepository collaborationEquipmentGalleryRepository = new CollaborationEquipmentsGalleryRepository();
        CollaborationEquipmentsGalleryService collaborationEquipmentGalleryService = new CollaborationEquipmentsGalleryService(collaborationEquipmentGalleryRepository);
        // Sum power from Gallery Equipments
        CollaborationEquipments collaborationEquipment = await collaborationEquipmentGalleryService.SumPowerCollaborationEquipmentsGalleryAsync();
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

        IUserCollaborationEquipmentsRepository userCollaborationEquipmentRepository = new UserCollaborationEquipmentsRepository();
        UserCollaborationEquipmentsService userCollaborationEquipmentService = new UserCollaborationEquipmentsService(userCollaborationEquipmentRepository);
        // Sum power from User Collaboration Equipments
        collaborationEquipment = await userCollaborationEquipmentService.SumPowerUserCollaborationEquipmentsAsync();
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
    public async Task<PowerManager> GetEquipmentsPowerAsync()
    {
        PowerManager powerManager = new PowerManager();
        // Equipments equipments = new Equipments();
        IEquipmentsGalleryRepository equipmentsGalleryRepository = new EquipmentsGalleryRepository();
        EquipmentsGalleryService equipmentsGalleryService = new EquipmentsGalleryService(equipmentsGalleryRepository);
        // Gallery Equipments power sum
        Equipments equipments = await equipmentsGalleryService.SumPowerEquipmentsGalleryAsync();

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
    public async Task<PowerManager> GetMagicFormationCirclesPowerAsync()
    {
        PowerManager powerManager = new PowerManager();
        // MagicFormationCircle magicFormationCircle = new MagicFormationCircle();
        IMagicFormationCirclesGalleryRepository magicFormationCircleGalleryRepository = new MagicFormationCirclesGalleryRepository();
        MagicFormationCirclesGalleryService magicFormationCircleGalleryService = new MagicFormationCirclesGalleryService(magicFormationCircleGalleryRepository);
        // Gallery
        MagicFormationCircles magicFormationCircle = await magicFormationCircleGalleryService.SumPowerMagicFormationCirclesGalleryAsync();
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

        IUserMagicFormationCirclesRepository userMagicFormationCircleRepository = new UserMagicFormationCirclesRepository();
        UserMagicFormationCirclesService userMagicFormationCircleService = new UserMagicFormationCirclesService(userMagicFormationCircleRepository);
        // User
        magicFormationCircle = await userMagicFormationCircleService.SumPowerUserMagicFormationCirclesAsync();
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

        IMagicFormationCirclesRepository magicFormationCircleRepository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService magicFormationCircleService = new MagicFormationCirclesService(magicFormationCircleRepository);
        // Percent
        magicFormationCircle = await magicFormationCircleService.SumPowerMagicFormationCirclesPercentAsync();
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
    public async Task<PowerManager> GetRelicsPowerAsync()
    {
        // Relics relics = new Relics();
        PowerManager powerManager = new PowerManager();

        IRelicsGalleryRepository relicsGalleryRepository = new RelicsGalleryRepository();
        RelicsGalleryService relicsGalleryService = new RelicsGalleryService(relicsGalleryRepository);
        // Gallery
        Relics relics = await relicsGalleryService.SumPowerRelicsGalleryAsync();
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
        relics = await userRelicsService.SumPowerUserRelicsAsync();
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
        relics = await relicsService.SumPowerRelicsPercentAsync();
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
    public async Task<PowerManager> GetMedalsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Medals medals = new Medals();
        IMedalsGalleryRepository medalsGalleryRepository = new MedalsGalleryRepository();
        MedalsGalleryService medalsGalleryService = new MedalsGalleryService(medalsGalleryRepository);
        // Gallery
        Medals medals = await medalsGalleryService.SumPowerMedalsGalleryAsync();
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
        medals = await userMedalsService.SumPowerUserMedalsAsync(); // Giả định SumPowerUserMedals cũng trả về một đối tượng Medals mới hoặc đã được reset
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
        medals = await medalsService.SumPowerMedalsPercentAsync(); // Giả định SumPowerMedalsPercent cũng trả về một đối tượng Medals mới hoặc đã được reset
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
    public async Task<PowerManager> GetPetsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Pets pets = new Pets();
        IPetsGalleryRepository petsGalleryRepository = new PetsGalleryRepository();
        PetsGalleryService petsGalleryService = new PetsGalleryService(petsGalleryRepository);
        // Gallery
        Pets pets = await petsGalleryService.SumPowerPetsGalleryAsync();
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
    public async Task<PowerManager> GetSymbolsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Symbols symbols = new Symbols();
        ISymbolsGalleryRepository symbolsGalleryRepository = new SymbolsGalleryRepository();
        SymbolsGalleryService symbolsGalleryService = new SymbolsGalleryService(symbolsGalleryRepository);
        // Gallery
        Symbols symbols = await symbolsGalleryService.SumPowerSymbolsGalleryAsync();
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
        symbols = await userSymbolsService.SumPowerUserSymbolsAsync(); // Giả định SumPowerUserSymbols cũng trả về một đối tượng Symbols mới hoặc đã được reset
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
        symbols = await symbolsService.SumPowerSymbolsPercentAsync(); // Giả định SumPowerSymbolsPercent cũng trả về một đối tượng Symbols mới hoặc đã được reset
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
    public async Task<PowerManager> GetSkillsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Skills skills = new Skills();
        ISkillsGalleryRepository skillsGalleryRepository = new SkillsGalleryRepository();
        SkillsGalleryService skillsGalleryService = new SkillsGalleryService(skillsGalleryRepository);
        // Gallery
        Skills skills = await skillsGalleryService.SumPowerSkillsGalleryAsync();
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
    public async Task<PowerManager> GetTitlesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ITitlesGalleryRepository titlesGalleryRepository = new TitlesGalleryRepository();
        TitlesGalleryService titlesGalleryService = new TitlesGalleryService(titlesGalleryRepository);
        // Gallery
        Titles titles = await titlesGalleryService.SumPowerTitlesGalleryAsync();
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
        titles = await userTitlesService.SumPowerUserTitlesAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
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
        titles = await titlesService.SumPowerTitlesPercentAsync(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
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
    public async Task<PowerManager> GetTalismansPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Talisman talisman = new Talisman();
        ITalismansGalleryRepository talismanGalleryRepository = new TalismansGalleryRepository();
        TalismansGalleryService talismanGalleryService = new TalismansGalleryService(talismanGalleryRepository);
        // Gallery
        Talismans talisman = await talismanGalleryService.SumPowerTalismansGalleryAsync();
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

        IUserTalismansRepository userTalismanRepository = new UserTalismansRepository();
        UserTalismansService userTalismanService = new UserTalismansService(userTalismanRepository);
        // User
        talisman = await userTalismanService.SumPowerUserTalismansAsync(); // Giả định SumPowerUserTalisman cũng trả về một đối tượng Talisman mới hoặc đã được reset
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

        ITalismansRepository talismanRepository = new TalismansRepository();
        TalismansService talismanService = new TalismansService(talismanRepository);
        // Percent
        talisman = await talismanService.SumPowerTalismansPercentAsync(); // Giả định SumPowerTalismanPercent cũng trả về một đối tượng Talisman mới hoặc đã được reset
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
    public async Task<PowerManager> GetPuppetsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Puppet puppet = new Puppet();
        IPuppetsGalleryRepository puppetGalleryRepository = new PuppetsGalleryRepository();
        PuppetsGalleryService puppetGalleryService = new PuppetsGalleryService(puppetGalleryRepository);
        // Gallery
        Puppets puppet = await puppetGalleryService.SumPowerPuppetsGalleryAsync();
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

        IUserPuppetsRepository userPuppetRepository = new UserPuppetsRepository();
        UserPuppetsService userPuppetService = new UserPuppetsService(userPuppetRepository);
        // User
        puppet = await userPuppetService.SumPowerUserPuppetsAsync(); // Giả định SumPowerUserPuppet cũng trả về một đối tượng Puppet mới hoặc đã được reset
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

        IPuppetsRepository puppetRepository = new PuppetsRepository();
        PuppetsService puppetService = new PuppetsService(puppetRepository);
        // Percent
        puppet = await puppetService.SumPowerPuppetsPercentAsync(); // Giả định SumPowerPuppetPercent cũng trả về một đối tượng Puppet mới hoặc đã được reset
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
    public async Task<PowerManager> GetAlchemiesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Alchemy alchemy = new Alchemy();
        IAlchemiesGalleryRepository alchemyGalleryRepository = new AlchemiesGalleryRepository();
        AlchemiesGalleryService alchemyGalleryService = new AlchemiesGalleryService(alchemyGalleryRepository);
        // Gallery
        Alchemies alchemy = await alchemyGalleryService.SumPowerAlchemyGalleryAsync();
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

        IUserAlchemiesRepository userAlchemyRepository = new UserAlchemiesRepository();
        UserAlchemiesService userAlchemyService = new UserAlchemiesService(userAlchemyRepository);
        // User
        alchemy = await userAlchemyService.SumPowerUserAlchemiesAsync(); // Giả định SumPowerUserAlchemy cũng trả về một đối tượng Alchemy mới hoặc đã được reset
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

        IAlchemiesRepository alchemyRepository = new AlchemiesRepository();
        AlchemiesService alchemyService = new AlchemiesService(alchemyRepository);
        // Percent
        alchemy = await alchemyService.SumPowerAlchemiesPercentAsync(); // Giả định SumPowerAlchemyPercent cũng trả về một đối tượng Alchemy mới hoặc đã được reset
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
    public async Task<PowerManager> GetForgesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Forge forge = new Forge();
        IForgesGalleryRepository forgeGalleryRepository = new ForgesGalleryRepository();
        ForgesGalleryService forgeGalleryService = new ForgesGalleryService(forgeGalleryRepository);
        // Gallery
        Forges forge = await forgeGalleryService.SumPowerForgesGalleryAsync();
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

        IUserForgesRepository userForgeRepository = new UserForgesRepository();
        UserForgesService userForgeService = new UserForgesService(userForgeRepository);
        // User
        forge = await userForgeService.SumPowerUserForgesAsync(); // Giả định SumPowerUserForge cũng trả về một đối tượng Forge mới hoặc đã được reset
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

        IForgesRepository forgeRepository = new ForgesRepository();
        ForgesService forgeService = new ForgesService(forgeRepository);
        // Percent
        forge = await forgeService.SumPowerForgesPercentAsync(); // Giả định SumPowerForgePercent cũng trả về một đối tượng Forge mới hoặc đã được reset
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
    public async Task<PowerManager> GetCardLivesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // CardLife cardLife = new CardLife();
        ICardLivesGalleryRepository cardLifeGalleryRepository = new CardLivesGalleryRepository();
        CardLivesGalleryService cardLifeGalleryService = new CardLivesGalleryService(cardLifeGalleryRepository);
        // Gallery
        CardLives cardLife = await cardLifeGalleryService.SumPowerCardLivesGalleryAsync();
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

        IUserCardLivesRepository userCardLifeRepository = new UserCardLivesRepository();
        UserCardLivesService userCardLifeService = new UserCardLivesService(userCardLifeRepository);
        // User
        cardLife = await userCardLifeService.SumPowerUserCardLivesAsync(); // Giả định SumPowerUserCardLife cũng trả về một đối tượng CardLife mới hoặc đã được reset
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

        ICardLivesRepository cardLifeRepository = new CardLivesRepository();
        CardLivesService cardLifeService = new CardLivesService(cardLifeRepository);
        // Percent
        cardLife = await cardLifeService.SumPowerCardLivesPercentAsync(); // Giả định SumPowerCardLifePercent cũng trả về một đối tượng CardLife mới hoặc đã được reset
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
    public async Task<PowerManager> GetArtworksPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Artwork Artwork = new Artwork();
        IArtworksGalleryRepository artworkGalleryRepository = new ArtworksGalleryRepository();
        ArtworksGalleryService artworkGalleryService = new ArtworksGalleryService(artworkGalleryRepository);
        // Gallery
        Artworks artwork = await artworkGalleryService.SumPowerArtworksGalleryAsync();
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

        IUserArtworksRepository userArtworkRepository = new UserArtworksRepository();
        UserArtworksService userArtworkService = new UserArtworksService(userArtworkRepository);
        // User
        artwork = await userArtworkService.SumPowerUserArtworksAsync(); // Giả định SumPowerUserartwork cũng trả về một đối tượng artwork mới hoặc đã được reset
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

        IArtworksRepository artworkRepository = new ArtworksRepository();
        ArtworksService artworkService = new ArtworksService(artworkRepository);
        // Percent
        artwork = await artworkService.SumPowerArtworksPercentAsync(); // Giả định SumPowerartworkPercent cũng trả về một đối tượng artwork mới hoặc đã được reset
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
    public async Task<PowerManager> GetSpiritBeastsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        ISpiritBeastsGalleryRepository spiritBeastGalleryRepository = new SpiritBeastsGalleryRepository();
        SpiritBeastsGalleryService SpiritBeastGalleryService = new SpiritBeastsGalleryService(spiritBeastGalleryRepository);
        // Gallery
        SpiritBeasts spiritBeast = await SpiritBeastGalleryService.SumPowerSpiritBeastsGalleryAsync();
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

        IUserSpiritBeastsRepository userSpiritBeastRepository = new UserSpiritBeastsRepository();
        UserSpiritBeastsService userSpiritBeastService = new UserSpiritBeastsService(userSpiritBeastRepository);
        // User SpiritBeast (Gallery)
        spiritBeast = await userSpiritBeastService.SumPowerUserSpiritBeastsAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
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

        ISpiritBeastsRepository spiritBeastRepository = new SpiritBeastsRepository();
        SpiritBeastsService spiritBeastService = new SpiritBeastsService(spiritBeastRepository);
        // Percent
        spiritBeast = await spiritBeastService.SumPowerSpiritBeastsPercentAsync(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
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
    public async Task<PowerManager> GetSpiritCardsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        ISpiritCardsGalleryRepository spiritCardGalleryRepository = new SpiritCardsGalleryRepository();
        SpiritCardsGalleryService spiritCardGalleryService = new SpiritCardsGalleryService(spiritCardGalleryRepository);
        // Gallery
        SpiritCards spiritCard = await spiritCardGalleryService.SumPowerSpiritCardsGalleryAsync();
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

        IUserSpiritCardsRepository userSpiritCardRepository = new UserSpiritCardsRepository();
        UserSpiritCardsService userSpiritCardService = new UserSpiritCardsService(userSpiritCardRepository);
        // User SpiritBeast (Gallery)
        spiritCard = await userSpiritCardService.SumPowerUserSpiritCardsAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
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

        ISpiritCardsRepository spiritCardRepository = new SpiritCardsRepository();
        SpiritCardsService spiritCardService = new SpiritCardsService(spiritCardRepository);
        // Percent
        spiritCard = await spiritCardService.SumPowerSpiritCardsPercentAsync(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
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
    public async Task<PowerManager> GetVehiclesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        IVehiclesGalleryRepository vehicleGalleryRepository = new VehiclesGalleryRepository();
        VehiclesGalleryService vehicleGalleryService = new VehiclesGalleryService(vehicleGalleryRepository);
        // Gallery
        Vehicles vehicle = await vehicleGalleryService.SumPowerVehiclesGalleryAsync();
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

        IUserVehiclesRepository userVehicleRepository = new UserVehiclesRepository();
        UserVehiclesService userVehicleService = new UserVehiclesService(userVehicleRepository);
        // User SpiritBeast (Gallery)
        vehicle = await userVehicleService.SumPowerUserVehiclesAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
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
        vehicle = await vehiclesService.SumPowerVehiclesPercentAsync(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
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
    public async Task<PowerManager> GetCardsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ICardsGalleryRepository cardsGalleryRepository = new CardsGalleryRepository();
        CardsGalleryService cardsGalleryService = new CardsGalleryService(cardsGalleryRepository);
        // Gallery
        Cards card = await cardsGalleryService.SumPowerCardsGalleryAsync();
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
        card = await userCardsService.SumPowerUserCardsAsync(); // Giả định SumPowerUserCards cũng trả về một đối tượng Cards mới hoặc đã được reset
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
        card = await cardsService.SumPowerCardsPercentAsync(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
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
    public async Task<PowerManager> GetTechnologiesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ITechnologiesGalleryRepository technologiesGalleryRepository = new TechnologiesGalleryRepository();
        TechnologiesGalleryService technologiesGalleryService = new TechnologiesGalleryService(technologiesGalleryRepository);
        // Gallery
        Technologies technology = await technologiesGalleryService.SumPowerTechnologiesGalleryAsync();
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
        technology = await userTechnologiesService.SumPowerUserTechnologiesAsync(); // Giả định SumPowerUserTechnologies cũng trả về một đối tượng Technologies mới hoặc đã được reset
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
        technology = await technologiesService.SumPowerTechnologiesPercentAsync(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
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
    public async Task<PowerManager> GetArchitecturesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IArchitecturesGalleryRepository architecturesGalleryRepository = new ArchitecturesGalleryRepository();
        ArchitecturesGalleryService architecturesGalleryService = new ArchitecturesGalleryService(architecturesGalleryRepository);
        // Gallery
        Architectures architecture =  await architecturesGalleryService.SumPowerArchitecturesGalleryAsync();
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
        architecture = await userArchitecturesService.SumPowerUserArchitecturesAsync(); // Giả định SumPowerUserArchitectures cũng trả về một đối tượng Architectures mới hoặc đã được reset
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
        architecture =  await architecturesService.SumPowerArchitecturesPercentAsync(); // Giả định SumPowerTitlesPercent cũng trả về một đối tượng Titles mới hoặc đã được reset
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
    public async Task<PowerManager> GetCoresPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        ICoresGalleryRepository coresGalleryRepository = new CoresGalleryRepository();
        CoresGalleryService coresGalleryService = new CoresGalleryService(coresGalleryRepository);
        // Gallery
        Cores core =  await coresGalleryService.SumPowerCoresGalleryAsync();
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
        core = await userCoresService.SumPowerUserCoresAsync(); // Giả định SumPowerUserCores cũng trả về một đối tượng Cores mới hoặc đã được reset
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
        core =  await coresService.SumPowerCoresPercentAsync(); // Giả định SumPowerCoresPercent cũng trả về một đối tượng Cores mới hoặc đã được reset
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
    public async Task<PowerManager> GetWeaponsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IWeaponsGalleryRepository weaponsGalleryRepository = new WeaponsGalleryRepository();
        WeaponsGalleryService weaponsGalleryService = new WeaponsGalleryService(weaponsGalleryRepository);
        // Gallery
        Weapons weapon =  await weaponsGalleryService.SumPowerWeaponsGalleryAsync();
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
        weapon = await userWeaponsService.SumPowerUserWeaponsAsync(); // Giả định SumPowerUserWeapons cũng trả về một đối tượng Weapons mới hoặc đã được reset
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
        weapon =  await weaponsService.SumPowerWeaponsPercentAsync(); // Giả định SumPowerWeaponsPercent cũng trả về một đối tượng Weapons mới hoặc đã được reset
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
    public async Task<PowerManager> GetRobotsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IRobotsGalleryRepository robotsGalleryRepository = new RobotsGalleryRepository();
        RobotsGalleryService robotsGalleryService = new RobotsGalleryService(robotsGalleryRepository);
        // Gallery
        Robots robot =  await robotsGalleryService.SumPowerRobotsGalleryAsync();
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
        robot = await userRobotsService.SumPowerUserRobotsAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
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
        robot =  await robotsService.SumPowerRobotsPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
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
    public async Task<PowerManager> GetBadgesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IBadgesGalleryRepository badgesGalleryRepository = new BadgesGalleryRepository();
        BadgesGalleryService badgesGalleryService = new BadgesGalleryService(badgesGalleryRepository);
        // Gallery
        Badges badge =  await badgesGalleryService.SumPowerBadgesGalleryAsync();
        powerManager.Power += badge.Power;
        powerManager.Health += badge.Health;
        powerManager.PhysicalAttack += badge.PhysicalAttack;
        powerManager.PhysicalDefense += badge.PhysicalDefense;
        powerManager.MagicalAttack += badge.MagicalAttack;
        powerManager.MagicalDefense += badge.MagicalDefense;
        powerManager.ChemicalAttack += badge.ChemicalAttack;
        powerManager.ChemicalDefense += badge.ChemicalDefense;
        powerManager.AtomicAttack += badge.AtomicAttack;
        powerManager.AtomicDefense += badge.AtomicDefense;
        powerManager.MentalAttack += badge.MentalAttack;
        powerManager.MentalDefense += badge.MentalDefense;
        powerManager.Speed += badge.Speed;
        powerManager.CriticalDamageRate += badge.CriticalDamageRate;
        powerManager.CriticalRate += badge.CriticalRate;
        powerManager.PenetrationRate += badge.PenetrationRate;
        powerManager.EvasionRate += badge.EvasionRate;
        powerManager.DamageAbsorptionRate += badge.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += badge.VitalityRegenerationRate;
        powerManager.AccuracyRate += badge.AccuracyRate;
        powerManager.LifestealRate += badge.LifestealRate;
        powerManager.ShieldStrength += badge.ShieldStrength;
        powerManager.Tenacity += badge.Tenacity;
        powerManager.ResistanceRate += badge.ResistanceRate;
        powerManager.ComboRate += badge.ComboRate;
        powerManager.ReflectionRate += badge.ReflectionRate;
        powerManager.Mana += badge.Mana;
        powerManager.ManaRegenerationRate += badge.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += badge.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += badge.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += badge.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += badge.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += badge.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += badge.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += badge.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += badge.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += badge.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += badge.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += badge.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += badge.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += badge.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += badge.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += badge.PercentAllMentalDefense;

        IUserBadgesRepository userBadgesRepository = new UserBadgesRepository();
        UserBadgesService userBadgesService = new UserBadgesService(userBadgesRepository);
        // User Robots (Gallery)
        badge = await userBadgesService.SumPowerUserBadgesAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += badge.Power;
        powerManager.Health += badge.Health;
        powerManager.PhysicalAttack += badge.PhysicalAttack;
        powerManager.PhysicalDefense += badge.PhysicalDefense;
        powerManager.MagicalAttack += badge.MagicalAttack;
        powerManager.MagicalDefense += badge.MagicalDefense;
        powerManager.ChemicalAttack += badge.ChemicalAttack;
        powerManager.ChemicalDefense += badge.ChemicalDefense;
        powerManager.AtomicAttack += badge.AtomicAttack;
        powerManager.AtomicDefense += badge.AtomicDefense;
        powerManager.MentalAttack += badge.MentalAttack;
        powerManager.MentalDefense += badge.MentalDefense;
        powerManager.Speed += badge.Speed;
        powerManager.CriticalDamageRate += badge.CriticalDamageRate;
        powerManager.CriticalRate += badge.CriticalRate;
        powerManager.PenetrationRate += badge.PenetrationRate;
        powerManager.EvasionRate += badge.EvasionRate;
        powerManager.DamageAbsorptionRate += badge.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += badge.VitalityRegenerationRate;
        powerManager.AccuracyRate += badge.AccuracyRate;
        powerManager.LifestealRate += badge.LifestealRate;
        powerManager.ShieldStrength += badge.ShieldStrength;
        powerManager.Tenacity += badge.Tenacity;
        powerManager.ResistanceRate += badge.ResistanceRate;
        powerManager.ComboRate += badge.ComboRate;
        powerManager.ReflectionRate += badge.ReflectionRate;
        powerManager.Mana += badge.Mana;
        powerManager.ManaRegenerationRate += badge.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += badge.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += badge.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += badge.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += badge.ResistanceToSameFactionRate;

        IBadgesRepository badgesRepository = new BadgesRepository();
        BadgesService badgesService = new BadgesService(badgesRepository);
        // Percent
        badge =  await badgesService.SumPowerBadgesPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += badge.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += badge.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += badge.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += badge.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += badge.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += badge.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += badge.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += badge.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += badge.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += badge.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += badge.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetMechaBeastsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IMechaBeastsGalleryRepository mechaBeastsGalleryRepository = new MechaBeastsGalleryRepository();
        MechaBeastsGalleryService mechaBeastsGalleryService = new MechaBeastsGalleryService(mechaBeastsGalleryRepository);
        // Gallery
        MechaBeasts mechaBeast =  await mechaBeastsGalleryService.SumPowerMechaBeastsGalleryAsync();
        powerManager.Power += mechaBeast.Power;
        powerManager.Health += mechaBeast.Health;
        powerManager.PhysicalAttack += mechaBeast.PhysicalAttack;
        powerManager.PhysicalDefense += mechaBeast.PhysicalDefense;
        powerManager.MagicalAttack += mechaBeast.MagicalAttack;
        powerManager.MagicalDefense += mechaBeast.MagicalDefense;
        powerManager.ChemicalAttack += mechaBeast.ChemicalAttack;
        powerManager.ChemicalDefense += mechaBeast.ChemicalDefense;
        powerManager.AtomicAttack += mechaBeast.AtomicAttack;
        powerManager.AtomicDefense += mechaBeast.AtomicDefense;
        powerManager.MentalAttack += mechaBeast.MentalAttack;
        powerManager.MentalDefense += mechaBeast.MentalDefense;
        powerManager.Speed += mechaBeast.Speed;
        powerManager.CriticalDamageRate += mechaBeast.CriticalDamageRate;
        powerManager.CriticalRate += mechaBeast.CriticalRate;
        powerManager.PenetrationRate += mechaBeast.PenetrationRate;
        powerManager.EvasionRate += mechaBeast.EvasionRate;
        powerManager.DamageAbsorptionRate += mechaBeast.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += mechaBeast.VitalityRegenerationRate;
        powerManager.AccuracyRate += mechaBeast.AccuracyRate;
        powerManager.LifestealRate += mechaBeast.LifestealRate;
        powerManager.ShieldStrength += mechaBeast.ShieldStrength;
        powerManager.Tenacity += mechaBeast.Tenacity;
        powerManager.ResistanceRate += mechaBeast.ResistanceRate;
        powerManager.ComboRate += mechaBeast.ComboRate;
        powerManager.ReflectionRate += mechaBeast.ReflectionRate;
        powerManager.Mana += mechaBeast.Mana;
        powerManager.ManaRegenerationRate += mechaBeast.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += mechaBeast.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += mechaBeast.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += mechaBeast.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += mechaBeast.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += mechaBeast.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += mechaBeast.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += mechaBeast.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += mechaBeast.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += mechaBeast.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += mechaBeast.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += mechaBeast.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += mechaBeast.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += mechaBeast.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += mechaBeast.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += mechaBeast.PercentAllMentalDefense;

        IUserMechaBeastsRepository userMechaBeastsRepository = new UserMechaBeastsRepository();
        UserMechaBeastsService userMechaBeastsService = new UserMechaBeastsService(userMechaBeastsRepository);
        // User Robots (Gallery)
        mechaBeast = await userMechaBeastsService.SumPowerUserMechaBeastsAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += mechaBeast.Power;
        powerManager.Health += mechaBeast.Health;
        powerManager.PhysicalAttack += mechaBeast.PhysicalAttack;
        powerManager.PhysicalDefense += mechaBeast.PhysicalDefense;
        powerManager.MagicalAttack += mechaBeast.MagicalAttack;
        powerManager.MagicalDefense += mechaBeast.MagicalDefense;
        powerManager.ChemicalAttack += mechaBeast.ChemicalAttack;
        powerManager.ChemicalDefense += mechaBeast.ChemicalDefense;
        powerManager.AtomicAttack += mechaBeast.AtomicAttack;
        powerManager.AtomicDefense += mechaBeast.AtomicDefense;
        powerManager.MentalAttack += mechaBeast.MentalAttack;
        powerManager.MentalDefense += mechaBeast.MentalDefense;
        powerManager.Speed += mechaBeast.Speed;
        powerManager.CriticalDamageRate += mechaBeast.CriticalDamageRate;
        powerManager.CriticalRate += mechaBeast.CriticalRate;
        powerManager.PenetrationRate += mechaBeast.PenetrationRate;
        powerManager.EvasionRate += mechaBeast.EvasionRate;
        powerManager.DamageAbsorptionRate += mechaBeast.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += mechaBeast.VitalityRegenerationRate;
        powerManager.AccuracyRate += mechaBeast.AccuracyRate;
        powerManager.LifestealRate += mechaBeast.LifestealRate;
        powerManager.ShieldStrength += mechaBeast.ShieldStrength;
        powerManager.Tenacity += mechaBeast.Tenacity;
        powerManager.ResistanceRate += mechaBeast.ResistanceRate;
        powerManager.ComboRate += mechaBeast.ComboRate;
        powerManager.ReflectionRate += mechaBeast.ReflectionRate;
        powerManager.Mana += mechaBeast.Mana;
        powerManager.ManaRegenerationRate += mechaBeast.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += mechaBeast.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += mechaBeast.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += mechaBeast.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += mechaBeast.ResistanceToSameFactionRate;

        IMechaBeastsRepository mechaBeastsRepository = new MechaBeastsRepository();
        MechaBeastsService mechaBeastsService = new MechaBeastsService(mechaBeastsRepository);
        // Percent
        mechaBeast =  await mechaBeastsService.SumPowerMechaBeastsPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += mechaBeast.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += mechaBeast.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += mechaBeast.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += mechaBeast.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += mechaBeast.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += mechaBeast.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += mechaBeast.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += mechaBeast.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += mechaBeast.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += mechaBeast.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += mechaBeast.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetRunesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IRunesGalleryRepository runesGalleryRepository = new RunesGalleryRepository();
        RunesGalleryService runesGalleryService = new RunesGalleryService(runesGalleryRepository);
        // Gallery
        Runes rune =  await runesGalleryService.SumPowerRunesGalleryAsync();
        powerManager.Power += rune.Power;
        powerManager.Health += rune.Health;
        powerManager.PhysicalAttack += rune.PhysicalAttack;
        powerManager.PhysicalDefense += rune.PhysicalDefense;
        powerManager.MagicalAttack += rune.MagicalAttack;
        powerManager.MagicalDefense += rune.MagicalDefense;
        powerManager.ChemicalAttack += rune.ChemicalAttack;
        powerManager.ChemicalDefense += rune.ChemicalDefense;
        powerManager.AtomicAttack += rune.AtomicAttack;
        powerManager.AtomicDefense += rune.AtomicDefense;
        powerManager.MentalAttack += rune.MentalAttack;
        powerManager.MentalDefense += rune.MentalDefense;
        powerManager.Speed += rune.Speed;
        powerManager.CriticalDamageRate += rune.CriticalDamageRate;
        powerManager.CriticalRate += rune.CriticalRate;
        powerManager.PenetrationRate += rune.PenetrationRate;
        powerManager.EvasionRate += rune.EvasionRate;
        powerManager.DamageAbsorptionRate += rune.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += rune.VitalityRegenerationRate;
        powerManager.AccuracyRate += rune.AccuracyRate;
        powerManager.LifestealRate += rune.LifestealRate;
        powerManager.ShieldStrength += rune.ShieldStrength;
        powerManager.Tenacity += rune.Tenacity;
        powerManager.ResistanceRate += rune.ResistanceRate;
        powerManager.ComboRate += rune.ComboRate;
        powerManager.ReflectionRate += rune.ReflectionRate;
        powerManager.Mana += rune.Mana;
        powerManager.ManaRegenerationRate += rune.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += rune.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += rune.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += rune.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += rune.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += rune.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += rune.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += rune.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += rune.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += rune.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += rune.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += rune.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += rune.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += rune.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += rune.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += rune.PercentAllMentalDefense;

        IUserRunesRepository userRunesRepository = new UserRunesRepository();
        UserRunesService userRunesService = new UserRunesService(userRunesRepository);
        // User Robots (Gallery)
        rune = await userRunesService.SumPowerUserRunesAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += rune.Power;
        powerManager.Health += rune.Health;
        powerManager.PhysicalAttack += rune.PhysicalAttack;
        powerManager.PhysicalDefense += rune.PhysicalDefense;
        powerManager.MagicalAttack += rune.MagicalAttack;
        powerManager.MagicalDefense += rune.MagicalDefense;
        powerManager.ChemicalAttack += rune.ChemicalAttack;
        powerManager.ChemicalDefense += rune.ChemicalDefense;
        powerManager.AtomicAttack += rune.AtomicAttack;
        powerManager.AtomicDefense += rune.AtomicDefense;
        powerManager.MentalAttack += rune.MentalAttack;
        powerManager.MentalDefense += rune.MentalDefense;
        powerManager.Speed += rune.Speed;
        powerManager.CriticalDamageRate += rune.CriticalDamageRate;
        powerManager.CriticalRate += rune.CriticalRate;
        powerManager.PenetrationRate += rune.PenetrationRate;
        powerManager.EvasionRate += rune.EvasionRate;
        powerManager.DamageAbsorptionRate += rune.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += rune.VitalityRegenerationRate;
        powerManager.AccuracyRate += rune.AccuracyRate;
        powerManager.LifestealRate += rune.LifestealRate;
        powerManager.ShieldStrength += rune.ShieldStrength;
        powerManager.Tenacity += rune.Tenacity;
        powerManager.ResistanceRate += rune.ResistanceRate;
        powerManager.ComboRate += rune.ComboRate;
        powerManager.ReflectionRate += rune.ReflectionRate;
        powerManager.Mana += rune.Mana;
        powerManager.ManaRegenerationRate += rune.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += rune.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += rune.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += rune.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += rune.ResistanceToSameFactionRate;

        IRunesRepository runesRepository = new RunesRepository();
        RunesService runesService = new RunesService(runesRepository);
        // Percent
        rune =  await runesService.SumPowerRunesPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += rune.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += rune.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += rune.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += rune.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += rune.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += rune.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += rune.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += rune.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += rune.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += rune.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += rune.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetFurnituresPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        IFurnituresGalleryRepository furnitureGalleryRepository = new FurnituresGalleryRepository();
        FurnituresGalleryService furnitureGalleryService = new FurnituresGalleryService(furnitureGalleryRepository);
        // Gallery
        Furnitures furniture = await furnitureGalleryService.SumPowerFurnituresGalleryAsync();
        powerManager.Power += furniture.Power;
        powerManager.Health += furniture.Health;
        powerManager.PhysicalAttack += furniture.PhysicalAttack;
        powerManager.PhysicalDefense += furniture.PhysicalDefense;
        powerManager.MagicalAttack += furniture.MagicalAttack;
        powerManager.MagicalDefense += furniture.MagicalDefense;
        powerManager.ChemicalAttack += furniture.ChemicalAttack;
        powerManager.ChemicalDefense += furniture.ChemicalDefense;
        powerManager.AtomicAttack += furniture.AtomicAttack;
        powerManager.AtomicDefense += furniture.AtomicDefense;
        powerManager.MentalAttack += furniture.MentalAttack;
        powerManager.MentalDefense += furniture.MentalDefense;
        powerManager.Speed += furniture.Speed;
        powerManager.CriticalDamageRate += furniture.CriticalDamageRate;
        powerManager.CriticalRate += furniture.CriticalRate;
        powerManager.PenetrationRate += furniture.PenetrationRate;
        powerManager.EvasionRate += furniture.EvasionRate;
        powerManager.DamageAbsorptionRate += furniture.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += furniture.VitalityRegenerationRate;
        powerManager.AccuracyRate += furniture.AccuracyRate;
        powerManager.LifestealRate += furniture.LifestealRate;
        powerManager.ShieldStrength += furniture.ShieldStrength;
        powerManager.Tenacity += furniture.Tenacity;
        powerManager.ResistanceRate += furniture.ResistanceRate;
        powerManager.ComboRate += furniture.ComboRate;
        powerManager.ReflectionRate += furniture.ReflectionRate;
        powerManager.Mana += furniture.Mana;
        powerManager.ManaRegenerationRate += furniture.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += furniture.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += furniture.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += furniture.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += furniture.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += furniture.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += furniture.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += furniture.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += furniture.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += furniture.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += furniture.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += furniture.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += furniture.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += furniture.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += furniture.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += furniture.PercentAllMentalDefense;

        IUserFurnituresRepository userFurnitureRepository = new UserFurnituresRepository();
        UserFurnituresService userFurnitureService = new UserFurnituresService(userFurnitureRepository);
        // User SpiritBeast (Gallery)
        furniture = await userFurnitureService.SumPowerUserFurnituresAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.Power += furniture.Power;
        powerManager.Health += furniture.Health;
        powerManager.PhysicalAttack += furniture.PhysicalAttack;
        powerManager.PhysicalDefense += furniture.PhysicalDefense;
        powerManager.MagicalAttack += furniture.MagicalAttack;
        powerManager.MagicalDefense += furniture.MagicalDefense;
        powerManager.ChemicalAttack += furniture.ChemicalAttack;
        powerManager.ChemicalDefense += furniture.ChemicalDefense;
        powerManager.AtomicAttack += furniture.AtomicAttack;
        powerManager.AtomicDefense += furniture.AtomicDefense;
        powerManager.MentalAttack += furniture.MentalAttack;
        powerManager.MentalDefense += furniture.MentalDefense;
        powerManager.Speed += furniture.Speed;
        powerManager.CriticalDamageRate += furniture.CriticalDamageRate;
        powerManager.CriticalRate += furniture.CriticalRate;
        powerManager.PenetrationRate += furniture.PenetrationRate;
        powerManager.EvasionRate += furniture.EvasionRate;
        powerManager.DamageAbsorptionRate += furniture.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += furniture.VitalityRegenerationRate;
        powerManager.AccuracyRate += furniture.AccuracyRate;
        powerManager.LifestealRate += furniture.LifestealRate;
        powerManager.ShieldStrength += furniture.ShieldStrength;
        powerManager.Tenacity += furniture.Tenacity;
        powerManager.ResistanceRate += furniture.ResistanceRate;
        powerManager.ComboRate += furniture.ComboRate;
        powerManager.ReflectionRate += furniture.ReflectionRate;
        powerManager.Mana += furniture.Mana;
        powerManager.ManaRegenerationRate += furniture.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += furniture.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += furniture.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += furniture.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += furniture.ResistanceToSameFactionRate;

        IFurnituresRepository furnitureRepository = new FurnituresRepository();
        FurnituresService furnituresService = new FurnituresService(furnitureRepository);
        // Percent
        furniture = await furnituresService.SumPowerFurnituresPercentAsync(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
        powerManager.PercentAllHealth += furniture.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += furniture.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += furniture.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += furniture.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += furniture.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += furniture.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += furniture.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += furniture.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += furniture.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += furniture.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += furniture.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetFoodsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IFoodsGalleryRepository foodsGalleryRepository = new FoodsGalleryRepository();
        FoodsGalleryService foodsGalleryService = new FoodsGalleryService(foodsGalleryRepository);
        // Gallery
        Foods food =  await foodsGalleryService.SumPowerFoodsGalleryAsync();
        powerManager.Power += food.Power;
        powerManager.Health += food.Health;
        powerManager.PhysicalAttack += food.PhysicalAttack;
        powerManager.PhysicalDefense += food.PhysicalDefense;
        powerManager.MagicalAttack += food.MagicalAttack;
        powerManager.MagicalDefense += food.MagicalDefense;
        powerManager.ChemicalAttack += food.ChemicalAttack;
        powerManager.ChemicalDefense += food.ChemicalDefense;
        powerManager.AtomicAttack += food.AtomicAttack;
        powerManager.AtomicDefense += food.AtomicDefense;
        powerManager.MentalAttack += food.MentalAttack;
        powerManager.MentalDefense += food.MentalDefense;
        powerManager.Speed += food.Speed;
        powerManager.CriticalDamageRate += food.CriticalDamageRate;
        powerManager.CriticalRate += food.CriticalRate;
        powerManager.PenetrationRate += food.PenetrationRate;
        powerManager.EvasionRate += food.EvasionRate;
        powerManager.DamageAbsorptionRate += food.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += food.VitalityRegenerationRate;
        powerManager.AccuracyRate += food.AccuracyRate;
        powerManager.LifestealRate += food.LifestealRate;
        powerManager.ShieldStrength += food.ShieldStrength;
        powerManager.Tenacity += food.Tenacity;
        powerManager.ResistanceRate += food.ResistanceRate;
        powerManager.ComboRate += food.ComboRate;
        powerManager.ReflectionRate += food.ReflectionRate;
        powerManager.Mana += food.Mana;
        powerManager.ManaRegenerationRate += food.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += food.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += food.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += food.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += food.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += food.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += food.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += food.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += food.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += food.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += food.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += food.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += food.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += food.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += food.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += food.PercentAllMentalDefense;

        IUserFoodsRepository userFoodsRepository = new UserFoodsRepository();
        UserFoodsService userFoodsService = new UserFoodsService(userFoodsRepository);
        // User Robots (Gallery)
        food = await userFoodsService.SumPowerUserFoodsAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += food.Power;
        powerManager.Health += food.Health;
        powerManager.PhysicalAttack += food.PhysicalAttack;
        powerManager.PhysicalDefense += food.PhysicalDefense;
        powerManager.MagicalAttack += food.MagicalAttack;
        powerManager.MagicalDefense += food.MagicalDefense;
        powerManager.ChemicalAttack += food.ChemicalAttack;
        powerManager.ChemicalDefense += food.ChemicalDefense;
        powerManager.AtomicAttack += food.AtomicAttack;
        powerManager.AtomicDefense += food.AtomicDefense;
        powerManager.MentalAttack += food.MentalAttack;
        powerManager.MentalDefense += food.MentalDefense;
        powerManager.Speed += food.Speed;
        powerManager.CriticalDamageRate += food.CriticalDamageRate;
        powerManager.CriticalRate += food.CriticalRate;
        powerManager.PenetrationRate += food.PenetrationRate;
        powerManager.EvasionRate += food.EvasionRate;
        powerManager.DamageAbsorptionRate += food.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += food.VitalityRegenerationRate;
        powerManager.AccuracyRate += food.AccuracyRate;
        powerManager.LifestealRate += food.LifestealRate;
        powerManager.ShieldStrength += food.ShieldStrength;
        powerManager.Tenacity += food.Tenacity;
        powerManager.ResistanceRate += food.ResistanceRate;
        powerManager.ComboRate += food.ComboRate;
        powerManager.ReflectionRate += food.ReflectionRate;
        powerManager.Mana += food.Mana;
        powerManager.ManaRegenerationRate += food.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += food.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += food.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += food.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += food.ResistanceToSameFactionRate;

        IFoodsRepository foodsRepository = new FoodsRepository();
        FoodsService foodsService = new FoodsService(foodsRepository);
        // Percent
        food =  await foodsService.SumPowerFoodsPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += food.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += food.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += food.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += food.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += food.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += food.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += food.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += food.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += food.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += food.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += food.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetBeveragesPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IBeveragesGalleryRepository beveragesGalleryRepository = new BeveragesGalleryRepository();
        BeveragesGalleryService beveragesGalleryService = new BeveragesGalleryService(beveragesGalleryRepository);
        // Gallery
        Beverages beverage =  await beveragesGalleryService.SumPowerBeveragesGalleryAsync();
        powerManager.Power += beverage.Power;
        powerManager.Health += beverage.Health;
        powerManager.PhysicalAttack += beverage.PhysicalAttack;
        powerManager.PhysicalDefense += beverage.PhysicalDefense;
        powerManager.MagicalAttack += beverage.MagicalAttack;
        powerManager.MagicalDefense += beverage.MagicalDefense;
        powerManager.ChemicalAttack += beverage.ChemicalAttack;
        powerManager.ChemicalDefense += beverage.ChemicalDefense;
        powerManager.AtomicAttack += beverage.AtomicAttack;
        powerManager.AtomicDefense += beverage.AtomicDefense;
        powerManager.MentalAttack += beverage.MentalAttack;
        powerManager.MentalDefense += beverage.MentalDefense;
        powerManager.Speed += beverage.Speed;
        powerManager.CriticalDamageRate += beverage.CriticalDamageRate;
        powerManager.CriticalRate += beverage.CriticalRate;
        powerManager.PenetrationRate += beverage.PenetrationRate;
        powerManager.EvasionRate += beverage.EvasionRate;
        powerManager.DamageAbsorptionRate += beverage.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += beverage.VitalityRegenerationRate;
        powerManager.AccuracyRate += beverage.AccuracyRate;
        powerManager.LifestealRate += beverage.LifestealRate;
        powerManager.ShieldStrength += beverage.ShieldStrength;
        powerManager.Tenacity += beverage.Tenacity;
        powerManager.ResistanceRate += beverage.ResistanceRate;
        powerManager.ComboRate += beverage.ComboRate;
        powerManager.ReflectionRate += beverage.ReflectionRate;
        powerManager.Mana += beverage.Mana;
        powerManager.ManaRegenerationRate += beverage.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += beverage.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += beverage.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += beverage.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += beverage.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += beverage.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += beverage.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += beverage.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += beverage.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += beverage.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += beverage.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += beverage.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += beverage.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += beverage.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += beverage.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += beverage.PercentAllMentalDefense;

        IUserBeveragesRepository userBeveragesRepository = new UserBeveragesRepository();
        UserBeveragesService userBeveragesService = new UserBeveragesService(userBeveragesRepository);
        // User Robots (Gallery)
        beverage = await userBeveragesService.SumPowerUserBeveragesAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += beverage.Power;
        powerManager.Health += beverage.Health;
        powerManager.PhysicalAttack += beverage.PhysicalAttack;
        powerManager.PhysicalDefense += beverage.PhysicalDefense;
        powerManager.MagicalAttack += beverage.MagicalAttack;
        powerManager.MagicalDefense += beverage.MagicalDefense;
        powerManager.ChemicalAttack += beverage.ChemicalAttack;
        powerManager.ChemicalDefense += beverage.ChemicalDefense;
        powerManager.AtomicAttack += beverage.AtomicAttack;
        powerManager.AtomicDefense += beverage.AtomicDefense;
        powerManager.MentalAttack += beverage.MentalAttack;
        powerManager.MentalDefense += beverage.MentalDefense;
        powerManager.Speed += beverage.Speed;
        powerManager.CriticalDamageRate += beverage.CriticalDamageRate;
        powerManager.CriticalRate += beverage.CriticalRate;
        powerManager.PenetrationRate += beverage.PenetrationRate;
        powerManager.EvasionRate += beverage.EvasionRate;
        powerManager.DamageAbsorptionRate += beverage.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += beverage.VitalityRegenerationRate;
        powerManager.AccuracyRate += beverage.AccuracyRate;
        powerManager.LifestealRate += beverage.LifestealRate;
        powerManager.ShieldStrength += beverage.ShieldStrength;
        powerManager.Tenacity += beverage.Tenacity;
        powerManager.ResistanceRate += beverage.ResistanceRate;
        powerManager.ComboRate += beverage.ComboRate;
        powerManager.ReflectionRate += beverage.ReflectionRate;
        powerManager.Mana += beverage.Mana;
        powerManager.ManaRegenerationRate += beverage.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += beverage.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += beverage.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += beverage.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += beverage.ResistanceToSameFactionRate;

        IBeveragesRepository beveragesRepository = new BeveragesRepository();
        BeveragesService beveragesService = new BeveragesService(beveragesRepository);
        // Percent
        beverage =  await beveragesService.SumPowerBeveragesPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += beverage.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += beverage.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += beverage.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += beverage.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += beverage.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += beverage.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += beverage.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += beverage.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += beverage.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += beverage.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += beverage.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetBuildingsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        IBuildingsGalleryRepository buildingGalleryRepository = new BuildingsGalleryRepository();
        BuildingsGalleryService buildingGalleryService = new BuildingsGalleryService(buildingGalleryRepository);
        // Gallery
        Buildings building = await buildingGalleryService.SumPowerBuildingsGalleryAsync();
        powerManager.Power += building.Power;
        powerManager.Health += building.Health;
        powerManager.PhysicalAttack += building.PhysicalAttack;
        powerManager.PhysicalDefense += building.PhysicalDefense;
        powerManager.MagicalAttack += building.MagicalAttack;
        powerManager.MagicalDefense += building.MagicalDefense;
        powerManager.ChemicalAttack += building.ChemicalAttack;
        powerManager.ChemicalDefense += building.ChemicalDefense;
        powerManager.AtomicAttack += building.AtomicAttack;
        powerManager.AtomicDefense += building.AtomicDefense;
        powerManager.MentalAttack += building.MentalAttack;
        powerManager.MentalDefense += building.MentalDefense;
        powerManager.Speed += building.Speed;
        powerManager.CriticalDamageRate += building.CriticalDamageRate;
        powerManager.CriticalRate += building.CriticalRate;
        powerManager.PenetrationRate += building.PenetrationRate;
        powerManager.EvasionRate += building.EvasionRate;
        powerManager.DamageAbsorptionRate += building.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += building.VitalityRegenerationRate;
        powerManager.AccuracyRate += building.AccuracyRate;
        powerManager.LifestealRate += building.LifestealRate;
        powerManager.ShieldStrength += building.ShieldStrength;
        powerManager.Tenacity += building.Tenacity;
        powerManager.ResistanceRate += building.ResistanceRate;
        powerManager.ComboRate += building.ComboRate;
        powerManager.ReflectionRate += building.ReflectionRate;
        powerManager.Mana += building.Mana;
        powerManager.ManaRegenerationRate += building.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += building.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += building.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += building.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += building.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += building.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += building.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += building.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += building.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += building.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += building.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += building.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += building.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += building.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += building.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += building.PercentAllMentalDefense;

        IUserBuildingsRepository userBuildingRepository = new UserBuildingsRepository();
        UserBuildingsService userBuildingService = new UserBuildingsService(userBuildingRepository);
        // User SpiritBeast (Gallery)
        building = await userBuildingService.SumPowerUserBuildingsAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.Power += building.Power;
        powerManager.Health += building.Health;
        powerManager.PhysicalAttack += building.PhysicalAttack;
        powerManager.PhysicalDefense += building.PhysicalDefense;
        powerManager.MagicalAttack += building.MagicalAttack;
        powerManager.MagicalDefense += building.MagicalDefense;
        powerManager.ChemicalAttack += building.ChemicalAttack;
        powerManager.ChemicalDefense += building.ChemicalDefense;
        powerManager.AtomicAttack += building.AtomicAttack;
        powerManager.AtomicDefense += building.AtomicDefense;
        powerManager.MentalAttack += building.MentalAttack;
        powerManager.MentalDefense += building.MentalDefense;
        powerManager.Speed += building.Speed;
        powerManager.CriticalDamageRate += building.CriticalDamageRate;
        powerManager.CriticalRate += building.CriticalRate;
        powerManager.PenetrationRate += building.PenetrationRate;
        powerManager.EvasionRate += building.EvasionRate;
        powerManager.DamageAbsorptionRate += building.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += building.VitalityRegenerationRate;
        powerManager.AccuracyRate += building.AccuracyRate;
        powerManager.LifestealRate += building.LifestealRate;
        powerManager.ShieldStrength += building.ShieldStrength;
        powerManager.Tenacity += building.Tenacity;
        powerManager.ResistanceRate += building.ResistanceRate;
        powerManager.ComboRate += building.ComboRate;
        powerManager.ReflectionRate += building.ReflectionRate;
        powerManager.Mana += building.Mana;
        powerManager.ManaRegenerationRate += building.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += building.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += building.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += building.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += building.ResistanceToSameFactionRate;

        IBuildingsRepository BuildingRepository = new BuildingsRepository();
        BuildingsService BuildingsService = new BuildingsService(BuildingRepository);
        // Percent
        building = await BuildingsService.SumPowerBuildingsPercentAsync(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
        powerManager.PercentAllHealth += building.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += building.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += building.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += building.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += building.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += building.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += building.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += building.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += building.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += building.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += building.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetPlantsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        // Titles titles = new Titles();
        IPlantsGalleryRepository plantsGalleryRepository = new PlantsGalleryRepository();
        PlantsGalleryService plantsGalleryService = new PlantsGalleryService(plantsGalleryRepository);
        // Gallery
        Plants plant =  await plantsGalleryService.SumPowerPlantsGalleryAsync();
        powerManager.Power += plant.Power;
        powerManager.Health += plant.Health;
        powerManager.PhysicalAttack += plant.PhysicalAttack;
        powerManager.PhysicalDefense += plant.PhysicalDefense;
        powerManager.MagicalAttack += plant.MagicalAttack;
        powerManager.MagicalDefense += plant.MagicalDefense;
        powerManager.ChemicalAttack += plant.ChemicalAttack;
        powerManager.ChemicalDefense += plant.ChemicalDefense;
        powerManager.AtomicAttack += plant.AtomicAttack;
        powerManager.AtomicDefense += plant.AtomicDefense;
        powerManager.MentalAttack += plant.MentalAttack;
        powerManager.MentalDefense += plant.MentalDefense;
        powerManager.Speed += plant.Speed;
        powerManager.CriticalDamageRate += plant.CriticalDamageRate;
        powerManager.CriticalRate += plant.CriticalRate;
        powerManager.PenetrationRate += plant.PenetrationRate;
        powerManager.EvasionRate += plant.EvasionRate;
        powerManager.DamageAbsorptionRate += plant.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += plant.VitalityRegenerationRate;
        powerManager.AccuracyRate += plant.AccuracyRate;
        powerManager.LifestealRate += plant.LifestealRate;
        powerManager.ShieldStrength += plant.ShieldStrength;
        powerManager.Tenacity += plant.Tenacity;
        powerManager.ResistanceRate += plant.ResistanceRate;
        powerManager.ComboRate += plant.ComboRate;
        powerManager.ReflectionRate += plant.ReflectionRate;
        powerManager.Mana += plant.Mana;
        powerManager.ManaRegenerationRate += plant.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += plant.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += plant.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += plant.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += plant.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += plant.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += plant.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += plant.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += plant.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += plant.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += plant.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += plant.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += plant.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += plant.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += plant.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += plant.PercentAllMentalDefense;

        IUserPlantsRepository userPlantsRepository = new UserPlantsRepository();
        UserPlantsService userPlantsService = new UserPlantsService(userPlantsRepository);
        // User Robots (Gallery)
        plant = await userPlantsService.SumPowerUserPlantsAsync(); // Giả định SumPowerUserRobots cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.Power += plant.Power;
        powerManager.Health += plant.Health;
        powerManager.PhysicalAttack += plant.PhysicalAttack;
        powerManager.PhysicalDefense += plant.PhysicalDefense;
        powerManager.MagicalAttack += plant.MagicalAttack;
        powerManager.MagicalDefense += plant.MagicalDefense;
        powerManager.ChemicalAttack += plant.ChemicalAttack;
        powerManager.ChemicalDefense += plant.ChemicalDefense;
        powerManager.AtomicAttack += plant.AtomicAttack;
        powerManager.AtomicDefense += plant.AtomicDefense;
        powerManager.MentalAttack += plant.MentalAttack;
        powerManager.MentalDefense += plant.MentalDefense;
        powerManager.Speed += plant.Speed;
        powerManager.CriticalDamageRate += plant.CriticalDamageRate;
        powerManager.CriticalRate += plant.CriticalRate;
        powerManager.PenetrationRate += plant.PenetrationRate;
        powerManager.EvasionRate += plant.EvasionRate;
        powerManager.DamageAbsorptionRate += plant.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += plant.VitalityRegenerationRate;
        powerManager.AccuracyRate += plant.AccuracyRate;
        powerManager.LifestealRate += plant.LifestealRate;
        powerManager.ShieldStrength += plant.ShieldStrength;
        powerManager.Tenacity += plant.Tenacity;
        powerManager.ResistanceRate += plant.ResistanceRate;
        powerManager.ComboRate += plant.ComboRate;
        powerManager.ReflectionRate += plant.ReflectionRate;
        powerManager.Mana += plant.Mana;
        powerManager.ManaRegenerationRate += plant.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += plant.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += plant.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += plant.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += plant.ResistanceToSameFactionRate;

        IPlantsRepository plantsRepository = new PlantsRepository();
        PlantsService plantsService = new PlantsService(plantsRepository);
        // Percent
        plant =  await plantsService.SumPowerPlantsPercentAsync(); // Giả định SumPowerRobotsPercent cũng trả về một đối tượng Robots mới hoặc đã được reset
        powerManager.PercentAllHealth += plant.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += plant.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += plant.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += plant.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += plant.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += plant.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += plant.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += plant.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += plant.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += plant.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += plant.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
    public async Task<PowerManager> GetFashionsPowerAsync()
    {
        PowerManager powerManager = new PowerManager(); // Khởi tạo PowerManager mới

        IFashionsGalleryRepository fashionsGalleryRepository = new FashionsGalleryRepository();
        FashionsGalleryService fashionsGalleryService = new FashionsGalleryService(fashionsGalleryRepository);
        // Gallery
        Fashions fashion = await fashionsGalleryService.SumPowerFashionsGalleryAsync();
        powerManager.Power += fashion.Power;
        powerManager.Health += fashion.Health;
        powerManager.PhysicalAttack += fashion.PhysicalAttack;
        powerManager.PhysicalDefense += fashion.PhysicalDefense;
        powerManager.MagicalAttack += fashion.MagicalAttack;
        powerManager.MagicalDefense += fashion.MagicalDefense;
        powerManager.ChemicalAttack += fashion.ChemicalAttack;
        powerManager.ChemicalDefense += fashion.ChemicalDefense;
        powerManager.AtomicAttack += fashion.AtomicAttack;
        powerManager.AtomicDefense += fashion.AtomicDefense;
        powerManager.MentalAttack += fashion.MentalAttack;
        powerManager.MentalDefense += fashion.MentalDefense;
        powerManager.Speed += fashion.Speed;
        powerManager.CriticalDamageRate += fashion.CriticalDamageRate;
        powerManager.CriticalRate += fashion.CriticalRate;
        powerManager.PenetrationRate += fashion.PenetrationRate;
        powerManager.EvasionRate += fashion.EvasionRate;
        powerManager.DamageAbsorptionRate += fashion.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += fashion.VitalityRegenerationRate;
        powerManager.AccuracyRate += fashion.AccuracyRate;
        powerManager.LifestealRate += fashion.LifestealRate;
        powerManager.ShieldStrength += fashion.ShieldStrength;
        powerManager.Tenacity += fashion.Tenacity;
        powerManager.ResistanceRate += fashion.ResistanceRate;
        powerManager.ComboRate += fashion.ComboRate;
        powerManager.ReflectionRate += fashion.ReflectionRate;
        powerManager.Mana += fashion.Mana;
        powerManager.ManaRegenerationRate += fashion.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += fashion.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += fashion.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += fashion.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += fashion.ResistanceToSameFactionRate;

        powerManager.PercentAllHealth += fashion.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += fashion.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += fashion.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += fashion.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += fashion.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += fashion.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += fashion.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += fashion.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += fashion.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += fashion.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += fashion.PercentAllMentalDefense;

        IUserFashionsRepository userFashionsRepository = new UserFashionsRepository();
        UserFashionsService userFashionsService = new UserFashionsService(userFashionsRepository);
        // User SpiritBeast (Gallery)
        fashion = await userFashionsService.SumPowerUserFashionsAsync(); // Giả định SumPowerUserTitles cũng trả về một đối tượng Titles mới hoặc đã được reset
        powerManager.Power += fashion.Power;
        powerManager.Health += fashion.Health;
        powerManager.PhysicalAttack += fashion.PhysicalAttack;
        powerManager.PhysicalDefense += fashion.PhysicalDefense;
        powerManager.MagicalAttack += fashion.MagicalAttack;
        powerManager.MagicalDefense += fashion.MagicalDefense;
        powerManager.ChemicalAttack += fashion.ChemicalAttack;
        powerManager.ChemicalDefense += fashion.ChemicalDefense;
        powerManager.AtomicAttack += fashion.AtomicAttack;
        powerManager.AtomicDefense += fashion.AtomicDefense;
        powerManager.MentalAttack += fashion.MentalAttack;
        powerManager.MentalDefense += fashion.MentalDefense;
        powerManager.Speed += fashion.Speed;
        powerManager.CriticalDamageRate += fashion.CriticalDamageRate;
        powerManager.CriticalRate += fashion.CriticalRate;
        powerManager.PenetrationRate += fashion.PenetrationRate;
        powerManager.EvasionRate += fashion.EvasionRate;
        powerManager.DamageAbsorptionRate += fashion.DamageAbsorptionRate;
        powerManager.VitalityRegenerationRate += fashion.VitalityRegenerationRate;
        powerManager.AccuracyRate += fashion.AccuracyRate;
        powerManager.LifestealRate += fashion.LifestealRate;
        powerManager.ShieldStrength += fashion.ShieldStrength;
        powerManager.Tenacity += fashion.Tenacity;
        powerManager.ResistanceRate += fashion.ResistanceRate;
        powerManager.ComboRate += fashion.ComboRate;
        powerManager.ReflectionRate += fashion.ReflectionRate;
        powerManager.Mana += fashion.Mana;
        powerManager.ManaRegenerationRate += fashion.ManaRegenerationRate;
        powerManager.DamageToDifferentFactionRate += fashion.DamageToDifferentFactionRate;
        powerManager.ResistanceToDifferentFactionRate += fashion.ResistanceToDifferentFactionRate;
        powerManager.DamageToSameFactionRate += fashion.DamageToSameFactionRate;
        powerManager.ResistanceToSameFactionRate += fashion.ResistanceToSameFactionRate;

        IFashionsRepository fashionsRepository = new FashionsRepository();
        FashionsService fashionsService = new FashionsService(fashionsRepository);
        // Percent
        fashion = await fashionsService.SumPowerFashionsPercentAsync(); // Giả định SumPowerspiritBeastPercent cũng trả về một đối tượng spiritBeast mới hoặc đã được reset
        powerManager.PercentAllHealth += fashion.PercentAllHealth;
        powerManager.PercentAllPhysicalAttack += fashion.PercentAllPhysicalAttack;
        powerManager.PercentAllPhysicalDefense += fashion.PercentAllPhysicalDefense;
        powerManager.PercentAllMagicalAttack += fashion.PercentAllMagicalAttack;
        powerManager.PercentAllMagicalDefense += fashion.PercentAllMagicalDefense;
        powerManager.PercentAllChemicalAttack += fashion.PercentAllChemicalAttack;
        powerManager.PercentAllChemicalDefense += fashion.PercentAllChemicalDefense;
        powerManager.PercentAllAtomicAttack += fashion.PercentAllAtomicAttack;
        powerManager.PercentAllAtomicDefense += fashion.PercentAllAtomicDefense;
        powerManager.PercentAllMentalAttack += fashion.PercentAllMentalAttack;
        powerManager.PercentAllMentalDefense += fashion.PercentAllMentalDefense;

        return powerManager; // Trả về đối tượng PowerManager chứa tổng các thuộc tính
    }
}