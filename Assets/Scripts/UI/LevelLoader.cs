using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }
}