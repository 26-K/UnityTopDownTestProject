#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace PPD
{
    public class PostXcodeBuild
    {
        [PostProcessBuild]
        public static void SetXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS) return;

            var plistPath = pathToBuiltProject + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            var rootDict = plist.root;
            // ここに記載したKey-ValueがXcodeのinfo.plistに反映されます
            rootDict.SetString("GADApplicationIdentifier", "ca-app-pub-6662903043616813~7475975276");

            var str = "SKAdNetworkIdentifier";
            PlistElementArray backgroundModes;
            if (rootDict.values.ContainsKey(str))
            {
                backgroundModes = rootDict[str].AsArray();
            }
            else
            {
                backgroundModes = rootDict.CreateArray(str);
            }
            backgroundModes.AddString("v9wttpbfk9.skadnetwork");
            backgroundModes.AddString("n38lu8286q.skadnetwork");

            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}
#endif