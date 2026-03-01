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

}
