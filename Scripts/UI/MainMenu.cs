using System.Net.Sockets;
using Godot;

using Array = Godot.Collections.Array;

public class MainMenu : Control
{
    private Node containerNode;

    public static MainMenu Instance;
    
    [Export] public string containerNodeName;
    
    private string myPath;

    public bool shown;

    public void SetVisible(bool shown)
    {
        this.shown = shown;
        if (shown) Show();
        else Hide();
    }
    
    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        myPath = GetPath() + "/";        
        
        containerNode = GetNode(myPath + containerNodeName);
        // Declare main menu static windows
        
        
        
        foreach(Node obj in containerNode.GetChildren())
        {
            if (obj.GetClass() == "Button")
            {
                GD.Print(obj.GetPath());
                
                Array parameters = new Array();
                parameters.Add((string) obj.Get("command_name"));
                
                obj.Connect("pressed", this, nameof(_On_Button_Pressed), parameters);
            }
        }
    }

    private void _On_Button_Pressed(string command)
    {
        switch (command)
        {
            case "EXIT":
                GetTree().Quit();
                break;
            case "PLAY":
                UIManager.Instance.PlayWindow.Show();
                break;
            case "EDITOR":
                UIManager.Instance.EditorWindow.Show();
                break;
            case "OPTIONS":
                UIManager.Instance.OptionsWindow.Show();
                break;
            case "WORKSHOP":
                UIManager.Instance.WorkshopWindow.Show();
                break;
        }
    }
}
