using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

/* UserController

Responsabilidades:
	Control de los datos de usuarios que van a ser escritos a disco

	Conocer las herramientas para guardar/leer dichos datos y actuar como puente
	entre el solicitante del servicio y el encargado de ejecutarlo 

Colaboradores:
	IFormatter: se apoya para serializar los datos
	IPersitor: se apoya para persistir los datos en disco
	Es el nexo entre ambos

GRASP:
	Bajo Acoplamiento: hay una independencia alta entre las clases que necesitan 
	guardar/leer información y las clases que efectúan el guardado debido a que 
	UserController actúa de puente, no se llama al servicio directamente

Patrones aplicados:
	Bridge Pattern: Es la única clase que (apoyandose en UserPersistor y UserFormatter)
	guarda datos de usuarios a disco. Ésto permite un desacoplamiento de la abstracción
	(el guardado de datos) con la implementación (cómo se guardan), por lo que si en el
	futuro hubiera que cambiar cómo se guardan los datos o el formato de los mismos solo 
	habría que hacer modificaciones en las implementaciones, pero las clases que hacen uso 
	de ellas no tienen por qué enterarse ni conocer las herramientas que se estan utilizando.
 */


namespace Proyect
{
	public class UserController
	{
		private IPersistor userPersistor;
		private IFormatter<User> userFormatter;
		private List<User> currentUsers;	

		public UserController()
		//Crea un UserController y carga los usuarios
		{
			this.userPersistor = new TxtPersistor(Constants.Paths.UserProgress);
			this.userFormatter = new TxtUserFormatter(this.userPersistor);
			this.currentUsers = userFormatter.DeSerialize();
		}

		public void AddUser(string newUserName)
		//Agregar un usuario dada una string que represente su nombre
		//Antes de guardarlo se asegura que esté en el formato correcto y,
		//en caso de ya existir levanta una excepción
		{
			string formatedUserName = formatUserName(newUserName);
			if (SearchUserByName(formatedUserName) == null)
			{
				Level initialLevel = new Level(Constants.Stages.MainMenu);
				User newUser = new User(formatedUserName, initialLevel);
				this.currentUsers.Add(newUser);
				SaveChanges();
			}
			else
			{
				string err_msg = String.Format("El usuario {0} ya existe, por favor intente con otro nombre" , newUserName);
				throw new UserAlreadyExistsError(err_msg);
			}
		}

		public User SearchUserByName(string userName)
		//Dada una string que representa el nombre de usuario, busca si ya existe.
		//Si existe devuelve una instancia del usuario y si no el método find por defecto
		//devuelve null
		{
			return this.currentUsers.Find(user => user.Name == formatUserName(userName));
		}

		public void UpdateUserProgress(User updatedUser)
		//Actualiza el progreso de un usuario recibido por parámetro, tanto dentro 
		//de la lista interna de usuarios como en el disco.
		//En caso de no existir registro del usuario se levanta una excepción

		{
			try
			{
				User userToUpdate = SearchUserByName(updatedUser.Name);
				userToUpdate.Progress = updatedUser.Progress;
				SaveChanges();
			}
			catch (NullReferenceException)
			{
				string err_msg = String.Format("El usuario {0} no existe, por favor proporcione un usuario valido", updatedUser.Name); 
				throw new UserDoesNotExistError(err_msg);
			}
		}
		

		public List<string> GetCurrentUserNames()
		//Devuelve una lista de los nombres de los usuarios actuales en el sistema
		{
			List<string> currentUserNames = new List<string>();
			foreach (User user in this.currentUsers)
			{
				currentUserNames.Add(user.Name);
			}
			return currentUserNames;
		}

		private void SaveChanges()
		//Delega a UserFormatter la persistencia de datos a disco, llamado cada vez que hay
		//un cambio en el progreso de un jugador o se agregan/quitan jugadores
		{
			this.userFormatter.Serialize(this.currentUsers);
		}

		public void DeleteUser(string userName) 
		//Borrado de un usuario. Se chequea antes su existencia y si no existe, 
		//se raisea una excepción 

		{
			string formatedUserName = formatUserName(userName);
			try
			{
				User userToDelete = SearchUserByName(formatedUserName);
				this.currentUsers.Remove(userToDelete);
				SaveChanges();
			}
			catch (ArgumentOutOfRangeException)
			{
				string err_msg = String.Format("El usuario {0} no existe, por favor proporcione un usuario valido", userName);
				throw new UserDoesNotExistError(err_msg);
			}
		}

		string formatUserName(string userName) 
		//Da un formato homogéneo al nombre de los usuarios antes de ser guardados
		//internamente y en disco, sacando múltiples espacios entre palabras y 
		//capitalizandolas
		{
			string[] names =  userName.Trim().Split(' ');
			List<string> cleanedNames = new List<string>();
			foreach (string name in names) 
			{
				if (name != "") 
				{
					string cleanedName = char.ToUpper(name[0]) + name.Substring(1);
					cleanedNames.Add(cleanedName);
				}
			}
			return String.Join(" ", cleanedNames.ToArray());
		}
	}
}