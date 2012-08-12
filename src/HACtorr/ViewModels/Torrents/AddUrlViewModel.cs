namespace HACtorr.ViewModels.Torrents
{
    using System.ComponentModel.DataAnnotations;

    public class AddUrlViewModel
    {
        [Required]
        [DataType(DataType.Url)]
        public string TorrentUrl
        {
            get;
            set;
        }
    }
}

