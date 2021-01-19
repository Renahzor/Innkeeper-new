﻿using UnityEngine;

public class StateTracker : MonoBehaviour{

    public enum States { Idle, WantsFood, WantsDrink, WantsQuest, WantsSocial, WantsHealth, HasScoutingMission }

    public GameObject objectCurrentlyTouching = null;

    States state = States.Idle;

    //state tracking
    public bool wantsQuest, hasActivity, hasScoutingRequest = false;

    public string advActivity = "Relaxing...";
    public float activityTime = 0.0f;
    public float attackTimer = 1.0f;

    public Quest currentQuest = null;

    void Start()
    {
        UpdateState(GetComponent<AdventurerNeeds>());
    }

    public States GetCurrentState()
    {
        return state;
    }

    public void UpdateState(AdventurerNeeds needs)
    {
        state = States.Idle;

        float tmp = 85.0f;
        /*if (needs.socialNeed < tmp)
        {
            state = States.WantsSocial;
            tmp = needs.socialNeed;
        }*/

        if (needs.questNeed < tmp)
        {
            state = States.WantsQuest;
            tmp = needs.questNeed;
        }

        if (state == States.WantsQuest && GameMaster.Instance.GetComponent<ActiveQuestManager>().activeQuests.Count == 0)
        {
            state = States.Idle;
            tmp = 85.0f;
        }

        if (needs.drinkNeed < tmp)
        {
            state = States.WantsDrink;
            tmp = needs.drinkNeed;
        }

        if (needs.foodNeed <= tmp)
        {
            state = States.WantsFood;
            tmp = needs.foodNeed;
        }

        if (hasScoutingRequest)
            state = States.HasScoutingMission;
    }

    public void ResetState()
    {
        state = States.Idle;
        advActivity = "Looking For Activity";
        activityTime = 0.0f;
    }
}
