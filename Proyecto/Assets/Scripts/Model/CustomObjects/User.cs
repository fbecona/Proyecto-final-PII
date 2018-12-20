using System;
using System.Collections.Generic;
using System.Collections;

/* User

Responsabilidades: 
    Encapsular los datos referentes a los usuarios

SOLID:
    Single Responsibility: su única razón de cambio es que surja la necesidad
    de agregar algún dato nuevo como por ejemplo la edad o el sexo

 */

namespace Proyect
{
    public class User : IEquatable<User>
    {
        private string name;
        public string Name { get {return name;} }
        public Level Progress { get; set; }

        public User(string name, Level progress)
        //Crea un usuario
        {
            this.name = name;
            this.Progress = progress;
        }

        public bool Equals(User other)
        //Se utiliza para poder comparar dos objetos de la clase
        {
            if (this.Name.ToLower() == other.Name.ToLower())
            {
                return true;
            }
            return false;
        }
    }
}

