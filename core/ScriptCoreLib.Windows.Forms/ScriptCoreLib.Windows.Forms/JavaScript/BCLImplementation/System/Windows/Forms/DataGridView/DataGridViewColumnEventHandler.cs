﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewColumnEventHandler))]
    internal delegate void __DataGridViewColumnEventHandler(object sender, DataGridViewColumnEventArgs e);
}
