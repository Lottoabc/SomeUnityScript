using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Net;
public class TimeAndWeather : MonoBehaviour
{
    public Text Date;
    public Text Time;
    public Text Weather;
    public Text Celcius;
    public Text WindDirection;

    public string url = "https://restapi.amap.com/v3/weather/weatherInfo?city=350102&key=dc8d371ffc7b531d2170fe77e5f7dad0&extensions=base&output=JSON";
    // Start is called before the first frame update
    void Start()
    {
        LoadNetJson<LiveData>(url,
            (data) =>                   //���Ϊdataʱ����ȡ����json���ݽ�����ȡ
            {
                //Debug.Log(JsonUtility.ToJson(data, true));
                Weather.text = data.lives[0].weather;
                Celcius.text = data.lives[0].temperature + "��";
                WindDirection.text = data.lives[0].winddirection + "��";
            },
            (error) =>
            {
                //Debug.Log(error);
            }

            );
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Start", 60f);
        Date.text = DateTime.Now.ToString("yyyy-MM-dd");    //yyyy��Ϊ���ʵĿ�ͷ����д
        Time.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public void SaveJson(string Name,object data)
    {
        var json = JsonUtility.ToJson(data);                            //JsonUtility.ToJson()���ڽ��������л�Ϊ JSON ��ʽ���ַ���
        var path = Path.Combine(Application.persistentDataPath, Name);  //Path.Combine���ڴ������·��
        File.WriteAllText(path, json);                                  //ʹ��WriteAllText��д������
    }

    public void LoadNetJson<T>(string url,Action<T> onSuccess,Action<string> onFailure)     //��ȡ�����ϵ�json�ļ�
    {
        StartCoroutine(FetchDate(url,onSuccess,onFailure));                                 //����Э�̶�������ж�ȡ
    }
   
    private IEnumerator FetchDate<T>(string url, Action<T> onSuccess, Action<string> onFailure)     //Action<T>Ϊһ������ί��
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(url))    //����get����
        {
            yield return webRequest.SendWebRequest();                   //�������󲢵ȴ���Ӧ

            if(webRequest.result != UnityWebRequest.Result.Success)     //����ʧ��
            {
                onFailure?.Invoke(webRequest.error);                    //����ʧ�ܻص�
            }
            else
            {
                string json = webRequest.downloadHandler.text;          //��ȡ��Ӧ��json�ַ���
                T data = JsonUtility.FromJson<T>(json);                 //��jsonת��Ϊָ��������
                onSuccess?.Invoke(data);                                //���óɹ��ص�
            }
        }
        
    }
}

[Serializable]
public class LiveData
{
    public string status;
    public string count;
    public string info;
    public string infocode;
    public lives[] lives;
}

[Serializable]
public class lives  //livesΪ�������ͣ�����ʹ��һ���ཫ����д洢
{
    public string province;
    public string city;
    public string adcode;
    public string weather;
    public string temperature;
    public string winddirection;
    public string windpower;
    public string humidity;
    public string reporttime;
    public string temperature_float;
    public string humidity_float;
}

