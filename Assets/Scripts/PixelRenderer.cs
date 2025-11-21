using UnityEngine;

public enum TerrainType
{
    Meadow, Thicket, Ridge, Grove, Scrub, Bluff,
    Tundra, Beach, Coast, Ocean
}

public enum EntityType
{
    Farm, Lumberyard, Mine, Keep, Outpost, Barracks, Tower, Wall,
    Soldier, Walker, Runner, Brute
}

public class PixelRenderer : MonoBehaviour
{
    public static PixelRenderer Instance;

    [SerializeField] ColorData colorData;

    SpriteRenderer sr;
    Texture2D texture;
    
    Color32[] mapPixels;
    Color32[] displayPixels;

    int width;
    int height;

    bool isDirty;

    private void Awake()
    {
        Instance = this;
        sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if(isDirty)
        {
            Apply();
            isDirty = false;
        }
    }

    public void Initialize(TerrainType[,] terrainMap)
    {
        width = terrainMap.GetLength(0);
        height = terrainMap.GetLength(1);

        texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        sr.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.one * 0.5f, 1f);

        mapPixels = new Color32[width * height];
        displayPixels = new Color32[width * height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int index = y * width + x;
                mapPixels[index] = colorData.GetTerrainColor(terrainMap[x, y]);
                displayPixels[index] = mapPixels[index];
            }
        }

        Apply();
    }

    public void SetPixel(GridPosition position, EntityType entity)
    {
        if(position.X < 0 || position.X >= width || position.Y < 0 || position.Y >= height)
        {
            return;
        }

        int index = position.Y * width + position.X;
        displayPixels[index] = colorData.GetEntityColor(entity);
        isDirty = true;
    }

    public void ClearPixel(GridPosition position)
    {
        if(position.X < 0 || position.X >= width || position.Y < 0 || position.Y >= height)
        {
            return;
        }

        int index = position.Y * width + position.X;
        displayPixels[index] = mapPixels[index];
        isDirty = true;
    }

    void Apply()
    {
        texture.SetPixels32(displayPixels);
        texture.Apply();
    }
}
