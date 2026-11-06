using UnityEngine;

public class TargetManager : MonoBehaviour { //Makes the script be attachable to a physical object in the Unity Scene.
    public NumberData[] roundAvailable; //Creating a file for Pickles to drop the random allocated numbers into.

    public NumberData roundCurrent; //Stores the specific file that will store the current Lucky Number

    void Start() //Will run automatically when the game starts
    {
        SelectRandomRound(); //Triggers the custom logic, in order to pick the level (the lucky number)
    }
    
    public void SelectRandomRound() //Function: randomly picks the game.
    {
        int randomIndex = Random.Range(0, roundAvailable.Length); //Picks a random number between 0 and how many files Pickles inserted.
        roundCurrent = roundAvailble[randomIndex]; //Grabs the "winning" file and locks it in for the round.
    }
} 