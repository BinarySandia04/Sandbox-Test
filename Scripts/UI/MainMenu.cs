using System.Net.Sockets;
using Godot;

using Array = Godot.Collections.Array;

public class MainMenu : Control
{
    [Export] public string containerNodeName;

    [Export] public PackedScene OptionsMenu;
    [Export] public PackedScene WorkshopMenu;
    [Export] public PackedScene PlayMenu;
    [Export] public PackedScene EditorMenu;
    
    private Node containerNode;

    public static MainMenu Instance;
    
    private Utils.Window optionsWindow;
    private Utils.Window workshopWindow;
    private Utils.Window playWindow;
    private Utils.Window editorWindow;

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
        GD.Print("Ready");
        // Declare main menu static windows
        
        optionsWindow = new Utils.Window(this, "Options", true, OptionsMenu.Instance(), Utils.screenSize / 2, new Vector2(700, 500));
        workshopWindow = new Utils.Window(this, "Workshop", true,WorkshopMenu.Instance(),  Utils.screenSize / 2, new Vector2(1000, 700));
        playWindow = new Utils.Window(this, "Play", true,PlayMenu.Instance(),  Utils.screenSize / 2, new Vector2(500, 700));
        editorWindow = new Utils.Window(this, "Level Editor", true,EditorMenu.Instance(),  Utils.screenSize / 2, new Vector2(500, 700));

        
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
                playWindow.Show();
                break;
            case "EDITOR":
                editorWindow.Show();
                break;
            case "OPTIONS":
                optionsWindow.Show();
                break;
            case "WORKSHOP":
                workshopWindow.Show();
                break;
        }
    }
}
