using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Pirate.Logger.Data.Models;
using System.Text.Json;

namespace Pirate.Logger.Data;

public class AppDbContext : DbContext
{
    public DbSet<LogEntry> Logs { get; set; }

    private string _location;

    public AppDbContext(IConfiguration configuration)
    {
        _location = configuration.GetSection("Pirate.Logger:TargetDirectory").Value ?? $"{Directory.GetCurrentDirectory()}\\logs";
        Database.EnsureCreated();
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _location = configuration.GetSection("Pirate.Logger:TargetDirectory").Value ?? $"{Directory.GetCurrentDirectory()}\\logs";
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Properties)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
                    v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new Dictionary<string, object>()
                )
                .Metadata.SetValueComparer(CreateDictionaryComparer());
            entity.Property(e => e.LogType)
                .HasConversion<string>();
        });
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!Directory.Exists(_location))
        {
            Directory.CreateDirectory(_location);
        }
        if (!File.Exists($"{_location}/database.db"))
        {
            File.Create($"{_location}/database.db").Close();
        }
        optionsBuilder.UseSqlite($"Data Source={_location}/database.db");
        base.OnConfiguring(optionsBuilder);
    }



    private ValueComparer<Dictionary<string, object>> CreateDictionaryComparer()
    {
        return new ValueComparer<Dictionary<string, object>>(
            (dict1, dict2) => dict1.SequenceEqual(dict2),
            dict => dict.Aggregate(0, (hash, pair) => HashCode.Combine(hash, pair.GetHashCode())),
            dict => dict.ToDictionary(pair => pair.Key, pair => pair.Value)
        );
    }
}
