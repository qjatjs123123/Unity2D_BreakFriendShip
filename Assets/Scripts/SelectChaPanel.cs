using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SelectChaPanel : MonoBehaviour
{
    public Image select_img;
    public Image[] img;
    public static int char_num = 0;
    public GameObject NetworkManager;
    public int num = 0;
    public void select_character(int num)
    {
        if (num == 0)
        {
            select_img.transform.GetComponent<Image>().sprite = img[0].GetComponent<Image>().sprite;
            char_num  = 1;
        }
        else if (num == 1)
        {
            select_img.transform.GetComponent<Image>().sprite = img[1].GetComponent<Image>().sprite;
            char_num = 2;
        }
        else if (num == 2)
        {
            select_img.transform.GetComponent<Image>().sprite = img[2].GetComponent<Image>().sprite;
            char_num = 3;
        }
        else if (num == 3)
        {
            select_img.transform.GetComponent<Image>().sprite = img[3].GetComponent<Image>().sprite;
            char_num = 4;
        }
        
    }



    public void btn_next()
    {
        Debug.Log(char_num);
        if(char_num != 0)
        {
            PhotonNetwork.ConnectUsingSettings(); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
