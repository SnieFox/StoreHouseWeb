using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;

namespace StoreHouse.Database.StoreHouseDbContext;

public class StoreHouseContext : DbContext
{
    public StoreHouseContext(DbContextOptions<StoreHouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Dish> Dishes { get; set; }
    public virtual DbSet<Ingredient> Ingredients { get; set; }
    public virtual DbSet<IngredientsCategory> IngredientsCategories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<ProductList> ProductLists { get; set; }
    public virtual DbSet<Receipt> Receipts { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<SemiProduct> SemiProducts { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Supply> Supplies { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<WriteOff> WriteOffs { get; set; }
    public virtual DbSet<WriteOffCause> WriteOffCauses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Relations Configuring
        //Ingredient-Category
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.ToTable("Ingredient");

            entity.HasKey(e => e.Id);
            
            entity.HasOne(c => c.Category)
                .WithMany(i => i.Ingredients)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Name);
            entity.Property(e => e.PrimeCost);
            entity.Property(e => e.Remains);
            entity.Property(e => e.Unit);
        });
            
        
        //Product-Category
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasKey(e => e.Id);
            
            entity.HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Name);
            entity.Property(e => e.PrimeCost);
            entity.Property(e => e.Price);
            entity.Property(e => e.ImageId);
        });
        
        //WriteOff
        modelBuilder.Entity<WriteOff>(entity =>
        {
            entity.ToTable("WriteOff");

            entity.HasKey(e => e.Id);
            
            //WriteOff-User
            entity.HasOne(u => u.User)
                .WithMany(w => w.WriteOffs)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            //WriteOff-Cause
            entity.HasOne(c => c.Cause)
                .WithMany(p => p.WriteOffs)
                .HasForeignKey(c => c.CauseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.Comment);
            entity.Property(e => e.Date);
            entity.Property(e => e.UserName);
        });
            
        
        //User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasKey(e => e.Id);
            
            //User-Role
            entity.HasOne(r => r.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(r => r.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.Email);
            entity.Property(e => e.FullName);
            entity.Property(e => e.Login);
            entity.Property(e => e.HashedPassword);
            entity.Property(e => e.PinCode);
            entity.Property(e => e.LastLoginDate);
        });
            
        
        //Receipt
        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.ToTable("Receipt");

            entity.HasKey(e => e.Id);
            
            //Receipt-User
            entity.HasOne(u => u.User)
                .WithMany(r => r.Receipts)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            //Receipt-Client
            entity.HasOne(c => c.Client)
                .WithMany(r => r.Receipts)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.Type);
            entity.Property(e => e.ClientName);
            entity.Property(e => e.CloseDate);
            entity.Property(e => e.OpenDate);
            entity.Property(e => e.UserName);
        });
            
        //Dish
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("Dish");

            entity.HasKey(e => e.Id);

            //Dish-Category
            entity.HasOne(c => c.Category)
                .WithMany(p => p.Dishes)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Name);
            entity.Property(e => e.Price);
        });
        
        //Supply
        modelBuilder.Entity<Supply>(entity =>
        {
            entity.ToTable("Supply");

            entity.HasKey(e => e.Id);
            
            //Supply-User
            entity.HasOne(s => s.User)
                .WithMany(p => p.Supplies)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //Supply-Supplier
            entity.HasOne(s => s.Supplier)
                .WithMany(p => p.Supplies)
                .HasForeignKey(s => s.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            entity.Property(e => e.UserName);
            entity.Property(e => e.Date);
            entity.Property(e => e.Sum);
            entity.Property(e => e.Comment);
        });

        modelBuilder.Entity<ProductList>(entity =>
        {
            entity.ToTable("ProductList");

            entity.HasKey(e => e.Id);

            //ProductList-Dish
            entity.HasOne(c => c.Dish)
                .WithMany(p => p.ProductLists)
                .HasForeignKey(c => c.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            //ProductList-SemiProduct
            entity.HasOne(c => c.SemiProduct)
                .WithMany(p => p.ProductLists)
                .HasForeignKey(c => c.SemiProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //ProductList-WriteOff
            entity.HasOne(c => c.WriteOff)
                .WithMany(p => p.ProductLists)
                .HasForeignKey(c => c.WriteOffId)
                .OnDelete(DeleteBehavior.Cascade);
            
            //ProductList-Supply
            entity.HasOne(c => c.Supply)
                .WithMany(p => p.ProductLists)
                .HasForeignKey(c => c.SupplyId)
                .OnDelete(DeleteBehavior.Cascade);

            //ProductList-Receipt
            entity.HasOne(c => c.Receipt)
                .WithMany(p => p.ProductLists)
                .HasForeignKey(c => c.ReceiptId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Name);
            entity.Property(e => e.Count);
            entity.Property(e => e.Price);
            entity.Property(e => e.PrimeCost);
            entity.Property(e => e.Comment);
        });
    }
}