using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroDialogueManager : MonoBehaviour
{
    const int UNDEF = -77;    

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
        public string characterName = "";
        public string dialogue = "";
        public int sprite = UNDEF;
        public int background = UNDEF;
        public int fontSize = UNDEF;
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
    [SerializeField] GameObject nextTriangle;
    [SerializeField] GameObject sprite; 
    [SerializeField] GameObject background;
    [SerializeField] TextMeshProUGUI scenarioText;
    [SerializeField] string dialogueJsonName;
    [SerializeField] float typeSpeed = 0.1f;
    [SerializeField] float nextTriangleSpeed;
    [SerializeField] float nextTriangleTravelDistance;
    [SerializeField] int defaultFontSize = 80;
    [SerializeField] string nextSceneName;
    [SerializeField] List<Sprite> backgrounds;
    [SerializeField] List<Sprite> sprites;

    TextMeshProUGUI charactorNameText;
    TextMeshProUGUI dialogueText;
    List<Dialogue> scenario;
    Vector3 initialTrianglePositoion;
    int dialogueIdNow = 0;
    int dialogueIdNext = 1;
    int dialogueCharNow = 0;
    bool waitingForChoice = false;

    void LoadAllDialogues()
    {
        charactorNameText = CharactorNameGameObject.GetComponentInChildren<TextMeshProUGUI>();
        dialogueText = dialogueGameObject.GetComponentInChildren<TextMeshProUGUI>();

        TextAsset jsonText = Resources.Load<TextAsset>("Dialogue/"+dialogueJsonName);
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
                SceneManager.LoadScene(nextSceneName);

                Debug.Log("End of scenario.");
                return;
            }
            nextTriangle.transform.DOKill();

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
            
            if(dialogueNext.sprite != UNDEF)
            {
                if(dialogueNext.sprite == -1)
                {
                    sprite.SetActive(false);
                }
                else
                {
                    sprite.SetActive(true);
                    sprite.GetComponent<SpriteRenderer>().sprite = sprites[dialogueNext.sprite];
                }
            }

            if(dialogueNext.background != UNDEF)
            {
                if(dialogueNext.background == -1)
                {
                    background.SetActive(false);
                }
                else
                {
                    background.SetActive(true);
                    background.GetComponent<SpriteRenderer>().sprite = backgrounds[dialogueNext.background];
                }
            }

            scenarioText.fontSize = dialogueNext.fontSize==UNDEF ? defaultFontSize : dialogueNext.fontSize;

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
            nextTriangle.transform.position = initialTrianglePositoion;
            nextTriangle.transform.DOMove(
                nextTriangle.transform.position - new Vector3(0f, nextTriangleTravelDistance, 0f), 
                nextTriangleSpeed )
                .SetLoops(-1, LoopType.Yoyo)
                .OnStart(() => { nextTriangle.SetActive(true); })
                .OnKill(() => { nextTriangle.SetActive(false); });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        choiceBox.SetActive(false);
        LoadAllDialogues();
        UpdateDialogue();
        initialTrianglePositoion = nextTriangle.transform.position;
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
