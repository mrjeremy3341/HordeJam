using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] EntityData data;

    public GridPosition Position { get; private set; }
    public int CurrentHealth { get; private set; }
    public EntityData Data => data;

    public virtual void Spawn(GridPosition startPosition)
    {
        SetPosition(startPosition);
        CurrentHealth = Data.MaxHealth;
        TickSystem.Instance.RegisterEntity(this);
    }

    public virtual void Tick() { }

    public virtual void Despawn()
    {
        Destroy(gameObject);
    }


    public void SetPosition(GridPosition position)
    {
        Position = position;
    }

    public void Damage(int amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0)
        {
            Despawn();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, Data.MaxHealth);
    }
}
