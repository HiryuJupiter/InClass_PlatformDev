using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TafeDiplomaFramework.AI
{
    public class Spawner : MonoBehaviour
    {
        public Vector3 size = Vector3.one;
        public Vector3 center = Vector3.zero;

        //If it's flase, then randomize all 3 axis. If true, then only randomize XY.
        [SerializeField, Tooltip("Use the objects y position always where spawning an object.")] 
        private bool floorYPosition = false;
        [SerializeField]
        private Vector2 spawnRate = new Vector2(0, 1);

        [SerializeField]
        private bool shouldSpawnBoss = false;
        [SerializeField, Range(0, 100)]
        private float bossSpawnChance = 1;

        [SerializeField]
        private GameObject bossPrefab = null;
        [SerializeField] 
        private GameObject enemyPrefab = null;


        //
        float time = 0;
        float timeStep = 0;

        private void Spawn()
        {
            GameObject prefab = shouldSpawnBoss && Random.Range(0, 100) > bossSpawnChance ? bossPrefab : enemyPrefab;
            Vector3 position = transform.localPosition + new Vector3(
                Random.Range(-size.x * .5f, size.x * .5f) ,
                floorYPosition ? 0 : Random.Range(-size.y * .5f, size.y * .5f),
                Random.Range(-size.z * .5f, size.z * .5f)) + center;



            Instantiate(prefab, position, transform.rotation, transform);

            timeStep = Random.Range(spawnRate.x, spawnRate.y);
            time = 0;
        }

        private void Start()
        {
            timeStep = Random.Range(spawnRate.x, spawnRate.y);
        }

        private void Update()
        {
            if (time < timeStep)
            {
                time += Time.deltaTime * Time.timeScale;
            }
            else
            {
                Spawn();
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected() //Great for drawing range of towers
        {
            //Visualize the spawning bounds
            //Gizmos don't rotate with the object by default
            //We took the matrix, rotate it, then give it back to the gizmos

            //Store default matrix
            Matrix4x4 baseMatrix = Gizmos.matrix; //Matrix is what the actual thing looks like

            //Matrix is like a 2D array, it's what Unity uses to draw.
            Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
            Gizmos.matrix = rotationMatrix;

            //Draw a green, partially transparent cube
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(center, size);

            //Reset the gizmos matrix back to default
            Gizmos.matrix = baseMatrix;
        }
#endif
    }
}
