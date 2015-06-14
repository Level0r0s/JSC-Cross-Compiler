using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:
    Script,
    ScriptTypeFilter(ScriptType.C, typeof(TestStructByRefInvoke.Program)),
]

namespace TestStructByRefInvoke
{
    //#pragma pack(8)
    //// TestStructByRefInvoke.foo
    //typedef struct tag_TestStructByRefInvoke_foo
    //{
    //    int field32;
    //} TestStructByRefInvoke_foo, *LPTestStructByRefInvoke_foo;
    //#pragma pack()
    //#define __new_TestStructByRefInvoke_foo(count) \
    //    (LPTestStructByRefInvoke_foo) malloc(sizeof(TestStructByRefInvoke_foo) * count)



    [Script]
    struct foo
    {
        public int field32;

        //// instance TestStructByRefInvoke.foo.Increment
        //void TestStructByRefInvoke_foo_Increment(TestStructByRefInvoke_foo __that)
        //{
        //    __that.field32 = (__that.field32 + 1);
        //}

        public void Increment(ref foo shadow)
        {

            this.field32++;

            shadow.field32 = this.field32;
        }

        public void Print()
        {

            Console.Write("field32: ");
            Console.Write(this.field32);
            Console.WriteLine();
        }
    }

    [Script]
    class Program : ScriptCoreLibNative.IAssemblyReferenceToken
    {
        static void Main(string[] args)
        {
            var stackalloc2 = default(foo);
            var stackalloc1 = default(foo);

            stackalloc1.field32 = 40;

            //TestStructByRefInvoke_foo_Increment((TestStructByRefInvoke_foo)foo0);
            stackalloc1.Increment(ref stackalloc2);
            stackalloc1.Print();
            stackalloc1.Increment(ref stackalloc2);
            stackalloc1.Print();

            //field32: 41
            //field32: 42



        }
    }
}

//TestStructByRefInvoke.exe.c(1084) : error C2440: 'type cast' : cannot convert from 'TestStructByRefInvoke_foo' to 'TestStructByRefInvoke_foo'
//TestStructByRefInvoke.exe.c(1084) : error C2198: 'TestStructByRefInvoke_foo_Increment' : too few arguments for call