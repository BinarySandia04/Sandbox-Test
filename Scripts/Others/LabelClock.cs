using Godot;
using System;

public class LabelClock : Label
{
    Timer t = new Timer();
    
    public override void _Ready()
    {
        Update();
        
        AddChild(t);
        t.Connect("timeout", this, nameof(Update));
        t.Start(1f);
    }

    public void Update()
    {
        DateTime ahora = DateTime.Now;
        Text = ahora.Hour.ToString().PadLeft(2, '0') + ":" + ahora.Minute.ToString().PadLeft(2, '0');
    }
}
