using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PongBall : MonoBehaviour
{
    Vector2 velocity = new Vector2(-15.0f,-15);

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        int resolve = 0;
        if ((resolve = isOutOfBound() ) != 0)
        {
            //top 
            if ( resolve % 2 != 0 )
            {
                //TODO - it collide top
                velocity.y = -Mathf.Abs(velocity.y);
            }
            resolve = resolve >> 1; 
            //bottom 
            if (resolve % 2 != 0)
            {
                //TODO - it collide bottom
                velocity.y = Mathf.Abs(velocity.y);
            }
            resolve = resolve >> 1;
            //left
            if (resolve % 2 != 0)
            {
                //TODO - it collide left
                velocity.x = Mathf.Abs(velocity.x);
            }
            resolve = resolve >> 1;
            //right 
            if (resolve % 2 != 0)
            {
                //TODO - it collide right 
                velocity.x = -Mathf.Abs(velocity.x);
            }
        } 
    }

    private int isOutOfBound()
    {
        int ret = 0 ; 
        Camera cam = Camera.main ;
        Vector3 pos ;
        Vector3 viewPos ;

        //Check Top 
        pos = transform.position; pos.y += gameObject.GetComponent<Renderer>().bounds.size.y / 2;
        viewPos = cam.WorldToViewportPoint(pos);

        if (isViewPosOutOfBound(viewPos,false ))
        {
            ret += 1;
        }

        //Check Bottom 
        pos = transform.position; pos.y -= gameObject.GetComponent<Renderer>().bounds.size.y / 2;
        viewPos = cam.WorldToViewportPoint(pos);
        
        if (isViewPosOutOfBound(viewPos,false))
        {
            ret += 2;
        }

        //Check Left 
        pos = transform.position; pos.x -= gameObject.GetComponent<Renderer>().bounds.size.x / 2;
        viewPos = cam.WorldToViewportPoint(pos);

        if (isViewPosOutOfBound(viewPos,true))
        {
            ret += 4;
        }
 
        pos = transform.position; pos.x += gameObject.GetComponent<Renderer>().bounds.size.x / 2;
        viewPos = cam.WorldToViewportPoint(pos);

        if (isViewPosOutOfBound(viewPos,true))
        {

            ret += 8 ;
        }
        return ret;
    }

    private bool isViewPosOutOfBound(Vector3 viewPos, bool horizontal)
    {
        if (horizontal)
            return ! (viewPos.x >= 0 && viewPos.x <= 1);


        return ! (viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)  ;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PongEntity>())
        {
            velocity.x *= -1 ;
            velocity.y = UnityEngine.Random.Range(0,2) == 0 ? -velocity.y : velocity.y  ; 
        }
    }

}