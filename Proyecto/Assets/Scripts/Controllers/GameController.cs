using System;
using System.Collections.Generic;

/*  GameController

Patrones:
    Se cumple con MVC dado que en la View carga la información del modelo y 
    este también interactúa con los controllers, los cuales son los responsables
    del manejo del juego entre sus diferentes etapas sin que le importe 
    concretamente que es lo que sucede en cada una de ellas ni lo que se 
    muestra en pantalla.

Responsabilidades:
	Controlar el flujo del juego, conociendo el orden en el que las pantallas deben 
    ser cargadas, el usuario actual y su progreso 
	
Colaboradores:
	UserController: le reporta a userController cambios en el progreso del usuario
    para que sean persistidos 
    SceneLoader: le delega a sceneLoader el cargar las escenas

GRASP:
	Controller: Responde a eventos generados por el usuario. Si bien no son eventos
    "directos" (ya que estos son atrapados y escuchados por cada una de las stages),
    las stages se apoyan en GameController para saber cómo responder frente a ellos.
    Por ejemplo: cada stage individual sabe cuando se completó el nivel, y se apoya
    en GameController para pasar al siguiente
    Alta Cohesión: La información que almacena esta clase debe es coherente con su
    prósito de ser la que controla el desarrollo del juego.

 */

namespace Proyect
{
    public class GameController
    {
        private User actualUser;
        public User ActualUser { get {return actualUser;} }
        private List<Level> levels;
        private List<Level> levelsLeft = new List<Level>();
        public List<Level> LevelsLeft { get {return levelsLeft;} }
        private UserController userController = Singleton<UserController>.Instance;
        private SceneLoader sceneLoader = new SceneLoader();

        public void StartGame(User currentUser)
        //Es la que comienza el desarrollo del juego tras haber cargado el usuario
        {
            if (currentUser != null)
            {
                this.actualUser = currentUser;
                LoadStageNames();
                UpdatePassedStages();
                LoadLevel(Constants.Stages.StageMenu);
            }
            else
            {
                string err_msg = "Debe haber un usuario seleccionado para comenzar el juego";
                throw new GameFlowError(err_msg);
            }
        }

        private void LoadStageNames()
        //Carga los niveles en el controlador
        {
            this.levels = new List<Level>();
            this.levels.Add(new Level(Constants.Stages.MainMenu));
            this.levels.Add(new Level(Constants.Stages.StageMenu));
            this.levels.Add(new Level(Constants.Stages.Stage1_1));
            this.levels.Add(new Level(Constants.Stages.Stage1_2));
            this.levels.Add(new Level(Constants.Stages.Stage1_3));
            this.levels.Add(new Level(Constants.Stages.Stage2_1));
            this.levels.Add(new Level(Constants.Stages.Stage2_2));
            this.levels.Add(new Level(Constants.Stages.Stage2_3));
            this.levels.Add(new Level(Constants.Stages.Stage3_1));
            this.levels.Add(new Level(Constants.Stages.Won));
        }

        public void UpdatePassedStages()
        //Actualiza las stages ya desbloqueadas por el jugador actual
        {
            this.levelsLeft = new List<Level>();
            Level UserProgress = this.actualUser.Progress;
            int currentLevelPriority = this.levels.IndexOf(UserProgress);

            int levelPriority;
            foreach (Level level in this.levels)
            {
                levelPriority = this.levels.IndexOf(level);
                if (levelPriority > currentLevelPriority)
                {
                    this.levelsLeft.Add(level);
                }
            }
        }

        public void LevelPassed(Level currentLevel)
        //Se ocupa de pasar de nivel, carga el siguiente nivel y si corresponde 
        //guarda el progreso del jugador
        {
            Level nextLevel = GetNextLevel(currentLevel);
            if (nextLevel != null)
            {
                this.levelsLeft.Remove(currentLevel);
                if (SaveOrNot(currentLevel))
                {
                    this.actualUser.Progress = nextLevel;
                    this.userController.UpdateUserProgress(this.actualUser);
                }
                LoadLevel(nextLevel.Name);
            }
        }

        public bool SaveOrNot(Level actualLevel)
        //Devuelve si hay que guardar o no el nivel pasado dependiendo de si el
        //usuario ya paso por ese nivel anteriormente
        {
            return levels.IndexOf(actualLevel)>=levels.IndexOf(actualUser.Progress);
        }

        private Level GetNextLevel(Level currentLevel)
        //Dado un nivel devuelve el nivel siguiente
        {
            int nextLevelIndex = 0;
            nextLevelIndex = this.levels.IndexOf(currentLevel) + 1;
            Level nextLevel = this.levels[nextLevelIndex];
            return nextLevel;
        }

        public void LoadFromStageMenu(Level Level)
        //Se comunica con el modelo para cargar una escena desde el StageMenu
        {   
            Level stageMenu = new Level(Constants.Stages.StageMenu);
            levelsLeft.Remove(stageMenu);
            sceneLoader.LoadScene(Level);
        }

        private void LoadLevel(string levelName)
        //Se comunica con el modelo para cargar un determinado nivel
        {
            Level levelToLoad = new Level(levelName);
            if (this.levels.Contains(levelToLoad))
            {
                sceneLoader.LoadScene(levelToLoad);
            }
            else
            {
                string err_msg = String.Format("Level {} does not exist", levelName);
                throw new GameFlowError(err_msg);
            }
        }

        public void GoHome()
        //Se comunica con el modelo para cargar la escena StageMenu
        {
            UpdatePassedStages();
            LoadLevel(Constants.Stages.StageMenu);
        }
    }
}
