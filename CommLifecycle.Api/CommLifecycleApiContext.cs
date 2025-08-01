using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
public class CommLifecycleApiContext : DbContext
{
  public CommLifecycleApiContext(DbContextOptions<CommLifecycleApiContext> options) : base(options)
  {
  }
  public DbSet<Communication> Communications { get; set; }
  public DbSet<CommunicationStatus> CommunicationStatuses { get; set; }
  public DbSet<CommunicationStatusChange> CommunicationStatusChanges { get; set; }
  public DbSet<CommunicationType> CommunicationTypes { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CommunicationStatusChange>()
      .HasOne<Communication>()
      .WithMany(c => c.StatusHistory)
      .HasForeignKey(c => c.CommunicationId)
      .HasPrincipalKey(c => c.Id)
      .OnDelete(DeleteBehavior.Restrict);
  }
}