namespace MagicLetters.DrawLetter
{
    using System.Collections.Generic;
    using MagicLetters.DrawLetter.Prefabs;
    using MagicLetters.DrawLetter.Structs;
    using MagicLetters.DrawLetter.Structs.Args;
    using UnityEngine;

    public class ModelLetter
    {
        private ModelLetterArgs _details;

        private Dictionary<int, List<ModelLine>> _modelLines;


        public ModelLetter(ModelLetterArgs args)
        {
            _details = args;
        }


        public void DrawModelLines()
        {
            _modelLines = new Dictionary<int, List<ModelLine>>();

            for (int i = 0; i < _details.LetterAttributes.strokes.Count; i++)
            {
                Stroke stroke = _details.LetterAttributes.strokes[i];
                _modelLines[i] = new List<ModelLine>();
                for (int k = 0; k < stroke.points.Count - 1; k++)
                {
                    List<Vector2> linePoints = new List<Vector2>
                    {
                        stroke.points[k],
                        stroke.points[k +1]
                    };
                    ModelLine modelLine = GameObject.Instantiate(_details.Prefab);
                    modelLine.Draw(new DrawLineArgs { ParentObj = _details.Container, Points = linePoints });
                    _modelLines[i].Add(modelLine);
                }
            }
        }


        public EdgeCollider2D GetEdgeCollider(int strokeIndex, int lineIndex)
        {
            return _modelLines[strokeIndex][lineIndex].EdgeCollider2D;
        }


    }

}

