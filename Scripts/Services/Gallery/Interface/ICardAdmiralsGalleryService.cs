using System.Collections.Generic;

public interface ICardAdmiralsGalleryService
{
    List<CardAdmirals> GetCardAdmiralsCollection(string type, int pageSize, int offset);
    int GetCardAdmiralsCount(string type); 
    void InsertCardAdmiralsGallery(string Id);
    void UpdateStatusCardAdmiralsGallery(string Id);
    CardAdmirals SumPowerCardCaptainsGallery();
}