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
            (data) =>                   //结果为data时将读取到的json内容进行提取
            {
                //Debug.Log(JsonUtility.ToJson(data, true));
                Weather.text = data.lives[0].weather;
                Celcius.text = data.lives[0].temperature + "℃";
                WindDirection.text = data.lives[0].winddirection + "风";
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
        Date.text = DateTime.Now.ToString("yyyy-MM-dd");    //yyyy即为单词的开头的缩写
        Time.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public void SaveJson(string Name,object data)
    {
        var json = JsonUtility.ToJson(data);                            //JsonUtility.ToJson()用于将对象序列化为 JSON 格式的字符串
        var path = Path.Combine(Application.persistentDataPath, Name);  //Path.Combine用于处理相对路径
        File.WriteAllText(path, json);                                  //使用WriteAllText来写入内容
    }

    public void LoadNetJson<T>(string url,Action<T> onSuccess,Action<string> onFailure)     //读取网络上的json文件
    {
        StartCoroutine(FetchDate(url,onSuccess,onFailure));                                 //调用协程对网络进行读取
    }
   
    private IEnumerator FetchDate<T>(string url, Action<T> onSuccess, Action<string> onFailure)     //Action<T>为一个泛型委托
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(url))    //创建get请求
        {
            yield return webRequest.SendWebRequest();                   //发送请求并等待回应

            if(webRequest.result != UnityWebRequest.Result.Success)     //请求失败
            {
                onFailure?.Invoke(webRequest.error);                    //调用失败回调
            }
            else
            {
                string json = webRequest.downloadHandler.text;          //获取相应的json字符串
                T data = JsonUtility.FromJson<T>(json);                 //将json转化为指定的类型
                onSuccess?.Invoke(data);                                //调用成功回调
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
public class lives  //lives为数组类型，单独使用一个类将其进行存储
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

