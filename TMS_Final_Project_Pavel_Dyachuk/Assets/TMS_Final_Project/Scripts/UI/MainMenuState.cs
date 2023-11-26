using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Promenade.Scripts.GamePhysics.UI
{
    public class MainMenuState : BaseUIState
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private GameObject _healthBar;
        [SerializeField]
        private GameObject _cherryBar;


        public override void Enter()
        {
            base.Enter();

            Time.timeScale = 0f;

            _startButton.onClick.AddListener(OnStart);
            _quitButton.onClick.AddListener(OnQuit);
        }

        public override void Exit()
        {
            base.Exit();

            _startButton.onClick.RemoveListener(OnStart);
            _quitButton.onClick.RemoveListener(OnQuit);
            _healthBar.SetActive(true);
            _cherryBar.SetActive(true);
            Time.timeScale = 1f;
        }

        private void OnStart()
        {

            _healthBar.SetActive(false);
            _cherryBar.SetActive(false);
            _UIManager.SwitchTo(typeof(GameState));
        }

        private void OnQuit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}