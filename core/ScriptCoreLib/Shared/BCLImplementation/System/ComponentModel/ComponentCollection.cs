using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    // https://github.com/dot42/api/blob/master/System/ComponentModel/ComponentCollection.cs

    [Script(Implements = typeof(global::System.ComponentModel.ComponentCollection))]
    internal class __ComponentCollection : __ReadOnlyCollectionBase
    {
        public readonly ArrayList InternalElements = new ArrayList();
    }
}
