using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float damage, shootCooldown, range;

    private float nextShot;

    [SerializeField]
    private GameObject targetEnemy;

    [SerializeField]
    private float rotationModifier=90f, speedRotation=50f; //rotatioModifier é o ajuste inicial do angulo para pirar, speedRotation é a velocidade de gira

    // Start is called before the first frame update
    void Start()
    {
        range = 1.7f;
        nextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextShot) //Se tiver no tempo para atirar e tem um alvo na mira, atira e entra em cooldown
        {
            if (targetEnemy != null)
            {
                shoot();
                nextShot = Time.time + shootCooldown;
            }
        }
        followEnemy();
    }

    public void OnTriggerEnter2D(Collider2D other) //Entra na area e mira
    {
        if (other.tag == "Enemy")
        {
            targetEnemy = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other) //Sai da area e para de mirar
    {
        
        if (other.tag == "Enemy")
        {
            changeTarget();
        }
    }

    //public void OnTriggerStay2D(Collider2D other) //Metodo para trocar de alvo, mas acho que ele escolhe um alvo qualquer na area
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        targetEnemy = other.gameObject;
    //    }
    //}
    
    private void changeTarget()
    {
        //testa se tem outro inimigo na area quando termina a colisão
        //OnTriggerStay parece escolher um alvo random na area, por mais que possa mudar de alvo
        //Esse sempre pega o alvo mais proximo
        float distance;
        foreach(GameObject enemy in GameManager.enemyGroup)
        {
            distance = (transform.position - enemy.transform.position).magnitude;
            if (distance <= range)
                targetEnemy = enemy;
            else
                targetEnemy = null;

        }

    }

    private void shoot()
    {
        targetEnemy.GetComponent<EnemyBehaviour>().takeDamage(damage); //Acessa o script do inimigo e usa o metoto takeDamage
    }

    private void followEnemy()
    {
        if (targetEnemy != null)//Se a torre está mirando em um inimigo, é criado o vetor de mira e a rotação no eixo acompanhando o alvo
        {
            Vector3 aimToTarget = targetEnemy.transform.position - transform.position;  
            float angle = Mathf.Atan2(aimToTarget.y, aimToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
        }
    }
}
