using System.IO;

/* TextPersistor

Implementación específica de IPersistor guardando archivos como texto

 */

namespace Proyect
{
    public class TxtPersistor : IPersistor
    {
        private string path;

        public TxtPersistor(string path)
        //Crea un TxtPersistor y asigna un path
        {
            this.path = path;
        }

        public string[] Read()
        //Lee de un documento
        {
            return File.ReadAllLines(this.path);
        }

        public void Write(string DataToPersist)
        //Escribe en un documento
        {
            File.WriteAllText(this.path, DataToPersist);
        }
    }
}
