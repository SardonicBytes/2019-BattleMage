using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginAndRegistration : MonoBehaviour
{

    public int userModifier = 0;
    public float serverInfoRefreshRate = 0.5f;

    //Server Status GUI
    public Text txt_serverConnected;
    public Text txt_numJoinableGames;
    public Text txt_numTotalGames;
    public Text txt_numOnlinePlayers;

    public Button btn_JoinGame;
    public Button btn_QuickJoin;
    public Button btn_HostGame;

    public InputField nicknameField;

    private NetworkManager networkManager;

    bool serverConnected = false;
    public bool playOffline = false;
    string username;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        CreateUsernameAndPassword();

        if (!playOffline)
            StartCoroutine(AttemptLogin());

        StartCoroutine("UpdateServerInfo");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayOffline();
        }
    }


    IEnumerator AttemptLogin()
    {

        WWWForm form = new WWWForm();

        form.AddField("LoginUser", networkManager.username);
        form.AddField("LoginPass", networkManager.password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/battlemage/AttemptLogin.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error" + www.error);

        }
        else if( www.downloadHandler.text == "Login Success")
        {
            LoginSuccess();
        }
        else if (www.downloadHandler.text == "Player Not Registered.")
        {
            StartCoroutine(RegisterNewUser());
        }

    }

    //Register a new user with the server
    IEnumerator RegisterNewUser()
    {


        WWWForm form = new WWWForm();
        form.AddField("LoginUser", networkManager.username);
        form.AddField("LoginPass", networkManager.password);


        UnityWebRequest www = UnityWebRequest.Post("http://localhost/battlemage/RegisterUser.php", form);
        yield return www.SendWebRequest();

        //Debug.Log("Received");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);

        }
        else if (www.downloadHandler.text == "New user created")
        {
            serverConnected = true;
            UserRegistered();
        } 
        else
        {
            Debug.LogError("Exception: " + www.downloadHandler.text);
        }

    }

    //We've successfully created our new account
    void UserRegistered()
    {
        //Login to that new account
        StartCoroutine(AttemptLogin());
    }

    //When we want to cancel our attempts to connect to the server
    public void PlayOffline()
    {
        playOffline = true;
        StopCoroutine("UpdateServerInfo");
        UpdateServerInfo();
    }

    //When we have successfully logged into the server
    void LoginSuccess()
    {

        UpdateServerInfo();

        btn_HostGame.interactable = true;
        btn_QuickJoin.interactable = true;

    }
    
    //Check our connection with the server, get a list of how many players are online, how many games are active
    //The PHP script for this has not yet been made
    IEnumerator UpdateServerInfo()
    {
        WaitForSeconds refreshWait = new WaitForSeconds(serverInfoRefreshRate);
        while (true)
        {

            //Get the Info
            UnityWebRequest www = UnityWebRequest.Get("http://localhost/battlemage/UpdateServerInfo.php");
            yield return www.SendWebRequest();

            //Parse the return
            string[] serverInfo = www.downloadHandler.text.Split('*');
            serverConnected = !www.isNetworkError ? true : false;

            //OPT- rename variables to help with clarity
            string playersConnected = serverInfo[0];
            string gameRooms = serverInfo[1];


            ////////////////
            // Handle GUI //
            ////////////////
            if (playOffline)
            {
                txt_serverConnected.text = "OFFLINE";
                txt_serverConnected.color = Color.red;
            }
            else
            {
                if (serverConnected)
                {
                    txt_serverConnected.text = "CONNECTED";
                    txt_serverConnected.color = Color.green;
                }
                else
                {
                    txt_serverConnected.text = "CONNECTING";
                    txt_serverConnected.color = Color.yellow;
                }
            }

            // # Players Online
            txt_numOnlinePlayers.text = playersConnected;
            if (playersConnected != "0")
            {
                txt_numOnlinePlayers.color = Color.white;
            }
            else
            {
                txt_numOnlinePlayers.color = Color.grey;
            }



            // # Game rooms
            txt_numTotalGames.text = gameRooms;
            if (gameRooms != "0")
            {
                txt_numTotalGames.color = Color.white;
            }
            else
            {
                txt_numTotalGames.color = Color.grey;
            }

            yield return refreshWait;
        }
    }


    void CreateUsernameAndPassword()
    {
        //Because this is not intended to be scalable, with only a handful of users at a maximum, 
        //The user creation system is meant to be as painless as possible for the user/tester
        //For security purposes, we could hash these values, but since they provide some level 
        //of debugging value, and the security risk is minimal, it is left as plain text.  
        //A proper login system or integration with something like steam would be ideal for release.

        if (userModifier == 0)
            userModifier = Random.Range(1, 100);

        networkManager.username = SystemInfo.deviceName + userModifier;
        networkManager.password = SystemInfo.deviceName;

    }


}
