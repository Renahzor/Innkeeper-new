﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActiveHeroPanel : MonoBehaviour {

    public static ActiveHeroPanel Instance { get; private set; }
    Dictionary<Adventurer, GameObject> activeHeroes = new Dictionary<Adventurer, GameObject>();
    public GameObject activeHeroInfo;

    void Awake()
    {
        Instance = this;
    }

	public void AddHero(Adventurer a)
    {
        if (activeHeroes.ContainsKey(a))
            return;
        else
        {
            GameObject temp = Instantiate(activeHeroInfo);
            temp.transform.SetParent(this.transform, false);
            activeHeroes.Add(a, temp);
            UpdateHeroStats(a);
            a.GetComponent<NPCBehaviors>().healthBar = temp.transform.Find("HealthBar").GetComponent<Image>();
        }
        ScoutingUIScript.Instance.UpdateDropdowns(new List<Adventurer>(activeHeroes.Keys));
    }

    public void UpdateHeroStats(Adventurer a)
    {
        List<Text> textComponents = new List<Text>();
        activeHeroes[a].GetComponentsInChildren<Text>(textComponents);

        foreach(Text t in textComponents)
        {
            if (t.name == "HeroName")
                t.text = a.gameObject.GetComponent<AdventurerStats>().advName;
            else if (t.name == "Activity")
                t.text = a.GetComponent<StateTracker>().advActivity;
            else if (t.name == "Time")
                t.text = a.GetComponent<StateTracker>().activityTime.ToString("F2");
        }
        ScoutingUIScript.Instance.UpdateDropdowns(new List<Adventurer>(activeHeroes.Keys));
    }

    public void RemoveHero(Adventurer a)
    {
        if (activeHeroes.ContainsKey(a))
        {
            GameObject.Destroy(activeHeroes[a]);
            activeHeroes.Remove(a);
        }

        ScoutingUIScript.Instance.UpdateDropdowns(new List<Adventurer>(activeHeroes.Keys));
    }

    public int NumberOfHeroesActive()
    {
        return activeHeroes.Count;
    }

    public Adventurer AdventurerInSlot(GameObject go)
    {
        //Values in activeHeroes are necessarily unique, we can return a key form the first value that matches
        if (activeHeroes.ContainsValue(go))
        {
            foreach(KeyValuePair<Adventurer, GameObject> kvp in activeHeroes)
            {
                if (kvp.Value == go)
                    return kvp.Key;

            }
        }

        return null;
    }

    //workaround method for seeding the hero panel on initialization
    public Adventurer ReturnHeroByIndex(int i)
    {
        int j = 0;
        foreach (KeyValuePair<Adventurer, GameObject> kvp in activeHeroes)
        {
            if (j == i)
                return kvp.Key;
        }

        return null;
    }
}
