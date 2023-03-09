using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : WeaponController
{

    private List<GameObject> books = new List<GameObject>(); // List to hold the book objects

    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < weapons[currentWeaponIndex].ProjCount; ++i)
        {
            float angle = i * Mathf.PI * 2f / weapons[currentWeaponIndex].ProjCount; // Calculate the angle between books
            float x = Mathf.Cos(angle) * weapons[currentWeaponIndex].OffsetRadius; // Calculate the x position of the book on the ellipse
            float y = Mathf.Sin(angle) * weapons[currentWeaponIndex].OffsetRadius; // Calculate the y position of the book on the ellipse
            Vector3 pos = transform.position + new Vector3(x, y, 0f); // Calculate the position of the book on the ellipse
            GameObject book = Instantiate(weapons[currentWeaponIndex].Prefab, pos, Quaternion.identity); // Instantiate the book
            book.transform.parent = transform; // Set the parent of the book to the controller object
            books.Add(book); // Add the book to the list

            FindObjectOfType<SFXController>().Play("BookWepSFX");
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}