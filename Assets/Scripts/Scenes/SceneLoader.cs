using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Helpers
{
    public class SceneLoader : MonoBehaviour
    {
        public static void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

    }
}