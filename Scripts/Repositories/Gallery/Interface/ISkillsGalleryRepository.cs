using System.Collections.Generic;

public interface ISkillsGalleryRepository
{
    List<Skills> GetSkillsCollection(string type, int pageSize, int offset, string rare);
    int GetSkillsCount(string type, string rare);
    void InsertSkillsGallery(string Id, Skills skillFromDB);
    void UpdateStatusSkillsGallery(string Id);
    Skills SumPowerSkillsGallery();
}