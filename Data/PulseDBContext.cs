using Microsoft.EntityFrameworkCore;
using ETR.Infra.EagleEye.API.Entity;

namespace ETR.Infra.EagleEye.API.Data
{
    public class PulseDBContext : DbContext
    {

        public PulseDBContext(DbContextOptions<PulseDBContext> options) : base(options) 
        { 
        
        }

        public DbSet<PulseLog> PulseLogs { get; set; }  

    }
}
