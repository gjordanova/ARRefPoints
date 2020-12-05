using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class Ref_Point_Manager : MonoBehaviour
{

    public Text debugLog;
    //private Text referencePointCount;
    public Button ExitButton;
    public GameObject prefabSphere;
    private ARRaycastManager arRaycastManager;
    private ARReferencePointManager arReferencePointManager;
    private ARPlaneManager arPlaneManager;
    private List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    List<AddText> addText = new List<AddText>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arReferencePointManager = GetComponent<ARReferencePointManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        ExitButton.onClick.AddListener(ClearReferencePoints);
        debugLog.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;
        if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Debug.Log("hit pose" + hitPose);
            ARReferencePoint referencePoint = arReferencePointManager.AddReferencePoint(hitPose);
            if (referencePoint == null)
            {
                debugLog.gameObject.SetActive(true);
                string errorEntry = "Error creating a reference point\n";
                Debug.Log(errorEntry);
                debugLog.text += errorEntry;
            }

            else
            {
                referencePoint.GetComponent<AddText>().positionItem.SetText(referencePoint.transform.position.ToString());
                referencePoint.GetComponent<AddText>().Name_Scene.SetText("Reference point");
                referencePoints.Add(referencePoint);
                if (referencePoints.Count == 1)
                {
                    Scene.referencePoint1 = referencePoint.transform.position;
                }
                if (referencePoints.Count == 2)
                {
                    Scene.referencePoint2 = referencePoint.transform.position;

                    foreach (Item item in Scene.items)
                    {
                        GameObject _item = Instantiate(prefabSphere, item.position, Quaternion.identity);
                        Color MyColour = Color.clear;
                        ColorUtility.TryParseHtmlString(item.color, out MyColour);
                        _item.GetComponent<MeshRenderer>().material.color = MyColour;
                        _item.GetComponent<AddText>().Name_Scene.SetText(item.name);
                        _item.GetComponent<AddText>().Color_Item.SetText("Color: " + item.color);
                        _item.GetComponent<AddText>().positionItem.SetText(_item.transform.position.ToString());
                        Get_Param(MyColour, item.name, item.color, _item.transform.position);
                    }

                    arReferencePointManager.referencePointPrefab.SetActive(false);
                }

            }
            //referencePointCount.text = $"Reference Point Count: {referencePoints.Count}";
        }
    }
    public void Get_Param(Color mat, string scene_name, string color_item, Vector3 pos_item)
    {

    }
    public void ClearReferencePoints()
    {
        Debug.Log("load cene main");
        SceneManager.LoadScene("Enter_Scene");
    }
}
