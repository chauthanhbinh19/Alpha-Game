using System.Collections.Generic;

public interface IAvatarsGalleryService
{
    List<Avatars> GetAvatarsCollection(int pageSize, int offset, string rare);
    int GetAvatarsCount(string rare);
    void InsertAvatarsGallery(string Id);
    void UpdateStatusAvatarsGallery(string Id);
    Avatars SumPowerAvatarsGallery();
}