using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyect;

/* WonView

Responsabilidad:
	Conocer y manejar los componentes de la pantalla final, al terminar el juego.

SOLID:
	Cumple con SRP al tener una sola razon de cambio.

 */

public class WonView : MonoBehaviour
{
    public Text Username;
    public Button goHome;
	private GameController gameController = Singleton<GameController>.Instance;

    void Start()
    //Inicia la Stage cargando la función de su único botón y colocando el nombre del
    //usuario como texto ganador
    {
        Username.text = gameController.ActualUser.Name;
        Button home = goHome.GetComponent<Button>();
        home.onClick.AddListener(gameController.GoHome);
    }

}