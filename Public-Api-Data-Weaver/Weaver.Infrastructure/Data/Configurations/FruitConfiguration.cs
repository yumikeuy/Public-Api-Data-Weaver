using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weaver.Models.Entities;

namespace Weaver.Infrastructure.Data.Configurations
{
    public class FruitConfiguration : IEntityTypeConfiguration<Fruit>
    {
        public void Configure(EntityTypeBuilder<Fruit> builder)
        {
            builder.ToTable("Fruits");
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.Nutritions)         
                .WithOne(n => n.Fruit)
                .HasForeignKey<Nutritions>(n => n.FruitId);

            var highVitaminsConverter = new ValueConverter<HashSet<char>, string>(
                v => new string(v.ToArray()),
                v => v.ToCharArray().ToHashSet()
            );

            var highVitaminsComparer = new ValueComparer<HashSet<char>>(
                (v1, v2) => v1 != null && v2 != null && v1.SetEquals(v2),
                v => v.Aggregate(0, (a, v) => a ^ v.GetHashCode()),
                v => new HashSet<char>(v)
            );

            builder.Property(f => f.HighVitamins)
                .HasConversion(highVitaminsConverter)
                .Metadata.SetValueComparer(highVitaminsComparer);

            var fitnessCategoryConverter = new ValueConverter<HashSet<string>, string>(
                v => string.Join(',', v),
                v => string.IsNullOrEmpty(v) ? new HashSet<string>() :
                v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet()
            );

            var fitnessCategoryComparer = new ValueComparer<HashSet<string>>(
                (fc1, fc2) => fc1!.SetEquals(fc2!),
                fc => fc.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                fc => fc.ToHashSet()
            );

            builder.Property(f => f.FitnessCategories)
                .HasConversion(fitnessCategoryConverter)
                .Metadata.SetValueComparer(fitnessCategoryComparer);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Genus)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Order)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Family)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.ProteinPerCalorie)
                .HasPrecision(18, 2);
        }
    }
}
