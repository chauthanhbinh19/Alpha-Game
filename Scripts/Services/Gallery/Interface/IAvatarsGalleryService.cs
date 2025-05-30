using System.Collections.Generic;

public interface IAvatarsGalleryService
{
    List<Avatars> GetAvatarsCollection(int pageSize, int offset);
    int GetAvatarsCount();
    void InsertAvatarsGallery(string Id);
    void UpdateStatusAvatarsGallery(string Id);
    Avatars SumPowerAvatarsGallery();
}