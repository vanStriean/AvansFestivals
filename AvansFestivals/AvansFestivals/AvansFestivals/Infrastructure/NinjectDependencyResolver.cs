using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using AvansFestivals.Models;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Concrete;

namespace AvansFestivals.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
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
            kernel.Bind<IFestivalRepo>().To<FestivalRepo>();
            kernel.Bind<IUserRepo>().To<UserRepo>();
            kernel.Bind<ITicketRepo>().To<TicketRepo>();
            kernel.Bind<IOrderRepo>().To<OrderRepo>();
        }
    }
}