using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Proyect;

/*  MainMenu

Responsabilidades:
	Es la pantalla de bienvenida donde comienza el juego. Al cargar se le da opción de
	crear un usuario nuevo, borrar usuarios ya existentes o comenzar el flujo del juego 
	si ya hay un usuario creado
	
Colaboradores:
	Se apoya en UserController para persistir los datos del usuario entre sesiones
	y para controlar que no se ingresen usuarios repetidos

	Cuando el jugador quiere comenzar el juego, se lo notifica a GameController 

GRASP:
	Controller: Responde a eventos generados por el usuario (crear/seleccionar/borrar usuarios
	y el comienzo del juego) y los delega a los colaboradores pertinentes
 */
 
public class MainMenuView : MonoBehaviour 
{
	public Dropdown ddSelectUser;
	public GameObject panelAddNewUser, popUp;
	public Button  Play, AddNewUser, RemoveUser;
	public InputField inNewUserName;
	public Text popUpText, popUpButtonText;
	public GameController gameController = Singleton<GameController>.Instance;
	private UserController userController = Singleton<UserController>.Instance;
	private string selectedUserName;
	private List<string> options = new List<string>();
	private const string menuOption1 = "Elegir usuario";
	private const string menuOption2 = "Crear nuevo usuario";
	
	void Start()
	//Carga las opciones y las funciones asignadas a los botones
	{
		HidePopup();

		this.options.Add(menuOption1);
		this.options.Add(menuOption2);
		
        Play.onClick.AddListener(BeginGame);
        RemoveUser.onClick.AddListener(DeleteUser);
        AddNewUser.onClick.AddListener(AddUser);

		updateUserDropdown();
		this.panelAddNewUser.SetActive(false);
	}

	void updateUserDropdown()
	//Cada vez que hay cambios en la lista de usuarios se encarga de actualizar
	//el dropdown de selección para que sea coherente con los usuarios "reales" al momento
	{
		this.ddSelectUser.ClearOptions();
		this.ddSelectUser.AddOptions(this.options);
		this.ddSelectUser.AddOptions(this.userController.GetCurrentUserNames());
	}

	public void AddUser() 
	//Se ejecuta al clickear el botón de agregar usuario y le delega la tarea
	//a usercontroller, atrapando la excepción por si es un usuario ya existente
	//y mostrando un mensaje indicándolo
	{
		string username = this.inNewUserName.text;
		try
		{
			userController.AddUser(username);
		}
		catch (UserAlreadyExistsError e)
		{
			Show_Warning(e.Message);
		}
		finally
		{
			this.updateUserDropdown();
		}
	}

	public void DeleteUser()
	//Borra de usuarios del registro atrapando las excepciones de ArgumentOutOfRangeException
	//y UserDoesNotExistError por si se intenta borrar una opción del dropdown en vez
	//de un usuario
	{
		try
		{
			this.selectedUserName = ddSelectUser.options[ddSelectUser.value].text;
			this.userController.DeleteUser(this.selectedUserName);
		}
		catch (Exception ex)
		{
			if (ex is ArgumentOutOfRangeException || ex is UserDoesNotExistError)
			{
				Show_Warning("Por favor seleccione un usuario valido para borrar");
			} 
		}
		finally
		{
			this.updateUserDropdown();
		}
	}

	public void IndexChanged(int index) 
	//Función para leer cuál es la opción actual seleccionada en el UserDropdown
	
	{
		if (index == 1) 
		{
			this.panelAddNewUser.SetActive(true);
		}
		else if (index > 1) 
		{
			int n_options = this.options.Count;
			this.selectedUserName = userController.GetCurrentUserNames()[index - n_options];
		}
	}

	public void BeginGame()
	//Cuando el jugador da play, se chequea que haya un usuario seleccionado. Si lo hay
	//se le manda un mensaje a GameController para que comienze el juego y sino, 
	//se muestra un popUp pidiendo que se seleccione uno
	{
		if (this.selectedUserName == null)
		{
			Show_Warning("Por favor seleccione un usuario para comenzar el juego");
		}
		else
		{
			User SelectedUser = userController.SearchUserByName(this.selectedUserName);
			try
			{
				gameController.StartGame(SelectedUser);
			}
			catch (GameFlowError e)
			{
				Show_Warning(e.Message);
			}
		}
	}

	private void Show_Warning(string message)
	//Muestra el popUp con el mensaje deseado
	{
		this.popUpText.text = message;
		this.popUpButtonText.text = "Continuar";
		this.popUp.SetActive(true);
	}
	public void HidePopup()
	//Esconde el popUp para proseguir con el juego
	{
		this.popUp.SetActive(false);
	}
}