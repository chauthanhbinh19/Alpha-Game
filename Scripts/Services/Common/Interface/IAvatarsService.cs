using System.Collections.Generic;

public interface IAvatarsService
{
    List<string> GetUniqueAvatarsId();
    List<Achievements> GetAvatars(int pageSize, int offset, string rare);
    int GetAvatarsCount(string rare);
    List<Achievements> GetAvatarsWithPrice(int pageSize, int offset);
    int GetAvatarsWithPriceCount();
    Achievements GetAvatarsById(string Id);
    Achievements SumPowerAvatarsPercent();
}