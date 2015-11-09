using ScriptCoreLib.Query.Experimental;

class Program
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151109/contains
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            //  where Contains()
            where x.Tag.ToLower().Contains("xx")

            select x.field3
        ).Average();

    }
}
