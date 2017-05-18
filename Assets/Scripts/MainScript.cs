using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

using SimpleJSON;

public class MainScript : MonoBehaviour {

    public Text QuestionText, DashesText, ResultsText;
    public Image HangmanImage, FinalImage;
    public Sprite[] HangmanSprites;
    public Sprite WinSprite, LoseSprite;

    public GameObject MainDialogue, FinalDialogue;

    private int currentHangmanSprite = 0;
    private const int TOTAL_HANGMAN_SPRITES = 8;
    private const char PLACEHOLDER = '*';

    private Dictionary<string, string> gameDict;
    private string answer, userInput;

	void Start () {
        gameDict = new Dictionary<string, string>();
        LoadDictionary("FruitsDictionary", gameDict); // file should be without ext - thanks unity !
        LoadDictionary("SportsDictionary", gameDict);
        PickRandomQuestion();
    }
	
    public void OnRestartClicked() {
        MainDialogue.SetActive(true);
        FinalDialogue.SetActive(false);
        currentHangmanSprite = 0;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
        PickRandomQuestion();
    }

    public void OnGuessSubmitted(Button button) {
        char letter = button.GetComponentInChildren<Text>().text.ToCharArray()[0];
        if ( answer.Contains(letter) ) {
            UpdateAnswerText(letter);
            if ( CheckWinCondition() ) {
                Debug.Log("You won the game !");
                ShowFinalDialogue(true);
            }
        }
        else
        {
            if (CheckLoseCondition()) {
                Debug.Log("You lost the game");
                ShowFinalDialogue(false);
            }
            else { DrawNextHangmanPart(); }
        }
    }

    private void PickRandomQuestion()
    {
        int randInt = Random.Range(0, gameDict.Count);
        QuestionText.text = gameDict.ElementAt(randInt).Key;
        answer = gameDict.ElementAt(randInt).Value.ToUpper();
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < answer.Length; i++) { sb.Append(PLACEHOLDER); }
        DashesText.text = sb.ToString();
        userInput = sb.ToString();
        Debug.Log("Answer: " + answer);
    }

    private void LoadDictionary(string dictFileName, Dictionary<string, string> outputDict)
    {
        TextAsset ta = Resources.Load(dictFileName) as TextAsset;
        JSONObject jsonObj = (JSONObject)JSON.Parse(ta.text);
        foreach (var key in jsonObj.GetKeys()) { outputDict[key] = jsonObj[key]; }
    }

    private void UpdateAnswerText(char letter) {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++) {
            if (userInputArray[i] != PLACEHOLDER) { continue; } // already guessed
            if (answer[i] == letter) { userInputArray[i] = letter; }
        }
        userInput = new string(userInputArray);
        DashesText.text = userInput;
    }

    private void DrawNextHangmanPart() {
        currentHangmanSprite = ++currentHangmanSprite % TOTAL_HANGMAN_SPRITES;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
    }

    private bool CheckWinCondition() { return answer.Equals(userInput); }
    private bool CheckLoseCondition() { return currentHangmanSprite == TOTAL_HANGMAN_SPRITES-1; }

    private void ShowFinalDialogue(bool win) {
        MainDialogue.SetActive(false);
        FinalDialogue.SetActive(true);
        FinalImage.sprite = win ? WinSprite : LoseSprite;
        ResultsText.text = win ? "Victory !" : "Defeat !!!";
    }
}
