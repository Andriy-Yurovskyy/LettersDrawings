namespace MagicLetters.DrawLetter
{
    using UnityEngine;
    using MagicLetters.DrawLetter.ScriptableObjects;
    using MagicLetters.DrawLetter.Structs.Args;
    using MagicLetters.DrawLetter.Prefabs;
    using System;

    public class LetterManager : MonoBehaviour
    {
        public static EventHandler OnVictory;

        public static EventHandler OnStrokeReset;

        public static EventHandler OnDrawingStarted;


        [SerializeField]
        private LetterAttributes letterAttributes;

        [SerializeField]
        private float maxMouseOffset;


        [SerializeField]
        private Camera cam;

        [SerializeField]
        private RectTransform canvasRect;


        [SerializeField]
        private UserLine userLinePrefab;

        [SerializeField]
        private GameObject userLinesContainer;


        [SerializeField]
        private ModelLine modelLinePrefab;

        [SerializeField]
        private GameObject modelLinesContainer;


        [SerializeField]
        private GameObject letterNodesContainer;

        [SerializeField]
        private LetterNode letterNodePrefab;


        private ModelLetter _modelLetterObj;

        private UserLetter _userLetterObj;

        private bool _victory;


        private void Start()
        {

            DisplayModelLines();
            DisplayLetterNodes();
            SetupUserLetter();
        }


        private void Update()
        {
            _victory = _userLetterObj.Done();
            if (_victory)
            {
                OnVictory?.Invoke(null, EventArgs.Empty);
                return;
            }
            _userLetterObj.Display();
        }


        private void SetupUserLetter()
        {
            UserLetterArgs args = new UserLetterArgs
            {
                LetterAttributes = letterAttributes,
                Container = userLinesContainer,
                Prefab = userLinePrefab,
                Offset = maxMouseOffset,
                ModelLetterObj = _modelLetterObj
            };
            _userLetterObj = new UserLetter(args);

            _userLetterObj.InitUserLetter();
        }

        private void DisplayModelLines()
        {
            ModelLetterArgs args = new ModelLetterArgs
            {
                LetterAttributes = letterAttributes,
                Container = modelLinesContainer,
                Prefab = modelLinePrefab
            };
            _modelLetterObj = new ModelLetter(args);
            _modelLetterObj.DrawModelLines();
        }


        private void DisplayLetterNodes()
        {
            NodesManagerArgs args = new NodesManagerArgs
            {
                LetterAttributes = letterAttributes,
                CanvasRect = canvasRect,
                Container = letterNodesContainer,
                Prefab = letterNodePrefab,
                Cam = cam
            };
            NodesManager letterNodes = new NodesManager(args);
            letterNodes.DisplayNodes();
        }



        private void DisplayVictory()
        {
            Debug.Log("Victory");
        }


    }
}