using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")]
    public InputField NickNameInput;
    public GameObject DisconnectPanel;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField RoomInput;
    public Text WelcomeText;
    public Text LobbyInfoText;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;
    public Image Lobby_Img;
    public Text NicknameText;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text ListText;
    public Text RoomInfoText;
    public Text[] ChatText;
    public InputField ChatInput;
    public GameObject Left;

    [Header("ETC")]
    public Text StatusText;
    public PhotonView PV;
    //public GameObject[] P;
    public Image[] img;
    public GameObject timer;
    public GameObject prefab;
    

    [Header("Canvas")]
    public GameObject canvas;

    [Header("SelectCharacterImagePanel")]
    public GameObject SelectCharacterImagePanel;
    public Image selectImg;

    int Max_Player = 0;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;


    public GameObject Camera;
    
    public Transform SpawnPosition_P1;
    public Transform SpawnPosition_P2;
    public Transform SpawnPosition_P3;
    public Transform SpawnPosition_P4;
    public GameObject PlayerObj;
    //public Image deathImg;
    public static string RoomMaster = "";

    public void MaxPlayer(int num)
    {
        Max_Player = num;
    }

    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    void MyListRenewal()
    {
        // �ִ�������
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // ����, ������ư
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // �������� �´� ����Ʈ ����
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 100;
    }

    private void Update()
    {
        // ��Ʈ��ũ ����ǥ�� ����
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();

        //�κ� ���Ӽ� �� �� ���Ӽ� ǥ�� ����
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "�κ� / " + PhotonNetwork.CountOfPlayers + "����";

    }

    //������ ���� , Master������ �����ϸ� OnConnectedToMaster �ݹ�
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    // Master����, ���°� �Ǹ� �κ� ����
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    //JoinLobby() �ݹ��Լ��� ����
    public override void OnJoinedLobby()
    {
        SelectCharacterImagePanel.SetActive(false);

        Debug.Log("�κ�����");

            LobbyPanel.SetActive(true);
            DisconnectPanel.SetActive(false);
            PhotonNetwork.LocalPlayer.NickName = PlayFabManager.Nickname;
            WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "�� ȯ���մϴ�.";
            NicknameText.text = PhotonNetwork.LocalPlayer.NickName + " ��";

            Lobby_Img.transform.GetComponent<Image>().sprite = selectImg.GetComponent<Image>().sprite;
            

  



    }

    public void Collect_player()
    {
        Debug.Log("Collect����");
        GameObject[] Players;
        // Start is called before the first frame update

        Players = GameObject.FindGameObjectsWithTag("Player");



        // Update is called once per frame

        for (int i = 0; i < Players.Length; i++)
        {
            Debug.Log(Players[i].name);
            Players[i].transform.parent = PlayerObj.transform;
        }

    }

    //���� �������
    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (NickNameInput.text != "")
        {
            LobbyPanel.SetActive(false);
            RoomPanel.SetActive(false);
            NickNameInput.text = "";
        }

    }

    public void CreateRoom()
    {
        for (int i = 0; i < Left.transform.childCount; i++)
        {
            Destroy(Left.transform.GetChild(i).gameObject);
        }
        if (RoomInput.text == "")
        {

        }
        else
        {
            Debug.Log("�����");

            // ���̸��� RoomInput.text�� ��ɼ��� �ִ��ο��� 2�� ���������� ����Ǹ� OnJoinedRoom �ݹ��Լ�����
            if (Max_Player == 2)
            {

                PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 2 });
                //P[2].SetActive(false);
                //P[3].SetActive(false);
            }
            else if (Max_Player == 3)
            {
                PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 3 });
               // P[3].SetActive(false);
            }

            else if (Max_Player == 4)
                PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 4 });

        }
        RoomMaster = PhotonNetwork.LocalPlayer.NickName;
    }

    //ĳ���� ���� �Լ�
    public void Spawn()
    {
        Debug.Log("Spawn�Լ�");
        if (SelectChaPanel.char_num == 1)
        {
            GameObject Player = PhotonNetwork.Instantiate("MaskDude", SpawnPosition_P1.position, SpawnPosition_P1.rotation) as GameObject;            
            Player.transform.parent = PlayerObj.transform;          


        }
        else if (SelectChaPanel.char_num == 2)
        {           
            GameObject Player = PhotonNetwork.Instantiate("NinjaFrog", SpawnPosition_P2.position, SpawnPosition_P2.rotation) as GameObject;
            Player.transform.parent = PlayerObj.transform;


        }
        else if (SelectChaPanel.char_num == 3)
        {
            GameObject Player = PhotonNetwork.Instantiate("PinkMan", SpawnPosition_P3.position, SpawnPosition_P3.rotation) as GameObject;
            Player.transform.parent = PlayerObj.transform;

        }
        else if (SelectChaPanel.char_num == 4)
        {

            GameObject Player = PhotonNetwork.Instantiate("VitualGuy", SpawnPosition_P4.position, SpawnPosition_P4.rotation) as GameObject;
            Player.transform.parent = PlayerObj.transform;

        }
    }


    // �Լ� createRoom�� ���������� �������� ������ ��� ����
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
    }

    // �Լ� JoinRandomRoom�� ���������� �������� ������ ��� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
    }

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        RoomRenewal();
        Debug.Log("OnPlayerEnteredRoom����");
    }

    [PunRPC]
    public void player_left_room(string o)
    {
        for (int i = 0; i < Left.transform.childCount; i++)
        {
            if (Left.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text == o)
            {
                Destroy(Left.transform.GetChild(i).gameObject);
                break;
            }
        }
        for (int i = 0; i < Left.transform.childCount; i++)
        {
            Left.transform.GetChild(i).name = "P" + (i + 1);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("����");


        
        PV.RPC("player_left_room", RpcTarget.AllBuffered, otherPlayer.NickName);
        RoomRenewal();
    }

    public void btn_roomOut()
    {
        
        Debug.Log(Left.transform.childCount);
        for (int i = 0; i < Left.transform.childCount; i++)
        {         
                Destroy(Left.transform.GetChild(i).gameObject);     
        }
        GameObject a = Instantiate(prefab);
        a.name = "P" + (transform.childCount + 1);
        a.transform.GetChild(1).GetComponent<Text>().text = PhotonNetwork.LocalPlayer.NickName;
        a.transform.parent = Left.transform;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        if (SelectChaPanel.char_num == 1)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[0].GetComponent<Image>().sprite;

        }
        else if (SelectChaPanel.char_num == 2)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[1].GetComponent<Image>().sprite;

        }
        else if (SelectChaPanel.char_num == 3)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[2].GetComponent<Image>().sprite;

        }
        else if (SelectChaPanel.char_num == 4)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[3].GetComponent<Image>().sprite;

        }
        a.SetActive(false);
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        LeaveRoom();
    }



    public void ImageSelect(int num)
    {

        int[] array = new int[2];
        array[0] = num;
        int i = 0;
        for (i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                break;
            }
        }
        array[1] = i;
       // PV.RPC("ImageSelectRPC", RpcTarget.AllBuffered, array);

    }
    [PunRPC]
    public void spawnRPC()
    {
        Spawn();
    }

    public void Gotimer()
    {
        Debug.Log("Gotimer����");
        
        PV.RPC("GotimerRPC", RpcTarget.All);
        PV.RPC("spawnRPC", RpcTarget.All);
    }

    [PunRPC]
    public void GotimerRPC()
    {
        Debug.Log("GotimerRPC����");
        
        timer.SetActive(true);
    }


    void RoomRenewal()
    {
        for (int j = 0; j < Left.transform.childCount; j++)
        {
            for (int i = j + 1; i < Left.transform.childCount; i++)

                if (Left.transform.GetChild(j).transform.GetChild(1).GetComponent<Text>().text == Left.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text)
                {
                    Debug.Log("�ݺ���if������!!");
                    Debug.Log(Left.transform.GetChild(j).transform.GetChild(1).GetComponent<Text>().text);
                    Debug.Log(Left.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text);

                    Destroy(Left.transform.GetChild(j).gameObject);
                    Debug.Log("�ݺ���if����!!");
                    break;

                }

        }

        List<string> namelist = new List<string>();
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
            namelist.Add(PhotonNetwork.PlayerList[i].NickName);
        }
        for (int i = PhotonNetwork.PlayerList.Length; i < 4; i++)
        {
            namelist.Add("");
        }
        for (int i = 0; i < Left.transform.childCount; i++)
        {
        }

        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "�� / " + PhotonNetwork.CurrentRoom.MaxPlayers + "�ִ�";

    }

    [PunRPC]
    public void player_join_room(string[] array1)
    {

        Debug.Log("player_join_room����");
        
        GameObject a = Instantiate(prefab) ;
        
        a.name = "P" + (transform.childCount + 1);
        a.transform.GetChild(1).GetComponent<Text>().text = array1[0];
        a.transform.parent = Left.transform;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        if (int.Parse(array1[1]) == 1)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[0].GetComponent<Image>().sprite;

        }
        else if (int.Parse(array1[1]) == 2)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[1].GetComponent<Image>().sprite;

        }
        else if (int.Parse(array1[1]) == 3)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[2].GetComponent<Image>().sprite;

        }
        else if (int.Parse(array1[1]) == 4)
        {
            a.transform.GetChild(0).GetComponent<Image>().sprite = img[3].GetComponent<Image>().sprite;

        }
        a.SetActive(true);
       
    }

    public override void OnJoinedRoom()
    {

        Debug.Log("OnJoinedRoom����");
        for (int j = 0; j < Left.transform.childCount; j++)
        {
            Debug.Log(Left.transform.GetChild(j).transform.GetChild(1).GetComponent<Text>().text);
        }
            
        
        string[] array1 = new string[2];
        array1[0] = PhotonNetwork.LocalPlayer.NickName;
        array1[1] = SelectChaPanel.char_num.ToString();
        PV.RPC("player_join_room", RpcTarget.AllBuffered, array1);
        
        for (int j = 0; j < Left.transform.childCount; j++)
        {
            Left.transform.GetChild(j).gameObject.SetActive(true);
                
        }
        RoomRenewal();

        

        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";

        RoomPanel.SetActive(true);
        
    }

    public void Send()
    {
        string msg = PhotonNetwork.NickName + " : " + ChatInput.text;
        PV.RPC("ChatRPC", RpcTarget.AllBuffered, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";

    }

    [PunRPC] // RPC�� �÷��̾ �����ִ� �� ��� �ο����� �����Ѵ�
    void ChatRPC(string msg)
    {
        bool isInput = false;

        for (int i = 0; i < ChatText.Length; i++)
        {

            if (ChatText[i].text == "")
            {
                Debug.Log("chat if��: " + i);
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        }
        if (!isInput) // ������ ��ĭ�� ���� �ø�
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }

    //��ư
    public void Ready()
    {
        Debug.Log("Ready����");
        int i = 0;
        for ( i = 0; i < Left.transform.childCount; i++)
        {
            if (Left.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text == PhotonNetwork.LocalPlayer.NickName)
            {
                break;
            }
        }


        PV.RPC("ReadyRPC", RpcTarget.AllBuffered, Left.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text);
        ready_check();

    }
    public void ready_check()
    {
        int count = 0;
        for (int j = 0; j < Left.transform.childCount; j++)
        {
            if (Left.transform.GetChild(j).transform.GetChild(2).GetComponent<Text>().text == "<color=#ff0000>" + "Ready" + "</color>")
                count = count + 1;
        }
       
        if (count == Left.transform.childCount && Left.transform.childCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Gotimer();
        }
       
    }


    [PunRPC]
    public void ReadyRPC(string o)
    {
        Debug.Log("ReadyRPC����");
        //int count = 0;
        for(int i = 0; i < Left.transform.childCount; i++)
        {
            if(o == Left.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text)
            {
                if (Left.transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text == "<color=#ff0000>" + "Ready" + "</color>")
                {
                    Left.transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "<color=black>" + "Ready" + "</color>";
                }
                else
                {
                    Left.transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "<color=#ff0000>" + "Ready" + "</color>";
                }
                break;
            }
        }


        

        //for (int j = 0; j < Left.transform.childCount; j++)
        //{
        //    if (Left.transform.GetChild(j).transform.GetChild(2).GetComponent<Text>().text == "<color=#ff0000>" + "Ready" + "</color>")
        //        count = count + 1;
        //}

        //if (count == Left.transform.childCount && Left.transform.childCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        //{
        //    Gotimer();
        //}

    }


}