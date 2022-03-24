using DynamicConfigLibrary;
using DynamicConfigLibrary.Models;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Organization.Jobs
{
    public class ConfigCacherJob : BaseJob
    {
        private readonly ConfigurationReader _configReader;
        public ConfigCacherJob(ConfigurationReader configurationReader)
        {
            _configReader = configurationReader;
        }
        public override Task OnExecute(IJobExecutionContext context)
        {
            using (StreamReader r = new StreamReader("CachedData.json"))
            {
                string json = r.ReadToEnd();
                ConfigurationData data = JsonConvert.DeserializeObject<ConfigurationData>(json);
                _configReader.SetValue(data.ApplicationName , data);
            }
            return Task.CompletedTask;
        }
    }
}
