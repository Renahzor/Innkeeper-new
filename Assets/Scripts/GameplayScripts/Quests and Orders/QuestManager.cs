using UnityEngine;
using System.Collections.Generic;

//Class for storage of quest options, generation of stats, etc
public class QuestManager : MonoBehaviour {

    public List<string> questLocationsTextList = new List<string>();
    public List<string> questObjectivesTextList = new List<string>();

    List<LocationScript> locations = new List<LocationScript>();
    List<ObjectiveScript> objectives = new List<ObjectiveScript>();
    List<LocationScript> activeLocations = new List<LocationScript>();

    public TextAsset locationsText;
    public TextAsset locationsDescText;
    public TextAsset enemyNames;
    public TextAsset objectivesDescText;

    public int currentLevel = 1;

    int locationIndexPrev = -1;

    QuestWindowController questWindow;

    void Awake()
    {
        questWindow = GameObject.Find("QuestWindow").GetComponent<QuestWindowController>();
        var locationList = locationsText.text.Split('\n');
        var locationDescList = locationsDescText.text.Split('\n');
        int i = 0;
        foreach (var line in locationList)
        {
            locations.Add(new LocationScript(line, locationDescList[i]));
            i++;
        }

        var nameList = enemyNames.text.Split('\n');
        var objectiveDescList = objectivesDescText.text.Split('\n');
        i = 0;
        foreach (var line in nameList)
        {
            objectives.Add(new ObjectiveScript(line, objectiveDescList[i], i));
            i++;
        }

        ScoutedLocationComplete(1, 20.0f);
    }

    void Update()
    {
        //updates the objectives available at each location
        if (locationIndexPrev != questWindow.questLocationDropdown.value)
        {
            List<string> objectivesAtLocation = new List<string>();
            foreach (int i in activeLocations[questWindow.questLocationDropdown.value].enemiesPresent)
            {
                objectivesAtLocation.Add(objectives[i].objectiveEnemyName);
            }

            questWindow.UpdateDropdownText(questWindow.questObjectiveDropdown, objectivesAtLocation);
        }

        //keeps track of dropdown values and updates if location changes    
        locationIndexPrev = questWindow.questLocationDropdown.value;
    }

    public string GetQuestDescription(int locationIndex, int objectiveIndex)
    {
        if (locationIndex == -1 || objectiveIndex == -1)
            return "Select Options Above";

        return "Send heroes to the " + questLocationsTextList[locationIndex] + " to retrieve some " + questObjectivesTextList[objectiveIndex];
    }

    public List<Enemy> GenerateEnemyList(Quest q)
    {
        string enemyName = objectives[activeLocations[questWindow.questLocationDropdown.value].enemiesPresent[questWindow.questObjectiveDropdown.value]].objectiveEnemyName;
           
        return activeLocations[questWindow.questLocationDropdown.value].GetEnemyList(questWindow.questObjectiveDropdown.value, q, enemyName);
    }

    public void ScoutedLocationComplete(int level, float distance)
    {
        
        int locationIndex = Random.Range(0, locations.Count);
        int objectiveIndex = Random.Range(0, objectives.Count);

        if (locations.Count != 0)
        {
            foreach (LocationScript ls in activeLocations)
            {
                if (ls.locationName == locations[locationIndex].locationName)
                {
                    ls.AddEnemyToLocation(objectiveIndex);
                    questObjectivesTextList.Add(objectives[objectiveIndex].objectiveDescription);
                    questWindow.UpdateDropdownText(questWindow.questObjectiveDropdown, questObjectivesTextList);
                    return;
                }
            }
        }

        locations[locationIndex].LocationSetup(level, Random.Range(1, 100), distance, objectiveIndex);

        activeLocations.Add(locations[locationIndex]);

        questLocationsTextList.Add(locations[locationIndex].locationName);
        questObjectivesTextList.Add(objectives[objectiveIndex].objectiveDescription);

        questWindow.UpdateDropdownText(questWindow.questLocationDropdown, questLocationsTextList);
        questWindow.UpdateDropdownText(questWindow.questObjectiveDropdown, questObjectivesTextList);
    }
}
