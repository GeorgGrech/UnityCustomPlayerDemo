using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClicker : MonoBehaviour
{
    public GameObject comicTextPrefab;

    private void OnMouseDown()
    {
        Die();
    }

    void Die()
    {
        // Instantiate comic text at enemy's position with a random offset
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        GameObject comicText = Instantiate(comicTextPrefab, transform.position + randomOffset, Quaternion.identity);

        // Choose a random comic text from your collection (if you have multiple)
        // SpriteRenderer sr = comicText.GetComponent<SpriteRenderer>();
        // sr.sprite = // Choose a random sprite from your collection

        // Destroy the comic text after some time
        Destroy(comicText, 2f);

        // Destroy the enemy
        Destroy(gameObject);
    }
}
