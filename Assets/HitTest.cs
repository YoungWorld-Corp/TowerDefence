using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var collider = GetComponent<Collider2D>();
        var contactFilter = new ContactFilter2D();
        var hits = new List<Collider2D>();
        collider.OverlapCollider(contactFilter, hits);

        if (hits.Count != 0)
        {
            Debug.Log("HitTest::Update() hits.Count = " + hits.Count);
        } 
    }
}
