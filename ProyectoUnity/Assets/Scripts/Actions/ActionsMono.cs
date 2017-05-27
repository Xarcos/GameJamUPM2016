using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionsMono : MonoBehaviour {

	[SerializeField] protected Actions m_ACTION;
    [SerializeField] protected KeyCode key;
    [SerializeField] protected float seconds;

    GameManager gameManager;
    protected Animator player;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGestureMade += OnGestureMade;
        gameManager.OnActionMade += OnActionMade;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(key)) {
            StartCoroutine(_Action());
        }
    }

    public void OnClick_Button()
    {
        StartCoroutine(_Action());
    }

    IEnumerator _Action()
    {
        gameManager.StopTemporizer();

        MakeAnimation();

        yield return new WaitForSeconds(seconds);
        gameManager.MakeAction(m_ACTION);

    }

    protected abstract void MakeAnimation();


    void OnGestureMade()
    {
        GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    void OnActionMade()
    {
        GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
