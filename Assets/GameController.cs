using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class GameController : MonoBehaviour
{
    public static bool defaultControls = true;

    public GameObject titleScreen;
    public GameObject gameBoardGameobject;
    public bool didTutorial = false;
    public Text errorOne;
    public Text errorTwo;
    public Text textOne;
    public Text textTwo;
    public string username;
    [Space(10)]
    public bool isServer = false;
    public bool isClient = false;
    NetworkClient myClient;
    public bool inLobby = false;
    public bool gameStarted = false;
    [Space(10)]
    public GameObject titleCard;
    public GameObject madeBy;
    public Transform tetHolder;
    [Space(5)]
    public Image[] normalStar;
    public Image[] hardStar;

    public Image[] normalAiStar;
    public Image[] hardAiStar;

    public Image[] normalCleanUpStar;
    public Image[] hardCleanUpStar;
    [Space(5)]
    public GameObject openingSelection;
    public GameObject singleplayerSelection;
    public GameObject tutorialStart;
    public Toggle singleplayerSpeed;
    public GameObject versusAiSelection;
    public Dropdown versusAiBattleMode;
    public GameObject circuitSelection;
    public GameObject cleanUpSelection;
    public GameObject multiplayerSelection;
    public GameObject optionsSelection;
    public GameObject settingsSelection;
    public GameObject controlsSelection;
    public GameObject usernameSelection;
    public InputField usernameField;
    public Dropdown battleType;
    public Dropdown modifications;
    public Dropdown multiplier;
    public Button controlOne;
    public Button controlTwo;
    public GameObject controlDescOne;
    public GameObject controlDescTwo;
    public GameObject extrasSelection;
    public GameObject damageMenu;
    public GameObject scoreMenu;
    public GameObject tSpinMenu;
    public GameObject starsMenu;
    public GameObject highscoresMenu;
    public Text normalScores;
    public Text hardScores;
    public GameObject hostMenu;
    public InputField hostPortNumber;
    public GameObject clientMenu;
    public InputField clientPortNumber;
    public InputField clientIp;
    public Transform tutorialSeries;
    public int tutorialIndex;
    [Space(10)]
    public GameObject T_Tetris;
    public GameObject E_Tetris;
    public GameObject T2_Tetris;
    public GameObject R_Tetris;
    public GameObject I_Tetris;
    public GameObject S_Tetris;
    public int codeState;
    public bool codeEntered = false;
    [Space(10)]
    public GameBoard gameBoard;
    public Lobby lobby;

    private List<Tetrimino> upTet = new List<Tetrimino>();
    private List<Tetrimino> downTet = new List<Tetrimino>();
    private int frameCount;
    private int responseCount = 0;

    private int connectionCount;

    private bool joiningGame = false;
    private bool areYouSure = false;

    public int[] dominanceOrder;
    public string[] dominanceNames;
    public bool[] deadDominance;

    public int PortUser { get; set; }

    class StartMessageType
    {
        public const short ID = 816;
    }
    class LobbyMessage
    {
        public const short ID = 817;
    }
    class DamageMessage
    {
        public const short ID = 818;
    }
    class WinMessage
    {
        public const short ID = 819;
    }
    class GameStateMessage
    {
        public const short ID = 820;
    }
    class EnemyPieceMessage
    {
        public const short ID = 821;
    }
    class PerksMessage
    {
        public const short ID = 822;
    }
    class DeclareDeadMessage
    {
        public const short ID = 823;
    }

    class LobbyInfo : MessageBase
    {
        public string message;
    }
    class GameModeInfo : MessageBase
    {
        public bool hardMode;
        public int id;
        public int connectionCount;
        public string[] names;
        public int[] orders;
        public string name;
        public OnlinePlayMode playMode;
        public Modifications mods;
        public Multiplier multi;
        public bool final;
    }
    class TakeDamageInfo : MessageBase
    {
        public int id;
        public int damageAmount;
    }
    class WinInfo : MessageBase
    {
        public int id;
        public int score;
    }
    class GameState : MessageBase
    {
        public int id;
        public bool garbageRed;
        public int getAttacked;
        public Color[] grid = new Color[GameBoard.width * GameBoard.height];
    }
    class EnemyPiece : MessageBase
    {
        public int id;
        public Color colour;
        public int[] x = new int[4];
        public int[] y = new int[4];
    }
    class PerksGame : MessageBase
    {
        public int id;
        public int[] enemyPerks = new int[4];
    }
    class DeclareDead : MessageBase
    {
        public bool[] deads;
    }

    public IEnumerator PrintError(string message)
    {
        print(message);
        errorOne.enabled = true;
        errorTwo.enabled = true;
        errorOne.text = message;
        errorTwo.text = message;

        yield return new WaitForSeconds(1.5f);

        errorOne.enabled = false;
        errorTwo.enabled = false;
    }

    public IEnumerator PrintMessage(string message)
    {
        print(message);
        textOne.enabled = true;
        textTwo.enabled = true;
        textOne.text = message;
        textTwo.text = message;

        yield return new WaitForSeconds(1.5f);

        textOne.enabled = false;
        textTwo.enabled = false;
    }

    public void PrintSingleMessage(string message)
    {
        textOne.enabled = true;
        textTwo.enabled = true;
        textOne.text = message;
        textTwo.text = message;
    }

    public IEnumerator WaitForClientConnection()
    {
        float seconds = 3.99f;
        while (!myClient.isConnected && seconds > 0)
        {
            PrintSingleMessage("Attempting to connect... " + Mathf.FloorToInt(seconds));
            yield return 0;
            seconds -= Time.deltaTime;
        }

        if (!myClient.isConnected)
        {
            myClient.Disconnect();
            StartCoroutine(PrintError("Failed to connect"));

            textOne.enabled = false;
            textTwo.enabled = false;
        }
        else
        {
            seconds = 3.99f;

            while (!inLobby && seconds > 0)
            {
                PrintSingleMessage("Establishing connection... " + Mathf.FloorToInt(seconds));
                yield return 0;
                seconds -= Time.deltaTime;

                LobbyInfo msg = new LobbyInfo();

                myClient.Send(StartMessageType.ID, msg);
            }

            textOne.enabled = false;
            textTwo.enabled = false;

            if (!inLobby)
            {
                StartCoroutine(PrintError("Failed to establish connection"));
            }
        }
    }

    private void Start()
    {
        if (SaveLoadStats.Load())
        {
            openingSelection.SetActive(false);
            tutorialStart.SetActive(true);
        }
        else
        {
            openingSelection.SetActive(true);
            tutorialStart.SetActive(false);
        }
    }

    public void RedoTutorial()
    {
        tutorialStart.SetActive(true);

        hostMenu.SetActive(false);
        clientMenu.SetActive(false);
        multiplayerSelection.SetActive(false);
        singleplayerSelection.SetActive(false);
        versusAiSelection.SetActive(false);
        openingSelection.SetActive(false);
        usernameSelection.SetActive(false);
        circuitSelection.SetActive(false);
        cleanUpSelection.SetActive(false);
        settingsSelection.SetActive(false);
    }

    public void Freeplay(bool isTutorial)
    {
        titleScreen.SetActive(false);
        gameBoardGameobject.SetActive(true);

        gameBoard.mode = Mode.Offline;
        gameBoard.offlineMode = OfflinePlayMode.Freeplay;
        gameBoard.isTutorial = isTutorial;
        didTutorial = (isTutorial ? true : didTutorial);
        gameBoard.ResetBoard(singleplayerSpeed.isOn);
    }

    public int GetConnectionCount()
    {
        if (isServer)
        {
            return NetworkServer.connections.Count;
        }
        else
        {
            return connectionCount;
        }
    }

    public bool DeadIsFull()
    {
        for (int i = 0; i < deadDominance.Length; i++)
        {
            if (deadDominance[i])
            {
                return false;
            }
        }
        return true;
    }

    public int PlayersLeft()
    {
        int count = 0;
        for (int i = 0; i < deadDominance.Length; i++)
        {
            if (!deadDominance[i])
            {
                count++;
            }
        }
        return count;
    }

    int FindIndexIntDomOrder(int finding)
    {
        for (int i = 0; i < dominanceOrder.Length; i++)
        {
            if (dominanceOrder[i] == finding)
            {
                return i;
            }
        }
        return 0;
    }

    public string GetBehindName(int index) // down
    {
        return dominanceNames[GetBehindId(index)];
    }

    public string GetAheadName(int index) // up
    {
        return dominanceNames[GetAheadId(index)];
    }

    public int GetBehindId(int index) // down
    {
        index = FindIndexIntDomOrder(index);
        index--;
        if (index < 0)
            index = connectionCount - 1;

        while (deadDominance[dominanceOrder[index]])
        {
            index--;
            if (index < 0)
                index = connectionCount - 1;
        }
        return dominanceOrder[index];
    }

    public int GetAheadId(int index) // up
    {
        index = FindIndexIntDomOrder(index);
        index++;
        if (index > dominanceNames.Length - 1)
            index = 0;

        while (deadDominance[dominanceOrder[index]])
        {
            index++;
            if (index > dominanceNames.Length - 1)
                index = 0;
        }
        return dominanceOrder[index];
    }

    public void StartCircuit(Circuit usedCircuit)
    {
        titleScreen.SetActive(false);
        gameBoardGameobject.SetActive(true);

        gameBoard.perks = new Perks[3];
        gameBoard.enemyPerks = new Perks[3];
        gameBoard.fightNumber = 0;
        gameBoard.curCircuit = usedCircuit;
        gameBoard.mode = Mode.Offline;
        gameBoard.isTutorial = false;
        gameBoard.offlineMode = OfflinePlayMode.Circuit;
        gameBoard.ResetBoard(singleplayerSpeed.isOn);
    }

    public void StartCleanUp(CleanUp usedCleanUp)
    {
        titleScreen.SetActive(false);
        gameBoardGameobject.SetActive(true);

        gameBoard.perks = new Perks[3];
        gameBoard.enemyPerks = new Perks[3];
        gameBoard.fightNumber = 0;
        gameBoard.cleanUp = usedCleanUp;
        gameBoard.mode = Mode.Offline;
        gameBoard.isTutorial = false;
        gameBoard.offlineMode = OfflinePlayMode.CleanUp;
        gameBoard.ResetBoard(singleplayerSpeed.isOn);
    }

    public void VersusAI(AI usedAi)
    {
        titleScreen.SetActive(false);
        gameBoardGameobject.SetActive(true);

        gameBoard.ai.brain = usedAi;
        gameBoard.mode = Mode.Offline;
        gameBoard.fightNumber = 0;
        gameBoard.isTutorial = false;
        gameBoard.onlinePlayMode = (OnlinePlayMode)versusAiBattleMode.value;
        gameBoard.offlineMode = OfflinePlayMode.VersusAI;
        gameBoard.ResetBoard(singleplayerSpeed.isOn);
    }

    IEnumerator DC()
    {
        yield return new WaitForSeconds(0.1f);

        if (isClient)
        {
            myClient.Disconnect();
            myClient = null;
        }
        if (isServer)
        {
            NetworkServer.ClearHandlers();
            NetworkServer.Shutdown();

            if (myClient != null)
            {
                myClient.Disconnect();
                myClient = null;
            }
        }

        isClient = false;
        isServer = false;
        inLobby = false;
        gameStarted = false;
    }

    public void MainMenu()
    {
        if (gameBoard.mode == Mode.Client)
        {
            ClientSendWin(-10);
        }
        else if (gameBoard.mode == Mode.Host)
        {
            ServerSendWin(-10);
        }

        gameBoard.mode = Mode.Offline;

        gameBoard.stopped = true;
        titleScreen.SetActive(true);
        gameBoardGameobject.SetActive(false);
        titleCard.SetActive(true);
        madeBy.SetActive(true);

        StartCoroutine(DC());

        lobby.gameObject.SetActive(false);

        hostMenu.SetActive(false);
        clientMenu.SetActive(false);
        tutorialStart.SetActive(false);
        multiplayerSelection.SetActive(false);
        singleplayerSelection.SetActive(false);
        versusAiSelection.SetActive(false);
        openingSelection.SetActive(true);
        usernameSelection.SetActive(false);
        circuitSelection.SetActive(false);
        cleanUpSelection.SetActive(false);
        settingsSelection.SetActive(false);

        if (didTutorial)
        {
            tutorialIndex = 0;
            tutorialSeries.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void BackToLevelSelect()
    {
        gameBoard.stopped = true;
        titleScreen.SetActive(true);
        gameBoardGameobject.SetActive(false);
        titleCard.SetActive(true);
        madeBy.SetActive(true);

        if (isClient)
        {
            myClient.Disconnect();
            myClient = null;
        }
        if (isServer)
        {
            NetworkServer.ClearHandlers();
            NetworkServer.Shutdown();

            if (myClient != null)
            {
                myClient.Disconnect();
                myClient = null;
            }
        }

        isClient = false;
        isServer = false;
        inLobby = false;
        gameStarted = false;

        lobby.gameObject.SetActive(false);

        hostMenu.SetActive(false);
        clientMenu.SetActive(false);
        multiplayerSelection.SetActive(false);
        singleplayerSelection.SetActive(false);
        versusAiSelection.SetActive(false);
        openingSelection.SetActive(false);
        usernameSelection.SetActive(false);
        circuitSelection.SetActive(false);
        cleanUpSelection.SetActive(true);
        settingsSelection.SetActive(false);

        ReloadCleanUpStars();
    }

    public void Singleplayer()
    {
        singleplayerSelection.SetActive(true);
        openingSelection.SetActive(false);
        if (tutorialIndex == 4 && didTutorial)
        {
            AdvanceTutorial();
        }
    }

    public Color GetNormalColour(int val)
    {
        if (val == 4)
        {
            return new Color(0.588f, 0.933f, 1, 1);
        }
        if (val == 3)
        {
            return new Color(1, 1, 0, 1);
        }
        if (val == 2)
        {
            return new Color(0.6f, 0.6f, 0.6f, 1);
        }
        if (val == 1)
        {
            return new Color(0.698f, 0.467f, 0.169f, 1);
        }
        return new Color(0, 0, 0, 0.196f);
    }

    public Color GetHardColour(int val)
    {
        if (val == 4)
        {
            return new Color(1, 0.85f, 0.85f, 1);
        }
        if (val == 3)
        {
            return new Color(1f, 0.65f, 0.35f, 1);
        }
        if (val == 2)
        {
            return new Color(0.6f, 0.25f, 0.25f, 0.8f);
        }
        if (val == 1)
        {
            return new Color(0.3f, 0, 0, 0.5f);
        }
        return new Color(0, 0, 0, 0.196f);
    }

    public void ReloadVersusAI()
    {
        for (int i = 0; i < 6; i++)
        {
            normalAiStar[i].color = GetNormalColour(SaveLoadStats.normalAi[i]);
        }
        for (int i = 0; i < 6; i++)
        {
            hardAiStar[i].color = GetHardColour(SaveLoadStats.hardAi[i]);
        }
    }

    public void VersusAiMenu()
    {
        ReloadVersusAI();
        versusAiSelection.SetActive(true);
        singleplayerSelection.SetActive(false);
    }

    public void ReloadCircuitStars()
    {
        for (int i = 0; i < 4; i++)
        {
            normalStar[i].color = (SaveLoadStats.normalCircuits[i] ? new Color(1, 1, 0, 1) : new Color(0, 0, 0, 0.196f));
        }
        for (int i = 0; i < 4; i++)
        {
            hardStar[i].color = (SaveLoadStats.hardCircuits[i] ? new Color(1, 0, 0, 1) : new Color(0, 0, 0, 0.196f));
        }
    }

    public void CircuitMenu()
    {
        ReloadCircuitStars();
        circuitSelection.SetActive(true);
        singleplayerSelection.SetActive(false);
    }

    public void ReloadCleanUpStars()
    {
        for (int i = 0; i < 15; i++)
        {
            normalCleanUpStar[i].color = (SaveLoadStats.normalCleanUp[i] ? new Color(1, 1, 0, 1) : new Color(0, 0, 0, 0.196f));
        }
        for (int i = 0; i < 15; i++)
        {
            hardCleanUpStar[i].color = (SaveLoadStats.hardCleanUp[i] ? new Color(1, 0, 0, 1) : new Color(0, 0, 0, 0.196f));
        }
    }

    public void CleanUp()
    {
        ReloadCleanUpStars();
        cleanUpSelection.SetActive(true);
        singleplayerSelection.SetActive(false);
    }

    public void OpenUsername()
    {
        usernameSelection.SetActive(true);
        openingSelection.SetActive(false);
    }

    public void Multiplayer()
    {
        if (usernameField.text.Length < 2)
        {
            StartCoroutine(PrintError("Min 2 Characters."));
        }
        else
        {
            username = usernameField.text;
            multiplayerSelection.SetActive(true);
            usernameSelection.SetActive(false);
        }

        if (isClient)
        {
            myClient.Disconnect();
            myClient = null;
        }
        if (isServer)
        {
            NetworkServer.ClearHandlers();
            NetworkServer.Shutdown();

            if (myClient != null)
            {
                myClient.Disconnect();
                myClient = null;
            }
        }
    }

    public void Options()
    {
        optionsSelection.SetActive(true);
        openingSelection.SetActive(false);
    }

    public void EraseAllData()
    {
        if (areYouSure)
        {
            SaveLoadStats.Wipe();
            StartCoroutine(PrintError("ALL DATA ERASED."));
        }
        else
        {
            areYouSure = true;
            StartCoroutine(PrintMessage("Are you sure? Please click again to confirm."));
        }
    }

    public void Settings()
    {
        settingsSelection.SetActive(true);
        optionsSelection.SetActive(false);
        areYouSure = false;
    }

    public void Secret()
    {
        gameBoard.cheats = !gameBoard.cheats;
        if (gameBoard.cheats)
            StartCoroutine(PrintMessage("Cheats ON. You CAN'T get highscores nor stars."));
        else
            StartCoroutine(PrintMessage("Cheats OFF. You CAN get highscores and stars."));
    }

    public void Controls()
    {
        controlsSelection.SetActive(true);
        optionsSelection.SetActive(false);
    }

    private void OnDisable()
    {
        for (int i = 0; i < upTet.Count; i++)
        {
            if (upTet[i] != null)
                Destroy(upTet[i].gameObject);
        }
        for (int i = 0; i < downTet.Count; i++)
        {
            if (downTet[i] != null)
                Destroy(downTet[i].gameObject);
        }
        upTet = new List<Tetrimino>();
        downTet = new List<Tetrimino>();
    }

    void ResetCode()
    {
        codeState = 0;

        T_Tetris.SetActive(true);
        E_Tetris.SetActive(true);
        T2_Tetris.SetActive(true);
        R_Tetris.SetActive(true);
        I_Tetris.SetActive(true);
        S_Tetris.SetActive(true);
    }

    void StopCode()
    {
        T_Tetris.transform.rotation = Quaternion.identity;
        E_Tetris.transform.rotation = Quaternion.identity;
        T2_Tetris.transform.rotation = Quaternion.identity;
        R_Tetris.transform.rotation = Quaternion.identity;
        I_Tetris.transform.rotation = Quaternion.identity;
        S_Tetris.transform.rotation = Quaternion.identity;

        codeEntered = false;
        codeState = 0;
    }

    private void Update()
    {
        if (tutorialIndex != 4 && tutorialIndex <= 10 && Input.GetKeyDown(KeyCode.Space) && didTutorial)
        {
            AdvanceTutorial();
        }

        if (Input.anyKeyDown && !codeEntered && titleScreen.activeSelf)
        {
            if (codeState == 0)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    T_Tetris.SetActive(false);
                    codeState++;
                }
                else
                {
                    ResetCode();
                }
            }
            else if (codeState == 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    E_Tetris.SetActive(false);
                    codeState++;
                }
                else
                {
                    ResetCode();
                }
            }
            else if (codeState == 2)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    T2_Tetris.SetActive(false);
                    codeState++;
                }
                else
                {
                    ResetCode();
                }
            }
            else if (codeState == 3)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    R_Tetris.SetActive(false);
                    codeState++;
                }
                else
                {
                    ResetCode();
                }
            }
            else if (codeState == 4)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    S_Tetris.SetActive(false);
                    codeState++;
                }
                else
                {
                    ResetCode();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    codeEntered = true;
                    ResetCode();
                    Invoke("StopCode", 8f);
                }
                else
                {
                    ResetCode();
                }
            }
        }
        frameCount++;
        if (codeEntered)
        {
            T_Tetris.transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
            E_Tetris.transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime));
            T2_Tetris.transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
            R_Tetris.transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime));
            I_Tetris.transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
            S_Tetris.transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime));
        }
        if (frameCount >= (codeEntered ? 7 : 100))
        {
            frameCount = 0;
            if (Random.value < 0.5f) // up
            {
                upTet.Add(Instantiate(gameBoard.tetriminos[Random.Range(0, gameBoard.tetriminos.Count)], tetHolder));
                if (Random.value < 0.5f)
                    upTet[upTet.Count - 1].transform.localPosition = new Vector3(Random.Range(-22, -7.5f), -6.5f, 0);
                else
                    upTet[upTet.Count - 1].transform.localPosition = new Vector3(Random.Range(15.5f, 31), -6.5f, 0);

                for (int i = 0; i < Random.Range(0,4); i++)
                {
                    upTet[upTet.Count - 1].transform.Rotate(new Vector3(0, 0, 90));
                }

                upTet[upTet.Count - 1].preview = true;
            }
            else // down
            {
                downTet.Add(Instantiate(gameBoard.tetriminos[Random.Range(0, gameBoard.tetriminos.Count)], tetHolder));
                if (Random.value < 0.5f)
                    downTet[downTet.Count - 1].transform.localPosition = new Vector3(Random.Range(-22, -7.5f), 24.5f, 0);
                else
                    downTet[downTet.Count - 1].transform.localPosition = new Vector3(Random.Range(15.5f, 31), 24.5f, 0);

                for (int i = 0; i < Random.Range(0, 4); i++)
                {
                    downTet[downTet.Count - 1].transform.Rotate(new Vector3(0, 0, 90));
                }

                downTet[downTet.Count - 1].preview = true;
            }
        }

        for (int i = 0; i < upTet.Count; i++)
        {
            upTet[i].transform.position += Vector3.up * Time.deltaTime * 4 * (codeEntered ? 2.5f : 1);
            if (codeEntered)
            {
                upTet[i].transform.Rotate(new Vector3(0, 0, 45 * Time.deltaTime));
            }
            if (upTet[i].transform.localPosition.y > 25f)
            {
                Tetrimino tet = upTet[i];
                upTet.RemoveAt(i);
                Destroy(tet.gameObject);
            }
        }
        for (int i = 0; i < downTet.Count; i++)
        {
            downTet[i].transform.position -= Vector3.up * Time.deltaTime * 4 * (codeEntered ? 2.5f : 1);
            if (codeEntered)
            {
                downTet[i].transform.Rotate(new Vector3(0, 0, -45 * Time.deltaTime));
            }
            if (downTet[i].transform.localPosition.y < -7f)
            {
                Tetrimino tet = downTet[i];
                downTet.RemoveAt(i);
                Destroy(tet.gameObject);
            }
        }
    }

    public void ChangeScheme(bool scheme)
    {
        if (scheme)
        {
            controlOne.GetComponent<Image>().color = Color.white;
            controlTwo.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f);

            controlDescOne.gameObject.SetActive(true);
            controlDescTwo.gameObject.SetActive(false);

            defaultControls = true;
        }
        else
        {
            controlOne.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f);
            controlTwo.GetComponent<Image>().color = Color.white;

            controlDescOne.gameObject.SetActive(false);
            controlDescTwo.gameObject.SetActive(true);

            defaultControls = false;
        }
    }

    public void Extras()
    {
        extrasSelection.SetActive(true);
        openingSelection.SetActive(false);
    }

    public void Damage()
    {
        damageMenu.SetActive(true);
        extrasSelection.SetActive(false);
    }

    public void Score()
    {
        scoreMenu.SetActive(true);
        extrasSelection.SetActive(false);
    }

    public void TSpin()
    {
        tSpinMenu.SetActive(true);
        extrasSelection.SetActive(false);
    }

    public void Stars()
    {
        starsMenu.SetActive(true);
        extrasSelection.SetActive(false);
    }

    public void UpdateHighscore()
    {
        string normalText = "Normal Scores:";
        normalText += "\n#1 - " + SaveLoadStats.normalHighscores[0];
        normalText += "\n#2 - " + SaveLoadStats.normalHighscores[1];
        normalText += "\n#3 - " + SaveLoadStats.normalHighscores[2];
        normalText += "\n#4 - " + SaveLoadStats.normalHighscores[3];
        normalText += "\n#5 - " + SaveLoadStats.normalHighscores[4];

        normalScores.text = normalText;

        string hardText = "Hard Scores:";
        hardText += "\n#1 - " + SaveLoadStats.hardHighscores[0];
        hardText += "\n#2 - " + SaveLoadStats.hardHighscores[1];
        hardText += "\n#3 - " + SaveLoadStats.hardHighscores[2];
        hardText += "\n#4 - " + SaveLoadStats.hardHighscores[3];
        hardText += "\n#5 - " + SaveLoadStats.hardHighscores[4];

        hardScores.text = hardText;
    }

    public void Highscores()
    {
        UpdateHighscore();
        highscoresMenu.SetActive(true);
        extrasSelection.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HostMenu()
    {
        hostMenu.SetActive(true);
        multiplayerSelection.SetActive(false);
    }

    public void JoinMenu()
    {
        clientMenu.SetActive(true);
        multiplayerSelection.SetActive(false);
    }

    public void BackToMain()
    {
        singleplayerSelection.SetActive(false);
        tutorialStart.SetActive(false);
        circuitSelection.SetActive(false);
        cleanUpSelection.SetActive(false);
        multiplayerSelection.SetActive(false);
        optionsSelection.SetActive(false);
        extrasSelection.SetActive(false);
        damageMenu.SetActive(false);
        scoreMenu.SetActive(false);
        tSpinMenu.SetActive(false);
        starsMenu.SetActive(false);
        highscoresMenu.SetActive(false);
        openingSelection.SetActive(true);
        hostMenu.SetActive(false);
        clientMenu.SetActive(false);
        controlsSelection.SetActive(false);
        versusAiSelection.SetActive(false);
        usernameSelection.SetActive(false);
        settingsSelection.SetActive(false);
    }

    // Create a server and listen on a port
    public void SetupServer()
    {
        int port;
        if (int.TryParse(hostPortNumber.text, out port) && hostPortNumber.text != "")
        {
            if (NetworkServer.Listen(port))
            {
                PortUser = port;
                NetworkServer.RegisterHandler(StartMessageType.ID, ReturnLobby);
                NetworkServer.RegisterHandler(LobbyMessage.ID, ConfirmGame);
                NetworkServer.RegisterHandler(DamageMessage.ID, TakeDamage);
                NetworkServer.RegisterHandler(WinMessage.ID, Win);
                NetworkServer.RegisterHandler(GameStateMessage.ID, UpdateGameState);
                NetworkServer.RegisterHandler(EnemyPieceMessage.ID, UpdateEnemyCurrent);
                NetworkServer.RegisterHandler(PerksMessage.ID, StartPerkGame);
                NetworkServer.DisconnectAll();
                isServer = true;
                isClient = false;
                inLobby = true;

                lobby.gameObject.SetActive(true);

                titleCard.SetActive(false);
                madeBy.SetActive(false);

                openingSelection.SetActive(false);
                multiplayerSelection.SetActive(false);
                clientMenu.SetActive(false);
                hostMenu.SetActive(false);

                lobby.StartLobby(Mode.Host, false);


                //SetupLocalClient();
            }
            else
            {
                StartCoroutine(PrintError("ERROR: Port already in use"));
            }
        }
        else
        {
            StartCoroutine(PrintError("ERROR: Port Invalid"));
        }
    }

    // Create a client and connect to the server port
    public void SetupClient()
    {
        int port;
        if (int.TryParse(clientPortNumber.text, out port) && clientPortNumber.text != "")
        {
            string ip = clientIp.text;
            myClient = new NetworkClient();
            //myClient.RegisterHandler(MsgType.Error, RecieveError);
            myClient.RegisterHandler(MsgType.Connect, OnConnected);
            myClient.RegisterHandler(StartMessageType.ID, JoinLobby);
            myClient.RegisterHandler(LobbyMessage.ID, StartGame);
            myClient.RegisterHandler(DamageMessage.ID, TakeDamage);
            myClient.RegisterHandler(WinMessage.ID, Win);
            myClient.RegisterHandler(GameStateMessage.ID, UpdateGameState);
            myClient.RegisterHandler(EnemyPieceMessage.ID, UpdateEnemyCurrent);
            myClient.RegisterHandler(PerksMessage.ID, StartPerkGame);
            myClient.RegisterHandler(DeclareDeadMessage.ID, MarkDead);
            //myClient.RegisterHandler(StartMessageType.ID, StartGame);
            //myClient.RegisterHandler(RebuildMessageType.ID, ReplyRebuild);
            //myClient.RegisterHandler(TileMessageType.ID, ServerClaimedTile);
            myClient.Connect(ip, port);
            isClient = true;
            isServer = false;
            inLobby = false;
            StartCoroutine(WaitForClientConnection());
        }
        else
        {
            StartCoroutine(PrintError("ERROR: Port Invalid"));
        }
    }

    // Create a local client and connect to the local server
    /*
    void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
    }
    */

    // What the CLIENT should do when connected
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }

    // What the CLIENT should do when the server sends them the lobby data
    public void JoinLobby(NetworkMessage netMsg)
    {
        if (!inLobby && !openingSelection.activeSelf)
        {
            joiningGame = false;

            LobbyInfo myMsg = netMsg.ReadMessage<LobbyInfo>();
            inLobby = true;

            lobby.gameObject.SetActive(true);

            openingSelection.SetActive(false);
            multiplayerSelection.SetActive(false);
            clientMenu.SetActive(false);
            hostMenu.SetActive(false);

            titleCard.SetActive(false);
            madeBy.SetActive(false);

            lobby.StartLobby(Mode.Client, false);

            StopCoroutine(WaitForClientConnection());
            textOne.enabled = false;
            textTwo.enabled = false;

            Debug.Log(myMsg.message);
        }
    }

    // What the SERVER should do when a client joins send them the lobby data
    public void ReturnLobby(NetworkMessage netMsg)
    {
        LobbyInfo msg = new LobbyInfo();
        msg.message = "Lobby dude";
        print("Sent lobby");

        lobby.UpdateLobby(NetworkServer.connections.Count - 1);

        // use the values from the message here

        NetworkServer.SendToAll(StartMessageType.ID, msg);
    }

    // What the SERVER should do when the game is set to start
    public IEnumerator SendGame(bool hardMode)
    {
        float seconds = 2.99f;
        responseCount = 0;

        dominanceOrder = new int[NetworkServer.connections.Count];

        for (int i = 0; i < dominanceOrder.Length; i++)
        {
            dominanceOrder[i] = i;
        }

        for (int i = 1; i < dominanceOrder.Length; i++)
        {
            int holder = dominanceOrder[i];
            int index = Random.Range(1, dominanceOrder.Length);
            dominanceOrder[i] = dominanceOrder[index];
            dominanceOrder[index] = holder;
        }

        dominanceNames = new string[NetworkServer.connections.Count];
        deadDominance = new bool[NetworkServer.connections.Count];
        dominanceNames[0] = username;
        connectionCount = NetworkServer.connections.Count;

        for (int i = 0; i < dominanceOrder.Length; i++) //COMMENT OUT OMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATEROMMENT OUT LATER
        {
            print(dominanceOrder[i]);
        }

        while (seconds > 0)
        {
            PrintSingleMessage("Sending details... " + Mathf.FloorToInt(seconds) + " Responses: " + responseCount + " / " + (NetworkServer.connections.Count - 1));
            yield return 0;

            if (NetworkServer.connections.Count > 2)
            {
                for (int i = 1; i < NetworkServer.connections.Count; i++)
                {
                    GameModeInfo msg = new GameModeInfo();
                    msg.id = i;
                    msg.connectionCount = NetworkServer.connections.Count - 1;
                    msg.hardMode = hardMode;
                    msg.playMode = OnlinePlayMode.Dominance;
                    msg.mods = (Modifications)modifications.value;
                    gameBoard.mods = (Modifications)modifications.value;
                    msg.multi = (Multiplier)multiplier.value;
                    gameBoard.multi = (Multiplier)multiplier.value;
                    msg.final = false;

                    NetworkServer.connections[i].Send(LobbyMessage.ID, msg);
                }
            }
            else
            {
                GameModeInfo msg = new GameModeInfo();
                msg.name = username;
                msg.id = 0;
                msg.hardMode = hardMode;
                msg.playMode = (OnlinePlayMode)battleType.value;
                msg.mods = (Modifications)modifications.value;
                gameBoard.mods = (Modifications)modifications.value;
                msg.multi = (Multiplier)multiplier.value;
                gameBoard.multi = (Multiplier)multiplier.value;
                msg.final = false;

                NetworkServer.SendToAll(LobbyMessage.ID, msg);
            }

            seconds -= Time.deltaTime;
        }

        if (!gameStarted)
        {
            if (responseCount == NetworkServer.connections.Count - 1)
            {
                StartCoroutine(PrintError("Failed to send because some unknown reason."));
                textOne.enabled = false;
                textTwo.enabled = false;
            }
            else
            {
                StartCoroutine(PrintError("Not all clients active. Please restart server and have clients reconnect."));
                textOne.enabled = false;
                textTwo.enabled = false;
            }
        }
    }

    static int[] PtoI(Perks[] newList)
    {
        int[] returnable = new int[3];
        returnable[0] = (int)newList[0];
        returnable[1] = (int)newList[1];
        returnable[2] = (int)newList[2];
        return returnable;
    }

    static Perks[] ItoP(int[] newList)
    {
        Perks[] returnable = new Perks[3];
        returnable[0] = (Perks)newList[0];
        returnable[1] = (Perks)newList[1];
        returnable[2] = (Perks)newList[2];
        return returnable;
    }

    // What the SERVER should do when the client confirms the game running
    public void ConfirmGame(NetworkMessage netMsg)
    {
        GameModeInfo myMsg = netMsg.ReadMessage<GameModeInfo>();
        dominanceNames[myMsg.id] = myMsg.name;

        responseCount++;
        if (responseCount == NetworkServer.connections.Count - 1)
        {
            gameStarted = true;
            inLobby = false;

            textOne.enabled = false;
            textTwo.enabled = false;

            titleScreen.SetActive(false);
            lobby.gameObject.SetActive(false);
            gameBoardGameobject.SetActive(true);

            gameBoard.mode = Mode.Host;
            gameBoard.onlinePlayMode = myMsg.playMode;
            gameBoard.dominanceId = 0;
            gameBoard.SetName(gameBoard.onlinePlayMode == OnlinePlayMode.Dominance ? GetAheadName(gameBoard.dominanceId) : myMsg.name);
            gameBoard.ResetBoard(myMsg.hardMode);

            if (NetworkServer.connections.Count > 2)
            {
                for (int i = 1; i < NetworkServer.connections.Count; i++)
                {
                    GameModeInfo msg = new GameModeInfo();
                    msg.id = i;
                    msg.connectionCount = NetworkServer.connections.Count - 1;
                    msg.hardMode = myMsg.hardMode;
                    msg.playMode = OnlinePlayMode.Dominance;
                    msg.mods = myMsg.mods;
                    msg.multi = myMsg.multi;
                    msg.names = dominanceNames;
                    msg.orders = dominanceOrder;
                    msg.final = true;

                    NetworkServer.connections[i].Send(LobbyMessage.ID, msg);
                }
            }
            else
            {
                GameModeInfo msg = new GameModeInfo();
                myMsg.name = username;
                myMsg.id = 0;
                myMsg.final = true;

                NetworkServer.SendToAll(LobbyMessage.ID, myMsg);
            }
            NetworkServer.SendToAll(LobbyMessage.ID, myMsg);
        }
    }

    // What the CLIENT should do when a server sends them the game info
    public void StartGame(NetworkMessage netMsg)
    {
        GameModeInfo myMsg = netMsg.ReadMessage<GameModeInfo>();

        if (myMsg.final)
        {
            titleScreen.SetActive(false);
            lobby.gameObject.SetActive(false);
            gameBoardGameobject.SetActive(true);

            gameBoard.mode = Mode.Client;
            gameBoard.onlinePlayMode = myMsg.playMode;
            gameBoard.mods = myMsg.mods;
            gameBoard.multi = myMsg.multi;
            gameBoard.dominanceId = myMsg.id;

            connectionCount = myMsg.connectionCount;
            dominanceNames = myMsg.names;
            deadDominance = new bool[myMsg.connectionCount + 1];
            dominanceOrder = myMsg.orders;
            print(dominanceNames.Length);

            gameBoard.SetName(gameBoard.onlinePlayMode == OnlinePlayMode.Dominance ? GetAheadName(gameBoard.dominanceId) : myMsg.name);
            gameBoard.ResetBoard(myMsg.hardMode);

            gameStarted = true;
            joiningGame = false;
            inLobby = false;
        }
        else if (!joiningGame)
        {
            joiningGame = true;
            Invoke("KillJoin", 4f);

            myMsg.name = username;

            myClient.Send(LobbyMessage.ID, myMsg);
        }
    }

    public void KillJoin()
    {
        if (!gameStarted)
        {
            joiningGame = false;
        }
    }

    // What the SERVER and CLIENT should do when the client confirms the game running
    public void StartPerkGame(NetworkMessage netMsg)
    {
        PerksGame myMsg = netMsg.ReadMessage<PerksGame>();
        print("Got");

        if (isServer && gameBoard.onlinePlayMode == OnlinePlayMode.Dominance && GetAheadId(myMsg.id) != gameBoard.dominanceId)
        {
            gameBoard.enemyPerks = ItoP(myMsg.enemyPerks);

            NetworkServer.connections[GetAheadId(myMsg.id)].Send(PerksMessage.ID, myMsg);
        }
        else
        {
            gameBoard.enemyPerks = ItoP(myMsg.enemyPerks);

            if (gameBoard.onlinePlayMode != OnlinePlayMode.Dominance)
                gameBoard.readyForMultiPerk = true;
        }
    }

    public void ClientSendPerks()
    {
        PerksGame msg = new PerksGame();
        msg.enemyPerks = PtoI(gameBoard.perks);

        myClient.Send(PerksMessage.ID, msg);
    }

    public void ServerSendPerks()
    {
        PerksGame msg = new PerksGame();
        msg.enemyPerks = PtoI(gameBoard.perks);

        NetworkServer.connections[GetAheadId(gameBoard.dominanceId)].Send(PerksMessage.ID, msg);
    }

    public void ServerStartPerkGame()
    {
        PerksGame msg = new PerksGame();
        msg.enemyPerks = PtoI(gameBoard.perks);

        print("Sent " + NetworkServer.connections.Count);
        NetworkServer.SendToAll(PerksMessage.ID, msg);
    }

    public void ClientSendDamage(int damageAmount)
    {
        TakeDamageInfo msg = new TakeDamageInfo();
        msg.id = gameBoard.dominanceId;
        msg.damageAmount = damageAmount;

        myClient.Send(DamageMessage.ID, msg);
    }

    public void ServerSendDamage(int damageAmount)
    {
        if (gameBoard.onlinePlayMode == OnlinePlayMode.Dominance)
        {
            TakeDamageInfo msg = new TakeDamageInfo();
            msg.id = gameBoard.dominanceId;
            msg.damageAmount = damageAmount;

            NetworkServer.connections[GetAheadId(gameBoard.dominanceId)].Send(DamageMessage.ID, msg);
        }
        else
        {
            TakeDamageInfo msg = new TakeDamageInfo();
            msg.id = gameBoard.dominanceId;
            msg.damageAmount = damageAmount;

            NetworkServer.SendToAll(DamageMessage.ID, msg);
        }
    }

    public void TakeDamage(NetworkMessage netMsg)
    {
        TakeDamageInfo myMsg = netMsg.ReadMessage<TakeDamageInfo>();
        if (isServer && gameBoard.onlinePlayMode == OnlinePlayMode.Dominance && GetAheadId(myMsg.id) != gameBoard.dominanceId)
        {
            NetworkServer.connections[GetAheadId(myMsg.id)].Send(DamageMessage.ID, myMsg);
        }
        else
        {
            gameBoard.TakeDamage(myMsg.damageAmount);
        }
    }

    public void ClientSendWin(int score)
    {
        WinInfo msg = new WinInfo();
        msg.id = gameBoard.dominanceId;
        msg.score = score;

        myClient.Send(WinMessage.ID, msg);
        if (gameBoard.onlinePlayMode != OnlinePlayMode.Dominance && score != -10)
        {
            Invoke("SwitchToLobby", 4.5f);
        }
    }

    void MarkDead(NetworkMessage netMsg)
    {
        if (gameBoard.onlinePlayMode == OnlinePlayMode.Dominance)
        {
            DeclareDead myMsg = netMsg.ReadMessage<DeclareDead>();
            deadDominance = myMsg.deads;

            if (PlayersLeft() == 1)
            {
                gameBoard.DominanceOver();
                Invoke("SwitchToLobby", 4.5f);
            }
            else
            {
                gameBoard.SetBehind();
            }
        }
    }

    public void ServerSendWin(int score)
    {
        if (gameBoard.onlinePlayMode == OnlinePlayMode.Dominance)
        {
            WinInfo msg = new WinInfo();
            msg.id = gameBoard.dominanceId;
            msg.score = score;

            deadDominance[0] = true;

            DeclareDead newMsg = new DeclareDead();
            newMsg.deads = deadDominance;

            gameBoard.SetBehind();

            if (PlayersLeft() == 1)
            {
                gameBoard.DominanceOver();
                Invoke("SwitchToLobby", 4.5f);
            }

            NetworkServer.SendToAll(DeclareDeadMessage.ID, newMsg);

            NetworkServer.connections[GetBehindId(gameBoard.dominanceId)].Send(WinMessage.ID, msg);
        }
        else
        {
            WinInfo msg = new WinInfo();
            msg.id = gameBoard.dominanceId;
            msg.score = score;

            NetworkServer.SendToAll(WinMessage.ID, msg);
            Invoke("SwitchToLobby", 4.5f);
        }
    }

    public void Win(NetworkMessage netMsg) 
    {
        WinInfo myMsg = netMsg.ReadMessage<WinInfo>();

        if (isServer && gameBoard.onlinePlayMode == OnlinePlayMode.Dominance && GetBehindId(myMsg.id) != gameBoard.dominanceId) 
        {
            deadDominance[myMsg.id] = true;

            DeclareDead msg = new DeclareDead();
            msg.deads = deadDominance;

            gameBoard.SetBehind();

            if (PlayersLeft() == 1)
            {
                gameBoard.DominanceOver();
                Invoke("SwitchToLobby", 4.5f);
            }

            NetworkServer.SendToAll(DeclareDeadMessage.ID, msg);
            NetworkServer.connections[GetBehindId(myMsg.id)].Send(WinMessage.ID, myMsg);
        } 
        else
        {
            if (gameBoard.onlinePlayMode == OnlinePlayMode.Dominance)
            {
                deadDominance[myMsg.id] = true;

                DeclareDead msg = new DeclareDead();
                msg.deads = deadDominance;

                if (PlayersLeft() == 1)
                {
                    gameBoard.DominanceOver();
                    Invoke("SwitchToLobby", 4.5f);
                }

                NetworkServer.SendToAll(DeclareDeadMessage.ID, msg);

                gameBoard.GetKill();
            }
            else
            {
                gameBoard.Win();
                Invoke("SwitchToLobby", 4.5f);
            }
        } 
    }

    public void ClientSendGameState(bool garbageRed, int getAttacked, Color[] grid)
    {
        GameState msg = new GameState();
        msg.id = gameBoard.dominanceId;
        msg.garbageRed = garbageRed;
        msg.getAttacked = getAttacked;
        msg.grid = grid;

        myClient.Send(GameStateMessage.ID, msg);
    }

    public void ServerSendGameState(bool garbageRed, int getAttacked, Color[] grid)
    {
        if (gameBoard.onlinePlayMode == OnlinePlayMode.Dominance)
        {
            GameState msg = new GameState();
            msg.id = gameBoard.dominanceId;
            msg.garbageRed = garbageRed;
            msg.getAttacked = getAttacked;
            msg.grid = grid;

            NetworkServer.connections[GetBehindId(gameBoard.dominanceId)].Send(GameStateMessage.ID, msg);
        }
        else
        {
            GameState msg = new GameState();
            msg.id = gameBoard.dominanceId;
            msg.garbageRed = garbageRed;
            msg.getAttacked = getAttacked;
            msg.grid = grid;

            NetworkServer.SendToAll(GameStateMessage.ID, msg);
        }
    }

    public void UpdateGameState(NetworkMessage netMsg)
    {
        GameState myMsg = netMsg.ReadMessage<GameState>();

        if (isServer && gameBoard.onlinePlayMode == OnlinePlayMode.Dominance && GetBehindId(myMsg.id) != gameBoard.dominanceId)
        {
            NetworkServer.connections[GetBehindId(myMsg.id)].Send(GameStateMessage.ID, myMsg);
        }
        else
        {
            gameBoard.UpdateGameState(myMsg.garbageRed, myMsg.getAttacked, myMsg.grid);
        }
    }

    public void ClientSendCur(Tetrimino current)
    {
        EnemyPiece msg = new EnemyPiece();
        if (current != null)
        {
            msg.colour = current.GetColour();
            msg.id = gameBoard.dominanceId;
            msg.x = current.GetX();
            msg.y = current.GetY();
        }
        else
        {
            msg.colour = Color.black;
        }

        myClient.Send(EnemyPieceMessage.ID, msg);
    }

    public void ServerSendCur(Tetrimino current)
    {
        if (gameBoard.onlinePlayMode == OnlinePlayMode.Dominance)
        {
            EnemyPiece msg = new EnemyPiece();
            if (current != null)
            {
                msg.colour = current.GetColour();
                msg.id = gameBoard.dominanceId;
                msg.x = current.GetX();
                msg.y = current.GetY();
            }
            else
            {
                msg.colour = Color.black;
            }

            try
            {
                NetworkServer.connections[GetBehindId(gameBoard.dominanceId)].Send(EnemyPieceMessage.ID, msg);
            }
            catch
            {
                print(GetBehindId(gameBoard.dominanceId) + " " + gameBoard.dominanceId + " " + NetworkServer.connections.Count);
            }
        }
        else
        {
            EnemyPiece msg = new EnemyPiece();
            if (current != null)
            {
                msg.colour = current.GetColour();
                msg.id = gameBoard.dominanceId;
                msg.x = current.GetX();
                msg.y = current.GetY();
            }
            else
            {
                msg.colour = Color.black;
            }

            NetworkServer.SendToAll(EnemyPieceMessage.ID, msg);
        }
    }

    public void UpdateEnemyCurrent(NetworkMessage netMsg)
    {
        EnemyPiece myMsg = netMsg.ReadMessage<EnemyPiece>();

        if (isServer && gameBoard.onlinePlayMode == OnlinePlayMode.Dominance && GetBehindId(myMsg.id) != gameBoard.dominanceId)
        {
            NetworkServer.connections[GetBehindId(myMsg.id)].Send(EnemyPieceMessage.ID, myMsg);
        }
        else
        {
            gameBoard.UpdateEnemyCurrent(myMsg.colour, myMsg.x, myMsg.y);
        }
    }

    public void AdvanceTutorial()
    {
        tutorialIndex++;

        if (tutorialIndex <= 10)
        {
            tutorialSeries.GetChild(tutorialIndex).gameObject.SetActive(true);
        }
        else
        {
            didTutorial = false;
        }
        tutorialSeries.GetChild(tutorialIndex - 1).gameObject.SetActive(false);
    }

    void SwitchToLobby()
    {
        joiningGame = false;

        gameBoard.stopped = true;
        titleScreen.SetActive(true);
        gameBoardGameobject.SetActive(false);

        lobby.gameObject.SetActive(true);

        singleplayerSelection.SetActive(false);
        openingSelection.SetActive(false);
        circuitSelection.SetActive(false);
        cleanUpSelection.SetActive(false);
        multiplayerSelection.SetActive(false);
        optionsSelection.SetActive(false);
        damageMenu.SetActive(false);
        scoreMenu.SetActive(false);
        tSpinMenu.SetActive(false);
        starsMenu.SetActive(false);
        highscoresMenu.SetActive(false);
        clientMenu.SetActive(false);
        hostMenu.SetActive(false);
        extrasSelection.SetActive(false);
        controlsSelection.SetActive(false);
        versusAiSelection.SetActive(false);
        usernameSelection.SetActive(false);
        settingsSelection.SetActive(false);

        titleCard.SetActive(false);
        madeBy.SetActive(false);

        gameStarted = false;
        inLobby = true;

        if (isClient)
        {
            lobby.StartLobby(Mode.Client, false);
        }
        else
        {
            lobby.StartLobby(Mode.Host, true);
            lobby.UpdateLobby(NetworkServer.connections.Count - 1);
        }
    }
}
