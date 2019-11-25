using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Infrastructure
{
    public class CatalogContext
        : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
    }
}