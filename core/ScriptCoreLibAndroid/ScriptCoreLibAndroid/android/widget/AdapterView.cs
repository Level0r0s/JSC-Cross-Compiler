using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib;

namespace android.widget
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/widget/AdapterView.java
    // http://developer.android.com/reference/android/widget/AdapterView.html
    [Script(IsNative = true)]
    public abstract class AdapterView
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150429
        // base type clash?
        // <T> 
        : ViewGroup
    {
        // members and types are to be extended by jsc at release build

        public AdapterView(Context c)
            : base(c)
        {

        }

        public object getSelectedItem() { return null; }

        // SpinnerAdapter
        //public abstract void setAdapter(T adapter);
        //public virtual void setAdapter(SpinnerAdapter adapter) { }
        public virtual void setAdapter(object adapter) { }
    }
}
