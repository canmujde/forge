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
private void Start()
{
    EventManager<int>.Subscribe("OnPlayerDeath", OnPlayerDeath);
}

private void OnDestroy()
{
    EventManager<int>.Unsubscribe("OnPlayerDeath", OnPlayerDeath);
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.K))
    {
        EventManager<int>.Trigger("OnPlayerDeath", 100);
    }
}

private void OnPlayerDeath(int sc)
{
Debug.Log("Im dead! my score is : " + sc);
}
```

