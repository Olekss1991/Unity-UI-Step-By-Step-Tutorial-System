using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace SGS29.Tutorial
{
    public class TutorialSetup : MonoBehaviour
    {
        [SerializeField] private ConditionValidator validator;
        private List<TutorialStepMonoNode> tutorialStepsNode = new List<TutorialStepMonoNode>();

        private List<ITutorialStep> tutorialSteps = new List<ITutorialStep>();

        private void Awake()
        {
            InitializeStepsLocal();
        }

        private void Start()
        {
            // Отримання усіх графічних елементів типу Graphic з сцени
            List<Graphic> graphics = new List<Graphic>(FindObjectsOfType<Graphic>());

            SM.Instance<TutorialManager>().InitializeSteps(tutorialSteps, graphics, validator);

            // Реєстрація обробника подій для початку туторіалу
            validator.onTutorialStarted += OnTutorialStarted;
            validator.onTutorialEnded += OnTutorialEnded;
        }

        private void OnTutorialEnded()
        {
            SM.Instance<TutorialManager>().SetTutorialEnded();
        }

        private void OnTutorialStarted()
        {
            SM.Instance<TutorialManager>().StartTutorial();
        }

        private void InitializeStepsLocal()
        {
            tutorialStepsNode.Clear();
            tutorialStepsNode = new List<TutorialStepMonoNode>(FindObjectsOfType<TutorialStepMonoNode>());

            tutorialSteps.Clear(); // Очистіть перед додаванням нових кроків

            foreach (var step in tutorialStepsNode)
            {
                tutorialSteps.Add(step);
                Debug.Log($"Added step with index: {step.Index}");
            }
        }
    }
}