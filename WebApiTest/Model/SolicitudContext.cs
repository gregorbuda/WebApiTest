using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Model
{
    public class SolicitudContext : DbContext
    {
        public SolicitudContext(DbContextOptions<SolicitudContext> options) : base(options)
        {
        }

        public DbSet<Solicitud> solicitud { get; set; }
    }
}
