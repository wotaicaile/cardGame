using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowclubNumber : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas/Panel/Image/ClubText").gameObject.GetComponent<Text>().text = DataStructure.ClubValues.ToString();
    }


}
