using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCoinsNumber : MonoBehaviour
{
    // ���˳�����ʱ���Խ�����ݳ־û�

    public void closedatabase()
    {
        try
        {
            Common.sQLh.UpdateOneInto("Coins", "count", Common.countNumber, "Id", "0");
            Debug.Log("�����������ɹ�");
        }
        catch
        {
            Debug.Log("�����������ʧ��");
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
