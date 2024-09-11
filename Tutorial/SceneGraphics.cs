using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace SGS29.Tutorial
{
    [System.Serializable]
    public class SceneGraphics
    {
        [SerializeField] private List<Graphic> graphics;

        public SceneGraphics(List<Graphic> graphics)
        {
            this.graphics = graphics;
        }

        /// <summary>
        /// Метод, що задає затемнення графічним елементам.
        /// </summary>
        /// <param name="fade">Параметр, який обробляється від 0 до 1, де 0 - це оригінальний колір, а 1 - повністю затемнений.</param>
        /// <summary>
        /// Метод, що задає затемнення графічним елементам.
        /// </summary>
        /// <param name="fade">Параметр, який обробляється від 0 до 1, де 0 - це оригінальний колір, а 1 - повністю чорний.</param>
        public void SetFade(float fade)
        {
            foreach (var graphic in graphics)
            {
                SetFade(graphic, fade);
            }
        }

        public void SetFade(Graphic graphic, float fade)
        {
            // Отримуємо оригінальний колір
            Color originalColor = graphic.color;

            // Розраховуємо новий колір
            Color targetColor = new Color(
                originalColor.r * (1 - fade),
                originalColor.g * (1 - fade),
                originalColor.b * (1 - fade),
                originalColor.a  // Альфа-канал не змінюється
            );

            // Анімація зміни кольору
            graphic.DOColor(targetColor, 0.5f);  // 0.5f - час анімації, налаштуйте за необхідності
        }

        /// <summary>
        /// Підсвічує обраний графічний об'єкт, змінюючи його колір з затемненого на оригінальний.
        /// </summary>
       // Метод для підсвічування обраного графічного об'єкта
        public void SetTarget(Graphic target, bool chieldsGraphics = false)
        {
            // Змінюємо колір самого графічного об'єкта
            Color originalColor = Color.white;
            target.DOColor(originalColor, 0.5f);  // 0.5f - час анімації

            // Якщо потрібно, змінюємо колір всіх дочірніх графічних елементів
            if (chieldsGraphics)
            {
                // Збираємо всі дочірні графічні елементи
                List<Graphic> childGraphics = new List<Graphic>(target.GetComponentsInChildren<Graphic>());

                // Проходимо по всіх дочірніх графічних елементах і змінюємо їх колір
                foreach (var childGraphic in childGraphics)
                {
                    if (childGraphic != target)  // Щоб не змінювати колір самого цільового графічного елемента
                    {
                        childGraphic.DOColor(originalColor, 0.5f);
                    }
                }
            }
        }
    }
}
