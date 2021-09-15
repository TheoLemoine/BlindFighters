using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers.Arduino
{
    public class ControllerDataDispatcher : MonoBehaviour
    {
        [SerializeField] private List<ArduinoReceiveController> receivers;
        
        void OnMessageArrived(string msg)
        {
            float[] values = msg.Split(' ').Select(s => float.Parse(s)).ToArray();

            for (int i = 0; i < receivers.Count; i++)
            {
                if (i > values.Length || float.IsNaN(values[i]))
                {
                    Debug.LogError("Not enough data for all receivers");
                    return;
                }
                
                receivers[i].ReceiveData(values[i]);
            }
        }

        void OnConnectionEvent(bool success)
        {
            Debug.Log(success ? "Connected" : "Disconnected");
        }
    }
}