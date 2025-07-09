using NUnit.Framework.Interfaces;
using System;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class FileDataHandler 
{
    private string fullPath;
    private bool encrpyData;
    private string codeWord = "unityalexdev.com";

    public FileDataHandler(string dataDirPath, string dataFileName,bool encryptData)
    {
        fullPath = Path.Combine(dataDirPath, dataFileName);
        this.encrpyData = encryptData;
    }

    public void SaveData(GameData gameData)
    {
        try
        {
            // 1. Create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // 2. Convert GameData to JSON string
            string dataToSave = JsonUtility.ToJson(gameData, true);

            if(encrpyData)
                dataToSave = EncryptDecrypt(dataToSave);

            // 3. Open/create a new file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // 4. Write the JSON text to the file
                using (StreamWriter write = new StreamWriter(stream))
                {
                    write.Write(dataToSave);
                }
            }
        }

        catch (Exception e)
        {
            // Log any error that happens
            Debug.LogError("Error on trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public GameData LoadData()
    {
        GameData loadData = null;

        //  1. Check if the save file exists
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                //  2. Open the file 
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    // 3. Read file's text content
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if(encrpyData)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                // 4. Convert the JSON string back into a GameData object
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {   // Log any error that happens
                Debug.LogError("Error on trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete()
    {
        if(File.Exists(fullPath))
            File.Delete(fullPath);
    }

   
    private string EncryptDecrypt(string data)
    {
        string modifedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
        }

        return modifedData;
    }
}
