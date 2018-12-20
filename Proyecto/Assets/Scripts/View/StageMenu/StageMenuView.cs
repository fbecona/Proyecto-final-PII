using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Proyect;

/* StageMenuView

Responsabilidad:
	Conocer y manejar los componentes del menu de stages, incluyendo a que stages puede acceder un usuario.

SOLID:
	Cumple con SRP al tener una sola razón de cambio. Cumple con OCP porque podemos agregar 
    cuantos stages queramos sin necesidad de modificar el código. 

Patterns:
	Esta es la clase que tiene los datos necesarios para que el StageMenu funcione, por lo tanto es 
    la clase que da comienzo a dicho nivel.
    
 */


public class StageMenuView : MonoBehaviour
{
    public Button mainmenu,stage1_1,stage1_2,stage1_3,stage2_1,stage2_2,stage2_3,stage3_1;
	public GameController gameController = Singleton<GameController>.Instance;
    public List<Button> buttons;

    void Start()
    //Carga las funciones de los botones, los añade a una lista y desbloquea los niveles
    //no disponibles
    {
        Button Stage1_1 = stage1_1.GetComponent<Button>();
        Stage1_1.onClick.AddListener(delegate{GoToThisStage(stage1_1.name);});
                
        Button MainMenu = mainmenu.GetComponent<Button>();
        MainMenu.onClick.AddListener(delegate{GoToThisStage(Constants.Stages.MainMenu);});

        buttons.Add(stage1_2);
        buttons.Add(stage1_3);
        buttons.Add(stage2_1);
        buttons.Add(stage2_2);
        buttons.Add(stage2_3);
        buttons.Add(stage3_1);
        
        LockDisabledStages(buttons);
    }
    
    public void LockDisabledStages(List<Button> Buttons)
    //Se encarga de desabilitar aquellos niveles que el usuario todavia no tiene
    //accesso, poniendolos de color rojo
    {
        List<string> RestrictedLevelNames = GetRestrictedLevelNames();
        foreach(Button button in Buttons)
        {
            button.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            if(RestrictedLevelNames == null | !RestrictedLevelNames.Contains(button.name))
            {
                button.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                Button Button = button.GetComponent<Button>();
                Button.onClick.AddListener(delegate{GoToThisStage(button.name);});
            }
        }
    }

    void GoToThisStage(string level)
    //Carga la pantalla de StageMenu
    {
        Level Level = new Level(level);
        gameController.LoadFromStageMenu(Level);
    }

    private List<string> GetRestrictedLevelNames()
    // Obtener una lista con los nombres de los niveles que todavía faltan
    // para bloquearlos
    {
        List<string> restrictedLevelNames = new List<string>();
        foreach (Level level in this.gameController.LevelsLeft)
        {
            restrictedLevelNames.Add(level.Name);
        }
        return restrictedLevelNames;
    }
}   