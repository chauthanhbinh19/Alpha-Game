using System.Collections.Generic;

public interface IMagicFormationCircleGalleryRepository
{
    List<MagicFormationCircle> GetMagicFormationCircleCollection(string type, int pageSize, int offset, string rare);
    int GetMagicFormationCircleCount(string type, string rare);
    void InsertMagicFormationCircleGallery(string Id, MagicFormationCircle magicFormationCircleFromDB);
    void UpdateStatusMagicFormationCircleGallery(string Id);
    void UpdateStarMagicFormationCircleGallery(string Id, double star);
    void UpdateMagicFormationCircleGalleryPower(string Id, MagicFormationCircle MagicFormationCircleFromDB);
    MagicFormationCircle SumPowerMagicFormationCircleGallery();
}