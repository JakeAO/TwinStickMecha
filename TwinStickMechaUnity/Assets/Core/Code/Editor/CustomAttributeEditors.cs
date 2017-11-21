using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomPropertyDrawer(typeof(SceneAttribute))]
public class SceneAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            string[] sceneNames = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < sceneNames.Length; i++)
            {
                var scene = EditorBuildSettings.scenes[i];
                string sceneName = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                sceneNames[i] = i + ": " + sceneName;
            }

            property.intValue = EditorGUI.Popup(position, label.text, property.intValue, sceneNames);
        }
        else
        {
            GUI.color = Color.red;
            GUI.Label(position, string.Format("Invalid SceneAttribute on {0} type property \"{1}\"", property.propertyType, label.text));
            GUI.color = Color.white;
        }
    }
}
