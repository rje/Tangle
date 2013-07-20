using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace Tangle
{
    public class MenuItems
    {

        const string YAML_HEADER = "%YAML 1.1\n" +
            "%TAG !u! tag:unity3d.com,2011:\n" +
            "--- !u!13 &1\n" +
            "InputManager:\n" +
            "  m_ObjectHideFlags: 0\n" +
            "  m_Axes:\n";

        const string AXIS_ENTRY = 
            "  - serializedVersion: 3\n" +
            "    m_Name: joystick {0} axis {1}\n" +
            "    descriptiveName:\n" +
            "    descriptiveNegativeName:\n" +
            "    negativeButton:\n" +
            "    positiveButton:\n" +
            "    altNegativeButton:\n" +
            "    altPositiveButton:\n" +
            "    gravity: 0\n" +
            "    dead: 0.1899999998\n" +
            "    sensitivity: 1\n" +
            "    snap: 0\n" +
            "    invert: 0\n" +
            "    type: 2\n" +
            "    axis: {2}\n" +
            "    joyNum: {0}\n";

        static string GetInputManagerSettingsPath ()
        {
            var basePath = Application.dataPath;
            var path = basePath.Replace ("/Assets", ""); 
            path += "/ProjectSettings/InputManager.asset";
            return path;
        }

        [MenuItem("Edit/Tangle/Rewrite Input Manager")]
        public static void RewriteInputManager ()
        {
            File.Delete (GetInputManagerSettingsPath ());
            var writer = new StreamWriter (GetInputManagerSettingsPath ());
            writer.Write (YAML_HEADER);
            for (var i = 1; i <= 4; i++) {
                ConfigureJoystick (writer, i, 10);
            }
            writer.Flush ();
            writer.Close ();
            AssetDatabase.Refresh ();
        }

        static void ConfigureJoystick (StreamWriter writer, int joystick, int numaxes)
        {
            for (var axis = 0; axis < numaxes; axis++) {
                writer.Write (string.Format (AXIS_ENTRY, joystick, axis + 1, axis));
            }
        }
    }
}