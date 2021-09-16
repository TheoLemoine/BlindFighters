using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState", order = 0)]
public class GameState : ScriptableObject
{
    public float gameSpeed;
    public float gameTimer;
    [Range(1,3)]
    public int NumberOfPlayersRequired = 1;

    public event Action OnObstacleCollide;
    public event Action OnObstacleEvade;
    public event Action<int> OnEnterObstacleZone;
    public event Action<int> OnExitObstacleZone;
    
    public void TriggerObstacleCollision()
    {
        Debug.Log("ObstacleCollide");
        OnObstacleCollide?.Invoke();
    }

    public void TriggerObstacleEvade()
    {
        Debug.Log("ObstacleEvade");
        OnObstacleEvade?.Invoke();
    }

    public void TriggerOnEnterObstacleZone(int id)
    {
        Debug.Log("OnEnterObstacleZone");
        OnEnterObstacleZone?.Invoke(id);
    }

    public void TriggerOnExitObstacleZone(int id)
    {
        Debug.Log("OnExitObstacleZone");
        OnExitObstacleZone?.Invoke(id);
    }
}