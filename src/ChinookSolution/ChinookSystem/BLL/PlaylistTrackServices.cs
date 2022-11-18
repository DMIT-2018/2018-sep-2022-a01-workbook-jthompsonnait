#region Additional Namespacs
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
#endregion


namespace ChinookSystem.BLL
{
    public class PlaylistTrackServices
    {
        #region Constructor and Context Dependency

        private Chinook2018Context _context;

        internal PlaylistTrackServices(Chinook2018Context context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<PlaylistTrackInfo> PlaylistTrack_FetchPlaylist(string playlistname, string username)
        {
            if (string.IsNullOrWhiteSpace(playlistname))
            {
                throw new ArgumentNullException("No playlist name submitted");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("No user name submitted");
            }
            IEnumerable<PlaylistTrackInfo> results = _context.PlaylistTracks
                .Where(x => x.Playlist.Name.Equals(playlistname)
                            && x.Playlist.UserName.Equals(username))
                .Select(x => new PlaylistTrackInfo
                {
                    TrackId = x.TrackId,
                    TrackNumber = x.TrackNumber,
                    SongName = x.Track.Name,
                    Milliseconds = x.Track.Milliseconds
                })
                .OrderBy(x => x.TrackNumber);
            return results.ToList();
        }
        #endregion

        //  Don -Nov 4(2) Page 5 Transaction Methods
    }
}
