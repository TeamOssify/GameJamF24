using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour {
    [SerializeField]
    private LocationManager locationManager;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private DialogueTree dialogue1, dialogue2, dialogue3;

    [SerializeField]
    private Sprite saneSprite, insaneSprite;

    private DialogueTree[] Dialogue {
        get {
            var list = new List<DialogueTree>();

            if (dialogue1) {
                list.Add(dialogue1);
            }
            if (dialogue2) {
                list.Add(dialogue2);
            }
            if (dialogue3) {
                list.Add(dialogue3);
            }

            return list.ToArray();
        }
    }

    private DialogueUI _dialogueUI;

    private void Awake() {
        _dialogueUI = dialogueBox.GetComponent<DialogueUI>();
        dialogueBox.SetActive(false);
    }

    private void Start() {
        CheckDialogue();
    }

    private void CheckDialogue()
    {
        var sanityManager = SanityManager.Instance;
        var currentSanityPercent = sanityManager.Sanity / sanityManager.MaxSanity;
        var availableDialogue = Dialogue
            .Where(x => currentSanityPercent <= x.percentSanityRequired)
            .ToArray();

        if (availableDialogue.Length == 0) {
            Debug.LogFormat("No available dialogue found for current sanity. ({0})", currentSanityPercent);
            return;
        }

        var chosen = availableDialogue[Random.Range(0, availableDialogue.Length)].strings;
        dialogueBox.SetActive(true);
        _dialogueUI.ShowDialogueStrings(chosen);
    }
}