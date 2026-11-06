using System.Collections.Generic; //allows use of flexible lists that can shrink when we remove used hints.
using UnityEngine;

public class HintManager : MonoBehaviour // Makes script attachable to GameObjects
{
    public TargetManager targetmanager; //Connects the other Script "TargetManager", so the Script can see which round active.

    private List<string> hintsUnused = new List<string>(); 
    //Will create a tempry list to hold hits (elements) not yet displayed.
    private int givenHints = 0; 
    //Tracks the amount of hints the player has been given in a particular round.
    private int maxHints = 5; 
    //Sets a limit for the amount of hints used in one round.

    public void hintsSetup() //Function: Run this to get hints ready
    {
        hintsUnused.Clear(); //emptys out all the leftover hints from the previous round.
        hintsUnused.AddRange(targetmanager.roundCurrent.hints); // Copys Pickles hints into temp list.
        givenHints = 0; //resets counter to zero at the beginning of a new round.
    } 
    
    public void randomHint() //Function: Will run at the beggining to of new round.
    {
        if (givenHints >= maxHints) //Checks if the player has used all their hints.
        {
            Debug.Log("Wow... you have used all the hints and want more?"); //Stops player from getting more hints.
            return; //ends function
        }
        if (hintsUnused.Count == 0) //ensures that we havent accidently ran out of hints.
        {
            Debug.Log("Out of unique hints!"); //Tells the console there is no more lists in the hints.
            return; //ends function.
        }

        int randomIndex = Random.Range(0, hintsUnused.Count);
        //Picks a random position for the remaining unused hints.
        string hintChosen = hintsUnused[randomIndex];
        //Grabs the specific text from that random position.
        Debug.Log("Hint: " + hintChosen); //Displays hint to player.

        hintsUnused.RemoveAt(randomIndex);
        givenHints++;
    } 
} 
