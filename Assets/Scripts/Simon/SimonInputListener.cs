
using DG.Tweening;
using MJW.Utils;
using Rewired;
using System;
using System.Collections;
using UnityEngine;

namespace MJW.Simon
{
    public class SimonInputListener : Singleton<SimonInputListener>
    {
        public static event Action<ButtonType> OnInputDetected;

        private Rewired.Player _player;
        private Coroutine _cooldownCoroutine;
        private float _currentCooldown;

        private void Awake()
        {
            _player = ReInput.players.GetPlayer(0);
        }

        private void Update()
        {
            if (IsReady())
            {
                if (_player.GetButtonDown("L Pad Up"))
                {
                    ButtonDetected(ButtonType.Left_Up);
                }
                else if (_player.GetButtonDown("L Pad Down"))
                {
                    ButtonDetected(ButtonType.Left_Down);
                }
                else if (_player.GetButtonDown("L Pad Left"))
                {
                    ButtonDetected(ButtonType.Left_Left);
                }
                else if (_player.GetButtonDown("L Pad Right"))
                {
                    ButtonDetected(ButtonType.Left_Right);
                }
                else if (_player.GetButtonDown("D Pad Up"))
                {
                    ButtonDetected(ButtonType.Right_Up);
                }
                else if (_player.GetButtonDown("D Pad Down"))
                {
                    ButtonDetected(ButtonType.Right_Down);
                }
                else if (_player.GetButtonDown("D Pad Left"))
                {
                    ButtonDetected(ButtonType.Right_Left);
                }
                else if (_player.GetButtonDown("D Pad Right"))
                {
                    ButtonDetected(ButtonType.Right_Right);
                }
            }
        }

        private void OnDestroy()
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
                _cooldownCoroutine = null;
            }
        }

        private bool IsReady()
        {
            return _currentCooldown <= 0f;
        }

        private void Cooldown(float waitSeconds)
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
            }

            _cooldownCoroutine = StartCoroutine(CooldownCoroutine(waitSeconds));
        }

        private IEnumerator CooldownCoroutine(float waitSeconds)
        {
            _currentCooldown = waitSeconds;

            while (_currentCooldown > 0)
            {
                yield return new WaitForSeconds(0.1f);
                _currentCooldown -= 0.1f;
            }

            _cooldownCoroutine = null;
        }

        private void ButtonDetected(ButtonType button)
        {
            Camera.main.DOShakePosition(0.1f, 0.5f).Play();
            OnInputDetected?.Invoke(button);
        }
    }
}