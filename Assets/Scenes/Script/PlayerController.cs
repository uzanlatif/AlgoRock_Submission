using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{   
    public float currentHp, maxHp, coins;
    public float bulletSpeed, movementSpeed;
    public Transform firePoint, secondFirePoint, thirdFirePoint;
    public GameObject bulletPrefab, secondBulletPrefab;
    public float sensitivity = 10.0f;
    public Vector3 mousePos, objectPos;
    public TextMeshProUGUI hpText, coinsText;
    public GameObject pauseUI;

    private void Start() {
        currentHp = maxHp;    
        coins = 0;

        hpText.text = currentHp.ToString();
        coinsText.text = coins.ToString();
    }

    void Update()
    {
        CheckMovement();
        CheckCameraPos();
        CheckMouseInput();
    }

    void CheckMovement(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);

        transform.position = transform.position + (Vector3)(movement * movementSpeed * Time.deltaTime);
    }

    void CheckMouseInput(){
        //senjata 1
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet,3);
        }
        //senjata 2
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bullet = Instantiate(secondBulletPrefab, firePoint.transform.position, transform.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

            GameObject secondBullet = Instantiate(secondBulletPrefab, secondFirePoint.transform.position, transform.rotation);
            Rigidbody2D secondBulletRigidbody = secondBullet.GetComponent<Rigidbody2D>();
            secondBulletRigidbody.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

            GameObject thirdBullet = Instantiate(secondBulletPrefab, thirdFirePoint.transform.position, transform.rotation);
            Rigidbody2D thirdBulletRigidbody = thirdBullet.GetComponent<Rigidbody2D>();
            thirdBulletRigidbody.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

            Destroy(bullet,3);
            Destroy(secondBullet,3);
            Destroy(thirdBullet,3);

        }
    }

    void CheckCameraPos(){
        mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
    
    }

    private void FixedUpdate() {
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        
    }

    void Dead(){
        Time.timeScale = 0;
        //show pause UI
        pauseUI.SetActive(true);
    }

    void HPManager(int val){
        currentHp += val;

        if(currentHp>maxHp){
            currentHp=maxHp;
        }

        if(currentHp<=0){
            Dead();
        }

        hpText.text = currentHp.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Enemy"){
            HPManager(-10);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag=="Coins"){
            coins += 10;
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag=="HPPotions"){
            HPManager(30);
            Destroy(other.gameObject);
        }
    }
}
