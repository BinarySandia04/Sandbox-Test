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
        UIManager.Instance.HideMainMenuWindows();
        
        GeneralScript.Instance.StartEditor();
        GeneralScript.Instance.inMenu = false;
        
        UIManager.Instance.EditorMenu.Show();
        GeneralScript.Instance.mouseHoveringUI = false;
        
        HirearchyEditorWindow.UpdateHirearchy();
    }
}
