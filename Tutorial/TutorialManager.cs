using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace SGS29.Tutorial
{
    [RequireComponent(typeof(TutorialSetup))]
    public class TutorialManager : MonoSingleton<TutorialManager>
    {
        private List<ITutorialStep> steps = new List<ITutorialStep>();  // Список усіх кроків
        private ITutorialStep currentStep;  // Поточний активний крок
        [SerializeField] private SceneGraphics sceneGraphics;
        [Range(0, 1)]
        [SerializeField] private float startFade = 0.5f;
        private int currentStepIndex = -1;  // Початковий індекс встановлюємо як -1

        public event Action<int> OnStepChanged;  // Подія, яка сповіщає про зміну кроку
        public SceneGraphics SceneGraphics { get => sceneGraphics; }
        public bool TutorialCompleted { get; private set; }
        private ConditionValidator validator;

        public void InitializeSteps(List<ITutorialStep> steps, List<Graphic> graphics, ConditionValidator validator)
        {
            this.steps = steps;
            this.validator = validator;

            // Ініціалізуємо SceneGraphics
            sceneGraphics = new SceneGraphics(graphics);

            TutorialCompleted = validator.TutorialCompleted;
        }

        // Додаємо кроки в менеджер
        public void AddStep(ITutorialStep step)
        {
            steps.Add(step);
        }

        // Запускаємо туторіал
        public void StartTutorial()
        {
            if (steps.Count > 0)
            {
                // Затемнюємо усі графічні елементи
                sceneGraphics.SetFade(startFade);

                Debug.Log("Starting tutorial...");
                // Активуємо перший крок за його індексом
                ActivateStep(0);
            }
            else
            {
                Debug.LogError("No tutorial steps available.");
            }
        }


        // Активуємо конкретний крок за індексом
        public void ActivateStep(int index)
        {
            Debug.Log($"Activating step with index: {index}");

            if (currentStep != null)
            {
                currentStep.DeactivateStep();  // Деактивуємо попередній крок
            }

            currentStep = GetStepByIndex(index);
            if (currentStep != null)
            {
                currentStep.ActivateStep();  // Активуємо новий крок
                currentStepIndex = index;
                OnStepChanged?.Invoke(index);  // Викликаємо подію при зміні кроку
            }
            else
            {
                Debug.LogError($"Step with index {index} not found.");
            }
        }


        // Переходимо до наступного кроку
        public void NextStep()
        {
            int nextIndex = currentStepIndex + 1;
            // Перевіряємо, чи існує крок з наступним індексом
            ITutorialStep nextStep = GetStepByIndex(nextIndex);
            if (nextStep != null)
            {
                ActivateStep(nextIndex);
            }
        }

        // Повертаємось до попереднього кроку
        public void PreviousStep()
        {
            int prevIndex = currentStepIndex - 1;
            // Перевіряємо, чи існує крок з попереднім індексом
            ITutorialStep prevStep = GetStepByIndex(prevIndex);
            if (prevStep != null)
            {
                ActivateStep(prevIndex);
            }
        }

        // Перевіряємо, чи пройдено туторіал
        public bool IsTutorialCompleted()
        {
            // Перевіряємо, чи поточний індекс кроку є останнім
            int maxIndex = -1;
            foreach (var step in steps)
            {
                if (step.Index > maxIndex)
                {
                    maxIndex = step.Index;
                }
            }
            return currentStepIndex >= maxIndex;
        }

        // Отримати крок за індексом
        public ITutorialStep GetStepByIndex(int index)
        {
            return steps.Find(step => step.Index == index);
        }

        public void DeselectTarget(Graphic graphic)
        {
            SceneGraphics.SetFade(graphic, startFade);
        }

        public void SetTutorialEnded()
        {
            TutorialCompleted = true;
            SceneGraphics.SetFade(0);
            Debug.Log(123);
        }
    }
}