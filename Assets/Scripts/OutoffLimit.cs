using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutoffLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConsts.PLATFORM))
        {
            Destroy(collision.gameObject);  
        }
    }
}
