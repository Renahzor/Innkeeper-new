using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript{

    public string locationName { get; private set; }
    public string locationDecription { get; private set; }

    int averageEnemyLevel;
    public int enemyDensity;
    float travelDistance;

    public List<int> enemiesPresent = new List<int>();
    List<Enemy> legendaryEnemies = new List<Enemy>();

    public LocationScript(string locName, string locDesc)
    {
        locationName = locName;
        locationDecription = locDesc;
    }

    public void LocationSetup(int enemyLevel, int density, float distance, int enemyTypeIndex)
    {
        averageEnemyLevel = enemyLevel;
        enemyDensity = density;
        travelDistance = distance;
        enemiesPresent.Add(enemyTypeIndex);
    }
    
    public void AddEnemyToLocation(int enemyTypeIndex)
    {
        enemiesPresent.Add(enemyTypeIndex);
    }

    public List<Enemy> GetEnemyList(int enemyIndexAtLocation, Quest q, string enemyName)
    {
        List<Enemy> tmp = new List<Enemy>();
        int numberOfEnemies = Random.Range(1, (3 + (enemyDensity / 20)));
        for (int i = 0; i < numberOfEnemies; i++)
        {
            tmp.Add(new Enemy(enemyName + " " + (i + 1), (averageEnemyLevel * Random.Range(3, 7)), Random.Range(averageEnemyLevel - 1, averageEnemyLevel + 2), null, q.objectiveIndex));
        }

        return tmp;
    }
}
