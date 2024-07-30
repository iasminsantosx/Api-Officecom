using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<PersonPhone> PersonPhones { get; set; }
    public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonPhone>()
            .HasKey(pp => new { pp.BusinessEntityID, pp.PhoneNumberTypeID });

        // Remova a referência à propriedade PhoneNumberType
        modelBuilder.Entity<PersonPhone>()
            .HasOne<PhoneNumberType>() // Remova a propriedade PhoneNumberType
            .WithMany(pt => pt.PersonPhones)
            .HasForeignKey(pp => pp.PhoneNumberTypeID);
    }
}
