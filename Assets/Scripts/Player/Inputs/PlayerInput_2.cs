
using MJW.Player.Inputs;
using UnityEngine;

namespace MJW.Player
{
    public class PlayerInput_2 : IPlayerInput
    {
        public float GetAxisHorizontal()
        {
            return Input.GetAxis("Horizontal-2");
        }

        public float GetAxisVertical()
        {
            return Input.GetAxis("Vertical-2");
        }

        public bool GetInteractButton()
        {
            return Input.GetButtonDown("Fire2");
        }
    }
}