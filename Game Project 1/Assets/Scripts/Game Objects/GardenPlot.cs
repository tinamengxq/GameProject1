using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlotState
{
    Empty,
    Planted,
    Watered,
    Grown
}

public class GardenPlot : MonoBehaviour
{
    [Header("Growth")]
    public float growTimeSeconds = 6f;
    public int waterNeeded = 1;

    [Header("Harvest")]
    public int harvestYield = 1;

    [Header("Visuals")]
    public SpriteRenderer _spriteRenderer;
    public Sprite _empty;
    public Sprite _planted;
    public Sprite _watered;
    public Sprite _grown;

    private PlotState plotState = PlotState.Empty;
    private float growTimer = 0f;

    private void Update()
    {
        if(plotState == PlotState.Watered)
        {
            growTimer += Time.deltaTime;
            if(growTimer >= growTimeSeconds)
            {
                plotState = PlotState.Grown;
                UpdateVisual();
            }
        }
    }

    public void Interact(Player player)
    {
        switch (plotState)
        {
            case PlotState.Empty:
                TryPlant(player);
                break;
            
            case PlotState.Planted:
                TryWater(player);
                break;
            
            case PlotState.Watered:
                break;
            
            case PlotState.Grown:
                Harvest(player);
                break;
        }
    }

    public string GetPrompt(Player player)
    {
        switch (plotState)
        {
            case PlotState.Empty:
                return player.inventory.seeds > 0 ? "Press F to plant Seed" : "Need Seeds";
            
            case PlotState.Planted:
                if (!player.inventory.hasWaterCan)
                {
                    return "Need a Watering Can";
                }
                return player.inventory.CanWater(waterNeeded) ? "Press F to water" : "Need Water (fill at source)";
            
            case PlotState.Watered:
                float timeLeft = Mathf.Max(0f, growTimeSeconds - growTimer);
                return $"Growing ... ({timeLeft}s)";
            
            case PlotState.Grown:
                return "Press F to harvest Flower";

        }
        return "";
    }

    public void TryPlant(Player player)
    {
        player.inventory.seeds -= 1;
        plotState = PlotState.Planted;
        growTimer = 0f;
        UpdateVisual();
    }

    public void TryWater(Player player)
    {
        player.inventory.UseWater(waterNeeded);
        plotState = PlotState.Watered;
        growTimer = 0f;
        UpdateVisual();
    }

    public void Harvest(Player player)
    {
        Debug.Log($"Harvested {harvestYield} flower(s)!");
        plotState = PlotState.Empty;
        growTimer = 0f;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        switch (plotState)
        {
            case (PlotState.Empty):
                _spriteRenderer.sprite = _empty;
                break;
            
            case (PlotState.Planted):
                _spriteRenderer.sprite = _planted;
                break;
            
            case (PlotState.Watered):
                _spriteRenderer.sprite = _watered;
                break;
            
            case (PlotState.Grown):
                _spriteRenderer.sprite = _grown;
                break;
        }
    }



}
