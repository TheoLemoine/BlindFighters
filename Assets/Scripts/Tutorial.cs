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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip right;
    [SerializeField] private AudioClip left;
    [SerializeField] private AudioClip center;

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
        audioSource.clip = left;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        while (controller.WagonAlign != Align.Left)
        {
            yield return null;
        }
        
        ui.SetMessage("Good, now move your wagon to the right !");
        audioSource.clip = right;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        while (controller.WagonAlign != Align.Right)
        {
            yield return null;
        }
        
        ui.SetMessage("Great, now move your wagon to the middle !");
        audioSource.clip = center;
        audioSource.Play();
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