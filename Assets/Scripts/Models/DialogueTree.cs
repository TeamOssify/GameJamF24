using System;
using UnityEngine;

[Serializable]
public sealed class DialogueTree : MonoBehaviour {
    [SerializeField]
    public float percentSanityRequired = 1f;

    [SerializeField]
    public string[] strings;

    [SerializeField]
    public Sprite sprite;
}