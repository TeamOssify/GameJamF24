public sealed record MetricChangedArgs {
    public MetricChangedArgs(float oldValue, float newValue) {
        OldValue = oldValue;
        NewValue = newValue;
    }

    public float OldValue { get; }
    public float NewValue { get; }
}