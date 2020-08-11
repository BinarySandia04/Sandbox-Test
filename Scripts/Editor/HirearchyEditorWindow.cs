using Godot;
using System;

public class HirearchyEditorWindow : Control
{
    [Export] private NodePath TreePath;

    private Tree TreeNode;
    private Spatial EditorSpatialNode;

    public static HirearchyEditorWindow Instance;

    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        TreeNode = GetNode(TreePath) as Tree;
        EditorSpatialNode = GeneralScript.Instance._EditorSpatialNode;
        
        
        
        // Crea un arbol a partir de un nodo de manera recursiva
        UpdateHirearchy();
    }

    public void UpdateTree(TreeItem root, Node trification, Tree tree)
    {
        foreach (Node n in trification.GetChildren())
        {
            if(n.Name.StartsWith("$")) continue; // D$LLA MONEY PASTA DINEROS
            
            GD.Print(n.Name);
            
            TreeItem child = tree.CreateItem(root);
            child.SetText(0, n.Name);
            
            UpdateTree(child, n, tree);
        }
    }

    public static void UpdateHirearchy()
    {
        Instance.TreeNode.Clear();
        
        TreeItem TreeRoot = Instance.TreeNode.CreateItem();
        TreeRoot.SetText(0, "Scene");
        
        Instance.UpdateTree(TreeRoot, Instance.EditorSpatialNode, Instance.TreeNode);
    }
}
