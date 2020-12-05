using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Button Load_Scene;
    public InputField textField;
    public Text errortextField;

    private void Update()
    {
       
    }
    public void Awake()
    {
        Load_Scene.onClick.AddListener(LoadS);
    }
    public void LoadS()
    {
     
        if (textField.text.ToString() == "1")
        {
            SceneManager.LoadScene("2_ARScene");
            Debug.Log(textField.text);
            var jsonTextFile = Resources.Load<TextAsset>("Scene_1");
            Debug.Log(jsonTextFile);
            Debug.Log("1");
            Scene_Json jsd = JsonConvert.DeserializeObject<Scene_Json>(Resources.Load<TextAsset>("Scene_1").ToString()); ;
            Scene.iD = jsd.id;
            Scene.name = jsd.name;
            Scene.referencePoint1 = jsd.ReferencePoint1;
            Scene.referencePoint2 = jsd.ReferencePoint2;
            Scene.items = jsd.Items;

        }
    }
}
