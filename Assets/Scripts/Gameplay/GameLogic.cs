using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using OsuParser;
using UnityEngine.SceneManagement;
/* This is used for debugging */
using System.IO;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    /* GameObjects that will appear on the Canvas*/
    public GameObject healthBarBoss;
    private GameObject specialBar;
    public GameObject characterSpecialBar;
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
    public static bool nextHitHold = false;
    public int hitIndex = 0;
    public bool introFinished;
    public bool holdNoteInterval = false;

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
    public bool gameOver=  false;

    /* Variables used for note movement */
    public Beatmap beatmap;
    private Metronome metronome;
    private Vector2 startPos;
    private Vector2 endPos;
    private int noteIndex = 0;
    private decimal latency = 0.150m;
    public static decimal songStartTime;

    public void spawnCharacters()
    {
        /* This loop counts how many characters are on the current team and spawns them to the correct location.
         * HealthBars are also spawned and linked to their respective character. */
        Dictionary<int, Character> charactersInParty = GameManager.Instance.gameDataManager.GetCharactersInParty();
        foreach (int position in charactersInParty.Keys)
        {
            Vector3 characterSpawnPosition;
            Vector3 healthPos;
            Vector3 specialPos;

            //Front row
            if(position == (int)CharacterPosition.FrontRow)
            {
                characterSpawnPosition = new Vector3(2.87f, 1.75f, -4.8f);
                healthPos = new Vector3(216.3f, 107.34f, 0.0f);
                specialPos = new Vector3(203.77f, 99.6f, 0.0f);
                //Centre left (top)
            }
            else if(position == (int)CharacterPosition.CentreLeft)
            {
                characterSpawnPosition = new Vector3(1.02f, 0.33f, -5.1f);
                healthPos = new Vector3(92.6f, -134.4f, 0.0f);
                specialPos = new Vector3(80f, -142f, 0.0f);
            }
            //Centre right (bottom)
            else if (position == (int)CharacterPosition.CentreRight)
            {
                characterSpawnPosition = new Vector3(2.52f, -0.82f, -5.5f);
                healthPos = new Vector3(216.3f, -210.0f, 0.0f);
                specialPos = new Vector3(203.77f, -217.5f, 0.0f);
            }
            //Back row
            else
            {
                characterSpawnPosition = new Vector3(4.26f, 0.32f, -5.1f);
                healthPos = new Vector3(350.6f, -134.4f, 0.0f);
                specialPos = new Vector3(338f, -142f, 0.0f);
            }

            /* Health Bars */
            GameObject healthBar = Instantiate(characterHealthBar, healthPos, Quaternion.identity);
            healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Health Bar").transform, false);
            healthBar.name = "character_health_" + (position + 1);

            /* Special Bars */
            GameObject specialBar = Instantiate(characterSpecialBar, specialPos, Quaternion.identity);
            specialBar.transform.SetParent(GameObject.FindGameObjectWithTag("Health Bar").transform, false);
            specialBar.name = "character_special_" + (position + 1);

            characterPlaceholder.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(charactersInParty[position].sprite);
            GameObject spawnedPlayer = Instantiate(characterPlaceholder, characterSpawnPosition, Quaternion.identity) as GameObject;

            /* Characters */
            spawnedPlayer.name = "character_" + (position + 1);
            spawnedPlayer.GetComponent<CharacterLogic>().hp = charactersInParty[position].hp;
            spawnedPlayer.GetComponent<CharacterLogic>().currentHp = charactersInParty[position].hp;
            spawnedPlayer.GetComponent<CharacterLogic>().atk = charactersInParty[position].atk;
            spawnedPlayer.GetComponent<CharacterLogic>().def = charactersInParty[position].def;
            spawnedPlayer.GetComponent<CharacterLogic>().mgc = charactersInParty[position].mgc;
            spawnedPlayer.GetComponent<CharacterLogic>().rcv = charactersInParty[position].rcv;
            //TODO: This needs to eventually be changed to
            //spawnedPlayer.GetComponent<CharacterLogic>().attack = charactersInParty[position].magicAbility;
            spawnedPlayer.GetComponent<CharacterLogic>().attack = "fireball";   
        }
    }

    /* Returns the next note's beat in the song */
    public static float getNextHit()
    {
        if (nextHitHold)
        {
            return nextHitEnd;
        }
        else
        {
            return nextHit;
        }
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

    IEnumerator updateScoreCanvas(Canvas scoreCanvas, string grade)
    {
        AudioSource cashAudioSource = GameObject.FindGameObjectWithTag("ScoreLayer").GetComponent<AudioSource>();

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
                text.color = GradeColor(grade);
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Text text in scoreCanvas.GetComponentsInChildren<Text>())
        {
            if (text.name == ("Money"))
            {
                text.text += "$ " + VictoryCoin(grade);
                cashAudioSource.PlayOneShot(moneyClip(grade), 1.0f);
            }
        }

        returnButton.gameObject.SetActive(true);

        yield return null;
    }

    private Color GradeColor(string grade)
    {
        switch (grade)
        {
            case "S": return new Color(0, 255, 0);
            case "A": return new Color(0, 0, 255);
            case "B": return new Color(128, 128, 128);
            case "C": return new Color(255, 255, 0);
            case "D": return new Color(255, 165, 1);
            case "F": return new Color(255, 0, 0);
            default:  return new Color(128, 128, 128);                
        }        
    }
    
    private int VictoryCoin(string grade)
    {
        switch (grade)
        {
            case "S": return 3000;
            case "A": return 2500;
            case "B": return 2000;
            case "C": return 1500;
            case "D": return 1000;
            case "F": return 500;
            default: return 0;
        }
    }

    private AudioClip moneyClip(string grade)
    {
        switch (grade)
        {
            case "S": return (AudioClip)Resources.Load("SoundEffects/shop_purchase_special_item");
            case "A": return (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
            case "B": return (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
            case "C": return (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
            case "D": return (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
            case "F": return (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");
            default:  return (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");
        }
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

        /* This is the default song in case of an error */
        Debug.Log("Loading beatmap file for RedLips_Easy...");
        beatmap = new Beatmap(Application.streamingAssetsPath + "/Beatmaps/RedLips_Easy.osu");
        GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Songs/RedLips");

        GetComponent<BossLogic>().setupBoss();

        /* The spawn and kill points for incoming notes */
        startPos = new Vector2(-355.4f, 170.54F);
        endPos = new Vector2(352.0F, 170.54F);

        /* Tentative: Set boss' health tothe duration of the selected song */
        maxHealth = GetComponent<AudioSource>().clip.length;

        /* Plays the opening curtain animation */
        StartCoroutine(introDelay());
    }

    /* Update is called once per frame */
    void Update()
    {
        if (!gameOver)
        {
            if (songDone && !scoringDone)
            {
                Canvas scoreCanvas = GameObject.FindGameObjectWithTag("ScoreLayer").GetComponent<Canvas>();

                scoreCanvas.enabled = true;
                StartCoroutine(updateScoreCanvas(scoreCanvas, CaclulateSongGrade()));
                scoringDone = true;
            }

            //Debug.Log("Hold Interval: " + holdNoteInterval);

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
                        nextHitHold = true;
                        nextHitEnd = ((SliderObject)beatmap.HitObjects[hitIndex]).EndTimeInMs((float)beatmap.TimingPoints[0].TimePerBeat, beatmap.SliderMultiplier) + nextHit;
                    }
                    else
                    {
                        nextHitHold = false;
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
                healthBarBoss.transform.localScale = new Vector3(((maxHealth - currHealth) / maxHealth), transform.localScale.y, transform.localScale.z);

                /* Notifies the metronome of the current time, so it can publish a message to us at the expected time */
                metronome.Update((decimal)AudioSettings.dspTime, (decimal)Time.deltaTime);
            }
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

        /* Functional Requirement 
         * ID: 8.1-1
         * Description: The player must be able to view incoming notes.
         *
         * Spawn next note */
        //Debug.Log(string.Format("Position in Beats: {0} Next Note at Beat: {1}",e.positionInBeats,nextBeat-beatmap.GetApproachRate()));
        if (e.positionInBeats >= (nextBeat - beatmap.GetApproachRate()))
        {
            //Debug.Log(string.Format("Spawned note {0}!", hitIndex));
            /* The variable 'iterationsLeft' is used to keep track of where we are in a slider note in terms
             * of spawn and movement */
            string noteName = "";
            bool inSliderRange = beatmap.HitObjects[noteIndex].HitObjectType == HitObjectType.Slider;
            if (inSliderRange)
            {
                if (firstNoteOfSlider == true)
                {
                    iterationsLeft = 2;
                    noteName = "heldStart";
                }
                else
                {
                    noteName = "heldEnd";
                }
            }

            /* Instantiates the notes on the canvas, to avoid Z-fighting with the background elements */
            GameObject beatSprite;
            beatSprite = Instantiate(test, startPos, Quaternion.identity);
            beatSprite.name = noteName + "Note_" + noteIndex;

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
                    beatSprite.gameObject.name = "defendNoteStart_" + noteIndex;

                    beatSprite.GetComponent<CircleNote>().defendTarget = defendNote;
                    beatSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManager.Instance.gameDataManager.GetCharactersInParty()[defendNote].headshot);

                    defenseState = defendNote;

                }
                /* These are notes that either end a hold note pair or are just regular standalone notes*/
                else
                {
                    beatSprite.gameObject.name = "defendNoteEnd_" + noteIndex;

                    /* This is a standalone note */
                    if (defendNote > -1)
                    {
                        beatSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManager.Instance.gameDataManager.GetCharactersInParty()[defendNote].headshot);
                        beatSprite.GetComponent<CircleNote>().defendTarget = defendNote;
                    }
                    /* This is a note that ends a hold note pair */
                    else
                    {
                        beatSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManager.Instance.gameDataManager.GetCharactersInParty()[defenseState].headshot);
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
