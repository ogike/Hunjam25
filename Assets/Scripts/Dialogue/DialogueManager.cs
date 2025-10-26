// All of the code in this folder is copied/derived from Github user shapedbyrainstudios
// Repo: https://github.com/shapedbyrainstudios/ink-dialogue-system

using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        public TextAsset inkJson;
        [SerializeField] private TextAsset loadGlobalsJSON;
        
        [Header("Params")]
        public float typingSpeed = 0.04f;
        private bool _npcDialogueActive;
        private GameObject audioLoopDialog;
        private GameObject audioLoopCooking;


        private Story currentStory;
        public bool dialogueIsPlaying { get; private set; }

        private bool _canContinueToNextLine = false;
        private bool _continueInputBuffered = false;
        private bool _isPausedFromInk;
        private int _currentChoiceIndex = 0;
        private bool _hasShownChoice;
        private bool npcTalking;
        private string _npcTitle;
        private bool _switchedChoiceAlready;

        private Coroutine displayLineCoroutine;

        public const string PLAYER_STRING_TAG = "CHEF";
        public const string ENGI_STRING_TAG = "ENGI";
        public const string OFFI_STRING_TAG = "OFFI";
        public const string NAVI_STRING_TAG = "NAVI";

        private DialogueVariables dialogueVariables;

        private const float _floatingPointTolerance = 0.01f;

        public bool debugMode;

        private DialogueUI _ui;

        private void Awake() 
        {
            if (Instance != null)
            {
                Debug.LogWarning("Found more than one Dialogue Manager in the scene");
            }
            Instance = this;

            dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        }

        private void Start() 
        {

            _ui = DialogueUI.Instance;
            dialogueIsPlaying = false;
            
            currentStory = new Story(inkJson.text);
            BindExternalFunctions();
            StartListeningToStoryVariable(currentStory);
            DialogueTrigger.Instance.SetCanTalk(true);
        }

        private void Update() 
        {
            // return right away if dialogue isn't playing
            if (!dialogueIsPlaying) 
            {
                return;
            }
            

            // handle continuing to the next line in the dialogue when submit is pressed
            if (UserInput.Instance.InteractButtonPressedThisFrame)
            {
                if (_canContinueToNextLine)
                {
                    if (currentStory.currentChoices.Count == 0)
                        ContinueStory();
                    else
                    {
                        if (_hasShownChoice)
                            MakeChoice();
                        else
                            ShowChoicePanel();
                    }
                }
                else
                {
                    _continueInputBuffered = true;
                }
            }

            if (currentStory.currentChoices.Count > 0)
            {
                float xInput = UserInput.Instance.MoveInput.y;
                
                // Do not rapidly go thru choices if direction is held down
                if (_switchedChoiceAlready && xInput == 0)
                {
                    _switchedChoiceAlready = false;
                }
                
                if (!_switchedChoiceAlready)
                {
                    if (Math.Abs(xInput - 1) < 0.1f)
                    {
                        NextChoice();
                        _switchedChoiceAlready = true;
                    }
                    else if (Math.Abs(xInput - (-1)) < 0.1f)
                    {
                        PreviousChoice();
                        _switchedChoiceAlready = true;
                    }
                }
            }
        }

        //TODO: extract this to its own class
        private void BindExternalFunctions()
        {
            currentStory.BindExternalFunction ("fadeOutSequence", (float fadeOut, float wait, float fadeIn) => {
                if(debugMode) Debug.Log("InkDebug: Fading out for " + fadeOut + " seconds...");
                
                // Call this here too incase it would fall on the next frame because of StartCoroutines
                _isPausedFromInk = true;
                StartCoroutine(PauseLines(fadeOut + wait + fadeIn));
                StartCoroutine(ScreenFade.Instance.FadeOutSequence(fadeOut, wait, fadeIn));
            });
            
            currentStory.BindExternalFunction ("wait", (float time) => {
                if(debugMode) Debug.Log("InkDebug: Waiting for " + time + " seconds...");
                StartCoroutine(PauseLines(time));
            });
        }

        public void EnterMonologueMode(String inkPath)
        {
            currentStory.ChoosePathString(inkPath);
            dialogueIsPlaying = true;
            _hasShownChoice = false;
            _npcDialogueActive = false;

            ContinueStory();
        }

        public void EnterDialogueMode()
        {
            audioLoopCooking.GetComponent<AudioSource>().Pause();
            audioLoopDialog.GetComponent<AudioSource>().Play(0);
            //currentStory.ChoosePathString(inkPath);
            dialogueIsPlaying = true;
            _hasShownChoice = false;
            _npcDialogueActive = true;

            _canContinueToNextLine = false;
            ContinueStory();
        }

        private IEnumerator ExitDialogueMode() 
        {
            yield return new WaitForSeconds(0.2f);

            StopListeningToStoryVariable(currentStory);
            audioLoopCooking.GetComponent<AudioSource>().Play(0);
            audioLoopDialog.GetComponent<AudioSource>().Pause();

            dialogueIsPlaying = false;
            _ui.HideDialogueBoxes();
        }

        private void ContinueStory() 
        {
            if (_isPausedFromInk)
            {
                // Unpause should call ReContinueStory when we are ready, dont do anything until then
                if(debugMode) Debug.Log("Ink: Tried to continued story while Ink is paused, skipping...");
                return;
            }
            
            if (currentStory.canContinue) 
            {
                // set text for the current dialogue line
                if (displayLineCoroutine != null) 
                {
                    StopCoroutine(displayLineCoroutine);
                }

                string currentLine = currentStory.Continue();
                
                if (_isPausedFromInk)
                {
                    // Unpause should call ReContinueStory when we are ready, dont do anything until then
                    if(debugMode) Debug.Log("Ink: Tried to continued story while Ink is paused, skipping...");
                    return;
                }
                
                string lineToDisplay = ParseLine(currentLine);
                HandleTags(currentStory.currentTags);
                
                if(debugMode) Debug.Log("InkDebug: Continued story, ispaused state: " + _isPausedFromInk + ", next line: \n" + lineToDisplay);
                
                displayLineCoroutine = StartCoroutine(DisplayLine(lineToDisplay));
            }
            else 
            {
                if(debugMode) Debug.Log("InkDebug: Cant continue story, exiting");
                StartCoroutine(ExitDialogueMode());
            }
        }

        private void ReContinueStory()
        {
            string lineToDisplay = ParseLine(currentStory.currentText);
            HandleTags(currentStory.currentTags);
                
            if(debugMode) Debug.Log("InkDebug: Recontinued story, ispaused state: " + _isPausedFromInk + ", next line: \n" + lineToDisplay);
                
            displayLineCoroutine = StartCoroutine(DisplayLine(lineToDisplay));
        }

        //Needed for the GameEnd script to access global variable states that we manage
        public void StartListeningToStoryVariable(Story story)
        {
            dialogueVariables.StartListening(story);
        }
        
        public void StopListeningToStoryVariable(Story story)
        {
            dialogueVariables.StopListening(story);
        }

        //TODO: seperate this into the two bubbles
        private IEnumerator DisplayLine(string line)
        {
            if (line.Trim().Length == 0)
            {
                ContinueStory();
                yield break;
            }
            
            _canContinueToNextLine = false;
            _continueInputBuffered = false;
            bool isAddingRichTextTag = false;
            
            if(npcTalking) _ui.LoadLineNpc(line, _npcTitle);
            else           _ui.LoadLinePlayer(line);

            // wait to reset frame input
            yield return new WaitForSeconds(0);

            int visibleLetters = 0;
            
            // display each letter one at a time
            foreach (char letter in line.ToCharArray())
            {
                
                // if the submit button is pressed, finish up displaying the line right away
                if (_continueInputBuffered) 
                {
                    if(debugMode) Debug.Log("InkDebug: Skipping this line.");
                    
                    if(npcTalking) _ui.SetNpcTextVisibleCharacters(line.Length);
                    else           _ui.SetPlayerTextVisibleCharacters(line.Length);

                    break;
                }

                // check for rich text tag, if found, add it without waiting
                if (letter == '<' || isAddingRichTextTag) 
                {
                    isAddingRichTextTag = true;
                    if (letter == '>')
                    {
                        isAddingRichTextTag = false;
                    }
                }
                
                // if not (or not anymore) add the next letter and wait a small time
                if(!isAddingRichTextTag)
                {
                    visibleLetters++;
                    if(npcTalking) _ui.SetNpcTextVisibleCharacters(visibleLetters);
                    else           _ui.SetPlayerTextVisibleCharacters(visibleLetters);
                    yield return new WaitForSeconds(typingSpeed);
                }
            }

            // actions to take after the entire line has finished displaying
            _ui.ShowContinueIcon();

            _canContinueToNextLine = true;
            _continueInputBuffered = false;
        }

        private string ParseLine(string line)
        {
            string ret = line.Trim();
            string[] parts = ret.Split(':');
            if (parts.Length == 1)
            {
                npcTalking = false;
            }
            else if (parts.Length >= 2)
            {
                string characterName = parts[0].Trim();
                if (characterName == PLAYER_STRING_TAG)
                {
                    npcTalking = false;
                }
                else if (_npcDialogueActive)
                {
                    switch (characterName)
                    {
                        case ENGI_STRING_TAG:
                            npcTalking = true;
                            break;
                        case NAVI_STRING_TAG:
                            npcTalking = true;
                            break;
                        case OFFI_STRING_TAG:
                            npcTalking = true;
                            break;
                        default:
                            Debug.LogError($"Character name {characterName} could not be parsed!");
                            break;
                    }

                    if (npcTalking)
                        _npcTitle = characterName;
                }
                else
                {
                    return "Unable to handle character name in: " + ret;
                }

                return parts[1].Trim();
            }
            // else
                // return "Too many ':' characters in line: " + ret;
            
            return ret;
        }


        private void HandleTags(List<string> currentTags)
        {
            // loop through each tag and handle it accordingly
            foreach (string tag in currentTags) 
            {
                switch (tag.Trim())
                {
                    default:
                        Debug.LogWarning("Tag could not be appropriately parsed: " + tag);
                        break;
                }
            }
        }
        
        public void MakeChoice()
        {
            if (_canContinueToNextLine) 
            {
                if(debugMode) Debug.Log("InkDebug: Chosen choice index: " + _currentChoiceIndex 
                                                + "\ntext: " + currentStory.currentChoices[_currentChoiceIndex].text);
                currentStory.ChooseChoiceIndex(_currentChoiceIndex);
                ContinueStory();
                _hasShownChoice = false;
            }
        }

        private void ShowChoicePanel()
        {
            _hasShownChoice = true;
            
            _currentChoiceIndex = 0;
            List<string> choiceTexts = new List<string>();
            currentStory.currentChoices.ForEach(choice => choiceTexts.Add(choice.text));
            _ui.ShowChoicesPanel(choiceTexts, _currentChoiceIndex);
        }

        public void NextChoice()
        {
            if(currentStory.currentChoices.Count == 0) return;

            _currentChoiceIndex++;
            if (_currentChoiceIndex >= currentStory.currentChoices.Count)
                _currentChoiceIndex = 0;
            
            if(debugMode) Debug.Log($"InkDebug: new choice is {_currentChoiceIndex}");
            
            _ui.ChangeCoice(_currentChoiceIndex);
        }

        public void PreviousChoice()
        {
            if(currentStory.currentChoices.Count == 0) return;

            _currentChoiceIndex--;
            if (_currentChoiceIndex < 0)
                _currentChoiceIndex = currentStory.currentChoices.Count-1;
            
            if(debugMode) Debug.Log($"InkDebug: new choice is {_currentChoiceIndex}");
            
            _ui.ChangeCoice(_currentChoiceIndex);
        }

        public Ink.Runtime.Object GetVariableState(string variableName) 
        {
            Ink.Runtime.Object variableValue = null;
            dialogueVariables.variables.TryGetValue(variableName, out variableValue);
            if (variableValue == null) 
            {
                Debug.LogWarning("Ink Variable was found to be null: " + variableName);
            }
            return variableValue;
        }

        private IEnumerator PauseLines(float seconds)
        {
            if(debugMode) Debug.Log("Ink: pausing lines");
            _ui.HideDialogueBoxes();
            _isPausedFromInk = true;
            _canContinueToNextLine = false;
            
            yield return new WaitForSeconds(seconds);
            
            if(debugMode )Debug.Log("Ink: continuing lines");
            _isPausedFromInk = false;
            _canContinueToNextLine = true;
            ReContinueStory();
        }

        public void SetVariableBool(string var, bool val)
        {
            Debug.Log("Value before: " + currentStory.variablesState[var]);
            currentStory.variablesState[var] = val;
            // currentStory.variablesState.SetGlobal(var, (Ink.Runtime.BoolValue);
            Debug.Log("Value after: " + currentStory.variablesState[var]);
        }
        
        public void SetVariableString(string var, string val)
        {
            Debug.Log("Value before: " + currentStory.variablesState[var]);
            currentStory.variablesState[var] = val;
            // currentStory.variablesState.SetGlobal(var, (Ink.Runtime.BoolValue);
            Debug.Log("Value after: " + currentStory.variablesState[var]);
        }

        // This method will get called anytime the application exits.
        // Depending on your game, you may want to save variable state in other places.
        public void OnApplicationQuit() 
        {
            StopListeningToStoryVariable(currentStory);
            dialogueVariables.SaveVariables();
        }

    }
}
