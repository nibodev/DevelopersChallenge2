using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.DataContext
{
    public class ReconcileContext : DbContext
    {
        public ReconcileContext(DbContextOptions<ReconcileContext> options) : base(options) { }

        public DbSet<ImportedFile> ImportedFiles { get; set; }
    }
}