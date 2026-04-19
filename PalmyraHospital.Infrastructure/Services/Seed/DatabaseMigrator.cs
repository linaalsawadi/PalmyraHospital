using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Application.Interfaces.Seed;
using PalmyraHospital.Infrastructure.Data;

namespace PalmyraHospital.Infrastructure.Services.Seed
{
    public class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly ApplicationDbContext _context;

        public DatabaseMigrator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
