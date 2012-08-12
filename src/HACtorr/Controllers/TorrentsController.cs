namespace HACtorr.Controllers
{
    using System.Web.Mvc;

    using HACtorr.Framework.Torrents;
    using HACtorr.ViewModels.Torrents;

    public class TorrentsController : Controller
    {
        private readonly ITorrentService torrentService;

        public TorrentsController(ITorrentService torrentService)
        {
            this.torrentService = torrentService;
        }

        public ActionResult Index()
        {
            return View(new TorrentsViewModel { Torrents = this.torrentService.GetTorrents() });
        }

        public ActionResult AddUrl()
        {
            return View(new AddUrlViewModel());
        }

        [HttpPost]
        public ActionResult AddUrl(AddUrlViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.torrentService.AddTorrent(viewModel.TorrentUrl);

                return this.RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}

