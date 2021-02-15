using System.Collections;
using UnityEngine;
using BreadAndButter;

namespace BreadAndButter
{ 
    public class CoreTest : MonoSingleton<CoreTest>
    {
        [SerializeField]
        private RunnableTest runnableTest;

        [SerializeField]
        private bool testEnabled = true;

        private void Start()
        {
            CreateInstance();
            DontDestroyOnLoad();

            RunnableUtil.Setup(runnableTest, gameObject, "Sally", new Vector3(1, 1, 1));
        }

        private void Update()
        {
            runnableTest.Enabled = testEnabled;
            RunnableUtil.Run(runnableTest, gameObject);
        }
    }
}