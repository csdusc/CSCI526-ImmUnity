using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.Networking;
using System;
using System.Globalization;

public class SendDataToGoogle : MonoBehaviour
{
    [SerializeField] private string URL;

    private long _sessionID;
    private int _testInt;
    private int _testField;

    private void Awake()
    {
        // Assign sessionID to identify playtests
        _sessionID = DateTime.Now.Ticks;
        Send();
    }


    public void Send()
    {
        _testField = 1;
        _testInt = UnityEngine.Random.Range(0, 101);
        StartCoroutine(Post(_sessionID.ToString(), _testInt.ToString(), _testField.ToString()));
    }

    private IEnumerator Post(string sessionID, string testInt, string testField)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1138460748", sessionID);
        form.AddField("entry.1137121187", testInt);
        form.AddField("entry.1213893100", testField);
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

    }
}
