using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MainMenu
{
    /* This class is used to persist information between scenes */
    public class ApplicationModel
    {
        static public string songPathName = "";
        //static public List<CharacterScriptObject> characters = new List<CharacterScriptObject>();
        static public CharacterScriptObject[] characters = new CharacterScriptObject[4];
    }
}
