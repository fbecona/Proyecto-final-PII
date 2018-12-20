using System;
using System.Runtime.Serialization;

/* 
Excepción para uso cuando ocurren errores de tipo genérico durante el juego. 
Por ejemplo cuando se trata de empezar el juego sin un usuario seleccionado,
se trata de acceder a un nivel bloqueado, etc.
 */

namespace Proyect
{
    public class GameFlowError : Exception
    //Excepción utilizada para errores durante el transcurso del juego
    {
        public GameFlowError() {}

        public GameFlowError(string message) : base(message) {}

        public GameFlowError(string message, Exception innerException) : base(message, innerException) {}

    }
}