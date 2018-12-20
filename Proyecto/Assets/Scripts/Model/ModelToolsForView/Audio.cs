using UnityEngine;

/* Audio

Responsabilidades:
    Indicar al motor que reproduzca un clip de audio

Solo depende de funciones del motor
Como los métodos solo son llamados por otras clases para que sean ejecutados, mientras los nombres de estos sean respetados
la clase se puede modificar y agregar funcionalidades sin tener que tocar el resto de código. 

Documentación AudioSource.PlayClipAtPoint: https://docs.unity3d.com/ScriptReference/AudioSource.PlayClipAtPoint.html

*/


namespace Proyect
    {
    public class Audio : MonoBehaviour
    {    
        //Asigna un clip de audio a una variable
        static AudioClip correct = Resources.Load<AudioClip>("Audio/SoundCorrect");
        static AudioClip wrong = Resources.Load<AudioClip>("Audio/SoundWrong");

       //Los siguientes métodos reproducen clips de audio en la posición en el 
       //espacio del juego marcado por el vector 
        public static void PlayCorrect()
        {
            AudioSource.PlayClipAtPoint(correct, new Vector3(5, 1, 2)); 
        }
        
        public static void PlayWrong()
        {
            AudioSource.PlayClipAtPoint(wrong, new Vector3(5, 1, 2));
        }

    }
}