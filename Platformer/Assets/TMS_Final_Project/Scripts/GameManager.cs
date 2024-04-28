using Platformer.Common;
using Platformer.Events;
using Platformer.Pickables;
using Platformer.Player;
using UnityEditor;
using UnityEngine;
using System;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private PlayerController _player;

        [SerializeField]
        private IntEventChannel _scoreChannel;

        [SerializeField]
        private IntEventChannel _numLivesChannel;
        public static event Action OnCurrentCherryChanged;

        private int _score;


        private void Start()
        {
            if (_camera.TryGetComponent<Follower>(out var follower))
            {
                follower.Init(_player.transform);
            }

            _player.OnPickableCollected += OnPickableCollected;
        }

        private void OnDestroy()
        {
            _player.OnPickableCollected -= OnPickableCollected;
        }

        private void OnPickableCollected(IPickable pickable)
        {
            if (pickable == null)
            {
                return;
            }

            if (pickable is CherryOfInvulnerability)
            {
                pickable.Pick(_player.gameObject);
                SoundManager.Sound_Manager.PlayPickCherrySound();
                _score += pickable.ScoreIncrement;
                OnCurrentCherryChanged?.Invoke();
            }
            else
            {
                pickable.Pick(_player.gameObject);
                SoundManager.Sound_Manager.PlayPickGemSound();
                _score += pickable.ScoreIncrement;
            }

            _scoreChannel.Publish(_score);
        }
    }
}