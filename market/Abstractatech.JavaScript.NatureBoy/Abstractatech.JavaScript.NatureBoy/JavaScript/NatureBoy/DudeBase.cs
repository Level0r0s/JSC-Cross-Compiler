﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.Shared.Lambda;



namespace ScriptCoreLib.JavaScript.Controls.NatureBoy
{
    using div = IHTMLDiv;
    using img = IHTMLImage;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;
    using System;
    using ScriptCoreLib;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using Abstractatech.JavaScript.NatureBoy.HTML.Images.FromAssets;

    public abstract class DudeBase
    {
        public img Shadow;
        public img SelectionImage;
        public img HotImage;

        public Point<double> TargetLocation;

        public Point<double> CurrentLocation
        {
            get
            {
                return new Point<double> { X  = X, Y = Y };
            }
        }

        public double X { get; protected set; }
        public double Y { get; protected set; }

        public void TeleportToArc(double z, double a)
        {
            this.TeleportTo(
                this.X + System.Math.Cos(a) * z,
                this.Y + System.Math.Sin(a) * z
                );
        }





        public abstract void TeleportTo(double x, double y);

        public DudeBase()
        {
            Shadow = new _2();
            Shadow.style.position = IStyle.PositionEnum.absolute;

            SelectionImage = new green_ring();
            SelectionImage.style.position = IStyle.PositionEnum.absolute;

            HotImage = new  yellow_ring_50();
            HotImage.style.position = IStyle.PositionEnum.absolute;


            Control.style.position = IStyle.PositionEnum.absolute;
            Control.style.overflow = IStyle.OverflowEnum.hidden;

            this.IsSelected = false;

            Control.appendChild(Shadow, SelectionImage, HotImage);

            Control.onmouseover +=
                delegate
                {
                    this.IsHot = true;
                };

            Control.onmouseout +=
                delegate
                {
                    this.IsHot = false;
                };

            this.HotImage.style.display = IStyle.DisplayEnum.none;
        }


        private bool _HasShadow = true;

        public bool HasShadow
        {
            get { return _HasShadow; }
            set
            {
                _HasShadow = value;
                if (value)
                    this.Shadow.style.display = IStyle.DisplayEnum.block;
                else
                    this.Shadow.style.display = IStyle.DisplayEnum.none;
            }
        }


        private bool _IsHot;

