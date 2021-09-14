using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState", order = 0)]
public class GameState : ScriptableObject
{
    public float gameSpeed;
    public float gameTimer;

    public event Action OnObstacleCollide;
    public event Action OnObstacleEvade;

    public void TriggerObstacleCollision()
    {
        OnObstacleCollide?.Invoke();
    }

    public void TriggerObstacleEvade()
    {
        OnObstacleEvade?.Invoke();
    }
}