using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Remora.EntityFrameworkCore.Modular;
using Remora.EntityFrameworkCore.Modular.Services;

namespace EFCore.Repro.Library
{
    public class ContextConfigurationService
    {
        private readonly SchemaAwareDbContextService _schemaAwareDbContextService;
        private readonly Dictionary<Type, string> _knownSchemas;

        public ContextConfigurationService
        (
            SchemaAwareDbContextService schemaAwareDbContextService
        )
        {
            _schemaAwareDbContextService = schemaAwareDbContextService;
            _knownSchemas = new Dictionary<Type, string>();
        }

        private void EnsureSchemaIsCached<TContext>() where TContext : SchemaAwareDbContext
        {
            if (_knownSchemas.ContainsKey(typeof(TContext)))
            {
                return;
            }

            var dummyOptions = new DbContextOptionsBuilder<TContext>().Options;
            var dummyContext = Activator.CreateInstance(typeof(TContext), dummyOptions) as TContext;
            if (dummyContext is null)
            {
                return;
            }

            var schema = dummyContext.Schema;
            _knownSchemas.Add(typeof(TContext), schema);
        }

        public void ConfigureSchemaAwareContext<TContext>(DbContextOptionsBuilder optionsBuilder)
            where TContext : SchemaAwareDbContext
        {
            EnsureSchemaIsCached<TContext>();
            var schema = _knownSchemas[typeof(TContext)];

            optionsBuilder
                .UseSqlite
                (
                    "DataSource=:memory:",
                    b => b.MigrationsHistoryTable(HistoryRepository.DefaultTableName + schema)
                );

            _schemaAwareDbContextService.ConfigureSchemaAwareContext(optionsBuilder);
        }
    }
}
