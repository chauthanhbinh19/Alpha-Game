using System.Collections.Generic;

public interface ISkillsGalleryRepository
{
    List<Skills> GetSkillsCollection(string type, int pageSize, int offset);
    int GetSkillsCount(string type);
    void InsertSkillsGallery(string Id, Skills skillFromDB);
    void UpdateStatusSkillsGallery(string Id);
    Skills SumPowerSkillsGallery();
}