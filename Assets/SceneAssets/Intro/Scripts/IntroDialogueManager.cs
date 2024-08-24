using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class IntroDialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string characterName;
        public string dialogue;
    }

    [System.Serializable]
    public class DialogueList
    {
        public List<Dialogue> introduction;
    }
    
    [SerializeField] GameObject CharactorNameGameObject;
    [SerializeField] GameObject dialogueGameObject;

    TextMeshProUGUI charactorNameText;
    TextMeshProUGUI dialogueText;
    DialogueList dialogueList;
    int dialogueLengths;
    int dialogueCounter = -1;

    void LoadDialogue()
    {
        charactorNameText = CharactorNameGameObject.GetComponentInChildren<TextMeshProUGUI>();
        dialogueText = dialogueGameObject.GetComponentInChildren<TextMeshProUGUI>();

        TextAsset jsonText = Resources.Load<TextAsset>("Dialogue/intro");
        dialogueList = JsonUtility.FromJson<DialogueList>(jsonText.text);

        dialogueLengths = dialogueList.introduction.Count;
    }

    void ShowNextDialogue()
    {
        if (dialogueCounter + 1 == dialogueLengths)
        {
            Debug.Log("End of scenario.");
            return;
        }
        dialogueCounter++;

        charactorNameText.text = dialogueList.introduction[dialogueCounter].characterName;
        dialogueText.text = dialogueList.introduction[dialogueCounter].dialogue;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadDialogue();
        ShowNextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            ShowNextDialogue();
        }
    }
}
