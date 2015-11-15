using java.applet;

/*
namespace OutRun4KTemplate.Components
{*/

// todo: JSC should demote classes that use java top level classes.. 
public sealed class OutRun4KTemplate_Components_MyApplet1 : a
{
    // is java applets supported in 2016?
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151115/audio
    // http://stackoverflow.com/questions/24104046/is-it-possible-to-run-java-application-on-android-through-openjdk/33720451#33720451

    public const int DefaultWidth = 256 * 4;
    public const int DefaultHeight = 256 * 4;

    public string FooMethodX()
    {
        return this.FooMethod2011();
    }

    public override void init()
    {
        base.resize(DefaultWidth, DefaultHeight);
    }

}
/*
}
 * */
