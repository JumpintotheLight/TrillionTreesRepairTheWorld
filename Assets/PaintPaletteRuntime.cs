using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefabPainter
{
    [CreateAssetMenu(fileName = "prefabpainter_palette", menuName = "Prefab Painter/Create Runtime Palette", order = 1)]
    public class PaintPaletteRuntime : ScriptableObject
    {
        public List<PaintObjectRuntime> palette = new List<PaintObjectRuntime>();
    }
}