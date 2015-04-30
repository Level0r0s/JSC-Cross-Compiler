using android.content;
using android.graphics;
using android.util;
using android.widget;
using android.view;
using android.view.animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace AndroidCardboardExperiment
namespace xandroidcardboardcxperiment.xactivities
{
    public class CardboardOverlayView : android.widget.LinearLayout
    {
        // https://github.com/googlesamples/cardboard-java/blob/master/CardboardSample/src/main/java/com/google/vrtoolkit/cardboard/samples/treasurehunt/CardboardOverlayView.java

        private CardboardOverlayEyeView leftView;
        private CardboardOverlayEyeView rightView;
        private AlphaAnimation textFadeAnimation;

        public CardboardOverlayView(Context context, AttributeSet attrs)
            : base(context, attrs)
        {
            setOrientation(HORIZONTAL);

            LayoutParams @params = new LayoutParams(
              LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT, 1.0f);
            @params.setMargins(0, 0, 0, 0);

            leftView = new CardboardOverlayEyeView(context, attrs);
            leftView.setLayoutParams(@params);
            addView(leftView);

            rightView = new CardboardOverlayEyeView(context, attrs);
            rightView.setLayoutParams(@params);
            addView(rightView);

            // Set some reasonable defaults.
            setDepthOffset(0.016f);
            setColor(Color.rgb(150, 255, 180));
            setVisibility(View.VISIBLE);

            textFadeAnimation = new AlphaAnimation(1.0f, 0.0f);
            textFadeAnimation.setDuration(5000);
        }

        public void show3DToast(String message)
        {
            setText(message);
            setTextAlpha(1f);
            textFadeAnimation.setAnimationListener(
                new EndAnimationListener
                {
                    vAnimationEnd = (Animation animation) =>
                    {
                        setTextAlpha(0f);
                    }
                }
            );
            startAnimation(textFadeAnimation);
        }

        private class EndAnimationListener : Animation.AnimationListener
        {
            public Action<Animation> vAnimationRepeat = delegate { };
            public void onAnimationRepeat(Animation animation) { vAnimationRepeat(animation); }
            public Action<Animation> vAnimationStart = delegate { };
            public void onAnimationStart(Animation animation) { vAnimationStart(animation); }

            public Action<Animation> vAnimationEnd = delegate { };
            public void onAnimationEnd(Animation animation) { vAnimationEnd(animation); }
        }

        private void setDepthOffset(float offset)
        {
            leftView.setOffset(offset);
            rightView.setOffset(-offset);
        }

        private void setText(String text)
        {
            leftView.setText(text);
            rightView.setText(text);
        }

        private void setTextAlpha(float alpha)
        {
            leftView.setTextViewAlpha(alpha);
            rightView.setTextViewAlpha(alpha);
        }

        private void setColor(int color)
        {
            leftView.setColor(color);
            rightView.setColor(color);
        }

        /**
         * A simple view group containing some horizontally centered text underneath a horizontally
         * centered image.
         *
         * <p>This is a helper class for CardboardOverlayView.
         */
        private class CardboardOverlayEyeView : ViewGroup
        {
            private ImageView imageView;
            private TextView textView;
            private float offset;

            public CardboardOverlayEyeView(Context context, AttributeSet attrs)
                : base(context, attrs)
            {
                imageView = new ImageView(context, attrs);
                imageView.setScaleType(ImageView.ScaleType.CENTER_INSIDE);
                imageView.setAdjustViewBounds(true);  // Preserve aspect ratio.
                addView(imageView);

                textView = new TextView(context, attrs);
                textView.setTextSize(TypedValue.COMPLEX_UNIT_DIP, 14.0f);
                textView.setTypeface(textView.getTypeface(), Typeface.BOLD);
                textView.setGravity(Gravity.CENTER);
                textView.setShadowLayer(3.0f, 0.0f, 0.0f, Color.DKGRAY);
                addView(textView);
            }

            public void setColor(int color)
            {
                imageView.setColorFilter(color);
                textView.setTextColor(color);
            }

            public void setText(String text)
            {
                textView.setText(text);
            }

            public void setTextViewAlpha(float alpha)
            {
                textView.setAlpha(alpha);
            }

            public void setOffset(float offset)
            {
                this.offset = offset;
            }

            // protected abstract void onLayout (boolean changed, int l, int t, int r, int b)
            //protected override void onLayout(bool changed, int left, int top, int right, int bottom)
            protected void onLayout(bool changed, int left, int top, int right, int bottom)
            {
                // Width and height of this ViewGroup.
                int width = right - left;
                int height = bottom - top;

                // The size of the image, given as a fraction of the dimension as a ViewGroup.
                // We multiply both width and heading with this number to compute the image's bounding
                // box. Inside the box, the image is the horizontally and vertically centered.
                float imageSize = 0.12f;

                // The fraction of this ViewGroup's height by which we shift the image off the
                // ViewGroup's center. Positive values shift downwards, negative values shift upwards.
                float verticalImageOffset = -0.07f;

                // Vertical position of the text, specified in fractions of this ViewGroup's height.
                float verticalTextPos = 0.52f;

                // Layout ImageView
                float imageMargin = (1.0f - imageSize) / 2.0f;
                float leftMargin = (int)(width * (imageMargin + offset));
                float topMargin = (int)(height * (imageMargin + verticalImageOffset));
                imageView.layout(
                  (int)leftMargin, (int)topMargin,
                  (int)(leftMargin + width * imageSize), (int)(topMargin + height * imageSize));

                // Layout TextView
                leftMargin = offset * width;
                topMargin = height * verticalTextPos;
                textView.layout(
                  (int)leftMargin, (int)topMargin,
                  (int)(leftMargin + width), (int)(topMargin + height * (1.0f - verticalTextPos)));
            }
        }
    }
}