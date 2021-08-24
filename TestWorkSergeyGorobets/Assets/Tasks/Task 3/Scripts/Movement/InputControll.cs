using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace TestWorkSergeyGorobets.Movement.Interfaces
{
    public class InputControll : MonoBehaviour
    {
        private GameInputActions gameInputActions;
        private InputAction inputAction;
        private Camera mainCamera;
        private IMove imove;

        [Header("Layer")]
        [SerializeField] private LayerMask layerMask;

        private void Awake() => Initialize();

        private void OnDisable()
        {
            inputAction.Disable();
        }

        private void Initialize()
        {
            gameInputActions = new GameInputActions();
            inputAction = gameInputActions.Player.Movement;
            inputAction.Enable();

            imove = GetComponent<IMove>();

            mainCamera = Camera.main;
        }


        private void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = 7.6f;
            Vector3 position = mainCamera.ScreenToWorldPoint(mousePosition);

            imove.Move(position);
        }
    }
}