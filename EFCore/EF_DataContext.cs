using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Titan_Biometric.EFCore
{
    public class EF_DataContext : IdentityDbContext<IdentityUser>
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.UseSerialColumns();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
        }

        public  DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }
        public DbSet<SessionInfo> SessionsInfo { get; set; }
        public DbSet<Events> Events { get; set; }

    }
}
