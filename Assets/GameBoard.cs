using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Mode
{
    Offline,
    Host,
    Client
}

public enum OfflinePlayMode
{
    Freeplay,
    VersusAI,
    Circuit,
    CleanUp
}

public enum OnlinePlayMode
{
    Regular,
    OnePerk,
    TwoPerks,
    ThreePerks,
    Dominance
}

public enum Modifications
{
    Regular,
    LinesOnly,
    CombosOnly,
    TSpinsOnly,
    NoLines,
    NoCombos,
    NoTSpins
}

public enum Multiplier
{
    OneX,
    TwoX,
    ThreeX,
    PointFiveRoundUp,
    PointFiveRoundDown,
    OnePointFiveRoundUp,
    OnePointFiveRoundDown
}


public enum Perks
{
    None,

    ComboStartEarlier, //
    ComboAbove5PlusTwo, //
    EnemyStartThreeDam, //
    YouStartFourDam, //
    OneDamEvy600Points, //

    TwoDamEvy1000Points, //
    ThreeDamEvy1400Points, //
    OneDamEvy8Lines, //
    TwoDamEvy13Lines, //
    ThreeDamEvy17Lines, //

    BlocksFallSlower, //
    EnemyFallsFaster, //
    EnemyMovesSlower, //
    DamageIsNicer, //
    EnemyDamageIsMessy, //

    LineHoldEvy400Points, //
    THoldEvy6Lines, //
    IgnoreFirst5LinesDamage, //
    SecondChance, //

    IMoreLikely, //
    TMoreLikely, //
    EqualOneIsZero, //
    AboveFiveMinusOne, //
    EqualOneIsTwo, //

    AboveFivePlusOne, //
    Meme, //
    MiniTimeBomb,
    MegaTimeBomb,
    MiniTimeShield,

    MegaTimeShield
}

public enum Piece
{
    Null,
    I,
    O,
    T,
    S,
    Z,
    J,
    L
}

public class GameBoard : MonoBehaviour
{
    public static string PerkName(Perks newPerk)
    {
        switch (newPerk)
        {
            case Perks.ComboStartEarlier:
                return "Fast Combo";
            case Perks.ComboAbove5PlusTwo:
                return "Large Combo";
            case Perks.EnemyStartThreeDam:
                return "Sabotage";
            case Perks.YouStartFourDam:
                return "Self Harm";
            case Perks.OneDamEvy600Points:
                return "Small Score Damage";
            case Perks.TwoDamEvy1000Points:
                return "Medium Score Damage";
            case Perks.ThreeDamEvy1400Points:
                return "Large Score Damage";
            case Perks.OneDamEvy8Lines:
                return "Small Line Damage";
            case Perks.TwoDamEvy13Lines:
                return "Medium Line Damage";
            case Perks.ThreeDamEvy17Lines:
                return "Large Line Damage";
            case Perks.BlocksFallSlower:
                return "Slow Fall";
            case Perks.EnemyFallsFaster:
                return "Enemy Speed Fall";
            case Perks.EnemyMovesSlower:
                return "Enemy Slowness";
            case Perks.DamageIsNicer:
                return "Nice Damage";
            case Perks.EnemyDamageIsMessy:
                return "Messy Enemy Damage";
            case Perks.LineHoldEvy400Points:
                return "I Score Hold";
            case Perks.THoldEvy6Lines:
                return "T Line Hold";
            case Perks.IgnoreFirst5LinesDamage:
                return "Resilience";
            case Perks.SecondChance:
                return "Second Chance";
            case Perks.IMoreLikely:
                return "Common I";
            case Perks.TMoreLikely:
                return "Common T";
            case Perks.EqualOneIsZero:
                return "Minor Debuff";
            case Perks.AboveFiveMinusOne:
                return "Great Debuff";
            case Perks.EqualOneIsTwo:
                return "Minor Buff";
            case Perks.AboveFivePlusOne:
                return "Great Buff";
            case Perks.Meme:
                return "Meme";
            case Perks.MiniTimeBomb:
                return "Mini Time Bomb";
            case Perks.MegaTimeBomb:
                return "Mega Time Bomb";
            case Perks.MiniTimeShield:
                return "Mini Time Shield";
            case Perks.MegaTimeShield:
                return "Mega Time Shield";
        }
        return "oops... sorry bud name";
    }

    public static string PerkDescription(Perks newPerk)
    {
        switch (newPerk)
        {
            case Perks.ComboStartEarlier:
                return "Combo bonus starts 1x earlier.";
            case Perks.ComboAbove5PlusTwo:
                return "Combos above 3x deal 1 additional damage.";
            case Perks.EnemyStartThreeDam:
                return "Enemy starts with four damage with spread around holes (messy).";
            case Perks.YouStartFourDam:
                return "You start with six perfectly lined up (nice) damage.";
            case Perks.OneDamEvy600Points:
                return "For every 400 points you deal 1 damage.";
            case Perks.TwoDamEvy1000Points:
                return "For every 800 points you deal 2 damage.";
            case Perks.ThreeDamEvy1400Points:
                return "For every 1200 points you deal 3 damage.";
            case Perks.OneDamEvy8Lines:
                return "For every 7 lines cleared you deal 1 damage.";
            case Perks.TwoDamEvy13Lines:
                return "For every 14 lines cleared you deal 2 damage.";
            case Perks.ThreeDamEvy17Lines:
                return "For every 21 lines cleared you deal 3 damage.";
            case Perks.BlocksFallSlower:
                return "Your blocks fall at 65% speed.";
            case Perks.EnemyFallsFaster:
                return "Enemy blocks fall at 160% speed.";
            case Perks.EnemyMovesSlower:
                return "Enemy AI thinks and moves 25% slower.";
            case Perks.DamageIsNicer:
                return "Damage against you lines up more often (nicer).";
            case Perks.EnemyDamageIsMessy:
                return "Damage against the enemy lines up less often (messy).";
            case Perks.LineHoldEvy400Points:
                return "An I (cyan) piece is placed in your hold every 300 points.";
            case Perks.THoldEvy6Lines:
                return "A T (purple) piece is placed in your hold every 6 lines cleared.";
            case Perks.IgnoreFirst5LinesDamage:
                return "Ignore the first 4 damage against you.";
            case Perks.SecondChance:
                return "Once per game, remove the bottom 10 lines but give your opponent 2 resilience.";
            case Perks.IMoreLikely:
                return "You are more likely to get I (cyan) pieces.";
            case Perks.TMoreLikely:
                return "You are more likely to get T (purple) pieces.";
            case Perks.EqualOneIsZero:
                return "If you are attacked for exactly 1 damage, then ignore it.";
            case Perks.AboveFiveMinusOne:
                return "If you are attacked for 4 or more damage, subtract 1 from it.";
            case Perks.EqualOneIsTwo:
                return "If you attack the enemy for exactly 1 damage, then attack them for 2 instead.";
            case Perks.AboveFivePlusOne:
                return "If you attack the enemy for 4 or more damage, add 1 to it.";
            case Perks.Meme:
                return "Meme. Meme. Meme.";
            case Perks.MiniTimeBomb:
                return "Every 35 seconds, deal 1 damage.";
            case Perks.MegaTimeBomb:
                return "Every 70 seconds, deal 2 damage.";
            case Perks.MiniTimeShield:
                return "Every 35 seconds, gain 1 damage worth of resilience.";
            case Perks.MegaTimeShield:
                return "Every 70 seconds, gain 2 damage worth of resilience.";
        }
        return "oops... sorry bud desc";
    }

    public const int width = 10;
    public const int height = 21;

    public Mode mode;
    public int dominanceId;
    public Perks[] perks = new Perks[3];
    public Perks[] enemyPerks = new Perks[3];
    public OfflinePlayMode offlineMode;
    public OnlinePlayMode onlinePlayMode;
    public Modifications mods;
    public Multiplier multi;
    public SimuateBoardAI ai;
    public AI cleanUpBrain;
    public Circuit curCircuit;
    public CleanUp cleanUp;
    public bool stopped = false;
    public bool cheats = false;
    public bool readyForMultiPerk = false;
    [Space(10)]
    public bool isTutorial = false;
    public GameObject tutorialScreenOne;
    public Image tutorialLeft;
    public Image tutorialRight;
    public Image tutorialUp;
    public Image tutorialDown;
    public Image tutorialA;
    public Image tutorialD;
    public Image tutorialSpace;
    [Space(10)]
    public GameController manager;
    public TetrisPiece[,] grid = new TetrisPiece[width, height];
    public TetrisPiece[,] enemyGrid = new TetrisPiece[width, height];
    public List<Tetrimino> tetriminos = new List<Tetrimino>();
    public TetrisPiece garbageTetrisPiece;
    public SpriteRenderer[] fallDown;
    public Transform playerBlockHolder;
    public Transform enemyBlockHolder;
    public GameObject deathScreen;
    public GameObject resetButton;
    public GameObject pauseButton;
    public GameObject nextFightButton;
    public GameObject mainMenuButton;
    public GameObject levelSelectButton;
    public GameObject dottedLine;
    public Text sideText;
    [Space(10)]
    public GameObject playerBoard;
    public GameObject enemyBoard;
    public GameObject thanos;
    [Space(10)]
    public GameObject selectPerkButton;
    public GameObject perkOneButton;
    public GameObject perkTwoButton;
    public GameObject perkThreeButton;
    public Perks chosenPerkOne;
    public Perks chosenPerkTwo;
    public Perks chosenPerkThree;
    public Text perkList;
    public Text perkDesc;
    public Text perkDescWhite;
    public int chosenPerk = 2;
    public GameObject holdAdvice;
    public GameObject hangAdvice;
    public GameObject damageAdvice;
    public GameObject finishingAdvice;
    public GameObject whatIsGame;
    [Space(10)]
    public float fastFallSpeed = 12f;
    public float maxFallTimer = 0.8f;
    public int fightNumber = 0;
    public bool fastMode = false;
    public bool isWaiting = false;
    [Space(10)]
    public Text garbageText;
    public Text enemyGarbageText;
    public Text enemyNameText;
    [Space(10)]
    public Transform preview;
    public Transform hold;
    public Text holdText;
    public Text speedText;
    public SpriteRenderer holdBG;
    public Transform garbageHolder;
    public Transform enemyGarbageHolder;
    public SpriteRenderer garbagePrefab;
    public bool wasLastMoveSpin = false;
    [Space(10)]
    public Text lineText;
    public Text scoreText;
    public Text attackText;
    public Text timeText;
    [Space(10)]
    public Text effectBlack;
    public Text effectWhite;
    [Space(10)]
    public Text comboBlack;
    public Text comboWhite;
    [Space(10)]
    public Text currentDamage;

    private Tetrimino curTetrimino;
    private Tetrimino previewTetrimino;
    private Tetrimino holdTetrimino;

    private Tetrimino[] previousLongBlocks = new Tetrimino[8];
    private Tetrimino[] previousBlocks = new Tetrimino[5];
    private Tetrimino[] previousLongBlocksEnemy = new Tetrimino[8];
    private Tetrimino[] previousBlocksEnemy = new Tetrimino[5];
    private TetrisPiece[] enemyCur = new TetrisPiece[4];
    private bool canHold = true;
    private Coroutine topCo;
    private Coroutine bottomCo;

    private int lines = 0;
    private int score = 0;
    private int combo = 0;
    private int attacks = 0;
    private int currentCharge = 0;
    private float upTimer;
    private float time = 0;

