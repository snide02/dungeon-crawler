using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKey : MonoBehaviour
{
    public int credits = 1;
    private Vector3 enemyPos;
    private bool dropOnce = false;
    public int currHealth;


    [Header("Key")]
    [SerializeField] protected Key keyPrefab;
    //[SerializeField] protected int keyVal;

    void Start()
    {
        //currHealth = GetComponent<EnemyPath>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currHealth.updatehealth(1);
            DropKey();
        }
    }

    public void DropKey()
    {

        if (currHealth.health <= 0 && dropOnce == false)
        {
            InstantiateKey();

            dropOnce = true;
        }
    }

    protected virtual void InstantiateKey()
    {
        enemyPos = gameObject.transform.position;
        var rndX = Random.Range(0, 3);
        var rndY = Random.Range(0, 3);
        enemyPos.x += rndX;
        enemyPos.y += rndY;
        var key = Instantiate(key.gameObject);//, enemyPos, Quaternion.identity);
        key.GetComponent<Key>();
    }

}
