using Autofac;
using Autofac.Integration.Mvc;
using BLL;
using DALFactory;
using IDAL;
using IRedisDAL;
using IRepository;
using RedisDAL;
using Repository;
using RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace B2CWebTemplate.App_Start
{
    public static class AuthConfig
    {
        public static void  RegisterAuth()
        {
            var builder = new ContainerBuilder();
            Assembly controllers = Assembly.Load("B2CWebTemplate");

            builder.RegisterControllers(controllers);

            Assembly repository = Assembly.Load("Repository");
            Type[] rtypes = repository.GetTypes();
            builder.RegisterTypes(rtypes).AsImplementedInterfaces();

            Assembly service = Assembly.Load("BLL");
            Type[] stypes = service.GetTypes();
            builder.RegisterTypes(stypes).AsImplementedInterfaces();

            Assembly RepFactory = Assembly.Load("RepositoryFactory");
            Type[] factory = RepFactory.GetTypes();
            builder.RegisterTypes(factory).AsImplementedInterfaces();


            //builder.RegisterType<SessionFactory>().As<ISessionFactory>().InstancePerRequest();
            builder.RegisterType<SessionFactory>().As<ISessionFactory>().InstancePerRequest();
            builder.RegisterType<RepSingleton>().As<IRepSingleton>().InstancePerRequest();
            builder.RegisterType<Redis>().As<IRedis>().InstancePerRequest();
            RepSingleton rep = new RepSingleton();
            SessionFactory session = new SessionFactory();
            Redis redis = new Redis();
            builder.RegisterInstance<RepSingleton>(rep);
            builder.RegisterInstance<SessionFactory>(session);
            builder.RegisterInstance<Redis>(redis);

            builder.RegisterAssemblyTypes(typeof(RepAssemblyFactory).Assembly).Where(t => t.Name.EndsWith("Factory")).AsImplementedInterfaces();


            //builder.RegisterAssemblyTypes(typeof(RepSingleton).Assembly).Where(t => t.Name.EndsWith("RepSingleton")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(typeof(SessionFactory).Assembly).Where(t => t.Name.EndsWith("SessionFactory")).AsImplementedInterfaces();
            ////builder.RegisterAssemblyTypes(typeof(SessionFactory).Assembly).Where(t => t.Name.EndsWith("SessionFactory")).AsImplementedInterfaces();



            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}