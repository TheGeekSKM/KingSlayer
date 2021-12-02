using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    [Header("StateMachine Connections")]
    [SerializeField] CombatStateMachine _stateMachine;
    [SerializeField] PlayerCardSelectState _pCSState;
    [SerializeField] EnemyTurnCombatState _eTCState;
    [SerializeField] EnterCombatState _eCState;
    [SerializeField] HealthGameController _hgController;

    [Header("GameObject Connections")]
    [SerializeField] Transform _playerTransform;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] LayerMask _playerMask;
 
    [Header("Own Connections")]
    [SerializeField] Health _enemyHealth;
    [SerializeField] Transform _enemyTransform;
    [SerializeField] NavMeshAgent _navMesh;

    [Header("Editable Values")]
    [SerializeField] private bool useFootSteps = true;

    [Header("Sound Variables")]
    [SerializeField] private float _baseSoundSpeed = 0.5f;
    [SerializeField] private AudioSource _soundSource = default;
    [SerializeField] private List<AudioClip> _soundClipsList = new List<AudioClip>();
    private float footStepTimer = 0f;

    public NavMeshAgent Agent
    {
        get
        {
            return _navMesh;
        }
        set
        {
            _navMesh = value;
        }
    }

    //Enemy Patrolling State
    [SerializeField] Vector3 walkLocation;
    [SerializeField] bool isWalkSet;
    [SerializeField] float walkRange; 

    //Switching between States
    [SerializeField] float sightRange;

    [SerializeField] bool playerIsInSight;

    

    void Awake()
    {
        _pCSState.EnemyHealth = _enemyHealth;
        _eTCState.EnemyHealth = _enemyHealth;
        _eCState.EnemyTransform = this.transform;
        _eCState.enemyAI = _navMesh;

        _playerTransform = GameObject.Find("PlayerObject").transform;
        _navMesh = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        //Checking for player in Sight Range
        playerIsInSight = Physics.CheckSphere(transform.position, sightRange, _playerMask);

        if (!playerIsInSight)
        {
            Patroling();
        }
        else if (playerIsInSight)
        {
            Chase();
        }

        if (useFootSteps)
        {
            HandleSounds();
        }
    }


    void Patroling()
    {
        if (!isWalkSet)
        {
            SearchPoint();
        }

        if (isWalkSet)
        {
            _navMesh.SetDestination(walkLocation);
        }

        Vector3 distance = transform.position - walkLocation;

        if (distance.magnitude < 1f)
        {
            isWalkSet = false;
        }
    }

    void SearchPoint()
    {
        float randomZvalue = Random.Range(-48.2f, 148.5f);
        float randomXvalue = Random.Range(-48.2f, 146.4f);

        //walkLocation = new Vector3(transform.position.x + randomXvalue, transform.position.y, transform.position.z + randomZvalue);
        walkLocation = new Vector3(randomXvalue, transform.position.y, randomZvalue);

        if (Physics.Raycast(walkLocation, -transform.up, 2f, _groundMask))
        {
            isWalkSet = true;
        }
    }

    void Chase()
    {
        _navMesh.SetDestination(_playerTransform.position);
    }

    private void HandleSounds()
    {      

        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0 && !PauseMenuScript.isPaused)
        {
            int randomNum = Random.Range(0, _soundClipsList.Count);
            Debug.Log(randomNum);
            _soundSource.PlayOneShot(_soundClipsList[randomNum]);
            footStepTimer = _baseSoundSpeed;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _navMesh.speed = 0f;
            _navMesh.SetDestination(transform.position);
            transform.LookAt(_playerTransform);
            _stateMachine.ChangeState<EnemyTurnCombatState>();
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRange);
    }
}
