using System;
using System.Collections;
using Controllers;
using Obstacles;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private GameManager game;
    [SerializeField] private ObstacleSpawner spawner;
    [SerializeField] private WagonController controller;
    [SerializeField] private UIManager ui;
    
    private void Start()
    {
        StartTutorial();

        state.OnGameEnded += StartTutorial;
    }

    public void StartTutorial()
    {
        StartCoroutine(TutorialTimeline());
    }

    private IEnumerator TutorialTimeline()
    {
        spawner.Clear();
        spawner.enabled = false;
        
        ui.SetMessage("Move your wagon to the left !");
        yield return new WaitForSeconds(1);
        while (controller.WagonAlign != Align.Left)
        {
            yield return null;
        }
        
        ui.SetMessage("Good, now move your wagon to the right !");
        yield return new WaitForSeconds(1);
        while (controller.WagonAlign != Align.Right)
        {
            yield return null;
        }
        
        ui.SetMessage("Great, now move your wagon to the middle !");
        yield return new WaitForSeconds(1);
        while (controller.WagonAlign != Align.Center)
        {
            yield return null;
        }
        
        ui.SetMessage("Now the game starts !");
        yield return new WaitForSeconds(2);
        ui.SetMessage("");
        
        spawner.enabled = true;
        game.StartGame();
    }
}