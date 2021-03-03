using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TafeDiplomaFramework
{
    [CustomPropertyDrawer(typeof(SceneFieldAttribute))] 

    public class SceneFieldDrawer : PropertyDrawer
    {
        //Renders the property into the inspector
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);

            //Object field
            //Check if anything has changed in the inspector
            EditorGUI.BeginChangeCheck();

            //Draw the scene field as an objcect field with the scene asset type
            var newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset; //The false: whether you can drag scene from hierarchy into the editor

            //Did anything actually change in the inspector?
            if (EditorGUI.EndChangeCheck()) //This returns false if nohting was changed
            {
                //Set the string value to the path of the scene
                string path = AssetDatabase.GetAssetPath(newScene);
                property.stringValue = path;
            }


            EditorGUI.EndProperty();
        }

        //Gets the vertical space a single property will take in the inspector. GUIContent = name of variable or icons.
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
            //return base.GetPropertyHeight(property, label);
        }
    }
}
