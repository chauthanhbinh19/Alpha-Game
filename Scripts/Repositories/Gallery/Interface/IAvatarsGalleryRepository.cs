using System.Collections.Generic;

public interface IAvatarsGalleryRepository
{
    List<Avatars> GetAvatarsCollection(int pageSize, int offset, string rare);
    int GetAvatarsCount(string rare);
    void InsertAvatarsGallery(string Id, Avatars BorderFromDB);
    void UpdateStatusAvatarsGallery(string Id);
    Avatars SumPowerAvatarsGallery();
}