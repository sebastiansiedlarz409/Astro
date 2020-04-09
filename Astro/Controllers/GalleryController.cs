using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astro.Controllers
{
    public class GalleryController : Controller
    {
        private readonly JSONParse _JSONParse;
        private readonly NASAApi _NASAApi;

        public GalleryController(JSONParse JSONParse, NASAApi NASAApi)
        {
            _JSONParse = JSONParse;
            _NASAApi = NASAApi;
        }

        public IActionResult Index()
        {
            List<Gallery> galleryImages = new List<Gallery>();

            return View(galleryImages);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            IEnumerable<Gallery> galleryImages = _JSONParse.GetGalleryImages(await _NASAApi.GetGalleryJson(search));

            return View("Index", galleryImages.ToList());
        }
    }
}