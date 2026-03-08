using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum QuestType
{
    GrowVegetables,
    GrowAnimals,
    GrowBees
}
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private Dictionary<QuestType, bool> questDone = new Dictionary<QuestType, bool>();

    public event Action<QuestType, bool> OnQuestUpdated;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        questDone[QuestType.GrowVegetables] = false;
        questDone[QuestType.GrowAnimals] = false;
        questDone[QuestType.GrowBees] = false;
    }

    public bool IsDone(QuestType quest) => questDone.ContainsKey(quest) && questDone[quest];

    public void Complete(QuestType quest)
    {
        if (!questDone.ContainsKey(quest)) return;
        if (questDone[quest]) return;

        questDone[quest] = true;
        OnQuestUpdated?.Invoke(quest, true);

        AudioManager.Instance.PlayQuestCheck();
    }

    public void GrowVegetables()
    {
        if(questDone[QuestType.GrowVegetables] == true)
        {
            return;
        }

        if (Inventory.Instance.HasItem(ItemType.Vegetable))
        {
            questDone[QuestType.GrowVegetables] = true;
            OnQuestUpdated?.Invoke(QuestType.GrowVegetables, true);
            IsDone(QuestType.GrowVegetables);
        }
    }

    public void GrowBees()
    {
        if (questDone[QuestType.GrowBees] == true)
        {
            return;
        }

        if (Inventory.Instance.HasItem(ItemType.Honey))
        {
            questDone[QuestType.GrowBees] = true;
            OnQuestUpdated?.Invoke(QuestType.GrowBees, true);
            IsDone(QuestType.GrowBees);
        }
    }

    public void GrowAnimals()
    {
        if (questDone[QuestType.GrowAnimals] == true)
        {
            return;
        }

        if (!Inventory.Instance.HasItem(ItemType.AnimalFood))
        {
            questDone[QuestType.GrowAnimals] = true;
            OnQuestUpdated?.Invoke(QuestType.GrowAnimals, true);
            IsDone(QuestType.GrowAnimals);
        }
    }

}
