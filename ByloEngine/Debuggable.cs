using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

using ByloEngine.Input;

namespace ByloEngine
{
    public abstract class Debuggable
    {
        private Keys debugKey;

        public bool IsDebugging
        {
            get;
            private set;
        }

        public Debuggable(KeyboardManager keyboardManager, Keys selectedDebugKey)
        {
            debugKey = selectedDebugKey;
            IsDebugging = false;

            keyboardManager.KeyPressed += new KeyEventHandler(changeDebugState);
        }

        private void changeDebugState(Keys eventKey)
        {
            if (eventKey == debugKey)
            {
                IsDebugging = !IsDebugging;
            }
        }
    }
}
