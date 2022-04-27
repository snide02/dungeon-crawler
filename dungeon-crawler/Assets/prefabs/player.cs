using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{  
    public int healthPoints=20; //health points
    public int attackPoints=3;//atttack points
    private int manaPoints=5; //mana points
    public int damageAmount=1;//determines how much damage the player does
    //vector<int> moveSpeed;

    //5 health when player moves, lasts for 10 turns)
      public bool isBleed;
      public bool isWebbed;
     public bool inRange; //checks to see if enemy is in range for player to move
    public bool playerTurn;    //checks to see if player turn
    public bool commit; //changes when player decides that is where they want to be before they end their turn
    public bool normalAttack;
    private bool skillAttack;
     
    private int mpCost=1;
    private bool inCombat;
  public TestPlayerController movement;
    public GridOccupant positionUpdate;
    
    // Start is called before the first frame update
    
        void Start()
        {
            positionUpdate = GetComponent<GridOccupant>();
         }

    // Update is called once per frame
    void Update()
    {
      
      if(inCombat==true)
      {

      
            
            if(skillAttack==true)
            {
              if(attackPoints>2)
              {  
                if(manaPoints > mpCost)
                {
                    //dmg boss 
                    manaPoints-=mpCost;
                    attackPoints-=2; 
                    if (isBleed==true)
                {healthPoints-=1;}
                }
               
                else
                {
                    //cout<<"Not enough Mana Points"<<endl;
                }
              }
              else
              {
                  //cout<<"Not Enough Attack Points"<<endl;
              }
            
              if(normalAttack==true)
              {
              if(attackPoints>1)
                {    //dmg boss 
                    manaPoints-=mpCost;
                    attackPoints-=1;
                    if (isBleed==true)
                {healthPoints-=1;}
                }
              else
              {
                    //cout<<"Not Enough Attack Points"<<endl;
              }
            }
            GameManager.TurnOrderManager.ExecuteTurns();
        }      
    
     
     
     
    
      else
      {
        if (isBleed==true)
        {
        healthPoints-=1;
        }
      }
    }
  }
}//for the public class