using Quartz;
using System;
using System.Threading.Tasks;

namespace Engine.Organization.Jobs
{
    public abstract class BaseJob : IJob
    {

        public BaseJob()
        {
            
        }

        public abstract Task OnExecute(IJobExecutionContext context);

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine(context.JobDetail.Key.Name + " started");
                await this.OnExecute(context);
            }
            catch (Exception ex)
            {
                //Log.Error("BaseJob OnExecute Error", ex);
            }

        }
    }
}
