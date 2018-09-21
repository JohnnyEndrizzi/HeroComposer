using Game;
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

    //Constructor 
    public Metronome(decimal startTime, decimal tickLength)
    {    
        //convert ms to s and divide tick length by the number of divisions in a beat
        this.tickLength = tickLength/1000m/(decimal)this.beatDivisions; 
        nextTickTime = startTime + this.tickLength;
        Debug.Log(string.Format("Created metronome at start time {0} with tick length {1} and beat divisions {2}.\nNext tick time {3}",startTime,this.tickLength,beatDivisions,nextTickTime));
    }    
	
	// Update is called once per frame
	public void Update (decimal currentTime, decimal deltaTime) {
        currentTime += deltaTime;

        TickEventArgs args = new TickEventArgs();

        //Fire an initial tick on beat 0
        if (initalTick == false)
        {

            args.positionInBeats = 0;
            Tick(this, args);
            Debug.Log("Metronome ticked at beat 0.");
            initalTick = true;
        }

        //Look ahead 1 frame (approx) and capture any metronome ticks in that timeframe 
        while (currentTime > nextTickTime)
        {
            positionInBeats += (float)1/beatDivisions;
            args.positionInBeats = positionInBeats;
            Tick(this, args);
            Debug.Log(string.Format("Metronome ticked for beat {0} at {1}. Next tick at {2}", positionInBeats, currentTime, nextTickTime));
            nextTickTime += tickLength;
        }
    }
}
