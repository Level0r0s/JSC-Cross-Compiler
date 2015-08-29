
// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150827
// can we have jsc c native link to a c file via assets?
// first lets do it manually.
// idl for c?

char* fooinvoke(void)
{
	// how can we call this from our code?
	return "hello from foo.c";
}

/// out:xmegaapi.exe
//foo.obj
//xmegaapi.exe.obj
//done
