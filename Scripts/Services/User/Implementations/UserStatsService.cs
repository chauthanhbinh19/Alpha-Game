using System.Threading.Tasks;

public class UserStatsService : IUserStatsService
{
    // Khởi tạo Factory hoặc dùng DI như dự án của bạn
    public static IUserStatsService Create() => ServiceContainer.GetService<IUserStatsService>();

    public async Task<UserStatsContextDTO> GetUserStatsContextAsync(string userId)
    {
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(userId);
        var scienceFictionTask = UserScienceFictionsService.Create().GetSumUserScienceFictionsAsync(userId);
        var researchTask = UserResearchsService.Create().GetSumUserResearchsAsync(userId);
        var archiveTask = UserArchivesService.Create().GetSumUserArchivesAsync(userId);
        var universeTask = UserUniversesService.Create().GetSumUserUniversesAsync(userId);
        var hiinTask = UserHIINsService.Create().GetSumUserHIINsAsync(userId);
        var sswnTask = UserSSWNsService.Create().GetSumUserSSWNsAsync(userId);
        var hitnTask = UserHITNsService.Create().GetSumUserHITNsAsync(userId);
        var hihnTask = UserHIHNsService.Create().GetSumUserHIHNsAsync(userId);
        var hienTask = UserHIENsService.Create().GetSumUserHIENsAsync(userId);
        var hicaTask = UserHICAsService.Create().GetSumUserHICAsAsync(userId);
        var hirnTask = UserHIRNsService.Create().GetSumUserHIRNsAsync(userId);
        var hidcTask = UserHIDCsService.Create().GetSumUserHIDCsAsync(userId);
        var hicbTask = UserHICBsService.Create().GetSumUserHICBsAsync(userId);
        var hisnTask = UserHISNsService.Create().GetSumUserHISNsAsync(userId);
        var animeStatsTask = UserAnimesService.Create().GetSumUserAnimesAsync(userId);

        await Task.WhenAll(
            powerManagerTask, scienceFictionTask, researchTask, archiveTask, universeTask,
            hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
            hidcTask, hicbTask, hisnTask, animeStatsTask
        );

        return new UserStatsContextDTO
        {
            PowerManagerData = await powerManagerTask,
            ScienceFictionData = await scienceFictionTask,
            ResearchData = await researchTask,
            ArchiveData = await archiveTask,
            UniverseData = await universeTask,
            HiinData = await hiinTask,
            SswnData = await sswnTask,
            HitnData = await hitnTask,
            HihnData = await hihnTask,
            HienData = await hienTask,
            HicaData = await hicaTask,
            HirnData = await hirnTask,
            HidcData = await hidcTask,
            HicbData = await hicbTask,
            HisnData = await hisnTask,
            AnimeStatsData = await animeStatsTask
        };
    }
}