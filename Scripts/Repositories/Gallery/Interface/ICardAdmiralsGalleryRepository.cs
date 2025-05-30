using System.Collections.Generic;

public interface ICardAdmiralsGalleryRepository
{
    List<CardAdmirals> GetCardAdmiralsCollection(string type, int pageSize, int offset);
    int GetCardAdmiralsCount(string type); 
    void InsertCardAdmiralsGallery(string Id, CardAdmirals CaptainFromDB);
    void UpdateStatusCardAdmiralsGallery(string Id);
    CardAdmirals SumPowerCardCaptainsGallery();
}