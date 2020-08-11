using Godot;
using Godot.Collections;

public class EditorUI : Control
{
    [Export] private NodePath ButtonDownPanelPath;
    private Node ButtonDownPanelNode;
    
    [Export] private Dictionary<NodePath, PackedScene> ButtonWindowsDictionary;
    private Dictionary<string, Utils.Window> ButtonInstancedWindows = new Dictionary<string, Utils.Window>();

    public static EditorUI Instance;

    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        ButtonDownPanelNode = GetNode(ButtonDownPanelPath);
        
        Utils.ConnectAllNodesToUIEvents(ButtonDownPanelNode);

        foreach (var n in ButtonWindowsDictionary)
        {
            Button button = GetNode(n.Key) as Button;

            // TODO: Hacer algo de customizar el nombre de la ventana (hacerlo independiente del nombre del boton), ah y tambien tamaño!
            ButtonInstancedWindows.Add(button.Name, new Utils.Window(this, button.Name, true, ButtonWindowsDictionary[n.Key].Instance(), Utils.screenSize / 2, new Vector2(300, 700)));
                
            Array parameters = new Array();
            parameters.Add(button.Name);
                
            button.Connect("pressed", this, nameof(EditorDownBarPress), parameters);
        }
    }

    public void EditorDownBarPress(string command)
    {
        ButtonInstancedWindows[command].Show();
    }
}
