using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ViewModels.Plugins.File;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace BUS.System
{
    public interface IFileService
    {
        string GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        string SaveFile(FormFileCreateRequest file);

        Task DeleteFileAsync(string fileName);

        string SaveImage(FormFileCreateRequest form);
    }

    public class FileService : IFileService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "Upload";

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string folder, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder + "/" + folder + "/", fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public string SaveFile(FormFileCreateRequest form)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(form.File.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            SaveFileAsync(form.File.OpenReadStream(), form.Folder, fileName);

            return "/" + USER_CONTENT_FOLDER_NAME + "/" + form.Folder + "/" + fileName;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string SaveImageAsync(Stream mediaBinaryStream, string folder, string fileName)
        {
            var folderPath = string.IsNullOrWhiteSpace(folder) ? _userContentFolder + "/Image/" : _userContentFolder + "/Image/" + folder + "/";
            var filePath = Path.Combine(folderPath, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            mediaBinaryStream.CopyToAsync(output);

            return "/" + USER_CONTENT_FOLDER_NAME + "/Image/" + (string.IsNullOrWhiteSpace(folder) ? "" : (folder + "/")) + fileName;
        }

        public string SaveImage(FormFileCreateRequest form)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(form.File.ContentDisposition).FileName.Trim('"');
            var fileName = Path.GetFileName(originalFileName);
            var savedFilePath = SaveImageAsync(form.File.OpenReadStream(), form.Folder, fileName);
            return savedFilePath;
        }
    }
}
