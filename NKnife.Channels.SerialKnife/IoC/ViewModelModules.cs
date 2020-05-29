using Autofac;
using NKnife.Channels.SerialKnife.Views;

namespace NKnife.Channels.SerialKnife.IoC
{
    public class ViewModelModules : Module
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<WorkbenchViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<SerialPortViewModel>().AsSelf();
        }

        #endregion
    }
}
