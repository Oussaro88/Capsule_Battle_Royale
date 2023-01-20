using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script créé par Sengsamrach Vong, à part les codes en lien avec l'audio par Oussama Arouch
/// </summary>

public class Strike : MonoBehaviour
{
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject spearObject;
    [SerializeField] private GameObject hammerObject;
    [SerializeField] private Animator knifeAnim;
    [SerializeField] private Animator swordAnim;
    [SerializeField] private Animator spearAnim;
    [SerializeField] private Animator hammerAnim;
    private bool isAttack = false;
    private bool timerOn = false;
    private float timer = 0;
    [SerializeField] private Equipment.typeMelee myMelee;


    //Variables rajoutées par Oussama Arouch
    //Audio
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;
    [SerializeField] private AudioSource audioSource4;
    [SerializeField] private AudioClip audioKnife;
    [SerializeField] private AudioClip audioSword;
    [SerializeField] private AudioClip audioSpear;
    [SerializeField] private AudioClip audioHammer;

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;
        timerOn = false;
        timer = 0f;
        knifeObject.GetComponent<Weapon>().SetOrigin(gameObject);
        swordObject.GetComponent<Weapon>().SetOrigin(gameObject);
        spearObject.GetComponent<Weapon>().SetOrigin(gameObject);
        hammerObject.GetComponent<Weapon>().SetOrigin(gameObject);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isAttack = context.performed;
    }

    public void SetMelee(Equipment.typeMelee playerMelee)
    {
        myMelee = playerMelee;
    }

    public void AttackKnife()
    {
        timer = 0.5f;
        knifeObject.SetActive(true);
        if (knifeAnim) knifeAnim.SetTrigger("Attack");
        Invoke("SetInactiveKnife", timer);
        audioSource1.PlayOneShot(audioKnife); //Ligne rajoutée par Oussama Arouch
    }

    public void AttackSword()
    {
        timer = 0.5f;
        swordObject.SetActive(true);
        if (swordAnim) swordAnim.SetTrigger("Attack");
        Invoke("SetInactiveSword", timer);
        audioSource2.PlayOneShot(audioSword); //Ligne rajoutée par Oussama Arouch
    }

    public void AttackSpear()
    {
        timer = 0.75f;
        spearObject.SetActive(true);
        if (spearAnim) spearAnim.SetTrigger("Attack");
        Invoke("SetInactiveSpear", timer);
        audioSource3.PlayOneShot(audioSpear); //Ligne rajoutée par Oussama Arouch
    }

    public void AttackHammer()
    {
        timer = 1f;
        hammerObject.SetActive(true);
        if (hammerAnim) hammerAnim.SetTrigger("Attack");
        Invoke("SetInactiveHammer", timer);
        audioSource4.PlayOneShot(audioHammer); //Ligne rajoutée par Oussama Arouch
    }

    public void SetInactiveKnife()
    {
        knifeObject.SetActive(false);
    }

    public void SetInactiveSword()
    {
        swordObject.SetActive(false);
    }

    public void SetInactiveSpear()
    {
        spearObject.SetActive(false);
    }

    public void SetInactiveHammer()
    {
        hammerObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            isAttack = false;

            if (!timerOn)
            {
                timerOn = true;

                if (myMelee == Equipment.typeMelee.Knife)
                {
                    AttackKnife();
                }
                else if (myMelee == Equipment.typeMelee.Sword)
                {
                    AttackSword();
                }
                else if (myMelee == Equipment.typeMelee.Spear)
                {
                    AttackSpear();
                }
                else if (myMelee == Equipment.typeMelee.Hammer)
                {
                    AttackHammer();
                }
            }
        }

        if (timerOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f) 
            {
                timer = 0f;
                timerOn = false; 
            }
        }
    }
}