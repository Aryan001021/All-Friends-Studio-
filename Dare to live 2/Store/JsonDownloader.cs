using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class JsonDownloader : MonoBehaviour
{
    string jsonURL = "put link here";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("fromServer") == 1)
        {
            StartCoroutine(JsonGetter(jsonURL)); 
        }
    }
    
    IEnumerator JsonGetter(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result==UnityWebRequest.Result.Success) 
            {
                if (Directory.Exists(Path.Combine(Application.persistentDataPath + "/jsonFolder"))==false)
                {
                    Directory.CreateDirectory(Path.Combine(Application.persistentDataPath + "/jsonFolder"));
                }
                string[] jsonList= Directory.GetFiles(Path.Combine(Application.persistentDataPath + "/jsonFolder"), "*.json");
                if (jsonList.Length == 0)
                {
                    File.WriteAllText(Application.persistentDataPath + "/jsonFolder/" + "/FirstJson.json", request.downloadHandler.text);
                }
                else 
                { 
                    if (jsonList.Length == 1)
                    {
                        File.WriteAllText(Application.persistentDataPath + "/jsonFolder/" + "/SecondJson.json", request.downloadHandler.text);
                    }
                    else if (jsonList.Length==2)
                    {
                        string secondjsondata = File.ReadAllText(Application.persistentDataPath + "/jsonFolder/" + "/SecondJson.json");
                        File.Delete(Path.Combine(Application.persistentDataPath+ "/jsonFolder/" + "FirstJson.json"));
                        File.WriteAllText(Application.persistentDataPath + "/jsonFolder/" + "/FirstJson.json", secondjsondata);
                        File.Delete(Path.Combine(Application.persistentDataPath+ "/jsonFolder/" + "SecondJson.json"));
                        File.WriteAllText(Application.persistentDataPath  +"/jsonFolder/" + "/SecondJson.json", request.downloadHandler.text);

                    }
                }
                
            }
            else
            {
                Debug.Log("Error occur"+request.error);
            }
        } 
        
    }
    
}
