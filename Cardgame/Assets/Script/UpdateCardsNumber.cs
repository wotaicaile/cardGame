using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdateCardsNumber : MonoBehaviour
{

    public void UpdateCards()
    {
        Cards cards;
          string[] values = new string[6];
        foreach (KeyValuePair<string, Cards> kv in DataStructure.PlayerCardsDictionary)
        {
            //获取列表中的书数据
            DataStructure.PlayerCardsDictionary.TryGetValue(kv.Key, out cards);

            values[0] = "'"+ cards.getId()+"'";
            values[1] = "'" + cards.getName() + "'";
            values[2] = "'" + cards.getDescription() + "'";
            values[3] = "'" + cards.getImagePath() + "'";
            values[4] = "'" + cards.getDetail() + "'";
            values[5] = "'" + cards.getValue() + "'";

            try
            {
                Common.sQLh.InsertInto("ItemsObtained", values);
                Debug.Log("插入数据库成功!");

            }
            catch(Exception ex)
            {
                Debug.Log("插入数据库失败!");
                Debug.Log(ex.Message);

            }

        }

        try
        {
            //退出场景时释放数据库连接
            Common.sQLh.CloseSqlConnection();
            Debug.Log("关闭数据库成功!");
        }
        catch
        {
            Debug.Log("关闭数据库失败!");

        }


    }
}
