using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
    {
        Stand,
        Move
    }

public class EnemyController : MonoBehaviour
{
    public EnemyType type;
    public Transform target;
    public float rotationSpeed, movementSpeed;
    public bool enableRotate;
    public float maxHp, currentHp;

    private void Awake() {
        target = GameObject.Find("Player").transform;
        enableRotate = true;
        currentHp = maxHp;
    }

    private void Update()
    {
        if(type == EnemyType.Move){

            if(enableRotate){
                RotateToPlayer();
            }

            Move();
        }

        if(type == EnemyType.Stand){
            RotateToPlayer();
            Shoot();
        }

        CheckDistance();
    }

    void Shoot(){

    }

    void RotateToPlayer(){
        if(type == EnemyType.Move){
            enableRotate=false;
        }
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angle-90);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void Move(){
        transform.position += transform.up * movementSpeed * Time.deltaTime;
    }

    void CheckDistance(){
        float distance = Vector2.Distance(target.position, this.gameObject.transform.position);
        if (distance > 100)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Bullet"){
            Debug.Log("GetShoot");
            currentHp = currentHp-30;
            if(currentHp<=0){
                Destroy(gameObject);
            }
        }

        if(other.gameObject.tag=="SecondBullet"){
            Debug.Log("GetShoot");
            currentHp = currentHp-10;
            if(currentHp<=0){
                Destroy(gameObject);
            }
        }
    }
    
}
