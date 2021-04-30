using Microsoft.Extensions.DependencyInjection;
using Pagetechs.Framework.DbContexts;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Filebase;
using PageTechsLMS.Service.Finance;
using PageTechsLMS.Service.Member;
using PageTechsLMS.Service.Message;
using PageTechsLMS.Service.Orders;
using PageTechsLMS.Service.Pay;
using PageTechsLMS.Service.Settings;
using PageTechsLMS.Service.Teachers;
using PageTechsLMS.Service.Wx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddLMSService(this IServiceCollection services)
        {
            services
                .UseDbConfig<PageTechsLMSDbContext>()

            #region Course
                .AddTransient<CourseExploreService>()
                .AddTransient<CourseLearnService>()
                .AddTransient<CourseManageService>()
                .AddTransient<CoursePayService>()
            #endregion

            #region Finance
                .AddTransient<FinancePaltformService>()
                .AddTransient<FinanceStudentService>()
            #endregion

            #region Member
                .AddTransient<MemberAuthService>()
                .AddTransient<MemberInfoSerivce>()
                .AddTransient<MemberManageService>()
            #endregion

            #region Message
                .AddTransient<MessageRecieveService>()
                .AddTransient<MessageSendSerivce>()
            #endregion

            #region Pay
                .AddTransient<PayService>()
            #endregion

            #region Teacher
                .AddTransient<TeacherExploreService>()
                .AddTransient<TeacherManageService>()
            #endregion

                .AddTransient<CouseOrderService>()

                .AddTransient<WxPayService>()

                .AddTransient<WxService>()

                .AddTransient<FilebaseService>()

                .AddTransient<SettingService>();


            return services;
        }
    }
}
