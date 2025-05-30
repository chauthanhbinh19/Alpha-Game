using System.Collections.Generic;

public interface IMagicFormationCircleGalleryService
{
    List<MagicFormationCircle> GetMagicFormationCircleCollection(string type, int pageSize, int offset);
    int GetMagicFormationCircleCount(string type);
    void InsertMagicFormationCircleGallery(string Id);
    void UpdateStatusMagicFormationCircleGallery(string Id);
    MagicFormationCircle SumPowerMagicFormationCircleGallery();
}