using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class R2_Trigger1_Enter : MonoBehaviour
{
    public BulletScript bullet;
    static string[] player_floorIn = { "0", "0", "0", "0" };
    string[] player_name = new string[4];
    public PhotonView PV;
    public Tilemap BreakTile;

    void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            player_name[i] = player[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
        }
    }
    // Ʈ���� ���Ϳ� ������ �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {             
        if (collision.tag == "Player")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            string[] name_and_isIn = new string[2]; // �̸�, �÷��̾ ���� �ִ��� ������ ���� ����
            string LocalPlayer = collision.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text; // ���� �ڱ��ڽ� �÷��̾� �г���

            //���� �ڱ� �ڽ��̸� ���� �÷��� �ϰ��ִ� �÷��̾� �ε��� ��ġ ã�� �� �ε��� ��ġ�� 0����
            if (LocalPlayer == PhotonNetwork.LocalPlayer.NickName)
            {
                for (int i = 0; i < player.Length; i++)
                {
                    if (player_name[i] == LocalPlayer)
                    {
                        player_floorIn[i] = "1"; // 1�� �� ���� �ְ� 0�� �� ���� ����
                        name_and_isIn[0] = LocalPlayer;
                        name_and_isIn[1] = player_floorIn[i];
                        PV.RPC("ElevatorInRPC", RpcTarget.All, name_and_isIn);
                        break;
                    }
                }
            }


            int k = 0;
            for (k = 0; k < player.Length; k++)
            {
                if (player_floorIn[k] == "0")
                {
                    break;
                }
            }

            if (k == player.Length)
            {
                Debug.Log("if�� ����");
                bullet.BulletScriptTriiger = true;
                BreakTile.gameObject.SetActive(true);

                PV.RPC("BreakTileRPC", RpcTarget.All);
                for (k = 0; k < 4; k++)
                {
                    player_floorIn[k] = "0";
                }
            }
        }
    }

    // Ʈ���� ���Ϳ� ������ �� ����
    private void OnTriggerExit2D(Collider2D collision)
    {


        //if (collision.name == "Round2_Apple1" || collision.name == "Round2_Apple2" || collision.name == "Round2_Apple3" || collision.name == "Box1" || collision.name == "Box2" || collision.name == "Box3" || collision.name == "Square" || collision.name == "Bullet") { } //����ó�� ���
        if (collision.tag == "Player")
        {
            Debug.Log(collision.name);
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            string[] name_and_isIn = new string[2]; // �̸�, �÷��̾ ���� �ִ��� ������ ���� ����
            string LocalPlayer = collision.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text; // ���� �ڱ��ڽ� �÷��̾� �г���
                                                                                                                             //���� �ڱ� �ڽ��̸� ���� �÷��� �ϰ��ִ� �÷��̾� �ε��� ��ġ ã�� �� �ε��� ��ġ�� 0����
            if (LocalPlayer == PhotonNetwork.LocalPlayer.NickName)
            {
                for (int i = 0; i < player.Length; i++)
                {
                    if (player_name[i] == LocalPlayer)
                    {
                        player_floorIn[i] = "0"; // 1�� �� ���� �ְ� 0�� �� ���� ����
                        name_and_isIn[0] = LocalPlayer;
                        name_and_isIn[1] = player_floorIn[i];
                        PV.RPC("ElevatorOutRPC", RpcTarget.All, name_and_isIn);
                        break;
                    }
                }
            }
        }
    }
    [PunRPC]
    void BreakTileRPC()
    {
        bullet.BulletScriptTriiger = true;
        BreakTile.gameObject.SetActive(true);
        for (int k = 0; k < 4; k++)
        {
            player_floorIn[k] = "0";
        }
    }


    [PunRPC]
    void ElevatorOutRPC(string[] name_and_elevator)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            Debug.Log(player_name[i]);
            if (player_name[i] == name_and_elevator[0])
            {
                player_floorIn[i] = "0";
                break;
            }
        }
    }

    [PunRPC]
    void ElevatorInRPC(string[] name_and_elevator)
    {

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            Debug.Log(player_name[i]);
            if (player_name[i] == name_and_elevator[0])
            {
                player_floorIn[i] = "1";
                break;
            }
        }
    }
}
