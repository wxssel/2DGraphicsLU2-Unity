using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Helpers
{
    public class SceneLoader : MonoBehaviour
    {
        public static void Loadscene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public static void Quit()
        {
            Application.Quit();
        }

    }
}