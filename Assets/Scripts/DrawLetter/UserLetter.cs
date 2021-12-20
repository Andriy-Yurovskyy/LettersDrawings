namespace MagicLetters.DrawLetter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MagicLetters.DrawLetter.Prefabs;
    using MagicLetters.DrawLetter.Structs;
    using MagicLetters.DrawLetter.Structs.Args;
    using MagicLetters.Touches;
    using UnityEngine;

    public class UserLetter
    {
        private UserLetterArgs _details;

        private List<StrokeToAnalyse> _strokesToAnalyse;

        private int? _activeStroke;

        private int? _activeLine;

        private LineToAnalyse _lineToAnalyse;


        public UserLetter(UserLetterArgs args)
        {
            _details = args;
        }


        public void Display()
        {
            if (!TouchAnalyzer.GetTouchPos().HasValue)
            {
                ResetNotDrawedStrokes();
                return;
            }

            if (!SetLineToAnalyze())
            {
                return;
            }

            AnalyseIfShouldStartToDrawNewLine();
            AnalyseIfShouldContinueToDrawLine();
        }


        public bool Done()
        {
            return (_activeStroke == null && _activeLine == null);
        }


        public void InitUserLetter()
        {
            _strokesToAnalyse = new List<StrokeToAnalyse>();

            for (int i = 0; i < _details.LetterAttributes.strokes.Count; i++)
            {
                Stroke stroke = _details.LetterAttributes.strokes[i];
                _strokesToAnalyse.Add(new StrokeToAnalyse
                {
                    Lines = new List<LineToAnalyse>(),
                    IsDrawn = false
                });

                InitStrokeLines(stroke, i);
            }
            _activeLine = 0;
            _activeStroke = 0;
        }


        private void InitStrokeLines(Stroke stroke, int strokeIndex)
        {
            for (int k = 0; k < stroke.points.Count - 1; k++)
            {
                UserLine userLine = GameObject.Instantiate(_details.Prefab);
                userLine.gameObject.transform.SetParent(_details.Container.transform);

                _strokesToAnalyse.Last().Lines.Add(new LineToAnalyse
                {
                    IsDrawn = false,
                    IsDrawingStarted = false,
                    StartPoint = stroke.points[k],
                    EndPoint = stroke.points[k + 1],
                    ModelEdgeCollider2D = _details.ModelLetterObj.GetEdgeCollider(strokeIndex, k),
                    UserLine = userLine
                });
            }
        }


        private bool SetLineToAnalyze()
        {
            _activeLine = null;
            _activeStroke = null;
            for (int i = 0; i < _strokesToAnalyse.Count; i++)
            {

                if (_strokesToAnalyse[i].IsDrawn)
                {
                    continue;
                }

                for (int k = 0; k < _strokesToAnalyse[i].Lines.Count; k++)
                {
                    if (!_strokesToAnalyse[i].Lines[k].IsDrawn)
                    {
                        _activeLine = k;
                        _activeStroke = i;
                        _lineToAnalyse = _strokesToAnalyse[_activeStroke.Value].Lines[_activeLine.Value];
                        return true;
                    }
                }

            }
            return false;
        }


        private void ResetNotDrawedStrokes()
        {
            if (!_activeStroke.HasValue || !_activeLine.HasValue)
            {
                return;
            }

            bool sendEvent = false;
            StrokeToAnalyse stroke = _strokesToAnalyse[_activeStroke.Value];
            for (int i = 0; i < stroke.Lines.Count; i++)
            {
                LineToAnalyse lineToAnalyse = stroke.Lines[i];

                if (lineToAnalyse.IsDrawn || lineToAnalyse.IsDrawingStarted)
                {
                    sendEvent = true;
                }

                lineToAnalyse.IsDrawn = false;
                lineToAnalyse.IsDrawingStarted = false;
                stroke.Lines[i] = lineToAnalyse;
                lineToAnalyse.UserLine.Erase();
            }

            if (sendEvent)
            {
                LetterManager.OnStrokeReset?.Invoke(null, EventArgs.Empty);
            }
        }


        private void SetLineAsDrawingStarted()
        {
            if (!_activeStroke.HasValue || !_activeLine.HasValue)
            {
                return;
            }

            LineToAnalyse lineToAnalyse = _strokesToAnalyse[_activeStroke.Value].Lines[_activeLine.Value];
            lineToAnalyse.IsDrawingStarted = true;
            _strokesToAnalyse[_activeStroke.Value].Lines[_activeLine.Value] = lineToAnalyse;

            if (_activeLine.Value == 0)
            {
                LetterManager.OnDrawingStarted?.Invoke(null, EventArgs.Empty);
            }

        }


        private void AnalyseIfShouldContinueToDrawLine()
        {
            if (!_lineToAnalyse.IsDrawingStarted)
            {
                return;
            }

            Vector2 closestPoint = _lineToAnalyse.ModelEdgeCollider2D.ClosestPoint(TouchAnalyzer.GetTouchPos().Value);
            if (Vector2.Distance(closestPoint, TouchAnalyzer.GetTouchPos().Value) <= _details.Offset)
            {
                _lineToAnalyse.UserLine.Draw(closestPoint);
            }

            if (Vector2.Distance(closestPoint, _lineToAnalyse.EndPoint) <= _details.Offset)
            {
                _lineToAnalyse.UserLine.Draw(_lineToAnalyse.EndPoint);
                SetLineAsDrawingEnded();
                SetStrokeAsDrawingEnded();
            }
        }


        private void AnalyseIfShouldStartToDrawNewLine()
        {
            if (_lineToAnalyse.IsDrawingStarted)
            {
                return;
            }

            float distance = Vector2.Distance(_lineToAnalyse.StartPoint, TouchAnalyzer.GetTouchPos().Value);
            if (distance <= _details.Offset)
            {
                _lineToAnalyse.UserLine.Draw(_lineToAnalyse.StartPoint);
                SetLineAsDrawingStarted();
            }
        }


        private void SetLineAsDrawingEnded()
        {
            if (!_activeStroke.HasValue || !_activeLine.HasValue)
            {
                return;
            }

            LineToAnalyse lineToAnalyse = _strokesToAnalyse[_activeStroke.Value].Lines[_activeLine.Value];
            lineToAnalyse.IsDrawn = true;
            _strokesToAnalyse[_activeStroke.Value].Lines[_activeLine.Value] = lineToAnalyse;
        }


        private void SetStrokeAsDrawingEnded()
        {
            if (!_activeStroke.HasValue || !_activeLine.HasValue)
            {
                return;
            }

            StrokeToAnalyse stroke = _strokesToAnalyse[_activeStroke.Value];

            bool setLineAsDrawned = true;

            foreach (LineToAnalyse lineToAnalyse in stroke.Lines)
            {
                if (!lineToAnalyse.IsDrawn)
                {
                    setLineAsDrawned = false;
                }
            }

            if (!setLineAsDrawned)
            {
                return;
            }

            stroke.IsDrawn = true;
            _strokesToAnalyse[_activeStroke.Value] = stroke;
        }


    }
}