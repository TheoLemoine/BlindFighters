using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    [SerializeField] private float startTimer;
    [SerializeField] private GameState state;
    [SerializeField] private float accPerSec = 0.3f;
    [SerializeField] private float slowDownPerCollide = 10;
    [SerializeField] private float minGameSpeed = 5;
    private float startDistance;

    private void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return; //Avoid doing anything else
        }
        instance = this;
    }
    
    public bool IsGameOn { get; private set; } = false;
    
    private void Start()
    {
        state.OnObstacleCollide += SlowDownGame;
        ResetState();
    }

    private void SlowDownGame()
    {
        state.gameSpeed = Mathf.Max(minGameSpeed, state.gameSpeed - slowDownPerCollide);
    }

    private void Update()
    {
        if(!IsGameOn) return;
        
        // update game stats
        state.gameSpeed += accPerSec * Time.deltaTime;
        state.gameTimer -= Time.deltaTime;
        state.gameDistance += (state.gameSpeed * Time.deltaTime)/10;
        Debug.Log(state.gameDistance);

        if (state.gameTimer <= 0)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        ResetState();
        IsGameOn = true;
    }

    private void EndGame()
    {
        ResetState();
        IsGameOn = false;
        state.TriggerEndGame();
    }

    private void ResetState()
    {
        state.gameTimer = startTimer;
        state.gameSpeed = minGameSpeed;
        state.gameDistance = startDistance;
    }
    
}