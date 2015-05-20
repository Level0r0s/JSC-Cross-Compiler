package zjava.lang;

public abstract class Writer implements Appendable
{
	public Writer append(CharSequence	 c) { return this; }
	public Writer append(char c) { return this; }
}