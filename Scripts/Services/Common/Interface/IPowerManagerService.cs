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
    PowerManager GetCardMilitaryPower();
    PowerManager GetCardSpellPower();
    PowerManager GetCollaborationsPower();
    PowerManager GetCollaborationEquipmentsPower();
    PowerManager GetEquipmentsPower();
    PowerManager GetMagicFormationCirclePower();
    PowerManager GetRelicsPower();
    PowerManager GetMedalsPower();
    PowerManager GetPetsPower();
    PowerManager GetSymbolsPower();
    PowerManager GetSkillsPower();
    PowerManager GetTitlesPower();
    PowerManager GetTalismanPower();
    PowerManager GetPuppetPower();
    PowerManager GetAlchemyPower();
    PowerManager GetForgePower();
    PowerManager GetCardLifePower();
}