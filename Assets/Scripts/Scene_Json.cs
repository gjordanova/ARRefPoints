

using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class Scene_Json
{
    public string id { get; set; }
    public string name { get; set; }
    public Vector3 ReferencePoint1 { get; set; }
    public Vector3 ReferencePoint2 { get; set; }
    public List<Item> Items { get; set; }

}
public class Item
{
    
    public string name { get; set; }
    public string color{ get; set; }
    public Vector3 position { get; set; }
}


