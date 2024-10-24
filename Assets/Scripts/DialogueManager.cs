using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    public TextAsset inputTextFile;

    private string entireText;
    private List<string> line;
    private int lineNum = 1;


    void Start()
    {
        entireText = inputTextFile.text;
        line = new List<string>();
        line.AddRange(entireText.Split("\n"));
        Debug.Log(line[4]);

        textField.text = line[0];
    }

    public void nextText() {
        if (lineNum < line.Count) {
            textField.text = line[lineNum];
            lineNum++;
        } else {
            textField.text = "";
        }
    }
}
