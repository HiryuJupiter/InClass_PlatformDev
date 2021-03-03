using UnityEngine;

namespace TafeDiplomaFramework
{

}
public class SceneFieldAttribute : PropertyAttribute
{
    //Takes a file path and turn it into a file that we can load
    public static string LoadableName (string _path)
    {
        //Substrings we aer looking to ignore
        string start = "Assets/";
        string end = ".unity";

        if (_path.StartsWith(start))
        {
            _path = _path.Substring(start.Length);
        }

        if (_path.EndsWith(end))
        {
            _path = _path.Substring(0, _path.LastIndexOf(end));
        }

        //Return the edited path
        return _path;
    }
}
