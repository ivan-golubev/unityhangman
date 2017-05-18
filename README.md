Hangman game: description
-------------------------

Hangman is a paper and pencil guessing game for two or more players.
One player thinks of a word, phrase or sentence and the other tries to guess it by suggesting letters or numbers,
 within a certain number of guesses.

The word to guess is represented by a row of dashes, representing each letter of the word.
If the guessing player suggests a letter which occurs in the word, the other player writes it in all its correct positions.
If the suggested letter or number does not occur in the word, the other player draws one element of a hanged man stick figure as a tally mark.

Dictionaries:
------------

1. Fruits
2. Sports

Questions and answers are stored in JSON
{"Question" : "Answer", ...}
format in separate files: Assets/Resources/FruitsDictionary.json and Assets/Resources/SportsDictionary.json.

Rules:
-------
user can make up to MAX_ERRORS=7 mistakes before hangman diagram is complete.
Word is selected at random from the input dictionaries.

Pictures:
----

Simple 2D images:
Assets/HangmanDiagram/[0,7].jpg

UI flow:
--------
1. ChooseTopicDialogue
2. MainScene: WordDashesView + HangmanDiagram + ControlPanel
3. WinLostDialogue

Game components:
---------------

1. UI: Choose topic start dialog
2. UI: Word dashes
3. UI: Hangman diagram
4. UI: Control panel: user input alphabet panel (English alphabet), restart game button
5. UI: Win/ Lose dialogue
Status string + restart button
6. Controller
7. Word validator

Events:
-------

1. OnGameStart
2. OnTopicSelected(string)
3. OnSymbolSelected(char)
4. OnGameEnd(bool: win/lose)
5. DrawNextDiagramElement

Game State:
----------
Topic - dictionary selected
Answer - word from the input dictionary json
WordDashesState - shows valid characters
HangmanState: int - number of errors

Main Logic:
------
1. User clicks on a character.

2.1 If word does not contain it:
- increment error counter: HangmanState
- update HangmanDiagram according to the number of errors
- if number of errors = MAX_ERRORS - go to LoseDialogue

2.2 Else:
- find indexes of input char in the answer
- update the WordDashes UI to show the characters
- if WordDashesState = Answer - go to WinDialogue

Tech stack:
----------

Unity 5.4.1

Target platform:
---------------

PC, win 10, 64bit
