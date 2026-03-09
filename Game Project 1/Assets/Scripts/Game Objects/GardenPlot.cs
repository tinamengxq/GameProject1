using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlotState
{
    Empty,
    Planted,
    Growing,
    Ready
}

public class GardenPlot : MonoBehaviour, IInteractable
{
    [SerializeField] private float growSeconds = 5f;
    [SerializeField] private Sprite plantSprite;
    [SerializeField] private Sprite grownSprite;
    [SerializeField] private SpriteRenderer plotRenderer;
    [SerializeField] private Transform playerInteractionPoint;

    private PlotState state = PlotState.Empty;
    private ProgressTimer timer = new ProgressTimer();
    //private SpriteRenderer currentSprite;

    private void Awake()
    {
        timer.OnTick += (t) =>
        {
            UIController.Instance.ShowProgress(t);
        };
        timer.OnFinished += () =>
        {
            UIController.Instance.HideProgress();
            state = PlotState.Ready;
            plotRenderer.sprite = grownSprite;
            plotRenderer.sortingLayerName = "Player";
            //plotRenderer.transform.localScale *= 5f;
            plotRenderer.sortingOrder = 5;
        };
    }

    private void Update()
    {
        timer.Update(Time.deltaTime);
        if(state == PlotState.Ready && Input.GetKeyDown(KeyCode.F))
        {
            //plotRenderer.enabled = false;
        }
    }

    public string GetPrompt()
    {
        ItemType held = Inventory.Instance.SelectedItem;

        if (state == PlotState.Empty)
        {
            return held == ItemType.VegetableSeeds ? "Plant (F)" : "Need Seeds";
        }

        if (state == PlotState.Planted)
        {
            return held == ItemType.Water ? "Water (F)" : "Need Water";
        }

        if (state == PlotState.Growing)
        {
            //sprite = plantSprite;
            //Instantiate(sprite, playerInteractionPoint.position + new Vector3 (-1,0,0), Quaternion.identity, transform);
            return "Growing...";
        }

        if (state == PlotState.Ready)
        {
            //sprite = grownSprite;
            return "Harvest (F)";
        }

        return "Interact (F)";
    }

    public void Interact()
    {
        ItemType held = Inventory.Instance.SelectedItem;

        if (state == PlotState.Empty)
        {
            if (held != ItemType.VegetableSeeds) return;
            if (!Inventory.Instance.ConsumeSelected()) return;

            state = PlotState.Planted;
            plotRenderer.sprite = plantSprite;
            plotRenderer.sortingLayerName = "Player";
            plotRenderer.transform.localScale *= 5f;
            plotRenderer.sortingOrder = 5;
            return;
        }

        if (state == PlotState.Planted)
        {
            if (held != ItemType.Water) return;
            if (!Inventory.Instance.ConsumeSelected()) return;

            state = PlotState.Growing;
            timer.Start(growSeconds);
            plotRenderer.sprite = plantSprite;
            plotRenderer.sortingLayerName = "Player";
            //plotRenderer.transform.localScale *= 5f;
            plotRenderer.sortingOrder = 5;
            return;
        }

        if (state == PlotState.Ready)
        {
            bool added = Inventory.Instance.AddItem(ItemType.Vegetable);
            if (!added) return;

            AudioManager.Instance.PlayHarvest();
            QuestManager.Instance.Complete(QuestType.GrowVegetables);

            state = PlotState.Empty;
            plotRenderer.sprite = null;

            //plotRenderer.sprite = plantSprite;
            //plotRenderer.sortingLayerName = "Player";
            //plotRenderer.sortingOrder = 5;
            return;
        }
    }

}
