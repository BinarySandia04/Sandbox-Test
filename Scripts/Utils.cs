using Godot;
using System;

public class Utils
{
    public static Vector2 screenSize;

    public class Window : Godot.Object // Wat ns pq tiene que hacer esto
    {
        private static PackedScene windowResource = ResourceLoader.Load("res://Nodes/UIElements/UIWindow.tscn") as PackedScene;

        public Control node;

        private bool unique;
        
        public Window(Node subNode, string name, bool unique, Node windowContent, Vector2 startPosition, Vector2 size)
        {
            node = windowResource.Instance() as Control;
            
            this.unique = unique;
            if (unique) node.Hide();

            node.Connect("close_button", this, nameof(Close));
            
            subNode.AddChild(node);

            Label l = node.GetNode("./PanelContainer/VBoxContainer/WindowDraggableArea/Label") as Label;
            l.Text = name;
            
            
            Control content = node.GetNode("./PanelContainer/VBoxContainer/Content") as Control;

            node.MarginLeft = startPosition.x - size.x / 2;
            node.MarginTop = startPosition.y - size.y / 2;
            
            content.RectMinSize = size;
            content.AddChild(windowContent);
            
            // TODO: Crear control para manejar cuando se destruye una ventana!
        }
        
        public void Close()
        {
            if (unique) node.Hide();
            else node.QueueFree();
        }

        public void Show()
        {
            node.Show();
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