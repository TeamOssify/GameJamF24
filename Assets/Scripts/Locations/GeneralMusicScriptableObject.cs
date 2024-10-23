using UnityEngine;

[CreateAssetMenu(fileName = nameof(GeneralMusicScriptableObject), menuName = "Scriptable Objects/General Music")]
public sealed class GeneralMusicScriptableObject : ScriptableObject {
    [SerializeField]
    private AudioClip[] musicSane;

    [SerializeField]
    private AudioClip[] musicAnxious;

    [SerializeField]
    private AudioClip[] musicInsane;

    public AudioClip MusicSane => musicSane[Random.Range(0, musicSane.Length)];

    public AudioClip MusicAnxious => musicAnxious[Random.Range(0, musicAnxious.Length)];

    public AudioClip MusicInsane => musicInsane[Random.Range(0, musicInsane.Length)];
}