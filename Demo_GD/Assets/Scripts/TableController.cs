using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TableController : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            TransformBackToPlayer();
        }
    }

    void TransformBackToPlayer()
    {
        playerMovement.enabled = true;
        playerMovement.mainCamera.transform.SetParent(playerMovement.transform);
        playerMovement.mainCamera.transform.localPosition = new Vector3(0, 4, -10);
        playerMovement.mainCamera.transform.LookAt(playerMovement.transform);
        
        FindObjectOfType<EnemyMovement>().UpdateTarget(playerMovement.transform);

        Destroy(this);
    }
}