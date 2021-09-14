using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private float startTimer;
    [SerializeField] private GameState state;
    [SerializeField] private float accPerSec = 0.3f;
    [SerializeField] private float slowDownPerCollide = 10;
    [SerializeField] private float minGameSpeed = 5;

    private void Start()
    {
        state.OnObstacleCollide += SlowDownGame;
        state.gameTimer = startTimer;
    }

    private void SlowDownGame()
    {
        state.gameSpeed = Mathf.Max(minGameSpeed, state.gameSpeed - slowDownPerCollide);
    }

    private void Update()
    {
        state.gameSpeed += accPerSec * Time.deltaTime;
        state.gameTimer -= Time.deltaTime;
        if(state.gameTimer <= 0)
            EndGame();
    }

    private void EndGame()
    {
        state.gameTimer = startTimer;
        state.gameSpeed = minGameSpeed;
    }
    
}