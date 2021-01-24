using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutingUIScript : MonoBehaviour {

    public static ScoutingUIScript Instance { get; private set; }

    [SerializeField]
    Text rewardText;
    [SerializeField]
    Dropdown heroDropdown, levelDropdown;

    List<Adventurer> activeScoutHeroes = new List<Adventurer>();

    int rewardValue = 0;
    int scoutingMissionsCompleted = 1;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}

    void Update()
    {
        int prevreward = rewardValue;

        if (heroDropdown.value >= 0 && levelDropdown.value >= 0)
            rewardValue = activeScoutHeroes[heroDropdown.value].GetComponent<AdventurerStats>().level * (levelDropdown.value + 1) * 10 * scoutingMissionsCompleted;

        if (rewardValue != prevreward)
            rewardText.text = rewardValue.ToString();
    }

    public void UpdateDropdowns(List<Adventurer> currentActiveHeroes)
    {
        heroDropdown.ClearOptions();
        activeScoutHeroes = currentActiveHeroes;
        List<string> heroNames = new List<string>();
        int maxLevel = 1;
        foreach (Adventurer hero in activeScoutHeroes)
        { 
            heroNames.Add(hero.GetComponent<AdventurerStats>().advName);
            if (hero.GetComponent<AdventurerStats>().level > maxLevel)
                maxLevel = hero.GetComponent<AdventurerStats>().level;
        }

        heroDropdown.AddOptions(heroNames);

        List<string> levelSelect = new List<string>();
        for (int i = 1; i < maxLevel + 3; i++)
            levelSelect.Add(i.ToString());

        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(levelSelect);
    }

    public void UpdateSelection(int index)
    {
        heroDropdown.itemText.text = activeScoutHeroes[index].name;
    }

    public void StartScouting()
    {
        if (heroDropdown.value < 0 || levelDropdown.value < 0 || Player.Instance.playerGold < rewardValue)
        {
            GameMaster.Instance.SendGameMessage("Not enough Gold to start scouting mission");
            return;
        }

        activeScoutHeroes[heroDropdown.value].ScoutingRequest(levelDropdown.value + 1);
        GameMaster.Instance.SendGameMessage("Spent " + rewardValue + "g to send " + heroDropdown.GetComponentInChildren<Text>().text + " on a scouting mission.");
        Player.Instance.playerGold -= rewardValue;
        scoutingMissionsCompleted++;

        UpdateDropdowns(activeScoutHeroes);
    }

}
