using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weaver.Models.Entities;

namespace Weaver.Infrastructure.Data.Configurations
{
    public class NutritionsConfiguration : IEntityTypeConfiguration<Nutritions>
    {
        public void Configure(EntityTypeBuilder<Nutritions> builder)
        {
            builder.ToTable("Nutritions");
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Carbohydrates)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(n => n.Fat)
                .IsRequired()
                .HasPrecision(18, 2); 

            builder.Property(n => n.Sugar)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(n => n.Protein)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(n => n.Calories)
                .IsRequired();                
        }
    }
}
