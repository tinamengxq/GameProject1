using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    public static UIController Instance {get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        HideInteractPrompt();
        HideProgress();
        HideDialogue();
    }

    [Header("Interaction UI")]
    [SerializeField] private GameObject interactPanel;
    [SerializeField] private TMP_Text interactText;

    [Header("Progress UI")]
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private TMP_Text progressText;

    [Header("Quest UI")]
    [SerializeField] private TMP_Text questText;

    [Header("Bag UI")]
    [SerializeField] private TMP_Text bagText;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image speakerImage;
    [SerializeField] private TMP_Text dialogueText;

    private void Start()
    {
        Inventory.Instance.OnInventoryChanged += UpdateBagUI;
        Inventory.Instance.OnSelectionChanged += (i) => UpdateBagUISnapshot();

        QuestManager.Instance.OnQuestUpdated += (q, done) => UpdateQuestUI();

        UpdateQuestUI();
        UpdateBagUISnapshot();
    }

    // Interaction
    public void ShowInteractPrompt(string prompt)
    {
        interactPanel.SetActive(true);
        interactText.gameObject.SetActive(true);
        interactText.text = $"F - {prompt}";
    }

    public void HideInteractPrompt()
    {
        if (interactPanel != null)
        {
            interactText.gameObject.SetActive(false);
            interactPanel.SetActive(false);
        } 
    }

    // Progress
    public void ShowProgress(float remainingSeconds)
    {
        progressPanel.SetActive(true);
        progressText.text = remainingSeconds.ToString("0.00");
    }

    public void HideProgress()
    {
        if (progressPanel != null) progressPanel.SetActive(false);
    }

    // Quest
    private void UpdateQuestUI()
    {
        bool v = QuestManager.Instance.IsDone(QuestType.GrowVegetables);
        bool a = QuestManager.Instance.IsDone(QuestType.GrowAnimals);
        bool b = QuestManager.Instance.IsDone(QuestType.GrowBees);

        questText.text =
            $"{(v ? "✅" : "⬜")} Grow vegetables\n" +
            $"{(a ? "✅" : "⬜")} Grow animals\n" +
            $"{(b ? "✅" : "⬜")} Grow bees";
    }

    // Bag
    private void UpdateBagUI(ItemType[] items)
    {
        UpdateBagUISnapshot();
    }

    private void UpdateBagUISnapshot()
    {
        // simple text hotbar (fast to prototype)
        ItemType selected = Inventory.Instance.SelectedItem;

        bagText.text = $"Selected: {selected}\n" +
                       "1-5 to select";
    }

    // Dialogue
    public void ShowDialogue(Sprite speaker, string line)
    {
        dialoguePanel.SetActive(true);

    if (speakerImage != null)
    {
        speakerImage.gameObject.SetActive(speaker != null);
        if (speaker != null) speakerImage.sprite = speaker;
    }

        dialogueText.text = line;
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

}
