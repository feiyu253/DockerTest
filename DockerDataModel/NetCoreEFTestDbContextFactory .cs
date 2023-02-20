using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerDataModel
{
    public class NetCoreEFTestDbContextFactory : IDesignTimeDbContextFactory<NetCoreEFTestDbContext>
    {
        public NetCoreEFTestDbContext CreateDbContext(string[] args)
        {
            return new NetCoreEFTestDbContext("port=5432;database=NetCoreEFTest;host=localhost;password=123456;user id=postgres", EnumTableNameAndFieldNameCase.ToLower);
        }
    }
}
