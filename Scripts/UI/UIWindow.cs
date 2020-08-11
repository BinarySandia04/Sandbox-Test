using Godot;
using System;

public class UIWindow : Control
{
    [Export] private string draggabbleContainerPath;
    [Export] private string closeButtonPath;
    [Export] private string windowContainerPath;
    
    private PanelContainer draggableContainer;
    private Button closeButton;
    private Control windowContainer;
    
    private bool enteredWindow = false;
    private bool leftClickPressed = false;

    [Signal]
    delegate void close_button();

    private GeneralScript generalScript;
    
    public override void _Ready()
    {
        generalScript = GeneralScript.Instance;

        GD.PrintErr("Window ready");
        
        draggableContainer = GetNode("./" + draggabbleContainerPath) as PanelContainer;
        closeButton = GetNode("./" + closeButtonPath) as Button;
        windowContainer = GetNode("./" + windowContainerPath) as Control;


        if (draggableContainer is null)
        {
            GD.PrintErr("No hay dragabble container!");
            return;
        }
        
        draggableContainer.Connect("mouse_entered", this, nameof(MouseEnteredDrag));
        draggableContainer.Connect("mouse_exited", this, nameof(MouseExitedDrag));


        closeButton.Connect("pressed", this, nameof(CloseWindow));
    }

    public void CloseWindow()
    {
        EmitSignal(nameof(close_button));
    }
    
    private void MouseEnteredDrag()
    {
        enteredWindow = true;
    }

    private void MouseExitedDrag()
    {
        enteredWindow = false;
    }

    private Vector2 originalLeftTop, originalRightBottom, originalMousePosition;
    
    public override void _Input(InputEvent ev)
    {
        if (ev.GetType() == typeof(InputEventMouseButton))
        {
            InputEventMouseButton inputMouse = (InputEventMouseButton) ev;
            if (inputMouse.ButtonIndex == (int) ButtonList.Left)
            {
                if (inputMouse.IsPressed())
                {
                   leftClickPressed = true;
                   originalLeftTop = new Vector2(MarginLeft, MarginTop);
                   originalRightBottom = new Vector2(MarginRight, MarginBottom);
                   originalMousePosition = inputMouse.Position;
                   
                   if(enteredWindow) GetParent().MoveChild(this, GetParent().GetChildCount());
                }
                else
                {
                    leftClickPressed = false;
                }
            }
        }
        
        if (ev.GetType() == typeof(InputEventMouseMotion))
        {
            // Cogemos el evento de mover el raton
            InputEventMouseMotion mousevent = (InputEventMouseMotion) ev;
            if (enteredWindow && leftClickPressed)
            {
                Vector2 pos = mousevent.Position;
                
                MarginLeft = (pos.x - originalMousePosition.x) + originalLeftTop.x;
                MarginRight = (pos.x - originalMousePosition.x) + originalRightBottom.x;
                
                MarginTop = (pos.y - originalMousePosition.y) + originalLeftTop.y;
                MarginBottom = (pos.y - originalMousePosition.y) + originalRightBottom.y;
            }
        }
    }
}
