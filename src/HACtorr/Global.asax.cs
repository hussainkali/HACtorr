/*
 * Created by SharpDevelop.
 * User: Hussain
 * Date: 07/08/2012
 * Time: 22:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace HACtorr
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.MicroKernel;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.Ignore("{resource}.axd/{*pathInfo}");
			
			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {
					controller = "Home",
					action = "Index",
					id = UrlParameter.Optional
				});
		}
		
		protected void Application_Start()
		{
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(GetWindsorKernel()));
			RegisterRoutes(RouteTable.Routes);
		}

        private IKernel GetWindsorKernel()
        {
            WindsorContainer container = new WindsorContainer();

            container.Install(FromAssembly.InThisApplication());

            return container.Kernel;
        }
	}
}
