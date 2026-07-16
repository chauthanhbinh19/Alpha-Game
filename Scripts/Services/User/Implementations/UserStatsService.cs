using System.Threading.Tasks;

public class UserStatsService : IUserStatsService
    {
        // Khởi tạo Factory hoặc dùng DI như dự án của bạn
        public static IUserStatsService Create()
        {
            return new UserStatsService();
        }

        public async Task<UserStatsContextDTO> GetUserStatsContextAsync(string user_id)
        {
            var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
            var scienceFictionTask = UserScienceFictionsService.Create().GetSumUserScienceFictionsAsync(user_id);
            var researchTask = UserResearchsService.Create().GetSumUserResearchsAsync(user_id);
            var archiveTask = UserArchivesService.Create().GetSumUserArchivesAsync(user_id);
            var universeTask = UserUniversesService.Create().GetSumUserUniversesAsync(user_id);
            var hiinTask = UserHIINsService.Create().GetSumUserHIINsAsync(user_id);
            var sswnTask = UserSSWNsService.Create().GetSumUserSSWNsAsync(user_id);
            var hitnTask = UserHITNsService.Create().GetSumUserHITNsAsync(user_id);
            var hihnTask = UserHIHNsService.Create().GetSumUserHIHNsAsync(user_id);
            var hienTask = UserHIENsService.Create().GetSumUserHIENsAsync(user_id);
            var hicaTask = UserHICAsService.Create().GetSumUserHICAsAsync(user_id);
            var hirnTask = UserHIRNsService.Create().GetSumUserHIRNsAsync(user_id);
            var hidcTask = UserHIDCsService.Create().GetSumUserHIDCsAsync(user_id);
            var hicbTask = UserHICBsService.Create().GetSumUserHICBsAsync(user_id);
            var hisnTask = UserHISNsService.Create().GetSumUserHISNsAsync(user_id);
            var animeStatsTask = UserAnimesService.Create().GetSumUserAnimesAsync(user_id);

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