using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameBoard))]
public class SimuateBoardAI : MonoBehaviour
{
    public GameBoard board;
    public AI brain;
    public bool stop = false;

    public Tetrimino enemyCurTetra;
    private Tetrimino enemyPreviewTetra;
    private Tetrimino enemyHoldTetra;

    private int hole;

    private BoardState plannedState;
    private bool alreadySpun = false;
    private int currentCharge = 0;
    private bool failed = false;
    private int combo = 0;
    private bool garbagePrep = false;
    public bool isWaiting = false;
    private int timesSinceScrew = 4;
    private float screwChance;

    public void Begin ()
    {
        enemyPreviewTetra = Instantiate(board.ChooseTetriomino(true), board.enemyBlockHolder);
        enemyPreviewTetra.gameObject.SetActive(false);
        enemyPreviewTetra.transform.localPosition = new Vector3(4, 19, 0);
        enemyPreviewTetra.preview = true;
        enemyHoldTetra = Instantiate(board.ChooseTetriomino(true), board.enemyBlockHolder);
        enemyHoldTetra.gameObject.SetActive(false);
        enemyHoldTetra.transform.localPosition = new Vector3(4, 19, 0);
        enemyHoldTetra.Create(board, true);
        enemyHoldTetra.preview = true;
        stop = false;
        garbagePrep = false;

        combo = 0;
        currentCharge = 0;

        alreadySpun = false;
        hole = Random.Range(0, GameBoard.width);
        timesSinceScrew = brain.timesToScrew;
        screwChance = brain.screwChance;

        isWaiting = true;
        StartCoroutine(NewPiece());
        StartCoroutine(WaitThree());
    }

    IEnumerator WaitThree()
    {
        while (board.isWaiting)
        {
            yield return 0;
        }
        isWaiting = false;
    }

    public IEnumerator NewPiece()
    {
        StopCoroutine(Move());
        Tetrimino old = enemyCurTetra;

        enemyCurTetra = enemyPreviewTetra;
        if (enemyCurTetra)
        {
            enemyCurTetra.gameObject.SetActive(true);
        }
        enemyPreviewTetra = Instantiate(board.ChooseTetriomino(true), board.enemyBlockHolder);
        enemyPreviewTetra.gameObject.SetActive(false);
        enemyPreviewTetra.transform.localPosition = new Vector3(4, 19, 0);
        enemyPreviewTetra.preview = true;

        yield return 0;

        enemyCurTetra.Create(board, true);
        enemyCurTetra.preview = false;
        if (old != null)
        {
            Destroy(old.gameObject);
        }

        alreadySpun = false;
        failed = false;
        if ((Random.value < screwChance && timesSinceScrew <= 0 && !board.fastMode) || (board.cheats && Input.GetKey(KeyCode.S)))
        {
            timesSinceScrew = brain.timesToScrew;
            screwChance = brain.screwChance;

            plannedState = new BoardState(0, Random.Range(1, 8), Random.Range(0, 4), 0);
        }
        else
        {
            timesSinceScrew--; 
            if (timesSinceScrew <= 0)
            {
                screwChance *= 1.25f;
            }
            plannedState = enemyCurTetra.GetStates(board.enemyGrid, brain, (enemyHoldTetra.name == enemyPreviewTetra.name ? -10 : 0) + (enemyCurTetra.holdingGood ? -20 : 0), false);
            BoardState holdState = enemyHoldTetra.GetStates(board.enemyGrid, brain, (enemyCurTetra.name == enemyPreviewTetra.name ? -10 : 0) + (enemyHoldTetra.holdingGood ? -20 : 0), false);
            if (brain.usesHold && holdState.score > plannedState.score)
            {
                Tetrimino held = enemyHoldTetra;
                enemyHoldTetra = enemyCurTetra;
                enemyHoldTetra.Hold();
                enemyHoldTetra.preview = true;
                enemyHoldTetra.gameObject.SetActive(false);

                enemyCurTetra = held;
                enemyCurTetra.gameObject.SetActive(true);
                enemyCurTetra.Create(board, true);

                plannedState = holdState;
            }
        }
        StartCoroutine(Move());
    }
	
