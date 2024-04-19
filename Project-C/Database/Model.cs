using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace Model
{
    public class MyContext : DbContext 
    {

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
          optionsBuilder.UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=;Database=ProjCDone4;Maximum Pool Size=200");
            // .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information); 
        
        
        public DbSet<Manager_and_User> Manager_and_User { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<OGSM> OGSM { get; set; } = null!;
        public DbSet<Goals> Goals { get; set; } = null!;
        public DbSet<Strategies> Strategies { get; set; } = null!;
        public DbSet<Actions> Actions { get; set; } = null!;
        public DbSet<OGSMs_Per_User> OGSMs_Per_User { get; set; } = null!;
        public DbSet<Goals_In_OGSM> Goals_In_OGSM { get; set; } = null!;
        public DbSet<Strategies_In_OGSM> Strategies_In_OGSM { get; set; } = null!;
        public DbSet<Actions_In_OGSM> Actions_In_OGSM { get; set; } = null!;

        // 1 User has Many OGSMs
        // 1 OGSM has Many Goals
        // 1 OGSM has Many Strategies
        // 1 Strategie has Many Actions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Manager_and_User>()
            .HasOne(MAU => MAU.Users)
            .WithMany(U => U.Manager_and_User)
            .HasForeignKey(MAU => MAU.User_Id);

            modelBuilder.Entity<Manager_and_User>()
            .HasOne(MAU => MAU.Users)
            .WithMany(U => U.Manager_and_User)
            .HasForeignKey(MAU => MAU.Manager_Id);

            // 1 User has Many OGSMs
            modelBuilder.Entity<OGSMs_Per_User>()
            .HasOne(OPU => OPU.Users)
            .WithMany(U => U.OGSMs_Per_User)
            .HasForeignKey(OPU => OPU.User_Id);

            modelBuilder.Entity<OGSMs_Per_User>()
            .HasOne(OPU => OPU.OGSM)
            .WithMany(OGSM => OGSM.OGSMs_Per_User)
            .HasForeignKey(OPU => OPU.OGSM_Id);


            // 1 OGSM has Many Goals
            modelBuilder.Entity<Goals_In_OGSM>()
            .HasOne(GIO => GIO.Goal)
            .WithMany(G => G.Goals_In_OGSM)
            .HasForeignKey(GIO => GIO.Goal_Id);

            modelBuilder.Entity<Goals_In_OGSM>()
            .HasOne(GIO => GIO.OGSM)
            .WithMany(OGSM => OGSM.Goals_In_OGSM)
            .HasForeignKey(GIO => GIO.OGSM_Id);


            // 1 OGSM has Many Strategies
            modelBuilder.Entity<Strategies_In_OGSM>()
            .HasOne(SIO => SIO.Strategie)
            .WithMany(S => S.Strategies_In_OGSM)
            .HasForeignKey(SIO => SIO.Strategie_Id);
            
            modelBuilder.Entity<Strategies_In_OGSM>()
            .HasOne(SIO => SIO.OGSM)
            .WithMany(OGSM => OGSM.Strategies_In_OGSM)
            .HasForeignKey(SIO => SIO.OGSM_Id);


            // 1 OGSM has Many Actions
            modelBuilder.Entity<Actions_In_OGSM>()
            .HasOne(AIO => AIO.Action)
            .WithMany(A => A.Actions_In_OGSM)
            .HasForeignKey(AIO => AIO.Action_Id);

            modelBuilder.Entity<Actions_In_OGSM>()
            .HasOne(AIO => AIO.OGSM)
            .WithMany(OGSM => OGSM.Actions_In_OGSM)
            .HasForeignKey(AIO => AIO.OGSM_Id);
        }
      
    }
    

    public record Manager_and_User (Guid ID, Guid Manager_Id, Guid User_Id)
    {
        public Users Users { get; set; } = null!;
    }

    public record Users (Guid ID, string Email, string First_name, string Last_name, string Password, string User_type) // User-type = Manager or User
    {
        public List<OGSMs_Per_User> OGSMs_Per_User { get; set; } = null!;
        public List<Manager_and_User> Manager_and_User { get; set; } = null!;
    }

    public record OGSMs_Per_User (Guid ID, Guid User_Id, Guid OGSM_Id)
    {
        public Users Users { get; set; } = null!;
        public OGSM OGSM { get; set; } = null!;
    }
    public record OGSM (Guid ID, string Name, string Ambition = "")
    {
        public List<OGSMs_Per_User> OGSMs_Per_User { get; set; } = null!;
        public List<Goals_In_OGSM> Goals_In_OGSM { get; set; } = null!;
        public List<Strategies_In_OGSM> Strategies_In_OGSM { get; set; } = null!;
        public List<Actions_In_OGSM> Actions_In_OGSM { get; set; } = null!;
    }

    public record Goals_In_OGSM (Guid ID, Guid OGSM_Id, Guid Goal_Id)
    {
        public Goals Goal { get; set; } = null!;
        public OGSM OGSM { get; set; } = null!;
    }

    public record Strategies_In_OGSM (Guid ID, Guid OGSM_Id, Guid Strategie_Id)
    {
        public Strategies Strategie { get; set; } = null!;
        public OGSM OGSM { get; set; } = null!;
    }

    public record Actions_In_OGSM (Guid ID, Guid OGSM_Id, Guid Action_Id)
    {
        public Actions Action { get; set; } = null!;
        public OGSM OGSM { get; set; } = null!;
    }

    public record Goals (Guid ID, string Name)
    {
        public List<Goals_In_OGSM> Goals_In_OGSM { get; set; } = null!;
    }

    public record Strategies (Guid ID, string Name)
    {
        public List<Strategies_In_OGSM> Strategies_In_OGSM { get; set; } = null!;
    }

    public record Actions (Guid ID, string Name, bool Done = false)
    {
        public List<Actions_In_OGSM> Actions_In_OGSM { get; set; } = null!;
    }
}