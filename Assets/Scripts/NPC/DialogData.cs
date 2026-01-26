[System.Serializable]
public class DialogOption
{
    public string text;
    public int nextIndex;
    public DialogAction action;
    public InventoryItem itemToGive;
}

public enum DialogAction
{
    None,
    OpenShop,
    GiveItem
}

[System.Serializable]
public class DialogNode
{
    public string text;
    public DialogOption[] options;
}
