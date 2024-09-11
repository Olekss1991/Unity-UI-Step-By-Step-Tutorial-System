public interface ITutorialStep
{
    int Index { get; }  // Індекс кроку

    void ActivateStep();  // Метод для активації кроку
    void DeactivateStep();  // Метод для деактивації кроку
}
