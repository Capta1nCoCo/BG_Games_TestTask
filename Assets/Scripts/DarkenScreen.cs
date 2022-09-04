using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenScreen : MonoBehaviour
{
    private Image image;

    private float _time = 1f;
    private float delayInSeconds = 3f;

    private bool isTransparent;

    private void Awake()
    {
        image = GetComponent<Image>();        
    }

    private void OnEnable()
    {
        MakeTransparent();
        StartCoroutine(DisableSelfWithDelay());
    }

    private void Update()
    {
        if (isTransparent)
        {
            _time += Time.deltaTime;            
        }
        else
        {
            _time -= Time.deltaTime;            
        }
        
        ChangeAChannel();
    }

    public void MakeTransparent()
    {
        _time = 1f;
        isTransparent = false;
    }

    public void MakeOpaque()
    {
        _time = 0f;
        isTransparent = true;
    }

    private void ChangeAChannel()
    {
        Color temp = image.color;
        temp.a = _time;
        image.color = temp;
    }

    private IEnumerator DisableSelfWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.SetActive(false);
    }
}
