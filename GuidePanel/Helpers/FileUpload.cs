using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace GuidePanel.Helpers
{
    public class FileUpload : IFileUpload
    {
        private string[] FileExtensions = { ".png", ".jpg", ".jpeg", ".ico" };
        private string[] VideoExtensions = { ".mp4" };
        private string[] DocumentExtensions = { ".pdf", ".txt", ".docx", ".doc" };
        private readonly IWebHostEnvironment _webHost;

        public FileUpload(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public string DeleteImage(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "Resim Bulunamadı";
            }
            string deleted = Path.Combine(_webHost.WebRootPath, "images\\*", Name);
            if (File.Exists(deleted))
            {
                File.Delete(deleted);
            }
            return "Resim Silindi";
        }

        public string DeleteImages(List<string> images)
        {
            if (images.Count > 0)
            {
                foreach (var item in images)
                {
                    var deleted = DeleteImage(item);
                }
            }
            return "Resimler Silindi";
        }

        public List<string> FileListUpload(List<IFormFile> files)
        {
            var imageUrlList = new List<string>();
            List<string> Failed = new List<string> { "Failed" };
            foreach (var file in files)
            {
                string UrlPath = "";
                string FileName = "";
                try
                {
                    ImageSave(file, ref UrlPath, ref FileName);
                    imageUrlList.Add(UrlPath);
                }
                catch (Exception)
                {

                    return Failed;
                }
            }
            return imageUrlList;
        }

        public string UpdateFile(IFormFile file, string Name)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSave(file, ref urlPath, ref fileName);

            }
            catch (Exception)
            {

                return "Failed";
            }

            string _imageToBeDeleted = Path.Combine(_webHost.WebRootPath, "images/", Name);
            if ((File.Exists(_imageToBeDeleted)))
            {
                File.Delete(_imageToBeDeleted);
            }

            return $"{urlPath}";
        }
        private void ImageSave(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Resource.Url);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = ReplacedFileName(file.FileName);
                var path = Path.Combine(Resource.Url, $"{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }

        private static string ReplacedFileName(string fileName)
        {
            fileName = fileName
            .Replace("ç", "c")
            .Replace("ğ", "g")
            .Replace("ı", "i")
            .Replace("ö", "o")
            .Replace("ş", "s")
            .Replace("ü", "u")
            .Replace("Ç", "C")
            .Replace("Ğ", "G")
            .Replace("İ", "I")
            .Replace("Ö", "O")
            .Replace("Ş", "S")
            .Replace("Ü", "U")
            .Replace(" ", "-");

            string regexedFileName = Regex.Replace(fileName, @"[^\w\d\-\.]", "");

            return regexedFileName;
        }

        public string FileUploads(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSave(file, ref urlPath, ref fileName);
            }
            catch (Exception ex)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }



        public string VideoUploads(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                VideoSave(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }

        private void VideoSave(IFormFile file, ref string UrlPath, ref string FileName)
        {

            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && VideoExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //FileName = file.FileName;
                FileName = ReplacedFileName(file.FileName);
                var path = Path.Combine(_webHost.WebRootPath, $"images/", $"{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }

        public string DocumentUploads(IFormFile files)
        {

            string Failed = new string("Failed");

            string UrlPath = "";
            string FileName = "";
            try
            {
                DocumentSave(files, ref UrlPath, ref FileName);

            }
            catch (Exception)
            {

                return Failed;
            }

            return $"{UrlPath}";


        }
        private void DocumentSave(IFormFile file, ref string UrlPath, ref string FileName)
        {

            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && DocumentExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Panel\\AdddocPanel/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //FileName = file.FileName;
                FileName = ReplacedFileName(file.FileName);
                var path = Path.Combine(_webHost.WebRootPath, $"Panel\\AdddocPanel//\\", $"{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
    }
}
