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
1. MainScene: WordDashesView + HangmanDiagram + ControlPanel
2. FinalDialogue


Tech stack:
----------

Unity 5.6.1f1

Target platform:
---------------

PC, win 10, 64bit
