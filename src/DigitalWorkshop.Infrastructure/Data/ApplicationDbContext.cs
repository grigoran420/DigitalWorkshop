using Microsoft.EntityFrameworkCore;
using DigitalWorkshop.Domain.Entities;

namespace DigitalWorkshop.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TechnologyProcess> TechnologyProcesses { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<BomItem> BomItems { get; set; }
        public DbSet<ToolRequirement> ToolRequirements { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связей и каскадного удаления
            modelBuilder.Entity<Operation>()
                .HasOne(o => o.TechnologyProcess)
                .WithMany(tp => tp.Operations)
                .HasForeignKey(o => o.TechnologyProcessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transition>()
                .HasOne(t => t.Operation)
                .WithMany(o => o.Transitions)
                .HasForeignKey(t => t.OperationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BomItem>()
                .HasOne(b => b.Transition)
                .WithMany(t => t.BomItems)
                .HasForeignKey(b => b.TransitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ToolRequirement>()
                .HasOne(t => t.Transition)
                .WithMany(t => t.Tools)
                .HasForeignKey(t => t.TransitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.TechnologyProcess)
                .WithMany(tp => tp.History)
                .HasForeignKey(a => a.TechnologyProcessId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}