using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Ϸ�ڵı�������һֱ���в��ݻ�
/// </summary>
public class BgmMusic : MonoBehaviour
{
    private static BgmMusic instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
