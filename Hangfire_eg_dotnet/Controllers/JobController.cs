using Hangfire;
using Hangfire_eg_dotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire_eg_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobController(ILogger<JobController> logger, IJobService jobService,IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _logger = logger;
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet(Name = "FireAndForgetJob")]
        public ActionResult TestFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet(Name = "DelayedJob")]
        public ActionResult TestDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _jobService.DelayedJob(), TimeSpan.FromSeconds(60));
            return Ok();
        }

        [HttpGet(Name = "ReccuringJob")]
        public ActionResult TestReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.ReccuringJob(), Cron.Minutely);
            return Ok();
        }

        [HttpGet("ContinuationJob")]
        public ActionResult TestContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobService.ContinuationJob());

            return Ok();
        }
    }
}
