using System.Collections.Generic;

public interface IMagicFormationCircleGalleryService
{
    List<MagicFormationCircles> GetMagicFormationCircleCollection(string type, int pageSize, int offset, string rare);
    int GetMagicFormationCircleCount(string type, string rare);
    void InsertMagicFormationCircleGallery(string Id);
    void UpdateStatusMagicFormationCircleGallery(string Id);
    void UpdateStarMagicFormationCircleGallery(string Id, double star);
    void UpdateMagicFormationCircleGalleryPower(string Id);
    MagicFormationCircles SumPowerMagicFormationCircleGallery();
}