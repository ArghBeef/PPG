using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance;

    public GameObject dialogPanel;
    public TMP_Text npcNameText;
    public TMP_Text dialogText;
    public Transform optionsParent;
    public Button optionPrefab;
    public bool IsOpen { get; private set; }

    NPCDialog current;
    int index;

    void Awake()
    {
        Instance = this;
        dialogPanel.SetActive(false);
    }

    public void StartDialog(NPCDialog npc)
    {
        current = npc;
        index = 0;
        dialogPanel.SetActive(true);
        IsOpen = true;
        Show();
    }

    void Show()
    {
        DialogNode node = current.dialog[index];
        npcNameText.text = current.npcName;
        dialogText.text = node.text;

        foreach (Transform c in optionsParent)
            Destroy(c.gameObject);

        foreach (var opt in node.options)
        {
            Button b = Instantiate(optionPrefab, optionsParent);
            b.GetComponentInChildren<TMP_Text>().text = opt.text;
            b.onClick.AddListener(() => Choose(opt));
        }
    }

    void Choose(DialogOption opt)
    {
        if (opt.action == DialogAction.OpenShop)
            FindObjectOfType<ShopUI>().OpenShop();

        if (opt.action == DialogAction.GiveItem && opt.itemToGive != null)
        {
            Inventory inv = FindObjectOfType<Inventory>();
            inv.AddItem(opt.itemToGive);
        }

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

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
        IsOpen = false;
        current = null;
    }
}
