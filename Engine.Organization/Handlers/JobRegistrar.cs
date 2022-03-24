using Quartz;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Engine.Organization.Handlers
{
    public class JobRegistrar
    {
        private readonly IConfigurationRoot _configuration;

        public JobRegistrar(IConfigurationRoot configuration)
        {
            this._configuration = configuration;
        }

        public void RegisterJobs(IScheduler scheduler)
        {
            var instanceName = "AsirGroup.OrganizationService";
            var jobs = new List<ScheduledJob>()
            {
                new ScheduledJob()
                {
                    Id = 1,
                    CronExpression = _configuration["CronSettings:ConfigCacherJobCron"],
                    JobClass = "Engine.Organization.Jobs.ConfigCacherJob, Engine.Organization",
                    JobName = "ConfigCacherJob"
                }
            };

            foreach (var job in jobs)
            {
                Type type = Type.GetType(job.JobClass, true);

                IJobDetail jobDetail = JobBuilder.Create(type)
                       .WithIdentity($"{job.Id}_{instanceName}_{job.JobName}")
                       .Build();

                TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                    .WithIdentity(job.JobName + "Trigger" + job.Id.ToString())
                    .StartNow();
                
                triggerBuilder = triggerBuilder.WithCronSchedule(job.CronExpression);                

                var trigger = triggerBuilder.Build();

                scheduler.ScheduleJob(jobDetail, trigger);

                //log.WarnFormat("Job scheduled {0} trigger: {1}, interval: {2}, cron: {3}", job.JobClass, job.TriggerType, job.IntervalInSeconds, job.CronExpression);
            }

        }
    }
}
