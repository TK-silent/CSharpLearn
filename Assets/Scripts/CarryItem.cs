using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public enum ItemType
{
    Hoe,
    SeedBag,
    WaterCan,
    Crop,
    Coin
}

public class CarryItem : MonoBehaviour,IInteractable
{
    public string itemName = "item";
    public ItemType itemType;

    private Rigidbody rb;
    private Collider itemCollider;
    private Transform originParent;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        itemCollider = GetComponent<Collider>();
        originParent = transform.parent;
    }

    public void Interact(PlayerCarry playerCarry)
    {
        Debug.Log("与" + itemName + "交互");
    }

    public void OnPickedUp(Transform holdPoint)
    {
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }
    }

    public void OnDroped(Vector3 dropPosition)
    {
        transform.SetParent(originParent);
        transform.position = dropPosition;

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
    }
}
