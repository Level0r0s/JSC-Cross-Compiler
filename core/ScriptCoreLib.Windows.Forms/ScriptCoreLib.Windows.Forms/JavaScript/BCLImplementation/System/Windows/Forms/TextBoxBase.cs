﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;


    [Script(Implements = typeof(global::System.Windows.Forms.TextBoxBase))]
    internal class __TextBoxBase : __Control
    {
        public bool Multiline { get; set; }

        public IHTMLTextArea  HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public __TextBoxBase()
        {
            HTMLTarget = new IHTMLTextArea();

            this.Size = new global::System.Drawing.Size(100, 20);
            
        }

        public override string Text
        {
            get
            {
                return this.HTMLTarget.value;
            }
            set
            {
                this.HTMLTarget.value = value;
            }
        }

        #region
        static public implicit operator TextBoxBase(__TextBoxBase e)
        {
            return (TextBoxBase)(object)e;
        }

        static public implicit operator __TextBoxBase(TextBoxBase e)
        {
            return (__TextBoxBase)(object)e;
        }
        #endregion
    }
}
