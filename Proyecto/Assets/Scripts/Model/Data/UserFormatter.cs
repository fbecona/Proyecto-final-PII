using System;
using System.Collections.Generic; 
 
/* TxtUserFormatter 
 
Responsabilidades: 
	Serializar y deserializar objetos de la clase User para su guardado en disco 
 
Colaboradores: 
	Persistor: le manda los datos prontos para ser guardados y también le pide 
	los datos ya guardados 
 
SOLID: 
	Single-Responsibility: la única razón que tiene para cambiar es que se reestructure 
	cómo se guardan los datos de usuarios 
 
GRASP: 
	Creator y Expert: dado que conoce la estructura interna de los objetos de la clase User 
	y también conoce los datos necesarios para su contrucción(Expert), junto con UserController
	son las únicas clases que crean objetos User(Creator)  
 
 Patrones:
	Builder: Se utiliza este patron siendo IFormatter la interfaz abstracta para crear productos
	UserFormatter una implementación del Builder, UserController quien llama al Builder para 
	construir objetos y User el objeto bajo construcción.
 */ 
 
namespace Proyect  
{ 
    public class TxtUserFormatter : IFormatter<User>  
	{ 
		private IPersistor persistor; 

		public TxtUserFormatter(IPersistor persistor) 
		//Crea un TxtUserFormatter y asigna un IPersistor
		{ 
			this.persistor = persistor; 
		} 

		public List<User> DeSerialize()
		//Devuelve una lista de usuarios desdearlizados 
		{ 
			List<User> userList = new List<User>(); 
			string[] currentStoredUsers = this.persistor.Read(); 
			foreach (string userData in currentStoredUsers) 
			{ 
				if (userData!="")
				{
					User user = this.DeSerialize(userData);
					userList.Add(user); 
				}
			} 
			return userList; 
		} 
		public void Serialize(List<User> UsersToSave)
		//Serializa una lista de usuarios 
		{ 
			string allUsersData = ""; 
			foreach (User user in UsersToSave) 
			{ 
				string serializedUser = this.Serialize(user); 
				allUsersData += serializedUser + '\n'; 
			} 
			this.persistor.Write(allUsersData); 
		} 
		private string Serialize(User user)
		//Serializa un usuario  
		{ 
			return user.Name + "-" + user.Progress.Name; 
		} 

		private User DeSerialize(string SerializedString)
		//Deserializa un usuario 
		{ 
			string[] strings = SerializedString.Split('-');
			Level currentProgress = new Level(strings[1]);  
			User LoadedUser = new User(strings[0], currentProgress); 
			return LoadedUser;
		} 
    } 
}