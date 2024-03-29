namespace TestCoClassForLayoutFSharp

    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open System
    open System.Linq
    open System.Xml.Linq

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() as me = 
        let this = me
        do ()

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        member this.WebMethod2(e : string, y : Action<string>) =
            // Send it back to the caller.
            do y.Invoke(e)
            ()

