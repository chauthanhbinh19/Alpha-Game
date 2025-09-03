using System.Collections.Generic;

public interface ICardAdmiralsGalleryService
{
    List<CardAdmirals> GetCardAdmiralsCollection(string type, int pageSize, int offset, string rare);
    int GetCardAdmiralsCount(string type, string rare); 
    void InsertCardAdmiralsGallery(string Id);
    void UpdateStatusCardAdmiralsGallery(string Id);
    void UpdateStarCardAdmiralsGallery(string Id, double star);
    void UpdateCardAdmiralsGalleryPower(string Id);
    CardAdmirals SumPowerCardCaptainsGallery();
}