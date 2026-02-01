using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public string npcName;
    public DialogNode[] dialog;

    public Shop npcShop;

    NPCBase wander;

    void Awake()
    {
        wander = GetComponent<NPCBase>();
    }

    public void Interact()
    {
        wander?.Pause();
        DialogSystem.Instance.StartDialog(this);
    }

    public void OnDialogClosed()
    {
        wander?.Resume();
    }
}
