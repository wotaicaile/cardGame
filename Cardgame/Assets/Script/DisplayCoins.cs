using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DataBaseUtils;


public class DisplayCoins : MonoBehaviour
{
    private Text CoinsText;

    void Start()
    {
        CoinsText = GameObject.Find("Canvas/Coins/conisCount").GetComponent<Text>(); 
        Common.sQLh = new SQLiteHelp(Common.FilePath);
        Common.getCountNumber();
        CoinsText.text = Common.countNumber.ToString();

        try
        { 
            Common.sQLh.LoadBookList(Common.sQLh.ReadFullTable("Cards"));
            Debug.Log("查找cards成功");

        }
        catch
        {
            Debug.Log("查找cards失败");
        }

    }


}


