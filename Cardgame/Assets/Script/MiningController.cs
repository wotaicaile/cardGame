using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBaseUtils;
using System;
using UnityEngine.UI;
using System.IO;

public class MiningController : MonoBehaviour
{
   
    private Text CoinsText;

    //string path = Application.streamingAssetsPath + "/Temp.db";//�ҵ�streamingAssetsPathĿ¼�µ��ļ� ��·������


    //��ʼ��ʱ�������ݿ⣬��ʾ�����
    public void Awake()
    {
        CoinsText = GameObject.Find("Canvas/Coins/conisCount").GetComponent<Text>();
      
          

     
            Common.sQLh = new SQLiteHelp(Common.FilePath);
            Common.getCountNumber();

        CoinsText.text = Common.countNumber.ToString();
    }


  
    //��һ
    public void getCoins()
    {
        //ÿ���һ�Σ������������1
        Common.countNumber = Common.countNumber + 1;
        //����ɹ���ʱ���½�������
        CoinsText.text = Common.countNumber.ToString();
    }


}



