namespace HACtorr.Framework.Torrents
{
    using MonoTorrent.Client;

    public class TorrentInfo
    {
        public string Name
        {
            get;
            set;
        }

        public TorrentManager TorrentManager
        {
            get;
            set;
        }
    }
}

