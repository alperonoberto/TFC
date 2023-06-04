using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Repositor.io.Interfaces
{
    public interface IFileService
    {
        void UploadFiles(List<IFormFile> files, string subDirectory);
        (byte[] archiveData, string fileType, string archiveName) DownloadFiles(string[] filepaths);
        string SizeConverter(long bytes);
    }
}
