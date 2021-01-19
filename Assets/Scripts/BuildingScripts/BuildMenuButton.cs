using UnityEngine;
using UnityEngine.UI;

public class BuildMenuButton : MonoBehaviour {

    public int itemIndexToBuild;
    Text itemNameText;
    Text itemCostText;

    public void StartBuildItem()
    {
        GetComponentInParent<BuildingMaster>().BuildItem(itemIndexToBuild);
    }

    public void SetupButton(int i)
    {
        itemIndexToBuild = i;

        itemNameText = transform.Find("ItemName").GetComponent<Text>();
        itemCostText = transform.Find("Cost").GetComponent<Text>();

        itemNameText.text = GetComponentInParent<BuildingMaster>().itemPrefabs[itemIndexToBuild].name;
        itemCostText.text = GetComponentInParent<BuildingMaster>().itemPrefabs[itemIndexToBuild].GetComponent<BuildableObject>().buildCost.ToString() + "g";

        //Workaround for colors over textures, temporary
        if (i > 2)
        {
            itemNameText.color = Color.white;
            itemCostText.color = Color.white;
        }
    }
    
}
