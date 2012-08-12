/*
 * Created by SharpDevelop.
 * User: Hussain
 * Date: 07/08/2012
 * Time: 22:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace HACtorr.Controllers
{
    using System.Web.Mvc;

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            return View();
		}
		
		public ActionResult Contact()
		{
			return View();
		}
	}
}
