using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour {
    [SerializeField]
    private LocationManager locationManager;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private GameObject speaker;

    [SerializeField]
    private DialogueTree[] dialogue;

    private DialogueUI _dialogueUI;

    private void Awake() {
        _dialogueUI = dialogueBox.GetComponent<DialogueUI>();
        speaker.SetActive(false);
    }

    private void Start() {
        CheckDialogue();
    }

    private void CheckDialogue()
    {
        var sanityManager = SanityManager.Instance;
        var currentSanityPercent = sanityManager.Sanity / sanityManager.MaxSanity;
        var availableDialogue = dialogue
            .Where(x => currentSanityPercent <= x.percentSanityRequired)
            .ToArray();

        if (availableDialogue.Length == 0) {
            Debug.LogFormat("No available dialogue found for current sanity. ({0})", currentSanityPercent);
            return;
        }

        var chosen = availableDialogue[Random.Range(0, availableDialogue.Length)];
        speaker.SetActive(true);
        _dialogueUI.ShowDialogueStrings(chosen);
    }
}