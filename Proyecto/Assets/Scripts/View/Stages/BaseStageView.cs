using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Proyect;

/* BaseStageView

Responsabilidades:
	Abstraer la funcionalidad común a todas las stages.

Colaboradores:
	StageModel: le delega el procesamiento de las opciones y el cargado de imágenes

Principio DRY y herencia:
	Como todas las stages del juego tienen una funcionalidad similiar que es
	la elección de opciones frente a imágenes que se le muestran al usuario, 
	se creó esta clase base abstracta en la cual se implementa dicho comportamiento
	en común (cargar imágenes, cargar opciones, mostrar el panel con mensajes, etc) 
	mientras que cada stage individual tiene la libertad de implementar nueva funcionalidad 
	para su uso particular. (Por ejemplo en las stage dos que tienen un método propio 
	para cambiar de opciones y mostrar texto)

SOLID:
	Open-Closed: Este modelo tiene la ventaja que agregar stages del mismo estilo de juego
	es muy simple: hay que extender esta clase base y proporcionar sus elementos individuales,
	sin tener que cambiar las ya existentes.
	Reconocemos que este modelo por herencia tiene la desventaja de la rigidez. Si el día de
	mañana el cliente solicitara un tipo de pantalla completamente distinto, esta clase base
	no sería reutilizable en absoluto. Sin embargo entendemos que abstraer aún más la funcionalidad
	que hoy en día resulta útil para posibles casos futuros tendría el defecto o "smell" de 
	"Needless complexity".
	Haciendo una cita del libro "Refactoring to Patterns":
	"When you make your code more flexible or sophisticated than it needs to be, you over-engineer it. 
	Some programmers do this because they believe they know their system's future requirements. 
	They reason that it's best to make a design more flexible or sophisticated today, so it can 
	accommodate the needs of tomorrow. That sounds reasonable—if you happen to be psychic." 
	DIP: Se cumple la noción de que entre las clases más bajas relacionadas con las stages y la clase 
	más elevada GameController hay una abstracción de por medio.
	
Patterns:
	Template Method: Se presenta un algoritmo con los pasos necesarios para el cargado de cada una 
	de las pantallas con el método StartStage(). 
	Luego cada StageView específica puede extender dicho algoritmo agregando funcionalidad nueva 
	antes o depúes de llamar a base.StartStage(), o hasta incluso dentro al hacer override del
	método virtual ChangeOption() el cual es llamado en StartStage(). 
	En un futuro si surgiera la necesidad de un algoritmo más complejo para pantallas que requierán más
	funcionalidad, también se podría reutilizar agregando "Hooks" dentro del método StartStage para
	implementar dichas variaciones 

 */

public abstract class BaseStageView : MonoBehaviour, IStageModelObserver
{
	protected StageModel stageModel = Singleton<StageModel>.Instance;
	public GameObject popUp;
	public Text popUpText, popUpButtonText;
	public Button gohome;
	public Button gonext;
	public List<Button> OptionButtons;
	public Sprite[] ImagesToPlay;
	public abstract string stageName { get; }
	public abstract string imagesPath { get; }
	public string actualImage{get;private set;}
	
	//Método principal común a todas las stages dónde se definen aquellas acciones
	//en común que deben ejecutarse al inicializar cada pantalla individual:
	//	* Suscribir la pantalla como el observer actual de StageModel para poder recibir
	//	  sus notificaciones sobre eventos
	//	* Cargar las imágenes y opciones detalladas en cada pantalla individual
	//	* Inicializar el nivel   			
	//	* Ejecutar al inicializar cada pantalla individual
	public void StartStage()
	//Carga las funciones de los botones de las stages y las imagenes en el modelo; 
	//además se ocupa de inicializar a este último
	{
		stageModel.Attach(this);

		stageModel.ActualLevel = new Level(this.stageName);
		
		gohome.onClick.AddListener(stageModel.GoHome);
		gonext.onClick.AddListener(ShowNext);

		ImagesToPlay = Resources.LoadAll<Sprite>(this.imagesPath);
		stageModel.ResetImages();
		foreach (Sprite image in ImagesToPlay)
		{
			stageModel.LoadImageIntoQueue(image.name);
		}

		stageModel.Initialize();
		StartALevel();
	}

	/*Este método es virtual y no abstracto dado que no es implementado 
	en todas las stages, solo en aquellas que tienen textos*/
	public virtual void ChangeOption(){} 


	public void LoadOptionButtons()
	//Carga las funciones de las botones correspondientes a las opciones de las stages
	//y añade sus nombres como opciones disponibles a seleccionar en el modelo
    {
		stageModel.ResetOptions();
		foreach(Button button in OptionButtons)
		{
			Button Button = button.GetComponent<Button>();
			Button.onClick.AddListener(delegate{stageModel.Play(button.name);});
			stageModel.AddOption(button.name);
		}
    }

	public void LoadImage(string ImageName)
	//Carga una imagen en la View
	{
		foreach (Sprite image in ImagesToPlay)
		{
			if(image.name == ImageName)
			{
				GameObject.Find("PanelImage").GetComponent<UnityEngine.UI.Image>().sprite = image;
				actualImage = ImageName;
			}
		}
		popUp.SetActive(false);
	}
	
	public void Notify(Notification n)
	//Actúa en base al tipo de notificación recibida desde el broadcaster de 
	//notificaciones (stagemodel)
	{
		switch (n)
		{
			case Notification.LoadImage: 
				LoadImage(stageModel.ActualImage.Name);
				break;

			case Notification.CorrectOption:
				SetCorrectText();
				Audio.PlayCorrect();
				break;
			
			case Notification.WrongOption:
				SetWrongText();
				Audio.PlayWrong();
				break;

			case Notification.EndStage:
				SetStageFinishedText();
				break;

			default:
				string err_msg = string.Format("Notificacion desconocida {0}", n);
				throw new GameFlowError(err_msg);
		}
		ShowPanel();
	}

	//Métodos para cambiar el texto del popUp que se muestra al usuario a la hora de 
	//seleccionar opciones y cambiar de nivel
	public void SetCorrectText()
	{
		popUpText.text = "Bien!!";
		popUpButtonText.text = "Siguiente";
	}

	public void SetWrongText()
	{
		popUpText.text = "No ha acertado!!!";
        popUpButtonText.text = "Inténtelo de nuevo";
	}

	public void SetStageFinishedText()
	{
		popUpText.text = "Bien Hecho!!!";
		popUpButtonText.text = "Siguiente nivel"; 
	}

	public void ShowPanel()
	//Mostrar el popUp con feedback al usuario
	{
		popUp.SetActive(true);
	}
	
	public void ShowNext()
	//Cargar el siguiente Nivel o la siguiente Stage según sea el caso
	{
		stageModel.ChangeState();
		StartALevel();
	}

	public void StartALevel()
	//Resetear las opciones al empezar el nivel y esconder el popUp
	{
		ChangeOption();
		popUp.SetActive(false);
	}	
}