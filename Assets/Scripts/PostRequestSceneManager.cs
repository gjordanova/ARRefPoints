using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PostRequestSceneManager : MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Button LoadScene;
    public InputField textField;
    public Text errortextField;

    public void GetidScene()
    {
        string sceneid = textField.text;
        Debug.Log(sceneid);
        StartCoroutine(PostRequest("https://arstudio-test.azurewebsites.net/api/v2/scenes", "{\"sceneId\":\"" + sceneid + "\"}"));
        errortextField.text = "";
    }
 
    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Accept", "text/plain");
        uwr.SetRequestHeader("Netcetera-Auth-Token", "n4woc1HUxOGVoRO3UihJN2TRTYGHgw");
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();

        if (uwr.isHttpError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            
            errortextField.text = "Scene is not valid";
        }
        else
        {

            Scene_Json jsd = JsonConvert.DeserializeObject<Scene_Json>(uwr.downloadHandler.text);
            Scene.iD = jsd.id;
            Scene.name = jsd.name;
            Scene.referencePoint1 = jsd.ReferencePoint1;
            Scene.referencePoint2 = jsd.ReferencePoint2;
            Scene.items = jsd.Items;
            SceneManager.LoadScene("2_ARScene");


        }
    }
}
