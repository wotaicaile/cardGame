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
    //��ȡ�б�
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


        GameObject targetObject;
        
        GameObject ParentObject = GameObject.Find("Canvas/Scroll View/Viewport/Content").gameObject;
        targetObject = ParentObject.transform.Find("ButtonGuide").gameObject;
        string ImagePath;
        string buttonName;

        foreach (KeyValuePair<string, Cards> kv in DataStructure.PlayerCardsDictionary)

        {
           
            //��ȡ�б��е�����
            DataStructure.PlayerCardsDictionary.TryGetValue(kv.Key, out cards);
            //Debug.Log("�û���ȡ���б�:"+kv.Key +","+ kv.Value);
           

            //��¡���ui�����������������
            buttonName = "Button" +"_"+ kv.Key;
            GameObject buttonTest = GameObject.Instantiate(targetObject);
            buttonTest.name = buttonName;
            buttonTest.transform.SetParent(ParentObject.transform);  //��ParentObject���ó�Button�ĸ�����

            //���뵽GridLayoutGroup������    
            buttonTest.transform.position = Vector3.zero;
            buttonTest.transform.localScale = Vector3.one;

            //��ͼ
            ImagePath = cards.getImagePath();
            buttonTest.GetComponent<Image>().sprite = Resources.Load<Sprite>(ImagePath);

          
        }

    }


    public void openBookButton()
    {

        GameObject ParentObject = GameObject.Find("Canvas/BookInside").gameObject;
        GameObject ChildObject = ParentObject.transform.Find("ButtonOpen").gameObject;
        
        // ChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Annakarenina");
        
        TextDescription = GameObject.Find("Canvas/TextDescription").GetComponent<Text>(); //��unity��Ķ���;

        //��ȡ��Ӧ��ŵ�����
        Name = EventSystem.current.currentSelectedGameObject.name.Split('_');
       
        try
        {
            DataStructure.PlayerCardsDictionary.TryGetValue(Name[1], out cards);

            TextDescription.text = cards.getDescription();
           
            ChildObject.SetActive(true);
        }
        catch
        {
            TextDescription.text = "����ָ�����ѵ���������Ϸ���磿�ȴ򿪿�����";

            ChildObject.SetActive(true);
        }
        
    }
    public void openBook()
    {

        GameObject ParentObject = GameObject.Find("Canvas/BookInside").gameObject;
        GameObject ChildObject = ParentObject.transform.Find("BookInsidePanel").gameObject;
        int temp;
        TextBook = ChildObject.transform.Find("TextBook").GetComponent<Text>(); //��unity��Ķ���;


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
                ChildObject.transform.Find("ClueAddText").GetComponent<Text>().text = "����ֵ+"+temp; //��unity��Ķ���;
                
            }
            catch
            {
                Debug.Log("�Ѿ�ʹ�ù�");
                ChildObject.transform.Find("ClueAddText").gameObject.SetActive(false);
            }

        ChildObject.SetActive(true);
        }
        catch
        {
            ChildObject.transform.Find("ClueAddText").gameObject.SetActive(false);
            TextBook.text = "����ǰ������һƫƧСɽ�帽��������ȫ����·���û���������������䡣\n�峤���̳����ڿ�ʱ�����˻赹�ڵ��ϵ��㣬�������㣬��������һ�Ѹ��ӣ���˵��ȫ����õ�һ�ѣ���ϣ��������������";
        ChildObject.SetActive(true);
        }


    }
}
