public sealed record MentalStateChangedArgs {
    public MentalStateChangedArgs(MentalState oldState, MentalState newState) {
        OldState = oldState;
        NewState = newState;
    }

    public MentalState OldState { get; }
    public MentalState NewState { get; }
}