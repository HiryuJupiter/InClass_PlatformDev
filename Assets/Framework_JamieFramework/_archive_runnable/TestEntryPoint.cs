using System.Collections;
using UnityEngine;
using TafeDiplomaFramework;

namespace TafeDiplomaFramework
{ 
    public class TestEntryPoint : MonoSingleton<TestEntryPoint>
    {
        [SerializeField]
        private ExampleRunnable runnableScript;

        [SerializeField]
        private bool testEnabled = true;

        private void Start()
        {
            CreateInstance();
            DontDestroyOnLoad();

            RunnableUtil.Setup(ref runnableScript, gameObject);
        }

        private void Update()
        {
            runnableScript.Enabled = testEnabled;
            RunnableUtil.Run(ref runnableScript, gameObject);
        }
    }
}