    private int fourHundredCount = 1;
    private int sixHundredCount = 1;
    private int oneThousandCount = 1;
    private int oneThousandFourHundredCount = 1;

    private int sevenCount = 1;
    private int twelveCount = 1;
    private int sixteenCount = 1;
    private int twentyCount = 1;

    private int thirtyDamageCount = 1;
    private int thirtyShieldCount = 1;
    private int sixtyDamageCount = 1;
    private int sixtyShieldCount = 1;

    private bool won = false;
    private bool garbagePrep = false;
    private bool secondChanced = false;
    private bool hasHoldCanceled = false;
    private bool hasDamageExplained = false;
    private bool wasTutorial = false;

    private int hole;
    private bool isAbsoluteQuit = false;

    private int currentlyDefending = 0;
    public int GetAttacked
    {
        get
        {
            return currentlyDefending;
        }
        set
        {
            value = Mathf.Clamp(value, -10, 150);

            currentlyDefending = value;

            SendGameState();

            int garbageHeight = 0;
            int temp = currentlyDefending;

            for (int i = 0; i < garbageSprites.Count; i++)
            {
                Destroy(garbageSprites[i].gameObject);
            }
            garbageSprites = new List<SpriteRenderer>();

            while (temp >= 20)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, garbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = Color.black;

                garbageSprites.Add(newGar);
                garbageHeight++;
                temp -= 20;
            }

            while (temp >= 5)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, garbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = Color.red;

                garbageSprites.Add(newGar);
                garbageHeight++;
                temp -= 5;
            }

            while (temp > 0)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, garbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = new Color(0.75f, 0.75f, 0.75f);

