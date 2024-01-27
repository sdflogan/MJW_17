
using MJW.Player.Inputs;
using UnityEngine;

namespace MJW.Player
{
    public class PlayerInput_1 : IPlayerInput
    {
        public float GetAxisHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }

        public float GetAxisVertical()
        {
            return Input.GetAxis("Vertical");
        }

        public bool GetInteractButton()
        {
            return Input.GetButtonDown("Fire1");
        }
    }
}