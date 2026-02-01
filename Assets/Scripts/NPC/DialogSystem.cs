using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance;
    public bool IsOpen { get; private set; }

    public GameObject panel;
    public TMP_Text npcNameText;
    public TMP_Text dialogText;
    public Transform optionsParent;
    public Button optionPrefab;

    NPCDialog currentNPC;
    int index;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void StartDialog(NPCDialog npc)
    {
        currentNPC = npc;
        index = 0;

        npcNameText.text = npc.npcName;
        panel.SetActive(true);
        IsOpen = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Show();
    }

    void Show()
    {
        DialogNode node = currentNPC.dialog[index];
        dialogText.text = node.text;

        foreach (Transform c in optionsParent)
            Destroy(c.gameObject);

        foreach (DialogOption opt in node.options)
        {
            Button b = Instantiate(optionPrefab, optionsParent);
            TMP_Text t = b.GetComponentInChildren<TMP_Text>();
            t.text = opt.text;

            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() => Choose(opt));
        }
    }

    void Choose(DialogOption opt)
    {
        ExecuteAction(opt);

        if (opt.nextIndex >= 0)
        {
            index = opt.nextIndex;
            Show();
        }
        else
        {
            CloseDialog();
        }
    }

    void ExecuteAction(DialogOption opt)
    {
        switch (opt.action)
        {
            case DialogAction.OpenShop:
                OpenNPCShop();
                break;

            case DialogAction.GiveItem:
                if (opt.itemToGive != null)
                    FindFirstObjectByType<Inventory>().AddItem(opt.itemToGive);
                break;
        }
    }

    public void CloseDialog()
    {
        panel.SetActive(false);
        IsOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentNPC?.OnDialogClosed();
        currentNPC = null;
    }

    void OpenNPCShop()
    {
        CloseDialog();
        currentNPC.npcShop.Toggle();
    }
}
