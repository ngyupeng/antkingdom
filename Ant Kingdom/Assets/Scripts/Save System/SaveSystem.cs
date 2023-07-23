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
    [SerializeField] Building TrainingCampPrefab;
    [SerializeField] Building IronStoragePrefab;

    public static List<Building> buildings = new List<Building>();
    public static List<Building> inProgress = new List<Building>();
    public static List<BreedingQueue> breeding = new List<BreedingQueue>();
    public static List<QuestInstance> quests = new List<QuestInstance>();
    public static List<TrainingPanel> training = new List<TrainingPanel>();
    const string RES_SUB = "/resources";
    const string ANT_SUB = "/ants";
    const string BUILDING_SUB = "/buildings";
    const string BUILDING_COUNT_SUB = "/buildings";
    const string DIS_SUB = "/disaster";
    
    void Start()
    {
        CheckGameOver();
        if (!MainMenu.isNewGame && !DisasterSystem.isGameOver) {
            buildings.Clear();
            inProgress.Clear();
            breeding.Clear();
            quests.Clear();
            LoadBuildings();
            LoadAnt();
            LoadResource();
            LoadDisaster();
            DisasterInfo di = GameHandler.FindObjectOfType<DisasterInfo>();
            di.UpdateText();
            
        } else {
            buildings.Clear();
            inProgress.Clear();
            breeding.Clear();
            quests.Clear();
        }
        DisasterSystem.isGameOver = false;
    }

    void OnApplicationQuit() {
        SaveStates();
    }
    public void SaveStates()
    {
        for (int i = 0; i < inProgress.Count; i++)
        {
            inProgress[i].RefundBuilding();
        }
       
        for (int j = 0; j < breeding.Count; j++)
        {
            breeding[j].RefundBreeding();
        }
        
        for (int k = 0; k < quests.Count; k++)
        {
            quests[k].RefundQuest();            
        }
        for (int l = 0; l < training.Count; l++)
        {
            training[l].CancelTraining();
        }
        training.Clear();
        quests.Clear();
        breeding.Clear();
        SaveAnt();
        SaveBuildings();
        SaveResource();
        SaveDisaster();
        
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

    void SaveDisaster()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DIS_SUB;
        FileStream stream = new FileStream(path, FileMode.Create);
        DisasterData data = new DisasterData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    void SaveBuildings()
    {
        Building building = Building.FindObjectOfType<QueenNest>();
        buildings.Add(building);
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
            GameResources.AddResourceAmount(GameResources.ResourceType.Wood, data.resourceAmountData[GameResources.ResourceType.Wood]);
            GameResources.AddResourceAmount(GameResources.ResourceType.Stone, data.resourceAmountData[GameResources.ResourceType.Stone]);
            GameResources.AddResourceAmount(GameResources.ResourceType.Food, data.resourceAmountData[GameResources.ResourceType.Food]);
            GameResources.AddResourceAmount(GameResources.ResourceType.Iron, data.resourceAmountData[GameResources.ResourceType.Iron]);
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
            AntManager.antUnlocked = data.antUnlocked;
        }
        else
        {
            Debug.LogError("Path not found in " + path);
        }
    }

    void CheckGameOver()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DIS_SUB;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            DisasterData data = formatter.Deserialize(stream) as DisasterData;
            stream.Close();
            DisasterSystem.isGameOver = data.isGameOver;
        }
        else
        {
            Debug.LogError("Path not found in " + path);
        }
    }

    void LoadDisaster()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DIS_SUB;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            DisasterData data = formatter.Deserialize(stream) as DisasterData;
            stream.Close();
            DisasterSystem.isGameOver = data.isGameOver;
            DisasterSystem.minDamage = data.minDamage;
            DisasterSystem.maxDamage = data.maxDamage;
            DisasterSystem.timeToNextDisaster = data.timeToNextDisaster;
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
            else if (data.buildingName == "Queen Nest")
            {
                Building building = Building.FindObjectOfType<QueenNest>();
                building.transform.position = position;
                building.area.position = areaPos;
            }
            else if (data.buildingName == "Iron Storage")
            {
                Building building = Instantiate(IronStoragePrefab, position, Quaternion.identity);
                building.FinishBuilding();
                building.Place();
                building.area.position = areaPos;
                for (int j = 0; j < data.level; j++)
                {
                    building.FinishUpgrade();
                }
            }
            else if (data.buildingName == "Training Camp")
            {
                Building building = Instantiate(TrainingCampPrefab, position, Quaternion.identity);
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
