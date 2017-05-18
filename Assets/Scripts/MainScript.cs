using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

using SimpleJSON;

public class MainScript : MonoBehaviour {

    public Text QuestionText, DashesText;
    public Image HangmanImage;
    public Sprite[] HangmanSprites;

    private int currentHangmanSprite = 0;
    private const int TOTAL_HANGMAN_SPRITES = 8;

    private Dictionary<string, string> gameDict;
    private string answer;

	void Start () {
        gameDict = new Dictionary<string, string>();
        loadDictionary("FruitsDictionary", gameDict); // file should be without ext - thanks unity !
        loadDictionary("SportsDictionary", gameDict);
        pickRandomQuestion();
    }
	
    public void OnRestartClicked() {
        currentHangmanSprite = 0;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
        pickRandomQuestion();
    }

    public void OnGuessSubmitted(Button button) {
        char letter = button.GetComponentInChildren<Text>().text.ToCharArray()[0];
        if (answer.Contains(letter)) {
            Debug.Log("!");
        }
        else
        {
            if (CheckLoseCondition()) { Debug.Log("You lost the game"); }
            else { DrawNextHangmanPart(); }
        }
    }

    private void pickRandomQuestion()
    {
        int randInt = Random.Range(0, gameDict.Count);
        QuestionText.text = gameDict.ElementAt(randInt).Key;
        answer = gameDict.ElementAt(randInt).Value.ToUpper();
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < answer.Length; i++) { sb.Append("_ "); }
        DashesText.text = sb.ToString();
        Debug.Log("Answer " + answer);
    }

    private void loadDictionary(string dictFileName, Dictionary<string, string> outputDict)
    {
        TextAsset ta = Resources.Load(dictFileName) as TextAsset;
        JSONObject jsonObj = (JSONObject)JSON.Parse(ta.text);
        foreach (var key in jsonObj.GetKeys()) { outputDict[key] = jsonObj[key]; }
    }

    private void DrawNextHangmanPart() {
        currentHangmanSprite = ++currentHangmanSprite % TOTAL_HANGMAN_SPRITES;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
    }

    private bool CheckWinCondition() { return false; }

    private bool CheckLoseCondition() { return currentHangmanSprite == TOTAL_HANGMAN_SPRITES-1; }

}
