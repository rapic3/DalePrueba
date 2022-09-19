using Dale.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dale.Repository.SQLServer
{
    public partial class MSDbContext : DbContext
    {
        #region Colecciones BD
        //Agregar todas las colecciones de la base de datos

        public DbSet<Client> Clients { get; set; }

        public DbSet<ItemOrder> ItemOrders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        #endregion

        public MSDbContext(DbContextOptions<MSDbContext> options)
            : base(options)
        {
        }

        #region Eventos del contexto
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Unique keys
            //builder.Entity<Usuario>()
            //    .HasIndex(u => u.UsuarioNombre)
            //    .IsUnique();

            // Interface that all of our Entity maps implement
            var mappingInterface = typeof(IEntityTypeConfiguration<>);

            // Types that do entity mapping
            var mappingTypes = typeof(MSDbContext).GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));

            // Get the generic Entity method of the ModelBuilder type
            var entityMethod = typeof(ModelBuilder).GetMethods()
                .Single(x => x.Name == "Entity" &&
                        x.IsGenericMethod &&
                        x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var mappingType in mappingTypes)
            {
                // Get the type of entity to be mapped
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();

                // Get the method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(builder, null);

                // Create the mapping type and do the mapping
                var mapper = Activator.CreateInstance(mappingType);
                mapper.GetType().GetMethod("Map").Invoke(mapper, new[] { entityBuilder });
            }
            builder.ApplyConfigurationsFromAssembly(typeof(MSDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        #endregion
    }
}
