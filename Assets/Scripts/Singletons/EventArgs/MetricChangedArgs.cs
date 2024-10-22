public sealed record MetricChangedArgs {
    public MetricChangedArgs(decimal oldValue, decimal newValue) {
        OldValue = oldValue;
        NewValue = newValue;
    }

    public decimal OldValue { get; }
    public decimal NewValue { get; }
}