using System.Collections.Generic;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public static TickSystem Instance;

    [SerializeField] int ticksPerSecond;
    
    float tickInterval;
    float tickTimer;

    List<Entity> entities = new List<Entity>();
    List<Entity> toAdd = new List<Entity>();
    List<Entity> toRemove = new List<Entity>();
    
    bool isDirty = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tickInterval = 1f / ticksPerSecond;
        tickTimer = 0f;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if(tickTimer >= tickInterval)
        {
            tickTimer -= tickInterval;
            ProcessTicks();
        }
    }

    public void RegisterEntity(Entity entity)
    {
        toAdd.Add(entity);
        isDirty = true;
    }

    public void UnregisterEntity(Entity entity)
    {
        toRemove.Add(entity);
        isDirty = true;
    }

    void ProcessTicks()
    {
        if(isDirty)
        {
            foreach(Entity e in toAdd)
            {
                if(!entities.Contains(e))
                {
                    entities.Add(e);
                }
            }

            foreach(Entity e in toRemove)
            {
                if(entities.Contains(e))
                {
                    entities.Remove(e);
                }
            }

            toAdd.Clear();
            toRemove.Clear();
            isDirty = false;
        }

        foreach(Entity e in entities)
        {
            e.Tick();
        }
    }
}
