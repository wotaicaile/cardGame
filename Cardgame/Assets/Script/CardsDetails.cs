using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBaseUtils;
using UnityEngine.EventSystems;


public class CardsDetails : MonoBehaviour
{

    private Text TextBook;
    private Text TextDescription;
    private string BookImagePath;
    private Cards cards;
    private string[] Name;

    // Start is called before the first frame update
    //获取列表
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


        GameObject targetObject;
        
        GameObject ParentObject = GameObject.Find("Canvas/Scroll View/Viewport/Content").gameObject;
        targetObject = ParentObject.transform.Find("ButtonGuide").gameObject;
        string ImagePath;
        string buttonName;

        foreach (KeyValuePair<string, Cards> kv in DataStructure.PlayerCardsDictionary)

        {
           
            //获取列表中的数据
            DataStructure.PlayerCardsDictionary.TryGetValue(kv.Key, out cards);
            //Debug.Log("用户获取的列表:"+kv.Key +","+ kv.Value);
           

            //克隆书的ui，并且命名放入界面
            buttonName = "Button" +"_"+ kv.Key;
            GameObject buttonTest = GameObject.Instantiate(targetObject);
            buttonTest.name = buttonName;
            buttonTest.transform.SetParent(ParentObject.transform);  //把ParentObject设置成Button的父物体

            //放入到GridLayoutGroup布局中    
            buttonTest.transform.position = Vector3.zero;
            buttonTest.transform.localScale = Vector3.one;

            //贴图
            ImagePath = cards.getImagePath();
            buttonTest.GetComponent<Image>().sprite = Resources.Load<Sprite>(ImagePath);

          
        }

    }


    public void openBookButton()
    {

        GameObject ParentObject = GameObject.Find("Canvas/BookInside").gameObject;
        GameObject ChildObject = ParentObject.transform.Find("ButtonOpen").gameObject;
        
        // ChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Annakarenina");
        
        TextDescription = GameObject.Find("Canvas/TextDescription").GetComponent<Text>(); //在unity里的定义;

        //获取对应编号的描述
        Name = EventSystem.current.currentSelectedGameObject.name.Split('_');
       
        try
        {
            DataStructure.PlayerCardsDictionary.TryGetValue(Name[1], out cards);

            TextDescription.text = cards.getDescription();
           
            ChildObject.SetActive(true);
        }
        catch
        {
            TextDescription.text = "新手指引？难道我身处在游戏世界？先打开看看吧";

            ChildObject.SetActive(true);
        }
        
    }
    public void openBook()
    {

        GameObject ParentObject = GameObject.Find("Canvas/BookInside").gameObject;
        GameObject ChildObject = ParentObject.transform.Find("BookInsidePanel").gameObject;
        int temp;
        TextBook = ChildObject.transform.Find("TextBook").GetComponent<Text>(); //在unity里的定义;


        Debug.Log(Name[0]);
        try
        {
        DataStructure.PlayerCardsDictionary.TryGetValue(Name[1], out cards);

        TextBook.text = cards.getDetail();


            try
            {
                //Debug.Log("1111111111111111111111111111111111111111"+books.getId());

                DataStructure.PlayerCardsDictionaryAlaredyUsed.Add(cards.getId(),0);
                int.TryParse(cards.getValue(), out temp);
                DataStructure.ClubValues = DataStructure.ClubValues + temp;
                //Debug.Log(DataStructure.ClubValues);
                ChildObject.transform.Find("ClueAddText").gameObject.SetActive(true);
                ChildObject.transform.Find("ClueAddText").GetComponent<Text>().text = "线索值+"+temp; //在unity里的定义;
                
            }
            catch
            {
                Debug.Log("已经使用过");
                ChildObject.transform.Find("ClueAddText").gameObject.SetActive(false);
            }

        ChildObject.SetActive(true);
        }
        catch
        {
            ChildObject.transform.Find("ClueAddText").gameObject.SetActive(false);
            TextBook.text = "两日前，你于一偏僻小山村附近醒来，全身除衣服外没有其他，包括记忆。\n村长奶奶出门挖矿时发现了昏倒在地上的你，她收留你，并且送你一把稿子，据说是全村最好的一把，她希望你有能力更生";
        ChildObject.SetActive(true);
        }


    }
}
