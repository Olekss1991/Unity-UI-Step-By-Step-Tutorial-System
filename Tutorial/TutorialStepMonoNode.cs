using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace SGS29.Tutorial
{
    public class TutorialStepMonoNode : MonoBehaviour, ITutorialStep
    {
        public int _index;  // Індекс кроку

        public float fadeInTime = 0.5f;  // Час для плавної активації
        public float fadeOutTime = 0.4f; // Час для плавної деактивації

        public Graphic target;  // Головний елемент для підсвітки
        public List<GameObject> otherObjects; // інші графічні об'єкти які треба увімкнути чи вимкнути 
        public List<Graphic> otherGraphics; // інші графічні елементи які треба виділити
        public Button m_button;

        private Color originalColor;  // Оригінальний колір елементу

        public int Index { get => _index; }

        private void Awake()
        {
            if (target != null)
            {
                originalColor = target.color;
            }

            if (m_button == null)
                m_button = GetComponent<Button>();
        }

        void Start()
        {
            if (!SM.HasSingleton<TutorialManager>())
            {
                enabled = false;
                return;
            }

            if (!SM.Instance<TutorialManager>().TutorialCompleted)
            {
                m_button?.onClick.AddListener(OnStepCompleted);
            }
        }

        public void ActivateStep()
        {
            OnActiveStep();
        }

        public void DeactivateStep()
        {
            OnDeactivateStep();
        }

        public void OnStepCompleted()
        {
            SM.Instance<TutorialManager>().NextStep();
        }

        // Логіка для активації кроку (підсвітка)
        public virtual void OnActiveStep()
        {
            if (target != null)
            {
                SM.Instance<TutorialManager>().SceneGraphics.SetTarget(target, true);
            }

            // підсвічуємо всі інші елементи
            foreach (var graphic in otherGraphics)
            {
                // graphic.CrossFadeColor(highlightColor, fadeOutTime, true, true);
            }

            // Вмикаємо видимість інших об'єктів
            foreach (var obj in otherObjects)
            {
                obj.SetActive(true);
            }
        }

        // Логіка для деактивації кроку (затемнення)
        public virtual void OnDeactivateStep()
        {
            if (target != null)
            {
                SM.Instance<TutorialManager>().DeselectTarget(target);
            }

            // Повертаємо інші елементи до оригінальних кольорів
            foreach (var graphic in otherGraphics)
            {
                graphic.CrossFadeColor(originalColor, fadeInTime, true, true);
            }

            // Вимикаємо видимість інших об'єктів
            foreach (var obj in otherObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}