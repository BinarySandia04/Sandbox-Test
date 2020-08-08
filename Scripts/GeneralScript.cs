using Godot;
using System;

public class GeneralScript : Node
{
	public bool mouseConfined = true;
	
    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public override void _Input(InputEvent ev)
    {
	    Type t = ev.GetType();
	    if (t == typeof(InputEventKey))
	    {
		    InputEventKey inputKey = (InputEventKey) ev;
		    if (inputKey.Scancode == (int) KeyList.Escape)
		    {
			    if (mouseConfined)
			    {
				    Input.SetMouseMode(Input.MouseMode.Visible);
				    mouseConfined = false;
			    }
		    }
	    }

	    if (t == typeof(InputEventMouseButton))
	    {
		    InputEventMouseButton inputMouse = (InputEventMouseButton) ev;
		    if (inputMouse.ButtonIndex == (int) ButtonList.Left && inputMouse.IsPressed())
		    {
			    if (!mouseConfined)
			    {
				    Input.SetMouseMode(Input.MouseMode.Captured);
				    mouseConfined = true;
			    }
		    }
	    }
    }
}