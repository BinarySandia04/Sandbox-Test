using Godot;
using System;

public class Utils
{
    public static Vector2 screenSize;

    public class Window : Godot.Object // Wat ns pq tiene que hacer esto
    {
        private static PackedScene windowResource = ResourceLoader.Load("res://Nodes/UIElements/UIWindow.tscn") as PackedScene;
        
        private bool unique;

        private UIWindow windowInstance;
        
        public Window(Node subNode, string name, bool unique, Node windowContent, Vector2 startPosition, Vector2 size)
        {
            windowInstance = windowResource.Instance() as UIWindow;
            
            this.unique = unique;
            if (unique) windowInstance.Hide();

            windowInstance.Connect("close_button", this, nameof(Close));
            
            subNode.AddChild(windowInstance);

            Label l = windowInstance.GetNode("./PanelContainer/VBoxContainer/WindowDraggableArea/Label") as Label;
            l.Text = name;
            
            
            Control content = windowInstance.GetNode("./PanelContainer/VBoxContainer/Content") as Control;

            windowInstance.MarginLeft = startPosition.x - size.x / 2;
            windowInstance.MarginTop = startPosition.y - size.y / 2;
            
            content.RectMinSize = size;
            content.AddChild(windowContent);

            windowInstance.RectMinSize = size;
            
            // Connect recursively!

            ConnectAllNodesToUIEvents(windowInstance);
        }
        
        public void Close()
        {
            if (unique) windowInstance.Hide();
            else windowInstance.QueueFree();
            
            GeneralScript.Instance.MouseExitedWindow();
        }

        public void Show()
        {
            windowInstance.Show();
        }
    }
    
    public static void ConnectAllNodesToUIEvents(Node node)
    {
        foreach (Node n in node.GetChildren())
        {
            if (n is Control)
            {
                n.Connect("mouse_entered", GeneralScript.Instance, "MouseEnteredWindow");
                n.Connect("mouse_exited", GeneralScript.Instance, "MouseExitedWindow");
                ConnectAllNodesToUIEvents(n);
            }
            
        }
    }
    
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(Lerp(a.x, b.x, t), Lerp(a.y, b.y, t), Lerp(a.z, b.z, t));
    }

    public static float Lerp(float a, float b, float t)
    {
        return (1 - t) * a + t * b;
    }
}