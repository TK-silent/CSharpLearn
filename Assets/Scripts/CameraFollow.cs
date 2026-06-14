using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 6.5f, -6.5f);
    public float smoothSpeed = 5f;

    // Start is called before the first frame update
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp
        (
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

    }
}
