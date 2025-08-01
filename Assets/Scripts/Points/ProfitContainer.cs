using Zenject;

public class ProfitContainer : IProfitContainer, IInitializable
{
    private IPlayerSoundContainer _playerSound;
    private ICoinStorage _coinStorage;

    public int Profits { get; private set; }

    public ProfitContainer(IPlayerSoundContainer playerSound, ICoinStorage coinStorage)
    {
        _playerSound = playerSound;
        _coinStorage = coinStorage;
    }

    public void AddProfitsToPlayer()
    {
        _coinStorage.Add(GetBoostProfits());
        _playerSound.Play(SoundName.SpendCoin);
    }

    public int GetBoostProfits(int multiplier = 1) => Profits * multiplier;

    public void IncreaseProfits(int profits) => Profits += profits;

    public void Initialize()
    {
        Profits = 0;
    }
}