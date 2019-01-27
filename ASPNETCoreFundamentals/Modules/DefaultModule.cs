using ASPNETCoreFundamentals.Core;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Modules
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyDependency>().As<IMyDependency>().InstancePerLifetimeScope();
            builder.RegisterType<Operation>().As<IOperationTransient>();
            builder.RegisterType<Operation>().As<IOperationScoped>().InstancePerLifetimeScope();
            builder.RegisterType<Operation>().As<IOperationSingleton>().SingleInstance();
            builder.Register<Operation>(o => new Operation(Guid.Empty)).As<IOperationSingletonInstance>().SingleInstance();
        }
    }
}
