using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF.Tables;

namespace SmartInvetory.DAL.EF;

public partial class SmartInventoryDbContext : DbContext
{
    public SmartInventoryDbContext()
    {
    }

    public SmartInventoryDbContext(DbContextOptions<SmartInventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<StockBalance> StockBalances { get; set; }

    public virtual DbSet<StockTransaction> StockTransactions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=dbconn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BE56A0613");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0529858E3").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8C55B7880");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CustomerName).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(30);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED3ABA8C76");

            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryID");

            entity.HasIndex(e => e.Barcode, "UQ__Products__177800D3E121A8AA").IsUnique();

            entity.HasIndex(e => e.Sku, "UQ__Products__CA1ECF0D9AB82E21").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProductName).HasMaxLength(150);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReorderLevel).HasDefaultValue(5);
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");
            entity.Property(e => e.Unit)
                .HasMaxLength(30)
                .HasDefaultValue("Piece");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__Purchase__036BAC4427DEAEA8");

            entity.HasIndex(e => e.SupplierId, "IX_PurchaseOrders_SupplierID");

            entity.HasIndex(e => e.PurchaseOrderNumber, "UQ__Purchase__9624194869131D73").IsUnique();

            entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PurchaseOrderNumber).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrders_Users");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrders_Suppliers");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOrders_Warehouses");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailId).HasName("PK__Purchase__5026B6F8ABAA1434");

            entity.Property(e => e.PurchaseOrderDetailId).HasColumnName("PurchaseOrderDetailID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");
            entity.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", true)
                .HasColumnType("decimal(29, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseDetails_Products");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseDetails_Orders");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A4098C147");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160AD296161").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK__SalesOrd__B14003C2CE69CE3A");

            entity.HasIndex(e => e.CustomerId, "IX_SalesOrders_CustomerID");

            entity.HasIndex(e => e.SalesOrderNumber, "UQ__SalesOrd__CF6C70EF0D01CC7A").IsUnique();

            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SalesOrderNumber).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrders_Users");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_SalesOrders_Customers");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrders_Warehouses");
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SalesOrderDetailId).HasName("PK__SalesOrd__6B9B5105F995C5B8");

            entity.Property(e => e.SalesOrderDetailId).HasColumnName("SalesOrderDetailID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            entity.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", true)
                .HasColumnType("decimal(29, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesDetails_Products");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesDetails_Orders");
        });

        modelBuilder.Entity<StockBalance>(entity =>
        {
            entity.HasKey(e => e.StockBalanceId).HasName("PK__StockBal__956815A7B6AAC4D1");

            entity.HasIndex(e => e.WarehouseId, "IX_StockBalances_WarehouseID");

            entity.HasIndex(e => new { e.ProductId, e.WarehouseId }, "UQ_StockBalances_ProductWarehouse").IsUnique();

            entity.Property(e => e.StockBalanceId).HasColumnName("StockBalanceID");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

            entity.HasOne(d => d.Product).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockBalances_Products");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockBalances_Warehouses");
        });

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasKey(e => e.StockTransactionId).HasName("PK__StockTra__C6E6C52AA4F228D5");

            entity.HasIndex(e => e.ProductId, "IX_StockTransactions_ProductID");

            entity.HasIndex(e => e.TransactionDate, "IX_StockTransactions_TransactionDate");

            entity.Property(e => e.StockTransactionId).HasColumnName("StockTransactionID");
            entity.Property(e => e.Notes).HasMaxLength(300);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PurchaseOrderDetailId).HasColumnName("PurchaseOrderDetailID");
            entity.Property(e => e.ReferenceNumber).HasMaxLength(100);
            entity.Property(e => e.SalesOrderDetailId).HasColumnName("SalesOrderDetailID");
            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TransactionType).HasMaxLength(20);
            entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransactions_Users");

            entity.HasOne(d => d.Product).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransactions_Products");

            entity.HasOne(d => d.PurchaseOrderDetail).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.PurchaseOrderDetailId)
                .HasConstraintName("FK_StockTransactions_PurchaseDetails");

            entity.HasOne(d => d.SalesOrderDetail).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.SalesOrderDetailId)
                .HasConstraintName("FK_StockTransactions_SalesDetails");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransactions_Warehouses");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666943DECA2A1");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.SupplierName).HasMaxLength(150);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB91F0D07");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E48B2F96BD").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342FD23491").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__Warehous__2608AFD950DE138B");

            entity.HasIndex(e => e.WarehouseName, "UQ__Warehous__180C89D93DC6895E").IsUnique();

            entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.ManagerName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.WarehouseName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