        public bool IsHot
        {
            get { return _IsHot; }
            set
            {
                _IsHot = value;
                if (value)
                    this.HotImage.style.display = IStyle.DisplayEnum.block;
                else
                    this.HotImage.style.display = IStyle.DisplayEnum.none;
            }
        }

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                if (value)
                    this.SelectionImage.style.display = IStyle.DisplayEnum.block;
                else
                    this.SelectionImage.style.display = IStyle.DisplayEnum.none;
            }
        }

        public readonly div Control = new div();
    }

    public class FrameInfo
    {
        public string Source;

        img _Image;

        /// <summary>
        /// loads the image first time accessed
        /// </summary>
        public img Image
        {
            get
            {
                if (_Image == null)
                    _Image = Source;

                return _Image;
            }
        }

        public double Weight;

        public int OffsetX;
        public int OffsetY;
    }

    public class DudeAnimationInfo
    {
        public FrameInfo[] Frames_Stand;
        public FrameInfo[][] Frames_Walk;

        public int WalkAnimationInterval = 100;

        public IEnumerable<IHTMLImage> Images
        {
            get
            {
                return new[] 
                    {
                        Frames_Stand
                    }
                    .Concat(Frames_Walk)
                    .SelectMany(f => f)
                    .Select(f => f.Image);
            }
        }
    }

    public class Dude2 : DudeBase
    {
        public readonly DudeAnimationInfo AnimationInfo = new DudeAnimationInfo();

        public event Action DoneWalking;
        public event Action Walking;

        Timer _WalkingTimer;

        bool _Paused;

        public bool Paused
        {
            get
            {
                return _Paused;
            }
            set
            {
                _Paused = value;

                if (_WalkingTimer != null)
                {
                    _WalkingTimer.Enabled = !Paused;
                }
            }
        }

        public double TargetLocationDistanceMultiplier = 8;

        public double CurrentDistanceToTarget
        {
            get
            {
                return this.TargetLocation.GetDistance(this.CurrentLocation);
            }
        }

        public bool IsWalking
        {
            get
            {
                return !(_WalkingTimer == null);
            }
            set
            {
                if (AnimationInfo.Frames_Stand == null) throw new ArgumentNullException("Frames_Stand");
                if (AnimationInfo.Frames_Walk == null) throw new ArgumentNullException("Frames_Walk");

                if (value == IsWalking)
                    return;

                if (value)
                {
                    _WalkingTimer = AnimationInfo.Frames_Walk.Swap(AnimationInfo.WalkAnimationInterval,
                        f =>
                        {
                            this.Frames = f;
                            this.UpdateFrameImage();

                            if (this.TargetLocation == null)
                                return;

                            var z = CurrentDistanceToTarget;

                            //Console.WriteLine("range: " + z + " to/from " + this.TargetLocation + " - " + this.CurrentLocation);

                            if (z < this.CurrentWalkSpeed * TargetLocationDistanceMultiplier)
                            {
                                // we are there
                                this.IsWalking = false;

                                if (this.DoneWalking != null)
                                    this.DoneWalking();

                                return;
                            }

                            this.TeleportToArc(this.CurrentWalkSpeed, this.Direction);

                            if (this.Walking != null)
                                this.Walking();
                        }
                    );
                }
                else
                {
                    _WalkingTimer.Stop();
                    _WalkingTimer = null;

                    this.Frames = AnimationInfo.Frames_Stand;
                    this.UpdateFrameImage();
                }


            }
        }

        public FrameInfo[] Frames { get; set; }


        public img CurrentFrameImage;
        public img CurrentFrameBufferImage;

        public void LookDown()
        {
            Direction = Math.PI / 2;
        }

        #region Direction
        double _Direction;

        public double Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = value;
            }
        }
        #endregion


        #region ImageDirection
        double _ImageDirection;

        public double ImageDirection
        {
            get
            {
                return _ImageDirection;
            }
            set
            {
                _ImageDirection = value;

                this.UpdateFrameImage(CurrentFrame);
            }
        }
        #endregion
        public FrameInfo CurrentFrame
        {
            get
            {
                var value = this.ImageDirection;

                if (this.Frames == null)
                    throw new Exception("Frames");

                if (value == 0)
                {
                    return this.Frames[0];
                }


                var z = this.Frames[0];

                var p = value / (Math.PI * 2);

                foreach (var v in this.Frames)
                {
                    if (p < v.Weight)
                    {
                        //Console.WriteLine(v.Image.src + " - " + p + " - " + v.Weight);

                        z = v;

                        p = 1;
                    }
                    else
                    {
                        p -= v.Weight;
                    }
                }

                return z;
            }
        }

        public void LookAt(Point point)
        {

            LookAt(
                    new Point<double> { X = point.X, Y = point.Y }
                );
        }

        public void LookAt(Point<double> point)
        {
            this.TargetLocation = point;

            var a = this.TargetLocation.GetRotation(this.X, this.Y);

            this.Direction = a;

            if (CurrentTranslator == null)
            {
                this.ImageDirection = a;
            }
            else
            {
                // map coords
                var dual = new CoordTranslatorDual(CurrentTranslator);

                dual.From.OnMap = CurrentLocation;
                dual.To.OnMap = point;

                this.ImageDirection = dual.To.OnCanvas.GetRotation(dual.From.OnCanvas);
            }
        }




        public Action LookAtMouse(IHTMLElement e)
        {


            System.Action<IEvent> h =
                delegate(IEvent ev)
                {

                    try
                    {
                        this.LookAt(ev.CursorPosition.ToDouble());
                    }
                    catch
                    {

                    }

                };


            e.onmousemove += h;

            return
                () =>
                {
                    if (h == null)
                        return;

                    e.onmousemove -= h;

                    h = null;
                };
        }


        #region ZoomInfo
        public class ZoomInfo
        {
            public event Action Changed;

            public Func<double, double> DynamicZoomFunc;

            public double DynamicZoom { get; set; }

            double _StaticZoom;
            public double StaticZoom
            {
                get { return _StaticZoom; }
                set
                {
                    _StaticZoom = value.Max(0.1).Min(4.0);

                    if (this.Changed != null)
                        this.Changed();
                }
            }

            public double Value
            {
                get
                {

                    return (DynamicZoom * StaticZoom).Max(0.1).Min(4.0);
                }
            }

            public ZoomInfo()
            {
                this.DynamicZoom = 1;
                this.StaticZoom = 1;
            }
        }

        public readonly ZoomInfo Zoom = new ZoomInfo();


        #endregion

        public double RawWalkSpeed = 7;

        public double CurrentWalkSpeed
        {
            get
            {
                return this.RawWalkSpeed * this.Zoom.Value;
            }
        }

        public Dude2()
        {
            this.CurrentFrameImage = new img();
            this.CurrentFrameBufferImage = new img();

            this.Control.style.overflow = IStyle.OverflowEnum.visible;

            this.CurrentFrameImage.style.display = IStyle.DisplayEnum.none;
            this.CurrentFrameBufferImage.style.display = IStyle.DisplayEnum.none;

            this.Control.appendChild(this.CurrentFrameImage, this.CurrentFrameBufferImage);

            /*
            this.CurrentFrameImage.style.border = "1px solid blue";
            this.CurrentFrameBufferImage.style.border = "1px solid blue";
            this.Control.style.border = "1px solid yellow";
            this.Shadow.style.border = "1px solid red";
            */
            this.Zoom.Changed +=
                delegate
                {
                    UpdateFrameImage();
                    // UpdateSize();
                    TeleportTo(this.X, this.Y);

                };
        }


        public void UpdateFrameImage()
        {
            UpdateFrameImage(this.CurrentFrame);
        }

        public void UpdateFrameImage(FrameInfo frame)
        {
            if (frame == null)
            {

                return;
            }

            var e = frame.Image;

            var ix = System.Convert.ToInt32(e.width * this.Zoom.Value);
            var iy = System.Convert.ToInt32(e.height * this.Zoom.Value);

            var dx = System.Convert.ToInt32((Width - e.width - frame.OffsetX) * this.Zoom.Value);
            var dy = System.Convert.ToInt32((Height - e.height - frame.OffsetY) * this.Zoom.Value);

            var a64 = (this.Zoom.Value * 64).ToInt32();
            var a32 = a64 / 2;
            var a16 = a32 / 2;

            this.CurrentFrameBufferImage.src = e.src;
            this.CurrentFrameBufferImage.style.SetLocation(
                dx / 2,
                dy - a16,
                ix,
                iy
                );

            this.CurrentFrameImage.style.display = IStyle.DisplayEnum.none;
            this.CurrentFrameBufferImage.style.display = IStyle.DisplayEnum.block;

            var f = this.CurrentFrameImage;

            this.CurrentFrameImage = this.CurrentFrameBufferImage;
            this.CurrentFrameBufferImage = f;

        }

        public int Width;
        public int Height;

        public int ZoomedWidth { get { return System.Convert.ToInt32(Width * this.Zoom.Value); } }
        public int ZoomedHeight { get { return System.Convert.ToInt32(Height * this.Zoom.Value); } }

        public void SetSize(int x, int y)
        {
            this.Width = x;
            this.Height = y;

            //Console.WriteLine("size: " + zx + ", " + zy );

            UpdateSize();

        }

        private void UpdateSize()
        {
            var zx = ZoomedWidth;
            var zy = ZoomedHeight;

            var a64 = (this.Zoom.Value * 64).ToInt32();
            var a32 = a64 / 2;

            if (HasShadow)
                this.Shadow.style.SetLocation((zx - a64) / 2, zy - a32, a64, a32);

            this.HotImage.style.SetLocation((zx - a64) / 2, zy - a32, a64, a32);
            this.SelectionImage.style.SetLocation((zx - a64) / 2, zy - a32, a64, a32);

            this.Control.style.SetSize(zx, zy);
        }

        public Func<Point, bool> CanTeleportTo;


        public CoordTranslatorBase CurrentTranslator;

        public override void TeleportTo(double x, double y)
        {
            if (CanTeleportTo != null)
                if (CanTeleportTo(new Point ( x.ToInt32(), y.ToInt32() )))
                    return;

            var f = this.Zoom.DynamicZoomFunc;

            if (f != null)
                this.Zoom.DynamicZoom = f(y);


            this.X = x;
            this.Y = y;

            var a64 = (this.Zoom.Value * 64).ToInt32();
            var a32 = a64 / 2;

            if (CurrentTranslator == null)
            {
                // using canvas coords

                this.Control.style.SetLocation(
                    System.Convert.ToInt32(x - ZoomedWidth / 2),
                    System.Convert.ToInt32(y - ZoomedHeight + a32 / 2)//,
                    );


                this.Control.style.zIndex = System.Convert.ToInt32(y);
            }
            else
            {
                // we have the map coords, but we need to use the canvas coords now

                var coords = this.CurrentTranslator.ConvertMapToCanvas(
                    new Point<double> { X = this.X, Y = this.Y }
                );

                this.Control.style.SetLocation(
                          System.Convert.ToInt32(coords.X - ZoomedWidth / 2),
                          System.Convert.ToInt32(coords.Y - ZoomedHeight + a32 / 2)//,
                          );

                this.Control.style.zIndex = System.Convert.ToInt32(coords.Y);
            }

            this.UpdateSize();
        }

        public event Action DoneWalkingOnce
        {
            add
            {
                var h = default(Action);

                h = delegate
                {
                    this.DoneWalking -= h;

                    value();
                };

                this.DoneWalking += h;
            }
            remove
            {
            }
        }


        public event Action WalkingOnce
        {
            add
            {
                var h = default(Action);

                h = delegate
                {
                    this.Walking -= h;

                    value();
                };

                this.Walking += h;
            }
            remove
            {
            }
        }


        public void WalkToArc(double z, double a)
        {
            var x = this.X + System.Math.Cos(a) * z;
            var y = this.Y + System.Math.Sin(a) * z;

            WalkTo(new Point<double> { X = x, Y = y });
        }

        public void WalkTo(Point point)
        {
            WalkTo(
                new Point<double> { X = point.X, Y = point.Y }
            );
        }
        public void WalkTo(Point<double> point)
        {
            this.TargetLocation = point;
            this.LookAt(this.TargetLocation);
            this.IsWalking = true;


        }
    }

}
