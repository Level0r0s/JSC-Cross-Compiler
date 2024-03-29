using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace gameclient.source.shared
{
    using Serializable = System.SerializableAttribute;

    public partial class Message
    {


        #region CreateExplosionByServer
        [Script, Serializable]
        public class _CreateExplosionByServer
        {
            public int x;
            public int y;
            public string text;

        }

        partial class ServerToClient
        {
            public void CreateExplosionByServer(int x, int y, string text, System.Action done)
            {
                var CreateExplosionByServer = new _CreateExplosionByServer { x = x, y = y, text = text};
                var m = new Message { CreateExplosionByServer = CreateExplosionByServer };
                this.Send(m, h => done());
            }
        }

        public _CreateExplosionByServer CreateExplosionByServer;
        #endregion


        #region IClient_DisplayNotification
        [Script, Serializable]
        public class _IClient_DisplayNotification
        {
            public string text;
            public int color;
        }

        partial class ServerToClient
        {
            public void IClient_DisplayNotification(string text, int color)
            {
                var IClient_DisplayNotification = new _IClient_DisplayNotification { text = text, color = color };
                var m = new Message { IClient_DisplayNotification = IClient_DisplayNotification };

                this.Send(m, null);
            }
        }

        public _IClient_DisplayNotification IClient_DisplayNotification;
        #endregion



        #region ForceReload
        [Script, Serializable]
        public class _ForceReload
        {
           
        }

        partial class ServerToClient
        {
            public void ForceReload()
            {
                var ForceReload = new _ForceReload {  };
                var m = new Message { ForceReload = ForceReload };

                this.Send(m, null);
            }
        }

        public _ForceReload ForceReload;
        #endregion

        #region IClient_DrawRectangle
        [Script, Serializable]
        public class _IClient_DrawRectangle
        {
            public RectangleInfo rect;
            public int color;
        }

        partial class ServerToClient
        {
            public void IClient_DrawRectangle(RectangleInfo rect, int color)
            {
                var IClient_DrawRectangle = new _IClient_DrawRectangle { rect = rect, color = color };
                var m = new Message { IClient_DrawRectangle = IClient_DrawRectangle };

                this.Send(m, null);
            }
        }

        public _IClient_DrawRectangle IClient_DrawRectangle;
        #endregion

    
        #region IClient_SpawnHarvester
        [Script, Serializable]
        public class _IClient_SpawnHarvester
        {
            public Point Location;
            public int Direction;
        }

        partial class ServerToClient
        {
            public void IClient_SpawnHarvester(Point Location, int Direction)
            {
                var IClient_SpawnHarvester = new _IClient_SpawnHarvester { Location = Location, Direction = Direction };
                var m = new Message { IClient_SpawnHarvester = IClient_SpawnHarvester };

                this.Send(m, null);
            }
        }

        public _IClient_SpawnHarvester IClient_SpawnHarvester;
        #endregion
    }
}