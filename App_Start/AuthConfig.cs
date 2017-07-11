using Autofac;
using Autofac.Integration.Mvc;
using BLL;
using DAL;
using DALFactory;
using IDAL;
using IRedisDAL;
using IRepository;
using RedisDAL;
using Repository;
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
            Assembly Factory = Assembly.Load("DALFactory");
            Type[] ftypes = Factory.GetTypes();
            builder.RegisterTypes(ftypes).AsImplementedInterfaces();
            Assembly dal = Assembly.Load("DAL");
            Type[] dtypes = dal.GetTypes();
            
            builder.RegisterTypes(dtypes).AsImplementedInterfaces();




            //builder.RegisterType<SessionFactory>().As<ISessionFactory>().InstancePerRequest();
            //builder.RegisterType<SessionFactory>().As<ISessionFactory>().InstancePerHttpRequest();
            //builder.RegisterType<Redis>().As<IRedis>().InstancePerHttpRequest();
            builder.RegisterType<SessionFactory>().As<ISessionFactory>().InstancePerDependency();
            builder.RegisterType<Redis>().As<IRedis>().InstancePerDependency();
            builder.RegisterType<DapperDal>().As<IDapperDal>().InstancePerDependency();


            builder.RegisterAssemblyTypes(typeof(BaseDataRepository).Assembly).Where(t => t.Name.EndsWith("DataRepository")).AsImplementedInterfaces();


            //builder.RegisterAssemblyTypes(typeof(RepSingleton).Assembly).Where(t => t.Name.EndsWith("RepSingleton")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(typeof(SessionFactory).Assembly).Where(t => t.Name.EndsWith("SessionFactory")).AsImplementedInterfaces();
            ////builder.RegisterAssemblyTypes(typeof(SessionFactory).Assembly).Where(t => t.Name.EndsWith("SessionFactory")).AsImplementedInterfaces();



            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}