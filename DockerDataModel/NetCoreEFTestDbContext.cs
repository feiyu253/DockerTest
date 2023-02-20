using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DockerDataModel
{
    public partial class NetCoreEFTestDbContext:DbContext
    {
        [Obsolete]
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        private string connectionString = null;
        
        private EnumTableNameAndFieldNameCase tableNameAndFieldNameCase= EnumTableNameAndFieldNameCase.None;
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        public NetCoreEFTestDbContext(DbContextOptions<NetCoreEFTestDbContext> options) :base(options) {
            
        }

        public NetCoreEFTestDbContext(string connectionString, EnumTableNameAndFieldNameCase tableNameAndFieldNameCase)
        {
            this.connectionString = connectionString;
            this.tableNameAndFieldNameCase = tableNameAndFieldNameCase;
            
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseNpgsql(this.connectionString);
            options.UseLoggerFactory(LoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (this.tableNameAndFieldNameCase != EnumTableNameAndFieldNameCase.None)
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    modelBuilder.Entity(entityType.Name, builder =>
                    {
                        if (this.tableNameAndFieldNameCase == EnumTableNameAndFieldNameCase.ToLower)
                        {
                            var tableName = entityType.ClrType.Name.ToLower();
                            try
                            {
                                var tableAnn = entityType.GetAnnotation("Relational:TableName");
                                if (tableAnn != null)
                                    tableName = tableAnn.Value.ToString().ToLower();
                            }
                            catch { }
                            builder.ToTable(tableName);
                            foreach (var property in entityType.GetProperties())
                            {
                                var propertyName = property.Name.ToLower();
                                try
                                {
                                    var ColumnAnn = property.GetAnnotation("Relational:ColumnName");
                                    if (ColumnAnn != null)
                                        propertyName = ColumnAnn.Value.ToString().ToLower();
                                }
                                catch{ }
                                builder.Property(property.Name).HasColumnName(propertyName);
                            }
                        }
                        else
                        {
                            builder.ToTable(entityType.ClrType.Name.ToUpper());
                            foreach (var property in entityType.GetProperties())
                            {
                                builder.Property(property.Name).HasColumnName(property.Name.ToUpper());
                            }
                        }
                    });
                }
            }
        }
    }
}
