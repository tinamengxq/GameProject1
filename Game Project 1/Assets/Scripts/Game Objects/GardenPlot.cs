using System.Collections;
using System.Collections.Generic;
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

    private PlotState state = PlotState.Empty;
    private ProgressTimer timer = new ProgressTimer();

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
        };
    }

    private void Update()
    {
        timer.Update(Time.deltaTime);
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
            return "Growing...";
        }

        if (state == PlotState.Ready)
        {
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
            return;
        }

        if (state == PlotState.Planted)
        {
            if (held != ItemType.Water) return;
            if (!Inventory.Instance.ConsumeSelected()) return;

            state = PlotState.Growing;
            timer.Start(growSeconds);
            return;
        }

        if (state == PlotState.Ready)
        {
            bool added = Inventory.Instance.AddItem(ItemType.Vegetable);
            if (!added) return;

            AudioManager.Instance.PlayHarvest();
            QuestManager.Instance.Complete(QuestType.GrowVegetables);

            state = PlotState.Empty;
            return;
        }
    }

}
