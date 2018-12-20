using System;
using System.Collections.Generic;
using System.Collections;

/* Level

Responsabilidades: 
    Encapsular los datos referentes a los niveles

SOLID:
    Single Responsibility: representar los niveles como un objeto permitiendo su
    comparación 

 */

namespace Proyect
{
    public class Level : IEquatable<Level>
    {
        private string name;
        public string Name { get {return this.name; }}

        public Level(string name)
        //Crea un nivel
        {
            this.name = name;
        }

        public bool Equals(Level other)
        //Se utiliza para poder comparar dos objetos de la clase
        {
            if (this.name == other.name)
            {
                return true;
            }
            return false;
        }
    }

}