using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class ComboSystem : IComboSystem, IDisposable
{
    private const int Point = 1;
    private const int ComboWindow = 250; // Окно для комбо в секундах
    private const int ComboStarter = 2;
    private readonly ParticleViewText _comboView;
    private readonly ICoinStorage _coinStorage;

    private int _currentCombo;
    private CancellationTokenSource _comboTimerCts;

    public ComboSystem(IFactoryParticle factoryParticle, IPlayer player, ICoinStorage coinStorage)
    {
        _comboView = factoryParticle.Create(AssetProvider.ParticleExplosionCountPath, player.Transform) as ParticleViewText;
        _comboView.Stop();

        _comboView.transform.position = player.Transform.position + player.Transform.up * 5f;
        _coinStorage = coinStorage;
    }

    public void Add()
    {
        _currentCombo += Point;
        RestartComboTimer().Forget();
    }

    private async UniTaskVoid RestartComboTimer()
    {
        // Отменяем предыдущий таймер если он был
        _comboTimerCts?.Cancel();
        _comboTimerCts?.Dispose();

        _comboTimerCts = new CancellationTokenSource();

        try
        {
            await UniTask.Delay(ComboWindow, cancellationToken: _comboTimerCts.Token);

            CompleteCombo();
        }
        catch (OperationCanceledException)
        {

        }
    }

    private void CompleteCombo()
    {
        if (_currentCombo > ComboStarter)
        {
            Show();
            AddPoints();
            ResetCombo();
        }
    }

    public void Show()
    {
        _comboView.SetText($"x{_currentCombo}");
        _comboView.Play();
    }

    private void AddPoints()
    {
        _coinStorage.Add(_currentCombo);
    }

    private void ResetCombo()
    {
        _currentCombo = 0;
    }

    public void Dispose()
    {
        _comboTimerCts?.Cancel();
        _comboTimerCts?.Dispose();
    }
}