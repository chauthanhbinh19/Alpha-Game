using System.Threading.Tasks;

public interface IPathOfLordImprintsService
{
    Task<PathOfLordImprints> GetPathOfLordImprintByIdAsync(string id);
    Task<InsertOrUpdateResult<PathOfLordImprints>> InsertPathOfLordImprintAsync(PathOfLordImprints hisn);
    Task<InsertOrUpdateResult<PathOfLordImprints>> UpdatePathOfLordImprintAsync(PathOfLordImprints hisn);
}