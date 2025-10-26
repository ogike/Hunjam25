using System;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        
        [Serializable]
        public class DialogueBoxVariant
        {
            public RectTransform panelImg;
            public TextMeshProUGUI textField;

            // [HideInInspector] public ContentSizeFitter textFitter;
            [HideInInspector] public Image image;

            public void SetCachedValues()
            {
                // textFitter = textField.GetComponent<ContentSizeFitter>();
                image = panelImg.GetComponent<Image>();
            }

            public void ShowText(string text)
            {
                panelImg.gameObject.SetActive(true);
            
                textField.text = text;
                textField.maxVisibleCharacters = 0;
                // textFitter.SetLayoutHorizontal();
            }

            public void ShowAllText()
            {
                textField.maxVisibleCharacters = 99;
            }

            public void Hide()
            {
                panelImg.gameObject.SetActive(false);
                textField.text = "";
            }

            public void ApplyStyle(DialogueBoxStyle style)
            {
                Vector3 newScale = new Vector3(style.scale, style.scale, style.scale);
                // LeanTween.scale(panelImg, newScale, dialogueChoiceSelectionTime);
                panelImg.localScale = newScale;
                image.color = style.color;
                // LeanTween.color(image.color, style.color);
            }
        }

        [Serializable]
        public class DialogueBoxStyle
        {
            public float scale;
            public Color color;
        }
        
        public static DialogueUI Instance { get; private set; }
        
        [Header("NPC Dialogue UI")]
        private bool _showNpcBubble;
        [SerializeField] private DialogueBoxVariant npcDialogueBox;
        [SerializeField] private GameObject npcDialogueContinueIcon;

        [Header("Player Normal Dialogue UI")]
        private bool _showPlayerBubble;
        [SerializeField] private DialogueBoxVariant playerDialogueBox;

        [Header("Player Choice Dialogue UI")]
        [SerializeField] private List<DialogueBoxVariant> playerChoiceBoxes;
        private DialogueBoxVariant _curChoiceBox;
        private int _curChoice;

        public DialogueBoxStyle choiceStyleSelected;
        public DialogueBoxStyle choiceStyleNotSelected;

        [Header("Others")]
        public Camera cameraRef;

        [FormerlySerializedAs("minPaddingX")] [Header("Limitations")] 
        public float minCanvasPaddingX;
        [FormerlySerializedAs("minPaddingY")] public float minCanvasPaddingY;

        [Header("Tweening")] 
        public static float dialogueChoiceSelectionTime;
        
        //PRIVATES 
        private RectTransform _transform;
        private float _canvasScaleX;
        private float _canvasScaleY;
        private float _canvasWidth;
        private float _canvasHeight;
        private float _minPlayerPosX;
        private float _minPlayerPosY;

        private string _curInteractBlurb;

        private DialogueManager _manager;

        float _leftWidth;
        float _rightWidth;
        float _upHeight;
        float _downHeight;
        


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Found more than one DialogueUI in the scene");
            }
            Instance = this;
            _transform = GetComponent<RectTransform>();

            npcDialogueBox.SetCachedValues();
            playerDialogueBox.SetCachedValues();
            playerChoiceBoxes.ForEach((box) =>
            {
                box.SetCachedValues();
                box.ApplyStyle(choiceStyleNotSelected);
            });
        }

        private void Start()
        {
            _manager = DialogueManager.Instance;
            cameraRef = Camera.main;

            HideDialogueBoxes();
        }
        

        // private Vector2 ConstrainRectPosToCanvas(Vector3 pos, RectTransform rect)
        // {
        //     //width relative to the pivot + minPadding
        //     _leftWidth = rect.rect.width * rect.pivot.x * rect.localScale.x + minCanvasPaddingX;
        //     _rightWidth = rect.rect.width * (1 - rect.pivot.x) * rect.localScale.x + minCanvasPaddingX;
        //     
        //     //height relative to the picvot + minPadding
        //     _downHeight = rect.rect.height * rect.pivot.y * rect.localScale.y + minCanvasPaddingY;
        //     _upHeight = rect.rect.height * (1 - rect.pivot.y) * rect.localScale.y + minCanvasPaddingY;
        //         
        //     //normalize it to current render texture
        //     pos.x /= _canvasScaleX;
        //     pos.y /= _canvasScaleY;
        //     pos.z = 0;
        //     
        //     pos.x = Mathf.Clamp(pos.x, _leftWidth, _canvasWidth - _rightWidth);
        //     pos.y = Mathf.Clamp(pos.y, _downHeight, _canvasHeight - _upHeight);
        //
        //     return pos;
        // }
        
        public void HideDialogueBoxes()
        {
            npcDialogueBox.Hide();
            playerDialogueBox.Hide();
            playerChoiceBoxes.ForEach(box => box.Hide());

        }
        
        public void ShowContinueIcon()
        {
            npcDialogueContinueIcon.SetActive(true);
        }

        private DialogueBoxVariant GetBoxLineNumberVariant(string text, List<DialogueBoxVariant> possibleVariants)
        {
            string[] textSplit = text.Split(new char[]{'<', '>'});
            string[] newlines = Array.FindAll(textSplit, x => x.Equals("br"));
            int numberOfNewlines = newlines.Length;
            numberOfNewlines = Mathf.Clamp(numberOfNewlines, 0, possibleVariants.Count - 1);
            return possibleVariants[numberOfNewlines];
        }
        
        public void SetPlayerTextVisibleCharacters(int num)
        {
            playerDialogueBox.textField.maxVisibleCharacters = num;
        }

        public void LoadLinePlayer(string text)
        {
            PanelsController.Instance.HidePanels();
            
            //hide other items while text is displaying
            playerChoiceBoxes.ForEach(box => box.Hide());
            npcDialogueBox.Hide();
            npcDialogueContinueIcon.SetActive(false);
            
            playerDialogueBox.ShowText(text);
        }
        
        public void LoadLineNpc(string text)
        {
            PanelsController.Instance.HidePanels();

            //hide other items while text is didplaying
            playerDialogueBox.Hide();
            playerChoiceBoxes.ForEach(box => box.Hide());
            npcDialogueContinueIcon.SetActive(false);
            
            npcDialogueBox.ShowText(text);
        }
        
        public void SetNpcTextVisibleCharacters(int num)
        {
            npcDialogueBox.textField.maxVisibleCharacters = num;
        }
        
        public void ShowChoicesPanel(List<string> choiceTexts, int selectedChoice)
        {
            npcDialogueBox.Hide();
            playerDialogueBox.Hide();

            if (choiceTexts.Count < 2)
            {
                Debug.LogError($"Only {choiceTexts.Count} choices present, at least 2 are expected by UI");
            }

            int maxCount = playerChoiceBoxes.Count;
            if (choiceTexts.Count > maxCount)
            {
                Debug.LogError($"More than {maxCount} choices received by UI, only 3 will be displayed.");
                choiceTexts.RemoveRange(maxCount, choiceTexts.Count - maxCount);
            }

            _curChoice = selectedChoice;
            for (int i = 0; i < choiceTexts.Count; i++)
            {
                playerChoiceBoxes[i].ShowText(choiceTexts[i]);
                playerChoiceBoxes[i].ShowAllText();
                DialogueBoxStyle style = (i == _curChoice) ? choiceStyleSelected : choiceStyleNotSelected;
                playerChoiceBoxes[i].ApplyStyle(style);
            }

            for (int j = choiceTexts.Count; j < playerChoiceBoxes.Count; j++)
            {
                playerChoiceBoxes[j].Hide();
            }
        }

        public void ChangeCoice(int selectedChoice)
        {
            playerChoiceBoxes[_curChoice].ApplyStyle(choiceStyleNotSelected);
            _curChoice = selectedChoice;
            playerChoiceBoxes[selectedChoice].ApplyStyle(choiceStyleSelected);
        }

        public void ShowInteractionBlurb(string line)
        {
            if(DialogueManager.Instance.dialogueIsPlaying) return;

            _curInteractBlurb = line;
            LoadLinePlayer(line);
            SetPlayerTextVisibleCharacters(line.Length);
        }

        public void HideInteractionBlurb(string line)
        {
            if(DialogueManager.Instance.dialogueIsPlaying) return;
            
            if(line != _curInteractBlurb) return;
            _curInteractBlurb = null;
            HideDialogueBoxes();
        }

    }
}
