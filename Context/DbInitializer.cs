using Microsoft.EntityFrameworkCore;
using app.Models;

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