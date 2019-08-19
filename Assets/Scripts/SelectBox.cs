using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class SelectBox : MonoBehaviour
{
    private new SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        transform.SetParent(UIManager.canvas.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        renderer.color = TRANSPARENT_COLOR;
    }

    private void OnTriggerExit(Collider other)
    {
        renderer.color = DEFAULT_COLOR;
    }

    public void showSelectBox()
    {
        gameObject.SetActive(true);
    }

    public void hideSelectBox()
    {
        gameObject.SetActive(false);
    }
}
