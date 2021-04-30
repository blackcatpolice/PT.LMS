using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PageTechsLMS.DataCore.Community;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.File;
using PageTechsLMS.DataCore.MemberCourse;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.DataCore.Message;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.DataCore.Payments;
using PageTechsLMS.DataCore.Setting;
using PageTechsLMS.DataCore.Setting.Basic; 
using PageTechsLMS.DataCore.Setting.File;
using PageTechsLMS.DataCore.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.DbContexts
{
    public class PageTechsLMSDbContext : IdentityDbContext<MemberAccount, MemberRole, string>, IPersistedGrantDbContext
    {
        public PageTechsLMSDbContext(DbContextOptions<PageTechsLMSDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DeviceFlowCodes>().HasNoKey();
            builder.Entity<PersistedGrant>().HasKey("Key");
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionItem> SectionItems { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Tags> Tags { get; set; }
        //public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Topics> Topics { get; set; }

        public DbSet<MemberCourseLearnLog> MemberCourseLearnLogs { get; set; }
        public DbSet<MemberCoursePayLog> MemberCoursePayLogs { get; set; }
        public DbSet<MemberFeedbackLog> MemberFeedbackLogs { get; set; }

        //public DbSet<MemberAccount> MemberAccount { get; set; }
        public DbSet<MemberBind> MemberBinds { get; set; }
        public DbSet<MemberInfo> MemberInfos { get; set; }

        public DbSet<Messagebox> Messageboxes { get; set; }

        public DbSet<CourseOrder> CourseOrder { get; set; }

        public DbSet<Paylog> Paylogs { get; set; }

        //public DbSet<SettingModel> Settings { get; set; }

        public DbSet<FilebaseInfo> FilebaseInfo { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        #region Setting

        public DbSet<SiteSetting> SiteSettings { get; set; }



        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }
    }
}