	IEnumerator Move()
    {
        while (isWaiting)
        {
            yield return 0;
        }
        if (!stop)
        {
            for (int i = 0; i < brain.initialThinkMid * Random.Range(0.75f, 1.5f) * (board.fastMode ? 0.6f : 1f) * (board.HasPerk(Perks.EnemyMovesSlower) ? 1.25f : 1); i++)
            {
                yield return 0;
            }
            while (enemyCurTetra != null)
            {
                if (!alreadySpun)
                {
                    alreadySpun = true;
                    for (int i = 0; i < plannedState.rot; i++)
                    {
                        for (int j = 0; j < brain.framesToMove; j++)
                        {
                            yield return 0;
                        }
                        if (enemyCurTetra != null)
                        {
                            if (enemyCurTetra.AttemptRightRotate(0, 0, true)) ;
                            else if (enemyCurTetra.AttemptRightRotate(-1, 0, true)) ;
                            else if (enemyCurTetra.AttemptRightRotate(1, 0, true)) ;
                            else if (enemyCurTetra.AttemptRightRotate(0, -1, true)) ;
                            else if (enemyCurTetra.AttemptRightRotate(-2, 0, true)) ;
                            else if (enemyCurTetra.AttemptRightRotate(2, 0, true)) ;
                        }
                    }
                }
                if (enemyCurTetra != null)
                {
                    if (!enemyCurTetra.IsFastFalling())
                    {
                        if (enemyCurTetra.x < plannedState.x && !failed)
                        {
                            failed = !enemyCurTetra.Move(1, true);
                            while (isWaiting)
                            {
                                yield return 0;
                            }
                        }
                        else if (enemyCurTetra.x > plannedState.x && !failed)
                        {
                            failed = !enemyCurTetra.Move(-1, true);
                            while (isWaiting)
                            {
                                yield return 0;
                            }
                        }
                        else
                        {
                            while (isWaiting)
                            {
                                yield return 0;
                            }
                            for (int i = 0; i < brain.framesToMove * 1.5f * (board.fastMode ? 0.6f : 1f) * (board.HasPerk(Perks.EnemyMovesSlower) ? 1.25f : 1); i++)
                            {
                                yield return 0;
                            }
                            if (plannedState.slowFall == 0)
                            {
                                if (enemyCurTetra)
                                {
                                    while (enemyCurTetra.Fall()) ;
                                }
                            }
                            else
                            {
                                if (enemyCurTetra)
                                {
                                    enemyCurTetra.SetupFastFall(plannedState.slowFall);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < brain.framesToMove * (board.fastMode ? 0.6f : 1f) * (board.HasPerk(Perks.EnemyMovesSlower) ? 1.25f : 1); i++)
                {
                    yield return 0;
                }
            }
            if (LineClear())
            {
                if (garbagePrep)
                {
                    float speedUp = 0;
                    while (board.EnemyGetAttacked > 0)
                    {
                        garbagePrep = false;
                        board.enemyGarbageText.color = Color.white;

                        for (int x = 0; x < GameBoard.width; x++)
                        {
                            if (board.enemyGrid[x, GameBoard.height - 1] != null)
                            {
                                StartCoroutine(board.enemyGrid[x, GameBoard.height - 1].LineClear());
                            }
                        }

                        for (int risingY = GameBoard.height - 2; risingY >= 0; risingY--)
                        {
                            for (int x = 0; x < GameBoard.width; x++)
                            {
                                board.enemyGrid[x, risingY + 1] = board.enemyGrid[x, risingY];
                                if (board.enemyGrid[x, risingY + 1] != null)
                                {
                                    board.enemyGrid[x, risingY].y++;
                                    board.enemyGrid[x, risingY].transform.localPosition = new Vector3(x, risingY + 1);
                                }
                            }
                        }

                        if (board.HasPerk(Perks.EnemyDamageIsMessy) ? Random.value < 0.7f : Random.value < 0.4f)
                        {
                            hole = Random.Range(0, GameBoard.width);
                        }
                        for (int x = 0; x < GameBoard.width; x++)
                        {
                            if (x != hole)
                            {
                                board.enemyGrid[x, 0] = Instantiate(board.garbageTetrisPiece, board.enemyBlockHolder);
                                board.enemyGrid[x, 0].transform.localPosition = new Vector3(x, 0, 0);
                            }
                            else
                            {
                                board.enemyGrid[x, 0] = null;
                            }
                        }

                        board.EnemyGetAttacked--;
                        yield return new WaitForSeconds(0.4f - speedUp);
                        speedUp += 0.05f;
                        if (speedUp > 0.35f)
                        {
                            speedUp = 0.35f;
                        }
                    }
                }
                else
                {
                    if (board.EnemyGetAttacked > 0)
                    {
                        garbagePrep = true;
                        board.enemyGarbageText.color = Color.red;
                    }
                }
            }
            else
            {
                garbagePrep = false;
                board.enemyGarbageText.color = Color.white;
            }
            StartCoroutine(NewPiece());
        }
        else
        {
            print("EE");
            Destroy(enemyCurTetra.gameObject);
            Destroy(enemyPreviewTetra.gameObject);
            Destroy(enemyHoldTetra.gameObject);
        }
	}

    public IEnumerator DoSabotage()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int risingY = GameBoard.height - 2; risingY >= 0; risingY--)
            {
                for (int x = 0; x < GameBoard.width; x++)
                {
                    board.enemyGrid[x, risingY + 1] = board.enemyGrid[x, risingY];
                    if (board.enemyGrid[x, risingY + 1] != null)
                    {
                        board.enemyGrid[x, risingY].y++;
                        board.enemyGrid[x, risingY].transform.localPosition = new Vector3(x, risingY + 1);
                    }
                }
            }

            hole = Random.Range(0, GameBoard.width);
            for (int x = 0; x < GameBoard.width; x++)
            {
                if (x != hole)
                {
                    board.enemyGrid[x, 0] = Instantiate(board.garbageTetrisPiece, board.enemyBlockHolder);
                    board.enemyGrid[x, 0].transform.localPosition = new Vector3(x, 0, 0);
                }
                else
                {
                    board.enemyGrid[x, 0] = null;
                }
            }

            yield return new WaitForSeconds(0.4f);
        }
    }

    bool LineClear()
    {
        int newLines = 0;
        bool boardCleared = true;
        for (int y = 0; y < GameBoard.height; y++)
        {
            bool failedLine = false;
            for (int x = 0; x < GameBoard.width; x++)
            {
                if (board.enemyGrid[x, y] == null)
                {
                    failedLine = true;
                }
            }

            if (!failedLine)
            {
                newLines++;
                for (int x = 0; x < GameBoard.width; x++)
                {
                    StartCoroutine(board.enemyGrid[x, y].LineClear());
                }
                for (int fallingY = y; fallingY < GameBoard.height - 1; fallingY++)
                {
                    for (int x = 0; x < GameBoard.width; x++)
                    {
                        board.enemyGrid[x, fallingY] = board.enemyGrid[x, fallingY + 1];
                        if (board.enemyGrid[x, fallingY] != null)
                        {
                            board.enemyGrid[x, fallingY].y--;
                            board.enemyGrid[x, fallingY].transform.localPosition = new Vector3(x, fallingY);
                        }
                    }
                }
                for (int x = 0; x < GameBoard.width; x++)
                {
                    board.enemyGrid[x, GameBoard.height - 1] = null;
                }
                y--;
            }
        }

        for (int y = 0; y < GameBoard.height; y++)
        {
            for (int x = 0; x < GameBoard.width; x++)
            {
                if (board.enemyGrid[x, y] != null)
                {
                    boardCleared = false;
                    break;
                }
            }
        }

        LinesCleared(newLines, boardCleared);
        return (newLines == 0);
    }

    void LinesCleared(int linesCleared, bool boardCleared)
    {
        if (linesCleared > 0)
        {
            combo++;

            if (linesCleared == 2)
            {
                currentCharge++;
            }
            else if (linesCleared == 3)
            {
                currentCharge += 2;
            }
            else if (linesCleared == 4)
            {
                currentCharge += 3;
            }

            if (boardCleared)
            {
                currentCharge += 6;
            }
        }
        else
        {
            currentCharge += board.EnemyComboBonus(combo);

            if (board.EnemyGetAttacked < 0)
            {
                if (currentCharge != 0)
                {
                    board.TakeDamage(currentCharge);
                }
            }
            else
            {
                board.EnemyGetAttacked -= currentCharge;
                if (board.EnemyGetAttacked < 0)
                {
                    board.TakeDamage(-board.EnemyGetAttacked);
                    board.EnemyGetAttacked = 0;
                }
            }


            currentCharge = 0;
            combo = 0;
        }
    }
}
