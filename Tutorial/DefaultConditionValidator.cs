using System.Collections;
using UnityEngine;
using Utilities;

namespace SGS29.Tutorial
{
    public class DefaultConditionValidator : ConditionValidator
    {
        private const string TUTORIAL_KEY = "fwg41d23hgtr'ju/?ie/defs643frag1re";

        private bool tutorialCompleted;
        private bool tutorialStarted;
        public override bool GetTutorialStarted() => tutorialStarted;

        private void Awake()
        {
            LoadData();
            // Ініціалізувати Action, якщо потрібно
            if (onTutorialStarted == null)
            {
                onTutorialStarted = delegate { };
            }

            if (onTutorialEnded == null)
            {
                onTutorialEnded = delegate { };
            }
        }

        // чекаємо 0.1 секунди до ініціалізації усіх компонентів туторіалу 
        public IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            if (!tutorialCompleted)
            {
                tutorialStarted = true;
                onTutorialStarted?.Invoke();
                SM.Instance<TutorialManager>().OnStepChanged += OnStepChanged;
            }
        }

        private void OnStepChanged(int stepID)
        {
            bool IsTutorialCompleted = SM.Instance<TutorialManager>().IsTutorialCompleted();

            if (IsTutorialCompleted)
            {
                tutorialCompleted = true;
                SaveTutorial();

                onTutorialEnded?.Invoke();
                Debug.Log("Туторіал завершено!");
            }
        }

        private void LoadData()
        {
            tutorialCompleted = PlayerPrefs.GetInt(TUTORIAL_KEY, 0) == 1;
        }

        private void SaveTutorial()
        {
            int valueToSave = tutorialCompleted ? 1 : 0;
            PlayerPrefs.SetInt(TUTORIAL_KEY, valueToSave);
        }
    }
}