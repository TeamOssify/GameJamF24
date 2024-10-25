    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public sealed class DialogueUI : MonoBehaviour {
        [SerializeField]
        private TMP_Text textField;

        [SerializeField]
        private GameObject speaker;

        private Queue<string> _dialogue;

        private bool _dialogueRunning;

        public void ShowDialogueStrings(string[] dialogue) {
            if (dialogue.Length == 0) {
                return;
            }

            _dialogue = new Queue<string>(dialogue);
            StartCoroutine(ShowDialogueString(_dialogue.Dequeue()));
        }

        private IEnumerator ShowDialogueString(string dialogue) {
            if (dialogue == null) {
                yield break;
            }

            _dialogueRunning = true;

            textField.text = "";
            foreach (var c in dialogue) {
                textField.text += c;
                yield return null;
            }

            _dialogueRunning = false;
        }

        public void NextDialogue() {
            if (_dialogueRunning) {
                return;
            }

            if (!_dialogue.TryDequeue(out var dialogue)) {
                speaker.SetActive(false);
                return;
            }

            StartCoroutine(ShowDialogueString(dialogue));
        }
    }