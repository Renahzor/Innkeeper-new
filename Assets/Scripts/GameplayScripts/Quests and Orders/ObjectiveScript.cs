using System.Collections;
using System.Collections.Generic;

public class ObjectiveScript{

    public int enemyTypeIndex;
    public string objectiveEnemyName;
    public string objectiveDescription;

    public ObjectiveScript(string name, string desc, int typeIndex)
    {
        objectiveEnemyName = name;
        objectiveDescription = desc;
        enemyTypeIndex = typeIndex;
    }
}
