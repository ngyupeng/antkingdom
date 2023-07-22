using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] Building FoodStoragePrefab;
    [SerializeField] Building StoneStoragePrefab;
    [SerializeField] Building WoodStoragePrefab;
    [SerializeField] Building HousingPrefab;

    public static List<Building> buildings = new List<Building>();
    const string RES_SUB = "/resources";
    const string ANT_SUB = "/ants";
    const string BUILDING_SUB = "/buildings";
    const string BUILDING_COUNT_SUB = "/buildings";
    
    void Start()
    {
        LoadBuildings();
        LoadAnt();
        LoadResource();
    }
    void OnApplicationQuit()
    {
        SaveAnt();
        SaveBuildings();
        SaveResource();
    }

    void SaveResource()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + RES_SUB;
        FileStream stream = new FileStream(path, FileMode.Create);
        ResourceData data = new ResourceData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    void SaveAnt()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ANT_SUB;
        FileStream stream = new FileStream(path, FileMode.Create);
        PopulationData data = new PopulationData();
        formatter.Serialize(stream, data);
        stream.Close();

    }

    void SaveBuildings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BUILDING_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + BUILDING_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, buildings.Count);
        countStream.Close();
        for (int i = 0; i < buildings.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BuildingData data = new BuildingData(buildings[i]);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    void LoadResource()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + RES_SUB;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            ResourceData data = formatter.Deserialize(stream) as ResourceData;
            stream.Close();
            GameResources.AddResourceAmount(GameResources.ResourceType.Wood, data.resourceAmountData[GameResources.ResourceType.Wood] - 500);
            GameResources.AddResourceAmount(GameResources.ResourceType.Stone, data.resourceAmountData[GameResources.ResourceType.Stone] - 500);
            GameResources.AddResourceAmount(GameResources.ResourceType.Food, data.resourceAmountData[GameResources.ResourceType.Food] - 500);
            GameResources.AddResourceAmount(GameResources.ResourceType.Iron, data.resourceAmountData[GameResources.ResourceType.Iron] - 500);
            GameResources.AddResourceAmount(GameResources.ResourceType.MagicCrystal, data.resourceAmountData[GameResources.ResourceType.MagicCrystal]);
        }
        else
        {
            Debug.LogError("Path not found in " + path);
        }
    }


    void LoadAnt()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ANT_SUB;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            PopulationData data = formatter.Deserialize(stream) as PopulationData;
            stream.Close();
            AntManager.AddAntAmount(AntManager.AntType.WorkerAnt, data.antCount[AntManager.AntType.WorkerAnt] - 5);
            AntManager.AddAntAmount(AntManager.AntType.EngineerAnt, data.antCount[AntManager.AntType.EngineerAnt]);
            AntManager.AddAntAmount(AntManager.AntType.SoldierAnt, data.antCount[AntManager.AntType.SoldierAnt]);
        }
        else
        {
            Debug.LogError("Path not found in " + path);
        }
    }

    void LoadBuildings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BUILDING_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + BUILDING_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int buildingsCount = 0;
        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);
            buildingsCount = (int)formatter.Deserialize(countStream);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Path not found in " + countPath);
        }
        for (int i = 0; i < buildingsCount; i++)
        {
            if (File.Exists(path + i))
            {
            FileStream stream = new FileStream(path + i, FileMode.Open);
            BuildingData data = formatter.Deserialize(stream) as BuildingData;

            stream.Close();
            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            Vector3Int areaPos = new Vector3Int(data.area[0], data.area[1], data.area[2]);
            if (data.buildingName == "Food Storage")
            {
                Building building = Instantiate(FoodStoragePrefab, position, Quaternion.identity);
                building.FinishBuilding();
                building.Place();
                building.area.position = areaPos;
                for (int j = 0; j < data.level; j++)
                {
                    building.FinishUpgrade();
                }
                
            }
            else if (data.buildingName == "Stone Storage")
            {
                Building building = Instantiate(StoneStoragePrefab, position, Quaternion.identity);
                building.FinishBuilding();
                building.Place();
                building.area.position = areaPos;
                for (int j = 0; j < data.level; j++)
                {
                    building.FinishUpgrade();
                }
                
            }
            else if (data.buildingName == "Wood Storage")
            {
                Building building = Instantiate(WoodStoragePrefab, position, Quaternion.identity);
                building.FinishBuilding();
                building.Place();
                building.area.position = areaPos;
                for (int j = 0; j < data.level; j++)
                {
                    building.FinishUpgrade();
                }
            }
            else 
            {
                Building building = Instantiate(HousingPrefab, position, Quaternion.identity);
                building.FinishBuilding();
                building.Place();
                building.area.position = areaPos;
                for (int j = 0; j < data.level; j++)
                {
                    building.FinishUpgrade();
                }
            }

            

            }
            else{
                Debug.LogError("Path not found in " + path + i);
            }
        }
    }
}
