using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Channel3.RetroRaid.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button btnPlay;
        [SerializeField] private Button btnSettings;
        [SerializeField] private Button btnQuit;

        private void Start()
        {
            btnPlay.onClick.AddListener(OnButtonPlayPressed);
            btnSettings.onClick.AddListener(OnButtonSettingsPressed);

#if !UNITY_EDITOR && !UNITY_STANDALONE
            btnQuit.gameObject.SetActive(false);
#else          
            btnQuit.onClick.AddListener(OnButtonQuitPressed);
#endif
        }

        private void OnDestroy()
        {
            btnPlay.onClick.RemoveListener(OnButtonPlayPressed);
            btnSettings.onClick.RemoveListener(OnButtonSettingsPressed);
            btnQuit.onClick.RemoveListener(OnButtonQuitPressed);
        }

        public void OnButtonPlayPressed()
        {
            
        }

        public void OnButtonSettingsPressed()
        {
            
        }

        public void OnButtonQuitPressed()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#elif UNITY_STANDALONE
            Application.Quit();
#endif
        }
    }
}
