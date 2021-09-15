using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObstacleSpawn : UnityEvent<int>
{
}

[System.Serializable]
public class ObstacleDespawn : UnityEvent
{
}