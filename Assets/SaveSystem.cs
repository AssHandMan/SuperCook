using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{   
    // можно будет переписать класс, создав новый класс DataManager и уже через него
    // сохранять/загружать Cells и Player
    // а то получается что просто CTRL+C и CTRL+V код
    private static string path = Application.persistentDataPath + "/player.fun"; // Сделали path статической переменной

    public static void SavePlayer(PlayerMovement player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
        }
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {   
                    // тут под вопросом, потому что по идее нужно
                    // возвращать null, НО с другой стороны
                    // таким образом фиксится баг когда файл где хранится все
                    // создался, но он пустой
                    if (stream.Length == 0)
                    {
                        return new PlayerData(new float[] {0f, 0f, 0f});
                    }

                    PlayerData data = formatter.Deserialize(stream) as PlayerData;
                    return data;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("LoadPlayer() error: " + ex.Message);
                return null;
            }
        }
        else
        {
            // Если файл не существует, создаем новые данные и возвращаем их
            return new PlayerData(new float[] {0f, 0f, 0f});
        }
    }

    public static void SaveCells(StationCollection place)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            SceneData data = new SceneData(place);
            formatter.Serialize(stream, data);
        }
    }

    public static SceneData LoadCells()
    {
        // не понял че за Cells, поэтому даже не лез сюда
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                SceneData data = formatter.Deserialize(stream) as SceneData;
                return data;
            }
        }
        else
        {
            return null;
        }
    }
}
