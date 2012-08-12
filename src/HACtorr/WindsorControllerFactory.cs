using System.Web;

namespace HACtorr
{
    using System.Web.Mvc;

    using Castle.MicroKernel;

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(
                    404, 
                    string.Format(
                        "The controller for path '{0}' could not be found",
                        requestContext.HttpContext.Request.Path));
            }

            return (IController)this.kernel.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            this.kernel.ReleaseComponent(controller);
        }
    }
}

