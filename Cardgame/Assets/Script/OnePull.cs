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

        //记录抽奖最低数额.用于判断金币是否足够
        public int minimumNumber;
        //记录抽奖次数,用于灵活改变抽奖次数.(可以实现抽奖折扣(例如本程序1次10金币,10次80金币))
        public int NumberOfDraws;

        //用于实时显示金币数量
        private Text CoinsText;
        //用于记录抽奖结果的图片路径,通过程序控制显示到ui界面上
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
                Debug.Log("查找ItemsObtained成功");
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
            catch
            {
                Debug.Log("查找ItemsObtained失败");
            }
        }

            public void startCheak()
        {
           
            
            //判断边界条件
            if (Common.countNumber < minimumNumber)
            {
                //显示失败面板,文本提示金币不足
                ParentObject = GameObject.Find("Canvas/errorFather");
                ChildObject = ParentObject.transform.Find("PanelErrror").gameObject;
                ChildObject.SetActive(true);
            }else if((DataStructure.PlayerCardsDictionary.Count == DataStructure.cardsDictionary.Count))
            {
                //显示失败面板,文本提示金币不足
                ParentObject = GameObject.Find("Canvas/errorFather");
                ChildObject = ParentObject.transform.Find("PanelErrror").gameObject;
                ChildObject.transform.Find("error").GetComponent<Text>().text = "卡池被抽空";
                ChildObject.SetActive(true);
      
            }
            else
            {

                GameObject targetObject;
             
                //获取抽卡结果的面板
                ParentObject = GameObject.Find("Canvas/cardFather");
                ChildObject = ParentObject.transform.Find("PanelCard").gameObject;
                targetObject = ChildObject.transform.Find("BookPanel").gameObject;

                
                //调用抽卡函数
                Cards cards = Common.sQLh.PrintRandomBookResult(Common.sQLh.ReadFullTable("Cards"));

               
                 //加入用户已获得的字典,方便程序使用书籍信息
                string key = cards.getId();
                while (DataStructure.PlayerCardsDictionary.ContainsKey(key) && DataStructure.PlayerCardsDictionary.Count != DataStructure.cardsDictionary.Count)
                {
                    cards = Common.sQLh.PrintRandomBookResult(Common.sQLh.ReadFullTable("Cards"));
                    key = cards.getId();
                }
                DataStructure.PlayerCardsDictionary.Add(key, cards);


                //获得随机抽奖结果的图片路径
                randomCardPath = cards.getImagePath(); 
                //动态加载贴图赋值给抽卡结果的面板
                targetObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(randomCardPath);
                ChildObject.SetActive(true);
                //让画面有一个缓冲的效果,不显得僵硬
                Debug.Log("waiting");
                Thread.Sleep(200);


                //抽奖完扣除金币,更新到数据库中
                Common.updateCountNumber(minimumNumber);
                Debug.Log("还剩下" + Common.countNumber + "金币");


                //获取显示金币的控件,显示更新完的金币数
                CoinsText = GameObject.Find("Canvas/Coins/conisCount").GetComponent<Text>();
                CoinsText.text = Common.countNumber.ToString();

            }
        }



    }

}

