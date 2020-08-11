using Godot;
using System;

public class UIManager : Control
{
    public Utils.Window OptionsWindow;
    public Utils.Window WorkshopWindow;
    public Utils.Window PlayWindow;
    public Utils.Window EditorWindow;

    [Export] private PackedScene OptionsMainMenu;
    [Export] private PackedScene WorkshopMainMenu;
    [Export] private PackedScene PlayMainMenu;
    [Export] private PackedScene EditorMainMenu;

    [Export] private string EditorMenuPath;
    public Control EditorMenu;
    
    public static UIManager Instance;
    
    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        EditorMenu = GetNode(GetPath() + "/" + EditorMenuPath) as Control;
        
        OptionsWindow = new Utils.Window(this, "Options", true, OptionsMainMenu.Instance(), Utils.screenSize / 2, new Vector2(700, 500));
        WorkshopWindow = new Utils.Window(this, "Workshop", true,WorkshopMainMenu.Instance(),  Utils.screenSize / 2, new Vector2(1000, 700));
        PlayWindow = new Utils.Window(this, "Play", true,PlayMainMenu.Instance(),  Utils.screenSize / 2, new Vector2(500, 700));
        EditorWindow = new Utils.Window(this, "Level Editor", true,EditorMainMenu.Instance(),  Utils.screenSize / 2, new Vector2(500, 700));
    }

    public void HideMainMenuWindows()
    {
        OptionsWindow.Close();
        WorkshopWindow.Close();
        PlayWindow.Close();
        EditorWindow.Close();
    }
}
