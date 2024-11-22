using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float transformRange = 2f;
    public Transform targetTransformableObject;
    public Camera mainCamera;
    
    private Rigidbody rb;
    private Vector2 moveInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed;
        Vector3 moveDirection = transform.TransformDirection(move);
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, targetTransformableObject.position) < transformRange)
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                TransformIntoObject();
            }
        }
    }

    private void TransformIntoObject()
    {
        mainCamera.transform.SetParent(targetTransformableObject);
        mainCamera.transform.localPosition = new Vector3(0, 4, -10);
        mainCamera.transform.LookAt(targetTransformableObject);

        if (!targetTransformableObject.gameObject.GetComponent<TableController>())
        {
            targetTransformableObject.gameObject.AddComponent<TableController>();
        }
        
        FindObjectOfType<EnemyMovement>().UpdateTarget(targetTransformableObject);

        enabled = false;
    }
}
