using Quartz;
using Quartz.Impl;
using REPORT.schedule.impl;
using REPORT.service.sms;
using REPORT.service.sms.impl;
using ReportSoftWare.schedule;

namespace REPORT.schedule.quaz
{
    public class JobScheduler
    {
        public static void Start()
        {
            /*
             * get time query
             */
            ITimer timer = new TimerImpl(new ModelClassLibrary.connection.DataContext());


            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<QueryTime>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInSeconds(timer.getFirstTimer().totaltime)
                     .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();
            scheduler.ScheduleJob(job, trigger);

        }
    }
}