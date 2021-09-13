using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState", order = 0)]
public class GameState : ScriptableObject
{
    public float GameSpeed { get; set; }
    public int GameScore { get; set; }
}