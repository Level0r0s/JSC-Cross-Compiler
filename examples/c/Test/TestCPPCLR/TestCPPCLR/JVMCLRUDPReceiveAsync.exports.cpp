//#include "stdafx.h"


public ref class _AppDomainAssemblyResolve_ abstract sealed
{
	static bool InitializeDone;
	
	public: static System::Reflection::Assembly^ __AssemblyResolve(System::Object^ _s, System::ResolveEventArgs^ _a)
	{
		
		System::Reflection::Assembly^ ex = System::Reflection::Assembly::GetExecutingAssembly();
		System::IO::FileInfo^ exf = gcnew System::IO::FileInfo(ex->Location);
		
            System::String^ Directory = exf->Directory->FullName;
            System::Reflection::AssemblyName^ _an = gcnew System::Reflection::AssemblyName(_a->Name);
            System::String^ AssemblyName = _an->Name;

            System::String^ dll = System::IO::Path::Combine(Directory, AssemblyName + ".dll");


            if (System::IO::File::Exists(dll))
                return System::Reflection::Assembly::LoadFrom(dll);

            System::String^ exe = System::IO::Path::Combine(Directory, AssemblyName + ".exe");


            if (System::IO::File::Exists(exe))
                return System::Reflection::Assembly::LoadFrom(exe);

     
            return nullptr;
	}
	
	public: static void Initialize()
	{
		if (InitializeDone)
			return;
		
		InitializeDone = true;
		
		 System::AppDomain::CurrentDomain->AssemblyResolve += gcnew System::ResolveEventHandler(&__AssemblyResolve);
	}
};



public ref class _ImportDirectoryBridge_ abstract sealed
{


  public: static void export___free_0200001e(System::IntPtr p0)
  {
    _ExportDirectoryBridge_::export___free_0200001e(p0);
  }



  public: static void export___0600007f(System::IntPtr p0)
  {
    _ExportDirectoryBridge_::export___0600007f(p0);
  }



  public: static void export___06000080(System::String^ p0)
  {
    _ExportDirectoryBridge_::export___06000080(p0);
  }



  public: static void export___06000081()
  {
    _ExportDirectoryBridge_::export___06000081();
  }



};


extern "C"
{
  __declspec(dllexport) void __cdecl export___free_0200001e(void* p0)
  {
//   try
//   {
//    System::Diagnostics::Debugger::Launch();
//    System::Diagnostics::Debugger::Break();

    _AppDomainAssemblyResolve_::Initialize();

    _ImportDirectoryBridge_::export___free_0200001e(* gcnew System::IntPtr(p0));

//	}
//	catch(System::Exception^ e)
//	{
//		System::Console::WriteLine("An error occurred.");
//		System::Console::WriteLine(e->Message); // Print the error message.
//		System::Console::WriteLine(e->StackTrace); //String that contains the stack trace for this exception.
//	}
  }
}


extern "C"
{
  __declspec(dllexport) void __cdecl export___0600007f(void* p0)
  {
//   try
//   {
//    System::Diagnostics::Debugger::Launch();
//    System::Diagnostics::Debugger::Break();

    _AppDomainAssemblyResolve_::Initialize();

    _ImportDirectoryBridge_::export___0600007f(* gcnew System::IntPtr(p0));

//	}
//	catch(System::Exception^ e)
//	{
//		System::Console::WriteLine("An error occurred.");
//		System::Console::WriteLine(e->Message); // Print the error message.
//		System::Console::WriteLine(e->StackTrace); //String that contains the stack trace for this exception.
//	}
  }
}


extern "C"
{
  __declspec(dllexport) void __cdecl export___06000080(void* p0)
  {
//   try
//   {
//    System::Diagnostics::Debugger::Launch();
//    System::Diagnostics::Debugger::Break();

    _AppDomainAssemblyResolve_::Initialize();

    _ImportDirectoryBridge_::export___06000080(System::Runtime::InteropServices::Marshal::PtrToStringAnsi(* gcnew System::IntPtr(p0)));

//	}
//	catch(System::Exception^ e)
//	{
//		System::Console::WriteLine("An error occurred.");
//		System::Console::WriteLine(e->Message); // Print the error message.
//		System::Console::WriteLine(e->StackTrace); //String that contains the stack trace for this exception.
//	}
  }
}


extern "C"
{
  __declspec(dllexport) void __cdecl export___06000081()
  {
//   try
//   {
//    System::Diagnostics::Debugger::Launch();
//    System::Diagnostics::Debugger::Break();

    _AppDomainAssemblyResolve_::Initialize();

    _ImportDirectoryBridge_::export___06000081();

//	}
//	catch(System::Exception^ e)
//	{
//		System::Console::WriteLine("An error occurred.");
//		System::Console::WriteLine(e->Message); // Print the error message.
//		System::Console::WriteLine(e->StackTrace); //String that contains the stack trace for this exception.
//	}
  }
}

