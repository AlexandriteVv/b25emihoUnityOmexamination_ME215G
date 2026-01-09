using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol2D : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement")]
    public float speed = 2f;
    public float switchDistance = 0.5f;

    [Header("Optional Animation")]
    public Animator animator;
    public string runBoolName = "isRunning";

    private Rigidbody2D rb;
    private Transform currentPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator != null)
            animator.SetBool(runBoolName, true);
    }

    void Start()
    {
        currentPoint = pointB;
    }

    void FixedUpdate()
    {
        Move();
        CheckSwitchPoint();
    }

    void Move()
    {
        float direction = currentPoint == pointB ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void CheckSwitchPoint()
    {
        if (Vector2.Distance(transform.position, currentPoint.position) <= switchDistance)
        {
            currentPoint = currentPoint == pointB ? pointA : pointB;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}