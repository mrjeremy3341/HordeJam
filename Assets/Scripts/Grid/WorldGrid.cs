using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid Instance;

    [SerializeField] int width;
    [SerializeField] int height;

    GridCell[,] cells;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pathfinder.Initialize(this);
        TerrainType[,] map = new TerrainType[width, height];
        PixelRenderer.Instance.Initialize(map);

        cells = new GridCell[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GridPosition position = new GridPosition(x, y);
                TerrainType terrain = map[x, y];
                GridCell cell = new GridCell(position, terrain);
                
                cells[x, y] = cell;
            }
        }
    }

    public int Width => width;
    public int Height => height;

    public bool IsValidPosition(GridPosition position)
    {
        return position.X >= 0 && position.X < width && position.Y >= 0 && position.Y < height;
    }

    public GridCell GetCell(GridPosition position)
    {
        if(!IsValidPosition(position))
        {
            return null;
        }

        return cells[position.X, position.Y];
    }

    public Vector3 GridToWorld(GridPosition position)
    {
        float x = position.X - width / 2f + 0.5f;
        float y = position.Y - height / 2f + 0.5f;
        
        return new Vector3(x, y, 0);
    }

    public GridPosition WorldToGrid(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x + width / 2f);
        int y = Mathf.FloorToInt(position.y + height / 2f);
    
        return new GridPosition(x, y);
    }
}
