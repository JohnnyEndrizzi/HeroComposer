﻿using Game;
using System;
using UnityEngine;

public class Metronome {
    public decimal tickLength = 0m;
    private float beatDivisions = 128; 
    private decimal nextTickTime;
    private float positionInBeats = 0f;
    public Boolean initalTick = false;

    public event EventHandler<TickEventArgs> Tick;
    public class TickEventArgs : EventArgs
    {
        public float positionInBeats { get; set; }
    }
    public delegate void EventHandler(Metronome m, EventArgs e);

    /* Functional Requirement
     * ID: 8.1-1
     * Description: The player must be able to view incoming notes.
     * 
     * This is the constructor for Metronome 
     * The metronome object is how we get passed the innaccuracies of the Update function. Even when using FixedUpdate, the 
     * benchmark for its calls are inconsistent (you can specify 60 FPS, but that only ensures that the overall average of all
     * calls will be ~60 FPS). Our rhythm game requires consistent and accurate timing, and Metronome is what provides that. 
     * It acts as a time publisher for its subscribers. */
    public Metronome(decimal startTime, decimal tickLength)
    {    
        /* Converts ms to s and divide tick length by the number of divisions in a beat */
        this.tickLength = tickLength/1000m/(decimal)this.beatDivisions;
        nextTickTime = startTime + this.tickLength;
        Debug.Log(string.Format("Created metronome at start time {0} with tick length {1} and beat divisions {2}.\nNext tick time {3}",startTime,this.tickLength,beatDivisions,nextTickTime));
    }    
	
	/* Update is called once per frame */
	public void Update (decimal currentTime, decimal deltaTime)
    {
        currentTime += deltaTime;
        TickEventArgs args = new TickEventArgs();

        /* Fire an initial tick on beat 0 */
        if (initalTick == false)
        {
            args.positionInBeats = 0;
            Debug.Log("Firing initial tick at beat 0.");
            Tick(this, args);
            initalTick = true;
        }

        /* Look ahead ~1 frame and capture any metronome ticks in that timeframe */
        while (currentTime > nextTickTime)
        {
            positionInBeats += (float)1/beatDivisions;
            args.positionInBeats = positionInBeats;
            Tick(this, args);
            nextTickTime += tickLength;
        }
    }
}
