using System.Data.Entity;
using JonasDemoApi.Models;

namespace JonasDemoApi.EntityFramework
{
    public class JonasDataContext : DbContext
    {
        public JonasDataContext()
            :base("name=JonasDataConnectionString")
        {
        }

        public DbSet<MergedUnique> MergedUniques { get; set; }


    }
}