using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotControl : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject fireObj;
    public Transform firePoint; // 총을 발사할 지점
    Vector3 fireDirection;
    public float fireSpeed = 3;
   int enableAttack = 0;
    Vector3 lastInputPosition;
    Vector3 tempVector3;
    Vector2 tempVector2 = new Vector2();
     GameObject tempObj;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            if (enableAttack>0)
            {
                enableAttack--;
                //마우스 입력 위치를 카메라가 바라보는 영역 안의 월드 좌표로 변환
                tempVector3 = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                tempVector3.z = 0;

                //벡터의 뺄셈 후 방향만 지닌 단위 벡터로 변경
                fireDirection = tempVector3 - firePoint.position;
                fireDirection = fireDirection.normalized;
                //발사
                tempObj = Instantiate(fireObj, firePoint.position, fireObj.transform.rotation) as GameObject;
                //발사한 오브젝트 속도 계산
                tempVector2.Set(fireDirection.x, fireDirection.y);
                tempVector2 = tempVector2 * fireSpeed;
                //속도 적용
                tempObj.GetComponent<Rigidbody2D>().velocity = tempVector2;
                // tempObj.transform.Translate(tempVector2 * Time.deltaTime);
                //tempObj.GetComponent<Transform>().transform.Translate(tempVector2 * Time.deltaTime);
                
            }
            


        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            enableAttack ++;
        }

    }
}
