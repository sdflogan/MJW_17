
using MJW.Player.Inputs;
using Rewired;
using UnityEngine;

namespace MJW.Player
{
    public class PlayerInput_2 : IPlayerInput
    {
        public float GetAxisHorizontal()
        {
            return ReInput.players.GetPlayer(0).GetAxis("Move Horizontal Right");
        }

        public float GetAxisVertical()
        {
            return ReInput.players.GetPlayer(0).GetAxis("Move Vertical Right");
        }

        public bool GetInteractButton()
        {
            return ReInput.players.GetPlayer(0).GetButtonDown("Action Right");
        }
    }
}