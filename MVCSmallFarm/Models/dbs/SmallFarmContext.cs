using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCSmallFarm.Models.dbs;

public partial class SmallFarmContext : DbContext
{
    public SmallFarmContext()
    {
    }

    public SmallFarmContext(DbContextOptions<SmallFarmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EcommerceSummary> EcommerceSummaries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductWithComment> ProductWithComments { get; set; }

    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public virtual DbSet<UserWithPoint> UserWithPoints { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("SmallFarm"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<EcommerceSummary>(entity =>
        {
            entity.ToTable("ECommerceSummary");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.NetDc).HasColumnName("NetDC");
            entity.Property(e => e.NetVat).HasColumnName("NetVAT");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNo).HasMaxLength(30);
            entity.Property(e => e.PaidDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiveDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.UserIp)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Vatrate).HasColumnName("VATRate");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId).HasMaxLength(100);
            entity.Property(e => e.DiscountPerUnit).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.CommentTotals).HasDefaultValue(0);
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(100);
            entity.Property(e => e.PointTotals).HasDefaultValue(0);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<ProductWithComment>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.RunningNumber }).HasName("PK_ProductWithComment_1");

            entity.ToTable("ProductWithComment");

            entity.Property(e => e.Comment)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CommentDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UserIp)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductWithComments)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductWithComment_Product");
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.ToTable("ShoppingCartItem");

            entity.Property(e => e.ShoppingCartId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingCartItem_Product");
        });

        modelBuilder.Entity<UserWithPoint>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId });

            entity.ToTable("UserWithPoint");

            entity.Property(e => e.UserId).HasMaxLength(100);
            entity.Property(e => e.PointDate).HasColumnType("datetime");
            entity.Property(e => e.UserIp)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.UserWithPoints)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserWithPoint_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
