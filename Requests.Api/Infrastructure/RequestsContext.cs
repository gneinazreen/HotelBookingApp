using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Domain;

namespace Requests.Api.Infrastructure
{
    public class RequestsContext : DbContext
    {
        public RequestsContext(DbContextOptions<RequestsContext> options)
            : base(options) { }

        // Tables
        public DbSet<SpecialRequest> Requests => Set<SpecialRequest>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // SpecialRequest
            b.Entity<SpecialRequest>().HasKey(x => x.RequestId);

            b.Entity<SpecialRequest>()
                .Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            b.Entity<SpecialRequest>()
                .Property(x => x.Category)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
