using UnityEngine;
using Game;
using UnityEngine.UI;
using OsuParser;

public class GameLogic : MonoBehaviour
{
    public GameObject healthBar;
    public SpriteRenderer note;
    public SpriteRenderer curtains;

    public static float nextHit;
    public static int hitIndex = 0;

    private float maxHealth;

    private bool notesDone = false;
    private bool songDone = false;

    private Vector3 startPos;
    private Vector3 endPos;
    private Metronome metronome;
    private Beatmap beatmap;
    private int noteIndex = 0;
    private decimal latency = 0.150m;
    public static decimal songStartTime;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Loading beatmap file...");
        beatmap = new Beatmap("C:/Users/Damian/Documents/GitHub/HeroComposer/Assets/Songs/trigger_happy-normal.osu");
        startPos = new Vector3(-3.06f, 2.15F, -7.45F);
        endPos = new Vector3(2.88F, 2.15F, -7.45F);
        songStartTime = (decimal)AudioSettings.dspTime + latency;

        maxHealth = GetComponent<AudioSource>().clip.length;

        metronome = new Metronome(songStartTime, beatmap.TimingPoints[0].TimePerBeat);
        metronome.Tick += SpawnNote;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<AudioSource>().clip.length);
        Debug.Log(GetComponent<AudioSource>().time);
        Debug.Log(GetComponent<AudioSource>().isPlaying);
        if (!notesDone)
        {
            nextHit = beatmap.HitObjects[hitIndex].StartTimeInMiliseconds();
        }
        else if (!GetComponent<AudioSource>().isPlaying && !songDone)
        {
            Instantiate(curtains, curtains.transform.position, Quaternion.identity);
            songDone = true;
            Debug.Log("Song ended.");
        }

        float currHealth = GetComponent<AudioSource>().time;
        healthBar.transform.localScale = new Vector3(((maxHealth - currHealth) / maxHealth), transform.localScale.y, transform.localScale.z);
        //healthBar.SetActive(false);

        metronome.Update((decimal)AudioSettings.dspTime, (decimal)Time.deltaTime);
    }

    void SpawnNote(object sender, Metronome.TickEventArgs e)
    {
        //Song ended
        if (noteIndex >= beatmap.HitObjects.Count)
        {
            //Instantiate(curtains, curtains.transform.position, Quaternion.identity);
            notesDone = true;
            Debug.Log("Notes ended.");

            return;
        }
        //Spawn next note
        else if (e.positionInBeats == (beatmap.HitObjects[noteIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat) - beatmap.ApproachRate))
        {
            //Debug.Log(string.Format("Spawning note {0} at beat {1}.", noteIndex, e.positionInBeats));
            //Instantiate circle prefab
            SpriteRenderer beatSprite;
            beatSprite = Instantiate(note, startPos, Quaternion.identity);

            metronome.Tick += beatSprite.GetComponent<CircleNote>().UpdateSongPosition;

            beatSprite.GetComponent<CircleNote>().startPos = startPos;
            beatSprite.GetComponent<CircleNote>().endPos = endPos;
            beatSprite.GetComponent<CircleNote>().beatNumber = noteIndex;

            beatSprite.GetComponent<CircleNote>().approachRate = beatmap.ApproachRate;
            beatSprite.GetComponent<CircleNote>().startTimeInBeats = beatmap.HitObjects[noteIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat);
            beatSprite.GetComponent<CircleNote>().songPosInBeats = e.positionInBeats;

            noteIndex++;
        }
    }
}
