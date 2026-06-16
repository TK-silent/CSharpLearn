using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum PlotState
{
    Empty,
    Tilled,
    Planted,
    Grown
}
public class FarmPlot : MonoBehaviour,IInteractable
{
    public PlotState state = PlotState.Empty;
    public Material emptyMaterial;
    public Material tilledMaterial;
    public GameObject seedLingObject;
    public GameObject grownCropObject;
    public float growTime = 20f;
    public float wateredDuration = 5f;
    public float wateredGrowMultiplier = 2f;
    public CarryItem cropItemprefab;

    private Renderer plotRenderer;
    private float growTimer;
    private float wateredTimer;

    void Awake()
    {
        plotRenderer = GetComponent<Renderer>();
        UpdateVisual();
    }

    void Update()
    {
        if (state != PlotState.Planted)
        {
            return;
        }

        float growthMultiplier = 1f;

        if (wateredTimer > 0f)
        {
            growthMultiplier = wateredGrowMultiplier;
            wateredTimer -= Time.deltaTime;
        }

        growTimer += Time.deltaTime * growthMultiplier;

        if (growTimer >= growTime)
        {
            Grown();
        }
    }

    public void Interact(PlayerCarry playerCarry)
    {
        CarryItem currentItem = playerCarry.GetCurrentItem();

        if (currentItem == null)
        {
            if (state == PlotState.Grown)
            {
                Harvest(playerCarry);
                return;
            }

            Debug.Log("你需要拿着工具或种子才能互动");
            return;
        }
        
        if (currentItem.itemType == ItemType.Hoe && state == PlotState.Empty)
        {
            state = PlotState.Tilled;
            UpdateVisual();
            Debug.Log("耕地成功");
            return;
        }

        if (currentItem.itemType == ItemType.SeedBag && state == PlotState.Tilled)
        {
            state = PlotState.Planted;
            growTimer = 0f;
            wateredTimer = 0f;
            UpdateVisual();
            Debug.Log("播种成功");
            return;
        }

        if (currentItem.itemType == ItemType.WaterCan && state ==PlotState.Planted)
        {
            wateredTimer = wateredDuration;
            Debug.Log("浇水成功");
            return;
        }

        Debug.Log("现在不能这样互动");

    }

    void Grown()
    {
        state = PlotState.Grown;
        UpdateVisual();
        Debug.Log("作物已成熟");
    }

    void Harvest(PlayerCarry playerCarry)
    {
        growTimer = 0f;
        UpdateVisual();

        if (cropItemprefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 3f;

            CarryItem carryItem = Instantiate
            (
                cropItemprefab, 
                spawnPosition, 
                Quaternion.identity
            );

            playerCarry.PickUp(carryItem);
        }

        state = PlotState.Tilled;
        UpdateVisual();
        Debug.Log("收获成功");

    }

    void UpdateVisual()
    {
        if (plotRenderer == null)
        {
            return;
        }

        if (state == PlotState.Empty)
        {
            plotRenderer.material = emptyMaterial;
        }
        else if(state == PlotState.Tilled || state == PlotState.Planted || state == PlotState.Grown)
        {
            plotRenderer.material = tilledMaterial;
        }

        if (seedLingObject != null)
        {
            seedLingObject.SetActive(state == PlotState.Planted);
        }

        if (grownCropObject != null)
        {
            grownCropObject.SetActive(state == PlotState.Grown);
        }
    }
}
