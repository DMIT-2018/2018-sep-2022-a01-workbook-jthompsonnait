using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        #region Private variables and DI Constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly AboutServices _aboutServices;

        public IndexModel(ILogger<IndexModel> logger,
                            AboutServices aboutServices)
        {
            _logger = logger;
            _aboutServices = aboutServices;
        }
        #endregion

        #region Feedback and ErrorHandling
        [TempData]
        public string FeedBack { get; set; }

        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBack);
        #endregion

        public void OnGet()
        {
            //  Consume a service
            DbVersionInfo info = _aboutServices.GetDbVersion();
            if (info == null)
            {
                FeedBack = "Version unknown";
            }
            else
            {
                FeedBack = $"Version: {info.Major}.{info.Minor}.{info.Build}" +
                           $" Release date of {info.ReleaseDate.ToShortDateString()}";
            }

        }
    }
}