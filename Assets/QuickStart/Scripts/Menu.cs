using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class Menu : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene("GameList");
        }

        void Start()
        {
            //presume players quit multiplayer, and wants to check highscores or single player now.
            if (NetworkManager.singleton != null) { Destroy(NetworkManager.singleton.gameObject); }
        }
    }
}