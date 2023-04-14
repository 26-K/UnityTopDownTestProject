using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace PPD
{
    [Serializable]
    public class ED_SelectedSceneObject
    {
        [MenuItem("GameObject/★便利メソッド★/子を名前順にソート", false, 0)]
        public static void SortSiblingByName()
        {
            var o = SelectGameObjects();
            o.ForEach(e =>
            {
                Undo.RecordObject(e, "名前順にソート");
                e.transform.GetComponentsInChildren<Transform>()
                .OrderBy(t => t.name)
                .ForEach(t =>
                {
                    if (t != e.transform)
                    {
                        t.SetAsLastSibling();
                    }
                });
            });
        }

        [BoxGroup("等間隔で並べる"), HideLabel] public Vector3 eachDistance;

        [BoxGroup("等間隔で並べる")]
        [Button("GO")]
        public void SortPosition()
        {
            var o = SelectGameObjects();

            var basePosition = o[0].transform.localPosition;
            for (int i = 0; i < o.Length; i++)
            {
                o[i].transform.localPosition = basePosition + eachDistance * i;
            }
        }

        [BoxGroup("同じ名前で番号を振る"), LabelText("名前")] public string number_name;
        [BoxGroup("同じ名前で番号を振る"), LabelText("0埋めする")] public bool number_zero;

        [BoxGroup("同じ名前で番号を振る")]
        [Button("GO")]
        public void WriteNumbers()
        {
            var o = SelectGameObjects();

            if (number_zero)
            {
                if (o.Length >= 100)
                {
                    for (int i = 0; i < o.Length; i++)
                    {
                        o[i].name = $"{number_name} ({i:000})";
                    }
                }
                {

                    for (int i = 0; i < o.Length; i++)
                    {
                        o[i].name = $"{number_name} ({i:00})";
                    }
                }
            }
            else
            {
                for (int i = 0; i < o.Length; i++)
                {
                    o[i].name = $"{number_name} ({i})";
                }
            }
        }

        public static GameObject[] SelectGameObjects()
        {
            var o = Selection.objects.Select(e => e as GameObject); ;

            if (o.Any(e => e == null))
            {
                #if UNITY_EDITOR
                ST_Pop.ED_Error("GameObjectを選択してください");
                #endif
            }

            return o.ToArray();
        }
    }
}
