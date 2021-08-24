using UnityEngine;
using System;

namespace TestWorkSergeyGorobets.Obstacles
{
    public class BackObstacle : Obstacle
    {
        [SerializeField] public event Action OnObjectCollided;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent(out BallController ballController))
            {
                OnObjectCollided?.Invoke();
            }
        }
    }
}