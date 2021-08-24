using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TestWorkSergeyGorobets.Obstacles;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    [Header("Movement")] [Range(2f, 8f)]
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool canMove = false;

    [Header("Child Sphere To Rotate")]
    [SerializeField] private Transform childSphere;

    [Header("Cashed Components")]
    [SerializeField] private Rigidbody rigidbodyObject;
    [SerializeField] private SphereCollider sphereCollider;

    [Header("Calculated Angles")]
    [SerializeField] private Vector3 angleVector;

    [Header("Paddel Ref")]
    [SerializeField] private Paddle paddle;

    private void Awake() => Initialize();

    private void Start()
    {
        GetStartRandomAngle();
    }

    private void FixedUpdate()
    {
        if((Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed) && !canMove)
        {
            canMove = true;

            StartCoroutine(IncreaseSpeed(2f, 8f, 25f));
        }

        if(!canMove)
        {
            LockBallToPaddle();
        }
        else
        {
            MoveBall();
        }
    }

    private void Initialize()
    {
        rigidbodyObject = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        paddle = FindObjectOfType<Paddle>();

        canMove = false;
    }

    private void LockBallToPaddle()
    {
        transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y, paddle.transform.position.z + paddle.GetPaddleSize().z);
    }

    private IEnumerator IncreaseSpeed(float speedStart, float maxSpeed, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration )
        {
            speed = Mathf.Lerp( speedStart, maxSpeed, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }

        speed = maxSpeed;
    }

    private void MoveBall()
    {
        Vector3 direction = rigidbodyObject.position + angleVector * (Time.deltaTime * speed);
        direction.y = sphereCollider.radius;
        rigidbodyObject.MovePosition(direction);

        transform.LookAt(direction);

        childSphere.Rotate(Vector3.right * (speed * sphereCollider.radius * 2 * Mathf.PI), Space.Self);
    }

    private void GetStartRandomAngle()
    {
        float angle = UnityEngine.Random.Range(75f, 105f);
        angle *= Mathf.Deg2Rad;
        angleVector.x = Mathf.Cos(angle);
        angleVector.z = Mathf.Sin(angle);
    }

    private void GetCollisionAngle(Vector3 point)
    {
        angleVector = Vector3.Reflect(transform.forward, point);
        angleVector.y = sphereCollider.radius;
    }

    private void GetPaddelCollisionAngle(Vector3 point, Vector3 center)
    {
        //center
        angleVector = Vector3.Reflect(transform.forward, point);
        angleVector.y = sphereCollider.radius;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!canMove) { return; }

        if(other.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            GetCollisionAngle(other.contacts[0].normal);
        }

        if(other.gameObject.TryGetComponent<Paddle>(out Paddle paddleContact))
        {
            GetPaddelCollisionAngle(other.contacts[0].normal, paddleContact.GetCenterPointCollider().normalized);
        }
    }
}