using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Proyect;

/* Stage22View

Responsabilidades:
	Conocer y manejar los componentes del stage 2_2.

Colaboradores:
	BaseStageView: hereda el método Start() y ChangeOption(), con el primero comienza la ejecución de 
    otros métodos que hacen posible el funcionamiento del nivel, con el segundo se cambian los 
    textos de las opciones.

SOLID:
	Cumple con SRP al tener una sola razón de cambio (cambiar los textos está dentro de manejar los 
    componentes) Podemos agregar más objetos o componentes del nivel sin necesidad de modificar el codigo.
    Cumple con OCP porque, por ejemplo, se pueden agregar más opciones para cambiar pero el diccionario
    contendrá todos los textos necesarios, por lo que la estructura de la clase será la misma.

Patterns:
	Esta es la clase que tiene los datos necesarios para que el nivel 2_2 funcione, por lo tanto es 
    la clase que da comienzo a dicho nivel.
    Crea una instancia de LoadText porque lo usa estrechamente en ChangeOption(), y porque es él que
    tiene los datos necesarios (path) para crearlo.

 */

public class Stage22View : BaseStageView
{
    public Text Text_A, Text_B, Text_C;
    public Dictionary<string, string> dicc{get; private set;}
    public override string imagesPath { get {return "Sprites22"; }}
    public override string stageName { get {return Constants.Stages.Stage2_2; }}
    private LoadText TextLoad = new LoadText();
    public Button Button1, Button2, Button3;

    void Start()
    //Añade los botones a la lista de botones en BaseStageView y empieza la cadena de funciones que 
    //componen el nivel
    {
        base.OptionButtons.Add(Button1);
        base.OptionButtons.Add(Button2);
        base.OptionButtons.Add(Button3);
        base.LoadOptionButtons();

        base.StartStage();
        
        ChangeOption();
    }
    public override void ChangeOption()
    //Cambia el texto de las opciones dependiendo de la imagen presente
    {
        dicc = TextLoad.TextLoad(@"Assets\Resources\Stage22Answers.txt");
        Text_A.text = dicc[stageModel.ActualImage.Name + "1"];
        Text_B.text = dicc[stageModel.ActualImage.Name + "2"];
        Text_C.text = dicc[stageModel.ActualImage.Name + "3"];
    }
}