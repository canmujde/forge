# Forge




# Features

## Pool Manager

You can variate pool objects by creating PoolableScriptableObject asset under Resources>PoolableObjects.

1. Go to Resources folder and create PoolableObjects folder in it if it doesn't exist.
2. Create PoolableScriptableObject asset inside newly created(or existing) PoolableObjects folder.
3. Fill the asset's needs in Inspector.

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
        Debug.Log("My health after damage is : " + health);
    }
}
```

## Audio Manager

1. Go to Resources folder and create Audio folder in it if it doesn't exist.
2. Put your audio files inside newly created(or existing) Audio folder.

### Usage


```csharp
AudioManager.Play2D("CoinEarned");
```
```csharp
AudioManager.Play2D("CoinEarned", volume);
```
```csharp
AudioManager.Play2DPitched("CoinEarned", pitch);
```
```csharp
AudioManager.Play2DTrimmed("CoinEarned", start = 0.5f, end = 1.0f);
// passing 0.5f as start means clip will start from half of it's length.
```

```csharp
AudioManager.Play3D("PlayerGotDamage", transform.position);
```
```csharp
AudioManager.Play3D("PlayerGotDamage", transform.position, volume);
```
```csharp
AudioManager.Play3DPitched("PlayerGotDamage", transform.position, pitch);
```
```csharp
AudioManager.Play3DPitched("PlayerGotDamage", transform.position, start = 0.5f, end = 1.0f);
// passing 0.5f as start means clip will start from half of it's length.
```




