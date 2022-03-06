using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    public Grid Grid;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3? targetLocation = null;


        if (Input.GetKeyDown(KeyCode.A)) // 0 - left button; 1 - right button; 2 - middle button
        {

            Vector3Int gridLocation = Grid.LocalToCell(transform.position);
            gridLocation.x -= 1;
            targetLocation = Grid.CellToLocal(gridLocation);

        }

        if (Input.GetKeyDown(KeyCode.D)) // 0 - left button; 1 - right button; 2 - middle button
        {

            Vector3Int gridLocation = Grid.LocalToCell(transform.position);
            gridLocation.x += 1;
            targetLocation = Grid.CellToLocal(gridLocation);

        }

        if (Input.GetKeyDown(KeyCode.W)) // 0 - left button; 1 - right button; 2 - middle button
        {

            Vector3Int gridLocation = Grid.LocalToCell(transform.position);
            gridLocation.y+= 1;
            targetLocation = Grid.CellToLocal(gridLocation);

        }


        if (Input.GetKeyDown(KeyCode.S)) // 0 - left button; 1 - right button; 2 - middle button
        {

            Vector3Int gridLocation = Grid.LocalToCell(transform.position);
            gridLocation.y -= 1;
            targetLocation = Grid.CellToLocal(gridLocation);

        }

        if (Input.GetKeyDown(KeyCode.Space)) // 0 - left button; 1 - right button; 2 - middle button
        {

            Rigidbody2D body = GetComponent<Rigidbody2D>();

            body.AddForce(new Vector2(0.1f, 0.1f));

        }


        if (targetLocation.HasValue) {
            transform.position = targetLocation.Value;
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    { 
        DoorTeleportDestination destination = other.GetComponent<DoorTeleportDestination>();
        transform.position = destination.teleportDestination;
        Debug.Log("Collision????????");
    }
}
