using Bookstore.Services.Contracts;
using Bookstore.Services.Providers;
using Ninject.Modules;

namespace Bookstore.Services.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IBookService>().To<BookService>().InTransientScope();
        }
    }
}
