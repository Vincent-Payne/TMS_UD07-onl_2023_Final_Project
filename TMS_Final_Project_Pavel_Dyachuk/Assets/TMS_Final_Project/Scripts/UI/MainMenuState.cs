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

            Time.timeScale = 1f;
        }

        private void OnStart()
        {
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