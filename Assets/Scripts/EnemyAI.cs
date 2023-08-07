// using UnityEngine;
// using System.Collections;

// public class EnemyChase : MonoBehaviour {

//     private Transform Player;
//     private UnityEngine.AI.NavMeshAgent NMA;

// 	void Start () 
//         {
//                 Player = GameObject.FindGameObjectWithTag("Player").transform;
//                 NMA = GetComponent<UnityEngine.AI.NavMeshAgent>();
// 	}
	
	
// 	void Update () 
//         {
//                 NMA.SetDestination(Player.position);
// 	}
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{       
        public NavMeshAgent ai;
        public List<Transform> destinations;
        public int destinationsCount;
        public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
        public bool walking, chasing;
        public Transform player;
        Transform currentDest;
        Vector3 dest;
        int randNum;
        public Vector3 rayCastOffset;
        public string deathScene;
        private int destinationsLayer;
        private int destinationsNum;

        void Start()
        {
                walking = true;
                destinationsNum = 0;
                currentDest = destinations[destinationsNum];
        }

        void Update()
        {
                Vector3 direction = (player.position - transform.position).normalized;
                RaycastHit hit;
                if(Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
                {
                        if(hit.collider.gameObject.tag == "Player")
                        {
                                walking = false;
                                StopCoroutine("stayIdle");
                                StopCoroutine("chaseRoutine");
                                StartCoroutine("chaseRoutine");
                                chasing = true;
                        }
                }
                if(chasing == true)
                {
                        ai.destination = player.position;
                        ai.speed = chaseSpeed;
                        if (ai.remainingDistance <= catchDistance)
                        {
                                player.gameObject.SetActive(false);
                                StartCoroutine(deathRoutine());
                                chasing = false;
                        }
                }
                if(walking == true)
                {
                        ai.destination = destinations[destinationsNum].position;
                        ai.speed = walkSpeed;
                        if (ai.remainingDistance <= ai.stoppingDistance)
                        {
                                ai.speed = 0;
                                StopCoroutine("stayIdle");
                                StartCoroutine("stayIdle");
                                walking = false;
                        }
                        
                }
        }
        IEnumerator stayIdle()
        {
                idleTime = Random.Range(minIdleTime, maxIdleTime);
                yield return new WaitForSeconds(idleTime);
                destinationsNum++;
                if (destinationsNum == destinationsCount)
                {
                        destinationsNum = 0;       
                }
                ai.destination = destinations[destinationsNum].position;
                walking = true;
        }
        IEnumerator chaseRoutine()
        {
                chaseTime = Random.Range(minChaseTime, maxChaseTime);
                yield return new WaitForSeconds(chaseTime);
                walking = true;
                chasing = false;
                currentDest = destinations[destinationsNum];
        }
        IEnumerator deathRoutine()
        {
                yield return new WaitForSeconds(jumpscareTime);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
}