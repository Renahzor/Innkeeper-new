//Holds info for current quests for heroes
public class Quest{

    public int locationIndex { get; private set; } 
    public int objectiveIndex { get; private set; } 
    public int objectiveQuantity { get; private set; }  
    public int goldReward { get; private set; }

    public int timesCompleted { get; private set; }

    public Quest(int _locationIndex, int _objectiveIndex, int _goldReward)
    {
        locationIndex = _locationIndex;
        objectiveIndex = _objectiveIndex;
        goldReward = _goldReward;
    }
}
