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

        private bool _foodNaviDone = false; 
        private bool _foodEngiDone = false;
        private bool _foodOffiDone = false;

        public Preference naviPref;
        public Preference engiPref;
        public Preference offiPref;

        private bool _canTalk = true;

        private void Awake() 
        {
            if (Instance != null)
            {
                Debug.LogWarning("Found more than one DialogueTrigger in the scene");
            }
            Instance = this;
        }

        public void FoodDone(Preference pref)
        {
            if(engiPref == pref)
            {
                _foodEngiDone = true;
            }
            else if(naviPref == pref)
            {
                _foodNaviDone = true;
            }
            else if(offiPref == pref)
            {
                _foodOffiDone = true;
            }
            else
            {
                Debug.LogError("Could not match preference for " + pref);
            }
            CheckChapterDone();
        }

        public void CheckChapterDone()
        {
            if (_foodNaviDone && _foodEngiDone && _foodOffiDone)
            {
                _foodNaviDone = false;
                _foodEngiDone = false;
                _foodOffiDone = false;
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
            PanelsController.Instance.HidePanels();
            _canTalk = false;
        }

        private void OnMouseDown()
        {
            if(_canTalk)
                EnterDialogue();
        }
    }
}