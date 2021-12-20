namespace MagicLetters.DrawLetter.Views
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class Messages : MonoBehaviour
    {
        public Text text;



        public void OnEnable()
        {
            LetterManager.OnVictory += ChangeTextOnVictory;
            LetterManager.OnStrokeReset += ChangeTextOnStrokeReset;
            LetterManager.OnDrawingStarted += ChangeTextOnDrawingStarted;
        }


        public void OnDisable()
        {
            LetterManager.OnVictory -= ChangeTextOnVictory;
            LetterManager.OnStrokeReset -= ChangeTextOnStrokeReset;
            LetterManager.OnDrawingStarted -= ChangeTextOnDrawingStarted;
        }


        private void ChangeTextOnVictory(object sender, EventArgs e)
        {
            text.enabled = true;
            text.text = "!!! Victory !!!";
        }


        private void ChangeTextOnStrokeReset(object sender, EventArgs e)
        {
            text.enabled = true;
            text.text = "Please try to draw this stroke again:(";
        }

        private void ChangeTextOnDrawingStarted(object sender, EventArgs e)
        {
            text.enabled = false;
        }


    }
}