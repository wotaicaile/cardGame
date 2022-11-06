using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBaseUtils;


/// <summary>
///当退出场景时,释放数据库链接,防止内存泄漏
/// </summary>
public class CloseDatabase : MonoBehaviour
{
   public void closedatabase()
    {
       //退出场景时释放数据库连接
                Common.sQLh.CloseSqlConnection();
                Debug.Log("关闭数据库成功!");
    }
   
}
