using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBaseUtils;


/// <summary>
///���˳�����ʱ,�ͷ����ݿ�����,��ֹ�ڴ�й©
/// </summary>
public class CloseDatabase : MonoBehaviour
{
   public void closedatabase()
    {
       //�˳�����ʱ�ͷ����ݿ�����
                Common.sQLh.CloseSqlConnection();
                Debug.Log("�ر����ݿ�ɹ�!");
    }
   
}
