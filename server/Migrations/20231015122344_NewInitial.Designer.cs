﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Model.Data;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231015122344_NewInitial")]
    partial class NewInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("server.Model.Comments", b =>
                {
                    b.Property<string>("commentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("loginUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("commentId");

                    b.HasIndex("loginUser");

                    b.ToTable("Commentss");
                });

            modelBuilder.Entity("server.Model.Employee", b =>
                {
                    b.Property<string>("loginEmp")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("abbreviation")
                        .IsRequired()
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

                    b.HasIndex("abbreviation");

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

            modelBuilder.Entity("server.Model.Gender", b =>
                {
                    b.Property<string>("abbreviation")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("meaning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("abbreviation");

                    b.ToTable("Genders");
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

            modelBuilder.Entity("server.Model.Users", b =>
                {
                    b.Property<string>("loginUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("passwordUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("loginUser");

                    b.ToTable("Userss");
                });

            modelBuilder.Entity("server.Model.Comments", b =>
                {
                    b.HasOne("server.Model.Users", "Users")
                        .WithMany()
                        .HasForeignKey("loginUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("server.Model.Employee", b =>
                {
                    b.HasOne("server.Model.Gender", "Gender")
                        .WithMany("Employees")
                        .HasForeignKey("abbreviation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
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

            modelBuilder.Entity("server.Model.Gender", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("server.Model.Organization", b =>
                {
                    b.Navigation("EmployeeOrganizationMaps");
                });
#pragma warning restore 612, 618
        }
    }
}