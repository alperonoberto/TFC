using Backend_Repositor.io.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;


namespace Backend_Repositor.io.Services
{
    public class FileService : IFileService
    {
        #region Property  
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Constructor  
        public FileService(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Upload File  
        public void UploadFiles(List<IFormFile> files, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;
            var directory = Path.Combine(_hostingEnvironment.WebRootPath, subDirectory);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            //files.ForEach(async file =>
            //{
            //    if (file.Length <= 0) return;
            //    var filePath = Path.Combine(directory, file.FileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        try
            //        {
            //            await file.CopyToAsync(stream);
            //        }
            //        finally
            //        {
            //            stream.Close();
            //            stream.Dispose();
            //        }
            //    }
            //});

            foreach (var file in files)
            {
                if (file.Length <= 0) continue;

                var filePath = Path.Combine(directory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

        }
        #endregion

        #region Download File  
        public (byte[] archiveData, string fileType, string archiveName) DownloadFiles(string[] filepaths)
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach(var path in filepaths)
                    {
                        var filename = Path.GetFileName(path);
                        var theFile = archive.CreateEntry(filename, CompressionLevel.Optimal);

                        using (var entryStream = theFile.Open())
                        using (var fileStream = File.OpenRead(path))
                        {
                            fileStream.CopyTo(entryStream);
                        }

                    }
                }

                return (memoryStream.ToArray(), "application/zip", zipName);
            }

        }
        #endregion

        #region Size Converter  
        public string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }
        #endregion
    }
}
