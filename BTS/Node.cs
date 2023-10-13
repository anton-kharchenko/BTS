namespace BTS;

public class Node
{
    private Node? _left;
    private Node? _right;
    private int _value;

    private bool NodeExists(Node? node)
    {
        return node != null;
    }

    private void CreateNode(Node? node, int value)
    {
        node = new Node
        {
            _left = new Node(),
            _right = new Node(),
            _value = value
        };
    }

    public void Insert(Node? node, int value)
    {
        if (!NodeExists(node))
        {
            CreateNode(node, value);
        }
        else if (value < node!._value)
        {
            Insert(node._left, value);
        }
        else if (value >= node._value)
        {
            Insert(node._right, value);
        }
    }

    public Node? Search(Node? node, int value)
    {
        if (!NodeExists(node)) return null;
        if (node._value == value) return node;
        return Search(value < node._value
            ? node._left
            : node._right, value);
    }

    public Node? GetMin(Node? node)
    {
        if (!NodeExists(node)) return null;
        return NodeExists(node?._left) ? GetMin(node!._left) : node;
    }

    public Node? GetMax(Node? node)
    {
        if (!NodeExists(node)) return null;
        return NodeExists(node?._right) ? GetMax(node!._right) : node;
    }

    public void InOrder(Node node)
    {
        if (!NodeExists(node)) return;
        InOrder(node._left);
        Console.WriteLine(node._value);
        InOrder(node._right);
    }

    public void PostOrder(Node node)
    {
        if (!NodeExists(node)) return;
        PostOrder(node._left);
        PostOrder(node._right);
        Console.WriteLine(node._value);
    }

    public void PreOrder(Node node)
    {
        if (!NodeExists(node)) return;
        Console.WriteLine(node._value);
        PostOrder(node._left);
        PostOrder(node._right);
    }

    private void TransplantNode(Node toNode, Node? fromNode)
    {
        toNode._value = fromNode!._value;
        toNode._left = fromNode._left;
        toNode._right = fromNode._right;
    }

    public int GetChildrenCount(Node node)
    {
        int count = 0;
        if (NodeExists(node._left)) count++;
        if (NodeExists(node._right)) count++;
        return count;
    }

    private Node? GetChildOrNull(Node node)
    {
        return NodeExists(node._left) ? node._left : node._right;
    }

    public void RemoveNodeWithOneOrZeroChild(Node noteToDelete)
    {
        Node? childOrNull = GetChildOrNull(noteToDelete);
        TransplantNode(noteToDelete, childOrNull);
    }

    public bool Remove(Node root, int value)
    {
        var nodeToDelete = Search(root, value);
        if (!NodeExists(nodeToDelete)) return false;

        var childrenCount = GetChildrenCount(nodeToDelete);
        if (childrenCount < 2)
        {
            RemoveNodeWithOneOrZeroChild(nodeToDelete);
        }
        else
        {
            var minNode = GetMin(nodeToDelete._right);
            nodeToDelete._value = minNode!._value;
            RemoveNodeWithOneOrZeroChild(minNode);
        }

        return true;
    }
}