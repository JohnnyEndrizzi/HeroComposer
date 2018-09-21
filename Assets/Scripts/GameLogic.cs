using UnityEngine;
using Game;
using OsuParser;

public class GameLogic : MonoBehaviour {

    public SpriteRenderer note;
    private Vector3 startPos;
    private Vector3 endPos;
    private Metronome metronome;
    private Beatmap beatmap;
    private int noteIndex = 0;
    private decimal latency = 0.150m;
    private decimal songStartTime;

    // Use this for initialization
    void Start () {
        Debug.Log("Loading beatmap file...");
        beatmap = new Beatmap("C:/Users/nicol/Documents/McMaster Year 2018/Semester 1/4GP6/OsuParser/OsuParserUnityTest/Assets/Songs/trigger_happy-normal.osu");
        startPos = new Vector3(4.5F, 2.0F, 0.0F);
        endPos = new Vector3(-4.5F, 2.0F, 0.0F);
        songStartTime = (decimal)AudioSettings.dspTime + latency;
        metronome = new Metronome(songStartTime, (decimal)beatmap.TimingPoints[0].TimePerBeat);
        metronome.Tick += SpawnNote;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        metronome.Update((decimal)AudioSettings.dspTime, (decimal)Time.deltaTime);
    }

    void SpawnNote(object sender, Metronome.TickEventArgs e)
    {
        //Color of metronome   
        Color c;
        //If beat 1 of bar
        if (e.positionInBeats % 4 == 0)
        {
            //Green
            c = new Color(0, 255, 0);
        }
        else
        {
            //Black
            c = new Color(0, 0, 0);
        }
        GetComponent<Renderer>().material.color = c;

        //Song ended
        if (noteIndex >= beatmap.HitObjects.Count)
        {
            Debug.Log("Song ended.");
        }
        //Spawn next note
        else if (e.positionInBeats == (beatmap.HitObjects[noteIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat) - beatmap.ApproachRate))
        {
            Debug.Log(string.Format("Spawning note {0} at beat {1}.", noteIndex, e.positionInBeats));
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
