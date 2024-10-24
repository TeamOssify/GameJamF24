using UnityEngine;

[CreateAssetMenu(fileName = nameof(InsaneMusicScriptableObject), menuName = "Scriptable Objects/Insane Music")]
public sealed class InsaneMusicScriptableObject : ScriptableObject {
    [SerializeField]
    private AudioClip[] musicInsane;

    public AudioClip MusicInsane => musicInsane[Random.Range(0, musicInsane.Length)];
}