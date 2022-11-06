using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructure 
{
    //金币数量
    public static int CoinsNumber;
    //普通卡池字典
    public static Dictionary<string, Cards> cardsDictionary = new Dictionary<string, Cards>();

    //玩家获得的卡片列表，key的值是抽卡的顺序，是为了顺序输出并且方便查找。
    public static Dictionary<string, Cards> PlayerCardsDictionary = new Dictionary<string, Cards>();
   
    public static int ClubValues;

    //记录已经被打开过的
    public static Dictionary<string,int> PlayerCardsDictionaryAlaredyUsed = new Dictionary<string,int>();

}
