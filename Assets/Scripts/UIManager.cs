using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private TextMeshProUGUI textSpeed;

    // Update is called once per frame
    void Update()
    {
        textSpeed.text = state.gameSpeed.ToString("N2");
        textTimer.text = TimeSpan.FromSeconds(state.gameTimer).ToString("mm':'ss");
    }
}
