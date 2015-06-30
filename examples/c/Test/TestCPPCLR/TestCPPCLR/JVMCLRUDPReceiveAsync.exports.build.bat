
@@echo off

set _toolkit=X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC
set _init=%_toolkit%\..\Common7\Tools\vsvars32.bat

echo call "%_init%"
call "%_init%"  

rem mkdir bin

rem Cannot open compiler generated file: 'bin\JVMCLRUDPReceiveAsync.exports.obj': No such file or directory
rem X:\jsc.svn\examples\c\Test\TestCPPCLR\TestCPPCLR\JVMCLRUDPReceiveAsync.exports.cpp : fatal error C1083: Cannot open compiler generated file: 'bin\JVMCLRUDPReceiveAsync.exports.obj': No such file or directory
 call "%_toolkit%\bin\cl.exe" /c /AI"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0" /AI.\bin /Zi /clr /nologo /W3 /WX- /O2 /Oy- /D WIN32 /D NDEBUG /D _WINDLL /D _UNICODE /D UNICODE /EHa /MD /GS /fp:precise /Zc:wchar_t /Zc:forScope /Fo"bin\\" /TP /FU"References\JVMCLRUDPReceiveAsync.exe" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Core.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Data.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Xml.dll" /analyze- /FC /errorReport:prompt /clr:nostdlib  "JVMCLRUDPReceiveAsync.exports.cpp"


rem /Yu"stdafx.h" /GS /analyze- /W3 /Zc:wchar_t /Zi /Od /Fd"Debug\vc140.pdb" /Zc:inline /fp:precise /D "WIN32" /D "_DEBUG" /D "_UNICODE" /D "UNICODE" /errorReport:prompt /WX- /Zc:forScope /Oy- /clr 
rem /FU"X:\jsc.svn\examples\c\Test\TestCPPCLR\TestCPPCLR\References\JVMCLRUDPReceiveAsync.exe" 
rem /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\mscorlib.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.Data.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.dll" /FU"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\System.Xml.dll"
rem /MDd /Fa"Debug\" /EHa /nologo /Fo"Debug\" /Fp"Debug\TestCPPCLR.pch" 

rem call "%_toolkit%\bin\link.exe"  /DLL /LTCG /CLRIMAGETYPE:IJW /LIBPATH:"X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\lib" /OUT:bin\JVMCLRUDPReceiveAsync.exports bin\JVMCLRUDPReceiveAsync.exports.obj bin\*.obj
rem call "%_toolkit%\bin\link.exe"  /DLL  /MACHINE:X86   /LTCG /CLRIMAGETYPE:IJW  /OUT:bin\JVMCLRUDPReceiveAsync.exports "/LIBPATH:C:\Program Files (x86)\Windows Kits\NETFXSDK\4.6\Lib\um\x86\" bin\JVMCLRUDPReceiveAsync.exports.obj
rem echo call "%_toolkit%\bin\link.exe" /LIBPATH:"X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\lib\"   /DLL  /MACHINE:X86   /LTCG /CLRIMAGETYPE:IJW  /OUT:bin\JVMCLRUDPReceiveAsync.exports bin\JVMCLRUDPReceiveAsync.exports.obj

rem X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\LIB;X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\ATLMFC\LIB;C:\Program Files (x86)\Windows Kits\10\\lib\10.0.10056.0\ucrt\x86;C:\Program Files (x86)\Windows Kits\8.1\lib\winv6.3\um\x86;
echo %LIB%
set LIB=%LIB%;C:\Program Files (x86)\Windows Kits\NETFXSDK\4.6\Lib\um\x86

:: https://msdn.microsoft.com/en-us/library/6y6t9esh.aspx

:: LINK : warning LNK4098: defaultlib 'LIBCMT' conflicts with use of other libs; use /NODEFAULTLIB:library
 call "%_toolkit%\bin\link.exe" /DLL  /MACHINE:X86   /LTCG /CLRIMAGETYPE:IJW   /OUT:bin\JVMCLRUDPReceiveAsync.exports bin\JVMCLRUDPReceiveAsync.exports.obj References\lib_jnistb10.obj

:: http://ss64.com/nt/syntax-esc.html
rem http://stackoverflow.com/questions/10481193/errors-when-compiling-library-in-command-line

rem LINK : fatal error LNK1181: cannot open input file 'Files.obj'
rem LINK : fatal error LNK1104: cannot open file 'MSVCRT.lib'
rem ren 1>      Searching X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\lib\MSVCRTD.lib:

rem LINK : fatal error LNK1104: cannot open file 'MSCOREE.lib'
rem call "%_toolkit%\bin\link.exe" "/OUT:X:\jsc.svn\examples\c\Test\TestCPPCLR\Debug\TestCPPCLR.dll" /VERBOSE  /LTCG /SUBSYSTEM:CONSOLE   /DYNAMICBASE /FIXED:NO /NXCOMPAT /MACHINE:X86 /DLL Debug\JVMCLRUDPReceiveAsync.exports.obj


rem "/OUT:X:\jsc.svn\examples\c\Test\TestCPPCLR\Debug\TestCPPCLR.dll" /VERBOSE /INCREMENTAL /LTCG:STATUS /MANIFEST:NO /ASSEMBLYDEBUG:DISABLE 
rem /SUBSYSTEM:CONSOLE /TLBID:1 /DYNAMICBASE /FIXED:NO /NXCOMPAT /MACHINE:X86 /DLL Debug\JVMCLRUDPReceiveAsync.exports.obj



rem LINK : fatal error LNK1104: cannot open file 'X:\jsc.svn\examples\c\Test\TestCPPCLR\Debug\TestCPPCLR.dll /VERBOSE /INCREMENTAL /LTCG:STATUS /MANIFEST:NO /ASSEMBLYDEBUG:DISABLE  /SUBSYSTEM:CONSOLE /TLBID:1 /DYNAMICBASE /FIXED:NO /NXCOMPAT /MACHINE:X86 /DLL Debug\JVMCLRUDPReceiveAsync.exports.obj'