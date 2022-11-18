#region Additional Namespacs
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
#endregion

namespace ChinookSystem.BLL
{
    public class TrackServices
    {
        //  This class needs to be accessed by an "outside user" (WebApp)
        //      therefore the class needs to be public

        #region Constructor and Context Dependency

        private Chinook2018Context _context;

        internal TrackServices(Chinook2018Context context)
        {
            _context = context;
        }
        #endregion

        #region Services

        //  Services are methods

        //  Query to obtain the DbVersion data

        #region Queries
        public List<TrackSelection> Track_FetchTracksBy(string searcharg, string searchby)
        {
            if (string.IsNullOrWhiteSpace(searcharg))
            {
                throw new ArgumentNullException("No search value submitted");
            }
            if (string.IsNullOrWhiteSpace(searchby))
            {
                throw new ArgumentNullException("No search style submitted");
            }
            IEnumerable<TrackSelection> results = _context.Tracks
                .Where(x => (x.Album.Artist.Name.Contains(searcharg) &&
                             searchby.Equals("Artist")) ||
                            (x.Album.Title.Contains(searcharg) &&
                             searchby.Equals("Album")))
                .Select(x => new TrackSelection
                {
                    TrackId = x.TrackId,
                    SongName = x.Name,
                    AlbumTitle = x.Album.Title,
                    ArtistName = x.Album.Artist.Name,
                    Milliseconds = x.Milliseconds,
                    Price = x.UnitPrice
                });
            return results.ToList();
        }


        #endregion


        #endregion
    }
}
