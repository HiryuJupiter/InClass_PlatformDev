using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter
{
    public interface IRunnables
    {
        bool Enabled { get; set; }
        void Setup(params object[] _param);
        void Run(params object[] _param);
    }
}
