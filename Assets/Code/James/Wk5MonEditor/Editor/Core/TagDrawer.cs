using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//[SerializeField, TagAttributes]
//proivate string groundTag;

namespace TafeDiplomaFramework
{
    [CustomPropertyDrawer(typeof(TagAttributes))] //Tells unity what to apply this Drawer to
    public class TagDrawer : PropertyDrawer
    {
        //Renders the property into the inspector
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property); //We are beginning this instance of the property,not every property.

            //Check if the property is set to nothing by default
            bool isNotSet = string.IsNullOrEmpty(property.stringValue);

            //Draw the string as a tag instead of a normal string box
            //We take that targe objhect and convert it to a component
            property.stringValue = EditorGUI.TagField(position, label, isNotSet ? 
                (property.serializedObject.targetObject as Component).gameObject.tag : property.stringValue);

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

/*
 * //Renders the property into the ispector.n
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
        }
 */