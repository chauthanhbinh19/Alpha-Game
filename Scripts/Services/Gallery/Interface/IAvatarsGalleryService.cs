using System.Collections.Generic;

public interface IAvatarsGalleryService
{
    List<Achievements> GetAvatarsCollection(int pageSize, int offset, string rare);
    int GetAvatarsCount(string rare);
    void InsertAvatarsGallery(string Id);
    void UpdateStatusAvatarsGallery(string Id);
    void UpdateStarAvatarsGallery(string Id, double star);
    void UpdateAvatarsGalleryPower(string Id);
    Achievements SumPowerAvatarsGallery();
}