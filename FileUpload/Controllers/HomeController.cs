using FileUpload.Interfaces;
using FileUpload.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.Xml;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly IProductService _productService;
        readonly IBufferedFileUploadService _bufferedFileUploadService;
        readonly IStreamFileUploadService _streamFileUploadService;
        static readonly Dictionary<string, List<byte[]>> _fileSignature =
            new Dictionary<string, List<byte[]>>
            {
                { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
                { ".jpeg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    }
                },
                { ".jpg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    }
                },
            };


        public HomeController(ILogger<HomeController> logger,
            IProductService productService,
            IBufferedFileUploadService bufferedFileUploadService,
            IStreamFileUploadService streamFileUploadService)
        {
            _logger = logger;
            _productService = productService;
            _bufferedFileUploadService = bufferedFileUploadService;
            _streamFileUploadService = streamFileUploadService;
        }

        public async Task<IActionResult> BufferedFileUpload(int id)
        {
            if (id > 0)
            {
                // TODO: get product info by id
                var product = await _productService.Get(id);
                ViewBag.ProductThumb = await _bufferedFileUploadService.ReadImageToBase64String(product.Thumb);
                return View(product);
            }
            else return View();
        }

        [HttpPost]
        public async Task<ActionResult> BufferedFileUpload(Product product, IFormFile fileThumb, IFormFileCollection fileImages)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    #region - Upload Images -

                    if (fileThumb == null || fileThumb.Length == 0)
                    {
                        ViewBag.Message = "Thumb image is empty";
                        return View(product);
                    }

                    //File extension valivation
                    string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };
                    var ext = Path.GetExtension(fileThumb.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                    {
                        ViewBag.Message = "File extension is invalid";
                        return View(product);
                    }

                    // File signature validation
                    using (var reader = new BinaryReader(fileThumb.OpenReadStream()))
                    {
                        var signatures = _fileSignature[ext];
                        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
                        if (!signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature)))
                        {
                            ViewBag.Message = "File is not permitted";
                            return View(product);
                        }
                    }

                    // File size validation
                    if (fileThumb.Length > 2097152)
                    {
                        ViewBag.Message = "Files with the size of +2MB are not allowed";
                        return View(product);
                    }

                    var uploadResult = await _bufferedFileUploadService.UploadFile(fileThumb);
                    if(uploadResult.Result)
                    {
                        product.Thumb = uploadResult.SavedFileName;
                    }
                    else
                    {
                        ViewBag.Message = "File upload failed";
                        return View(product);
                    }

                    #endregion

                    var p = await _productService.Add(product);

                    ViewBag.Message = "User saved successfully";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Upload Failed");
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }

        public IActionResult StreamFileUpload()
        {
            return View();
        }


        [ActionName("StreamFileUpload")]
        [HttpPost]
        public async Task<ActionResult> SaveFileToPhysicalFolder()
        {
            try
            {
                var boundary = HeaderUtilities.RemoveQuotes(
                    MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;

                var reader = new MultipartReader(boundary, Request.Body);
                var section = await reader.ReadNextSectionAsync();
                string response = string.Empty;

                try
                {
                    if (await _streamFileUploadService.UploadFile(reader, section))
                    {
                        ViewBag.Message = "File Upload Successful";
                    }
                    else
                    {
                        ViewBag.Message = "File Upload Failed";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "File Upload Failed");
                    ViewBag.Message = "File Upload Failed";
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Upload Failed");
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}