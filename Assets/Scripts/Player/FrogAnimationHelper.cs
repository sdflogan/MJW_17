
using MJW.Player;
using UnityEngine;

namespace MJW.Utils
{
    public class FrogAnimationHelper : MonoBehaviour
    {
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;

        [SerializeField] private ParticleSystem _stepFX;
        [SerializeField] private PlayerBase _player;

        public void StepLeft()
        {
            if (_player.CurrentSpeed > 0.1f)
            {
                SimplePool.SpawnComponent(_stepFX, _left).Play();
            }
        }

        public void StepRight()
        {
            if (_player.CurrentSpeed > 0.1f)
            {
                SimplePool.SpawnComponent(_stepFX, _right).Play();
            }
        }
    }
}