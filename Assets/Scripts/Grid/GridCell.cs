using UnityEngine;

public class GridCell
{
    public GridPosition Position { get; private set; }
    public TerrainType Terrain { get; private set; }

    public Entity Entity { get; private set; }
    public bool IsOccupied => Entity != null;
    public bool IsWalkable => Terrain != TerrainType.Coast && Terrain != TerrainType.Ocean;

    public GridCell(GridPosition position, TerrainType terrain)
    {
        Position = position;
        Terrain = terrain;
    }

    public void SetEntity(Entity entity)
    {
        Entity = entity;
    }

    public void ClearEntity()
    {
        Entity = null;    
    }
}
