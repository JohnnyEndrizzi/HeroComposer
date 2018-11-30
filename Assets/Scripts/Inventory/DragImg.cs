using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragImg : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private Image imgIcon = null;

    void Update() {
        int ScW = Screen.width;
        int ScH = Screen.height;

        Vector3 centreFixHalf = new Vector3(ScW/2, ScH/2, 0f);
        Vector3 centreFix = new Vector3(ScW, ScH, 0f);

         //Debug.Log("X: " +Input.mousePosition.x + " Y: " + Input.mousePosition.y);
//         Debug.Log("X: " +Screen.width.ToString() + " Y: " + Screen.height.ToString());
//         Debug.Log("X: " +centreFixHalf.x.ToString() + " Y: " + centreFixHalf.y.ToString());

        Vector3 temp = Input.mousePosition; //Mouse Follow

        //temp = Camera.main.ScreenToWorldPoint(temp);

        //temp = Camera.main.ScreenToViewportPoint(temp);
        //temp.x = temp.x * centreFix.x;
        //temp.y = temp.y * centreFix.y;



        //Maximise on Play???
        //temp.x = temp.x /2;
        //temp.y = temp.y /2;
        //


        temp = temp - centreFixHalf;
        temp.z = 0f;

        this.transform.localPosition = temp;

//        float debugX = Input.mousePosition.x - this.transform.localPosition.x;
//        float debugY = Input.mousePosition.y - this.transform.localPosition.y;
//
//        Debug.Log("X1: " + debugX.ToString() + " Y1: " + debugY.ToString());

//        Debug.Log("X1: " + Input.mousePosition.x.ToString() + " Y1: " + Input.mousePosition.y.ToString());
//        Debug.Log("X2: " + this.transform.localPosition.x.ToString() + " Y2: " + this.transform.localPosition.y.ToString());



    }

    public void SetIcon(Sprite image) { //Set Sprite
        if (image == null){imgIcon.GetComponent<Image>().enabled = false;}
        else{imgIcon.GetComponent<Image>().enabled = true;} 

        imgIcon.sprite = image; 
    }

    public Sprite GetIcon() { //Get Sprite
        return imgIcon.sprite;
    }

    public bool Dragging(){ //Is something being held?
        if (imgIcon.sprite == null) {return false;}
        else{return true;}
    }
}