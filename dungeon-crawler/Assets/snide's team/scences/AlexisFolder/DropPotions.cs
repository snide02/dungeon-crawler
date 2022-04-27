using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPotions : MonoBehaviour
{
    private Vector3 chestPos;
    private bool dropOnce = false;
    public int chestHealth = 1;


    [Header("HealthPotion")]
    [Header("StrengthPotion")]
    [Header("APPotion")]
    [SerializeField] protected HealthPotion healthPotionPrefab;
    [SerializeField] protected StrengthPotion strengthPotionPrefab;
    [SerializeField] protected APPotion apPotionPrefab;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        chestPos.x = chestPos.x + 1;
        var health = Instantiate(healthPotionPrefab.gameObject, chestPos, Quaternion.identity);
        health.GetComponent<HealthPotion>();
        chestPos = gameObject.transform.position;
        var strength = Instantiate(strengthPotionPrefab.gameObject, chestPos, Quaternion.identity);
        strength.GetComponent<StrengthPotion>();
        chestPos.x = chestPos.x - 1;
        var ap = Instantiate(apPotionPrefab.gameObject, chestPos, Quaternion.identity);
        ap.GetComponent<APPotion>();
    }
    protected virtual void InstantiateAPPotion()
    {
        chestPos = gameObject.transform.position;
        var ap = Instantiate(apPotionPrefab.gameObject, chestPos, Quaternion.identity);
        ap.GetComponent<APPotion>();
    }
    protected virtual void InstantiateHealthPotion()
    {
        chestPos = gameObject.transform.position;
        var health = Instantiate(healthPotionPrefab.gameObject, chestPos, Quaternion.identity);
        health.GetComponent<HealthPotion>();
    }
    protected virtual void InstantiateStrengthPotion()
    {
        chestPos = gameObject.transform.position;
        var strength = Instantiate(strengthPotionPrefab.gameObject, chestPos, Quaternion.identity);
        strength.GetComponent<StrengthPotion>();
    }
}
