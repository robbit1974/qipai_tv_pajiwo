using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SetWechatHead : Manager
{
    // Use this for initialization
    void Start()
    {

        Init();

        /*if (LocalData.GetHead() != "" && LocalData.GetHead() != null)
        {
            SetAsyncImage(LocalData.GetHead(), this.GetComponent<UITexture>());
           
        }*/
    }



    #region 微信头像

    public string path
    {
        get
        {
            //pc,ios //android :jar:file//  
            return Application.persistentDataPath + "/ImageCache/";
        }
    }


    public bool Init()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/ImageCache/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ImageCache/");
        }
        return true;
    }

    public void ClearImageCache()
    {
        if (Directory.Exists(Application.persistentDataPath + "/ImageCache/"))
        {
            Directory.Delete(Application.persistentDataPath + "/ImageCache/");
        }
    }
    public void SetAsyncImage(string url, UITexture image)
    {
        //开始下载图片前，将UITexture的主图片设置为占位图  
        //if (myWXPic != null)
        //{
        //image.mainTexture = myWXPic;
        //return;
        // }

        //判断是否是第一次加载这张图片  
        if (!File.Exists(path + url.GetHashCode()))
        {
            //如果之前不存在缓存文件  
            StartCoroutine(DownloadImage(url, image));
        }
        else
        {
            StartCoroutine(LoadLocalImage(url, image));
        }
    }

    public void SetAsyncImage(string url)
    {
        Debug.Log(url);
        if (!File.Exists(path + url.GetHashCode()))
        {
            //如果之前不存在缓存文件  
            StartCoroutine(DownloadImage(url));
        }
    }

    IEnumerator DownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        Texture2D tex2d = www.texture;
        if (tex2d.width <= 8)
        {
            StartCoroutine(DownloadImage(url));
            yield break;
        }
        //将图片保存至缓存路径  
        byte[] pngData = tex2d.EncodeToPNG();                         //将材质压缩成byte流  
        File.WriteAllBytes(path + url.GetHashCode(), pngData);        //然后保存到本地  
    }

    IEnumerator DownloadImage(string url, UITexture image)
    {
        WWW www = new WWW(url);
        yield return www;

        Texture2D tex2d = www.texture;
        if (tex2d.width <= 8)
        {
            StartCoroutine(DownloadImage(url, image));
            yield break;
        }
        //将图片保存至缓存路径  
        byte[] pngData = tex2d.EncodeToPNG();                         //将材质压缩成byte流  
        File.WriteAllBytes(path + url.GetHashCode(), pngData);        //然后保存到本地  

        image.mainTexture = tex2d;
    }

    IEnumerator LoadLocalImage(string url, UITexture image)
    {
        string filePath = "file:///" + path + url.GetHashCode();
        WWW www = new WWW(filePath);
        yield return www;
        Texture2D texture = www.texture;
        image.mainTexture = texture;
        if (texture.width <= 8)
        {
            image.mainTexture = null;
            //File.Delete(filePath);
            StartCoroutine(DownloadImage(url, image));
            yield break;
        }
    }
    #endregion
}
