using System.Collections.Generic;

public interface IAvatarsRepository
{
    List<Avatars> GetAvatars(int pageSize, int offset);
    int GetAvatarsCount();
    List<Avatars> GetAvatarsWithPrice(int pageSize, int offset);
    int GetAvatarsWithPriceCount();
    Avatars GetAvatarsById(string Id);
    Avatars SumPowerAvatarsPercent();
}