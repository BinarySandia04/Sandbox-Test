using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

public class MainMenu : Panel
{
    [Export] public string containerNodeName;

    private Node containerNode;
    
    public override void _Ready()
    {
        containerNode = GetNode(GetPath() + containerNodeName);
        
        
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
        GD.Print(command);
        if (command == "EXIT")
        {
            GetTree().Quit();
        }
    }
}
