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
            FlagAsPersistant();

            RunnableUtil.Setup(ref runnableTest, gameObject, "Sally", new Vector3(1, 1, 1));
        }

        private void Update()
        {
            runnableTest.Enabled = testEnabled;
            RunnableUtil.Run(ref runnableTest, gameObject);
        }
    }
}