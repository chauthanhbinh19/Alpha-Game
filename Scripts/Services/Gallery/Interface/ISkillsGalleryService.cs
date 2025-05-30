using System.Collections.Generic;

public interface ISkillsGalleryService
{
    List<Skills> GetSkillsCollection(string type, int pageSize, int offset);
    int GetSkillsCount(string type);
    void InsertSkillsGallery(string Id);
    void UpdateStatusSkillsGallery(string Id);
    Skills SumPowerSkillsGallery();
}