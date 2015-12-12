using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XContainer.cs

    [Script(Implements = typeof(XContainer))]
    public abstract class __XContainer : __XNode
    {
        public __XName InternalElementName;

        public IXMLElement InternalElement
        {
            get
            {
                IXMLElement e = (IXMLElement)this.InternalValue;
                return e;
            }
        }





        #region Add
        public void Add(params object[] content)
        {
            foreach (var item in content)
            {
                this.Add(item);
            }
        }

        public void Add(object content)
        {
            // Z:\jsc.svn\examples\javascript\test\TestIEXElement\Application.cs

            //Console.WriteLine("enter XContainer Add " + new { content });

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/uploadvaluestaskasync
            InternalAdd(content);

            //Console.WriteLine("exit XContainer Add = " + this.ToString());

            //Console.WriteLine("exit XContainer Add = " + new { this.InternalValue.firstChild });
            //Console.WriteLine("exit XContainer Add = " + new { childNodes = this.InternalValue.childNodes.Length });


            var firstNode = this.Nodes().FirstOrDefault();

            //Console.WriteLine(new { firstNode });


        }

        private void InternalAdd(object content)
        {


            InternalValueInitialize();


            #region int
            // X:\jsc.svn\examples\javascript\LINQ\test\vb\TestXMLSelect\TestXMLSelect\Application.vb

            //   i = !(b instanceof wNCFl85B5DOD64VaExqsNg);
            //if (content is int)
            if (ScriptCoreLib.JavaScript.Runtime.Expando.IsNativeNumberObject(content))
            {
                Console.WriteLine("XContainer Add int");

                if (this.InternalValue == null)
                {
                    // web worker mode? do we need to store elements on our own?
                }
                else
                {
                    this.InternalValue.appendChild(
                        this.InternalValue.ownerDocument.createTextNode(Convert.ToString((int)content))
                    );
                }

                return;
            }
            #endregion


            #region string
            {
                // Z:\jsc.svn\examples\javascript\Test\TestObjectIsString\TestObjectIsString\Application.cs
                var xstring = content as string;

                if (xstring != null)
                {

                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                        Console.WriteLine("XContainer Add string InternalValue  null!");
                    }
                    else
                    {
                        //Console.WriteLine("XContainer Add string " + new { xstring });

                        var n = this.InternalValue.ownerDocument.createTextNode(xstring);

                        this.InternalValue.appendChild(
                            n
                        );
                    }

                    return;
                }
            }
            #endregion

            #region XText
            {
                var e = content as XText;

                if (e != null)
                {

                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                        Console.WriteLine("XContainer Add XText. InternalValue null!");
                    }
                    else
                    {
                        // no value?

                        //Console.WriteLine("XContainer Add XText. " + new { e.Value });

                        this.InternalValue.appendChild(
                            this.InternalValue.ownerDocument.createTextNode(e.Value)
                        );
                    }

                    return;
                }
            }
            #endregion


            #region XComment
            {
                var e = content as XComment;

                if (e != null)
                {
                    Console.WriteLine("XContainer Add XComment");

                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {

                        this.InternalValue.appendChild(
                            this.InternalValue.ownerDocument.createComment(e.Value)
                        );
                    }

                    return;
                }
            }
            #endregion


            #region XAttribute
            {
                var e = (__XAttribute)(object)(content as XAttribute);
                if (e != null)
                {
                    //Console.WriteLine("XContainer Add XAttribute");

                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                    }
                    else
                    {

                        var CurrentValue = e.Value;

                        e.InternalElement = this;
                        e.Value = CurrentValue;
                    }

                    return;
                }
            }
            #endregion


            #region XElement
            {
                var IncomingXElement = (__XElement)(object)(content as XElement);
                if (IncomingXElement != null)
                {


                    if (this.InternalValue == null)
                    {
                        // web worker mode? do we need to store elements on our own?
                        Console.WriteLine("XContainer Add XElement InternalValue null");
                    }
                    else
                    {
                        // X:\jsc.svn\examples\javascript\Test\TestAttachXElementToDocument\TestAttachXElementToDocument\Application.cs

                        if (IncomingXElement.InternalValue == null)
                        {
                            Console.WriteLine("XContainer Add XElement IncomingXElement InternalValue null");

                            IncomingXElement.InternalValue = this.InternalValue.ownerDocument.createElement(
                                IncomingXElement.InternalElementName.LocalName
                            );

                            // missing content?
                        }

                        //Console.WriteLine("XContainer Add XElement " + new { IncomingXElement });

                        // http://stackoverflow.com/questions/1811116/ie-support-for-dom-importnode
                        // The solution to all of my problems was to not use a DOM method after all, and instead use my own implementation. Here, in all of its glory, is my final solution to the importNode() problem coded in a cross-browser compliant way: (Line wraps marked » —Ed.)

                        //__adoptNode(NewXElement);

                        __XContainer.InternalRebuildDocument(this, IncomingXElement);


                        //ie will complain. why?
                        // does it expect adobtion?

                        this.InternalValue.appendChild(IncomingXElement.InternalValue);
                    }

                    return;
                }
            }
            #endregion

            Console.WriteLine("XContainer Add fault?");

            // what is it?
            throw new NotImplementedException();
        }


        public override void InternalValueInitialize()
        {
            // bugcheck
            //Console.WriteLine("XContainer InternalValueInitialize");

            if (this.InternalValue == null)
            {
                if (Native.window == null)
                {

                    // what if we are running in a web worker?
                    // then we dont have the DOM xml available!
                    // tested by
                    // X:\jsc.svn\examples\javascript\Test\TestSolutionBuilder\TestSolutionBuilderV1\Application.cs
                }
                else
                {



                    var doc = new IXMLDocument(this.InternalElementName.LocalName);


                    this.InternalValue = doc.documentElement;
                }
            }
        }


        #endregion



        public IEnumerable<XNode> Nodes()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/uploadvaluestaskasync

            return this.InternalElement.childNodes.Select(
                item =>
                {
                    //Console.WriteLine(new { item });

                    if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
                        return (XNode)(object)new __XElement(null, null) { InternalValue = item };

                    if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.TextNode)
                    {
                        var InternalStringValue = ((ScriptCoreLib.JavaScript.DOM.ITextNode)this.InternalValue).text;
                        //var InternalStringValue2 = ((ScriptCoreLib.JavaScript.DOM.ITextNode)this.InternalValue).nodeValue;

                        //Console.WriteLine("XContainer Nodes __XText " + new { InternalStringValue, InternalStringValue2 });
                        //Console.WriteLine("XContainer Nodes __XText " + new { InternalStringValue });

                        return (XNode)(object)new __XText { InternalValue = item, InternalStringValue = InternalStringValue };
                    }

                    if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.CommentNode)
                        return (XNode)(object)new __XComment(null) { InternalValue = item };

                    // what is it?

                    throw new NotImplementedException();
                }
            ).Where(k => k != null);
        }

        #region Elements
        public XElement Element(XName name)
        {
            return Elements(name).FirstOrDefault();
        }

        public IEnumerable<XElement> Elements(XName name)
        {
            // X:\jsc.svn\examples\javascript\Test\TestXElementAdd\TestXElementAdd\Application.cs

            //Console.WriteLine("Elements " + new { name });

            return this.Elements().Where(
                k =>
                {
                    //Console.WriteLine("Elements " + new { name, k.Name.LocalName });

                    return k.Name.LocalName == name.LocalName;
                }
            );
        }

        public IEnumerable<XElement> Elements()
        {
            var e = InternalElement;
            var a = new List<XElement>();

            foreach (var item in e.childNodes)
            {
                if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
                {
                    var x = new __XElement(null, null) { InternalValue = item };

                    a.Add(x);
                }

            }

            return a;

        }
        #endregion

        public void RemoveNodes()
        {
            if (this.InternalElement == null)
                return;

            var p = this.InternalElement.firstChild;

            while (p != null)
            {
                this.InternalElement.removeChild(p);

                p = this.InternalElement.firstChild;
            }
        }

        public IEnumerable<XElement> Descendants()
        {
            // X:\jsc.svn\examples\javascript\svg\DEAGELForecast\DEAGELForecast\Application.cs
            return this.Elements().SelectMany(k => k.DescendantsAndSelf());


        }

    }
}
