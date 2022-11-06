using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCoinsNumber : MonoBehaviour
{
    // 当退出界面时，对金币数据持久化

    public void closedatabase()
    {
        try
        {
            Common.sQLh.UpdateOneInto("Coins", "count", Common.countNumber, "Id", "0");
            Debug.Log("金币数量保存成功");
        }
        catch
        {
            Debug.Log("金币数量保存失败");
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
