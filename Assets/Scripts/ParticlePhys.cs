
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ParticlePhys : MonoBehaviour {

    bool isPause { get; set; } = false;
    Rigidbody2D currVel { get; set; }
    Vector2 helpVel { get; set; }
    float Secs { get; set; }
    List<Vector2> points { get; set; } = new List<Vector2>();
    Color color { get; set; }
    void Start() {

        gameObject.GetComponent<SpriteRenderer>().color = color;
        currVel = gameObject.GetComponent<Rigidbody2D>();
        currVel.velocity = new Vector2(helpVel.x, helpVel.y);
        StartCoroutine(PointToSave());

    }
    public void DeleteMe() {

        Destroy(gameObject.GetComponent<SpriteRenderer>());

        var goLine = new GameObject("LineGO");
        var lr = goLine.AddComponent<LineRenderer>();
        lr.material = Resources.Load<Material>("Line");
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.05f);
        lr.useWorldSpace = true;
        lr.positionCount = points.Count;
        Vector3[] positions = new Vector3[points.Count];
        for (var i = 0; i < points.Count; ++i) {
            positions[i] = points[i];
        }
        lr.SetPositions(positions);

        Destroy(gameObject);
    }
    public void SetUp(float Secs, Vector2 helpVel, Color color) {
        this.Secs = Secs;
        this.helpVel = helpVel;
        this.color = color;
    }
    public IEnumerator PointToSave() {
        while (true) {
            points.Add(gameObject.transform.position);
            yield return new WaitForSeconds(Secs);
        }
    }

    public void StartPause() {
        if (!isPause) {
            helpVel = currVel.velocity;
            currVel.velocity = new Vector2(0, 0);
            isPause = true;
        }
        else {
            currVel.velocity = helpVel;
            isPause = false;
        }
    }
}
