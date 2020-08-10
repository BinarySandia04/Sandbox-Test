using Godot;
using System;

public class EditorMainMenu : Control
{
    [Export]
    public string CreateLevelNodePath;

    public override void _Ready()
    {
        (GetNode(CreateLevelNodePath) as Button)?.Connect("pressed", this, nameof(CreateLevel));
    }

    private void CreateLevel()
    {
        MainMenu.Instance.SetVisible(false);
        GeneralScript.Instance.StartEditor();
        GeneralScript.Instance.inMenu = false;
    }
}
