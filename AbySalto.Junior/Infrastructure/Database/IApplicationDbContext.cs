using AbySalto.Junior.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Infrastructure.Database
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<OrderStatus> OrderStatuses { get; }
        DbSet<PaymentType> PaymentTypes { get; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}
