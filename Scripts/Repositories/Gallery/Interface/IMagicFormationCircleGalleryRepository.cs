using System.Collections.Generic;

public interface IMagicFormationCircleGalleryRepository
{
    List<MagicFormationCircle> GetMagicFormationCircleCollection(string type, int pageSize, int offset);
    int GetMagicFormationCircleCount(string type);
    void InsertMagicFormationCircleGallery(string Id, MagicFormationCircle magicFormationCircleFromDB);
    void UpdateStatusMagicFormationCircleGallery(string Id);
    MagicFormationCircle SumPowerMagicFormationCircleGallery();
}