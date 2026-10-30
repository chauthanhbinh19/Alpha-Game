using System.Threading.Tasks;

public interface IPrimordialImprintsService
{
    Task<PrimordialImprints> GetPrimordialImprintByIdAsync(string id);
    Task<InsertOrUpdateResult<PrimordialImprints>> InsertPrimordialImprintAsync(PrimordialImprints hisn);
    Task<InsertOrUpdateResult<PrimordialImprints>> UpdatePrimordialImprintAsync(PrimordialImprints hisn);
}