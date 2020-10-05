
using ModelClass.auth;
using ModelClassLibrary.area.auth;
using ModelClassLibrary.area.auth.roles;
using System.Data.Entity;

namespace ModelClassLibrary.connection
{
    public class PermissionContext : DbContext 
    {
        public PermissionContext() :  base("PermissionContext")
        {
        }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<GroupPermissions> GroupPermissions { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionTranslations> PermissionTranslations { get; set; }
        public DbSet<Languages> Languages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermissions>().HasKey(i => new { i.usid, i.groupid });
            modelBuilder.Entity<GroupPermissions>().HasKey(i => new { i.groupid, i.perid });
        }
    }
}
