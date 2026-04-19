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
                (c1, c2) => c1 != null && c2 != null && c1.SetEquals(c2),
                c => c.Aggregate(0, (a, v) => a ^ v.GetHashCode()),
                c => new HashSet<char>(c)
            ); 

            builder.Property(f => f.HighVitamins)
                .HasConversion(highVitaminsConverter)
                .Metadata.SetValueComparer(highVitaminsComparer);

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

            builder.Property(f => f.FitnessCategory)
                .HasMaxLength(100);

            builder.Property(f => f.ProteinPerCalorie)
                .HasPrecision(18, 2);
        }
    }
}
