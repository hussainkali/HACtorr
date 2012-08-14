namespace HACtorr.Framework.Torrents
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    using MonoTorrent.Client;
    using MonoTorrent.Common;

    public class TorrentService : ITorrentService
    {
        private readonly ClientEngine engine = new ClientEngine(new EngineSettings());
        private readonly string downloadsDirectory;

        public TorrentService()
        {
            var userDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            this.downloadsDirectory = Path.Combine(userDirectory, "Downloads");
        }

        public IEnumerable<TorrentInfo> GetTorrents()
        {
            return engine.Torrents.Select(t => new TorrentInfo { Name = t.Torrent.Name, TorrentManager = t });
        }

        public void AddTorrent(string torrentUrl)
        {
            // TODO: This temp file stuff is a bit dodgy, but we can leave it until we start to sort out making the settings configurable
            var tempFile = Path.GetTempFileName();

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(torrentUrl, tempFile);
            }

            Torrent torrent = Torrent.Load(tempFile);
            TorrentManager manager = new TorrentManager(torrent, downloadsDirectory, new TorrentSettings());
            engine.Register(manager);

            manager.Start();
        }

        public void AddTorrents(IEnumerable<IFileContainer> files)
        {
            foreach (var file in files)
            {
                var torrent = Torrent.Load(file.Content);
                var manager = new TorrentManager(torrent, downloadsDirectory, new TorrentSettings());
                this.engine.Register(manager);

                manager.Start();
            }
        }
    }
}

