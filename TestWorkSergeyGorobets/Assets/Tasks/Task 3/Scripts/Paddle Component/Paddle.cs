using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class Paddle : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] public event Action OnBallCollide;

    private void Awake() => boxCollider.GetComponent<BoxCollider>();

    public Vector3 GetPaddleSize()
    {
        return boxCollider.size;
    }

    public Vector3 GetCenterPointCollider()
    {
        return boxCollider.bounds.center;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out BallController ballController))
        {
            OnBallCollide?.Invoke();
        }
    }
}
