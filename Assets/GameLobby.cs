using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{

    public GameObject errorPanel;

    public string[] randomNicknames;
    string nickname = null;

    private GameSettings gameSettings = new GameSettings();

    private GameSettings[] allGameRooms;

    private NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

    }

    //Change when we have a game Setup Screen
    void QuickHost()
    {
        StartCoroutine(IQuickHost());
    }

    //Change when we have a game Setup Screen
    IEnumerator IQuickHost()
    {
        yield return 0;
    }

    //Attempt to join any game.
    public void QuickJoin()
    {
        StartCoroutine(IQuickJoin());
    }

    //Joins the first game that it finds.
    private IEnumerator IQuickJoin()
    {


        yield return IGetGamesList();

        if (allGameRooms.Length != 0)
        {
            yield return IJoinGame(allGameRooms[0].roomID);
        }
        else
        {
            NoGamesFound();
        }

    }

    //Needs Displaying info. Functionality not yet added.
    void BrowseServers()
    {
        StartCoroutine(IGetGamesList());
        //Then display the info
    }

    //Create a new game room
    public void CreateGame()
    {
        StartCoroutine(IRequestNewGameRoom());
    }

    //Ask the server to create a new room.
    private IEnumerator IRequestNewGameRoom ()
    {

        string gameSettingsParsed = gameSettings.Parse();

        WWWForm form = new WWWForm();

        //form.AddField("GameSettings", gameSettingsParsed);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/battlemage/CreateNewRoom.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error" + www.error);

        }
        else if (www.downloadHandler.text.Contains("Game Created")){
            //Parse: Expected Format "Game Created*GameID"
            string[] returnParse = www.downloadHandler.text.Split('*');
            StartCoroutine(IJoinGame(returnParse[1]));
            GameCreated( returnParse[1]);
            
        }
    }

    //Get List of games from the server
    private IEnumerator IGetGamesList()
    {

        UnityWebRequest www = UnityWebRequest.Get("http://localhost/battlemage/GetGamesList.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error" + www.error);
        }
        else if (www.downloadHandler.text.Contains("List Of Games:"))
        {
            Debug.Log(www.downloadHandler.text);
            string[] returnParse = www.downloadHandler.text.Split('*');

            GameSettings[] newGameRooms = new GameSettings[returnParse.Length-1];
            for (int i = 0; i < newGameRooms.Length; i++)
            {
                newGameRooms[i] = new GameSettings();

                newGameRooms[i].roomID = returnParse[i + 1];
            }
            allGameRooms = newGameRooms;
        }
        else
        {
            Debug.LogError(www.downloadHandler.text);
        }
    }

    //Once we have our gameID that we want to join, Join that game.
    private IEnumerator IJoinGame(string newGameRoom)
    {
        Debug.Log("join");
        if (!VerifyNickname(nickname))
            nickname = RandomNickname();

        WWWForm form = new WWWForm();
        form.AddField("Username", networkManager.username);
        form.AddField("Nickname", nickname);
        form.AddField("gameID", newGameRoom);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/battlemage/JoinGame.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);

        }
        else if (www.downloadHandler.text.Contains("Joined Game"))
        {
            GameJoined();
        }
        else
        {
            Debug.LogError(www.error);
        }
    }

    //Server says game was Created. Do Client stuff. This function might be better removed
    void GameCreated( string newGameRoomName )
    {
        Debug.Log("Game Created.  Room name is " + newGameRoomName + ".");
    }

    //Game joined. Time to change scenes and load the lobby
    void GameJoined()
    {
        Debug.Log("GameJoined");
    }

    //Bring up the error message
    void NoGamesFound()
    {
        //Optimize later
        errorPanel.SetActive(true);
        errorPanel.GetComponentInChildren<Text>().text = "No Games Found";
    }

    //Verify that the username given meets minimum requirements
    bool VerifyNickname(string nameToCheck)
    {
        if (nameToCheck == null)
            return false;
        if (nameToCheck.Length < 3)
        {
            return false;
        }


        List<char> nickNameLetters = nameToCheck.ToList<char>();
        for (int i = 0; i < nameToCheck.Length; i++)
        {

            if (!char.IsLetterOrDigit(nickNameLetters[i]) || !nickNameLetters[i].Equals('_'))
            {
                return false;
            }
        }
        return true;
    }

    //Generate Random Nickname if the user hasn't made one.
    string RandomNickname()
    {

        string newNickname = randomNicknames[Random.Range(0, randomNicknames.Length - 1)];

        return newNickname;
    }

    public void DebugGetGameList()
    {
        StartCoroutine(IGetGamesList());
    }


}
