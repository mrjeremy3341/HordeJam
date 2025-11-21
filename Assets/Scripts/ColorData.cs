using UnityEngine;

[CreateAssetMenu(menuName = "Data/Color Data")]
public class ColorData : ScriptableObject
{
    [SerializeField] Color[] terrainColors;
    [SerializeField] Color[] entityColors;

    public Color GetTerrainColor(TerrainType terrain)
    {
        return terrainColors[(int)terrain];
    }

    public Color GetEntityColor(EntityType entity)
    {
        return entityColors[(int)entity];
    }
}
