using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    public int x = 5;
    public int y = 19;
    public bool holdingGood = false;
    public bool canTSpin = false;
    public bool rotatable;
    public bool preview = true;
    public bool offset = false;

    private TetrisPiece[] pieces = new TetrisPiece[4];
    private int[] pieceX = new int[4];
    private int[] pieceY = new int[4];
    public GameBoard board { get; set; }

    public float fallTimer;

    private float moveTimer;
    private const float maxMoveTimer = 0.25f;
    private const float timeToMove = 0.05f;
    private bool isAi = false;

    private bool isAiFastFalling = false;
    private bool hasAiFastFalling = false;
    private int aiAfterHit;

    public Color GetColour()
    {
        return (pieces[0] != null) ? pieces[0].GetComponent<SpriteRenderer>().color : Color.black;
    }

    public int[] GetX()
    {
        int[] xRet = new int[4];
        for (int i = 0; i < 4; i++)
        {
            xRet[i] = pieces[i].x + x;
        }
        return xRet;
    }

    public int[] GetY()
    {
        int[] yRet = new int[4];
        for (int i = 0; i < 4; i++)
        {
            yRet[i] = pieces[i].y + y;
        }
        return yRet;
    }

    public bool IsFastFalling()
    {
        return isAiFastFalling;
    }

    public void SetupFastFall(int movement)
    {
        hasAiFastFalling = true;
        isAiFastFalling = true;
        aiAfterHit = movement;
    }

    private void Awake()
    {
        pieces[0] = transform.GetChild(0).GetComponent<TetrisPiece>();
        pieces[1] = transform.GetChild(1).GetComponent<TetrisPiece>();
        pieces[2] = transform.GetChild(2).GetComponent<TetrisPiece>();
        pieces[3] = transform.GetChild(3).GetComponent<TetrisPiece>();

        for (int i = 0; i < 4; i++)
        {
            pieceX[i] = pieces[i].x;
            pieceY[i] = pieces[i].y;
        }
    }

    public void Create(GameBoard newBoard, bool newIsAi)
    {
        board = newBoard;
        x = 4;
        y = 19;
        transform.localPosition = new Vector3(x, y);

        isAi = newIsAi;

        if (isAi)
        {
            for (int i = 0; i < 4; i++)
            {
                if (board.enemyGrid[x + pieces[i].x, y + pieces[i].y] != null)
                {
                    newBoard.Death(true);
                    preview = true;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (board.grid[x + pieces[i].x, y + pieces[i].y] != null)
                {
                    newBoard.Death(false);
                    preview = true;
                    return;
                }
            }
        }

        preview = false;

        if (!isAi)
        {
            UpdateFallDown();
        }
    }

    public void Hold()
    {
        for (int i = 0; i < 4; i++)
        {
            pieces[i].x = pieceX[i];
            pieces[i].y = pieceY[i];
            pieces[i].transform.localPosition = new Vector3(pieces[i].x, pieces[i].y, 0);
        }
    }

    private void LateUpdate()
    {
        if (!preview && !board.isWaiting)
        {
            if (!isAi)
            {
                fallTimer += Time.deltaTime * ((GameController.defaultControls ? Input.GetKey(KeyCode.UpArrow) : Input.GetKey(KeyCode.DownArrow)) && FallDownFall(y) != y ? board.fastFallSpeed : 1) * (board.HasPerk(Perks.BlocksFallSlower) ? 0.65f : 1) * (board.EnemyHasPerk(Perks.EnemyFallsFaster) ? 1.6f : 1);
            }
            else
            {
                fallTimer += Time.deltaTime * (hasAiFastFalling ? board.fastFallSpeed : 1) * (board.HasPerk(Perks.EnemyFallsFaster) ? 1.6f : 1);
                if (hasAiFastFalling && FallDownFall(y) == y)
                {
                    Move(aiAfterHit, false);
                    aiAfterHit = 0;
                    hasAiFastFalling = false;
                }
            }

            while (fallTimer >= (FallDownFall(y) != y ? board.maxFallTimer : 1))
            {
                if (!Fall())
                {
                    return;
                }
                if (!isAi)
                {
                    board.wasLastMoveSpin = false;
                }
                fallTimer -= board.maxFallTimer;
            }

            if (!isAi)
            {
                if ((GameController.defaultControls ? Input.GetKeyDown(KeyCode.A) : Input.GetKeyDown(KeyCode.Z)) && rotatable)
                {
                    board.wasLastMoveSpin = canTSpin;

                    if (AttemptLeftRotate(0, 0)
                        || AttemptLeftRotate(1, 0)
                        || AttemptLeftRotate(-1, 0)
                        || AttemptLeftRotate(2, 0)
                        || AttemptLeftRotate(-2, 0)
                        || AttemptLeftRotate(0, -1)
                        || AttemptLeftRotate(1, -1)
                        || AttemptLeftRotate(-1, -1)
                        || AttemptLeftRotate(0, -2)
                        || AttemptLeftRotate(1, -2)
                        || AttemptLeftRotate(-1, -2))
                    {
                        board.wasLastMoveSpin = canTSpin;
                    }
                }
                if ((GameController.defaultControls ? Input.GetKeyDown(KeyCode.D) : (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.UpArrow))) && rotatable)
                {
                    if (AttemptRightRotate(0, 0)
                        || AttemptRightRotate(-1, 0)
                        || AttemptRightRotate(1, 0)
                        || AttemptRightRotate(-2, 0)
                        || AttemptRightRotate(2, 0)
                        || AttemptRightRotate(0, -1)
                        || AttemptRightRotate(-1, -1)
                        || AttemptRightRotate(1, -1)
                        || AttemptRightRotate(0, -2)
                        || AttemptRightRotate(-1, -2)
                        || AttemptRightRotate(1, -2))
                    {
                        board.wasLastMoveSpin = canTSpin;
                    }
                }

                if (GameController.defaultControls ? Input.GetKeyDown(KeyCode.DownArrow) : Input.GetKeyDown(KeyCode.Space))
                {
                    while (Fall()) ;
                    return;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (Move(-1, false))
                    {
                        board.wasLastMoveSpin = false;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (Move(1, false))
                    {
                        board.wasLastMoveSpin = false;
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        moveTimer += Time.deltaTime;
                        if (moveTimer >= maxMoveTimer)
                        {
                            Move(-1, false);
                            board.wasLastMoveSpin = false;
                            moveTimer -= timeToMove;
                        }
                    }
                    else if (Input.GetKey(KeyCode.RightArrow))
                    {
                        moveTimer += Time.deltaTime;
                        if (moveTimer >= maxMoveTimer)
                        {
                            Move(1, false);
                            board.wasLastMoveSpin = false;
                            moveTimer -= timeToMove;
                        }
                    }
                    else
                    {
                        moveTimer = 0;
                    }
                }
            }

            transform.localPosition = new Vector3(x, y);
        }
    }

    public bool Move(int xMovement, bool theoretical)
    {
        for (int i = 0; i < 4; i++)
        {
            if (isAi)
            {
                if (x + pieces[i].x + xMovement < 0 || x + pieces[i].x + xMovement > 9 || board.enemyGrid[x + pieces[i].x + xMovement, y + pieces[i].y] != null)
                {
                    return false;
                }
            }
            else
            {
                if (x + pieces[i].x + xMovement < 0 || x + pieces[i].x + xMovement > 9 || board.grid[x + pieces[i].x + xMovement, y + pieces[i].y] != null)
                {
                    return false;
                }
            }
        }
        x += xMovement;
        if (!theoretical && !isAi)
        {
            UpdateFallDown();
        }
        return true;
    }

    public void Place()
    {
        if (!isAi)
        {
            if (board.wasLastMoveSpin)
            {
                bool hanged = false;
                if (y >= 19)
                {
                    board.wasLastMoveSpin = false;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (board.grid[x + pieces[i].x, y + pieces[i].y + 1] != null)
                        {
                            hanged = true;
                            break;
                        }
                    }
                    board.wasLastMoveSpin = hanged;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                pieces[i].transform.SetParent(board.playerBlockHolder);
                pieces[i].x = x + pieces[i].x;
                pieces[i].y = y + pieces[i].y;
                pieces[i].transform.localPosition = new Vector3(pieces[i].x, pieces[i].y, 0);
                board.grid[pieces[i].x, pieces[i].y] = pieces[i];
            }

            StartCoroutine(board.NewPiece(true));
            enabled = false;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                pieces[i].transform.SetParent(board.enemyBlockHolder);
                pieces[i].x = x + pieces[i].x;
                pieces[i].y = y + pieces[i].y;
                pieces[i].transform.localPosition = new Vector3(pieces[i].x, pieces[i].y, 0);
                board.enemyGrid[pieces[i].x, pieces[i].y] = pieces[i];
            }
            board.ai.enemyCurTetra = null;
            enabled = false;
        }
    }

    private bool AttemptLeftRotate(int xMod, int yMod)
    {
        for (int i = 0; i < 4; i++)
        {
            if (x - pieces[i].y + xMod < 0 || x - pieces[i].y + xMod > 9 || y + pieces[i].x + yMod < 0 || y + pieces[i].x + yMod >= GameBoard.height || board.grid[x - pieces[i].y + xMod, y + pieces[i].x + yMod] != null)
            {
                return false;
            }
        }

        x += xMod;
        y += yMod;

        for (int i = 0; i < 4; i++)
        {
            int pieceX = -pieces[i].y;
            pieces[i].y = pieces[i].x;
            pieces[i].x = pieceX;

            pieces[i].transform.localPosition = new Vector3(pieces[i].x, pieces[i].y, 0);
        }
        UpdateFallDown();

        return true;
    }

    public bool AttemptRightRotate(int xMod, int yMod, bool draw = true)
    {
        if (isAi)
        {
            for (int i = 0; i < 4; i++)
            {
                if (x + pieces[i].y + xMod < 0 || x + pieces[i].y + xMod > 9 || y - pieces[i].x + yMod < 0 || y - pieces[i].x + yMod >= GameBoard.height || board.enemyGrid[x + pieces[i].y + xMod, y - pieces[i].x + yMod] != null)
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (x + pieces[i].y + xMod < 0 || x + pieces[i].y + xMod > 9 || y - pieces[i].x + yMod < 0 || y - pieces[i].x + yMod >= GameBoard.height || board.grid[x + pieces[i].y + xMod, y - pieces[i].x + yMod] != null)
                {
                    return false;
                }
            }
        }

        x += xMod;
        y += yMod;

        for (int i = 0; i < 4; i++)
        {
            int pieceY = -pieces[i].x;
            pieces[i].x = pieces[i].y;
            pieces[i].y = pieceY;

            if (draw)
            {
                pieces[i].transform.localPosition = new Vector3(pieces[i].x, pieces[i].y, 0);
            }
        }
        if (!isAi)
        {
            UpdateFallDown();
        }

        return true;
    }

    public bool Fall()
    {
        for (int i = 0; i < 4; i++)
        {
            if (isAi)
            {
                if ((y + pieces[i].y - 1) < 0 || board.enemyGrid[x + pieces[i].x, y + pieces[i].y - 1] != null)
                {
                    //print((y + pieces[i].y - 1) < 0);
                    //print(board.grid[x + pieces[i].x, y + pieces[i].y - 1] != null);
                    Place();
                    return false;
                }
            }
            else
            {
                if ((y + pieces[i].y - 1) < 0 || board.grid[x + pieces[i].x, y + pieces[i].y - 1] != null)
                {
                    //print((y + pieces[i].y - 1) < 0);
                    //print(board.grid[x + pieces[i].x, y + pieces[i].y - 1] != null);
                    Place();
                    return false;
                }
            }
        }
        y--;
        if (!isAi)
        {
            UpdateFallDown();
        }
        return true;
    }

    private int FallDownFall(int usedY)
    {
        if (isAi)
        {
            int fakeY = usedY;
            while (true)
            {
                bool failed = false;
                for (int i = 0; i < 4; i++)
                {
                    if ((fakeY + pieces[i].y - 1) < 0 || board.enemyGrid[x + pieces[i].x, fakeY + pieces[i].y - 1] != null)
                    {
                        failed = true;
                        break;
                    }
                }
                if (failed)
                {
                    break;
                }
                fakeY--;
            }
            return fakeY;
        }
        else
        {
            int fakeY = usedY;
            while (true)
            {
                bool failed = false;
                for (int i = 0; i < 4; i++)
                {
                    if ((fakeY + pieces[i].y - 1) < 0 || board.grid[x + pieces[i].x, fakeY + pieces[i].y - 1] != null)
                    {
                        failed = true;
                        break;
                    }
                }
                if (failed)
                {
                    break;
                }
                fakeY--;
            }
            return fakeY;
        }
    }

    public void UpdateFallDown()
    {
        int fallDistance = FallDownFall(y);

        for (int i = 0; i < 4; i++)
        {
            board.fallDown[i].size = new Vector2(1, y - fallDistance);
            board.fallDown[i].transform.localPosition = new Vector3(x + pieces[i].x, (board.fallDown[i].size.y / 2) - 0.5f + fallDistance + pieces[i].y);

            board.fallDown[i].transform.GetChild(0).transform.localPosition = new Vector3(0, -board.fallDown[i].size.y / 2 + 0.5f);
        }
    }

    public BoardState GetStates(TetrisPiece[,] grid, AI brain, int flatBonus, bool isCleanUp)
    {
        List<BoardState> states = new List<BoardState>();
        int endX = x;
        int endY = y;

        x = 4;
        y = 18;
        int tempX = x;

        if (rotatable)
        {
            for (int i = 0; i < 4; i++)
            {
                while (Move(-1, true)) ;

                tempX = x;
                states.Add(RateState(grid, brain, 0, i, flatBonus, isCleanUp));
                x = tempX;
                if (!isCleanUp)
                {
                    states.Add(RateState(grid, brain, -1, i, flatBonus, isCleanUp));
                    x = tempX;
                    states.Add(RateState(grid, brain, 1, i, flatBonus, isCleanUp));
                    x = tempX;
                }
                while (Move(1, true))
                {
                    tempX = x;
                    states.Add(RateState(grid, brain, 0, i, flatBonus, isCleanUp));
                    x = tempX;
                    if (!isCleanUp)
                    {
                        states.Add(RateState(grid, brain, -1, i, flatBonus, isCleanUp));
                        x = tempX;
                        states.Add(RateState(grid, brain, 1, i, flatBonus, isCleanUp));
                        x = tempX;
                    }
                }

                x = 4;
                if (AttemptRightRotate(0, 0, false)) ;
                else if (AttemptRightRotate(0, -1, false)) ;
            }
        }
        else
        {
            while (Move(-1, true)) ;

            tempX = x;
            states.Add(RateState(grid, brain, 0, 0, flatBonus, isCleanUp));
            x = tempX;
            if (!isCleanUp)
            {
                states.Add(RateState(grid, brain, -1, 0, flatBonus, isCleanUp));
                x = tempX;
                states.Add(RateState(grid, brain, 1, 0, flatBonus, isCleanUp));
                x = tempX;
            }
            while (Move(1, true))
            {
                tempX = x;
                states.Add(RateState(grid, brain, 0, 0, flatBonus, isCleanUp));
                x = tempX;
                if (!isCleanUp)
                {
                    states.Add(RateState(grid, brain, -1, 0, flatBonus, isCleanUp));
                    x = tempX;
                    states.Add(RateState(grid, brain, 1, 0, flatBonus, isCleanUp));
                    x = tempX;
                }
            }
        }

        x = endX;
        y = endY;
        if (isCleanUp)
        {
            for (int i = states.Count - 1; i >= 0; i--)
            {
                if (states[i].clears > 0)
                {
                    states.RemoveAt(i);
                }
            }
        }
        states.Sort(SortByScore);
        //print(states[0].score + " " + states[states.Count - 1].score);
        //print(states[0].score + " " + states[0].hangs);
        if (isCleanUp)
            return states[states.Count - 1];
        return states[0];
    }

    static int SortByScore(BoardState s1, BoardState s2)
    {
        return (-s1.score).CompareTo(-s2.score);
    }

    bool PieceOverLaps(int testX, int testY, int fakeY = 0)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].x + x == testX && pieces[i].y + fakeY == testY)
            {
                return true;
            }
        }
        return false;
    }

    int SimpleStateHangs(TetrisPiece[,] grid)
    {
        List<int> linesCleared = new List<int>();
        for (int yT = 0; yT < GameBoard.height; yT++)
        {
            bool lineClear = true;
            int holes = 0;
            for (int xT = 0; xT < GameBoard.width; xT++)
            {
                if (grid[xT, yT] == null && !PieceOverLaps(xT, yT))
                {
                    lineClear = false;
                    holes++;
                }
            }
            if (lineClear)
            {
                linesCleared.Add(yT);
                //print("Cleared Line");
            }
        }

        int hangCount = 0;
        for (int xT = 0; xT < GameBoard.width; xT++)
        {
            for (int yT = 0; yT < GameBoard.height; yT++)
            {
                if (grid[xT, yT] == null && !PieceOverLaps(xT, yT) && !linesCleared.Contains(yT))
                {
                    if (yT == 0 || (grid[xT, yT - 1] != null || PieceOverLaps(xT, yT - 1)))
                    {
                        bool isHang = false;
                        for (int y2 = yT + 1; y2 < GameBoard.height; y2++)
                        {
                            if (!linesCleared.Contains(y2))
                            {
                                if (grid[xT, y2] != null || PieceOverLaps(xT, y2))
                                {
                                    isHang = true;
                                    break;
                                }
                            }
                        }
                        if (isHang)
                        {
                            hangCount++;
                        }
                    }
                }
            }
        }

        return hangCount;
    }

    BoardState RateState(TetrisPiece[,] grid, AI brain, int extraMovement, int rotational, int flatBonus, bool isCleanUp)
    {
        BoardState returnable = new BoardState(0, x, rotational, extraMovement);

        int fakeY = FallDownFall(y);
        if (extraMovement != 0)
        {
            int realY = y;
            y = fakeY;
            if (!Move(extraMovement, true))
            {
                y = realY;
                returnable.score = -999999;
                return returnable;
            }
            y = realY;
            fakeY = FallDownFall(fakeY);
            returnable.score -= 1;
            returnable.score -= brain.randomAdditiveMax * 0.5f;
        }

        returnable.score += flatBonus;

        //Bumpiness
        int[] heights = new int[GameBoard.width];
        for (int xT = 0; xT < GameBoard.width; xT++)
        {
            for (int yT = GameBoard.height - 1; yT >= 0; yT--)
            {
                if (yT == 0 || grid[xT, yT] != null || PieceOverLaps(xT, yT, fakeY))
                {
                    heights[xT] = yT;
                    break;
                }
            }
        }
        
        //Ignore Lowest
        int lowestVal = heights[0];
        int lowestIndex = 0;
        for (int i = 1; i < GameBoard.width; i++)
        {
            if (heights[i] < lowestVal)
            {
                lowestIndex = i;
                lowestVal = heights[i];
            }
        }

        //Add highest
        int highestVal = heights[0];
        for (int i = 1; i < GameBoard.width; i++)
        {
            if (heights[i] > highestVal)
            {
                highestVal = heights[i];
            }
        }

        if (isCleanUp)
        {
            returnable.score += highestVal * AI.maxHeightCare * 10;
        }
        else
        {
            if (highestVal <= 8)
            {
                returnable.score -= highestVal * AI.maxHeightCare;
            }
            else
            {
                returnable.score -= (8 * AI.maxHeightCare) + ((highestVal - 6) * (highestVal - 8) * 0.5f * AI.maxHeightCare);
            }
        }

        //Get Bumpiness
        int bumps = 0;
        int last = lowestIndex == 0 ? heights[1] : heights[0];
        for (int i = 0; i < GameBoard.width; i++)
        {
            if (i == lowestIndex)
            {
                continue;
            }
            bumps += Mathf.Abs(last - heights[i]);
            if (Mathf.Abs(last - heights[i]) >= 3)
            {
                returnable.score -= AI.threeHoleHate;
            }
            last = heights[i];
        }

        returnable.score -= bumps * AI.bumpinessCare;

        //Four Hole

        int secondVal = 1000;

        for (int i = 0; i < GameBoard.width; i++)
        {
            if (i != lowestIndex && heights[i] < secondVal)
            {
                secondVal = heights[i];
            }
        }

        returnable.score += Mathf.Clamp((secondVal - lowestVal), 0, 5) * AI.tetrisCare;

        int clearedLines = 0;
        List<int> linesCleared = new List<int>();
        for (int yT = 0; yT < GameBoard.height; yT++)
        {
            bool lineClear = true;
            int holes = 0;
            for (int xT = 0; xT < GameBoard.width; xT++)
            {
                if (grid[xT, yT] == null && !PieceOverLaps(xT, yT, fakeY))
                {
                    lineClear = false;
                    holes++;
                }
            }
            if (lineClear)
            {
                clearedLines++;
                returnable.score += AI.oneHoleCare;
                linesCleared.Add(yT);
                //print("Cleared Line");
            }
            else if (holes == 1)
            {
                returnable.score += AI.oneHoleCare;
            }
        }

        if (!isCleanUp)
        {
            returnable.score += clearedLines * AI.maxHeightCare;
            if (clearedLines == 1)
            {
                if (highestVal <= 8)
                    returnable.score -= AI.oneLineHate;
            }
            if (clearedLines == 2)
            {
                if (highestVal >= 7)
                    returnable.score += 0.75f * AI.clearCare;
                else
                    returnable.score -= AI.twoLineHate;
            }
            else if (clearedLines == 3)
            {
                if (highestVal >= 6)
                    returnable.score += 2f * AI.clearCare;
                else
                    returnable.score -= AI.threeLineHate;
            }
            else if (clearedLines == 4)
            {
                returnable.score += 5f * AI.clearCare;
            }
        }

        //Holes

        if (!isCleanUp)
        {
            int holeCount = 0;
            for (int xT = 0; xT < GameBoard.width; xT++)
            {
                for (int yT = 0; yT < GameBoard.height; yT++)
                {
                    if (grid[xT, yT] == null && !PieceOverLaps(xT, yT, fakeY) && !linesCleared.Contains(yT))
                    {
                        if (xT == 0 || (grid[xT - 1, yT] != null || PieceOverLaps(xT - 1, yT, fakeY)))
                        {
                            if (xT == GameBoard.width - 1 || (grid[xT + 1, yT] != null || PieceOverLaps(xT + 1, yT, fakeY)))
                            {
                                if (yT == 0 || (grid[xT, yT - 1] != null || PieceOverLaps(xT, yT - 1, fakeY)))
                                {
                                    if (yT == GameBoard.height - 1 || (grid[xT, yT + 1] != null || PieceOverLaps(xT, yT + 1, fakeY)))
                                    {
                                        holeCount++;
                                        //print(holeCount);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            returnable.score -= holeCount * AI.holeCare;
        }

        //Overhangs

        if (!isCleanUp)
        {
            int hangCount = 0;
            for (int xT = 0; xT < GameBoard.width; xT++)
            {
                for (int yT = 0; yT < GameBoard.height; yT++)
                {
                    if (grid[xT, yT] == null && !PieceOverLaps(xT, yT, fakeY) && !linesCleared.Contains(yT))
                    {
                        if (yT == 0 || (grid[xT, yT - 1] != null || PieceOverLaps(xT, yT - 1, fakeY)))
                        {
                            bool isHang = false;
                            int hangSize = 0;
                            for (int y2 = yT + 1; y2 < GameBoard.height; y2++)
                            {
                                if (!linesCleared.Contains(y2))
                                {
                                    if (grid[xT, y2] != null || PieceOverLaps(xT, y2, fakeY))
                                    {
                                        isHang = true;
                                        hangSize++;
                                    }
                                    else if (isHang)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (isHang)
                            {
                                hangCount++;
                                returnable.score -= hangSize * AI.hangStackCare;
                                returnable.score -= AI.hangCare;
                            }
                        }
                    }
                }
            }

            returnable.hangs = hangCount;
        }

        //Clears


        returnable.score += Random.Range(0, brain.randomAdditiveMax);

        if (x == 3)
        {
            returnable.score += brain.randomAdditiveMax * 0.25f;
        }
        if (x == 4)
        {
            returnable.score += brain.randomAdditiveMax * 0.5f;
        }
        if (x == 5)
        {
            returnable.score += brain.randomAdditiveMax * 0.25f;
        }

        returnable.clears = clearedLines;
        return returnable;
    }
}
