using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private TextMeshProUGUI textSpeed;
    [SerializeField] private TextMeshProUGUI textMessageContainer;

    // Update is called once per frame
    private void Update()
    {
        textSpeed.text = state.gameSpeed.ToString("N2");
        textTimer.text = TimeSpan.FromSeconds(state.gameTimer).ToString("mm':'ss");
    }

    public void SetMessage(string message)
    {
        textMessageContainer.text = message;
    }
}
