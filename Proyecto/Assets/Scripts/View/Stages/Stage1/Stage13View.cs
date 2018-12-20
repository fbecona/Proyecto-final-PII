using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyect;

/* Stage13View

Responsabilidad:
	Conocer y manejar los componentes del stage 1_3.

Colaboradores:
	BaseStageView: hereda el método Start(), con el cual comienza la ejecución de otros métodos que 
    hacen posible el funcionamiento del nivel.

SOLID:
	Cumple con SRP al tener una sola razón de cambio. Cumple con OCP porque podemos agregar más 
    objetos o componentes del nivel sin necesidad de modificar el código. 

Patterns:
	Esta es la clase que tiene los datos necesarios para que el nivel 1_3 funcione, por lo tanto es 
    la clase que da comienzo a dicho nivel.

 */

public class Stage13View : BaseStageView
{
    public override string imagesPath { get {return "Sprites13"; }}
    public override string stageName { get {return Constants.Stages.Stage1_3; }} 
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
    }
}