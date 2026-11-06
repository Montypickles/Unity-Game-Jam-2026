using UnityEngine;
[CreateAssetMenu(fileName = "NewNumberData", menuName = "Game Jam/Number Data")] //This will help you find this script easily in the editor
public class NumberData : ScriptableObject
{
    public int luckyNumber; //This is the variable that will hold the lucky number

    [TextArea(2, 5)]
    public string[] hints; //This is the variable that will hold the hints for the lucky number

    [TextArea(2, 5)]
    public string[] insults; 

    [TextArea(2, 5)]
    public string[] successReply;
}