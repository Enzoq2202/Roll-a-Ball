using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerTransform; 
    public float speed = 5.0f; 
    public float avoidanceRadius = 1.5f; 
    public LayerMask enemyLayer; 

    private Rigidbody rb; 
    private float fixedYPosition; 

    void Start()
    {
        fixedYPosition = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 direction = CalculateDirectionWithAvoidance();
            direction.y = 0; 
            direction.Normalize(); 
            Vector3 newVelocity = direction * speed;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
            if (newVelocity.magnitude > 0.1f)
            {
                float rotationScale = 0.02f; 
                float rotationMagnitude = newVelocity.magnitude * rotationScale;
                Vector3 rotationDirection = Vector3.Cross(Vector3.up, newVelocity).normalized;
                rb.angularVelocity = rotationDirection * rotationMagnitude * Mathf.Rad2Deg;
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
    }


    Vector3 CalculateDirectionWithAvoidance()
    {
        Vector3 directionToPlayer = new Vector3(playerTransform.position.x, fixedYPosition, playerTransform.position.z) - transform.position;

        Collider[] hits = Physics.OverlapSphere(transform.position, avoidanceRadius, enemyLayer);
        Vector3 avoidanceVector = Vector3.zero;
        foreach (var hit in hits)
        {
            if (hit.gameObject != gameObject) 
            {
                avoidanceVector += (transform.position - hit.transform.position);
            }
        }
        if (hits.Length > 1) 
        {
            directionToPlayer += avoidanceVector;
        }

        return directionToPlayer;
    }
}
