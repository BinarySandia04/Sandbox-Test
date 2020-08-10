using Godot;
using System;
using System.Collections.Generic;

public class GeneralScript : Node
{
	public bool pausedMovement = false;
	public bool inMenu = true;

	[Export] public string GameSpatialPath;
	[Export] public string EditorSpatialPath;

	[Export] public PackedScene EditorStartNode;
	
	private Spatial _GameSpatialNode, _EditorSpatialNode;
	
	public static GeneralScript Instance;
	
	public override void _EnterTree()
	{
		Instance = this;
		Utils.screenSize = OS.GetRealWindowSize();
	}

	public override void _Ready()
	{
		_GameSpatialNode = GetNode(GameSpatialPath) as Spatial;
		_EditorSpatialNode = GetNode(EditorSpatialPath) as Spatial;

		_GameSpatialNode.Visible = false;
		_EditorSpatialNode.Visible = false;

		Input.SetMouseMode(Input.MouseMode.Confined);
	}
	
	List<Type> omitClearTypes = new List<Type>(){typeof(CameraController), typeof(DirectionalLight), typeof(WorldEnvironment)};
	
	public void StartEditor()
	{
		foreach (Node node in _EditorSpatialNode.GetChildren())
		{
			bool clear = false;
			Type nodeType = node.GetType();
			GD.Print(nodeType);
			foreach (Type T in omitClearTypes)
			{
				if (T.Equals(nodeType)) clear = true;
			}
			
			if(!clear) node.QueueFree();
		}
		
		
		Node n = EditorStartNode.Instance();
		_EditorSpatialNode.AddChild(n);

		_EditorSpatialNode.Visible = true;
		pausedMovement = false;
		GD.Print(_EditorSpatialNode.GetChildren());
	}

	public override void _Input(InputEvent ev)
    {
	    Type t = ev.GetType();
	    if (t == typeof(InputEventKey))
	    {
		    InputEventKey inputKey = (InputEventKey) ev;
		    if (inputKey.Scancode == (int) KeyList.Escape)
		    {
			    if (pausedMovement)
			    {
				    Input.SetMouseMode(Input.MouseMode.Confined);
				    pausedMovement = false;
			    }
		    }
	    }

	    if (t == typeof(InputEventMouseButton))
	    {
		    InputEventMouseButton inputMouse = (InputEventMouseButton) ev;
		    if (inputMouse.IsPressed())
		    {
			    if (inputMouse.ButtonIndex == (int) ButtonList.Left && !inMenu)
			    {
				    if (!pausedMovement)
				    {
					    Input.SetMouseMode(Input.MouseMode.Captured);
					    pausedMovement = true;
				    }
			    }
		    }
		    
	    }
    }
}