                garbageSprites.Add(newGar);
                garbageHeight++;
                temp--;
            }

            while (temp < 0)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, garbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = new Color(0.4f, 1f, 0.4f);

                garbageSprites.Add(newGar);
                garbageHeight++;
                temp++;
            }
        }
    }

    private int enemyCurrentlyDefending = 0;
    public int EnemyGetAttacked
    {
        get
        {
            return enemyCurrentlyDefending;
        }
        set
        {
            value = Mathf.Clamp(value, -10, 150);

            enemyCurrentlyDefending = value;

            int garbageHeight = 0;
            int temp = enemyCurrentlyDefending;

            for (int i = 0; i < enemyGarbageSprite.Count; i++)
            {
                Destroy(enemyGarbageSprite[i].gameObject);
            }
            enemyGarbageSprite = new List<SpriteRenderer>();

            while (temp >= 20)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, enemyGarbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = Color.black;

                enemyGarbageSprite.Add(newGar);
                garbageHeight++;
                temp -= 20;
            }

            while (temp >= 5)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, enemyGarbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = Color.red;

                enemyGarbageSprite.Add(newGar);
                garbageHeight++;
                temp -= 5;
            }

            while (temp > 0)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, enemyGarbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = new Color(0.75f, 0.75f, 0.75f);

                enemyGarbageSprite.Add(newGar);
                garbageHeight++;
                temp--;
            }

            while (temp < 0)
            {
                SpriteRenderer newGar = Instantiate(garbagePrefab, enemyGarbageHolder);
                newGar.transform.localPosition = new Vector3(0, garbageHeight, 0);
                newGar.color = new Color(0.4f, 1f, 0.4f);

                enemyGarbageSprite.Add(newGar);
                garbageHeight++;
                temp++;
            }
        }
    }

    private List<SpriteRenderer> garbageSprites = new List<SpriteRenderer>();
    private List<SpriteRenderer> enemyGarbageSprite = new List<SpriteRenderer>();

    float GetSlowMaxFallTimer()
    {
        if (fastMode)
        {
            if (lines <= 5)
            {
                return 0.2f;
            }
            if (lines <= 10)
            {
                return 0.18f;
            }
            if (lines <= 15)
            {
                return 0.16f;
            }
            if (lines <= 20)
            {
                return 0.14f;
            }
            else if (lines <= 30)
            {
                return 0.12f;
            }
            else if (lines <= 40)
            {
                return 0.11f;
            }
            else if (lines <= 55)
            {
                return 0.1f;
            }
            else if (lines <= 70)
            {
                return 0.09f;
            }
            else if (lines <= 90)
            {
                return 0.08f;
            }
            return 0.07f;
        }
        else
        {
            if (lines <= 5)
            {
                return 0.8f;
            }
            if (lines <= 10)
            {
                return 0.7f;
            }
            if (lines <= 15)
            {
                return 0.6f;
            }
            if (lines <= 20)
            {
                return 0.5f;
            }
            else if (lines <= 30)
            {
                return 0.45f;
            }
            else if (lines <= 40)
            {
                return 0.4f;
            }
            else if (lines <= 55)
            {
                return 0.35f;
            }
            else if (lines <= 70)
            {
                return 0.3f;
            }
            else if (lines <= 90)
            {
                return 0.25f;
            }
            return 0.2f;
        }
    }

    float GetFastFallSpeed()
    {
        if (fastMode)
        {
            if (lines <= 5)
            {
                return 4f;
            }
            else if (lines <= 10)
            {
                return 3.75f;
            }
            else if (lines <= 15)
            {
                return 3.5f;
            }
            else if (lines <= 20)
            {
                return 3.25f;
            }
            else if (lines <= 30)
            {
                return 3f;
            }
            else if (lines <= 40)
            {
                return 2.75f;
            }
            else if (lines <= 55)
            {
                return 2.5f;
            }
            else if (lines <= 70)
            {
                return 2.25f;
            }
            else if (lines <= 90)
            {
                return 2f;
            }
            return 2f;
        }
        else
        {
            if (lines <= 5)
            {
                return 11f;
            }
            else if (lines <= 10)
            {
                return 10f;
            }
            else if (lines <= 15)
            {
                return 9f;
            }
            else if (lines <= 20)
            {
                return 8f;
            }
            else if (lines <= 30)
            {
                return 7f;
            }
            else if (lines <= 40)
            {
                return 6f;
            }
            else if (lines <= 55)
            {
                return 5.5f;
            }
            else if (lines <= 70)
            {
                return 5f;
            }
            else if (lines <= 90)
            {
                return 4.5f;
            }
            return 4f;
        }
    }

    public void Pause()
    {
        isWaiting = !isWaiting;
        if (offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
        {
            ai.isWaiting = isWaiting;
        }
        Stop(topCo);
        if (isWaiting)
        {
            effectWhite.gameObject.SetActive(true);
            effectBlack.gameObject.SetActive(true);

            effectBlack.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            effectBlack.transform.rotation = Quaternion.Euler(0, 0, 0);

            effectWhite.text = "Paused";
            effectBlack.text = "Paused";

            effectWhite.color = new Color(1, 1, 1, 1);
            effectBlack.color = new Color(0, 0, 0, 0.5f);
        }
        else
        {
            effectWhite.gameObject.SetActive(false);
            effectBlack.gameObject.SetActive(false);
        }
    }

    public void SetName(string enemyName)
    {
        enemyNameText.text = enemyName;
    }

    public void ResetBoard(bool speed)
    {
        if (mode == Mode.Offline)
        {
            mods = Modifications.Regular;
            multi = Multiplier.OneX;
        }

        upTimer = 0;
        readyForMultiPerk = false;
        secondChanced = false;

        fourHundredCount = 1;
        sixHundredCount = 1;
        oneThousandCount = 1;
        oneThousandFourHundredCount = 1;

        thirtyDamageCount = 1;
        thirtyShieldCount = 1;
        sixtyDamageCount = 1;
        sixtyShieldCount = 1;

        sevenCount = 1;
        twelveCount = 1;
        sixteenCount = 1;
        twentyCount = 1;

        dottedLine.SetActive(mode == Mode.Offline && offlineMode == OfflinePlayMode.CleanUp);

        whatIsGame.SetActive(false);
        damageAdvice.SetActive(false);
        holdAdvice.SetActive(false);
        hangAdvice.SetActive(false);
        finishingAdvice.SetActive(false);

        tutorialScreenOne.SetActive(false);

        nextFightButton.SetActive(false);
        selectPerkButton.SetActive(false);

        perkOneButton.SetActive(false);
        perkTwoButton.SetActive(false);
        perkThreeButton.SetActive(false);

        perkDesc.gameObject.SetActive(false);
        perkDescWhite.gameObject.SetActive(false);

        Stop(topCo);
        Stop(bottomCo);

        effectWhite.gameObject.SetActive(false);
        effectBlack.gameObject.SetActive(false);

        comboWhite.gameObject.SetActive(false);
        comboBlack.gameObject.SetActive(false);

        mainMenuButton.gameObject.SetActive(true);
        levelSelectButton.gameObject.SetActive(false);

        pauseButton.SetActive(false);

        stopped = false;
        won = false;
        garbagePrep = false;
        garbageText.color = Color.white;
        enemyGarbageText.color = Color.white;
        resetButton.SetActive(false);

        if (mode == Mode.Offline)
        {
            if (offlineMode == OfflinePlayMode.Freeplay)
            {
                sideText.text = "Freeplay";
                perks = new Perks[3];
                enemyPerks = new Perks[3];
                perkList.text = "";
                enemyBoard.gameObject.SetActive(false);
                playerBoard.transform.position = new Vector3(0, 0, 0);

                for (int i = 0; i < enemyCur.Length; i++)
                {
                    if (enemyCur[i] != null)
                    {
                        Destroy(enemyCur[i].gameObject);
                        enemyCur[i] = null;
                    }
                }
            }
            else if (offlineMode == OfflinePlayMode.VersusAI)
            {
                sideText.text = "Versus AI";
                perks = new Perks[3];
                enemyPerks = new Perks[3];
                perkList.text = "";
                enemyBoard.gameObject.SetActive(true);
                playerBoard.transform.position = new Vector3(-7.5f, 0, 0);
                enemyBoard.transform.position = new Vector3(13.5f, 0, 0);

                enemyNameText.text = ai.brain.name;

                for (int i = 0; i < enemyCur.Length; i++)
                {
                    if (enemyCur[i] != null)
                    {
                        Destroy(enemyCur[i].gameObject);
                        enemyCur[i] = null;
                    }
                }
            }
            else if (offlineMode == OfflinePlayMode.Circuit)
            {
                sideText.text = "Circuit";

                ai.brain = curCircuit.ais[fightNumber];
                if (fightNumber == 0)
                {
                    perkList.text = "";
                }
                if (fightNumber == 1)
                {
                    perkList.text = PerkName(perks[0]);
                }
                if (fightNumber == 2)
                {
                    perkList.text = PerkName(perks[0]) + ", " + PerkName(perks[1]);
                }
                if (fightNumber == 3)
                {
                    perkList.text = PerkName(perks[0]) + ", " + PerkName(perks[1]) + ", " + PerkName(perks[2]);
                }

                enemyBoard.gameObject.SetActive(true);
                playerBoard.transform.position = new Vector3(-7.5f, 0, 0);
                enemyBoard.transform.position = new Vector3(13.5f, 0, 0);

                enemyNameText.text = ai.brain.name;

                for (int i = 0; i < enemyCur.Length; i++)
                {
                    if (enemyCur[i] != null)
                    {
                        Destroy(enemyCur[i].gameObject);
                        enemyCur[i] = null;
                    }
                }
            }
            else
            {
                sideText.text = "Clean Up - Level " + cleanUp.level;
                perks = new Perks[3];
                enemyPerks = new Perks[3];
                perkList.text = "";
                enemyBoard.gameObject.SetActive(false);
                playerBoard.transform.position = new Vector3(0, 0, 0);

                for (int i = 0; i < enemyCur.Length; i++)
                {
                    if (enemyCur[i] != null)
                    {
                        Destroy(enemyCur[i].gameObject);
                        enemyCur[i] = null;
                    }
                }
            }
        }
        else
        {
            sideText.text = (onlinePlayMode == OnlinePlayMode.Dominance) ? manager.GetBehindName(dominanceId) + " Attacking You! " + manager.PlayersLeft() + " Left." : "Duel";

            fightNumber = 0;
            perks = new Perks[3];
            enemyPerks = new Perks[3];
            perkList.text = "";
            enemyBoard.gameObject.SetActive(true);
            playerBoard.transform.position = new Vector3(-7.5f, 0, 0);
            enemyBoard.transform.position = new Vector3(13.5f, 0, 0);

            for (int i = 0; i < enemyCur.Length; i++)
            {
                if (enemyCur[i] == null)
                {
                    enemyCur[i] = Instantiate(garbageTetrisPiece, enemyBlockHolder.transform);
                }
            }
        }

        thanos.SetActive(HasPerk(Perks.Meme));

        currentDamage.enabled = false;
        lines = 0;
        score = 0;
        combo = 0;
        attacks = 0;
        currentCharge = 0;
        time = 0;
        canHold = true;
        wasTutorial = false;

        fastMode = speed;
        speedText.text = fastMode ? "Hard" : "Normal";

        fastFallSpeed = GetFastFallSpeed();
        maxFallTimer = GetSlowMaxFallTimer();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
                if (enemyGrid[x, y] != null)
                {
                    Destroy(enemyGrid[x, y].gameObject);
                    enemyGrid[x, y] = null;
                }
            }
        }

        /*while (enemyBlockHolder.childCount > 0)
        {
            Destroy(enemyBlockHolder.GetChild(0).gameObject);
        }

        while (playerBlockHolder.childCount > 0)
        {
            Destroy(playerBlockHolder.GetChild(0).gameObject);
        }*/

        if (curTetrimino)
        {
            Destroy(curTetrimino.gameObject);
            curTetrimino = null;
        }
        if (previewTetrimino)
        {
            Destroy(previewTetrimino.gameObject);
            previewTetrimino = null;
        }
        if (holdTetrimino)
        {
            Destroy(holdTetrimino.gameObject);
            holdTetrimino = null;
        }

        if (HasPerk(Perks.IgnoreFirst5LinesDamage))
        {
            GetAttacked = -4;
        }
        else
        {
            GetAttacked = 0;
        }
        EnemyGetAttacked = 0;

        lineText.text = "0";
        scoreText.text = "0";
        attackText.text = "0";
        timeText.text = "0:00";

        deathScreen.gameObject.SetActive(false);

        for (int i = 0; i < previousBlocks.Length; i++)
        {
            previousBlocks[i] = null;
        }
        for (int i = 0; i < previousLongBlocks.Length; i++)
        {
            previousLongBlocks[i] = null;
        }

        for (int i = 0; i < previousBlocksEnemy.Length; i++)
        {
            previousBlocksEnemy[i] = null;
        }
        for (int i = 0; i < previousLongBlocksEnemy.Length; i++)
        {
            previousLongBlocksEnemy[i] = null;
        }

        previewTetrimino = Instantiate(ChooseTetriomino(false), playerBlockHolder);
        previewTetrimino.transform.position = preview.transform.position - new Vector3(previewTetrimino.offset ? 0.5f : 0, 0, 0);

        chosenPerk = 2;

        hole = Random.Range(0, width);
        isWaiting = true;
        StartCoroutine(NewPiece(false));
        StartCoroutine(WaitThree());

        if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
        {
            ai.Begin();
        }

        if (offlineMode == OfflinePlayMode.Circuit)
        {
            if (HasPerk(Perks.EnemyStartThreeDam))
            {
                StartCoroutine(ai.DoSabotage());
            }
            if (HasPerk(Perks.YouStartFourDam))
            {
                StartCoroutine(DoSelfHarm());
            }
        }
        if (mode == Mode.Offline && offlineMode == OfflinePlayMode.CleanUp)
        {
            StartCoroutine(DoCleanUpStart());
        }
    }

    IEnumerator DoCleanUpStart()
    {
        for (int garb = 0; garb < cleanUp.garbageTiles.Length; garb++)
        {
            int curHole = hole;
            while (curHole == hole)
            {
                hole = Random.Range(0, width);
            }
            for (int i = 0; i < cleanUp.garbageTiles[garb]; i++)
            {
                for (int risingY = height - 2; risingY >= 0; risingY--)
                {
                    for (int x = 0; x < width; x++)
                    {
                        grid[x, risingY + 1] = grid[x, risingY];
                        if (grid[x, risingY + 1] != null)
                        {
                            grid[x, risingY].y++;
                            grid[x, risingY].transform.localPosition = new Vector3(x, risingY + 1);
                        }
                    }
                }
                for (int x = 0; x < width; x++)
                {
                    if (x != hole)
                    {
                        grid[x, 0] = Instantiate(garbageTetrisPiece, playerBlockHolder);
                        grid[x, 0].transform.localPosition = new Vector3(x, 0, 0);
                    }
                    else
                    {
                        grid[x, 0] = null;
                    }
                }

                if (curTetrimino)
                {
                    curTetrimino.board = this;
                    curTetrimino.UpdateFallDown();
                }

                yield return new WaitForSeconds(0.2f);
                SendGameState();
            }
        }
        for (int i = 0; i < cleanUp.aiTiles; i++)
        {
            yield return 0;
            yield return 0;
            yield return 0;
            yield return 0;
            BoardState plannedState = curTetrimino.GetStates(grid, cleanUpBrain, 0, true);
            curTetrimino.x = plannedState.x;
            yield return 0;
            yield return 0;
            yield return 0;
            yield return 0;
            for (int j = 0; j < plannedState.rot; j++)
            {
                yield return 0;
                yield return 0;
                if (curTetrimino.AttemptRightRotate(0, 0, true)) ;
                else if (curTetrimino.AttemptRightRotate(-1, 0, true)) ;
                else if (curTetrimino.AttemptRightRotate(1, 0, true)) ;
                else if (curTetrimino.AttemptRightRotate(0, -1, true)) ;
                else if (curTetrimino.AttemptRightRotate(-2, 0, true)) ;
                else if (curTetrimino.AttemptRightRotate(2, 0, true)) ;
            }
            yield return 0;
            yield return 0;
            yield return 0;
            yield return 0;
            while (curTetrimino.Fall()) ;
        }
        yield return 0;
    }

    public void SelectPerk()
    {
        if (chosenPerk == 1)
        {
            perks[fightNumber - 1] = chosenPerkOne;
        }
        else if (chosenPerk == 2)
        {
            perks[fightNumber - 1] = chosenPerkTwo;
        }
        else
        {
            perks[fightNumber - 1] = chosenPerkThree;
        }

        if (fightNumber == 1)
        {
            perkList.text = PerkName(perks[0]);
        }
        if (fightNumber == 2)
        {
            perkList.text = PerkName(perks[0]) + ", " + PerkName(perks[1]);
        }
        if (fightNumber == 3)
        {
            perkList.text = PerkName(perks[0]) + ", " + PerkName(perks[1]) + ", " + PerkName(perks[2]);
        }

        if (!((onlinePlayMode == OnlinePlayMode.OnePerk && fightNumber == 1) || (onlinePlayMode == OnlinePlayMode.TwoPerks && fightNumber == 2) || (onlinePlayMode == OnlinePlayMode.ThreePerks && fightNumber == 3)))
        {
            fightNumber++;
            RandoPerks();
            UpdatePerks();
        }
    }

    IEnumerator WaitThree()
    {
        Stop(topCo);
        if ((mode != Mode.Offline && (onlinePlayMode != OnlinePlayMode.Regular && onlinePlayMode != OnlinePlayMode.Dominance)) || (mode == Mode.Offline && offlineMode == OfflinePlayMode.VersusAI && onlinePlayMode != OnlinePlayMode.Regular))
        {
            fightNumber++;
            RandoPerks();
            UpdatePerks();
            selectPerkButton.gameObject.SetActive(true);

            if (onlinePlayMode == OnlinePlayMode.OnePerk)
            {
                while (perks[0] == Perks.None && isWaiting)
                {
                    yield return 0;
                }
            }
            else if (onlinePlayMode == OnlinePlayMode.TwoPerks)
            {
                while (perks[1] == Perks.None && isWaiting)
                {
                    yield return 0;
                }
            }
            else
            {
                while (perks[2] == Perks.None && isWaiting)
                {
                    yield return 0;
                }
            }

            perkOneButton.SetActive(false);
            perkTwoButton.SetActive(false);
            perkThreeButton.SetActive(false);

            perkDesc.gameObject.SetActive(false);
            perkDescWhite.gameObject.SetActive(false);

            selectPerkButton.SetActive(false);

            if (mode == Mode.Host)
            {
                manager.ServerStartPerkGame();
            }
            if (mode == Mode.Client)
            {
                manager.ClientSendPerks();
            }

            if (mode != Mode.Offline)
            {
                while (!readyForMultiPerk && isWaiting)
                {
                    print(readyForMultiPerk + " " + isWaiting);
                    yield return 0;
                }
                print(readyForMultiPerk + " " + isWaiting);
            }
        }
        if (mode != Mode.Offline)
        {
            if (HasPerk(Perks.YouStartFourDam))
            {
                StartCoroutine(DoSelfHarm());
            }
            else if (EnemyHasPerk(Perks.EnemyStartThreeDam))
            {
                StartCoroutine(DoSabotage());
            }

            if (HasPerk(Perks.IgnoreFirst5LinesDamage))
            {
                GetAttacked = -4;
            }
            thanos.SetActive(EnemyHasPerk(Perks.Meme));
        }
        else if (offlineMode != OfflinePlayMode.Circuit)
        {
            if (HasPerk(Perks.EnemyStartThreeDam))
            {
                StartCoroutine(ai.DoSabotage());
            }
            if (HasPerk(Perks.YouStartFourDam))
            {
                StartCoroutine(DoSelfHarm());
            }

            if (HasPerk(Perks.IgnoreFirst5LinesDamage))
            {
                GetAttacked = -4;
            }
            thanos.SetActive(HasPerk(Perks.Meme));
        }
        if (mode == Mode.Offline && offlineMode == OfflinePlayMode.CleanUp)
        {
            yield return 0;
            effectWhite.gameObject.SetActive(true);
            effectBlack.gameObject.SetActive(true);
            comboWhite.gameObject.SetActive(true);
            comboBlack.gameObject.SetActive(true);

            effectBlack.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            effectBlack.transform.rotation = Quaternion.Euler(0, 0, 0);

            comboBlack.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            comboBlack.transform.rotation = Quaternion.Euler(0, 0, 0);

            effectWhite.text = "Time Limit:";
            effectBlack.text = "Time Limit:";


            if ((cleanUp.timeLimit * (fastMode ? 0.667f : 1f))  % 60 < 10)
            {
                comboWhite.text = Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) / 60f) + ":0" + Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) % 60);
                comboBlack.text = Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) / 60f) + ":0" + Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) % 60);
                timeText.text = Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) / 60f) + ":0" + Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) % 60);
            }
            else
            {
                comboWhite.text = Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) / 60f) + ":" + Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) % 60);
                comboBlack.text = Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) / 60f) + ":" + Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) % 60);
                timeText.text = Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) / 60f) + ":" + Mathf.Floor((cleanUp.timeLimit * (fastMode ? 0.667f : 1f)) % 60);
            }
            time = (cleanUp.timeLimit * (fastMode ? 0.667f : 1f));

            effectWhite.color = new Color(1, 1, 1, 1);
            effectBlack.color = new Color(0, 0, 0, 0.5f);

            comboWhite.color = new Color(1, 1, 1, 1);
            comboBlack.color = new Color(0, 0, 0, 0.5f);

            yield return new WaitForSeconds(3f);

            effectWhite.gameObject.SetActive(false);
            effectBlack.gameObject.SetActive(false);
            comboWhite.gameObject.SetActive(false);
            comboBlack.gameObject.SetActive(false);
        }
        if (mode != Mode.Offline && onlinePlayMode == OnlinePlayMode.Dominance)
        {
            yield return 0;
            effectWhite.gameObject.SetActive(true);
            effectBlack.gameObject.SetActive(true);
            comboWhite.gameObject.SetActive(true);
            comboBlack.gameObject.SetActive(true);

            effectBlack.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            effectBlack.transform.rotation = Quaternion.Euler(0, 0, 0);

            comboBlack.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            comboBlack.transform.rotation = Quaternion.Euler(0, 0, 0);

            effectWhite.text = "Attacking you:";
            effectBlack.text = "Attacking you:";

            comboWhite.text = manager.GetBehindName(dominanceId);
            comboBlack.text = manager.GetBehindName(dominanceId);

            effectWhite.color = new Color(1, 1, 1, 1);
            effectBlack.color = new Color(0, 0, 0, 0.5f);

            comboWhite.color = new Color(1, 1, 1, 1);
            comboBlack.color = new Color(0, 0, 0, 0.5f);

            yield return new WaitForSeconds(3f);

            effectWhite.gameObject.SetActive(false);
            effectBlack.gameObject.SetActive(false);
            comboWhite.gameObject.SetActive(false);
            comboBlack.gameObject.SetActive(false);
        }
        if (mode == Mode.Offline && offlineMode == OfflinePlayMode.Freeplay && isTutorial)
        {
            tutorialScreenOne.SetActive(true);
            tutorialLeft.color = Color.white;
            tutorialRight.color = Color.white;
            tutorialUp.color = Color.white;
            tutorialDown.color = Color.white;
            tutorialA.color = Color.white;
            tutorialD.color = Color.white;
            tutorialSpace.color = Color.white;

            while (isTutorial)
            {
                yield return 0;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    tutorialLeft.color = Color.green;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    tutorialRight.color = Color.green;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    tutorialUp.color = Color.green;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    tutorialDown.color = Color.green;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    tutorialA.color = Color.green;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    tutorialD.color = Color.green;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialSpace.color = Color.green;
                }

                if (tutorialLeft.color == Color.green && tutorialRight.color == Color.green &&
                    tutorialUp.color == Color.green && tutorialDown.color == Color.green &&
                    tutorialA.color == Color.green && tutorialD.color == Color.green && tutorialSpace.color == Color.green)
                {
                    isTutorial = false;
                    wasTutorial = true;
                    tutorialScreenOne.SetActive(false);
                }
            }

            whatIsGame.SetActive(true);
            yield return 0;

            while (!Input.GetKeyDown(KeyCode.Space) && lines != 1)
            {
                yield return 0;
            }

            lines = 0;
            whatIsGame.SetActive(false);
        }
        if (mode != Mode.Offline)
        {
            if (mods == Modifications.Regular)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "No Mods"));
            else if (mods == Modifications.LinesOnly)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "Lines Only"));
            else if (mods == Modifications.CombosOnly)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "Combos Only"));
            else if (mods == Modifications.TSpinsOnly)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "T-Spins Only"));
            else if (mods == Modifications.NoLines)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "No Lines"));
            else if (mods == Modifications.NoCombos)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "No Combos"));
            else 
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "No T-Spins"));
            yield return new WaitForSeconds(2f);
            Stop(topCo);
            if (multi == Multiplier.OneX)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "1x Damage"));
            else if (multi == Multiplier.TwoX)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "2x Damage"));
            else if (multi == Multiplier.ThreeX)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "3x Damage"));
            else if (multi == Multiplier.PointFiveRoundUp)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "0.5x Dam (R. Up)"));
            else if (multi == Multiplier.PointFiveRoundDown)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "0.5x Dam (R. Down)"));
            else if (multi == Multiplier.OnePointFiveRoundUp)
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "1.5x Dam (R. Up)"));
            else 
                topCo = StartCoroutine(EffectMidNoShake(effectWhite, effectBlack, "1.5x Dam (R. Down)"));
            yield return new WaitForSeconds(2f);
            Stop(topCo);
        }
        topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "3", false));
        yield return new WaitForSeconds(0.7f);
        Stop(topCo);
        topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "2", false));
        yield return new WaitForSeconds(0.7f);
        Stop(topCo);
        topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "1", false));
        yield return new WaitForSeconds(0.7f);
        Stop(topCo);
        topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "Go!", false));
        isWaiting = false;
        pauseButton.SetActive(mode == Mode.Offline);
    }

    public void ResetButton()
    {
        fightNumber = 0;
        perks = new Perks[3];
        ResetBoard(fastMode);
    }

    public void NextFight()
    {
        if (chosenPerk == 1)
        {
            perks[fightNumber - 1] = chosenPerkOne;
        }
        else if (chosenPerk == 2)
        {
            perks[fightNumber - 1] = chosenPerkTwo;
        }
        else
        {
            perks[fightNumber - 1] = chosenPerkThree;
        }
        ResetBoard(fastMode);
    }

    IEnumerator ResetSecondChance()
    {
        yield return new WaitForSeconds(0.1f);
        curTetrimino.Create(this, false);
        curTetrimino.preview = false;
    }

    public void Death(bool aiDied)
    {
        if (!won)
        {
            if (aiDied)
            {
                Win();
                ai.stop = true;
            }
            else
            {
                if (HasPerk(Perks.SecondChance) && !secondChanced)
                {
                    secondChanced = true;

                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (grid[x, 0] != null)
                            {
                                StartCoroutine(grid[x, 0].LineClear());
                            }
                        }
                        for (int fallingY = 0; fallingY < height - 1; fallingY++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                grid[x, fallingY] = grid[x, fallingY + 1];
                                if (grid[x, fallingY] != null)
                                {
                                    grid[x, fallingY].y--;
                                    grid[x, fallingY].transform.localPosition = new Vector3(x, fallingY);
                                }
                            }
                        }
                        for (int x = 0; x < width; x++)
                        {
                            grid[x, height - 1] = null;
                        }
                    }

                    if (mode == Mode.Host)
                    {
                        manager.ServerSendDamage(-2);
                    }
                    else if (mode == Mode.Client)
                    {
                        manager.ClientSendDamage(-2);
                    }
                    else if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                    {
                        EnemyGetAttacked -= 2;
                    }

                    StartCoroutine(ResetSecondChance());

                    Stop(topCo);
                    topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "Second Chance...", true));

                    return;
                }

                deathScreen.SetActive(true);
                if (mode == Mode.Offline && wasTutorial)
                {
                    finishingAdvice.SetActive(true);
                }
                if (curTetrimino)
                {
                    curTetrimino.preview = true;
                }

                GetAttacked = 0;
                EnemyGetAttacked = 0;

                if (mode == Mode.Client)
                {
                    manager.ClientSendWin(score);
                }
                else if (mode == Mode.Host)
                {
                    manager.ServerSendWin(score);
                }

                if (mode == Mode.Offline)
                {
                    resetButton.SetActive(true);
                    if (offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                    {
                        ai.stop = true;
                        if (ai.enemyCurTetra != null)
                        {
                            ai.enemyCurTetra.preview = true;
                        }
                    }
                    else if (!cheats && offlineMode != OfflinePlayMode.CleanUp)
                    {
                        if (!fastMode)
                        {
                            Stop(topCo);

                            int top = SaveLoadStats.PushNormalHighscore(score);
                            if (top == 1)
                            {
                                topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "New Highscore! #1", false));
                            }
                            else if (top == 2 || top == 3)
                            {
                                topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "New Highscore! #" + top, false));
                            }
                            else if (top == 4 || top == 5)
                            {
                                topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "New Highscore! #" + top, false));
                            }
                            SaveLoadStats.Save();
                        }
                        else
                        {
                            Stop(topCo);

                            int top = SaveLoadStats.PushHardHighscore(score);
                            if (top == 1)
                            {
                                topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "New Highscore! #1", false));
                            }
                            else if (top == 2 || top == 3)
                            {
                                topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "New Highscore! #" + top, false));
                            }
                            else if (top == 4 || top == 5)
                            {
                                topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "New Highscore! #" + top, false));
                            }
                            SaveLoadStats.Save();
                        }
                    }
                }
            }
        }
    }

    public void AdviceIsGay()
    {
        isWaiting = false;

        holdAdvice.SetActive(false);
        damageAdvice.SetActive(false);
        hangAdvice.SetActive(false);
    }

    void ActivateDamage()
    {
        isWaiting = true;
        hasDamageExplained = true;

        damageAdvice.SetActive(true);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && mode == Mode.Offline && pauseButton.activeSelf)
        {
            Pause();
        }

        if (!hasHoldCanceled && time > 20 && mode == Mode.Offline && offlineMode == OfflinePlayMode.Freeplay && holdTetrimino == null && wasTutorial)
        {
            isWaiting = true;
            hasHoldCanceled = true;

            holdAdvice.SetActive(true);
        }

        if (time > 40 && upTimer < 2 && wasTutorial)
        {
            isWaiting = true;
            upTimer = 2;

            hangAdvice.SetActive(true);
        }

        if (cheats && Input.GetKey(KeyCode.Alpha0) && Input.GetKeyDown(KeyCode.R) && perkTwoButton.activeSelf)
        {
            RandoPerks();
            UpdatePerks();
        }
        if (!isWaiting)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                upTimer += Time.deltaTime;
            }

            thanos.transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
            if (!deathScreen.activeSelf && !won)
            {
                if (mode == Mode.Host)
                {
                    manager.ServerSendCur(curTetrimino);
                }
                else if (mode == Mode.Client)
                {
                    manager.ClientSendCur(curTetrimino);
                }
            }

            if (cheats && Input.GetKey(KeyCode.Alpha0) && Input.GetKey(KeyCode.K) && Input.GetKeyDown(KeyCode.B) && holdTetrimino != null)
            {
                if (mode == Mode.Host)
                {
                    manager.ServerSendDamage(100);
                }
                else if (mode == Mode.Client)
                {
                    manager.ClientSendDamage(100);
                }
                else if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                {
                    EnemyGetAttacked += 100;
                }
            }
            if (!deathScreen.activeSelf && !won && !stopped)
            {
                if (time / 35f >= thirtyDamageCount)
                {
                    thirtyDamageCount++;
                    if (HasPerk(Perks.MiniTimeBomb))
                    {
                        if (mode == Mode.Host)
                        {
                            manager.ServerSendDamage(1);
                        }
                        else if (mode == Mode.Client)
                        {
                            manager.ClientSendDamage(1);
                        }
                        else if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                        {
                            EnemyGetAttacked += 1;
                        }

                        Stop(bottomCo);
                        bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "Mini Time Bomb", false));
                    }
                }
                if (time / 35f >= thirtyShieldCount)
                {
                    thirtyShieldCount++;
                    if (HasPerk(Perks.MiniTimeShield))
                    {
                        GetAttacked--;

                        Stop(bottomCo);
                        bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "Mini Time Shield", false));
                    }
                }

                if (time / 70f >= sixtyDamageCount)
                {
                    sixtyDamageCount++;
                    if (HasPerk(Perks.MegaTimeBomb))
                    {
                        if (mode == Mode.Host)
                        {
                            manager.ServerSendDamage(2);
                        }
                        else if (mode == Mode.Client)
                        {
                            manager.ClientSendDamage(2);
                        }
                        else if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                        {
                            EnemyGetAttacked += 2;
                        }

                        Stop(bottomCo);
                        bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "Mega Time Bomb!!!", false));
                    }
                }
                if (time / 70f >= sixtyShieldCount)
                {
                    sixtyShieldCount++;
                    if (HasPerk(Perks.MegaTimeShield))
                    {
                        GetAttacked -= 2;

                        Stop(bottomCo);
                        bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "Mega Time Shield!!!", false));
                    }
                }

                time += Time.deltaTime * ((mode == Mode.Offline && offlineMode == OfflinePlayMode.CleanUp) ? -1f : 1);
                if (mode == Mode.Offline && offlineMode == OfflinePlayMode.CleanUp)
                {
                    if (time <= 0)
                    {
                        timeText.text = "Time Up";
                        mainMenuButton.gameObject.SetActive(false);
                        levelSelectButton.gameObject.SetActive(true);
                        Death(false);
                    }
                }
                if (time > 0)
                {
                    if (time % 60 < 10)
                    {
                        timeText.text = Mathf.Floor(time / 60f) + ":0" + Mathf.Floor(time % 60);
                    }
                    else
                    {
                        timeText.text = Mathf.Floor(time / 60f) + ":" + Mathf.Floor(time % 60);
                    }
                }
                if (GameController.defaultControls ? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.C))
                {
                    if (canHold)
                    {
                        canHold = false;
                        if (holdTetrimino == null)
                        {
                            holdTetrimino = curTetrimino;
                            holdTetrimino.Hold();
                            holdTetrimino.preview = true;
                            holdTetrimino.transform.position = hold.transform.position - new Vector3(holdTetrimino.offset ? 0.5f : 0, 0, 0);

                            curTetrimino = null;
                            StartCoroutine(NewPiece(false));
                        }
                        else
                        {
                            Tetrimino held = holdTetrimino;
                            holdTetrimino = curTetrimino;
                            holdTetrimino.Hold();
                            holdTetrimino.preview = true;
                            holdTetrimino.transform.position = hold.transform.position - new Vector3(holdTetrimino.offset ? 0.5f : 0, 0, 0);

                            curTetrimino = held;
                            curTetrimino.Create(this, false);
                        }
                        holdText.color = new Color(0.45f, 0.45f, 0.45f);
                        holdBG.color = new Color(0.13f, 0.20f, 0.32f);
                    }
                }
            }
        }
    }

    public void DominanceOver()
    {
        if (!deathScreen.activeSelf)
        {
            Win();
        }
        else
        {
            Stop(topCo);
            topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "Game Over.", false));
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectMid(comboWhite, comboBlack, "Back to lobby.", false));
        }
    }

    public void SetBehind()
    {
        sideText.text = manager.GetBehindName(dominanceId) + " Attacking You! " + manager.PlayersLeft() + " Left.";
    }

    public void GetKill()
    {
        if (!won)
        {
            StartCoroutine(GetDominanceKill());
            SetName(manager.GetAheadName(dominanceId));
        }
    }

    private IEnumerator GetDominanceKill()
    {
        mainMenuButton.gameObject.SetActive(false);
        while (isWaiting)
        {
            yield return 0;
        }
        GetAttacked -= 2;
        isWaiting = true;
        Stop(topCo);
        topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "KO!", false));

        yield return new WaitForSeconds(1.75f);

        fightNumber++;
        RandoPerks();
        UpdatePerks();
        if (fightNumber < 2)
        {
            Stop(topCo);
            topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "5", false));
            yield return new WaitForSeconds(1f);
            Stop(topCo);
            topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "4", false));
            yield return new WaitForSeconds(1f);
        }
        Stop(topCo);
        topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "3", false));
        yield return new WaitForSeconds(1f);
        Stop(topCo);
        topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "2", false));
        yield return new WaitForSeconds(1f);
        Stop(topCo);
        topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "1", false));
        yield return new WaitForSeconds(1f);
        Stop(topCo);
        topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "Go!", false));

        if (chosenPerk == 1)
        {
            perks[fightNumber - 1] = chosenPerkOne;
        }
        else if (chosenPerk == 2)
        {
            perks[fightNumber - 1] = chosenPerkTwo;
        }
        else
        {
            perks[fightNumber - 1] = chosenPerkThree;
        }

        if (perks[fightNumber - 1] == Perks.IgnoreFirst5LinesDamage)
        {
            GetAttacked -= 4;
        }

        if (fightNumber == 1)
        {
            perkList.text = PerkName(perks[0]);
        }
        if (fightNumber == 2)
        {
            perkList.text = PerkName(perks[0]) + ", " + PerkName(perks[1]);
        }
        if (fightNumber == 3)
        {
            perkList.text = PerkName(perks[0]) + ", " + PerkName(perks[1]) + ", " + PerkName(perks[2]);
        }

        nextFightButton.SetActive(false);

        perkOneButton.SetActive(false);
        perkTwoButton.SetActive(false);
        perkThreeButton.SetActive(false);

        perkDesc.gameObject.SetActive(false);
        perkDescWhite.gameObject.SetActive(false);

        if (mode == Mode.Client)
        {
            manager.ClientSendPerks();
        }
        if (mode == Mode.Host)
        {
            manager.ServerSendPerks();
        }

        isWaiting = false;
        mainMenuButton.gameObject.SetActive(true);
    }

    public Tetrimino ChooseTetriomino(bool enemy)
    {
        if (!enemy)
        {
            List<Tetrimino> possibleTetriminos = new List<Tetrimino>(tetriminos);
            if (previousBlocks[0] != null)
            {
                possibleTetriminos.Remove(previousBlocks[0]);
            }
            if (previousBlocks[1] != null)
            {
                possibleTetriminos.Remove(previousBlocks[1]);
            }
            if (previousBlocks[2] != null)
            {
                possibleTetriminos.Remove(previousBlocks[2]);
            }

            if (HasPerk(Perks.IMoreLikely))
            {
                possibleTetriminos.Add(tetriminos[0]);
            }
            if (HasPerk(Perks.TMoreLikely))
            {
                possibleTetriminos.Add(tetriminos[5]);
            }

            List<Tetrimino> unaddedTetriminos = new List<Tetrimino>(tetriminos);
            for (int i = 0; i < previousBlocks.Length; i++)
            {
                if (previousBlocks[i] != null)
                {
                    if (unaddedTetriminos.Contains(previousBlocks[i]))
                    {
                        unaddedTetriminos.Remove(previousBlocks[i]);
                    }
                }
            }
            List<Tetrimino> unaddedLongTetriminos = new List<Tetrimino>(tetriminos);
            for (int i = 0; i < previousLongBlocks.Length; i++)
            {
                if (previousLongBlocks[i] != null)
                {
                    if (unaddedLongTetriminos.Contains(previousLongBlocks[i]))
                    {
                        unaddedLongTetriminos.Remove(previousLongBlocks[i]);
                    }
                }
            }
            for (int i = 0; i < unaddedTetriminos.Count; i++)
            {
                possibleTetriminos.Add(unaddedTetriminos[i]);
                possibleTetriminos.Add(unaddedTetriminos[i]);
            }
            for (int i = 0; i < unaddedLongTetriminos.Count; i++)
            {
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
            }

            Tetrimino chosen = (Input.GetKey(KeyCode.I) && cheats) ? tetriminos[0] : possibleTetriminos[Random.Range(0, possibleTetriminos.Count)];
            for (int i = previousBlocks.Length - 1; i > 0; i--)
            {
                previousBlocks[i] = previousBlocks[i - 1];
            }
            previousBlocks[0] = chosen;
            for (int i = previousLongBlocks.Length - 1; i > 0; i--)
            {
                previousLongBlocks[i] = previousLongBlocks[i - 1];
            }
            previousLongBlocks[0] = chosen;
            return chosen;
        }
        else
        {
            List<Tetrimino> possibleTetriminos = new List<Tetrimino>(tetriminos);
            if (previousBlocksEnemy[0] != null)
            {
                possibleTetriminos.Remove(previousBlocksEnemy[0]);
            }
            if (previousBlocksEnemy[1] != null)
            {
                possibleTetriminos.Remove(previousBlocksEnemy[1]);
            }
            if (previousBlocksEnemy[2] != null)
            {
                possibleTetriminos.Remove(previousBlocksEnemy[2]);
            }

            if (previousBlocksEnemy[0] != tetriminos[0] && previousBlocksEnemy[1] != tetriminos[0])
            {
                possibleTetriminos.Add(tetriminos[0]);
            }
            List<Tetrimino> unaddedTetriminos = new List<Tetrimino>(tetriminos);
            for (int i = 0; i < previousBlocksEnemy.Length; i++)
            {
                if (previousBlocksEnemy[i] != null)
                {
                    if (unaddedTetriminos.Contains(previousBlocksEnemy[i]))
                    {
                        unaddedTetriminos.Remove(previousBlocksEnemy[i]);
                    }
                }
            }
            List<Tetrimino> unaddedLongTetriminos = new List<Tetrimino>(tetriminos);
            for (int i = 0; i < previousLongBlocksEnemy.Length; i++)
            {
                if (previousLongBlocksEnemy[i] != null)
                {
                    if (unaddedLongTetriminos.Contains(previousLongBlocksEnemy[i]))
                    {
                        unaddedLongTetriminos.Remove(previousLongBlocksEnemy[i]);
                    }
                }
            }
            for (int i = 0; i < unaddedTetriminos.Count; i++)
            {
                possibleTetriminos.Add(unaddedTetriminos[i]);
                possibleTetriminos.Add(unaddedTetriminos[i]);
            }
            for (int i = 0; i < unaddedLongTetriminos.Count; i++)
            {
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
                possibleTetriminos.Add(unaddedLongTetriminos[i]);
            }

            Tetrimino chosen = (Input.GetKey(KeyCode.I) && cheats) ? tetriminos[0] : possibleTetriminos[Random.Range(0, possibleTetriminos.Count)];
            for (int i = previousBlocksEnemy.Length - 1; i > 0; i--)
            {
                previousBlocksEnemy[i] = previousBlocksEnemy[i - 1];
            }
            previousBlocksEnemy[0] = chosen;
            for (int i = previousLongBlocksEnemy.Length - 1; i > 0; i--)
            {
                previousLongBlocksEnemy[i] = previousLongBlocksEnemy[i - 1];
            }
            previousLongBlocksEnemy[0] = chosen;
            return chosen;
        }
    }

    public bool HasPerk(Perks checkingPerk)
    {
        return perks[0] == checkingPerk || perks[1] == checkingPerk || perks[2] == checkingPerk;
    }

    public bool EnemyHasPerk(Perks checkingPerk)
    {
        return enemyPerks[0] == checkingPerk || enemyPerks[1] == checkingPerk || enemyPerks[2] == checkingPerk;
    }

    public void SetLinesOne()
    {
        lines = 1;
    }

    public IEnumerator DoSabotage()
    {
        for (int i = 0; i < (mode == Mode.Client ? 2 : 4); i++)
        {
            for (int risingY = height - 2; risingY >= 0; risingY--)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, risingY + 1] = grid[x, risingY];
                    if (grid[x, risingY + 1] != null)
                    {
                        grid[x, risingY].y++;
                        grid[x, risingY].transform.localPosition = new Vector3(x, risingY + 1);
                    }
                }
            }

            hole = Random.Range(0, width);

            for (int x = 0; x < width; x++)
            {
                if (x != hole)
                {
                    grid[x, 0] = Instantiate(garbageTetrisPiece, playerBlockHolder);
                    grid[x, 0].transform.localPosition = new Vector3(x, 0, 0);
                }
                else
                {
                    grid[x, 0] = null;
                }
            }

            if (curTetrimino)
            {
                curTetrimino.board = this;
                curTetrimino.UpdateFallDown();
            }

            yield return new WaitForSeconds(0.4f);
            SendGameState();
        }
    }

    public IEnumerator DoSelfHarm()
    {
        for (int i = 0; i < (mode == Mode.Client ? 3 : 6); i++)
        {
            for (int risingY = height - 2; risingY >= 0; risingY--)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, risingY + 1] = grid[x, risingY];
                    if (grid[x, risingY + 1] != null)
                    {
                        grid[x, risingY].y++;
                        grid[x, risingY].transform.localPosition = new Vector3(x, risingY + 1);
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                if (x != hole)
                {
                    grid[x, 0] = Instantiate(garbageTetrisPiece, playerBlockHolder);
                    grid[x, 0].transform.localPosition = new Vector3(x, 0, 0);
                }
                else
                {
                    grid[x, 0] = null;
                }
            }

            if (curTetrimino)
            {
                curTetrimino.board = this;
                curTetrimino.UpdateFallDown();
            }

            yield return new WaitForSeconds(0.4f);
            SendGameState();
        }

        if (EnemyHasPerk(Perks.EnemyStartThreeDam))
        {
            StartCoroutine(DoSabotage());
        }
    }

    public IEnumerator NewPiece(bool rehold)
    {
        if (!stopped && !won)
        {
            canHold = false;
            if (rehold)
            {
                if (LineClear())
                {
                    if (garbagePrep)
                    {
                        float speedUp = 0;
                        while (GetAttacked > 0)
                        {
                            garbagePrep = false;
                            garbageText.color = Color.white;

                            for (int x = 0; x < width; x++)
                            {
                                if (grid[x, height - 1] != null)
                                {
                                    StartCoroutine(grid[x, height - 1].LineClear());
                                }
                            }

                            for (int risingY = height - 2; risingY >= 0; risingY--)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    grid[x, risingY + 1] = grid[x, risingY];
                                    if (grid[x, risingY + 1] != null)
                                    {
                                        grid[x, risingY].y++;
                                        grid[x, risingY].transform.localPosition = new Vector3(x, risingY + 1);
                                    }
                                }
                            }

                            if (HasPerk(Perks.DamageIsNicer) && !EnemyHasPerk(Perks.EnemyDamageIsMessy))
                            {
                                if (Random.value < 0.15f)
                                {
                                    hole = Random.Range(0, width);
                                }
                            }
                            else if (!HasPerk(Perks.DamageIsNicer) && EnemyHasPerk(Perks.EnemyDamageIsMessy))
                            {
                                if (Random.value < 0.7f)
                                {
                                    hole = Random.Range(0, width);
                                }
                            }
                            else
                            {
                                if (Random.value < 0.4f)
                                {
                                    hole = Random.Range(0, width);
                                }
                            }
                            for (int x = 0; x < width; x++)
                            {
                                if (x != hole)
                                {
                                    grid[x, 0] = Instantiate(garbageTetrisPiece, playerBlockHolder);
                                    grid[x, 0].transform.localPosition = new Vector3(x, 0, 0);
                                }
                                else
                                {
                                    grid[x, 0] = null;
                                }
                            }

                            GetAttacked--;
                            yield return new WaitForSeconds(0.4f - speedUp);
                            speedUp += 0.05f;
                            if (speedUp > 0.35f)
                            {
                                speedUp = 0.35f;
                            }
                            SendGameState();
                        }
                    }
                    else
                    {
                        if (GetAttacked > 0)
                        {
                            garbagePrep = true;
                            garbageText.color = Color.red;
                        }
                    }
                }
                else
                {
                    garbagePrep = false;
                    garbageText.color = Color.white;
                }
            }

            Tetrimino old = curTetrimino;

            curTetrimino = previewTetrimino;
            previewTetrimino = Instantiate(ChooseTetriomino(false), playerBlockHolder);
            previewTetrimino.transform.position = preview.transform.position - new Vector3(previewTetrimino.offset ? 0.5f : 0, 0, 0);

            yield return 0;

            if (!isWaiting && mode == Mode.Offline && offlineMode == OfflinePlayMode.CleanUp)
            {
                int[] heights = new int[width];
                for (int xT = 0; xT < width; xT++)
                {
                    for (int yT = height - 1; yT >= 0; yT--)
                    {
                        if (yT == 0 || grid[xT, yT] != null)
                        {
                            heights[xT] = yT;
                            break;
                        }
                    }
                }

                //Ignore Lowest
                int highestVal = heights[0];
                int highestIndex = 0;
                for (int i = 1; i < GameBoard.width; i++)
                {
                    if (heights[i] > highestVal)
                    {
                        highestIndex = i;
                        highestVal = heights[i];
                    }
                }

                if (highestVal <= 1)
                {
                    mainMenuButton.gameObject.SetActive(false);
                    levelSelectButton.gameObject.SetActive(true);

                    Win();
                    isWaiting = true;
                    if (!fastMode)
                    {
                        SaveLoadStats.normalCleanUp[cleanUp.level - 1] = true;
                        SaveLoadStats.Save();
                    }
                    else
                    {
                        SaveLoadStats.hardCleanUp[cleanUp.level - 1] = true;
                        SaveLoadStats.Save();
                    }
                }
            }

            curTetrimino.Create(this, false);
            canHold = rehold;
            if (old != null)
            {
                Destroy(old.gameObject);

                holdText.color = canHold ? Color.white : new Color(0.45f, 0.45f, 0.45f);
                holdBG.color = canHold ? new Color(0.22f, 0.34f, 0.53f) : new Color(0.13f, 0.20f, 0.32f);
            }
            else
            {
                canHold = true;
                holdText.color = Color.white;
                holdBG.color = new Color(0.22f, 0.34f, 0.53f);
            }
        }
        SendGameState();
    }

    Color[] ConvertGridToColor()
    {
        Color[] newGrid = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                newGrid[x + y * width] = (grid[x, y] != null) ? grid[x,y].GetComponent<SpriteRenderer>().color : Color.black;
            }
        }
        return newGrid;
    }

    bool LineClear()
    {
        int newLines = 0;
        bool boardCleared = true;
        for (int y = 0; y < height; y++)
        {
            bool failedLine = false;
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] == null)
                {
                    failedLine = true;
                }
            }

            if (!failedLine)
            {
                newLines++;
                for (int x = 0; x < width; x++)
                {
                    StartCoroutine(grid[x, y].LineClear());
                }
                for (int fallingY = y; fallingY < height - 1; fallingY++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        grid[x, fallingY] = grid[x, fallingY + 1];
                        if (grid[x, fallingY] != null)
                        {
                            grid[x, fallingY].y--;
                            grid[x, fallingY].transform.localPosition = new Vector3(x, fallingY);
                        }
                    }
                }
                for (int x = 0; x < width; x++)
                {
                    grid[x, height - 1] = null;
                }
                y--;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x,y] != null)
                {
                    boardCleared = false;
                    break;
                }
            }
        }

        LinesCleared(newLines, boardCleared);
        return (newLines == 0);
    }

    public int Multi(int val)
    {
        if (multi == Multiplier.OneX)
        {
            return val;
        }
        if (multi == Multiplier.TwoX)
        {
            return val * 2;
        }
        if (multi == Multiplier.ThreeX)
        {
            return val * 3;
        }
        if (multi == Multiplier.PointFiveRoundUp)
        {
            return Mathf.CeilToInt(val / 2f);
        }
        if (multi == Multiplier.PointFiveRoundDown)
        {
            return Mathf.FloorToInt(val / 2f);
        }
        if (multi == Multiplier.OnePointFiveRoundUp)
        {
            return Mathf.CeilToInt(val * 1.5f);
        }
        return Mathf.FloorToInt(val * 1.5f);
    }

    void LinesCleared(int linesCleared, bool boardCleared)
    {
        lines += linesCleared;

        fastFallSpeed = GetFastFallSpeed();
        maxFallTimer = GetSlowMaxFallTimer();

        if (linesCleared > 0)
        {
            combo++;

            if (mods != Modifications.LinesOnly && mods != Modifications.TSpinsOnly && mods != Modifications.NoCombos)
            {
                if (GetComboBonus(combo) == 1)
                {
                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, combo + "x Combo", false));
                }
                else if (GetComboBonus(combo) == 2)
                {
                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectMid(comboWhite, comboBlack, combo + "x Combo", false));
                }
                else if (GetComboBonus(combo) == 3)
                {
                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectHigh(comboWhite, comboBlack, combo + "x Combo", false));
                }
                else if (GetComboBonus(combo) >= 4)
                {
                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectInsane(comboWhite, comboBlack, combo + "x Combo", false));
                }
            }

            if (wasLastMoveSpin && mods != Modifications.LinesOnly && mods != Modifications.CombosOnly && mods != Modifications.NoTSpins)
            {
                if (linesCleared == 1)
                {
                    score += 30;
                    currentCharge += 1;
                    if (!boardCleared)
                    {
                        Stop(topCo);
                        topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "T-Spin Single", false));
                    }
                }
                else if (linesCleared == 2)
                {
                    score += 250;
                    currentCharge += 3;
                    if (!boardCleared)
                    {
                        Stop(topCo);
                        topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "T-Spin Double!", false));
                    }
                }
                else
                {
                    score += 650;
                    currentCharge += 5;
                    if (!boardCleared)
                    {
                        Stop(topCo);
                        topCo = StartCoroutine(EffectInsane(effectWhite, effectBlack, "T-Spin Triple!!!", false));
                    }
                }
            }
            else if (mods != Modifications.TSpinsOnly && mods != Modifications.CombosOnly && mods != Modifications.NoLines)
            {
                if (linesCleared == 1)
                {
                    score += 10;
                }
                else if (linesCleared == 2)
                {
                    score += 25;
                    currentCharge++;
                    if (!boardCleared)
                    {
                        Stop(topCo);
                        topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, "Double", false));
                    }
                }
                else if (linesCleared == 3)
                {
                    score += 60;
                    currentCharge += 2;
                    if (!boardCleared)
                    {
                        Stop(topCo);
                        topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, "Triple!", false));
                    }
                }
                else
                {
                    score += 250;
                    currentCharge += 3;
                    if (!boardCleared)
                    {
                        Stop(topCo);
                        topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, "TETRIS!", false));
                    }
                }
            }

            if (boardCleared)
            {
                score += 1000;
                currentCharge += 6;
                Stop(topCo);
                topCo = StartCoroutine(EffectInsane(effectWhite, effectBlack, "BOARD CLEAR!!!", false));
            }

            if (Multi(currentCharge) > 0)
            {
                currentDamage.enabled = true;
                currentDamage.text = Multi(currentCharge + GetComboBonus(combo)).ToString();
            }
            else
            {
                currentDamage.enabled = false;
            }

            if (lines / 7f >= twelveCount)
            {
                twelveCount++;
                if (HasPerk(Perks.OneDamEvy8Lines))
                {
                    currentCharge++;

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "7 L -> 1 Damage", false));
                }
            }
            if (lines / 14f >= sixteenCount)
            {
                sixteenCount++;
                if (HasPerk(Perks.TwoDamEvy13Lines))
                {
                    currentCharge += 2;

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "14 L -> 2 Damage!", false));
                }
            }
            if (lines / 21f >= twentyCount)
            {
                twentyCount++;
                if (HasPerk(Perks.ThreeDamEvy17Lines))
                {
                    currentCharge += 3;

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectMid(comboWhite, comboBlack, "21 L -> 3 Damage!", false));
                }
            }

            if (lines / 6f >= sevenCount)
            {
                sevenCount++;

                if (HasPerk(Perks.THoldEvy6Lines))
                {
                    if (holdTetrimino)
                        Destroy(holdTetrimino.gameObject);
                    holdTetrimino = Instantiate(tetriminos[5], playerBlockHolder);
                    holdTetrimino.transform.position = hold.transform.position - new Vector3(holdTetrimino.offset ? 0.5f : 0, 0, 0);

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "6 L -> T Piece Held", false));
                }
            }

            if (score / 400f >= sixHundredCount)
            {
                sixHundredCount++;
                if (HasPerk(Perks.OneDamEvy600Points))
                {
                    currentCharge++;

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "400 S -> 1 Damage", false));
                }
            }
            if (score / 800f >= oneThousandCount)
            {
                oneThousandCount++;
                if (HasPerk(Perks.TwoDamEvy1000Points))
                {
                    currentCharge += 2;

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "800 S -> 2 Damage!", false));
                }
            }
            if (score / 1200f >= oneThousandFourHundredCount)
            {
                oneThousandFourHundredCount++;
                if (HasPerk(Perks.ThreeDamEvy1400Points))
                {
                    currentCharge += 3;

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectMid(comboWhite, comboBlack, "1200 S -> 3 Damage!", false));
                }
            }

            if (score / 300f >= fourHundredCount)
            {
                fourHundredCount++;
                if (HasPerk(Perks.LineHoldEvy400Points))
                {

                    if (holdTetrimino)
                        Destroy(holdTetrimino.gameObject);
                    holdTetrimino = Instantiate(tetriminos[0], playerBlockHolder);
                    holdTetrimino.transform.position = hold.transform.position - new Vector3(holdTetrimino.offset ? 0.5f : 0, 0, 0);

                    Stop(bottomCo);
                    bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "300 -> I Piece Held", false));
                }
            }
        }
        else
        {
            score += Pos(combo - 1) * Pos(combo - 1) * 10;
            currentCharge += GetComboBonus(combo);
            currentCharge = Multi(currentCharge);

            if (currentCharge == 1 && HasPerk(Perks.EqualOneIsTwo))
            {
                currentCharge++;
            }
            if (currentCharge >= 4 && HasPerk(Perks.AboveFivePlusOne))
            {
                currentCharge++;
            }

            attacks += currentCharge;
            int damageDone = currentCharge;

            if (GetAttacked < 0)
            {
                if (currentCharge != 0)
                {
                    if (mode == Mode.Host)
                    {
                        manager.ServerSendDamage(currentCharge);
                    }
                    else if (mode == Mode.Client)
                    {
                        manager.ClientSendDamage(currentCharge);
                    }
                    else if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                    {
                        EnemyGetAttacked += currentCharge;
                    }
                }
            }
            else
            {
                GetAttacked -= currentCharge;
                if (GetAttacked < 0)
                {
                    damageDone = -GetAttacked;
                    if (mode == Mode.Host)
                    {
                        manager.ServerSendDamage(-GetAttacked);
                    }
                    else if (mode == Mode.Client)
                    {
                        manager.ClientSendDamage(-GetAttacked);
                    }
                    else if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Freeplay && offlineMode != OfflinePlayMode.CleanUp)
                    {
                        EnemyGetAttacked -= GetAttacked;
                    }
                    GetAttacked = 0;
                }
            }

            if (damageDone > 0 && damageDone <= 2)
            {
                Stop(topCo);
                topCo = StartCoroutine(EffectLow(effectWhite, effectBlack, damageDone + "x Damage", false));

                if (!hasDamageExplained && wasTutorial)
                {
                    Invoke("ActivateDamage", 2.5f);
                }
            }
            else if (damageDone > 2 && damageDone <= 5)
            {
                Stop(topCo);
                topCo = StartCoroutine(EffectMid(effectWhite, effectBlack, damageDone + "x Damage", false));

                if (!hasDamageExplained && wasTutorial)
                {
                    Invoke("ActivateDamage", 3f);
                }
            }
            else if (damageDone > 5 && damageDone <= 7)
            {
                Stop(topCo);
                topCo = StartCoroutine(EffectHigh(effectWhite, effectBlack, damageDone + "x Damage!", false));

                if (!hasDamageExplained && wasTutorial)
                {
                    Invoke("ActivateDamage", 3.5f);
                }
            }
            else if (damageDone > 7)
            {
                Stop(topCo);
                topCo = StartCoroutine(EffectInsane(effectWhite, effectBlack, damageDone + "x Damage!!!", false));

                if (!hasDamageExplained && wasTutorial)
                {
                    Invoke("ActivateDamage", 4.5f);
                }
            }

            currentCharge = 0;
            currentDamage.enabled = false;
            combo = 0;
        }

        lineText.text = lines.ToString();
        scoreText.text = score.ToString();
        attackText.text = attacks.ToString();
    }

    void Stop(Coroutine co)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
    }

    private void OnApplicationQuit()
    {
        if (!isAbsoluteQuit)
        {
            Application.CancelQuit();
            if (mode == Mode.Client)
            {
                manager.ClientSendWin(score);
            }
            else if (mode == Mode.Host)
            {
                manager.ServerSendWin(score);
            }
            StartCoroutine(ReQuit());
        }
    }

    IEnumerator ReQuit()
    {
        yield return new WaitForSeconds(0.1f);
        isAbsoluteQuit = true;
        Application.Quit();
    }

    public int EnemyComboBonus(int amount)
    {
        return Pos(Mathf.CeilToInt((amount - 2) / 2f));
    }

    public int GetComboBonus(int amount)
    {
        if (HasPerk(Perks.ComboStartEarlier) && HasPerk(Perks.ComboAbove5PlusTwo))
        {
            if (amount >= 4)
            {
                return Pos(Mathf.CeilToInt((amount - 1) / 2f)) + 1;
            }
            return Pos(Mathf.CeilToInt((amount - 1) / 2f));
        }
        if (HasPerk(Perks.ComboStartEarlier) && !HasPerk(Perks.ComboAbove5PlusTwo))
        {
            return Pos(Mathf.CeilToInt((amount - 1) / 2f));
        }
        if (!HasPerk(Perks.ComboStartEarlier) && HasPerk(Perks.ComboAbove5PlusTwo))
        {
            if (amount >= 4)
            {
                return Pos(Mathf.CeilToInt((amount - 2) / 2f)) + 1;
            }
        }
        return Pos(Mathf.CeilToInt((amount - 2) / 2f));
    }

    IEnumerator EffectLow(Text white, Text black, string msg, bool bad)
    {
        float fade = 0.5f;

        white.gameObject.SetActive(true);
        black.gameObject.SetActive(true);

        black.transform.localScale = new Vector3(1, 1, 1);

        white.text = msg;
        black.text = msg;

        black.transform.rotation = Quaternion.Euler(0, 0, 0);

        white.color = bad ? new Color(1, 0, 0, fade) : new Color(1, 1, 1, fade);
        black.color = new Color(0, 0, 0, fade);

        yield return new WaitForSeconds(1.5f);

        while (fade > 0)
        {
            white.color = bad ? new Color(1, 0, 0, fade) : new Color(1, 1, 1, fade);
            black.color = new Color(0, 0, 0, fade);

            fade -= Time.deltaTime;
            yield return 0;
        }

        white.gameObject.SetActive(false);
        black.gameObject.SetActive(false);
    }

    IEnumerator EffectMid(Text white, Text black, string msg, bool bad)
    {
        float fade = 0.5f;
        float time = 2f;

        white.gameObject.SetActive(true);
        black.gameObject.SetActive(true);

        black.transform.localScale = new Vector3(1.1f, 1.1f, 1);

        white.text = msg;
        black.text = msg;

        white.color = bad ? new Color(1, 0, 0, fade * 2) : new Color(1, 1, 1, fade * 2);
        black.color = new Color(0, 0, 0, fade);

        while (time > 0)
        {
            float rot = Random.Range(-2f, 2f);

            black.transform.rotation = Quaternion.Euler(0, 0, rot);

            time -= Time.deltaTime;
            yield return 0;
        }

        while (fade > 0)
        {
            float rot = Random.Range(-2f, 2f);

            black.transform.rotation = Quaternion.Euler(0, 0, rot);

            white.color = bad ? new Color(1, 0, 0, fade * 2) : new Color(1, 1, 1, fade * 2);
            black.color = new Color(0, 0, 0, fade);

            fade -= Time.deltaTime;
            yield return 0;
        }

        white.gameObject.SetActive(false);
        black.gameObject.SetActive(false);
    }

    IEnumerator EffectMidNoShake(Text white, Text black, string msg)
    {
        float fade = 0.5f;
        float time = 2f;

        white.gameObject.SetActive(true);
        black.gameObject.SetActive(true);

        black.transform.localScale = new Vector3(1.1f, 1.1f, 1);

        black.transform.rotation = Quaternion.Euler(0, 0, 0);
        white.transform.rotation = Quaternion.Euler(0, 0, 0);

        white.text = msg;
        black.text = msg;

        white.color = new Color(1, 1, 1, fade * 2);
        black.color = new Color(0, 0, 0, fade);

        while (time > 0)
        {
            float rot = Random.Range(-2f, 2f);

            time -= Time.deltaTime;
            yield return 0;
        }

        while (fade > 0)
        {
            float rot = Random.Range(-2f, 2f);

            white.color = new Color(1, 1, 1, fade * 2);
            black.color = new Color(0, 0, 0, fade);

            fade -= Time.deltaTime;
            yield return 0;
        }

        white.gameObject.SetActive(false);
        black.gameObject.SetActive(false);
    }

    IEnumerator EffectHigh(Text white, Text black, string msg, bool bad)
    {
        float fade = 0.5f;
        float time = 2.5f;

        white.gameObject.SetActive(true);
        black.gameObject.SetActive(true);

        black.transform.localScale = new Vector3(1.4f, 1.4f, 1);

        white.text = msg;
        black.text = msg;

        white.color = bad ? new Color(1, 0, 0, fade * 2) : new Color(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), fade * 2);
        black.color = new Color(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), fade);

        while (time > 0)
        {
            float rot = Random.Range(-5f, 5f);

            black.transform.rotation = Quaternion.Euler(0, 0, rot);

            white.color = bad ? new Color(1, 0, 0, fade * 2) : new Color(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), fade * 2);
            black.color = new Color(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), fade);

            time -= Time.deltaTime;
            yield return 0;
        }

        while (fade > 0)
        {
            float rot = Random.Range(-5f, 5f);

            black.transform.rotation = Quaternion.Euler(0, 0, rot);

            white.color = bad ? new Color(1, 0, 0, fade * 2) : new Color(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), fade * 2);
            black.color = new Color(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), fade);

            fade -= Time.deltaTime;
            yield return 0;
        }

        white.gameObject.SetActive(false);
        black.gameObject.SetActive(false);
    }

    IEnumerator EffectInsane(Text white, Text black, string msg, bool bad)
    {
        float fade = 1f;
        float time = 3f;

        white.gameObject.SetActive(true);
        black.gameObject.SetActive(true);

        black.transform.localScale = new Vector3(2, 2, 1);

        white.text = msg;
        black.text = msg;

        white.color = bad ? new Color(1, 0, 0, fade) : new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), fade);
        black.color = new Color(Random.Range(0f, 0.4f), Random.Range(0f, 0.4f), Random.Range(0f, 0.4f), fade);

        while (time > 0)
        {
            float rot = Random.Range(-12f, 12f);

            black.transform.rotation = Quaternion.Euler(0, 0, rot);

            white.color = bad ? new Color(1, 0, 0, fade) : new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), fade);
            black.color = new Color(Random.Range(0f, 0.4f), Random.Range(0f, 0.4f), Random.Range(0f, 0.4f), fade);

            time -= Time.deltaTime;
            yield return 0;
        }

        while (fade > 0)
        {
            float rot = Random.Range(-12f, 12f);

            black.transform.rotation = Quaternion.Euler(0, 0, rot);

            white.color = bad ? new Color(1,0,0,fade) : new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), fade);
            black.color = new Color(Random.Range(0f, 0.4f), Random.Range(0f, 0.4f), Random.Range(0f, 0.4f), fade);

            fade -= Time.deltaTime;
            yield return 0;
        }

        white.gameObject.SetActive(false);
        black.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (damage == 1 && HasPerk(Perks.EqualOneIsZero))
        {
            return;
        }
        if (damage >= 4 && HasPerk(Perks.AboveFiveMinusOne))
        {
            damage--;
        }

        if (damage < 0)
        {
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectMid(comboWhite, comboBlack, "Healed " + (-damage) + "x", false));
        }
        else if (damage >= 0 && damage < 2)
        {
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectLow(comboWhite, comboBlack, "Hurt " + damage + "x", true));
        }
        else if (damage >= 2 && damage < 5)
        {
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectMid(comboWhite, comboBlack, "Hurt " + damage + "x", true));
        }
        else if (damage >= 5 && damage <= 7)
        {
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectHigh(comboWhite, comboBlack, "Hurt " + damage + "x!", true));
        }
        else if (damage > 7)
        {
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectInsane(comboWhite, comboBlack, "Hurt " + damage + "x!!!", true));
        }
        GetAttacked += damage;
    }

    IEnumerator NextFightTime()
    {
        yield return new WaitForSeconds(4f);
        if (fightNumber != 4)
        {
            RandoPerks();
            UpdatePerks();
            nextFightButton.SetActive(true);
        }
    }

    public int GetWinIndex()
    {
        if (onlinePlayMode == OnlinePlayMode.Regular)
        {
            return 4;
        }
        else if (onlinePlayMode == OnlinePlayMode.OnePerk)
        {
            return 3;
        }
        else if (onlinePlayMode == OnlinePlayMode.TwoPerks)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public void Win()
    {
        if (mode == Mode.Offline && offlineMode == OfflinePlayMode.Circuit)
        {
            fightNumber++;
            if (fightNumber == 4)
            {
                Stop(topCo);
                topCo = StartCoroutine(EffectInsane(effectWhite, effectBlack, "Win! Win! Win!", false));
                Stop(bottomCo);
                bottomCo = StartCoroutine(EffectInsane(comboWhite, comboBlack, "BEAT THE CIRCUIT!", false));
                won = true;
                if (!cheats)
                {
                    if (!fastMode)
                    {
                        SaveLoadStats.normalCircuits[curCircuit.circuitIndex] = true;
                        SaveLoadStats.Save();
                    }
                    else
                    {
                        SaveLoadStats.hardCircuits[curCircuit.circuitIndex] = true;
                        SaveLoadStats.Save();
                    }
                }
            }
            else
            {
                mainMenuButton.SetActive(false);
                resetButton.SetActive(false);
                pauseButton.SetActive(false);

                Stop(topCo);
                topCo = StartCoroutine(EffectInsane(effectWhite, effectBlack, "Win!", false));
                Stop(bottomCo);
                if (fightNumber == 3)
                    bottomCo = StartCoroutine(EffectHigh(comboWhite, comboBlack, "Next fight! Boss!", false));
                else
                    bottomCo = StartCoroutine(EffectHigh(comboWhite, comboBlack, "Next fight! #" + (fightNumber + 1), false));
                won = true;
            }
            StartCoroutine(NextFightTime());
        }
        else
        {
            Stop(topCo);
            topCo = StartCoroutine(EffectInsane(effectWhite, effectBlack, "Win!", false));
            Stop(bottomCo);
            bottomCo = StartCoroutine(EffectHigh(comboWhite, comboBlack, "Number 1!", false));
            won = true;
        }

        if (mode == Mode.Offline && offlineMode == OfflinePlayMode.VersusAI && !cheats)
        {
            if (!fastMode)
            {
                if (SaveLoadStats.normalAi[ai.brain.aiIndex] < GetWinIndex())
                {
                    SaveLoadStats.normalAi[ai.brain.aiIndex] = GetWinIndex();
                    SaveLoadStats.Save();
                }
            }
            else
            {
                if (SaveLoadStats.hardAi[ai.brain.aiIndex] < GetWinIndex())
                {
                    SaveLoadStats.hardAi[ai.brain.aiIndex] = GetWinIndex();
                    SaveLoadStats.Save();
                }
            }
        }

        GetAttacked = 0;
        EnemyGetAttacked = 0;

        if (curTetrimino)
        {
            curTetrimino.preview = true;
        }
        if (mode == Mode.Offline && offlineMode != OfflinePlayMode.Circuit)
        {
            resetButton.SetActive(true);
        }
    }

    public void UpdateGameState(bool enemyRed, int enemyDamage, Color[] newGrid)
    {
        enemyGarbageText.color = enemyRed ? Color.red : Color.white;
        EnemyGetAttacked = enemyDamage;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (newGrid[x + y * width] != Color.black)
                {
                    if (enemyGrid[x, y] != null)
                    {
                        enemyGrid[x, y].GetComponent<SpriteRenderer>().color = newGrid[x + y * width];
                    }
                    else
                    {
                        enemyGrid[x, y] = Instantiate(garbageTetrisPiece, enemyBlockHolder);
                        enemyGrid[x, y].transform.localPosition = new Vector3(x, y, 0);
                        enemyGrid[x, y].GetComponent<SpriteRenderer>().color = newGrid[x + y * width];
                    }
                }
                else
                {
                    if (enemyGrid[x, y] != null)
                    {
                        enemyGrid[x, y].GetComponent<SpriteRenderer>().enabled = false;
                        Destroy(enemyGrid[x, y].gameObject);
                        enemyGrid[x, y] = null;
                    }
                }
            }
        }
    }

    void RandoPerks()
    {
        chosenPerkOne = (Perks)Random.Range(1, 31);
        chosenPerkTwo = (Perks)Random.Range(1, 31);
        chosenPerkThree = (Perks)Random.Range(1, 31);

        while (chosenPerkOne == perks[0] || chosenPerkOne == perks[1] || chosenPerkOne == perks[2] || (mode != Mode.Offline && chosenPerkOne == Perks.EnemyMovesSlower) || (mode == Mode.Offline && chosenPerkOne == Perks.Meme) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkOne == Perks.Meme) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkOne == Perks.EnemyStartThreeDam) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkOne == Perks.YouStartFourDam))
        {
            chosenPerkOne = (Perks)Random.Range(1, 31);
        }

        while (chosenPerkTwo == perks[0] || chosenPerkTwo == perks[1] || chosenPerkTwo == perks[2] || chosenPerkTwo == chosenPerkOne || (mode != Mode.Offline && chosenPerkTwo == Perks.EnemyMovesSlower) || (mode == Mode.Offline && chosenPerkTwo == Perks.Meme) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkTwo == Perks.Meme) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkTwo == Perks.EnemyStartThreeDam) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkTwo == Perks.YouStartFourDam))
        {
            chosenPerkTwo = (Perks)Random.Range(1, 31);
        }

        while (chosenPerkThree == perks[0] || chosenPerkThree == perks[1] || chosenPerkThree == perks[2] || chosenPerkThree == chosenPerkOne || chosenPerkThree == chosenPerkTwo || (mode != Mode.Offline && chosenPerkThree == Perks.EnemyMovesSlower) || (mode == Mode.Offline && chosenPerkThree == Perks.Meme) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkThree == Perks.Meme) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkThree == Perks.EnemyStartThreeDam) || (onlinePlayMode == OnlinePlayMode.Dominance && chosenPerkThree == Perks.YouStartFourDam))
        {
            chosenPerkThree = (Perks)Random.Range(1, 31);
        }
    }

    public void ChoosePerkButton(int val)
    {
        chosenPerk = val;
        UpdatePerks();
    }

    void UpdatePerks()
    {
        perkOneButton.SetActive(true);
        perkTwoButton.SetActive(true);
        perkThreeButton.SetActive(true);

        perkDesc.gameObject.SetActive(true);
        perkDescWhite.gameObject.SetActive(true);

        perkOneButton.transform.GetChild(0).GetComponent<Text>().text = PerkName(chosenPerkOne);
        perkTwoButton.transform.GetChild(0).GetComponent<Text>().text = PerkName(chosenPerkTwo);
        perkThreeButton.transform.GetChild(0).GetComponent<Text>().text = PerkName(chosenPerkThree);

        if (chosenPerk == 1)
        {
            perkDesc.text = PerkDescription(chosenPerkOne);
            perkDescWhite.text = PerkDescription(chosenPerkOne);

            perkOneButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.9f);
            perkTwoButton.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f, 0.9f);
            perkThreeButton.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f, 0.9f);
        }
        else if(chosenPerk == 2)
        {
            perkDesc.text = PerkDescription(chosenPerkTwo);
            perkDescWhite.text = PerkDescription(chosenPerkTwo);

            perkOneButton.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f, 0.9f);
            perkTwoButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.9f);
            perkThreeButton.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f, 0.9f);
        }
        else
        {
            perkDesc.text = PerkDescription(chosenPerkThree);
            perkDescWhite.text = PerkDescription(chosenPerkThree);

            perkOneButton.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f, 0.9f);
            perkTwoButton.GetComponent<Image>().color = new Color(0.705f, 0.705f, 0.705f, 0.9f);
            perkThreeButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.9f);
        }
    }


    public void UpdateEnemyCurrent(Color col, int[] xPos, int[] yPos)
    {
        if (col != Color.black)
        {
            for (int i = 0; i < 4; i++)
            {
                enemyCur[i].gameObject.SetActive(true);
                enemyCur[i].GetComponent<SpriteRenderer>().color = col;
                enemyCur[i].transform.localPosition = new Vector3(xPos[i], yPos[i], 1);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                enemyCur[i].gameObject.SetActive(false);
            }
        }
    }

    static int Pos(int val)
    {
        if (val < 0)
        {
            return 0;
        }
        return val;
    }

    void SendGameState()
    {
        if (!won)
        {
            if (mode == Mode.Client)
            {
                manager.ClientSendGameState(garbagePrep, currentlyDefending, ConvertGridToColor());
            }
            else if (mode == Mode.Host)
            {
                manager.ServerSendGameState(garbagePrep, currentlyDefending, ConvertGridToColor());
            }
        }
    }
}
