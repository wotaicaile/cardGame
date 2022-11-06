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
            //��ȡ�б��е�������
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
                Debug.Log("�������ݿ�ɹ�!");

            }
            catch(Exception ex)
            {
                Debug.Log("�������ݿ�ʧ��!");
                Debug.Log(ex.Message);

            }

        }

        try
        {
            //�˳�����ʱ�ͷ����ݿ�����
            Common.sQLh.CloseSqlConnection();
            Debug.Log("�ر����ݿ�ɹ�!");
        }
        catch
        {
            Debug.Log("�ر����ݿ�ʧ��!");

        }


    }
}
