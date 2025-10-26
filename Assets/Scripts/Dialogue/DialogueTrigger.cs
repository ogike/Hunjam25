using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Dialogue
{
    /// <summary>
    /// Holds everything needed for a dialogue from NPCs side
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        public static DialogueTrigger Instance { get; private set; }

        [Header("UI")]
        public GameObject visualCue;

        private bool _food1Done = false; 
        private bool _food2Done = false;

        private bool _canTalk = true;

        public void Food1Done()
        {
            _food1Done = true;
            CheckChapterDone();
        }
        
        public void Food2Done()
        {
            _food2Done = true;
            CheckChapterDone();
        }

        public void CheckChapterDone()
        {
            if (_food1Done && _food2Done)
            {
                _food1Done = false;
                _food2Done = false;
                //TODO: fade to black
                EnterDialogue();
            }
        }

        public void SetCanTalk(bool value)
        {
            _canTalk = value;
            visualCue.SetActive(value);
        }
        
        public void EnterDialogue()
        {
            DialogueManager.Instance.EnterDialogueMode();
            _canTalk = false;
        }

        private void OnMouseDown()
        {
            if(_canTalk)
                EnterDialogue();
        }
    }
}