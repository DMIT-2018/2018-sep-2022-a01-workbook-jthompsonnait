using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class DbVersionInfo
    {
        //  The view that is used by the "Outside World"
        //  Access must match the method where the class is used (typically public)
        //  Purpose:    Use to simply carry data
        //              Create data fields as auto-implemented properties
        //              Consist of the "raw" data of the query 
        //              No validation done here
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
