
// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150827
// can we have jsc c native link to a c file via assets?
// first lets do it manually.
// idl for c?

// foo/foo.c(8) : fatal error C1083: Cannot open include file: 'bar/bar.h': No such file or directory
// is that header aware we want to call it from C ?
//#include "bar/bar.h"
#include "bar.h"

//foo.c
//bar\bar.h(10) : fatal error C1189 : #error :  "is only for C++! remove /TC parameter or implement C++ to C exports!"

char* fooinvoke(void)
{
	//foo.obj : error LNK2019 : unresolved external symbol abc_bar_barinvoke referenced in function fooinvoke
	//	xmegaapi.exe : fatal error LNK1120 : 1 unresolved externals
	// this means the implementation wasnt there..

	return abc_bar_barinvoke();

	// how can we call this from our code?
	//return "hello from foo.c";
}

/// out:xmegaapi.exe
//foo.obj
//xmegaapi.exe.obj
//done
