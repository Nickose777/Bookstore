using Bookstore.Data;
using Bookstore.Data.Contracts;
using Ninject.Modules;

namespace Bookstore.Services.NinjectModules
{
    public class UnitOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
