using UnityEngine;

namespace Platformer.Pickables
{
    public interface IPickable
    {
        void Pick(GameObject picker);
        int ScoreIncrement { get; }
    }
}