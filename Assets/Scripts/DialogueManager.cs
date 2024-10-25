using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour {
    [SerializeField]
    private LocationManager locationManager;

    [SerializeField]
    private TextAsset dialogueFile;

    [SerializeField]
    private GameObject dialogueBox;

    private DialogueUI _dialogueUI;

    private DialogueFile _dialogue;

    private void Awake() {
        // _dialogueUI = dialogueBox.GetComponent<DialogueUI>();
        _dialogue = JsonUtility.FromJson<DialogueFile>(dialogueFile.text);
        dialogueBox.SetActive(false);
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
        dialogueBox.SetActive(true);
        _dialogueUI.ShowDialogueStrings(chosen);
    }
}