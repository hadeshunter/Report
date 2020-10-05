using ModelClass.auth;
using ModelClass.model.proccess;
using ModelClass.model.sms;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace ModelClassLibrary.connection
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataContext")
        {
        }

        //public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public DbSet<Process> Process { get; set; }
        public DbSet<SLAData> SLAData { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<Timer> Timer { get; set; }
        public DbSet<PhoneSended> PhoneSended { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}
