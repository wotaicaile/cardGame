using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    //��ʼ��ʱ�������ݿ⣬��ʾ�����
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
            string topath = Application.persistentDataPath + "/" + "Temp.db";//Ŀ��·��
            Debug.Log("1111111111111111111111111111111" + topath);
            if (!File.Exists(topath))
            {
                File.WriteAllBytes(topath, www.bytes);//���ļ�����д��   ���Դ����APK���ֻ��ϲ��� ��unity��streamingAssetsPathĿ¼��ѡ�����ļ��������ֻ���Ŀ��λ�á�

            }
        }

    }

}
