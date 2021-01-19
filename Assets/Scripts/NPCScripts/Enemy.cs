using UnityEngine;
using System.Collections.Generic;
using System;

public class Enemy{

    public string name;
    int HP, attackMin, attackMax, attackBonusChance;
    public int level, armor;
    float attackTimer, attackSwingReset;
    public AdventurerStats.AttackType weaknessType;
    public bool critLastHit = false;
    public Sprite mySprite;
    public int enemyTypeIndex;

    public Enemy(string _name, int _hpMax, int _level, Sprite enemySprite, int typeIndex)
    {
        name = _name;
        HP = UnityEngine.Random.Range(_hpMax, _hpMax * _level);
        level = _level;
        attackMin = UnityEngine.Random.Range(1, _level * 3);
        attackMax = UnityEngine.Random.Range(attackMin, attackMin + _level * 4);
        attackTimer = UnityEngine.Random.Range(1.0f, 5.0f);
        attackSwingReset = UnityEngine.Random.Range(2.0f, 6.0f);
        attackBonusChance = _level;
        armor = UnityEngine.Random.Range(8, 13) + _level;
        var values = Enum.GetValues(typeof(AdventurerStats.AttackType));
        weaknessType = (AdventurerStats.AttackType)UnityEngine.Random.Range(0, values.Length);
        enemyTypeIndex = typeIndex;

        if (enemySprite != null)
            mySprite = enemySprite;
        else
        {
            List<Sprite> critterSheet = new List<Sprite>(Resources.LoadAll<Sprite>("Critters"));
            critterSheet.Add(Resources.Load<Sprite>("rat"));

            if (name.Contains("Rat"))
                mySprite = critterSheet[10];
            else if (name.Contains("Spider"))
                mySprite = critterSheet[0];
            else if (name.Contains("Goblin"))
                mySprite = critterSheet[4];
            else if (name.Contains("Wasp"))
                mySprite = critterSheet[2];
            else if (name.Contains("Serpent"))
                mySprite = critterSheet[3];
            else if (name.Contains("Skeleton"))
                mySprite = critterSheet[7];
            else if (name.Contains("Lizard"))
                mySprite = critterSheet[8];
            else if (name.Contains("Slime"))
                mySprite = critterSheet[9];
            else if (name.Contains("Troll"))
                mySprite = critterSheet[1];
            else
                mySprite = critterSheet[10];
  
            /*  This doesnt work in current unity?
            mySprite = name switch
            {
                string a when a.Contains("Rat") => critterSheet[10],
                string a when a.Contains("Spider") => critterSheet[0],
                string a when a.Contains("Goblin") => critterSheet[4],
                string a when a.Contains("Wasp") => critterSheet[2],
                string a when a.Contains("Serpent") => critterSheet[3],
                string a when a.Contains("Skeleton") => critterSheet[7],
                string a when a.Contains("Lizard") => critterSheet[8],
                string a when a.Contains("Slime") => critterSheet[9],
                string a when a.Contains("Troll") => critterSheet[1],
                _ => critterSheet[10]
            };
            */
        }
               

    }


    public int Attack(int armorValue, bool opportunityAttack)
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0.0 && !opportunityAttack)
        {
            return 0;
        }

        else
        {
            critLastHit = false;
            attackTimer = attackSwingReset;

            int attackRoll = UnityEngine.Random.Range(1, 21);

            if (attackRoll == 20)
            {
                critLastHit = true;
                return UnityEngine.Random.Range(attackMin, attackMax) * 2;
            }

            if (attackRoll + attackBonusChance >= armorValue)
            {
                return UnityEngine.Random.Range(attackMin, attackMax);
            }

            else return 0;
        }
    }

    //returns true if enemy is killed
    public bool ReduceHP(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            return true;
        }

        return false;
    }
}
