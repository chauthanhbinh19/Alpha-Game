using System.Collections.Generic;

public interface ICardAdmiralsGalleryRepository
{
    List<CardAdmirals> GetCardAdmiralsCollection(string type, int pageSize, int offset, string rare);
    int GetCardAdmiralsCount(string type, string rare); 
    void InsertCardAdmiralsGallery(string Id, CardAdmirals CaptainFromDB);
    void UpdateStatusCardAdmiralsGallery(string Id);
    CardAdmirals SumPowerCardCaptainsGallery();
}