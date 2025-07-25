﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using e_commercial.Models;

namespace e_commercial.Data;

public partial class ReagvnContext : DbContext
{
    public ReagvnContext()
    {
    }

    public ReagvnContext(DbContextOptions<ReagvnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Keyboard> Keyboards { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:MySQLConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PRIMARY");

            entity.ToTable("branches");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.BranchId)
                .HasMaxLength(36)
                .HasColumnName("branch_id");
            entity.Property(e => e.BranchAddress)
                .HasMaxLength(255)
                .HasColumnName("branch_address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BranchName)
                .HasMaxLength(255)
                .HasColumnName("branch_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Branches)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("branches_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.ToTable("departments");

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(36)
                .HasColumnName("department_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(255)
                .HasColumnName("department_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.BranchId, "branch_id");

            entity.HasIndex(e => e.DepartmentId, "department_id");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .HasColumnName("employee_id");
            entity.Property(e => e.BranchId)
                .HasMaxLength(36)
                .HasColumnName("branch_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(36)
                .HasColumnName("department_id");
            entity.Property(e => e.EmployeeGender)
                .HasMaxLength(4)
                .HasColumnName("employee_gender")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .HasColumnName("employee_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Branch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("employees_ibfk_2");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("employees_ibfk_1");
        });

        modelBuilder.Entity<Keyboard>(entity =>
        {
            entity.HasKey(e => e.KeyboardId).HasName("PRIMARY");

            entity.ToTable("keyboards");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.ManufacturerId, "manufacturer_id");

            entity.Property(e => e.KeyboardId)
                .HasMaxLength(36)
                .HasColumnName("keyboard_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.KeyboardDescription)
                .HasMaxLength(255)
                .HasColumnName("keyboard_description")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.KeyboardImage)
                .HasColumnType("json")
                .HasColumnName("keyboard_image");
            entity.Property(e => e.KeyboardName)
                .HasMaxLength(255)
                .HasColumnName("keyboard_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.KeyboardSwitch)
                .HasMaxLength(255)
                .HasColumnName("keyboard_switch")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ManufacturerId)
                .HasMaxLength(36)
                .HasColumnName("manufacturer_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Category).WithMany(p => p.Keyboards)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("keyboards_ibfk_2");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Keyboards)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("keyboards_ibfk_1");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasKey(e => e.LaptopId).HasName("PRIMARY");

            entity.ToTable("laptops");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.ManufacturerId, "manufacturer_id");

            entity.Property(e => e.LaptopId)
                .HasMaxLength(36)
                .HasColumnName("laptop_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.LaptopDescription)
                .HasMaxLength(255)
                .HasColumnName("laptop_description")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.LaptopImage)
                .HasColumnType("json")
                .HasColumnName("laptop_image");
            entity.Property(e => e.LaptopName)
                .HasMaxLength(255)
                .HasColumnName("laptop_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.LaptopSize).HasColumnName("laptop_size");
            entity.Property(e => e.ManufacturerId)
                .HasMaxLength(36)
                .HasColumnName("manufacturer_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Category).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laptops_ibfk_2");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laptops_ibfk_1");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PRIMARY");

            entity.ToTable("manufacturers");

            entity.Property(e => e.ManufacturerId)
                .HasMaxLength(36)
                .HasColumnName("manufacturer_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.ManufacturerAddress)
                .HasMaxLength(255)
                .HasColumnName("manufacturer_address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(255)
                .HasColumnName("manufacturer_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserAddress)
                .HasMaxLength(255)
                .HasColumnName("user_address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserDistrict)
                .HasMaxLength(255)
                .HasColumnName("user_district");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .HasColumnName("user_email");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(12)
                .HasColumnName("user_phone");
            entity.Property(e => e.UserRole)
                .HasMaxLength(255)
                .HasColumnName("user_role")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserShownname)
                .HasMaxLength(255)
                .HasColumnName("user_shownname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserWard)
                .HasMaxLength(255)
                .HasColumnName("user_ward");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(255)
                .HasColumnName("userpassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
