using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour {
    [SerializeField]
    private LocationManager locationManager;

    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private TextAsset dialogueFile;

    private DialogueFile _dialogue;

    private void Awake() {
        _dialogue = JsonUtility.FromJson<DialogueFile>(dialogueFile.text);
    }

    private void Start() {
        CheckDialogue();
    }

    private void CheckDialogue()
    {
        var currentLocation = locationManager.CurrentLocation;
        var locationDialogue = _dialogue.LocationDialogue.FirstOrDefault(x => x.Location == currentLocation);
        if (locationDialogue == null) {
            Debug.LogFormat("No dialogue for the current location was found. ({0})", currentLocation);
            return;
        }

        var sanityManager = SanityManager.Instance;
        var currentSanityPercent = sanityManager.Sanity / sanityManager.MaxSanity;
        var availableDialogue = locationDialogue.Dialogue
            .Where(x => currentSanityPercent <= x.PercentSanityRequired)
            .ToArray();

        if (availableDialogue.Length == 0) {
            Debug.LogFormat("No available dialogue found for current sanity. ({0})", currentSanityPercent);
            return;
        }

        var chosen = availableDialogue[Random.Range(0, availableDialogue.Length)].Strings;
        StartCoroutine(ShowDialogueStrings(chosen));
    }

    private IEnumerator ShowDialogueStrings(string[] dialogue) {
        foreach (var s in dialogue) {
            yield return ShowDialogueString(s);


        }
    }

    private IEnumerator ShowDialogueString(string dialogue) {
        textField.text = "";

        foreach (var c in dialogue) {
            textField.text += c;
            yield return null;
        }
    }
}