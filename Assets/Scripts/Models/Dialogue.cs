public sealed class DialogueFile {
    public DialogueLocation[] LocationDialogue { get; set; }
}

public sealed class DialogueLocation {
    public Location Location { get; set; }
    public DialogueTree[] Dialogue { get; set; }
}

public sealed class DialogueTree {
    public float PercentSanityRequired { get; set; }
    public string[] Strings { get; set; }
}