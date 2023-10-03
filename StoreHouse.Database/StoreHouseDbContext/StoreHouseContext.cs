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
    public virtual DbSet<DishesCategory> DishesCategories { get; set; }
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
        modelBuilder.Entity<Ingredient>()
            .HasOne(c => c.Category)
            .WithMany(i => i.Ingredients)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        //Product-Category
        modelBuilder.Entity<Product>()
            .HasOne(c => c.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        //WriteOff-Cause
        modelBuilder.Entity<WriteOff>()
            .HasOne(c => c.Cause)
            .WithMany(p => p.WriteOffs)
            .HasForeignKey(c => c.CauseId);
        //WriteOff-User
        modelBuilder.Entity<WriteOff>()
            .HasOne(u => u.User)
            .WithMany(w => w.WriteOffs)
            .HasForeignKey(u => u.UserId);
        //User-Role
        modelBuilder.Entity<User>()
            .HasOne(r => r.Role)
            .WithMany(u => u.Users)
            .HasForeignKey(r => r.RoleId);
        //Receipt-Client
        modelBuilder.Entity<Receipt>()
            .HasOne(c => c.Client)
            .WithMany(r => r.Receipts)
            .HasForeignKey(c => c.ClientId);
        //Receipt-User
        modelBuilder.Entity<Receipt>()
            .HasOne(u => u.User)
            .WithMany(r => r.Receipts)
            .HasForeignKey(u => u.UserId);
        //Dish-Category
        modelBuilder.Entity<Dish>()
            .HasOne(c => c.Category)
            .WithMany(p => p.Dishes)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        //Supply-Supplier
        modelBuilder.Entity<Supply>()
            .HasOne(s => s.Supplier)
            .WithMany(p => p.Supplies)
            .HasForeignKey(s => s.SupplierId);
        //Supply-User
        modelBuilder.Entity<Supply>()
            .HasOne(s => s.User)
            .WithMany(p => p.Supplies)
            .HasForeignKey(s => s.UserId);

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
            entity.Property(e => e.Sum);
            entity.Property(e => e.PrimeCost);
            entity.Property(e => e.Comment);
        });
    }
}