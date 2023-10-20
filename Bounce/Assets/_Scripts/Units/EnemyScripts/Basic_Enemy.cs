using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Basic_Enemy : Unit
{
    // public float chaseRange = 10f;
    // public float maxChaseRange = 2f;
    private Transform target;
    private float minAmbientRange = 2.5f;
    private Vector3 targetDirection;
    private Vector3 startingPosition;
    private Vector2 movementDirection;
    private GameObject player;
    private float chaseTimer = 0f;

    private float directionTime = 0f;

    public void Awake()
    {
        startingPosition = transform.position;
        targetDirection = Random.insideUnitCircle.normalized;
        player = GameObject.FindWithTag("Player");
        target = player.transform;
    }
    
    public void Update()
    {
        movementDirection = new Vector2(target.position.x -  transform.position.x, target.position.y -  transform.position.y );
        float playerEnemyDistance = Vector3.Distance(transform.position, target.position);
        // chase player if inbetween range of maxChaseRange/chaseRange
        if (playerEnemyDistance <= chaseRange && playerEnemyDistance>= maxChaseRange  && checkCollision(movementDirection)==false )
        {
            // transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            // startingPosition = transform.position;
            // chaseTimer += Time.deltaTime;
        }
        // ambient movement if not in range of player
        else if(playerEnemyDistance>= maxChaseRange)
        { 
            // Calculate the distance from the starting position
            float distanceFromStart = Vector3.Distance(transform.position, startingPosition);

            // Check if the unit is beyond the maximum range
            if (distanceFromStart > minAmbientRange)
            {
                // Move the unit back towards the starting position
                /*Vector3 moveDirection = (startingPosition - transform.position).normalized;
                MoveUnit(moveDirection * Time.deltaTime* speed*4);*/

                // Set the target direction back to the starting position
                targetDirection = (startingPosition - transform.position).normalized;
                directionTime = Random.Range(0.5f, 1.5f);
            }

            // Universal move unit
            MoveUnit(targetDirection * Time.deltaTime * speed * 4);
            chaseTimer = 0f;

            // If the unit has been moving in the same direction for too long, generate a new random direction
            if (directionTime < 0f)
            {
                targetDirection = Random.insideUnitCircle.normalized;
                directionTime = Random.Range(0f, 1f);
            }

            directionTime -= Time.deltaTime;
        }
        //calculate starting position for enemy to tend towards during ambient movement
        if (Vector3.Distance(transform.position, target.position) <= maxChaseRange && chaseTimer >= 2.5f)
        {
            startingPosition = transform.position;
            chaseTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if(health - damage <=0)
        {
            Destroy(gameObject);
        }
        else
        {
            health-= damage;
        }
    }
}