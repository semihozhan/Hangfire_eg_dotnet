namespace Hangfire_eg_dotnet.Service
{
    public class JobService : IJobService
    {
        public JobService() { }

        public void ContinuationJob()
        {
            Console.WriteLine("Testing... Continuation Job");
        }

        public void DelayedJob()
        {
            Console.WriteLine("Testing... Delayed Job");
        }

        public void FireAndForgetJob()
        {
            Console.WriteLine("Testing... Fire and Forget Job");
        }

        public void ReccuringJob()
        {
            Console.WriteLine("Testing... Scheduled Job");
        }
    }
}
