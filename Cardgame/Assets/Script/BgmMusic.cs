using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 令游戏内的背景音乐一直运行不摧毁
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
