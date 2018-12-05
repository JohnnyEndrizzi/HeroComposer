using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyOutput : MonoBehaviour {
    public Text txtOut;

    void Start () {
        txtOut.text = "Money\n" + "$" +  spliter(StoredValues.Cash);
        //Debug.Log(StoredValues.Cash);
    }

    void Update () {
        txtOut.text = "Money\n" + "$" +  spliter(StoredValues.Cash);
    }

    private string spliter(int bigNum){
        string output = "";
        string word = bigNum.ToString();
        char[] parsed = word.ToCharArray();

        for (int i = parsed.Length-1; i >= 0; i--) {

            if(i % 3 == 2){
                output += ",";
            }
            output += parsed[i];
        }

        return output;
    }
}