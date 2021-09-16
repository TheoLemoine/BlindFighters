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
    [SerializeField] private AudioClip welcome;
    [SerializeField] private AudioClip bats_1;
    [SerializeField] private AudioClip bats_2;
    [SerializeField] private AudioClip ghosts_1;
    [SerializeField] private AudioClip ghosts_2;
    [SerializeField] private AudioClip tunnel_1;
    [SerializeField] private AudioClip tunnel_2;

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
        ui.SetMessage("");

        //INTRO
        audioSource.clip = welcome;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);

        //BATS PART
        spawner.SpawnBats();
        audioSource.clip = bats_1;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = bats_2;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        ui.SetMessage("Move your wagon to the left !");
        yield return new WaitForSeconds(2);
        while (controller.WagonAlign != Align.Left)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);

        //GHOST PART
        ui.SetMessage("");
        spawner.SpawnGhost();
        audioSource.clip = ghosts_1;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = ghosts_2;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        ui.SetMessage("Good, now move your wagon to the right !");
        yield return new WaitForSeconds(2);
        while (controller.WagonAlign != Align.Right)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);

        //TUNNEL PART
        ui.SetMessage("");
        spawner.SpawnTunnel();
        audioSource.clip = tunnel_1;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
        audioSource.clip = tunnel_2;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        ui.SetMessage("Great, now move your wagon to the middle !");
        yield return new WaitForSeconds(1);
        while (controller.WagonAlign != Align.Center)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);

        ui.SetMessage("Now the game starts !");
        yield return new WaitForSeconds(2);
        ui.SetMessage("");


        
        spawner.enabled = true;
        game.StartGame();
    }
}