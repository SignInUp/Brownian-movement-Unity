using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallT : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "particle") {
            var vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(vel.x, -vel.y);
            Debug.Log("T");
        }
    }

}
