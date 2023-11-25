using System;
using Platformer.Player;
using UnityEngine;

namespace Platformer.Pickables
{
    public class Gem : MonoBehaviour, IPickable
    {
        [SerializeField]
        private int _scoreIncrement;

        public int ScoreIncrement => _scoreIncrement;


        public void Pick(GameObject picker)
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Gem");
            var playerController = other.gameObject.GetComponentInParent<PlayerController>();
            if (other.tag == "Player")
            {
                playerController.Pick(this);
            }
        }
    }
}