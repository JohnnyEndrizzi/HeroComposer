using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using OsuParser;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    /* GameObjects that will appear on the Canvas*/
    public GameObject healthBar;
    public GameObject characterHealthBar;
    public GameObject holdNote;
    public GameObject test;
    public SpriteRenderer note;

    /* Used to spawn in characters */
    public GameObject characterPlaceholder;

    /* Used for the opening curtain animation */
    public SpriteRenderer curtains;
    bool delayLock = true;

    /* Variables used for hit logic */
    public static float nextHit;
    public static float nextBeat;
    public static float nextHitEnd = 0;
    public static int hitIndex = 0;
    public bool introFinished;

    /* Variables used for defending against attacks and health logic*/
    public static int defendNote = -1;
    private float maxHealth;

    /* Variables used for note logic */
    private string noteTag;
    private int iterationsLeft = 1;
    private bool firstNoteOfSlider = true;
    private int defenseState = -1;

    /* Variables used for song completion logic */
    private bool notesDone = false;
    public static bool songDone = false;
    private static bool scoringDone = false;
    [SerializeField]
    private Button returnButton;

    /* Variables used for note movement */
    public Beatmap beatmap;
    private Metronome metronome;
    private Vector2 startPos;
    private Vector2 endPos;
    private int noteIndex = 0;
    private decimal latency = 0.150m;
    public static decimal songStartTime;

    /* The following function spawns in the selected characters that are sent to ApplicationModel */
    public void spawnCharacters()
    {
        /* This loop counts how many characters are on the current team and spawns them to the correct location.
         * HealthBars are also spawned and linked to their respective character. */
        for (int i = 0; i < Assets.Scripts.MainMenu.ApplicationModel.characters.Length; i++)
        {
            Vector3 characterSpawnPosition;
            Vector3 healthPos;

            if (Assets.Scripts.MainMenu.ApplicationModel.characters[i] != null)
            {
                if (i == 0)
                {
                    characterSpawnPosition = new Vector3(3.29f, 1.75f, -4.8f);
                    healthPos = new Vector3(187.7f, 97.2f, 0.0f);
                }
                else if (i == 1)
                {
                    characterSpawnPosition = new Vector3(1.02f, 0.33f, -5.1f);
                    healthPos = new Vector3(74.3f, -103.2f, 0.0f);
                }
                else if (i == 2)
                {
                    characterSpawnPosition = new Vector3(2.94f, -0.82f, -5.5f);
                    healthPos = new Vector3(198.6f, -166.4f, 0.0f);
                }
                else
                {
                    characterSpawnPosition = new Vector3(5.31f, 0.32f, -5.1f);
                    healthPos = new Vector3(337.0f, -103.2f, 0.0f);
                }

                /* Health Bars */
                GameObject healthBar = Instantiate(characterHealthBar, healthPos, Quaternion.identity);
                healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Health Bar").transform, false);
                healthBar.name = "character_health_" + (i + 1);

                characterPlaceholder.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Assets.Scripts.MainMenu.ApplicationModel.characters[i].sprite);
                GameObject spawnedPlayer = Instantiate(characterPlaceholder, characterSpawnPosition, Quaternion.identity) as GameObject;

                /* Characters */
                spawnedPlayer.name = "character_" + (i + 1);
                spawnedPlayer.GetComponent<CharacterLogic>().hp = Assets.Scripts.MainMenu.ApplicationModel.characters[i].hp;
                spawnedPlayer.GetComponent<CharacterLogic>().currentHp = Assets.Scripts.MainMenu.ApplicationModel.characters[i].hp;
                spawnedPlayer.GetComponent<CharacterLogic>().atk = Assets.Scripts.MainMenu.ApplicationModel.characters[i].atk;
                spawnedPlayer.GetComponent<CharacterLogic>().def = Assets.Scripts.MainMenu.ApplicationModel.characters[i].def;
                spawnedPlayer.GetComponent<CharacterLogic>().mgc = Assets.Scripts.MainMenu.ApplicationModel.characters[i].mgc;
                spawnedPlayer.GetComponent<CharacterLogic>().rcv = Assets.Scripts.MainMenu.ApplicationModel.characters[i].rcv;
                spawnedPlayer.GetComponent<CharacterLogic>().attack = Assets.Scripts.MainMenu.ApplicationModel.characters[i].mag_Eqp;
            }
        }
    }

    /* Returns the next note's beat in the song */
    public float getNextHit()
    {
        return nextHit;
    }

    /* Sets the next noteas a defend note */
    public void setDefendNote(int value)
    {
        defendNote = value;
    }

    /* Returns the next note's beat in the song */
    public float getNextBeat()
    {
        return nextBeat;
    }

    /* Returns the starting beat of the selected beatmap */
    public decimal getSongStartTime()
    {
        return songStartTime;
    }

    /* Returns the state of the song */
    public bool isSongDone()
    {
        return songDone;
    }

    /* Animation sequence used to open curtains at the start of a song */
    private IEnumerator introDelay()
    {
        yield return new WaitForSeconds(6.0f);
        songStartTime = (decimal)AudioSettings.dspTime + latency;

        /* Creates the metronome publisher and connects it to the SpawnNote function as its subscriber */
        metronome = new Metronome(songStartTime, beatmap.TimingPoints[0].TimePerBeat);
        metronome.Tick += SpawnNote;

        /* Plays applause */
        GetComponent<AudioSource>().Play();

        delayLock = false;
        yield return null;
    }

    public void tempLoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    IEnumerator updateScoreCanvas(Canvas scoreCanvas, string grade)
    {
        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.text.Contains("Perfect"))
            {
                text.text += GetComponent<CharacterListener>().perfectCount;
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.text.Contains("Great"))
            {
                text.text += GetComponent<CharacterListener>().greatCount;
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.text.Contains("Good"))
            {
                text.text += GetComponent<CharacterListener>().goodCount;
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.text.Contains("Miss"))
            {
                text.text += GetComponent<CharacterListener>().missCount;
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.text.Contains("Final Score"))
            {
                text.text += GetComponent<CharacterListener>().songScore;
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.name == "Grade")
            {
                text.text = grade;
            }
        }

        returnButton.gameObject.SetActive(true);

        yield return null;
    }

    private string CaclulateSongGrade()
    {
        if (beatmap.HitObjects.Count * 300.0f == GetComponent<CharacterListener>().songScore)
        {
            return "S";
        }
        else if (GetComponent<CharacterListener>().songScore > beatmap.HitObjects.Count * 300.0f * 0.85f)
        {
            return "A";
        }
        else if (GetComponent<CharacterListener>().songScore > beatmap.HitObjects.Count * 300.0f * 0.75f)
        {
            return "B";
        }
        else if (GetComponent<CharacterListener>().songScore > beatmap.HitObjects.Count * 300.0f * 0.60f)
        {
            return "C";
        }
        else if (GetComponent<CharacterListener>().songScore > beatmap.HitObjects.Count * 300.0f * 0.50f)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }

    /* Used for initialization */
    void Start ()
    {
        /* Spawns the current team */
        spawnCharacters();

        if (Assets.Scripts.MainMenu.ApplicationModel.songPathName != "")
        {
            /* Creates a beatmap object from the selected song */
            Debug.Log("Loading beatmap file for " + Assets.Scripts.MainMenu.ApplicationModel.songPathName + "...");
            beatmap = new Beatmap("Assets/Resources/Songs/" + Assets.Scripts.MainMenu.ApplicationModel.songPathName + ".osu");
            Debug.Log(Assets.Scripts.MainMenu.ApplicationModel.songName);
            GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Songs/" + Assets.Scripts.MainMenu.ApplicationModel.songName);
        }
        else
        {
            /* This is the default song in case of an error */
            Debug.Log("Loading beatmap file for ALiVE_Normal...");
            beatmap = new Beatmap("Assets/Resources/Songs/ALiVE_Normal.osu");
            GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Songs/ALiVE");
        }

        /* The spawn and kill points for incoming notes */
        startPos = new Vector2(-372f, 134.2F);
        endPos = new Vector2(322.37F, 134.2F);

        /* Tentative: Set boss' health tothe duration of the selected song */
        maxHealth = GetComponent<AudioSource>().clip.length;

        /* Plays the opening curtain animation */
        StartCoroutine(introDelay());
    }

    /* Update is called once per frame */
    void Update()
    {
        if (songDone && !scoringDone)
        {
            Canvas scoreCanvas = GameObject.FindGameObjectWithTag("ScoreLayer").GetComponent<Canvas>();

            scoreCanvas.enabled = true;
            StartCoroutine(updateScoreCanvas(scoreCanvas, CaclulateSongGrade()));
            scoringDone = true;
        }

        if (delayLock == false)
        {
            /* There are 4 states of a song:
             * 1) introDelay phase (pre-song)
             * 2) Song (incoming notes)
             * 3) notesDone phase (but song is still playing)
             * 4) songDone phase (song and notes are finished, and level ends) */
            if (!notesDone)
            {
                /* This is the time in millseconds in respect to the start of the song of when the next note will arrive */
                nextHit = beatmap.HitObjects[hitIndex].StartTimeInMiliseconds();
                nextBeat = beatmap.HitObjects[hitIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat);

                /* Since held notes pairs are stored together, if the next note is a hold note the end time is also saved */
                if (beatmap.HitObjects[hitIndex].HitObjectType == HitObjectType.Slider)
                {
                    nextHitEnd = ((SliderObject)beatmap.HitObjects[hitIndex]).EndTimeInMs((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier) + nextHit;
                }
            }
            else if (!GetComponent<AudioSource>().isPlaying && notesDone && !songDone)
            {
                /* Waits for the song's AudioClip to finish, then marks it as done */
                songDone = true;
                Debug.Log("Song ended.");
            }

            /* Updates the boss' health as the song plays out */
            float currHealth = GetComponent<AudioSource>().time;
            healthBar.transform.localScale = new Vector3(((maxHealth - currHealth) / maxHealth), transform.localScale.y, transform.localScale.z);

            /* Notifies the metronome of the current time, so it can publish a message to us at the expected time */
            metronome.Update((decimal)AudioSettings.dspTime, (decimal)Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (!pauseMenu.enabled) //TODO
            //{
            //    //Unpause game
            //    //close pause menu
            //}
            //if (pauseMenu.enabled)
            //{
            //    //Pause game
            //    //open pause menu
            //}
        }
    }

    /* Functional Requirement 
     * ID: 8.1-1
     * Description: The player must be able to view incoming notes.
     *
     * This function will spawn notes in accordance to published messages from the metronome */
    void SpawnNote(object sender, Metronome.TickEventArgs e)
    {
        float endTime;
        float nextBeat;

        /* Functional Requirement
         * ID: 8.1.1-5
         * Description: The player must be able to complete a level.
         * 
         * The song and incoming notes have ended */
        if (noteIndex >= beatmap.HitObjects.Count)
        {
            notesDone = true;
            return;
        }

        /* The following is the logic that defines how held notes spawn, which sets appropriate 'SecondaryNote' and 
         * 'PrimaryNote' labels to each GameObject */ 
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

        //Debug.Log(beatmap.HitObjects[hitIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat) + " == " + e.positionInBeats);

        /* This will display a 'Miss' label when a note travels past the input region of the note bar. */
        if (e.positionInBeats > beatmap.HitObjects[hitIndex].StartTimeInBeats(beatmap.TimingPoints[0].TimePerBeat))
        {
            GetComponent<CharacterListener>().missCount++;
            StartCoroutine(GetComponent<CharacterListener>().spawnNoteScore(new Vector3(2.45f, 1.87f, -7.77f), 0.3f, Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss")));
            hitIndex++;
        }

        /* Functional Requirement 
         * ID: 8.1-1
         * Description: The player must be able to view incoming notes.
         *
         * Spawn next note */
        if (e.positionInBeats == (nextBeat - beatmap.GetApproachRate()))
        {
            /* The variable 'iterationsLeft' is used to keep track of where we are in a slider note in terms
             * of spawn and movement */
            bool inSliderRange = beatmap.HitObjects[noteIndex].HitObjectType == HitObjectType.Slider;
            if (inSliderRange && firstNoteOfSlider == true)
            {
                iterationsLeft = 2;
            }

            /* Instantiates the notes on the cavas, to avoid Z-fighting with the background elements */
            GameObject beatSprite;
            beatSprite = Instantiate(test, startPos, Quaternion.identity);

            /* Adds the note to the NotesLayer canvas */
            beatSprite.transform.SetParent(GameObject.FindGameObjectWithTag("NotesLayer").transform, false);

            /* Functional Requirement
             * ID: 8.1.1-4
             * Description: The system must allow the enemies to randomly attack the players throughout a song.
             * 
             * This code will spawn the corresponding character's headshot on the notebar to represent a queued attack by the
             * boss This code will either run when: 1) The boss has queued an attack; 2) They queued at the start of a hold note 
             * and the end note in the pair is next */
            if ((defendNote != -1 && firstNoteOfSlider == true) || (defenseState > -1 && firstNoteOfSlider == false))
            {
                /* These are notes that lead a hold note pair*/
                if (firstNoteOfSlider == true && iterationsLeft == 2)
                {
                    beatSprite.gameObject.name = "defendNoteStart";

                    beatSprite.GetComponent<CircleNote>().defendTarget = defendNote;
                    beatSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(Assets.Scripts.MainMenu.ApplicationModel.characters[defendNote].headshot);

                    defenseState = defendNote;

                }
                /* These are notes that either end a hold note pair or are just regular standalone notes*/
                else
                {
                    beatSprite.gameObject.name = "defendNoteEnd";

                    /* This is a standalone note */
                    if (defendNote > -1)
                    {
                        beatSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(Assets.Scripts.MainMenu.ApplicationModel.characters[defendNote].headshot);
                        beatSprite.GetComponent<CircleNote>().defendTarget = defendNote;
                    }
                    /* This is a note that ends a hold note pair */
                    else
                    {
                        beatSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(Assets.Scripts.MainMenu.ApplicationModel.characters[defenseState].headshot);
                        beatSprite.GetComponent<CircleNote>().defendTarget = defenseState;
                    }

                    defenseState = -1;
                }

                /* Resize the character's headshot to be more visible on the notebar */
                ((RectTransform)beatSprite.GetComponent(typeof(RectTransform))).sizeDelta = new Vector2(150, 150);
                defendNote = -1;
            }

            /* Subscribing to the metronome will allow us to receive notes faster than what the traditional 'Update'
             * function allows. Rather than operating based on FPS, it operates on a beat-basis.*/
            metronome.Tick += beatSprite.GetComponent<CircleNote>().UpdateSongPosition;
            metronome.Tick += GetComponent<BossLogic>().Test;

            /* Functional Requirement 
             * ID: 8.1-1
             * Description: The player must be able to view incoming notes.
             *
             * Different logic is required if the incoming note is a part of a held note */
            if (inSliderRange)
            {
                //Debug.Log(string.Format("Spawning slider note {0} at beat {1}.", noteIndex, e.positionInBeats));

                /* If the next note is the first of a held note pair, we must spawn the bar connecting the two notes as well */
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

                    /* Transforms the world coordinates to canvas coordinates. This is necessary because the anchors of the middle bar 
                     * have been shifted to account for its skewed scaling */
                    barStartPos.x -= GameObject.FindGameObjectWithTag("NotesLayer").transform.GetComponent<RectTransform>().rect.width / 2;
                    barEndPos.x -= GameObject.FindGameObjectWithTag("NotesLayer").transform.GetComponent<RectTransform>().rect.width / 2;

                    /* Spawns the held note's connecting bar */
                    GameObject holdNoteGO;
                    holdNoteGO = Instantiate(holdNote, startPos, Quaternion.identity);
                    holdNoteGO.transform.SetParent(GameObject.FindGameObjectWithTag("NotesLayer").transform, false);
                    holdNoteGO.transform.SetAsFirstSibling();

                    /* The slider note must also subscribe to the metronome publisher */
                    metronome.Tick += holdNoteGO.GetComponent<SliderNote>().UpdateSongPosition;

                    /* "Deserializes" the values of the held note to the GameObject */
                    holdNoteGO.GetComponent<SliderNote>().id = noteIndex;
                    holdNoteGO.GetComponent<SliderNote>().startTimeInBeats = nextBeat;
                    holdNoteGO.GetComponent<SliderNote>().endTimeInBeats = nextBeat + ((SliderObject)beatmap.HitObjects[noteIndex]).EndTimeInBeats((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier);
                    holdNoteGO.GetComponent<SliderNote>().startPos = barStartPos;
                    holdNoteGO.GetComponent<SliderNote>().endPos = barEndPos;
                    holdNoteGO.GetComponent<SliderNote>().approachRate = beatmap.GetApproachRate();
                    holdNoteGO.GetComponent<SliderNote>().songPosInBeats = e.positionInBeats;
                }
            }

            /* "Deserializes" the values of the next note to the GameObject */
            beatSprite.gameObject.tag = noteTag;
            beatSprite.GetComponent<CircleNote>().id = noteIndex;
            beatSprite.GetComponent<CircleNote>().startTimeInBeats = nextBeat;
            beatSprite.GetComponent<CircleNote>().startPos = startPos;
            beatSprite.GetComponent<CircleNote>().endPos = endPos;
            beatSprite.GetComponent<CircleNote>().beatNumber = nextBeat;
            beatSprite.GetComponent<CircleNote>().approachRate = beatmap.GetApproachRate();
            beatSprite.GetComponent<CircleNote>().songPosInBeats = e.positionInBeats;

            /* The first note has been killed */
            if (iterationsLeft == 1)
            {
                firstNoteOfSlider = true;
                noteIndex++;
            }
        }
    }
}
