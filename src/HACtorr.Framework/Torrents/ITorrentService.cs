namespace HACtorr.Framework.Torrents
{
    using System.Collections.Generic;

    public interface ITorrentService
    {
        IEnumerable<TorrentInfo> GetTorrents();
        void AddTorrent(string torrentUrl);
        void AddTorrents(IEnumerable<IFileContainer> streams);
    }
}

