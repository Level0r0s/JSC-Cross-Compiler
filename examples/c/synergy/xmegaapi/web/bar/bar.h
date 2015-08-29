// h file is needed to link external c to this c/obj code..
// Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\staging\jni\Oculus360PhotosSDK\Oculus360Photos.h



// can we do some c++ and jsc?


#ifndef __cplusplus
// #error "is only for C++! remove /TC parameter or implement C++ to C exports!"
// http://www.c4learn.com/c-programming/c-pragma-startup-and-pragma-exit-directive/

	char* abc_bar_barinvoke();



#else

extern "C" {
	char* abc_bar_barinvoke();
}


namespace abc
{


	class bar
	{
	public:
		char* bar_barinvoke(void);

		// just like idl. need terminate scope..
	};

}
//bar::bar_barinvoke

#endif
