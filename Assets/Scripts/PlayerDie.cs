using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{
    //public PhotonView PV;    
    public GameObject Player;
    public GameObject PlayerSpawn;
    public GameObject R6_GhostRespawn;
    public GameObject R6_GhostMonster;
    //public Rigidbody2D PlayerPosition;
    //public PlayerScript playerscript;
    //public Animator AN;

    //[SerializeField]
    //private bool isCharacterDie = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        //if (isCharacterDie) 
        {
            //ĳ���� ����� �����Լ�
            //PlayerDieRPC();
        }

    }

    //DieArea ���Խ�
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int index = 0;
            Debug.Log("dieArea ����!");
            for (index = 0; index < Player.transform.childCount; index++) {
                if (Player.transform.GetChild(index).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == PhotonNetwork.LocalPlayer.NickName) 
                {
                    break;
                }
            }

            //Respawn�Լ�
            Player.transform.GetChild(index).transform.position = new Vector3(PlayerSpawn.transform.position.x, PlayerSpawn.transform.position.y, PlayerSpawn.transform.position.z);
            R6_GhostMonster.transform.position = new Vector3(R6_GhostRespawn.transform.position.x, R6_GhostRespawn.transform.position.y, R6_GhostRespawn.transform.position.z);

            //������ �ִϸ��̼�, ����
            //playerscript = Player.transform.GetChild(index).GetComponent<PlayerScript>();
            //playerscript.isPlayerDie = true;


        }
    }

}
