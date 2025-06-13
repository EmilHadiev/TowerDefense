public interface ITrainingMode
{
    bool IsTrainingProcess();
    void ShowNextTraining();
    void InitTraining(ILevelStateSwitcher levelStateSwitcher);

    void TrainingOver();
}