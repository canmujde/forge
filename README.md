# Forge

Forge is a robust and extensible Unity library designed to streamline Unity development by providing essential tools, modular components, and optimized workflows. Built with scalability, performance, and flexibility in mind, this library helps developers maintain a clean and efficient project structure while minimizing repetitive coding tasks.

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

* **Play 2D sound**
```csharp
AudioManager.Play2D("CoinEarned");
```
* **Play 2D sound and set volume**
```csharp
AudioManager.Play2D("CoinEarned", volume);
```
* **Play 2D sound pitched. Pass pitch value as second parameter of method.**
```csharp
AudioManager.Play2DPitched("CoinEarned", pitch);
```
* #### Play 2D sound trimmed. Play specific portion of your AudioClip. Pass the start and end parameters between 0 and 1. 
**Example:** Assuming the length of your clip is 3 seconds. You've passed 0.5f as the start parameter and 1.0f as the end parameter, it will play from the one and a half second to the 3rd second.
```csharp
AudioManager.Play2DTrimmed("CoinEarned", start, end);
```
* **Play 3D sound**
```csharp
AudioManager.Play3D("PlayerGotDamage", transform.position);
```
* **Play 3D sound and set volume**
```csharp
AudioManager.Play3D("PlayerGotDamage", transform.position, volume);
```
* **Play 3D sound pitched. Pass pitch value as second parameter of method.**
```csharp
AudioManager.Play3DPitched("PlayerGotDamage", transform.position, pitch);
```
* #### Play 3D sound trimmed. Play specific portion of your AudioClip. Pass the start and end parameters between 0 and 1.

**Example:** Assuming the length of your clip is 3 seconds. You've passed 0.5f as the start parameter and 1.0f as the end parameter, it will play from the one and a half second to the 3rd second.
```csharp
AudioManager.Play3DTrimmed("PlayerGotDamage", transform.position, start, end);
```
* **Play BGMs. Add true as second parameter to give it a transition effect.**
```csharp
AudioManager.PlayBackgroundMusic(clipName, transition);
```

* **You can reserve AudioSource from AudioManager and use however you want.**
```csharp
public class PlayerSounds : MonoBehaviour
{
    private AudioSource playerMovementAudioSource;
    
    private void Start()
    {
        playerMovementAudioSource = AudioManager.ReserveAudioSource();
    }
    
    private void OnMove()
    {
        if (playerMovementAudioSource && !playerMovementAudioSource.isPlaying)
            playerMovementAudioSource.Play();
    }
}
```
* **Enqueue your reserved AudioSource**
```csharp
AudioManager.EnqueueAudioSource(playerMovementAudioSource);
```


