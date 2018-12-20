/*
Detallado de los distintos tipos de notificaciones a ser enviadas/recibidas
en el transcurso del juego
 */

namespace Proyect
{
    public enum Notification
    //Contiene las opciones de notificación disponibles
    {
        CorrectOption,
        WrongOption,
        EndStage,
        LoadImage,
        
    }
    public interface IStageModelObserver
    {
        //Notifica de un evento
        void Notify(Notification notification);
    }
}