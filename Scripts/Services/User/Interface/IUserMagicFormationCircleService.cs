using System.Collections.Generic;

public interface IUserMagicFormationCircleService
{
    MagicFormationCircle GetNewLevelPower(MagicFormationCircle c, double coefficient);
    MagicFormationCircle GetNewBreakthroughPower(MagicFormationCircle c, double coefficient);
    List<MagicFormationCircle> GetUserMagicFormationCircle(string user_id, string type, int pageSize, int offset);
    int GetUserMagicFormationCircleCount(string user_id, string type);
    bool InsertUserMagicFormationCircle(MagicFormationCircle magicFormationCircle); // Corrected typo
    bool UpdateMagicFormationCircleLevel(MagicFormationCircle magicFormationCircle, int cardLevel);
    bool UpdateMagicFormationCircleBreakthrough(MagicFormationCircle magicFormationCircle, int star, int quantity);
    MagicFormationCircle GetUserMagicFormationCircleById(string user_id, string Id);
    MagicFormationCircle SumPowerUserMagicFormationCircle();
}