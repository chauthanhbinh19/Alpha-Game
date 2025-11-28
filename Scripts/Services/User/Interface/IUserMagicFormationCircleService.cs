using System.Collections.Generic;

public interface IUserMagicFormationCircleService
{
    MagicFormationCircles GetNewLevelPower(MagicFormationCircles c, double coefficient);
    MagicFormationCircles GetNewBreakthroughPower(MagicFormationCircles c, double coefficient);
    List<MagicFormationCircles> GetUserMagicFormationCircle(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserMagicFormationCircleCount(string user_id, string type, string rare);
    bool InsertUserMagicFormationCircle(MagicFormationCircles magicFormationCircle, string userId); // Corrected typo
    bool UpdateMagicFormationCircleLevel(MagicFormationCircles magicFormationCircle, int cardLevel);
    bool UpdateMagicFormationCircleBreakthrough(MagicFormationCircles magicFormationCircle, int star, double quantity);
    MagicFormationCircles GetUserMagicFormationCircleById(string user_id, string Id);
    MagicFormationCircles SumPowerUserMagicFormationCircle();
}