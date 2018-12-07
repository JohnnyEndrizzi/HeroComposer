using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MainMenu
{
    /* Functional Requirement 
     * ID: 8.2-1
     * Description: The player must be able to choose a level.
     * 
     * This class is used to persist information between scenes */
    public class ApplicationModel
    {
        static public string songPathName = "";
        //static public List<CharacterScriptObject> characters = new List<CharacterScriptObject>();
        static public CharacterScriptObject[] characters = new CharacterScriptObject[4];
    }
}
