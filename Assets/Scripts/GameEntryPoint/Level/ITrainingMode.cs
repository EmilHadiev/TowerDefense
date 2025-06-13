public interface ITrainingMode
{
    bool IsStartProcess();
    void ShowNextTraining();
    void InitTraining(ILevelStateSwitcher levelStateSwitcher);

    void TrainingOver();
}