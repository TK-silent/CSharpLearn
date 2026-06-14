using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 moveInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        Vector3 moveOffset = moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveOffset);
    }
}