﻿#nullable disable
namespace ChinookSystem.ViewModels
{
    public class TrackSelection
    {
        public int TrackId { get; set; }
        public string SongName { get; set; }
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public int Milliseconds { get; set; }
        public decimal Price { get; set; }
    }
}
