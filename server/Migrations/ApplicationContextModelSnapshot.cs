﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Model.Data;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("server.Model.Employee", b =>
                {
                    b.Property<string>("loginEmp")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("fisrstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("middleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordEmp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("loginEmp");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("server.Model.EmployeeOrganizationMap", b =>
                {
                    b.Property<string>("loginEmp")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("orgId")
                        .HasColumnType("int");

                    b.HasKey("loginEmp", "orgId");

                    b.HasIndex("orgId");

                    b.ToTable("EmployeeOrganizationMaps");
                });

            modelBuilder.Entity("server.Model.Organization", b =>
                {
                    b.Property<int>("orgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orgId"));

                    b.Property<string>("nameOrg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("orgId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("server.Model.EmployeeOrganizationMap", b =>
                {
                    b.HasOne("server.Model.Employee", "Employee")
                        .WithMany("EmployeeOrganizationMaps")
                        .HasForeignKey("loginEmp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Model.Organization", "Organization")
                        .WithMany("EmployeeOrganizationMaps")
                        .HasForeignKey("orgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("server.Model.Employee", b =>
                {
                    b.Navigation("EmployeeOrganizationMaps");
                });

            modelBuilder.Entity("server.Model.Organization", b =>
                {
                    b.Navigation("EmployeeOrganizationMaps");
                });
#pragma warning restore 612, 618
        }
    }
}
