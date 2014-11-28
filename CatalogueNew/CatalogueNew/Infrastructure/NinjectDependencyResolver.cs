using log4net;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Parameters;
using Ninject.Extensions.NamedScope;
using CatalogueNew.Web.Controllers;

namespace CatalogueNew.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ILog>().ToMethod(x => LogManager.GetLogger("CatalogueLogger"));
            kernel.Bind<ICatalogueContext>().To<CatalogueContext>()
                .InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IManufacturerService>().To<ManufacturerService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>()
                .InRequestScope()
                .WithConstructorArgument("context", kernel.Get<ICatalogueContext>());
            kernel.Bind<UserManager<User>>().ToSelf().InRequestScope(); 
            //kernel.Bind(typeof(IPaginationService<>)).To(typeof(PaginationService<>));

        }
    }
}