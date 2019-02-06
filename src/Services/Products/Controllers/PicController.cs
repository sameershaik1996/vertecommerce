using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Products.Controllers
{
	[Route("/api/pic")]
	public class PicController : Controller
    {
		private readonly IHostingEnvironment _environment;

		public PicController(IHostingEnvironment environment)
		{
			_environment = environment;
		}
		[HttpGet]
		[Route("{id}")]
        public IActionResult GetImage(int id)
        {
			var webRoot = _environment.WebRootPath;
			var path = Path.Combine(webRoot + "/Images/image-"+id+".png");
			var imageBytes = System.IO.File.ReadAllBytes(path);
			return File(imageBytes,"image/png");
        }
    }
}