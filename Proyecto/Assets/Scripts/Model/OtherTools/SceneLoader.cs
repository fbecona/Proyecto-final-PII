using UnityEngine.SceneManagement;

namespace Proyect
{
    public class SceneLoader
    {
        public void LoadScene(Level level)
        //Indica al motor que cambie a la escena enviada en el parámetro
        {
            SceneManager.LoadScene(level.Name);
        }
    }
}
