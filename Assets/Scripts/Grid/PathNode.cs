using UnityEngine;

public class PathNode
{
    public GridPosition Position { get; private set; }

    public int G { get; private set; }
    public int H { get; private set; }
    public int F => G + H;

    public PathNode Parent { get; private set; }

    public PathNode(GridPosition position)
    {
        Position = position;
        G = int.MaxValue;
        H = 0;
        Parent = null;
    }

    public void SetG(int g)
    {
        G = g;
    }

    public void SetH(int h)
    {
        H = h;
    }

    public void SetParent(PathNode parent)
    {
        Parent = parent;
    }
}
