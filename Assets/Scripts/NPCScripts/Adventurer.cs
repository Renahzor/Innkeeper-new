﻿
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Adventurer : MonoBehaviour {

    float stateCheckCooldown = 5.0f;

    StateTracker state;
    NPCBehaviors behaviors;
    AdventurerNeeds needs;
    AdventurerStats stats;

    int scoutingRequestLevel;

    //creates a new random adventurer
    void Awake()
    {
        NewAdventurer(0);
    }

    void NewAdventurer(int _level)
    {
        stats = GetComponent<AdventurerStats>();
        stats.SetStats(_level, GameMaster.Instance.names.GetName());
        state = GetComponent<StateTracker>();
        behaviors = GetComponent<NPCBehaviors>();
        needs = GetComponent<AdventurerNeeds>();
    }

    void Start()
    {
        CheckStateImmediate();
    }

    void Update()
    {
        stateCheckCooldown -= Time.deltaTime;

        if (stateCheckCooldown <= 0.0f)
        {
            state.UpdateState(needs);
            stateCheckCooldown = 5.0f;
        }

        //Check for health first, TODO: Add health from other sources
        if (stats.HP < stats.maxHP * 0.8 && !state.hasActivity)
        {
            state.hasActivity = true;
            StartCoroutine(behaviors.MoveTo(NPCBehaviors.InteractableObjects.Bed));
        }

        //if no activity, find an activity that most interests the npc
        if (!state.hasActivity)
        {
            switch(state.GetCurrentState())
            {
                case StateTracker.States.WantsQuest:
                    behaviors.GetQuest();
                    break;                   
                case StateTracker.States.WantsFood:
                    StartCoroutine(behaviors.MoveTo(NPCBehaviors.InteractableObjects.Table));
                    break;
                case StateTracker.States.WantsDrink:
                    StartCoroutine(behaviors.MoveTo(NPCBehaviors.InteractableObjects.Bar));
                    break;
                case StateTracker.States.Idle:
                    StartCoroutine(behaviors.MoveTo(NPCBehaviors.InteractableObjects.IdleActivity));
                    break;
                case StateTracker.States.HasScoutingMission:
                    StartCoroutine(behaviors.RunScouting(scoutingRequestLevel));
                    break;
                default: break;   
            }
        }
    }

    public void CheckStateImmediate()
    {
        state.UpdateState(needs);
        stateCheckCooldown = 5.0f;
    }

    public void CompleteScoutingMission()
    {
        state.hasScoutingRequest = false;
    }

    public void ScoutingRequest(int level)
    {
        scoutingRequestLevel = level;
        state.hasScoutingRequest = true;
    }
}