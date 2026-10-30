using System.Threading.Tasks;

public interface IPrimordialImprintsRepository
{
    Task<PrimordialImprints> GetPrimordialImprintByIdAsync(string id);
    Task<InsertOrUpdateResult<PrimordialImprints>> InsertPrimordialImprintAsync(PrimordialImprints hisn);
    Task<InsertOrUpdateResult<PrimordialImprints>> UpdatePrimordialImprintAsync(PrimordialImprints hisn);
}