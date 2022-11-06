using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBaseUtils;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using DrawsUtils;
using  System.IO;

/// <summary>
/// 此类用于定义全局静态变量及静态方法,减少重复代码
/// </summary>

public class Common
{
    //为了在退出场景时完成关闭数据库的操作,将其定义为static
    public static SQLiteHelp sQLh;
    //金币数量
    public static int countNumber;
    //判断金币数是否足够
    public static bool isEnought;
    //金币显示
    // public static string FilePath = @"Data Source= D:\Unity\projects\Cardgame\Assets\Script\Temp.db";

  
    public static string FilePath = "Data Source= "+ Application.persistentDataPath + "/Temp.db";






    //获取0到n的随机数
    public static int getRandomNumber(int max)
    {
        System.Random rd = new System.Random();
        return rd.Next(0, max);
    }

    //获取金币数量
    public static void getCountNumber()
    {
        sQLh.ReadFullTable("Coins");
        countNumber = int.Parse(sQLh.PrintCoinCount().ToString());
       // Debug.Log("当前的金币数是：" + countNumber);
    }

    //更新金币数量
    public static void updateCountNumber(int value)
    {
        countNumber = countNumber - value;
        sQLh.UpdateOneInto("Coins", "count", countNumber, "Id", "0");
    }
}


