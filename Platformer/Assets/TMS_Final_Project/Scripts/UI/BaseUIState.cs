using Promenade.Scripts.GamePhysics.UI;
using UnityEngine;

namespace Promenade.Scripts.GamePhysics.UI
{
    public class BaseUIState : MonoBehaviour
    {
        protected UIManager _UIManager;

        public virtual void Init(UIManager manager)
        {
            _UIManager = manager;
        }

        public virtual void Enter()
        {
            gameObject.SetActive(true);
        }

        public virtual void Exit()
        {
            gameObject.SetActive(false);
        }
    }
}