using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Repositor.io.Interfaces
{
    public interface IFileService
    {
        void UploadFiles(List<IFormFile> files, string subDirectory);
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);
        string SizeConverter(long bytes);
    }
}
