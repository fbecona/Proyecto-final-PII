/* IPersistor

Interfaz para la persistencia de datos 

Grasp:
    Polimorfismo: cada clase adherente provee su propia implementación del guardado/escritura
    de datos

    Hollywood principle: las clases de bajo nivel (las que adhieran al contrato detallando 
	cómo se guardan los datos) proveen una implementación para que las clases de alto nivel (los 
	controladores) las llamen cuando necesiten el servicio

SOLID:
    Single Responsibility: las clases que adhieran a este contrato solo se comprometen
    a leer y escribir datos

    Open-Closed: Se favorece una implementación abierta a la extensión porque cada 
	clase que necesite guardar información, simplemente debe utilizar un persistor adecuado
    (cada posible persistor se especializa en un tipo de guardado de datos: archivos de texto, 
    base de datos, json, etc) y, en caso de no existir solo se debe implementar un nuevo. 
    Es cerrado al cambio ya que al agregar nuevas fuentes de datos no se deben modificar los 
    anteriores ya implementados 

    DIP: se estructura el guardado de información contra una abstracción: la escritura/lectura 
    de datos. Tanto los módulos de alto nivel (que necesiten que sus datos sean guardados) como 
    los módulos de bajo nivel (los que proveen la implementación específica de escritura/lectura)
    dependen de dicha abstracción
 
	ISP: es una interfaz específica, que obliga sólo a utilizar dos métodos íntimamente relacionados,
	no obligando a sus clientes a implementar otros métodos que no van a utilizar

 */

namespace Proyect 
{
    public interface IPersistor
    {
        //Devuelve datos
        string[] Read();
        //Guarda datos
        void Write(string DataToPersist);
    }
}