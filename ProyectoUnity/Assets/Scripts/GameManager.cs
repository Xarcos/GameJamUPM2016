using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum Actions {Bark, MakeDeath, Paw, Sit }

[System.Serializable]
public class LvlDef
{
    public long puntuationRequired;
    public float tempoTime;
    public int life_on_fail;
    public int life_on_success;
    public long successScore;
}

[System.Serializable]
public class boost
{
    public int followedSuccesses;
    public float multiplier;
}

public delegate void StandardDelegate ();

public class GameManager : MonoBehaviour {

    long m_score = 0;
    float m_temporizer = 0;
    int followedSuccesses = 0;

    long Score
    {
        get
        {
            return m_score;
        }
        set
        {
            m_score = value;
            m_scoreText.text = "Score:\n" + m_score.ToString();

            // Subimos de nivel en función de la puntuación
            if ((actualLvlDef < (m_lvlDefs.Length - 1)) && (m_lvlDefs[actualLvlDef + 1].puntuationRequired <= m_score))
            {
                ++actualLvlDef;
            }
        }
    }

    int FollowedSuccesses
    {
        get
        {
            return followedSuccesses;
        }
        set
        {
            followedSuccesses = value;
            if (followedSuccesses == 0)
            {
                actualBoost = 0;
                m_boostText.text = "";
                m_avatar.setBoost(false);
            }
            else if ((actualLvlDef == m_lvlDefs.Length -1) && (actualBoost < (m_boost.Length - 1)) && (m_boost[actualBoost + 1].followedSuccesses <= followedSuccesses))
            {
                ++actualBoost;
                m_boostText.text = "x" + m_boost[actualBoost].multiplier.ToString();
                m_avatar.setBoost(true);
            }
        }
    }

    Actions m_actualGesture;

    int actualLvlDef = 0; [SerializeField] LvlDef[] m_lvlDefs;
    int actualBoost = 0; [SerializeField] boost[] m_boost;

    //[SerializeField] UnityEngine.UI.Text m_lifeText;
    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.Text m_boostText;
    
    [SerializeField] RectTransform m_floatingTransformOrigin;
    [SerializeField] DelayedDestroy m_floatingScore;

    [SerializeField] float OWNER_REACTION_TIME;
    [SerializeField] float GAMEOVER_WAIT_TIME;

    [SerializeField] Animator redFeedback;

    bool tempoOn;

    Owner m_owner;
    DogAvatar m_avatar;
    Animator player;

    public event StandardDelegate OnGestureMade;
    public event StandardDelegate OnActionMade;

	// Use this for initialization
	void Start () {
        m_owner = GameObject.FindGameObjectWithTag("Owner").GetComponent<Owner>();
        m_avatar = GameObject.FindGameObjectWithTag("Avatar").GetComponent<DogAvatar>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        MakeGesture(true);
	}
	
	// Update is called once per frame
	void Update () {
        // Temporizador
        if (tempoOn && (m_temporizer -= Time.deltaTime) < 0)
        {
            StartCoroutine(OnWrongAction());
        }
	}

    public void StopTemporizer()
    {
        tempoOn = false;

        // Avisar a los demas
        if (OnActionMade != null)
            OnActionMade();
    }

    public void MakeGesture(bool newg)
    {
        // Escoger un gesto
        if (newg)
        {
            var values = System.Enum.GetValues(typeof(Actions));
            m_actualGesture = (Actions)values.GetValue(Random.Range(0, values.Length));
        }
        // Hacer el gesto
        m_owner.MakeGesture(m_actualGesture);

        // Iniciar temporizador
        m_temporizer = m_lvlDefs[actualLvlDef].tempoTime;
        tempoOn = true;

        // Avisar a los demas
        if (OnGestureMade != null)
            OnGestureMade();
    }

    public void MakeAction(Actions action)
    {
        if (m_actualGesture == action)
            StartCoroutine(OnCorrectAction());
        else
            StartCoroutine(OnWrongAction());
    }

    public IEnumerator OnWrongAction()
    {
        // Reacción del dueño
        m_owner.AngryReaction();
        redFeedback.SetTrigger("error");
        player.SetTrigger("sit");
        yield return new WaitForSeconds(OWNER_REACTION_TIME);

        // Resetear el número de aciertos seguidos
        FollowedSuccesses = 0;

        // Perder vida
        if (m_avatar.loseHp(m_lvlDefs[actualLvlDef].life_on_fail)) {
            // Hacer el nuevo gesto
            MakeGesture(actualLvlDef != 0);
        }
    }

    public IEnumerator OnCorrectAction()
    { 
        // Reacción del dueño
        m_owner.HappyReaction();
        yield return new WaitForSeconds(OWNER_REACTION_TIME);

        // Ganar vida
        m_avatar.gainHp(m_lvlDefs[actualLvlDef].life_on_success);

        // Ganar puntuación
        var score = m_lvlDefs[actualLvlDef].successScore * m_boost[actualBoost].multiplier;
        Score += System.Convert.ToInt64(score);
        CreateFloatingScore(score.ToString());

        // Aciertos seguidos
        ++FollowedSuccesses;

        // Hacer el nuevo gesto
        MakeGesture(true);
    }

    public void EndGame()
    {
        player.SetTrigger("death");

        ScoreManager.SCORE = Score;

        StartCoroutine(ExitGame());
    }

    void CreateFloatingScore(string text)
    {
        var instantiated = Instantiate<DelayedDestroy>(m_floatingScore);
        instantiated.SetText(text.ToString());
        instantiated.transform.SetParent(m_floatingTransformOrigin, false);
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(GAMEOVER_WAIT_TIME);
        SceneManager.LoadScene("endgame");
    }
}
