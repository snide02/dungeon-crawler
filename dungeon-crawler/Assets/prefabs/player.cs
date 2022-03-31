using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{ 
    
    int healthPoints=20; //health points
    int attackPoints=3;//atttack points
    int manaPoints=5; //mana points
    int damageAmount=1;//determines how much damage the player does
    vector<int> moveSpeed;

    
     bool inRange; //checks to see if enemy is in range for player to move
    bool playerTurn;    //checks to see if player turn
    bool commit; //changes when player decides that is where they want to be before they end their turn
    bool normalAttack;
    bool skillAttack;
    int mpCost=1;
    // Start is called before the first frame update
    
        void Start()
        {
       
    
         }

    // Update is called once per frame
    void Update()
    {
        
        if(playerTurn== true)
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
                }
                else
                {
                    cout<<"Not enough Mana Points"<<endl;
                }
              }
              else
              {
                  cout<<"Not Enough Attack Points"<<endl;
              }
            }
            if(normalAttack==true)
            {
              if(attackPoints>1)
                {    //dmg boss 
                    manaPoints-=mpCost;
                    attackPoints-=1;
                }
              else
              {
                  cout<<"Not Enough Attack Points"<<endl;
              }
            }
           

        }
            

        
    }
}
