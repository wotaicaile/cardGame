using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructure 
{
    //�������
    public static int CoinsNumber;
    //��ͨ�����ֵ�
    public static Dictionary<string, Cards> cardsDictionary = new Dictionary<string, Cards>();

    //��һ�õĿ�Ƭ�б�key��ֵ�ǳ鿨��˳����Ϊ��˳��������ҷ�����ҡ�
    public static Dictionary<string, Cards> PlayerCardsDictionary = new Dictionary<string, Cards>();
   
    public static int ClubValues;

    //��¼�Ѿ����򿪹���
    public static Dictionary<string,int> PlayerCardsDictionaryAlaredyUsed = new Dictionary<string,int>();

}
