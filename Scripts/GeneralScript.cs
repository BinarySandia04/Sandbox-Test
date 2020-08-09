using Godot;
using System;

public class GeneralScript : Node
{
	public bool pausedMovement = false;
	public bool inMenu = true;

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
				    Input.SetMouseMode(Input.MouseMode.Visible);
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