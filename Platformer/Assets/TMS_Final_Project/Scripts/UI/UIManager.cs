using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Promenade.Scripts.GamePhysics.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coinsText;

        [SerializeField]
        private TextMeshProUGUI _timerText;


        [SerializeField]
        private MainMenuState _mainMenuState;

        [SerializeField]
        private GameState _gameState;

        [SerializeField]
        private PauseState _pauseState;


        private BaseUIState _currentState;
        private Dictionary<Type, BaseUIState> _states;


        private void Start()
        {
            _mainMenuState?.Init(this);
            _gameState?.Init(this);
            _pauseState?.Init(this);

            _states = new Dictionary<Type, BaseUIState>()
            {
                { typeof(MainMenuState), _mainMenuState },
                { typeof(GameState), _gameState },
                { typeof(PauseState), _pauseState }
            };

            SwitchTo(typeof(MainMenuState));
        }


        public void SwitchTo(Type newState)
        {
            _currentState?.Exit();
            _currentState = _states[newState];
            _currentState?.Enter();
        }


        private void OnTimeChanged(float time)
        {
            var totalSeconds = Mathf.FloorToInt(time);
            var numMinutes = totalSeconds / 60;
            var numMinutesFull = $"{(numMinutes < 10 ? "0" : "")}{numMinutes}";
            var numSeconds = totalSeconds % 60;
            var numSecondsFull = $"{(numSeconds < 10 ? "0" : "")}{numSeconds}";

            _timerText.text = $"<mspace=0.45em>{numMinutesFull}:{numSecondsFull}";
        }
    }
}