using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public string npcName;
    public DialogNode[] dialog;

    public void Interact()
    {
        DialogSystem.Instance.StartDialog(this);
    }
}
