using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // MARK: - Components
    private Rigidbody2D rb;

    // Start is called before the first frame update

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetupBall(Vector2 _direction) {
        rb.velocity = _direction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
