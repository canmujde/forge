# Forge


## Pool Manager

You can variate pool objects by creating PoolableScriptableObject asset under Resources>PoolableObjects.

1. Go to Resources folder and create PoolableObjects folder in it if it doesn't exist.
2. Create PoolableScriptableObject asset inside newly created(or existing) folder PoolableObjects.
3. Fill the asset's needs in Inspector.
4. Done!

Note: Poolable objects will be given their id as their prefab game object's name that you assigned in PoolableScriptableObject asset.

### Usage

#### Retrieve

```csharp
var cube = PoolManager.GetObject("Ball");
```

#### Repool
```csharp
PoolManager.ReturnObject(id, ball); (string, GameObject)
```

## Event System

### Usage
```csharp
    public class Player : MonoBehaviour
    {
        private float health;
        
        private void Start()
        {
            EventManager<float>.Subscribe("OnPlayerDamaged", OnPlayerDamaged);
        }
        private void OnDestroy()
        {
            EventManager<float>.Unsubscribe("OnPlayerDamaged", OnPlayerDamaged);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                EventManager<float>.Trigger("OnPlayerDamaged", 5);
            }
        }
        
        
        private void OnPlayerDamaged(float damage)
        {
            health -= damage;
            Debug.Log("My health is after damage : " + health);
        }
    }
```

