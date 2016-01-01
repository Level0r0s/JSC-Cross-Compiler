using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.graphics;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/View.java
    // http://developer.android.com/reference/android/view/View.html
    [Script(IsNative = true)]
    public class View
    {
        public static readonly int SYSTEM_UI_FLAG_LAYOUT_STABLE;

        public const int SYSTEM_UI_FLAG_HIDE_NAVIGATION = 0x00000002;

        public const int SYSTEM_UI_FLAG_FULLSCREEN = 0x00000004;

        public const int SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION = 0x00000200;
        public const int SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN = 0x00000400;
        public const int SYSTEM_UI_FLAG_IMMERSIVE_STICKY = 0x00001000;

        // X:\jsc.svn\examples\java\android\AndroidLacasCameraServerActivity\AndroidLacasCameraServerActivity\ApplicationActivity.cs

        // X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRMyCubeWorld\ApplicationActivity.cs
        public void postInvalidate() { }

        // Z:\jsc.svn\examples\java\android\AndroidWebViewActivity\AndroidWebViewActivity\ApplicationActivity.cs
        public int getSystemUiVisibility()
        {
            throw null;
        }

        protected virtual void onDraw(Canvas canvas)
        {
        }

        public void setEnabled(bool enabled) { }
        public void setSystemUiVisibility(int visibility)
        {
        }

        public void forceLayout() { }
        public void requestLayout() { }

        public void invalidate() { }

        public void setPadding(int left, int top, int right, int bottom)
        {
        }

        public View getRootView()
        {
            return default(View);
        }


        public ViewParent getParent()
        {
            return default(ViewParent);
        }

        public virtual void setScrollBarStyle(int style)
        {
        }

        public virtual bool dispatchKeyEvent(KeyEvent @event)
        {
            throw null;

        }


        public virtual bool onTouchEvent(MotionEvent @event)
        {
            throw null;
        }

        // http://developer.android.com/reference/android/view/View.OnTouchListener.html
        [Script(IsNative = true)]
        public interface OnTouchListener
        {
            bool onTouch(View v, MotionEvent @event);
        }

        public void setOnTouchListener(View.OnTouchListener l)
        { }

        // members and types are to be extended by jsc at release build

        public View(Context c)
        {

        }

        public int getWidth()
        {
            throw null;
        }

        public int getHeight()
        {
            throw null;
        }

        public void setBackgroundColor(int color)
        { }


        public virtual ViewGroup.LayoutParams getLayoutParams()
        {
            throw null;
        }
        public virtual void setLayoutParams(ViewGroup.LayoutParams @params)
        { }

        public void setTag(Object tag)
        {
        }

        public Object getTag()
        {
            throw null;
        }

        [Script(IsNative = true)]
        public interface OnClickListener
        {
            void onClick(View v);
        }

        public void setOnClickListener(OnClickListener h)
        {

        }

        public void setClickable(bool clickable)
        {
        }


        public Context getContext()
        {
            throw null;
        }

        public int getMeasuredHeight() { throw null; }
        public int getMeasuredWidth() { throw null; }
        public void layout(int l, int t, int r, int b) { }

        public void measure(int widthMeasureSpec, int heightMeasureSpec)
        { }

        public void draw(Canvas canvas) { }



        public bool dispatchTouchEvent(MotionEvent e) { throw null; }
    }
}
