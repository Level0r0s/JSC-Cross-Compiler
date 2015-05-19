using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPrintWriter
{
    // http://docs.oracle.com/javase/7/docs/api/java/io/PrintWriter.html
    public class PrintWriter : Writer
    {
    }

    // http://docs.oracle.com/javase/7/docs/api/java/io/Writer.html
    public abstract class Writer : Appendable
    {
        public Writer append(char c)
        {
            throw new NotImplementedException();
        }

        // X:\jsc.svn\examples\java\android\Test\TestPrintWriter\TestPrintWriter\References\PrintWriter.java:3: error: PrintWriter is not abstract and does not override abstract method append(CharSequence) in Appendable

        Appendable Appendable.append(char c)
        {
            throw new NotImplementedException();
        }
    }

    // http://docs.oracle.com/javase/7/docs/api/java/lang/Appendable.html
    public interface Appendable
    {
        Appendable append(char c);
    }
}
