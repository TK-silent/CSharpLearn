using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItemTimer
{
    public CarryItem item;
    public float soldTimer = 0f;
}

public class SellZone : MonoBehaviour
{
    public float soldTime = 3f;
    public CarryItem coinPrefab;
    public Transform coinSpawnPos;
    
    private List<SellItemTimer> itemInZone = new List<SellItemTimer>();

    void Update()
    {
        for (int i = itemInZone.Count - 1; i >= 0; i--)
        {
            SellItemTimer sellItem = itemInZone[i];

            if (sellItem.item == null)
            {
                itemInZone.RemoveAt(i);
                return;
            }

            sellItem.soldTimer += Time.deltaTime;

            if (sellItem.soldTimer >= soldTime)
            {
                Destroy(sellItem.item.gameObject);
                SpawnCoin();
                itemInZone.RemoveAt(i);
                Debug.Log("一个作物出售成功!");
            }
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null )
        {
            return;
        }

        Vector3 spawnPos = transform.position + Vector3.up * 1f;

        if (coinSpawnPos != null)
        {
            spawnPos = coinSpawnPos.position;
        }

        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        CarryItem carryItem = other.GetComponent<CarryItem>();

        if (carryItem == null || carryItem.itemType != ItemType.Crop)
        {
            return;            
        }

        for (int i = 0; i < itemInZone.Count; i++)
        {
            if (itemInZone[i].item == carryItem)
            {
                return;
            }
        }

        SellItemTimer sellItem = new SellItemTimer();
        sellItem.item = carryItem;
        sellItem.soldTimer = 0f;

        itemInZone.Add(sellItem);
    }

    void OnTriggerExit(Collider other)
    {
        CarryItem carryItem = other.GetComponent<CarryItem>();

        if (carryItem == null)
        {
            return;
        }

        for (int i = itemInZone.Count - 1; i >= 0; i--)
        {
            if (itemInZone[i].item == carryItem)
            {
                itemInZone.RemoveAt(i);
            }
        }
    }
}
