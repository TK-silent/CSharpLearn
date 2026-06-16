using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDraggable : MonoBehaviour
{
    public float dragHeight = 1.2f; //拖动时悬浮高度
    public float moveSmoothSpeed = 20f; //平滑跟随的速度

    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 targetPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void BeginDrag()
    {
        isDragging = true;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public void DragTo(Vector3 groundPos)
    {
        targetPosition = groundPos + Vector3.up * dragHeight;
    }

    public void EndDrag()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        
    }

    void Update()
    {
        if (isDragging == false)
        {
            return;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            moveSmoothSpeed * Time.deltaTime
        );
    }
}
