using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Input
    {
        public static Hashtable keys = new Hashtable();

        public static void ChangeState(Keys key, bool state)
        {
            keys[key] = state;
        }

        public static bool Pressed(Keys key)
        {
            if (keys[key] == null)
                return false;
            if ((bool)keys[key])
            {
                keys[key] = false;
                return true;
            }
            else
                return false;
        }
    }
}
