public enum HealthChangeType
{
    Damage,
    Heal,
    Death
}

public sealed class HealthChangedEvent
{
    public CardBase Target { get; }
    public CardBase Source { get; }
    public HealthChangeType ChangeType { get; }
    public double PreviousHealth { get; }
    public double CurrentHealth { get; }
    public double Amount { get; }

    public HealthChangedEvent(
        CardBase target,
        CardBase source,
        HealthChangeType changeType,
        double previousHealth,
        double currentHealth)
    {
        Target = target;
        Source = source;
        ChangeType = changeType;
        PreviousHealth = previousHealth;
        CurrentHealth = currentHealth;
        Amount = System.Math.Abs(currentHealth - previousHealth);
    }
}

public static class GameplayEventHub
{
    public static readonly GameplaySubject<HealthChangedEvent> HealthChanged =
        new GameplaySubject<HealthChangedEvent>();
}
