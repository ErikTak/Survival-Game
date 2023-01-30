using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : WeaponController
{
    public int numBooks;
    public List<GameObject> bookList = new List<GameObject>();
    public float radius;
    public float rotationSpeed;
    
    protected override void Start()
    {

    }
    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < numBooks; ++i)
        {
            // This code spawns circular formation of objects, calculates angle and position for each, creates instances of prefab, sets as child of script object, and adds to bookList.
            
            float angle = i * Mathf.PI * 2f / numBooks;
            Vector3 position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject spawnedBook = Instantiate(weaponData.Prefab, position, Quaternion.identity);
            spawnedBook.transform.parent = transform; // Reference and set direction
            bookList.Add(spawnedBook);
        }
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}