using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class isn't needed as you don't expect any class other than Runnable to implemen 
// this interface,
// 
namespace BreadAndButter
{
    public interface IRunnables_archive
    {
        bool Enabled { get; set; }
        void Setup(params object[] _param);
        void Run(params object[] _param);
    }
}
