using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls; //For drawing box handles, collider editors, arc handles
using UnityEditor.AnimatedValues; //Animate properties appearing and dissapearing. Hide and show.

namespace TafeDiplomaFramework.AI
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        private Spawner spawner;
        private SerializedProperty sizeProperty; //aka pSize
        private SerializedProperty centerProperty; //aka pCenter

        private SerializedProperty floorProperty;
        private SerializedProperty spawnRateProperty;

        private SerializedProperty shouldSpawnBossProperty;
        private SerializedProperty bossSpawnChanceProperty;
        private SerializedProperty bossPrefabProperty;
        private SerializedProperty enemyPrefabProperty;

        private AnimBool shouldSpawnBoss = new AnimBool(); //Animation in inspector
        private BoxBoundsHandle handle; //Use for box colliders


        private void OnEnable() //like start
        {
            spawner             = target as Spawner; //Casts editor.target as spawner

            sizeProperty        = serializedObject.FindProperty("size");
            centerProperty      = serializedObject.FindProperty("center");

            floorProperty       = serializedObject.FindProperty("floorYPosition");
            spawnRateProperty   = serializedObject.FindProperty("spawnRate");

            shouldSpawnBossProperty = serializedObject.FindProperty("shouldSpawnBoss");
            bossSpawnChanceProperty = serializedObject.FindProperty("bossSpawnChance");
            bossPrefabProperty      = serializedObject.FindProperty("bossPrefab");
            enemyPrefabProperty     = serializedObject.FindProperty("enemyPrefab");

            shouldSpawnBoss.value = shouldSpawnBossProperty.boolValue;
            shouldSpawnBoss.valueChanged.AddListener(Repaint); //OnClick on buttons, the plus sign, is AddListener

            handle = new BoxBoundsHandle();
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update();

            //Where we draw all the properties

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(sizeProperty); //This draws it regularly
                EditorGUILayout.PropertyField(centerProperty);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(floorProperty);

                //Cache the original value of spawn rate and create the label
                Vector2 spawnRate = spawnRateProperty.vector2Value;
                string label = $"Spawn Rate Bounds ({spawnRate.x.ToString("0.00")}s - {spawnRate.y.ToString("0.00")}s";

                //Render the spawn rate as a min max slider and reset the vector3 value
                EditorGUILayout.MinMaxSlider(label, ref spawnRate.x, ref spawnRate.y, 0, 3);
                spawnRateProperty.vector2Value = spawnRate;

                //Apply spacing between lines
                EditorGUILayout.Space();

                //Render the enemyPrefab and should SpawnBoss as normal
                EditorGUILayout.PropertyField(enemyPrefabProperty);
                EditorGUILayout.PropertyField(shouldSpawnBossProperty);

                //Attempt to fade the next variable in and out
                shouldSpawnBoss.target = shouldSpawnBossProperty.boolValue; //Value is where it start, target is where it want to end
                if (EditorGUILayout.BeginFadeGroup(shouldSpawnBoss.faded))
                {
                    //Only visible when shouldSpawnBossProperty is true
                    EditorGUI.indentLevel++;

                    //Draw boss spawn chance and prefab as normal
                    EditorGUILayout.PropertyField(bossSpawnChanceProperty);
                    EditorGUILayout.PropertyField(bossPrefabProperty);

                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
            }

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }


        //Enable editor to handle an event in the scene view
        private void OnSceneGUI()
        {
            Handles.color = Color.green;
            //--
            //Visualize the spawning bounds
            //Gizmos don't rotate with the object by default
            //We took the matrix, rotate it, then give it back to the gizmos

            //Store default matrix
            Matrix4x4 baseMatrix = Handles.matrix; //Matrix is what the actual thing looks like

            //Matrix is like a 2D array, it's what Unity uses to draw.
            Matrix4x4 rotationMatrix = spawner.transform.localToWorldMatrix;
            Handles.matrix = rotationMatrix;

            //Draw a green, partially transparent cube
            Handles.color = new Color(0, 1, 0, 0.5f);

            //--
            //Hand is in world space, so we offset the center by the spawner's position
            handle.center = spawner.center;
            handle.size = spawner.size;

            //Begin listening for changes to the handle and then draw it
            EditorGUI.BeginChangeCheck();
            handle.DrawHandle();

            if (EditorGUI.EndChangeCheck())
            {
                //After dragging the handle, the center is now changed.
                //Reset the spawner vlaues to the new handle values
                spawner.size = handle.size;
                spawner.center = handle.center;
            }

            //Reset the handle matrix back to default
            Handles.matrix = baseMatrix;
        }




    }
}