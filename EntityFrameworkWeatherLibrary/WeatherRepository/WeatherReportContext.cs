using WeatherDomainLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace WeatherDomainLibrary.WeatherRepository
{
    class WeatherReportContext: DbContext
    {
        public DbSet<WeatherReport> WeatherReports { get; set; }

        public static WeatherReportContext Create()
        {
            return new WeatherReportContext();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // User "admin" with table creation rights is needed in server, Windows auto connection had problems in service
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WeatherDB;Trusted_Connection=False;user=admin;password=admin");
        }
    }
}
