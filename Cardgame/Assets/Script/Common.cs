using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBaseUtils;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using DrawsUtils;
using  System.IO;

/// <summary>
/// �������ڶ���ȫ�־�̬��������̬����,�����ظ�����
/// </summary>

public class Common
{
    //Ϊ�����˳�����ʱ��ɹر����ݿ�Ĳ���,���䶨��Ϊstatic
    public static SQLiteHelp sQLh;
    //�������
    public static int countNumber;
    //�жϽ�����Ƿ��㹻
    public static bool isEnought;
    //�����ʾ
    // public static string FilePath = @"Data Source= D:\Unity\projects\Cardgame\Assets\Script\Temp.db";

  
    public static string FilePath = "Data Source= "+ Application.persistentDataPath + "/Temp.db";






    //��ȡ0��n�������
    public static int getRandomNumber(int max)
    {
        System.Random rd = new System.Random();
        return rd.Next(0, max);
    }

    //��ȡ�������
    public static void getCountNumber()
    {
        sQLh.ReadFullTable("Coins");
        countNumber = int.Parse(sQLh.PrintCoinCount().ToString());
       // Debug.Log("��ǰ�Ľ�����ǣ�" + countNumber);
    }

    //���½������
    public static void updateCountNumber(int value)
    {
        countNumber = countNumber - value;
        sQLh.UpdateOneInto("Coins", "count", countNumber, "Id", "0");
    }
}


