using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputCtr : MonoBehaviour
{
    public Camera boardCamera;
    public LayerMask draggableLayer;
    public float groundY = 0f; 
    public float dragOffsetZ = -1f;

    private BoardDraggable currentDraggable;

    void Awake()
    {
        if (boardCamera != null)
        {
            return;
        }

        boardCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryBeginDrag();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    void TryBeginDrag()
    {
        Ray ray = boardCamera.ScreenPointToRay(Input.mousePosition);
        bool RaycastHit = Physics.Raycast(ray, out RaycastHit hit, 1000f, draggableLayer);

        if (!RaycastHit)
        {
            return;
        }

        currentDraggable = hit.collider.GetComponentInParent<BoardDraggable>();

        if (currentDraggable != null)
        {
            currentDraggable.BeginDrag();
        }
    }

    void UpdateDrag()
    {
        if (currentDraggable == null)
        {
            return;
        }

        Vector3 vector3 = new Vector3(0f, 0f, dragOffsetZ);
        Vector3 groundPos = GetMouseGroundPos() + vector3;
        currentDraggable.DragTo(groundPos);
    }

    void EndDrag()
    {
        if (currentDraggable == null)
        {
            return;
        }

        currentDraggable.EndDrag();
        currentDraggable = null;
    }

    private Vector3 GetMouseGroundPos()
    {
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, groundY, 0f));
        Ray ray = boardCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        
        return Vector3.zero;
    }
}
