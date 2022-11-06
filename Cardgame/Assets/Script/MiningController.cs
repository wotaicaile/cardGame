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

    //string path = Application.streamingAssetsPath + "/Temp.db";//找到streamingAssetsPath目录下的文件 把路径返回


    //初始化时连接数据库，显示金币数
    public void Awake()
    {
        CoinsText = GameObject.Find("Canvas/Coins/conisCount").GetComponent<Text>();
      
          

     
            Common.sQLh = new SQLiteHelp(Common.FilePath);
            Common.getCountNumber();

        CoinsText.text = Common.countNumber.ToString();
    }


  
    //加一
    public void getCoins()
    {
        //每点击一次，金币数量增加1
        Common.countNumber = Common.countNumber + 1;
        //插入成功后即时更新界面数据
        CoinsText.text = Common.countNumber.ToString();
    }


}



