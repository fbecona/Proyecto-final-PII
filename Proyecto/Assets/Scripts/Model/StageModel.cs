using System;
using System.Collections.Generic;

/* StageModel

Responsabilidades: 
    Brindar soporte para el funcionamiento de las stages

Colaboradores:
    GameController: apoyo para cambiar stages

Grasp:
    Polimorfismo: La operación Attach() cumple con no preguntar por la
    clase de los objetos con los que interactúa. Está se comunica de igual forma 
    con todos los objetos que tengan el interfaz IStageModelObserver.
    Don´t Talk to Strangers: Esta clase enviar mensajes sólo a sí mismo, a un
    parámetro, a un atributo, a un objeto creado en el método, 
    o al contenido de una colección poseída.

Patrones:
    Observer Pattern: StageModel cumple el rol del Sujeto, proporcionando una
    interfaz para que las StageView individuales(suscriptores) se sumen a su
    broadcast de mensajes(Attach()).
    Es una implementación un tanto peculiar ya que no se necesita una de-suscripción:
    Unity automáticamente destruye los objetos de una escena previa al pasar a otra. 
    Por ello no está implementado el método de UnSuscribe y hay un currentObserver
    en vez de una lista de observers.

 */

namespace Proyect
{
    public class StageModel
    {
      	private GameController gameController = Singleton<GameController>.Instance;
        private IStageModelObserver currentObserver;
        public Level ActualLevel{get;set;}
        public List<Image> StageImages{get;private set;}
        public Image ActualImage{get;private set;}
        public List<Option> AvailableOptions{get;private set;}
        public Option SelectedOption{get;private set;}
        private int CurrentImageIndex;
        private bool ChangeLevel;
        private bool ChangeStage;

        public void Initialize()
        //Inicia un nivel
        {
            LoadNextLevel();
        }

        public void ResetImages()
        //Resetea las imágenes guardadas en la instancia de StageModel
        {
            this.StageImages = new List<Image>();
        }

        public void ResetOptions()
        //Resetea las opciones guardadas en la instancia de StageModel
        {
            this.AvailableOptions = new List<Option>();
        }

        public void AddOption(string option)
        //Añade una opción a las disponibles de la Stage
        {
            if(option!=null)
            {
                AvailableOptions.Add(new Option(option));
            }
            else
            {
                string err_msg = "Error al cargar las opciones";
                throw new GameFlowError(err_msg);
            }
        }

        public void LoadImageIntoQueue(string image)
        //Crea las imágenes de la Stage enviando las opciones disponibles del nivel
        {
            this.StageImages.Add(new Image(image,AvailableOptions));
        }

        public void Play(string option)
        //Hace una jugada
        {
            if(option == null)
            {
                string err_msg = "Error con la opción seleccionada";
                throw new GameFlowError(err_msg);
            }
            this.SelectedOption = new Option(option);
            CheckState();
        }

        private void CheckState()
        //Notifica si se terminó el nivel, si finalizó la stage o si se seleccionó
        //una opción incorrecta
        {
            this.ChangeLevel = CheckWinner(this.SelectedOption);
            this.ChangeStage = CheckEndStage();
            if(this.ChangeLevel)
            {
                this.Notify(Notification.CorrectOption);
                if (this.ChangeStage)
                {
                    this.Notify(Notification.EndStage);
                }
            }
            else
            {
                this.Notify(Notification.WrongOption);
            }
        }

        public bool CheckWinner(Option option)
        //Verifica si el usuario seleccionó la opción correcta
        {
            return ActualImage.CorrectOption.Name == option.Name;
        }

        public void ChangeState()
        //Cambia el estado actual del juego pasando al siguiente nivel
        //o a la siguiente escena dependiendo del caso
        {
            if (this.ChangeLevel)
            {
                if(this.ChangeStage)
                {
                    gameController.LevelPassed(ActualLevel);
                }
                else
                {
                    LoadNextLevel();
                }
            }
        }

        private void LoadNextLevel()
        //Carga el siguiente nivel
        {
            System.Random random = new System.Random();
            CurrentImageIndex = random.Next(0, StageImages.Count);
            this.ActualImage = StageImages[CurrentImageIndex];
            StageImages.RemoveAt(CurrentImageIndex);
            this.Notify(Notification.LoadImage);
        }

        public bool CheckEndStage()
        //Verifica si la Stage se terminó o no
        {
            if (StageImages.Count == 0)
            {
                this.Notify(Notification.EndStage);
                return true;
            }
            return false;
        }

        public void GoHome()
        //Se comunica con el controllador de flujo de juego para que 
        //este le envie un mensaje al modelo y así se cargue el StageMenu
        {
            gameController.GoHome();
        }

        public void Attach(IStageModelObserver observer)
        //Agrega a la stage actual como observadora de las notificaciones
        {
            this.currentObserver = observer;
        }

        public void Notify(Notification notification)
        //Notifica a la View los diferentes eventos que suceden durante la Stage
        {
            currentObserver.Notify(notification);
        }
    }
}
