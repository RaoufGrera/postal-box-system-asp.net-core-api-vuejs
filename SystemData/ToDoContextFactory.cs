using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemData
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<SystemContext>
    {
        public SystemContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SystemContext>();
            builder.UseSqlServer("Server=.; Database=BoxSystem10; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new SystemContext(builder.Options);
        }
    }
}
