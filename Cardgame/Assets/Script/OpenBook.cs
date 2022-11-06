using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBook : MonoBehaviour
{
    public void Start()
    {

        GameObject ParentObject = GameObject.Find("Canvast").gameObject;
        GameObject ChildObject = ParentObject.transform.Find("BookInside/ButtonOpen").gameObject;
       // ChildObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Annakarenina");


 
        ChildObject.SetActive(true);
    }
}
