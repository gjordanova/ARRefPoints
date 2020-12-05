using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scene : MonoBehaviour
{
   
    public static string iD;
    public static string name;
    public static Vector3 referencePoint1;
    public static Vector3 referencePoint2;
    public static List<Item> items;
   
    private void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
