using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TranslationProgress> TranslationProgresses { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<PatchUpdate> PatchUpdates { get; set; }
        public DbSet<DownloadDetail> DownloadDetails { get; set; }
        public DbSet<ProjectStaff> ProjectStaff { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectStaff>()
                .HasKey(ps => new { ps.Id, ps.UserId, ps.Role });

            modelBuilder.Entity<ProjectStaff>()
                .HasOne(ps => ps.ProjectDetail)
                .WithMany(pd => pd.StaffRoles)
                .HasForeignKey(ps => ps.ProjectDetailId);

            modelBuilder.Entity<ProjectStaff>()
                .HasOne(ps => ps.User)
                .WithMany(u => u.StaffRoles)
                .HasForeignKey(ps => ps.UserId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.TranslationProgress)
                .WithOne(tp => tp.Project)
                .HasForeignKey<TranslationProgress>(tp => tp.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Detail)
                .WithOne(pd => pd.Project)
                .HasForeignKey<ProjectDetail>(pd => pd.ProjectId);

            modelBuilder.Entity<ProjectDetail>()
                .HasOne(pd => pd.DownloadDetail)
                .WithOne(dd => dd.ProjectDetail)
                .HasForeignKey<DownloadDetail>(dd => dd.ProjectDetailId);
        }
    }
}