using System;

/*
Excepciones para usar cuando ocurren errores a la hora de agregar o eliminiar usuarios
 */

namespace Proyect
{    

    public class UserAlreadyExistsError : Exception
    //Excepción si un usuario que se quiere crear ya existe
    {
        public UserAlreadyExistsError() {}

        public UserAlreadyExistsError(string message) : base(message) {}

        public UserAlreadyExistsError(string message, Exception innerException) : base(message, innerException) {}
    }

    public class UserDoesNotExistError : Exception 
    //Excepción si un usuario no existe
    {
        public UserDoesNotExistError() {}

        public UserDoesNotExistError(string message) : base(message) {}
        
        public UserDoesNotExistError(string message, Exception innerException) : base(message, innerException) {}
    }
}