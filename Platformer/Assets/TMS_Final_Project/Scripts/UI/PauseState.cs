using System;
using Platformer.Events;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Promenade.Scripts.GamePhysics.UI
{
    public class PauseState : BaseUIState
    {
        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private IntEventChannel _eventChannel;


        public override void Enter()
        {
            base.Enter();

            Time.timeScale = 0f;

            _resumeButton.onClick.AddListener(OnResume);
            _quitButton.onClick.AddListener(OnQuit);

            _eventChannel.OnEventPublished += OnEventPublished;
        }

        private void OnEventPublished(int arg0)
        {
            _UIManager.SwitchTo(typeof(GameState));
        }

        public override void Exit()
        {
            base.Exit();

            _resumeButton.onClick.RemoveListener(OnResume);
            _quitButton.onClick.RemoveListener(OnQuit);

            _eventChannel.OnEventPublished -= OnEventPublished;

            Time.timeScale = 1f;
        }

        private void OnResume()
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