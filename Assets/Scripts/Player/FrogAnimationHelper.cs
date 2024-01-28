
using MJW.Audio;
using MJW.Player;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Utils
{
    public class FrogAnimationHelper : MonoBehaviour
    {
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;

        [SerializeField] private ParticleSystem _stepFX;
        [SerializeField] private PlayerBase _player;

        [SerializeField] private List<SoundType> _steps;

        public void StepLeft()
        {
            if (_player.CurrentSpeed > 0.1f)
            {
                SimplePool.SpawnComponent(_stepFX, _left).Play();
                PlayRandomStep();
            }
        }

        public void StepRight()
        {
            if (_player.CurrentSpeed > 0.1f)
            {
                SimplePool.SpawnComponent(_stepFX, _right).Play();
                PlayRandomStep();
            }
        }

        private void PlayRandomStep()
        {
            int randomIndex = Random.Range(0, _steps.Count);
            AudioManager.Instance.PlaySFX(_steps[randomIndex], true);
        }
    }
}