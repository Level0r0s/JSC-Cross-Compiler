
// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150827
// can we have jsc c native link to a c file via assets?
// first lets do it manually.
// idl for c?

#ifndef __cplusplus
#error "is only for C++!"
#endif

// bar/bar.cpp(18) : error C2653: 'bar' : is not a class or namespace name
#include "bar.h"

extern "C" {
	// called by  Z:\jsc.svn\examples\c\synergy\xmegaapi\web\foo\foo.c

	char* abc_bar_barinvoke()
	{
		//return new abc::bar()->bar_barinvoke();
		return (new abc::bar())->bar_barinvoke();
		//return "program.cs -> foo.c -> bar.h -> bar.cpp extern C -> bar::bar_barinvoke";
	}
}

namespace abc
{
	// can we compile this can have it called from c?

	//char* bar::bar_barinvoke(void)

	// bar/bar.cpp(18) : error C2653: 'bar' : is not a class or namespace name

	char* bar::bar_barinvoke(void)
	{
		// how can we call this from our code?
		return "program.cs -> foo.c -> bar.h -> bar.cpp extern C -> bar::bar_barinvoke";
		//return "hello from bar.cpp";
		// can jsc do cpp yet?
	}

}

