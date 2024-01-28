
using MJW.Player;
using MJW.Simon;
using MJW.Utils;
using UnityEngine;

namespace MJW.Dance
{
    public class DanceManager : Singleton<DanceManager>
    {
        private DanceType _lastDance;

        [SerializeField] private PlayerBase _player_1;
        [SerializeField] private PlayerBase _player_2;

        private void Awake()
        {
            SimonInputListener.OnInputDetected += OnInputDetected;
        }

        private void OnDestroy()
        {
            SimonInputListener.OnInputDetected -= OnInputDetected;
        }

        private void OnInputDetected(ButtonType button)
        {
            var player = GetPlayer(button);
            IncrementDance();
            var danceId = _lastDance;

            if (player == PlayerType.Player_1)
            {
                _player_1.TriggerDance(danceId);
            }
            else
            {
                _player_2.TriggerDance(danceId);
            }
        }

        private DanceType GetDance()
        {
            return (_lastDance == DanceType.undefined ? DanceType.dance_1 : DanceType.dance_2);
        }

        private void IncrementDance()
        {
            if (_lastDance == DanceType.undefined || _lastDance == DanceType.dance_2)
            {
                _lastDance = DanceType.dance_1;
            }
            else
            {
                _lastDance = DanceType.dance_2;
            }
        }
        
        private PlayerType GetPlayer(ButtonType button)
        {
            switch (button)
            {
                case ButtonType.Left_Down:
                case ButtonType.Left_Left:
                case ButtonType.Left_Up:
                case ButtonType.Left_Right:
                    return PlayerType.Player_1;

                case ButtonType.Right_Down:
                case ButtonType.Right_Left:
                case ButtonType.Right_Right:
                case ButtonType.Right_Up:
                    return PlayerType.Player_2;
            }

            return PlayerType.Undefined;
        }
    }
}

public enum DanceType
{
    undefined, dance_1, dance_2
}