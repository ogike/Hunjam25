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

        public bool food1Done = false; 
        public bool food2Done = false; 
        

        private bool _playerInRange;

        private void Awake() 
        {
            _playerInRange = false;
            visualCue.SetActive(false);
        }

        private void Update() 
        {
            if (_playerInRange && !DialogueManager.Instance.dialogueIsPlaying) 
            {
                visualCue.SetActive(true);
                if (UserInput.Instance.InteractButtonPressedThisFrame)
                {
                    EnterDialogue();
                }
            }
            else 
            {
                visualCue.SetActive(false);
            }
        }

        public void Food1Done()
        {
            food1Done = true;
            if(food2Done) EnterDialogue(); 
        }

        public void CheckChapterDone()
        {
            
        }
        
        public void EnterDialogue()
        {
            DialogueManager.Instance.EnterDialogueMode();
        }
    }
}