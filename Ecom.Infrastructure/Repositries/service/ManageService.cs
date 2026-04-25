using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositries.service
{
    public class ManageService : IManageService
    {
        private readonly IFileProvider _fileProvider;
        public ManageService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var savedImagePaths = new List<string>();
            var imageDirectory = Path.Combine("wwwroot", "Images", src);
            if (Directory.Exists(imageDirectory) == false)
            {
                Directory.CreateDirectory(imageDirectory);
            }
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var imageName = file.FileName;
                    var imageSrc = $"Images/{src}/{imageName}";
                    var root = Path.Combine(imageDirectory, imageName);
                    using(FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    savedImagePaths.Add(imageSrc);
                }
            }
            return savedImagePaths;
        }



        public void DeleteImageAsync(string src)
        {
            var info= _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
