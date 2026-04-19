using PalmyraHospital.Application.Interfaces.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmyraHospital.Infrastructure.Services.Seed
{
    public class SeedOrchestrator : ISeedOrchestrator
    {
        private readonly IDatabaseMigrator _migrator;
        private readonly IRoleSeeder _roleSeeder;
        private readonly IUserSeeder _userSeeder;

        public SeedOrchestrator(
            IDatabaseMigrator migrator,
            IRoleSeeder roleSeeder,
            IUserSeeder userSeeder)
        {
            _migrator = migrator;
            _roleSeeder = roleSeeder;
            _userSeeder = userSeeder;
        }

        public async Task SeedAsync()
        {
            await _migrator.MigrateAsync();
            await _roleSeeder.SeedAsync();
            await _userSeeder.SeedAsync();
        }
    }
}
