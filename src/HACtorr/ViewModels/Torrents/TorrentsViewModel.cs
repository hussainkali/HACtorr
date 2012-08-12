namespace HACtorr.ViewModels.Torrents
{
    using System.Collections.Generic;

    using HACtorr.Framework.Torrents;

    public class TorrentsViewModel
    {
        public IEnumerable<TorrentInfo> Torrents { get; set; }
    }
}

