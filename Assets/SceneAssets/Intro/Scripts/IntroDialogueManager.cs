using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class IntroDialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Choice
    {
        public string text;
        public int nextDialogueId;
    }

    [System.Serializable]
    public class Dialogue
    {
        public int id;
        public string characterName;
        public string dialogue;
        public List<Choice> choices;
        public int nextDialogueId;
    }

    [System.Serializable]
    public class DialogueList
    {
        public List<Dialogue> scenario;
    }
    
    [SerializeField] GameObject CharactorNameGameObject;
    [SerializeField] GameObject dialogueGameObject;
    [SerializeField] GameObject choiceBox;
    [SerializeField] GameObject choiceButton;

    TextMeshProUGUI charactorNameText;
    TextMeshProUGUI dialogueText;
    List<Dialogue> scenario;
    int dialogueIdNow = 1;
    bool waitingForChoice = false;

    void LoadDialogue()
    {
        charactorNameText = CharactorNameGameObject.GetComponentInChildren<TextMeshProUGUI>();
        dialogueText = dialogueGameObject.GetComponentInChildren<TextMeshProUGUI>();

        TextAsset jsonText = Resources.Load<TextAsset>("Dialogue/intro");
        DialogueList dialogueList = JsonUtility.FromJson<DialogueList>(jsonText.text);
        scenario = dialogueList.scenario;
    }

    void ShowChoices(Dialogue dialogue)
    {
        if (waitingForChoice) return;
        waitingForChoice = true;
        for(int i = 0; i < dialogue.choices.Count; i++)
        {
            int index = i;
            GameObject tmpChoice = Instantiate(choiceButton, choiceBox.transform);
            tmpChoice.GetComponentInChildren<TextMeshProUGUI>().text = dialogue.choices[index].text;
            tmpChoice.GetComponent<Button>().onClick.AddListener(() => {
                dialogueIdNow = dialogue.choices[index].nextDialogueId;
                choiceBox.SetActive(false);
                waitingForChoice = false;
                ShowNextDialogue();
            });
        }
        choiceBox.SetActive(true);
    }

    void ShowNextDialogue()
    {
        Dialogue dialogueNow = scenario.Find(d => d.id == dialogueIdNow);

        if (dialogueNow == null)
        {
            Debug.Log("End of scenario.");
            return;
        }

        charactorNameText.text = dialogueNow.characterName;
        dialogueText.text = dialogueNow.dialogue;

        if(dialogueNow.choices.Count != 0)
        {
            ShowChoices(dialogueNow);
        }
        else if(dialogueNow.nextDialogueId != 0)
        {
            dialogueIdNow = dialogueNow.nextDialogueId;
        }
        else
        {
            dialogueIdNow++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        choiceBox.SetActive(false);
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
