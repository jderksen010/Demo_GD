using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float stoppingDistance = 2f;

    private Rigidbody rb;
    public Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (currentTarget == null) return;

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        float distance = Vector3.Distance(currentTarget.position, transform.position);

        if (distance > stoppingDistance)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
    
    public void UpdateTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}