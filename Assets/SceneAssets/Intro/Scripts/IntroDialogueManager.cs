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
        public string dialogue = "";
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
    [SerializeField] float typeSpeed = 0.2f;

    TextMeshProUGUI charactorNameText;
    TextMeshProUGUI dialogueText;
    List<Dialogue> scenario;
    int dialogueIdNow = 0;
    int dialogueIdNext = 1;
    int dialogueCharNow = 0;
    bool waitingForChoice = false;

    void LoadAllDialogues()
    {
        charactorNameText = CharactorNameGameObject.GetComponentInChildren<TextMeshProUGUI>();
        dialogueText = dialogueGameObject.GetComponentInChildren<TextMeshProUGUI>();

        TextAsset jsonText = Resources.Load<TextAsset>("Dialogue/intro");
        DialogueList dialogueList = JsonUtility.FromJson<DialogueList>(jsonText.text);
        scenario = dialogueList.scenario;
        scenario.Add(new Dialogue());
    }

    void ShowChoices(Dialogue dialogue)
    {
        waitingForChoice = true;
        for(int i = 0; i < dialogue.choices.Count; i++)
        {
            int index = i;
            GameObject tmpChoice = Instantiate(choiceButton, choiceBox.transform);
            tmpChoice.GetComponentInChildren<TextMeshProUGUI>().text = dialogue.choices[index].text;
            tmpChoice.GetComponent<Button>().onClick.AddListener(() => {
                dialogueIdNext = dialogue.choices[index].nextDialogueId;
                choiceBox.SetActive(false);
                waitingForChoice = false;
                UpdateDialogue();
            });
        }
        choiceBox.SetActive(true);
    }

    IEnumerator ShowDialogue(string text)
    {
        for(; dialogueCharNow <= text.Length; dialogueCharNow++)
        {
            dialogueText.text = text.Substring(0, dialogueCharNow);
            yield return new WaitForSeconds(typeSpeed);
        }
        UpdateDialogue(false);
    }

    void UpdateDialogue(bool getNext = true)
    {
        if (waitingForChoice) return;
        Dialogue dialogueNow = scenario.Find(d => d.id == dialogueIdNow);
        Dialogue dialogueNext = scenario.Find(d => d.id == dialogueIdNext);

 
        if (dialogueNow.dialogue.Length <= dialogueCharNow && getNext)
        {
            if (dialogueNext == null)
            {
                Debug.Log("End of scenario.");
                return;
            }
            charactorNameText.text = dialogueNext.characterName;
            dialogueCharNow = 1;
            dialogueIdNow = dialogueIdNext;

            if(dialogueNext.nextDialogueId != 0)
            {
                dialogueIdNext = dialogueNext.nextDialogueId;
            }
            else
            {
                dialogueIdNext++;
            }

            StartCoroutine(ShowDialogue(dialogueNext.dialogue));
        }
        else
        {
            dialogueCharNow = dialogueNow.dialogue.Length;
            dialogueText.text = dialogueNow.dialogue;
            if(dialogueNow.choices.Count != 0)
            {
                ShowChoices(dialogueNow);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        choiceBox.SetActive(false);
        LoadAllDialogues();
        UpdateDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateDialogue();
        }
    }
}
