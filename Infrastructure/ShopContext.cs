using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure;



public class ShopContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ItemCategory> ItemCategories { get; set; }
    public DbSet<OrderParrot> OrderParrots { get; set; }
    public DbSet<Parrot> Parrots { get; set; }
    public DbSet<RefreshToken> RefreshTokens { set; get; }
    public DbSet<RevokedJWT> revokedJWTs { set; get; }


    public ShopContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder mBuild)
    {
        mBuild.Entity<User>(u =>
        {
            u.HasKey(ent => ent.Id);
            u.HasAlternateKey(ent => ent.Username)
            .HasName("AK_Users_Username");
            u.HasIndex(ent => ent.Email)
            .IsUnique()
            .HasDatabaseName("IX_Users_Email");
            u.HasOne(ent => ent.Cart)
            .WithOne(crt => crt.User)
            .HasForeignKey<Cart>("UserId")
            .OnDelete(DeleteBehavior.Cascade);
        });
        mBuild.Entity<Item>(i =>
        {
            i.HasKey(ent => ent.Id);
            i.HasOne(ent => ent.Category)
            .WithMany(cat => cat.Items)
            .OnDelete(DeleteBehavior.Restrict);

            i.HasQueryFilter(ent => !ent.IsDeleted); //Filters all the queries based on whether they're "deleted"
        });
        mBuild.Entity<Cart>(c =>
        {
            c.HasKey(ent => ent.Id);
            c.HasMany(ent => ent.CartItems)
            .WithOne()
            .HasForeignKey(crtItem => crtItem.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        });
        mBuild.Entity<CartItem>(ci =>
        {
            ci.HasKey(ent => ent.Id);
            ci.HasOne(ent => ent.Item)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade); //So I delete the item and the cartitem vanishes.

        });
        mBuild.Entity<Order>(o =>
        {
            o.HasKey(ent => ent.Id);
            o.HasOne(ent => ent.User)
            .WithMany(u => u.Orders)
            .OnDelete(DeleteBehavior.Cascade);
            o.HasMany(ent => ent.OrderItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        });
        mBuild.Entity<OrderItem>(oi =>
        {
            oi.HasKey(ent => ent.Id);
            oi.HasOne(ent => ent.LinkedItem)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull);
        });
        mBuild.Entity<ItemCategory>(ic =>
        {
            ic.HasKey(ent => ent.Id);
            ic.HasMany(ent => ent.Items)
            .WithOne(i => i.Category)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        mBuild.Entity<RevokedJWT>(rjwt =>
        {
            rjwt.HasKey(ent => ent.Token);
            rjwt.HasOne(ent => ent.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        });

        mBuild.Entity<RefreshToken>(
            token =>
            {
                token.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(tk => tk.UserID)
                .OnDelete(DeleteBehavior.Cascade);

                token.HasKey(tk => tk.Token);
            }
        );
    }
}
