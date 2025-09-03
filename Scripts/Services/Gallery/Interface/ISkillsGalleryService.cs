using System.Collections.Generic;

public interface ISkillsGalleryService
{
    List<Skills> GetSkillsCollection(string type, int pageSize, int offset, string rare);
    int GetSkillsCount(string type, string rare);
    void InsertSkillsGallery(string Id);
    void UpdateStatusSkillsGallery(string Id);
    void UpdateStarSkillsGallery(string Id, double star);
    void UpdateSkillsGalleryPower(string Id);
    Skills SumPowerSkillsGallery();
}