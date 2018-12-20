using System.Collections.Generic;
using System.IO;
using System.Linq;

/* StagesTexloader

Responsabilidad:
	Crea y devuelve un diccionario a partir de un archivo de texto.

SOLID:
	Cumple con SRP al tener una sola razon de cambio. 

No utilizamos el diseño Formatter Persistor dado que lo único para lo que
se utiliza la información que interactúa con esta clase es para cargarla
en la view, el modelo no trabaja con ella ni tampoco se la utiliza para
crear "CustomObjects"(los que se encuentran en la carpeta con dicho nombre).
 */

public class LoadText 
{
    //Crea y devuelve un diccionario a partir de un archivo de texto.
    //El archivo debe tener un número par de lineas
    public Dictionary<string, string> TextLoad(string path)
    {
        Dictionary<string, string> dicc = new Dictionary<string, string>();
        List<string> allLinesText = File.ReadAllLines(path).ToList();
        foreach(string text in allLinesText)
        {
            int index = allLinesText.IndexOf(text);
            if(index % 2 == 0)
            {
                dicc.Add(text, allLinesText[index + 1]);
            }
        }
        return dicc;
    }
}
