using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPowerManagerService
{
    Task InsertUserStatsAsync(string user_id);
    Task UpdateUserStatsAsync(string user_id);
    Task<PowerManager> GetUserStatsAsync(string user_id);
    Task<PowerManager> GetAchievementsPowerAsync();
    Task<PowerManager> GetAvatarsPowerAsync();
    Task<PowerManager> GetBooksPowerAsync();
    Task<PowerManager> GetBordersPowerAsync();
    Task<PowerManager> GetCardHeroesPowerAsync();
    Task<PowerManager> GetCardCaptainsPowerAsync();
    Task<PowerManager> GetCardColonelsPowerAsync();
    Task<PowerManager> GetCardGeneralsPowerAsync();
    Task<PowerManager> GetCardAdmiralsPowerAsync();
    Task<PowerManager> GetCardMonstersPowerAsync();
    Task<PowerManager> GetCardMilitariesPowerAsync();
    Task<PowerManager> GetCardSpellsPowerAsync();
    Task<PowerManager> GetCollaborationsPowerAsync();
    Task<PowerManager> GetCollaborationEquipmentsPowerAsync();
    Task<PowerManager> GetEquipmentsPowerAsync();
    Task<PowerManager> GetMagicFormationCirclesPowerAsync();
    Task<PowerManager> GetRelicsPowerAsync();
    Task<PowerManager> GetMedalsPowerAsync();
    Task<PowerManager> GetPetsPowerAsync();
    Task<PowerManager> GetSymbolsPowerAsync();
    Task<PowerManager> GetSkillsPowerAsync();
    Task<PowerManager> GetTitlesPowerAsync();
    Task<PowerManager> GetTalismansPowerAsync();
    Task<PowerManager> GetPuppetsPowerAsync();
    Task<PowerManager> GetAlchemiesPowerAsync();
    Task<PowerManager> GetForgesPowerAsync();
    Task<PowerManager> GetCardLivesPowerAsync();
    Task<PowerManager> GetArtworksPowerAsync();
    Task<PowerManager> GetSpiritBeastsPowerAsync();
    Task<PowerManager> GetSpiritCardsPowerAsync();
    Task<PowerManager> GetVehiclesPowerAsync();
    Task<PowerManager> GetCardsPowerAsync();
    Task<PowerManager> GetTechnologiesPowerAsync();
    Task<PowerManager> GetArchitecturesPowerAsync();
    Task<PowerManager> GetCoresPowerAsync();
    Task<PowerManager> GetWeaponsPowerAsync();
    Task<PowerManager> GetRobotsPowerAsync();
    Task<PowerManager> GetBadgesPowerAsync();
    Task<PowerManager> GetMechaBeastsPowerAsync();
    Task<PowerManager> GetRunesPowerAsync();
}