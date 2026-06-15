using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShopItem : MonoBehaviour,IInteractable
{
    public CarryItem itemPrefab;
    public TextMeshPro priceText;
    public PaymentZone paymentZone;
    public Transform itemSpwanPoint;
    public int itemPrice;
    void Awake()
    {
        UpdatePriceText();
    }

    public void Interact(PlayerCarry playerCarry)
    {
        bool paid = paymentZone.TryUseCoin(itemPrice);
        
        if (!paid)
        {
            Debug.Log("付款区金币不足");
            return;
        }

        Vector3 spwanPos = transform.position + Vector3.up * 1f;

        if (itemSpwanPoint != null)
        {
            spwanPos = itemSpwanPoint.position;
        }

        Instantiate(itemPrefab, spwanPos, Quaternion.identity);
        Debug.Log("购买成功");
    }

    void UpdatePriceText()
    {
        if (priceText != null)
        {
            priceText.text = itemPrice.ToString();
        }
    }
}
