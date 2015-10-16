using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestSSLConnectionLimit
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // it seems chrome is capable of flooding server with port requests.

        //        enter https
        //        { RemoteEndPoint = 192.168.1.199:64596, isPeerProxy = False }
        //{ certificate = , chain =  }
        //    enter https
        //    { RemoteEndPoint = 192.168.1.199:64597, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64598, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64599, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64600, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64601, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64602, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64603, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64604, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64605, isPeerProxy = False }
        //{ certificate = , chain =  }
        //enter https
        //{ RemoteEndPoint = 192.168.1.199:64606, isPeerProxy = False }


    }
}
