namespace MagicLetters.DrawLetter
{
    using System;
    using System.Collections.Generic;
    using MagicLetters.DrawLetter.Structs.Args;
    using MagicLetters.DrawLetter.Structs;
    using UnityEngine;
    using MagicLetters.DrawLetter.Prefabs;

    public class NodesManager
    {
        private Dictionary<Vector2, List<int>> _nodes;

        private NodesManagerArgs _details;


        public NodesManager(NodesManagerArgs args)
        {
            _details = args;
        }


        public void DisplayNodes()
        {
            SetNodesDetails();
            DrawNodes();
        }


        private void SetNodesDetails()
        {
            _nodes = new Dictionary<Vector2, List<int>>();
            int pointIndex = 1;

            foreach (Stroke stroke in _details.LetterAttributes.strokes)
            {
                foreach (Vector2 point in stroke.points)
                {
                    if (!_nodes.ContainsKey(point))
                    {
                        _nodes[point] = new List<int>();
                    }

                    _nodes[point].Add(pointIndex);
                    pointIndex++;
                }
            }
        }


        private void DrawNodes()
        {
            foreach (var keyValuePair in _nodes)
            {
                LetterNode letterNode = GameObject.Instantiate(_details.Prefab);
                letterNode.transform.SetParent(_details.Container.transform);
                letterNode.GetComponent<RectTransform>().anchoredPosition = GetPosisionOnCanvas(keyValuePair.Key);
                letterNode.text.text = String.Join("|", keyValuePair.Value);
            }
        }


        private Vector2 GetPosisionOnCanvas(Vector2 worldPos)
        {
            Vector2 ViewportPosition = _details.Cam.WorldToViewportPoint(worldPos);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * _details.CanvasRect.sizeDelta.x) - (_details.CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * _details.CanvasRect.sizeDelta.y) - (_details.CanvasRect.sizeDelta.y * 0.5f)));
            return WorldObject_ScreenPosition;
        }



    }
}