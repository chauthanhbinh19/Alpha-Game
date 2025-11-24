using System.Collections.Generic;

public interface IPowerManagerService
{
    void InsertUserStats(string user_id);
    void UpdateUserStats(string user_id);
    PowerManager GetUserStats(string user_id);
    PowerManager GetAchievementsPower();
    PowerManager GetAvatarsPower();
    PowerManager GetBooksPower();
    PowerManager GetBordersPower();
    PowerManager GetCardHeroesPower();
    PowerManager GetCardCaptainsPower();
    PowerManager GetCardColonelsPower();
    PowerManager GetCardGeneralsPower();
    PowerManager GetCardAdmiralsPower();
    PowerManager GetCardMonstersPower();
    PowerManager GetCardMilitariesPower();
    PowerManager GetCardSpellsPower();
    PowerManager GetCollaborationsPower();
    PowerManager GetCollaborationEquipmentsPower();
    PowerManager GetEquipmentsPower();
    PowerManager GetMagicFormationCirclesPower();
    PowerManager GetRelicsPower();
    PowerManager GetMedalsPower();
    PowerManager GetPetsPower();
    PowerManager GetSymbolsPower();
    PowerManager GetSkillsPower();
    PowerManager GetTitlesPower();
    PowerManager GetTalismansPower();
    PowerManager GetPuppetsPower();
    PowerManager GetAlchemiesPower();
    PowerManager GetForgesPower();
    PowerManager GetCardLivesPower();
    PowerManager GetArtworksPower();
    PowerManager GetSpiritBeastsPower();
    PowerManager GetSpiritCardsPower();
    PowerManager GetVehiclesPower();
    PowerManager GetCardsPower();
    PowerManager GetTechnologiesPower();
    PowerManager GetArchitecturesPower();
    PowerManager GetCoresPower();
    PowerManager GetWeaponsPower();
    PowerManager GetRobotsPower();
}