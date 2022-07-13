using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Manager : MonoBehaviourPunCallbacks
{

    RoomOptions roomOptions;
    private byte oyuncuSayisi = 2;
    
   
    
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        roomOptions = new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true };
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Sunucuya girildi.");
        PhotonNetwork.JoinLobby();


    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye girildi.");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya giriş yapıldı.");
        GameObject player = PhotonNetwork.Instantiate("2x_0",Vector3.zero,Quaternion.identity,0,null);
        
    }




}
