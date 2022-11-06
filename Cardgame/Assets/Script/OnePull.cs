using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using DataBaseUtils;

namespace DrawsUtils
{
    public class OnePull : MonoBehaviour
    {

        //��¼�齱�������.�����жϽ���Ƿ��㹻
        public int minimumNumber;
        //��¼�齱����,�������ı�齱����.(����ʵ�ֳ齱�ۿ�(���籾����1��10���,10��80���))
        public int NumberOfDraws;

        //����ʵʱ��ʾ�������
        private Text CoinsText;
        //���ڼ�¼�齱�����ͼƬ·��,ͨ�����������ʾ��ui������
        private string randomCardPath;

        private GameObject ChildObject;
        private GameObject ParentObject;

        void Start()
        {
            try
            {
                Common.sQLh = new SQLiteHelp(Common.FilePath);
                //Common.sQLh.ReadFullTable("ItemsObtained");
                Common.sQLh.LoadObtainedBookList(Common.sQLh.ReadFullTable("ItemsObtained"));
                Debug.Log("����ItemsObtained�ɹ�");
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
            catch
            {
                Debug.Log("����ItemsObtainedʧ��");
            }
        }

            public void startCheak()
        {
           
            
            //�жϱ߽�����
            if (Common.countNumber < minimumNumber)
            {
                //��ʾʧ�����,�ı���ʾ��Ҳ���
                ParentObject = GameObject.Find("Canvas/errorFather");
                ChildObject = ParentObject.transform.Find("PanelErrror").gameObject;
                ChildObject.SetActive(true);
            }else if((DataStructure.PlayerCardsDictionary.Count == DataStructure.cardsDictionary.Count))
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
             
                //��ȡ�鿨��������
                ParentObject = GameObject.Find("Canvas/cardFather");
                ChildObject = ParentObject.transform.Find("PanelCard").gameObject;
                targetObject = ChildObject.transform.Find("BookPanel").gameObject;

                
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



    }

}

