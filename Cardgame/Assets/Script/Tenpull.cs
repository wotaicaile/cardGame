using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using DataBaseUtils;

public class Tenpull : MonoBehaviour
{
    //��¼�齱�������.�����жϽ���Ƿ��㹻
    public int minimumNumber;
    //��¼�齱����,�������ı�齱����.(����ʵ�ֳ齱�ۿ�(���籾����1��10���,10��80���))
    public static int NumberOfDraws = 10;

    //����ʵʱ��ʾ�������
    private Text CoinsText;
    //���ڼ�¼�齱�����ͼƬ·��,ͨ�����������ʾ��ui������
    private string randomCardPath;

    private GameObject ChildObject;
    private GameObject ParentObject;

    public void startCheak()
    {
        //�жϱ߽�����
        if (Common.countNumber < minimumNumber)
        {
            //��ʾʧ�����,�ı���ʾ��Ҳ���
            ParentObject = GameObject.Find("Canvas/errorFather");
            ChildObject = ParentObject.transform.Find("PanelErrror").gameObject;
            ChildObject.SetActive(true);
        }
        else if ((DataStructure.PlayerCardsDictionary.Count == DataStructure.cardsDictionary.Count))
        {
            //��ʾʧ�����,�ı���ʾ��Ҳ���
            ParentObject = GameObject.Find("Canvas/errorFather");
            ChildObject = ParentObject.transform.Find("PanelErrror").gameObject;
            ChildObject.transform.Find("error").GetComponent<Text>().text = "���ر����";
            ChildObject.SetActive(true);

        }
        else
        {
            GameObject targetObject;
        
            ////��ȡ�鿨��������
            ParentObject = GameObject.Find("Canvas/cardFathermultify");
            ChildObject = ParentObject.transform.Find("PanelCard").gameObject;
            targetObject = ChildObject.transform.Find("BookPanel").gameObject;

            //��ʾ��ǰΪ�ڼ��γ齱����ɺ�������һ
            //Debug.Log("ִ�е���" + NumberOfDraws);
            ChildObject.transform.Find("CardNumber").transform.Find("CountText").GetComponent<Text>().text = NumberOfDraws.ToString();
            NumberOfDraws = NumberOfDraws - 1;

            //���ó鿨����
            Cards cards = Common.sQLh.PrintRandomBookResult(Common.sQLh.ReadFullTable("Cards"));
            //�����û��ѻ�õ��ֵ�,�������ʹ���鼮��Ϣ
            string key = cards.getId();
            while (DataStructure.PlayerCardsDictionary.ContainsKey(key) && DataStructure.PlayerCardsDictionary.Count != DataStructure.cardsDictionary.Count)
            {
                cards = Common.sQLh.PrintRandomBookResult(Common.sQLh.ReadFullTable("Cards"));
                key = cards.getId();
            }

            DataStructure.PlayerCardsDictionary.Add(key, cards);

            //�������齱�����ͼƬ·��
            randomCardPath = cards.getImagePath();


            //��̬������ͼ��ֵ���鿨��������
            targetObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(randomCardPath);
            ChildObject.SetActive(true);

            //�û�����һ�������Ч��,���Եý�Ӳ
            Debug.Log("waiting");
            Thread.Sleep(200);


            //�齱��۳����,���µ����ݿ���
            Common.updateCountNumber(minimumNumber);
            Debug.Log("��ʣ��" + Common.countNumber + "���");

            //��ȡ��ʾ��ҵĿؼ�,��ʾ������Ľ����
            CoinsText = GameObject.Find("Canvas/Coins/conisCount").GetComponent<Text>();
            CoinsText.text = Common.countNumber.ToString();

        }
    }

    public void loopDraws()
    {
        GameObject ParentObject;
        GameObject ChildObject;
        
        //��ȡ�鿨��������,ʵ�ֵ��������һ������Ч��
        ParentObject = GameObject.Find("Canvas/cardFathermultify");
        ChildObject = ParentObject.transform.Find("PanelCard").gameObject;
        ChildObject.SetActive(false);


        if (NumberOfDraws > 0)
        {
            Debug.Log("NumberOfDraws"+NumberOfDraws);
            startCheak();

        }
        else
        {   //���±ջ�
            NumberOfDraws = 10;
        }
    }

    public void closePanel()
    {
        GameObject ParentObject;
        GameObject ChildObject;
        ParentObject = GameObject.Find("Canvas/cardFathermultify");
        ChildObject = ParentObject.transform.Find("PanelCard").gameObject;
        ChildObject.SetActive(false);
    }

}
