using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.swing;
using System.Windows.Forms;
using java.awt.@event;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    internal class __Form : __ContainerControl
    {
        // see: http://java.sun.com/docs/books/tutorial/uiswing/components/frame.html
        // see: http://java.sun.com/docs/books/tutorial/uiswing/events/windowlistener.html
        // see: http://www.dreamincode.net/forums/showtopic66100.htm
        // see: http://dev.eclipse.org/newslists/news.eclipse.tools.ve/msg00053.html
        // see: http://inversionconsulting.blogspot.com/2008/03/java-jdialog-and-jprogressbar-example.html
        // see: http://www.experts-exchange.com/Programming/Languages/Java/Q_21108794.html

        public JFrame InternalElement;
        //public JDialog InternalElement;

        public event FormClosedEventHandler FormClosed;
        public event FormClosingEventHandler FormClosing;

        public override java.awt.Component InternalGetElement()
        {
            return InternalElement;
        }

        public __Form()
        {
            this.InternalElement = new JFrame();
            //this.InternalElement = new JDialog();
            this.InternalElement.setSize(300, 300);

            this.InternalElement.getContentPane().setLayout(null);

            // fixme: jsc should make delegate methods public!
            // java cannot call them otherwise

            this.InternalElement.addWindowListener(
                new __WindowListener
                {
                    Closed = RaiseFormClosed,
                    Closing = RaiseFormClosing,
                }

            );
        }

        #region Close
        public void Close()
        {
            // http://mycodepage.blogspot.com/2006/09/how-to-close-swing-jframe-without.html
            // http://mindprod.com/jgloss/close.html

            this.InternalElement.dispose();
        }

        public void RaiseFormClosed(WindowEvent e)
        {
            if (this.FormClosed != null)
                this.FormClosed(this, new FormClosedEventArgs(CloseReason.None));

        }

        public void RaiseFormClosing(WindowEvent e)
        {
            var args = new FormClosingEventArgs(CloseReason.None, false);

            if (this.FormClosing != null)
                this.FormClosing(this, args);

            if (args.Cancel)
                return;

            // If the program does not explicitly hide or dispose the window while 
            // processing this event, the window close operation will be cancelled.


            this.Dispose();
        }


        [Script]
        public delegate void __WindowListenerDelegate(WindowEvent e);

        [Script]
        public class __WindowListener : WindowListener
        {
            public __WindowListenerDelegate Closed;
            public __WindowListenerDelegate Closing;

            #region WindowListener Members

            public void windowActivated(WindowEvent e)
            {
            }

            public void windowClosed(WindowEvent e)
            {
                if (Closed != null)
                    Closed(e);
            }

            public void windowClosing(WindowEvent e)
            {
                if (Closing != null)
                    Closing(e);
            }

            public void windowDeactivated(WindowEvent e)
            {
            }

            public void windowDeiconified(WindowEvent e)
            {
            }

            public void windowIconified(WindowEvent e)
            {
            }

            public void windowOpened(WindowEvent e)
            {
            }

            #endregion
        }
        #endregion

        public override string Text
        {
            get
            {
                return InternalElement.getTitle();
            }
            set
            {
                InternalElement.setTitle(value);
            }
        }


        // protected internal virtual void Dispose(bool e)
        // Error	4	'ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__Form.Dispose(bool)': cannot change access modifiers when overriding 'protected' inherited member 'ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel.__Component.Dispose(bool)'	X:\jsc.svn\core\ScriptCoreLibJava.Windows.Forms\ScriptCoreLibJava.Windows.Forms\BCLImplementation\System\Windows\Forms\Form.cs	149	36	ScriptCoreLibJava.Windows.Forms
        // tested by
        // X:\jsc.svn\examples\java\forms\AppletAsyncWhenReady\AppletAsyncWhenReady\ApplicationApplet.cs
        public override void Dispose(bool e)
        {
            this.InternalElement.dispose();
        }


        public override void InternalShow()
        {
            Console.WriteLine("InternalShow...");
            this.InternalElement.setLocationRelativeTo(null);
            this.InternalElement.show(true);
            this.InternalElement.toFront();
            Console.WriteLine("InternalShow... done");
        }


        public DialogResult ShowDialog()
        {
            // Z:\jsc.svn\examples\java\hybrid\forms\FormsUbuntuHello\FormsUbuntuHello\Program.cs

            var c = new TaskCompletionSource<DialogResult>();

            this.FormClosed += delegate
            {
                c.SetResult(DialogResult.OK);
            };

            this.Show();

            c.Task.Wait();

            return c.Task.Result;
        }
    }
}

//script: error JSC1000: Java : unable to emit br.s at 'ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__Control.InternalChildrenAnchorUpdate'#0054: Java : invalid br opcode at
// assembly: C:\util\jsc\bin\ScriptCoreLibJava.Windows.Forms.dll
// type: ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__Control, ScriptCoreLibJava.Windows.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0054