using System.Collections.Generic;

public interface IAvatarsService
{
    List<Avatars> GetAvatars(int pageSize, int offset);
    int GetAvatarsCount();
    List<Avatars> GetAvatarsWithPrice(int pageSize, int offset);
    int GetAvatarsWithPriceCount();
    Avatars GetAvatarsById(string Id);
    Avatars SumPowerAvatarsPercent();
}