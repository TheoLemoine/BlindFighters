using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.Containers
{
    [Serializable]
    public abstract class ASoundContainer : ScriptableObject
    {
        public abstract AudioClip GetClip();
    }
}