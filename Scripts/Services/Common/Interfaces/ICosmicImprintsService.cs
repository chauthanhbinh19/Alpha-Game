using System.Threading.Tasks;

public interface ICosmicImprintsService
{
    Task<CosmicImprints> GetCosmicImprintByIdAsync(string id);
    Task<InsertOrUpdateResult<CosmicImprints>> InsertCosmicImprintAsync(CosmicImprints hisn);
    Task<InsertOrUpdateResult<CosmicImprints>> UpdateCosmicImprintAsync(CosmicImprints hisn);
}