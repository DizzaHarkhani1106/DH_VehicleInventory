using DH_VehicleInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH_VehicleInventory.Infrastructure.Configurations
{
      public class DH_VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
        {
            public void Configure(EntityTypeBuilder<Vehicle> builder)
            {
                builder.ToTable("DH_Vehicles");

                builder.HasKey(v => v.Id);

                builder.Property(v => v.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(v => v.VehicleCode)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(v => v.LocationId)
                    .IsRequired();

                builder.Property(v => v.VehicleType)
                    .IsRequired()
                    .HasConversion<int>();

                builder.Property(v => v.Status)
                    .IsRequired()
                    .HasConversion<int>();

                builder.HasIndex(v => v.VehicleCode)
                    .HasDatabaseName("IX_DH_Vehicles_VehicleCode");

                builder.HasIndex(v => v.Status)
                    .HasDatabaseName("IX_DH_Vehicles_Status");
            }
        }
    }

