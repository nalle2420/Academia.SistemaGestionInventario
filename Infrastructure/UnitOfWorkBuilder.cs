using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure
{
    public class UnitOfWorkBuilder
    {
        readonly IServiceProvider _serviceProvider;

        public UnitOfWorkBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public IUnitOfWork BuilderSistemaGestionInventario()
        {
            DbContext dbContext = _serviceProvider.GetService<GestionInventarioDbContext>() ?? throw new NullReferenceException();
            return new UnitOfWork(dbContext);
        }
    }
}
