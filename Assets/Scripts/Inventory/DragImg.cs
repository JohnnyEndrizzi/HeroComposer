using UnityEngine;
using UnityEngine.UI;

public class DragImg : MonoBehaviour {
    //An image that follows the mouse
    //Currently bugged sometimes?
    //Computer 1 & 2: Follows mouse on normal view, Does not follow mouse on maximise, Does not follow mouse in build
    //Computer 3: Does not follow mouse on normal view, Follow mouse on maximise, Follows mouse in build

    //Gameobject locations
    [SerializeField]
    private Image imgIcon = null;

    void Update() {
        int ScW = Screen.width;
        int ScH = Screen.height;

        Vector3 centreFixHalf = new Vector3(ScW/2, ScH/2, 0f);  //Half Screen x,y
        Vector3 centreFix = new Vector3(ScW, ScH, 0f);          //Screen x,y

        Vector3 temp = Input.mousePosition; //Mouse Follow

        //Debug.Log("mouseX: " + Input.mousePosition.x + " mouseY: " + Input.mousePosition.y);
        //Debug.log("screenX: " + screen.width.tostring() + " screenY: " + screen.height.tostring());
        //Debug.Log("halfScreenX: " + centreFixHalf.x.ToString() + " halfScreenY: " + centreFixHalf.y.ToString());

        temp = temp - centreFixHalf; //fix 0,0 offset from bottom left corner to centre sceen
        temp.z = 0f; //z-coord should always be 0

        //Debug.Log("localPosX: " + this.transform.localPosition.x.ToString() + " localPosY: " + this.transform.localPosition.y.ToString());
        //Debug.Log("posX: " + this.transform.position.x.ToString() + " posY: " + this.transform.position.y.ToString());

        ////attempted to change view didnt affect the issue
        //temp = Camera.main.ScreenToWorldPoint(temp);
        //temp = Camera.main.ScreenToViewportPoint(temp);

        ////attempt to account for changing resolutions
        //temp.x = temp.x * centreFix.x;
        //temp.y = temp.y * centreFix.y;

        ////attempt to account for a 2x offset
        //temp.x = temp.x /2;
        //temp.y = temp.y /2;





        ////to maximize(or minimize) screen mid game right click "Game" tab 
        ////Currently this works in normal view but is off by a multiplier in maximise
        ////I am currently on version 2018.2.14f1 but newer version mention nothing related in release notes
        ///to test in game Play on scene Menu then click the top right or bottom left doors, then pick up something with left click

        this.transform.localPosition = temp; //set dragImg position
        



        //this.transform.position = temp; //does bad things (wierdly large values both +ve and -ve)

        ////These are always zero as they should be
        /// mouse w/o offset - localPos  (so it is where it 'should' be)
        //float debugX = Input.mousePosition.x - centreFixHalf.x - this.transform.localPosition.x;
        //float debugY = Input.mousePosition.y - centreFixHalf.y - this.transform.localPosition.y;
        //Debug.Log("X1: " + debugX.ToString() + " Y1: " + debugY.ToString());


        ////other attempts
        //temp.x = temp.x - debugX;
        //temp.y = temp.y - debugY;      
    }

    public void SetIcon(Sprite image) //Set Sprite on icon child object
    {
        if (image == null){imgIcon.GetComponent<Image>().enabled = false;}
        else{imgIcon.GetComponent<Image>().enabled = true;} 

        imgIcon.sprite = image; 
    }

    public Sprite GetIcon() //Get Sprite from child object
    {
        return imgIcon.sprite;
    }

    public bool Dragging() //Is something being held?
    {
        if (imgIcon.sprite == null) {return false;}
        else{return true;}
    }
}