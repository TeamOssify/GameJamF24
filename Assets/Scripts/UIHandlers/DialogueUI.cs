    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class DialogueUI : MonoBehaviour {
        [SerializeField]
        private TMP_Text textField;

        [SerializeField]
        private GameObject speaker;

        private Image _image;

        private Queue<string> _dialogue;

        private bool _dialogueRunning;

        private void Awake() {
            _image = speaker.GetComponent<Image>();
        }

        public void ShowDialogueStrings(DialogueTree dialogue) {
            if (dialogue.strings.Length == 0) {
                return;
            }

            _image.sprite = dialogue.sprite;
            _dialogue = new Queue<string>(dialogue.strings);
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