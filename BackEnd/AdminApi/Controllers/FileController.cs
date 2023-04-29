using BUS.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.ViewModels.Plugins.File;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;
        public FileController(IFileService fileService)
        {
            this._fileService = fileService;
        }

        [Route("Upload")]
        [HttpPost]
        public string SaveImage([FromForm] FormFileCreateRequest form)
        {
            return _fileService.SaveImage(form);
        }
    }
}
