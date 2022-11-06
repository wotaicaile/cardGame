using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
   public void ClickBtn()
    {
       // DataStructure.ClubValues = 65;
    
        if (DataStructure.ClubValues >= 65)
        {
            GameObject ParentObject = GameObject.Find("Canvas/Panel/ImageCircle/npc").gameObject;
            GameObject ChildObject = ParentObject.transform.Find("npcImage").gameObject;
            ChildObject.SetActive(true);
        }
    }
}
