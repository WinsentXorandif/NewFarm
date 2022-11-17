using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlantData", menuName = "Plant Data")]
public class PlantData : ScriptableObject
{
    public plantDataList allPlantData;

    [Serializable]
    public struct plantDataStruct
    {
        public EPlants namePlant;
        public float growthTime;
        public int experience;
        public GameObject smollPlant;
        public GameObject bigPlant;
    }

    [Serializable]
    public struct plantDataList
    {
        public List<plantDataStruct> plantList;
    }

}
