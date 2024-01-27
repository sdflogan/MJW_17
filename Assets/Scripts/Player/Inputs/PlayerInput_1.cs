
using MJW.Player.Inputs;
using Rewired;
using UnityEngine;

namespace MJW.Player
{
    public class PlayerInput_1 : IPlayerInput
    {
        public float GetAxisHorizontal()
        {
            return ReInput.players.GetPlayer(0).GetAxis("Move Vertical Left");
        }

        public float GetAxisVertical()
        {
            return ReInput.players.GetPlayer(0).GetAxis("Move Horizontal Left");
        }

        public bool GetInteractButton()
        {
            return ReInput.players.GetPlayer(0).GetButtonDown("Action Left");
        }
    }
}