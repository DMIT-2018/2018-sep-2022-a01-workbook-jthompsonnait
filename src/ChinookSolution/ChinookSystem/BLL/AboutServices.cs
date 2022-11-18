#nullable disable 
#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
#endregion


namespace ChinookSystem.BLL
{
    public class AboutServices
    {
        //  This class needs to be accessed by an "outside user" (WebApp)
        //      therefore the class needs to be public

        #region Constructor and Context Dependency

        private Chinook2018Context _context;

        internal AboutServices(Chinook2018Context context)
        {
            _context = context;
        }
        #endregion

        #region Services

        //  Services are methods

        //  Query to obtain the DbVersion data

        public DbVersionInfo GetDbVersion()
        {
            //  DbVersionInfo is a public "view" of data defined in a class
            //  DbVersionInfo can be a class used BOTH internally and by external users
            //  DbVersion is an internal entity description used ONLY in the library
            DbVersionInfo info = _context.DbVersions
                .Select(x => new DbVersionInfo()
                    {
                        Major = x.Major,
                        Minor = x.Minor,
                        Build = x.Build,
                        ReleaseDate = x.ReleaseDate
                    })
                .SingleOrDefault();
            return info;
        }

        #endregion
    }
}
