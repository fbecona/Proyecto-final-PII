/*
Patrón Singleton: Aplicado en aquellas clases controladoras en las que más
de una instancia de la misma clase podría generar un mal funcionamiento del
juego

Por ejemplo: la clase GameController lleva la información del jugador actual
y su progreso. Si hubiera más de una instancia habría una desincronización del
progreso del usuario, generando luego resultados inesperados.

 */

namespace Proyect
{
    public class Singleton<T> where T : new()
    //Se utiliza para que se pueda crear una sola instancia de objeto que persista durante
    //los cambios de escena de Unity
    {
        private static T instance;
        
        public static T Instance {
            get {
                if (instance == null) {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}