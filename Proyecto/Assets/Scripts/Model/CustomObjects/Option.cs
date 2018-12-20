using System;
using System.Collections.Generic;
using System.Collections;

/* Option

Responsabilidades: 
    Encapsular los datos referentes a las opciones a elegir dentro de cada nivel

SOLID:
    Single Responsibility: representar una opción como un objeto permitiendo su
    comparación

 */


namespace Proyect
{
    public class Option : IEquatable<Option>
    {
        private string name;
        public string Name { get {return this.name; }}

        public Option(string Name)
        //Crea una opción
        {
            this.name = Name;
        }
        
        public bool Equals(Option other)
        //Se utiliza para poder comparar dos objetos de la clase
        {
            if (this.Name == other.Name)
            {
                return true;
            }
            return false;
        }
    }
}