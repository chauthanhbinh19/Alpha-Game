using System.Collections.Generic;

public interface IAvatarsGalleryRepository
{
    List<Avatars> GetAvatarsCollection(int pageSize, int offset);
    int GetAvatarsCount();
    void InsertAvatarsGallery(string Id, Avatars BorderFromDB);
    void UpdateStatusAvatarsGallery(string Id);
    Avatars SumPowerAvatarsGallery();
}