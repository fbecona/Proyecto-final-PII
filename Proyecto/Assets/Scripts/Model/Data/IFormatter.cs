using System.Collections.Generic; 

/* IFormatter 
 
Interfaz para la serialización de objetos y su posterior reconstrucción 
 
GRASP: 
	Polimorfismo: cada clase que adhiere al contrato define sus propias formas de serializar 
	y deserializar objectos
 
	Hollywood principle: las clases de bajo nivel (las que adhieran al contrato detallando 
	cómo operan las funciones) proveen una implementación para que las clases de alto nivel (los 
	controladores) las llamen cuando necesiten el servicio 

	Cohesión alta: Considerando el proceso entero del guardado y lectura de datos hay un alto 
	acomplamiento ya que las responsabilidades y tareas necesarias para su ejecución están
	bien separadas:
		En el nivel más alto está el controlador quien es el solicitante del serivicio, quien 
		se encarga de hacer verificaciones/chequeos de la información antes de ser guardada

		En un segundo nivel está IFormatter quien recibe solicitudes de guardado/lectura sobre
		objetos ya controlados

		Y por úlitmo en el nivel más bajo se encuentra una implementación concreta de IPersistor 
		la que deberá únicamente escribir/leer los datos que se le solicitan, sin tener que conocer
		absolutamente nada sobre esos datos

	Bajo acoplamiento: si bien en el proceso de guardado/lectura de datos recién mencionado
	hay una interdependencia entre las clases para funcionar, cada una es lo suficientemente 
	independiente como para que cambios en alguna parte del proceso no afecten a las demás.
		Por ejemplo: si el día de mañana los datos fueran muchos más y de deseara usar una 
		base de datos relacional, lo único que habría que hacer es implementar un nuevo formatter
		para serializar/deserializar objectos y un nuevo persistor que guarde los datos a dicha
		base de datos en vez de un archivo de texto.

 
SOLID: 

	Single Responsibility: se encarga únicamente de traducir datos desde string hacia objetos
	y viceversa

	Open-Closed: El contrato favorece una implementación abierta a la extensión porque cada 
	clase que necesite guardar información, simplemente debe implementar su Formatter 
	definiendo cómo serilizar sus objetos. También es cerrado al cambio ya que al agregar un nuevo 
	formatter, no se deben modificar los anteriores ya implementados 
 
	DIP: tanto las clases de alto nivel cómo las implementaciones dependen en la abstracción 
	de serializar/deserializar un objeto, aislándose los detalles y posibles cambios. 
 
	ISP: es una interfaz específica, que obliga sólo a utilizar dos métodos íntimamente relacionados,
	no obligando a sus clientes a implementar otros métodos que no van a utilizar
 
 */ 
 
namespace Proyect 
{ 
	public interface IFormatter <T>   
	{ 
		//Convierte de una lista de objetos a string 
		void Serialize(List<T> objList); 
 
		//Construye objetos desde string 
		List<T> DeSerialize(); 
	} 
} 