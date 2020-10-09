using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataHandler
{

    private static string StreamingAssetPath;
    private static string PersistentDataPath;
    private static string DataPath;
    private static string TemporaryCachePath;


    /// <summary>
    /// This method allow to save data in the specified unityDirectory
    /// </summary>
    /// <param name="unityDirectory">The specified unityDirectory</param>
    /// <param name="data">The serializable data</param>
    /// <param name="fileName">The data fileName</param>
    /// <typeparam name="T">The serializable data type</typeparam>
    public static void Save<T>(UnityDirectory unityDirectory, T data, string fileName)
    {
        //Call CheckOrCreateDirectory
        CheckOrCreateDirectory(GetDirectory(unityDirectory));
        //Get the filePath
        string path = GetDirectory(unityDirectory) + Path.AltDirectorySeparatorChar + fileName;
        //Serialize data to Json file
        string jsonData = JsonUtility.ToJson(data);
        //Open the fileStream to the file
        FileStream fileStream = new FileStream(path, FileMode.Create);
        //Open the streamWriter on the fileStream
        StreamWriter streamWriter = new StreamWriter(fileStream);
        //Write the jsonData using the streamWriter
        streamWriter.Write(jsonData);
        //Close the streamWriter
        streamWriter.Close();
        //Close the fileStream
        fileStream.Close();
    }

    /// <summary>
    /// This method allow to Load data in the specified unityDirectory
    /// </summary>
    /// <param name="unityDirectory">The specified unityDirectory</param>
    /// <param name="fileName">The data fileName</param>
    /// /// <typeparam name="T">The serializable data type</typeparam>
    public static T Load<T>(UnityDirectory unityDirectory, string fileName)
    {
        //Call CheckOrCreateDirectory
        CheckOrCreateDirectory(GetDirectory(unityDirectory));
        //Get the filePath
        string filePath = GetDirectory(unityDirectory) + Path.AltDirectorySeparatorChar + fileName;
        //Call checkFile
        if(!CheckFile(filePath)) throw new Exception("Can't load: File does not exist");
        //Open file reader on the file
        StreamReader streamReader = new StreamReader(filePath);
        //Read the jsonData using the streamReader
        string jsonData = streamReader.ReadToEnd();
        //Close the streamWriter
        streamReader.Close();
        //Convert le json en data
        return JsonUtility.FromJson<T>(jsonData);
    }

    private static string GetDirectory(UnityDirectory unityDirectory)
    {
        switch (unityDirectory)
        {
            case UnityDirectory.StreamingAssetPath:
                return Application.streamingAssetsPath;
            case UnityDirectory.PersistentDataPath:
                return Application.persistentDataPath;
            case UnityDirectory.DataPath:
                return Application.dataPath;
            case UnityDirectory.TemporaryCachePath:
                return Application.temporaryCachePath;
            default:
                throw new ArgumentOutOfRangeException(nameof(unityDirectory), unityDirectory, null);
        }
    }
    
    //Verify if directory exist and create it if it doesn't 
    private static bool CheckOrCreateDirectory(string directoryPath)
    {
        bool isDirectoryExist = File.Exists(directoryPath);
        if (!isDirectoryExist)
            Directory.CreateDirectory(directoryPath);
        return isDirectoryExist;
    }

    //Verify if file exists
    private static bool CheckFile(string filePath)
    {
        bool isFileExist = File.Exists(filePath);
        return isFileExist;
    }
    
}

public enum UnityDirectory
{
    StreamingAssetPath, PersistentDataPath, DataPath, TemporaryCachePath
}
