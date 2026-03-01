using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHouse : MonoBehaviour, IInteractable
{
    [SerializeField]private float honeySeconds = 6f;

    private ProgressTimer timer = new ProgressTimer();
    private bool producing;

    private void Awake()
    {
        timer.OnTick += (t) => UIController.Instance.ShowProgress(t);
        timer.OnFinished += () =>
        {
            UIController.Instance.HideProgress();
            producing = false;

            Inventory.Instance.AddItem(ItemType.Honey);
            QuestManager.Instance.Complete(QuestType.GrowBees);
        };
    }
        private void Update()
    {
        timer.Update(Time.deltaTime);
    }

    public string GetPrompt()
    {
        if (producing) return "Bees working...";
        ItemType held = Inventory.Instance.SelectedItem;
        return held == ItemType.Water ? "Support Bees (F)" : "Need Water";
    }

    public void Interact()
    {
        if (producing) return;

        if (Inventory.Instance.SelectedItem != ItemType.Water) return;
        if (!Inventory.Instance.ConsumeSelected()) return;

        producing = true;
        timer.Start(honeySeconds);
    }

}
