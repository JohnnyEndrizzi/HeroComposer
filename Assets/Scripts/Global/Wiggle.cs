using UnityEngine;

public class Wiggle : MonoBehaviour {
    //wiggles things that should wiggle like signs
    
    //wiggle timing stuff
    float lerpTime = 3f;
    float currentLerpTime;

    //how much wiggle
    private float moveSpeed=5;
    private float moveLoc;
    private Quaternion Rotate_From;
    private Quaternion Rotate_To;

    //creak with wiggle
    AudioSource audioSource;
    AudioClip clip;

	void Update ()
    {
        //wiggler
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float perc = currentLerpTime / lerpTime;

        if(perc >= 1) //wiggled
        {
            moveLoc = moveSpeed*next(moveLoc/moveSpeed);
            currentLerpTime = 0;
        }

        //plans to wiggle
        Rotate_From = transform.rotation;
        Rotate_To = Quaternion.Euler (0,0,moveLoc);

        //THE wiggle
        transform.rotation = Quaternion.Lerp (Rotate_From, Rotate_To, Time.deltaTime*0.2f);
	}

    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("SoundEffects/creak");
    }

    private int next(float last) //pick where to wiggle to
    {
        int rnd = Random.Range(-2, 5);

        if (rnd > 2) {
            return (int)last;  //don't wiggle - makes the wiggle a little gentler and more random
        }
        else {
            //audioSource.PlayOneShot(clip, 0.1f);
            return  rnd;
        }
    }
}