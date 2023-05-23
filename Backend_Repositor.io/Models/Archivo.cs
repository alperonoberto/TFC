namespace Backend_Repositor.io.Models
{
    public class Archivo
    {
        public long Id { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public DateTime? FechaSubida { get; set; }

        public long RepositorioId { get; set; }
        public Repositorio? Repositorio { get; set; }
    }
}
