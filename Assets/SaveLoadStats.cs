using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class SaveLoadStats : MonoBehaviour
{
    public static bool[] normalCircuits = new bool[4];
    public static bool[] hardCircuits = new bool[4];

    public static int[] normalHighscores = new int[5];
    public static int[] hardHighscores = new int[5];

    public static int[] normalAi = new int[6];
    public static int[] hardAi = new int[6];

    public static bool[] normalCleanUp = new bool[15];
    public static bool[] hardCleanUp = new bool[15];

    public static bool shouldDoTutorial = false;

    public static void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "stats.tetris");

        using (BinaryWriter writer =new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(2);
            for (int i = 0; i < 4; i++) //circuits
            {
                writer.Write(normalCircuits[i]);
                writer.Write(hardCircuits[i]);
            }

            for (int i = 0; i < 5; i++) //highscores
            {
                writer.Write(normalHighscores[i]);
                writer.Write(hardHighscores[i]);
            }

            for (int i = 0; i < 6; i++) //versus ai
            {
                writer.Write(normalAi[i]);
                writer.Write(hardAi[i]);
            }

            for (int i = 0; i < 15; i++) //versus ai
            {
                writer.Write(normalCleanUp[i]);
                writer.Write(hardCleanUp[i]);
            }
        }
    }

    public static int PushNormalHighscore(int score)
    {
        if (score > normalHighscores[0])
        {
            normalHighscores[4] = normalHighscores[3];
            normalHighscores[3] = normalHighscores[2];
            normalHighscores[2] = normalHighscores[1];
            normalHighscores[1] = normalHighscores[0];

            normalHighscores[0] = score;
            return 1;
        }
        else if (score > normalHighscores[1])
        {
            normalHighscores[4] = normalHighscores[3];
            normalHighscores[3] = normalHighscores[2];
            normalHighscores[2] = normalHighscores[1];

            normalHighscores[1] = score;
            return 2;
        }
        else if (score > normalHighscores[2])
        {
            normalHighscores[4] = normalHighscores[3];
            normalHighscores[3] = normalHighscores[2];

            normalHighscores[2] = score;
            return 3;
        }
        else if (score > normalHighscores[3])
        {
            normalHighscores[4] = normalHighscores[3];

            normalHighscores[3] = score;
            return 4;
        }
        else if (score > normalHighscores[4])
        {
            normalHighscores[4] = score;
            return 5;
        }
        return 100;
    }

    public static int PushHardHighscore(int score)
    {
        if (score > hardHighscores[0])
        {
            hardHighscores[4] = hardHighscores[3];
            hardHighscores[3] = hardHighscores[2];
            hardHighscores[2] = hardHighscores[1];
            hardHighscores[1] = hardHighscores[0];

            hardHighscores[0] = score;
            return 1;
        }
        else if (score > hardHighscores[1])
        {
            hardHighscores[4] = hardHighscores[3];
            hardHighscores[3] = hardHighscores[2];
            hardHighscores[2] = hardHighscores[1];

            hardHighscores[1] = score;
            return 2;
        }
        else if (score > hardHighscores[2])
        {
            hardHighscores[4] = hardHighscores[3];
            hardHighscores[3] = hardHighscores[2];

            hardHighscores[2] = score;
            return 3;
        }
        else if (score > hardHighscores[3])
        {
            hardHighscores[4] = hardHighscores[3];

            hardHighscores[3] = score;
            return 4;
        }
        else if (score > hardHighscores[4])
        {
            hardHighscores[4] = score;
            return 5;
        }
        return 100;
    }

    public static bool Load()
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "stats.tetris");
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                int version = reader.ReadInt32();
                if (version == 0)
                {
                    Save();
                    Load();

                    return true;
                }
                else if (version == 1)
                {
                    for (int i = 0; i < 4; i++) //circuits
                    {
                        normalCircuits[i] = reader.ReadBoolean();
                        hardCircuits[i] = reader.ReadBoolean();
                    }

                    for (int i = 0; i < 5; i++) //highscores
                    {
                        normalHighscores[i] = reader.ReadInt32();
                        hardHighscores[i] = reader.ReadInt32();
                    }

                    for (int i = 0; i < 6; i++) //versus ai
                    {
                        normalAi[i] = reader.ReadInt32();
                        hardAi[i] = reader.ReadInt32();
                    }
                    return false;
                }
                else if (version == 2)
                {
                    bool failed = false;
                    for (int i = 0; i < 4; i++) //circuits
                    {
                        normalCircuits[i] = reader.ReadBoolean();
                        hardCircuits[i] = reader.ReadBoolean();
                        if (normalCircuits[i] || hardCircuits[i])
                        {
                            failed = true;
                        }
                    }

                    for (int i = 0; i < 5; i++) //highscores
                    {
                        normalHighscores[i] = reader.ReadInt32();
                        hardHighscores[i] = reader.ReadInt32();
                        if (normalHighscores[i] != 0 || hardHighscores[i] != 0)
                        {
                            failed = true;
                        }
                    }

                    for (int i = 0; i < 6; i++) //versus ai
                    {
                        normalAi[i] = reader.ReadInt32();
                        hardAi[i] = reader.ReadInt32();
                        if (normalAi[i] != 0 || hardAi[i] != 0)
                        {
                            failed = true;
                        }
                    }

                    for (int i = 0; i < 15; i++) //clean up
                    {
                        normalCleanUp[i] = reader.ReadBoolean();
                        hardCleanUp[i] = reader.ReadBoolean();
                        if (normalCleanUp[i] || hardCleanUp[i])
                        {
                            failed = true;
                        }
                    }

                    return !failed;
                }
            }
        }
        catch
        {
            Save();
            Load();
            return true;
        }
        return false;
    }

    public static void Wipe()
    {
        for (int i = 0; i < 4; i++) //circuits
        {
            normalCircuits[i] = false;
            hardCircuits[i] = false;
        }

        for (int i = 0; i < 5; i++) //highscores
        {
            normalHighscores[i] = 0;
            hardHighscores[i] = 0;
        }

        for (int i = 0; i < 6; i++) //versus ai
        {
            normalAi[i] = 0;
            hardAi[i] = 0;
        }

        for (int i = 0; i < 15; i++) //versus ai
        {
            normalCleanUp[i] = false;
            hardCleanUp[i] = false;
        }

        Save();
    }
}
