using System.Collections.Generic;

public interface IAvatarsRepository
{
    List<string> GetUniqueAvatarsId();
    List<Avatars> GetAvatars(int pageSize, int offset, string rare);
    int GetAvatarsCount(string rare);
    List<Avatars> GetAvatarsWithPrice(int pageSize, int offset);
    int GetAvatarsWithPriceCount();
    Avatars GetAvatarsById(string Id);
    Avatars SumPowerAvatarsPercent();
}