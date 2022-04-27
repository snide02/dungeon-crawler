using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPotions : MonoBehaviour
{
    private Vector3 chestPos;
    private bool dropOnce = false;
    public int chestHealth = 1;


    //[Header("Potion")]
    //[SerializeField] protected  keyPrefab;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            chestHealth--;
            Drop();
        }
    }

    public void Drop()
    {

        if (chestHealth <= 0 && dropOnce == false)
        {
            InstantiatePotions();
            Destroy(gameObject);

            dropOnce = true;
        }
    }

    protected virtual void InstantiatePotions()
    {
        chestPos = gameObject.transform.position;
        //var key = Instantiate(keyPrefab.gameObject, chestPos, Quaternion.identity);
        //key.GetComponent<Key>();
    }
}
