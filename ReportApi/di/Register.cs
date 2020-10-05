using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ModelClassLibrary.connection;
using ModelClassLibrary.imlp;
using ModelClassLibrary.interfaces;
using Permission;
using Permission.impl;
using REPORT.service.auth;
using REPORT.service.auth.impl;
using REPORT.service.holiday;
using REPORT.service.holiday.impl;
using REPORT.service.impl;
using REPORT.service.process;
using REPORT.service.process.impl;
using REPORT.service.report;
using REPORT.service.report.impl;
using REPORT.service.sms;
using REPORT.service.sms.impl;
using REPORT.service.user;
using REPORT.service.user.impl;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace REPORT.di
{
    public class Register
    {
        public static void builder()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            //builder.RegisterType<StoreDbContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<ReportImpl>().As<IReport>().InstancePerRequest();
            builder.RegisterType<AuthImpl>().As<IAuth>().WithParameter("context", new PermissionContext());
            builder.RegisterType<UserImpl>().As<IUser>().WithParameter("context", new PermissionContext());
            builder.RegisterType<HashPass>().As<IHashPass>().InstancePerRequest();
            builder.RegisterType<ProcessImpl>().As<IProcessReport>().InstancePerRequest("context", new DataContext());
            builder.RegisterType<SLAImpl>().As<ISLA>().WithParameter("context", new DataContext());
            builder.RegisterType<SmsImpl>().As<ISms>().WithParameter("context", new DataContext());
            builder.RegisterType<TimerImpl>().As<ITimer>().WithParameter("context", new DataContext());
            builder.RegisterType<ProcessImpl>().As<IProcessReport>().WithParameter("context", new DataContext());
            builder.RegisterType<PhoneSendedImpl>().As<IPhoneSended>().WithParameter("context", new DataContext());
            builder.RegisterType<HolidayImpl>().As<IHoliday>().InstancePerRequest();
            builder.RegisterType<GroupImpl>().As<IGroup>().WithParameter("context", new PermissionContext());
            builder.RegisterType<GroupPermissionImpl>().As<IGroupPermission>().WithParameter("context", new PermissionContext());
            builder.RegisterType<PermissionImpl>().As<IPermission>().WithParameter("context", new PermissionContext());
            builder.RegisterType<UserPerImpl>().As<IUserPer>().WithParameter("context", new PermissionContext());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);

        }
    }
}