using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class LevelController : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += SceneChanged;
        }

        private void SceneChanged(Scene arg0, Scene arg1)
        {
            GameManager.ChangeGameState(GameState.Preporations);
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= SceneChanged;
        }

        [ContextMenu("Change Scene")]
        private void ChangeScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}


