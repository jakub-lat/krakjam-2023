using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject howToPlay;
        
        public void HowToPlay()
        {
            mainMenu.SetActive(false);
            howToPlay.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
