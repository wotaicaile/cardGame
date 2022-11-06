using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    //初始化时连接数据库，显示金币数
    public void Awake()
    {
        StartCoroutine(Png());
      
        
    }
    IEnumerator Png()
    {
        WWW www = new WWW(Application.streamingAssetsPath + "/Temp.db");
        yield return www;
        if (www.isDone)
        {
            string topath = Application.persistentDataPath + "/" + "Temp.db";//目标路径
            Debug.Log("1111111111111111111111111111111" + topath);
            if (!File.Exists(topath))
            {
                File.WriteAllBytes(topath, www.bytes);//把文件数据写入   可以打包成APK在手机上测试 把unity里streamingAssetsPath目录下选定的文件拷贝到手机的目标位置。

            }
        }

    }

}
