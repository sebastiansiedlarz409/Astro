using System.Collections.Generic;
using Astro.BLL.JSONParsers;
using Astro.DAL.APICLIENT;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astro.Controllers
{
    public class GalleryController : Controller
    {
        private JSONParse _JSONParse;
        private NASAApi _NASAAPpi;

        public GalleryController(JSONParse JSONParse, NASAApi NASAApi)
        {
            _JSONParse = JSONParse;
            _NASAAPpi = NASAApi;
        }

        public IActionResult Index()
        {
            List<Gallery> galleryImages = new List<Gallery>();

            return View(galleryImages);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            List<Gallery> galleryImages = _JSONParse.GetGalleryImages(_NASAAPpi.GetGalleryJson(search));

            return View("Index", galleryImages);
        }
    }
}