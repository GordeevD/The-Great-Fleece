using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    private NavMeshAgent _agent;
    private int _currentTarget = 0;
    private bool _reverse = false;
    private bool _halt = false;
    private Animator _anim;
    private bool _followCoin = false;
    private Vector3 _coinPosition;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (wayPoints.Count > 0 && wayPoints[_currentTarget] != null && !_followCoin)
        {
            _agent.SetDestination(wayPoints[_currentTarget].position);

            if(_anim != null)
            {
                _anim.SetBool("Walk", !_halt);
            }

            float distance = Vector3.Distance(transform.position, wayPoints[_currentTarget].position);
            if (distance < 0.5f && _halt == false)
            {
                StartCoroutine(WaitBeforeMoving());
            }
        }

        if (_followCoin)
        {
            float distance = Vector3.Distance(transform.position, _coinPosition);
            if (distance < 5f)
            {
                if (_anim != null)
                {
                    _anim.SetBool("Walk", false);
                    GetComponent<NavMeshAgent>().autoBraking = false;
                }
                StartCoroutine(FollowCoinTimer());
            }
        }

    }

    IEnumerator WaitBeforeMoving()
    {
     
        if (_currentTarget == 0 || _currentTarget == wayPoints.Count-1)
        {
            _halt = true;
            yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        }

        if (wayPoints.Count > 1)
        {
            _halt = false;
        

            if (_reverse)
            {
                _currentTarget--;
                if (_currentTarget == 0)
                {
                    _reverse = false;
                }
            }
            else if (_reverse == false)
            {
                _currentTarget++;
                if (_currentTarget == wayPoints.Count-1)
                {
                    _reverse = true;
                }
            }

        }
    }

    public void OnEnable()
    {
        Player.OnCoinToss += FollowCoin;
    }

    private void FollowCoin(Vector3 coinPosition)
    {
        _coinPosition = coinPosition;
        _followCoin = true;

        _agent.SetDestination(_coinPosition);

        if (_anim != null)
        {
            _anim.SetBool("Walk", true);
        }

    }

    IEnumerator FollowCoinTimer()
    {
        yield return new WaitForSeconds(5f);
        _followCoin = false;
    }
}