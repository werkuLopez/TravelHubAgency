using Microsoft.AspNetCore.Hosting.Server;

namespace TravelHubAgency.Helpers
{
    public enum Foldders { Images = 0, Mails = 1 }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnv;
        private IServer server;

        public HelperPathProvider(IWebHostEnvironment hostEnv, IServer server)
        {
            this.hostEnv = hostEnv;
            this.server = server;
        }

        private string GetFolderPath(Foldders folder)
        {
            string carpeta = "";

            if (folder == Foldders.Images)
            {
                carpeta = "img";
            }
            else if (folder == Foldders.Mails)
            {
                carpeta = "mails";
            }

            return carpeta;
        }

        // recuperar la ruta absoluta de la imagen
        public string MapPath(string fileName, Foldders foldder)
        {
            string carpeta = this.GetFolderPath(foldder);
            string rootpath = this.hostEnv.WebRootPath;
            string path = Path.Combine(rootpath, carpeta, fileName);

            return path;
        }
    }
}
