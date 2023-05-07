using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Context
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            _builder.Entity<Vehicle>();
            _builder.Entity<Time>();
        }
    }
}