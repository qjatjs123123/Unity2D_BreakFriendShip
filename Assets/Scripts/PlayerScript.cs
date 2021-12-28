using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;
    public int Round = 0;

    string[] player_name = new string[4];
    static string[] player_jump = { "0", "0", "0", "0" };
    static bool[] player_ground = { true, true, true, true };
    static string[] player_axis = { "0", "0", "0", "0" };
    static bool[] player_isLeft = { false, false, false, false };

    public bool isGround;
    public bool isRun;
    public bool isLeft;
    Vector3 curPos;
    int jumpCount = 0;

    string direction;
    public bool isPlayerDie = false;

    public AudioSource mysfx;
    public AudioClip jumpsfx;


    void Awake()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            player_name[i] = player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text; //1~4P �޾ƿ���
        }

        // �г��� ǥ��
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;

        //�ڱ� �÷��̾ ����ٴϴ� ī�޶� ����
        //CM�� �ó׸ӽ� ī�޶� ����
        //������ ����
        if (PV.IsMine)
        {
            var CM = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }

    void Update()
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text);
        #region 1. ĳ���� ���ÿ� �����̸� ��� �����̱� (�� �� ��)
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");


        if (Round == 4)
        {

            

            /*ĳ���� ���ÿ� �����Ǿ��� ��*/
            int k = 0;
            for (k = 0; k < player.Length; k++)
            {
                if (player_ground[k] == false)
                {
                    break;
                }
            }
            if (k == player.Length)
            {
                GameObject.Find("R4_Clear").transform.GetChild(0).GetComponent<Together_Move>().Move_to_Up();
                //���������Ǿ����� �����Լ�
            }

            /*ĳ���� ���ÿ� �����Ǿ��� ��*/
            //int k1 = 0;
            //for (k1 = 0; k1 < player.Length; k1++)
            //{
            //    if (player_axis[k1] != "0")
            //    {
            //        break;
            //    }
            //}
            //if (k1 == player.Length)
            //{
            //    //Debug.Log(player_axis[0]);
            //    //ĳ���Ͱ� �����Ǿ����� ����
            //}

            /*ĳ���� ���� ���������� ������ ��*/
            int k2 = 0;
            for (k2 = 0; k2 < player.Length; k2++)
            {
                if (player_isLeft[k2] != false)
                {
                    break;
                }
            }
            if (k2 == player.Length)
            {

                GameObject.Find("R4_Clear").transform.GetChild(0).GetComponent<Together_Move>().Move_to_Right();
                // ĳ���Ͱ� ���������� ���������� ����
            }


            /*ĳ���� ���ÿ� �������� �������� ��*/
            int k3 = 0;
            for (k3 = 0; k3 < player.Length; k3++)
            {
                if (player_isLeft[k3] != true)
                {
                    break;
                }
            }
            if (k3 == player.Length)
            {
                GameObject.Find("R4_Clear").transform.GetChild(0).GetComponent<Together_Move>().Move_to_Left();
            }

            /*ĳ���͵��� ���� �ٸ��� �������� ��*/
            if (k != player.Length  && k2 != player.Length && k3 != player.Length)
            {
                //��ü �����Լ� ����
            }
        }
        #endregion

        //��Ʈ ������
        if (Round == 6)
        {
            int m = 0;
            for (m = 0; m < player.Length; m++)
            {
                if (player_isLeft[m] != false)
                {
                    break;
                }
            }

            if (m == player.Length)
            {
                GameObject.Find("R6_GhostMonster").GetComponent<GhostScript>().GhostMove();
            }

            if (m != player.Length)
            {
            }
        }

        if (PV.IsMine)
        {
            //string[] name_jump_list = new string[2];
            //string[] name_axis_list = new string[2];
            //string[] name_isLeft_list = new string[2];

            //name_jump_list[0] = PhotonNetwork.LocalPlayer.NickName;
            //name_axis_list[0] = PhotonNetwork.LocalPlayer.NickName;
            //name_isLeft_list[0] = PhotonNetwork.LocalPlayer.NickName;

            

            // <-(-1 ��ȯ), ->(1 ��ȯ) �̵�, �ȴ����� 0 ��ȯ
            float axis = Input.GetAxisRaw("Horizontal");
            //name_axis_list[1] = axis.ToString();

            //PV.RPC("RoomMaster_Axis", RpcTarget.All, name_axis_list);

            if (axis == -1)
            {
                //name_isLeft_list[1] = "true";
                isLeft = true;
                //PV.RPC("RoomMaster_isLeft", RpcTarget.All, name_isLeft_list);
            }
            if (axis == 1)
            {
                //name_isLeft_list[1] = "false";
                isLeft = false;
                //PV.RPC("RoomMaster_isLeft", RpcTarget.All, name_isLeft_list);
            }

            // transform���� �ϰԵǸ� ���� �ε�ĥ ��� ���� �հ� ������ ��
            RB.velocity = new Vector2(4 * axis, RB.velocity.y);

            if (axis != 0)
            {
                isRun = true;

                AN.SetBool("isRun", true);
                PV.RPC("FilpXRPC", RpcTarget.AllBuffered, axis);// �����ӽ� FilpX�� ����ȭ���ֱ� ���ؼ� AllBuffered
                PV.RPC("RunOn", RpcTarget.All);
            }
            else
            {
                isRun = false;

                AN.SetBool("isRun", false);
                PV.RPC("RunOFF", RpcTarget.All);
            }

            // ����, �ٴ�üũ
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.5f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));


            AN.SetBool("isJump", !isGround);
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
            {
                //Debug.Log(jumpCount);
                PV.RPC("JumpRPC", RpcTarget.All, isGround);
                //name_jump_list[1] = "1";
                JumpSound();
                jumpCount++;

                AN.SetBool("isDoubleJump", !isGround);
                PV.RPC("DoubleJumpRPC", RpcTarget.All, isGround);

                //name_jump_list[1] = "1";
                //PV.RPC("RoomMaster_Jump", RpcTarget.All, name_jump_list);
                // Debug.Log(jumpCount);
            }
            if (isGround)
            {
                jumpCount = 0;

                AN.SetBool("isDoubleJump", false);

                //name_jump_list[1] = "0";
                PV.RPC("JumpRpcOff", RpcTarget.All);
                PV.RPC("DoubleJumpOffRPC", RpcTarget.All);
                //PV.RPC("RoomMaster_Jump", RpcTarget.All, name_jump_list);
                //  PV.RPC("RunOFF", RpcTarget.All);
            }



        }

        //IsMine�� �ƴ� �͵��� �ε巴�� ��ġ ����ȭ
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);


        //�÷��̾ ����ִ��� üũ,(������ �ִϸ��̼� ����)
        if (isPlayerDie)
        {
            PV.RPC("PlayerDieRPC", RpcTarget.All);
            isPlayerDie = !isPlayerDie;
        }
        //else
        //{
        //    PV.RPC("PlayerRespawnRPC", RpcTarget.All);            
        //}
    }
    [PunRPC]
    void RoomMaster_isLeft(string[] name_isLeft_list)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");


        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == name_isLeft_list[0])
            {
                if (name_isLeft_list[1] == "true")
                    player_isLeft[i] = true;
                else if (name_isLeft_list[1] == "false")
                    player_isLeft[i] = false;
            }
        }
    }


    [PunRPC]
    void RoomMaster_Axis(string[] name_axis_list)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");


        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == name_axis_list[0])
            {
                player_axis[i] = name_axis_list[1];
            }
        }
    }



    [PunRPC]
    void RoomMaster_Jump(string[] name_jump_list)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");


        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == name_jump_list[0])
            {
                player_jump[i] = name_jump_list[1];
            }
        }
    }


    [PunRPC]
    void JumpRpcOff()
    {
        AN.SetBool("isJump", false);
    }

    [PunRPC]
    void RunOFF()
    {
        AN.SetBool("isRun", false);
    }

    [PunRPC]
    void RunOn()
    {
        AN.SetBool("isRun", true);
    }
    [PunRPC]
    void DoubleJumpOffRPC()
    {
        AN.SetBool("isDoubleJump", false);
    }

    [PunRPC]
    void FilpXRPC(float axis)
    {
        SR.flipX = axis == -1;
    }// ���� Ű�� ���� ��� True ��ȯ ������ Ű�� ������ ��� False ��ȯ

    [PunRPC]
    void JumpRPC(bool isGround)
    {
        AN.SetBool("isJump", !isGround);
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up * 300);
    }

    [PunRPC]
    void DoubleJumpRPC(bool isGround)
    {
        AN.SetBool("isDoubleJump", !isGround);
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up * 300);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(PV.Owner.NickName);
            stream.SendNext(isLeft);
            stream.SendNext(isGround);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            string PV_name = (string)stream.ReceiveNext();
            bool PV_isLeft = (bool)stream.ReceiveNext();
            bool PV_isGround = (bool)stream.ReceiveNext();
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == PV_name)
                {
                    player_isLeft[i] = PV_isLeft;
                    player_ground[i] = PV_isGround;
                }
            }

        }
    }

    //�÷��̾� �����
    [PunRPC]
    void PlayerDieRPC()
    {
        AN.SetBool("Appearing", true);
    }

    //�÷��̾� ��Ȱ��
    [PunRPC]
    void PlayerRespawnRPC()
    {
        AN.SetBool("Appearing", false);
    }

    public void JumpSound()
    {
        mysfx.PlayOneShot(jumpsfx);
    }
}