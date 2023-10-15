using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class ProvinceStats
{

    public Statistic lifePoints;
    public Statistic population;
    public Statistic warriors;
    public Statistic movementPoints;

  //   public Statistic coins;
  //public Statistic developmentPoints;


    public int index;
    public bool isSea;
    public int provinceOwnerIndex  = -1;// -1 == null , 0 is Player, >0 is Computer

    public int unitsCounter;

    public int buildingIndex = -1;

    [SerializeField]public Dictionary<int, int> units;

    [SerializeField]public List<int> neighbors = new List<int>();
    public void AddNeighbor(int index)
    {
        neighbors.Add(index);
    }
    public ProvinceStats(int index, float scienceDevelopment, float incomeInCoins, bool isSea)
    {
        this.index = index;
        this.provinceOwnerIndex = -1;
        this.unitsCounter = 0;
        this.units = new Dictionary<int, int>();
        buildingIndex = -1;
        this.isSea = isSea;
    }  
    public ProvinceStats()
    {

    }
    public void CopyData(ProvinceStats provinceStats)
    {
        population = new Statistic(Random.Range(100, 120), 0.5f,null, "Population");
        lifePoints = new Statistic(10, "LifePoint");
        warriors = new Statistic(5, "Warrior");
        movementPoints = new Statistic(2,"MovementPoint");

     //   developmentPoints = new Statistic(0, "DevelopmentPoint");
     //   developmentPoints.AddBonus(-100, new Bonus("Population", (float multiplier) => { return (int)population.value * multiplier; }, (float multiplier) => { return "";},0.01f));

    //    coins = new Statistic(0, "Coin");
    //    coins.AddBonus(-100,new Bonus("Population", (float multiplier) => { return (int)population.value * multiplier; }, (float multiplier) => { return ""; }, 0.1f));


        isSea = provinceStats.isSea;
        provinceOwnerIndex = provinceStats.provinceOwnerIndex;
        unitsCounter = provinceStats.unitsCounter;
        buildingIndex = provinceStats.buildingIndex;
        neighbors = provinceStats.neighbors;
        index = provinceStats.index;

        if(!provinceStats.isSea && provinceStats.provinceOwnerIndex == -1)
        {
            this.units = new Dictionary<int, int>();
            if (Random.Range(0, 4) != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    int number = Random.Range(0, 3);
                    int unitIndex = Random.Range(0, GameAssets.Instance.unitStats.Length);
                    if (units.ContainsKey(unitIndex))
                    {
                        units[unitIndex] = units[unitIndex] + number;
                    }
                    else
                    {
                        units.Add(unitIndex, number);
                    }

                    unitsCounter = unitsCounter + number;
                }
                GameManager.Instance.UpdateUnitCounter(this.index);
            }
        }
    }
    public void SetNewOwner(int index)
    {
        if (index == 0)
        {
            GameManager.Instance.humanPlayer.warriors.limit += warriors.value;
            GameManager.Instance.humanPlayer.movementPoints.limit += movementPoints.value;
            
            UIManager.Instance.UpdateCounters();
        }
        provinceOwnerIndex = index;
    }
    public void NextTurn()
    {
        population.NextTurn();
    }
}
