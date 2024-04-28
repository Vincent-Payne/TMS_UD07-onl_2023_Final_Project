using Platformer.Events;
using UnityEngine;

namespace Platformer
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private IntEventChannel _eventChannel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _eventChannel.Publish((int)KeyCode.Escape);
            }
        }
    }
}