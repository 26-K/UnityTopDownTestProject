using System.Diagnostics;
using UnityEditor;

namespace PPD
{
    public static class ST_Pop
    {
        #if UNITY_EDITOR
        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void ED_Error(string message, string person = "ウェレイ")
        {
            EditorUtility.DisplayDialog("エラー!", $"{message}\n\n{person}に連絡して下さい", "OK");
            Log.Check.Error(message);
        }

        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void ED_Message(string message)
        {
            EditorUtility.DisplayDialog("メッセージ", message, "OK");
        }

        /// <summary>
        /// ユニティエディタ上でしか動かないので注意
        /// </summary>
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void ED_Message(string title, string nameStr, string ok = "OK")
        {
            Log.Check.Info($"{title}\n{nameStr}");
            EditorUtility.DisplayDialog(title, nameStr, ok);
        }
        #endif
    }
}
