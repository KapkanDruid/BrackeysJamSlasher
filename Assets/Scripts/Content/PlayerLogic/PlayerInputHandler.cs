using Assets.Scripts.Architecture;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInputHandler
    {
        private readonly InputSystemActions _inputActions;

        public PlayerInputHandler(InputSystemActions inputActions)
        {
            _inputActions = inputActions;
            Debug.Log("sd");
        }
    }
}