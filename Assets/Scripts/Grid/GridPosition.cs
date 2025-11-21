using System;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
{
    public static readonly GridPosition Null = new GridPosition(int.MinValue, int.MinValue);

    public int X { get; private set; }
    public int Y { get; private set; }

    public GridPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public GridPosition North => new GridPosition(X, Y + 1);
    public GridPosition South => new GridPosition(X, Y - 1);
    public GridPosition East => new GridPosition(X + 1, Y);
    public GridPosition West => new GridPosition(X - 1, Y);
    public GridPosition NorthEast => new GridPosition(X + 1, Y + 1);
    public GridPosition NorthWest => new GridPosition(X - 1, Y + 1);
    public GridPosition SouthEast => new GridPosition(X + 1, Y - 1);
    public GridPosition SouthWest => new GridPosition(X - 1, Y - 1);

    public GridPosition[] AllNeighbors()
    {
        return new GridPosition[]
        {
            North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
        };
    }

    public GridPosition[] CardinalNeighbors()
    {
        return new GridPosition[]
        {
            North, East, South, West
        };
    }

    public List<GridPosition> Area(int radius)
    {
        List<GridPosition> positions = new List<GridPosition>();
        for(int x = -radius; x <= radius; x++)
        {
            int maxY = Mathf.FloorToInt(Mathf.Sqrt(radius * radius - x * x));
            for(int y = -maxY; y <= maxY; y++)
            {
                positions.Add(new GridPosition(X + x, Y + y));
            }
        }
        
        return positions;
    }

    public static int GridDistance(GridPosition a, GridPosition b)
    {
        return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
    }

    public static float WorldDistance(GridPosition a, GridPosition b)
    {
        float dx = a.X - b.X;
        float dy = a.Y - b.Y;
        
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.X + b.X, a.Y + b.Y);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.X - b.X, a.Y - b.Y);
    }

    public bool Equals(GridPosition other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        if(obj is GridPosition other)
        {
            return this == other;
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
