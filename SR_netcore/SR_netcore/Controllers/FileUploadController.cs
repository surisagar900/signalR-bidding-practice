using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment web;

        public FileUploadController(IWebHostEnvironment web)
        {
            this.web = web;
        }

        [HttpPost]
        //[ProducesResponseType(404)]
        public async Task<IActionResult> FileUploader(List<IFormFile> files)
        {
            try
            {
                if(files == null || files.Count == 0)
                    return Content("file not selected");
                long size = files.Sum(f => f.Length);
                var filePaths = new List<string>();
                foreach(var formFile in files)
                {
                    if(formFile.Length > 0)
                    {
                        // full path to file in temp location
                        var filePath = Path.Combine(web.WebRootPath, "userimages");

                        filePaths.Add(filePath);

                        var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName + "Temps");

                        using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
                // process uploaded files
                // Don't rely on or trust the FileName property without validation.
                return Ok(new { count = files.Count, size, filePaths });

            }
            catch(Exception)
            {
                //return BadRequest("not uploaded");
                throw;
            }
        }

    }
}

