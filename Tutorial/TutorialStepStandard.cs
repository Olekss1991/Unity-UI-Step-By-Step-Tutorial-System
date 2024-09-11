using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SGS29.Tutorial
{

    [System.Serializable]
    public class TutorialStepStandard : ITutorialStep
    {
        public float fadeInTime = 0.5f;  // Час для плавної активації
        public float fadeOutTime = 0.4f; // Час для плавної деактивації

        public Graphic target;  // Головний елемент для підсвітки
        public List<GameObject> otherObjects; // інші графічні об'єкти які треба увімкнути чи вимкнути 
        public List<Graphic> otherGraphics; // інші графічні елементи які треба виділити

        private readonly int _index;  // Індекс кроку
        public int Index { get => _index; }

        private Color originalColor;  // Оригінальний колір елементу
        private Color highlightColor = Color.white;  // Колір підсвітки

        public TutorialStepStandard(int index, Graphic targetGraphic)
        {
            _index = index;
            target = targetGraphic;
            originalColor = target.color;
        }

        public void ActivateStep()
        {
            OnActiveStep();
        }

        public void DeactivateStep()
        {
            OnDeactivateStep();
        }

        // Логіка для активації кроку (підсвітка)
        public virtual void OnActiveStep()
        {
            // Плавно підсвічуємо основний елемент
            target.CrossFadeColor(highlightColor, fadeInTime, true, true);

            // підсвічуємо всі інші елементи
            foreach (var graphic in otherGraphics)
            {
                graphic.CrossFadeColor(highlightColor, fadeOutTime, true, true);
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
            // Повертаємо оригінальний колір основного елементу
            target.CrossFadeColor(originalColor, fadeOutTime, true, true);

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