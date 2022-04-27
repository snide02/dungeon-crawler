using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKey : MonoBehaviour
{
    public int credits = 1;
    private Vector3 chestPos;
    private bool dropOnce = false;
    public int chestHealth = 1;


    [Header("Key")]
    [SerializeField] protected Key keyPrefab;

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
            InstantiateKey();
            Destroy(gameObject);

            dropOnce = true;
        }
    }

    protected virtual void InstantiateKey()
    {
        chestPos = gameObject.transform.position;
        var key = Instantiate(keyPrefab.gameObject, chestPos, Quaternion.identity);
        key.GetComponent<Key>();
    }

}
