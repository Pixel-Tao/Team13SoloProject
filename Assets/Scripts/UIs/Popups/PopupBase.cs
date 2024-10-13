

public class PopupBase : UIBase
{
    public bool IsOpen { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Show()
    {
        base.Show();
        IsOpen = true;
    }
    public override void Close()
    {
        base.Close();
        IsOpen = false;
    }
}