﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let c = (
                 from z in new xTable()
                     //where z.field1 == x.field1
                 orderby z.field1, z.field2, z.field3 descending

                 select z
             )

            let cc = c.FirstOrDefault()

            select cc

        ).FirstOrDefault();

    }
}