#nullable disable
using API.Model.Entities;
using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class PosDbContext : DbContext
{
    public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionItem> TransactionItems { get; set; }
    public DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Set Unique
        modelBuilder.Entity<Employee>().HasIndex(emp => new
        {
            emp.UserName
        }).IsUnique();

        //Set Relationship and Cardinality

        //Role to Employee (One to Many)
        modelBuilder.Entity<Role>()
            .HasMany(role => role.Employees)
            .WithOne(employee => employee.Role)
            .HasForeignKey(employee => employee.RoleGuid)
            .OnDelete(DeleteBehavior.SetNull);

        //Product to Prices (One to Many)
        modelBuilder.Entity<Product>()
            .HasMany(product => product.Prices)
            .WithOne(prices => prices.Product)
            .HasForeignKey(prices => prices.ProductGuid)
            .OnDelete(DeleteBehavior.SetNull);

        //Product to TransactionItem (One to Many)
        modelBuilder.Entity<Product>()
            .HasMany(product => product.TransactionItems)
            .WithOne(transactions_items => transactions_items.Product)
            .HasForeignKey(transactions_item => transactions_item.ProductGuid)
            .OnDelete(DeleteBehavior.SetNull);

        //Unit to Prices (One to Many)
        modelBuilder.Entity<Unit>()
            .HasMany(unit => unit.Prices)
            .WithOne(prices => prices.Unit)
            .HasForeignKey(prices => prices.UnitGuid)
            .OnDelete(DeleteBehavior.SetNull);

        //TransactionItem to Transaction (Many to One)
        modelBuilder.Entity<TransactionItem>()
            .HasOne(transaction_item => transaction_item.Transaction)
            .WithMany(transaction => transaction.TransactionItems)
            .HasForeignKey(transactions_item => transactions_item.TransactionGuid)
            .OnDelete(DeleteBehavior.SetNull);

        //TransactionsItem to Product (Many to One)
        modelBuilder.Entity<TransactionItem>()
            .HasOne(transaction_item => transaction_item.Product)
            .WithMany(product => product.TransactionItems)
            .HasForeignKey(transaction_item => transaction_item.ProductGuid)
            .OnDelete(DeleteBehavior.SetNull);

        //TransactionItem to Price (Many to One)
        modelBuilder.Entity<TransactionItem>()
            .HasOne(transaction_item=> transaction_item.Price)
            .WithMany(price => price.TransactionItems)
            .HasForeignKey(price => price.TransactionGuid)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
