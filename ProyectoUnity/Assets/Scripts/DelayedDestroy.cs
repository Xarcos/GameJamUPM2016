using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour {

    [SerializeField] float time;
    [SerializeField] float randomX;

    public void SetText (string text)
    {
        GetComponent<UnityEngine.UI.Text>().text = text;
    }

    void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        var local = rectTransform.localPosition;
        local.x += Random.Range(-randomX, randomX);
        rectTransform.localPosition = local;

    }

    void Update()
    {
        if ((time -= Time.deltaTime) <= 0)
        {
            Destroy(gameObject);
        }
    }
}
