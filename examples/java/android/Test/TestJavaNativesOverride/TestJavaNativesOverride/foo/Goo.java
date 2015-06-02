package foo;
public class Goo extends  Bar
{
    public String GetString()
	{
		// CLR overrides/reimplements base?
		//return this.field1;

		return this.GetBarString();
	}
}