/*  
Centralización del uso de strings bajo variables constantes. 
Tiene la ventaja de que las strings solo se escriben en este
lugar y todas las clases que necesiten hacer refencias a ellas 
lo hacen a travéz de esta clase, evitando posibles "typos" y 
facilidando un posible cambio a futuro (solo hay que cambiarlas acá
y los lugares en dónde se referencian no se ven afectados)
*/

namespace Proyect
{
	public class Constants
	//Contiene las constantes utilizadas durante el juego
	{
		public class Paths
		{
			//Ruta del archivo de guardado del progreso de los usuarios
			public const string UserProgress = @"Assets/Scripts/Model/Data/UsersProgress.txt";
		}
		
		public class Stages
		{
			//Variables constantes de los nombres de los Stages
			public const string MainMenu = "MainMenu";
			public const string StageMenu = "StageMenu";
			public const string Stage1_1 = "Stage1_1";
			public const string Stage1_2 = "Stage1_2";
			public const string Stage1_3 = "Stage1_3";
			public const string Stage2_1 = "Stage2_1";
			public const string Stage2_2 = "Stage2_2";
			public const string Stage2_3 = "Stage2_3";
			public const string Stage3_1 = "Stage3_1";
			public const string Won = "Won";
		}
	}
}
