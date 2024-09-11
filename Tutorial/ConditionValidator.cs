using System;
using UnityEngine;

namespace SGS29.Tutorial
{
    public abstract class ConditionValidator : MonoBehaviour
    {
        public Action onTutorialStarted;
        public Action onTutorialEnded;
        public bool TutorialCompleted { get; protected set; }
        public abstract bool GetTutorialStarted();
    }
}