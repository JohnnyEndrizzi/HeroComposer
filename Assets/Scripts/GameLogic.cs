using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using OsuParser;

public class GameLogic : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject holdNote;

    public GameObject test;
    public GameObject characterPlaceholder;

    public SpriteRenderer note;
    public SpriteRenderer curtains;

    public static float nextHit;
    public static float nextHitEnd = 0;
    public static int hitIndex = 0;
    public bool introFinished;

    private float maxHealth;

    private string noteTag;
    private int iterationsLeft = 1;
    private bool firstNoteOfSlider = true;

    private bool notesDone = false;
    public static bool songDone = false;

    bool delayLock = true;

    private Vector2 startPos;
    private Vector2 endPos;
    private Metronome metronome;
    private Beatmap beatmap;
    private int noteIndex = 0;
    private decimal latency = 0.150m;
    public static decimal songStartTime;

    public void spawnCharacters()
    {
        for (int i = 0; i < Assets.Scripts.MainMenu.ApplicationModel.characters.Count; i++)
        {
            Vector3 characterSpawnPosition;

            if (i == 0)
            {
                characterSpawnPosition = new Vector3(3.38f, 1.56f, -4.5f);
            }
            else if (i == 1)
            {
                characterSpawnPosition = new Vector3(1.43f, 1.19f, -4.8f);
            }
            else if (i == 2)
            {
                characterSpawnPosition = new Vector3(1.43f, -0.35f, -5.1f);
            }
            else
            {
                characterSpawnPosition = new Vector3(2.82f, -0.67f, -5.5f);
            }

            characterPlaceholder.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Assets.Scripts.MainMenu.ApplicationModel.characters[i].sprite);
            GameObject spawnedPlayer = Instantiate(characterPlaceholder, characterSpawnPosition, Quaternion.identity) as GameObject;

            spawnedPlayer.name = "character_" + (i + 1);
            spawnedPlayer.GetComponent<CharacterLogic>().hp = Assets.Scripts.MainMenu.ApplicationModel.characters[i].hp;
            spawnedPlayer.GetComponent<CharacterLogic>().atk = Assets.Scripts.MainMenu.ApplicationModel.characters[i].atk;
            spawnedPlayer.GetComponent<CharacterLogic>().def = Assets.Scripts.MainMenu.ApplicationModel.characters[i].def;
            spawnedPlayer.GetComponent<CharacterLogic>().mgc = Assets.Scripts.MainMenu.ApplicationModel.characters[i].mgc;
            spawnedPlayer.GetComponent<CharacterLogic>().rcv = Assets.Scripts.MainMenu.ApplicationModel.characters[i].rcv;
            spawnedPlayer.GetComponent<CharacterLogic>().attack = Assets.Scripts.MainMenu.ApplicationModel.characters[i].mgc_animation;
        }
    }

    public float getNextHit()
    {
        return nextHit;
    }

    public decimal getSongStartTime()
    {
        return songStartTime;
    }

    public bool isSongDone()
    {
        return songDone;
    }

    private IEnumerator introDelay()
    {
        yield return new WaitForSeconds(6.0f);

        songStartTime = (decimal)AudioSettings.dspTime + latency;
        metronome = new Metronome(songStartTime, beatmap.TimingPoints[0].TimePerBeat);
        metronome.Tick += SpawnNote;

        GetComponent<AudioSource>().Play();

        delayLock = false;
        yield return null;
    }

    // Use this for initialization
    void Start ()
    {
        spawnCharacters();

        if (Assets.Scripts.MainMenu.ApplicationModel.songPathName != "")
        {
            Debug.Log("Loading beatmap file for " + Assets.Scripts.MainMenu.ApplicationModel.songPathName + "...");
            beatmap = new Beatmap("Assets/Resources/Songs/" + Assets.Scripts.MainMenu.ApplicationModel.songPathName + ".osu");
        }
        else
        {
            Debug.Log("Loading beatmap file for ALiVE_Normal...");
            beatmap = new Beatmap("Assets/Resources/Songs/ALiVE_Normal.osu");
        }
        startPos = new Vector2(-372f, 134.2F);
        endPos = new Vector2(322.37F, 134.2F);

        maxHealth = GetComponent<AudioSource>().clip.length;
        StartCoroutine(introDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (delayLock == false)
        {
            if (!notesDone)
            {
                nextHit = beatmap.HitObjects[hitIndex].StartTimeInMiliseconds();

                if (beatmap.HitObjects[hitIndex].HitObjectType == HitObjectType.Slider)
                {
                    nextHitEnd = ((SliderObject)beatmap.HitObjects[hitIndex]).EndTimeInMs((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier) + nextHit;
                }
            }
            else if (!GetComponent<AudioSource>().isPlaying && notesDone && !songDone)
            {
                songDone = true;
                Debug.Log("Song ended.");
            }

            float currHealth = GetComponent<AudioSource>().time;
            healthBar.transform.localScale = new Vector3(((maxHealth - currHealth) / maxHealth), transform.localScale.y, transform.localScale.z);

            metronome.Update((decimal)AudioSettings.dspTime, (decimal)Time.deltaTime);
        }
    }

    void SpawnNote(object sender, Metronome.TickEventArgs e)
    {
        float endTime;
        float nextBeat;

        //Song ended
        if (noteIndex >= beatmap.HitObjects.Count)
        {
            notesDone = true;
            return;
        }

        if (iterationsLeft == 2 || firstNoteOfSlider == false)
        {
            noteTag = "SecondaryNote";
            HitObject currObject = beatmap.HitObjects[noteIndex];
            float startTime = currObject.StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat);
            endTime = ((SliderObject)currObject).EndTimeInBeats((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier) + startTime;

            nextBeat = endTime;
            iterationsLeft = 1;
            firstNoteOfSlider = false;
        }
        else
        {
            noteTag = "PrimaryNote";
            nextBeat = beatmap.HitObjects[noteIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat);
            firstNoteOfSlider = true;
        }

        Debug.Log(beatmap.HitObjects[hitIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat) + " == " + e.positionInBeats);
        if (e.positionInBeats > beatmap.HitObjects[hitIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat))
        {
            StartCoroutine(GetComponent<CharacterListener>().spawnNoteScore(new Vector3(2.45f, 1.87f, -7.77f), 0.3f, Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss")));
            hitIndex++;
            Debug.Log("MISS");
        }

        //Spawn next note
        if (e.positionInBeats == (nextBeat - beatmap.GetApproachRate()))
        {
            bool inSliderRange = beatmap.HitObjects[noteIndex].HitObjectType == HitObjectType.Slider;
            if (inSliderRange && firstNoteOfSlider == true)
            {
                iterationsLeft = 2;
            }

            GameObject beatSprite;
            beatSprite = Instantiate(test, startPos, Quaternion.identity);
            beatSprite.transform.SetParent(GameObject.FindGameObjectWithTag("NotesLayer").transform, false);

            metronome.Tick += beatSprite.GetComponent<CircleNote>().UpdateSongPosition;

            if (inSliderRange)
            {
                //Debug.Log(string.Format("Spawning slider note {0} at beat {1}.", noteIndex, e.positionInBeats));
                //beatSprite.GetComponent<Image>().color = Color.green;

                if (firstNoteOfSlider == true)
                {
                    HitObject currObject = beatmap.HitObjects[noteIndex];
                    float startTime = currObject.StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat);
                    endTime = ((SliderObject)currObject).EndTimeInBeats((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier) + startTime;

                    //Debug.Log(string.Format("Spawning slider bar for index {0} at beat {1}.", noteIndex, e.positionInBeats));
                    beatSprite.GetComponent<CircleNote>().firstNoteOfSlider = firstNoteOfSlider;
                    beatSprite.GetComponent<CircleNote>().endTimeOfSlider = endTime;

                    Vector2 barStartPos = startPos;
                    Vector2 barEndPos = endPos;

                    barStartPos.x -= GameObject.FindGameObjectWithTag("NotesLayer").transform.GetComponent<RectTransform>().rect.width / 2;
                    barEndPos.x -= GameObject.FindGameObjectWithTag("NotesLayer").transform.GetComponent<RectTransform>().rect.width / 2;

                    GameObject holdNoteGO;
                    holdNoteGO = Instantiate(holdNote, startPos, Quaternion.identity);
                    holdNoteGO.transform.SetParent(GameObject.FindGameObjectWithTag("NotesLayer").transform, false);

                    metronome.Tick += holdNoteGO.GetComponent<SliderNote>().UpdateSongPosition;

                    holdNoteGO.GetComponent<SliderNote>().id = noteIndex;
                    holdNoteGO.GetComponent<SliderNote>().startTimeInBeats = nextBeat;
                    holdNoteGO.GetComponent<SliderNote>().endTimeInBeats = nextBeat + ((SliderObject)beatmap.HitObjects[noteIndex]).EndTimeInBeats((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier);
                    holdNoteGO.GetComponent<SliderNote>().startPos = barStartPos;
                    holdNoteGO.GetComponent<SliderNote>().endPos = barEndPos;

                    holdNoteGO.GetComponent<SliderNote>().approachRate = beatmap.GetApproachRate();
                    holdNoteGO.GetComponent<SliderNote>().songPosInBeats = e.positionInBeats;
                }
            }

            beatSprite.gameObject.tag = noteTag;

            beatSprite.GetComponent<CircleNote>().id = noteIndex;
            beatSprite.GetComponent<CircleNote>().startTimeInBeats = nextBeat;
            beatSprite.GetComponent<CircleNote>().startPos = startPos;
            beatSprite.GetComponent<CircleNote>().endPos = endPos;
            beatSprite.GetComponent<CircleNote>().beatNumber = nextBeat;

            beatSprite.GetComponent<CircleNote>().approachRate = beatmap.GetApproachRate();
            beatSprite.GetComponent<CircleNote>().songPosInBeats = e.positionInBeats;

            if (iterationsLeft == 1)
            {
                firstNoteOfSlider = true;
                noteIndex++;
            }
        }
    }
}
