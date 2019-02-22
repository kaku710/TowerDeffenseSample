using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private string attackTag; //Inspectorで設定
    private float stopDistance;
    private float attackTimer;
    private bool isAttack;
    //ここから
    public int hp; 
    public int power;
    //ここまでInspectorで設定
    private enum targetType
    {
        normal,
        castle
    }
    private targetType currentTargetType;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        target = GameObject.Find(attackTag + "Castle");
        isAttack = false;
    }
    private void Update()
    {
        if (target == null)
        {
            target = GameObject.Find(attackTag + "Castle");
        }

        SetStopDistance();
        FintTarget();
        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) <= stopDistance)
        {
            isAttack = true;
            agent.speed = 0;
        }

        if (isAttack)
        {
            CheckNearTarget(); 
            SetStopDistance(); //攻撃中にtargetが変わった時のためにここでも記述
            Attack();
        }
    }
    //targetが近くにいるのかどうか判定する
    private void CheckNearTarget()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > stopDistance)
        {
            isAttack = false;
            agent.speed = 1;
        }
    }
    private void SetStopDistance()
    {
        if (target.gameObject.name.Contains("Castle"))
        {
            currentTargetType = targetType.castle;
            stopDistance = 2f;
        }
        else
        {
            currentTargetType = targetType.normal;
            stopDistance = 0.7f;
        }
    }
    private void FintTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(attackTag);

        float closestDistance = Vector3.Distance(transform.position, target.transform.position);

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < closestDistance)
            {
                target = enemy;
            }
        }
    }
    private void Attack()
    {
        attackTimer += Time.deltaTime;
        switch (currentTargetType)
        {
            case (targetType.normal):
                if (attackTimer > 1.0f)
                {
                    target.GetComponent<Character>().hp -= power;
                    attackTimer = 0f;
                }
                if (target.GetComponent<Character>().hp <= 0)
                {
                    isAttack = false;
                    Destroy(target.gameObject);
                    agent.speed = 1;
                }
                break;
            case (targetType.castle):
                if (attackTimer > 1.0f)
                {
                    target.GetComponent<Castle>().hp -= power;
                    attackTimer = 0f;
                }
                break;
        }
    }
}
