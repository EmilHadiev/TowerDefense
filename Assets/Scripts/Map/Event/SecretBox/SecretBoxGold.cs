public class SecretBoxGold : SecretBox
{
    private const int Coins = 100;
    private readonly ICoinStorage _coinStorage;

    public SecretBoxGold(ISoundContainer soundContainer, ICoinStorage coinStorage) : base(soundContainer)
    {
        _coinStorage = coinStorage;
    }

    public override void Activate()
    {
        _coinStorage.Add(Coins);
        PlaySound(SoundName.SpendCoin);
    }
}