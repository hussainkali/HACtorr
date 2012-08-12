namespace HACtorr.Framework.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using HACtorr.Framework.Torrents;

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITorrentService>().ImplementedBy<TorrentService>());
        }
    }
}

