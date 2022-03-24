
namespace Engine.Organization
{
    public class ScheduledJob
	{
		public int Id { get; set; }
		public string JobName { get; set; }
		public string JobClass { get; set; }
		public string CronExpression { get; set; }
		public int? IntervalInSeconds { get; set; }
		public bool IsActive { get; set; }
	}
}
