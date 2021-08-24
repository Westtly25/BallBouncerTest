using UnityEngine;
using UnityEngine.InputSystem;

namespace TestWorkSergeyGorobets.Movement.Interfaces
{
    public class MovementController : MonoBehaviour, IMove
    {
        [SerializeField] private float offsetX = 6f;

        public void Move(Vector3 position)
        {
            Debug.Log(position);
            Vector3 movePos = new Vector3(Mathf.Clamp((position.x), -offsetX, offsetX ), transform.position.y, transform.position.z);
            transform.position = movePos;
        }
    }
}