using System;
using System.Collections.Generic;
using System.Collections;

namespace Proyect
{
    public class Image : IEquatable<Image>
    {
        public List<Option> PossibleOptions{get;private set;}
        public Option CorrectOption { get; private set;}
        public string Name { get; private set;}

        public Image(string Name,List<Option> AvailableOptions)
        //Crea una imagen con las opciones posibles de la stage correspondiente
        {
            this.Name = Name;
            this.PossibleOptions = AvailableOptions;
            this.CorrectOption = correctOption(Name);
        }

        private Option correctOption(string Name)
        //Selecciona cual es la opción correcta de la imagen entre las disponibles
        {
            foreach (Option option in PossibleOptions)
            {
                if(Name.Contains(option.Name))
                {
                    return option;
                }
            }
            string err_msg = "Ha seleccionado una opción no reconocida por el juego"; 
			throw new GameFlowError(err_msg);
        }

        public bool Equals(Image other)
        //Se utiliza para poder comparar dos objetos de la clase
        {
            if (this.CorrectOption == other.CorrectOption)
            {
                return true;
            }
            return false;
        }
    }
}