using UnityEngine;
using UnityEngine.UI;

public class MoneyOutput : MonoBehaviour {
    //Controls Output of Money values

    public Text txtOut;

    void Start () {
        txtOut.text = "Money\n" + "$" +  spliter(StoredValues.Cash);
    }

    void Update () {
        txtOut.text = "Money\n" + "$" +  spliter(StoredValues.Cash);
    }

    private string spliter(int bigNum) //Adds , ev,ery, 3 ,dig,its
    {
        string output = "";
        string word = bigNum.ToString();
        char[] parsed = word.ToCharArray();

        for (int i = parsed.Length-1; i >= 0; i--) {

            if(i % 3 == 2 && i!= parsed.Length - 1)
            {
                output += ",";
            }
            output += parsed[parsed.Length - 1 - i];
        }
        return output;
    }
}