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
    public Transform target, firePoint;
    public float rotationSpeed, movementSpeed;
    public bool enableRotate;
    public float maxHp, currentHp;
    public GameObject coinsPrefab, hpPotionsrefab, bulletPrefab;

    private void Awake() {
        target = GameObject.Find("Player").transform;
        enableRotate = true;
        currentHp = maxHp;
    }

    private void Start() {
        if(type == EnemyType.Stand){
            InvokeRepeating("Shoot",1,3);
        }
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
        }

        CheckDistance();
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(transform.up * 10, ForceMode2D.Impulse);
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

    void HPManager(int val){
        currentHp += val;
        int rand;
        if(currentHp<=0){
            Destroy(gameObject);

            rand=Random.Range(0,1);
            if(rand==0){
                Instantiate(coinsPrefab, transform.position, Quaternion.identity);
            }

            else
                Instantiate(hpPotionsrefab, transform.position, Quaternion.identity);

        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Bullet"){
            HPManager(-50);
        }

        if(other.gameObject.tag=="SecondBullet"){
            HPManager(-20);
        }

        if(other.gameObject.tag=="Player"){
            Destroy(gameObject);
        }
    }
    
}
