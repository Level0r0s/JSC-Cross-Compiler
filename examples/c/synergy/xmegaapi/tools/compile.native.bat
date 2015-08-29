

@echo off

set ConfigurationName=%2

pushd ..\bin\%ConfigurationName%\web\
echo + compile native [%1] %ConfigurationName%
setlocal

set _libname=%1

echo - setting vars 
echo call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"
call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"

set _targetpath=bin\Debug\web


:: cleanup from last build
del *.obj

:: https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150829/lib
:: references as lib 

call "cl.exe" /Zl /Zi /LD /MT /c zlib/*.c
::call "lib.exe" *.obj /out:libzlib.lib
:: "Z:\jsc.svn\examples\c\synergy\xmegaapi\bin\Debug\web\libzlib.lib"
mkdir obj.libzlib
move *.obj obj.libzlib


call "cl.exe" /Zl /Zi /LD /MT /c libsqlite3/*.c
::call "lib.exe" *.obj /out:libsqlite3.lib
:: "Z:\jsc.svn\examples\c\synergy\xmegaapi\bin\Debug\web\libsqlite3.lib"
mkdir obj.libsqlite3
move *.obj obj.libsqlite3
::del *.obj


:: https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150829/lib

call "cl.exe" /EHsc /Zl /Zi /LD /MT /DCRYPTOPP_DISABLE_ASM /c cryptopp/cryptopp/*.cpp 
::call "lib.exe" *.obj /out:libcryptopp.lib
::del *.obj
mkdir obj.libcryptopp
move *.obj obj.libcryptopp




::set _sourcefiles=foo/foo.c *.c
::set _sourcefiles=zlib/contrib/blast/*.c
::set _sourcefiles=%_sourcefiles% zlib/contrib/minizip/*.c
::set _sourcefiles=%_sourcefiles% zlib/*.c
::set _sourcefiles=%_sourcefiles% zlib/contrib/puff/*.c

set _sourcefiles=
set _sourcefiles=%_sourcefiles% bar/bar.cpp 
set _sourcefiles=%_sourcefiles% Ws2_32.lib
set _sourcefiles=%_sourcefiles% winhttp.lib
:: https://msdn.microsoft.com/en-us/library/windows/desktop/aa376075(v=vs.85).aspx
set _sourcefiles=%_sourcefiles% Crypt32.lib
set _sourcefiles=%_sourcefiles% Shlwapi.lib

::set _sourcefiles=%_sourcefiles% libsqlite3.lib
set _sourcefiles=%_sourcefiles% obj.libsqlite3/*.obj

::set _sourcefiles=%_sourcefiles% cryptopp/cryptopp/*.cpp 
::set _sourcefiles=%_sourcefiles% libcryptopp.lib
set _sourcefiles=%_sourcefiles% obj.libcryptopp/*.obj
set _sourcefiles=%_sourcefiles% obj.libzlib/*.obj

::set _sourcefiles=%_sourcefiles% meganz/mega/gfx/*.cpp 
set _sourcefiles=%_sourcefiles% meganz/mega/gfx/external.cpp 
set _sourcefiles=%_sourcefiles% meganz/mega/thread/win32thread.cpp 
set _sourcefiles=%_sourcefiles% meganz/mega/win32/*.cpp 
set _sourcefiles=%_sourcefiles% meganz/mega/crypto/*.cpp 
set _sourcefiles=%_sourcefiles% meganz/mega/db/*.cpp 
set _sourcefiles=%_sourcefiles% meganz/mega/*.cpp 
 
 

set _sourcefiles=%_sourcefiles% foo/foo.c *.c

set _args=
::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"

:: not to be used?
:: used by zlib\contrib\minizip\ioapi.h
:: X:\opensource\github\meganz\sdk\include\mega\win32\meganet.h
set _args=%_args% /I "libsqlite3"
set _args=%_args% /I "zlib"
set _args=%_args% /I "bar"
::set _args=%_args% /I "meganz/mega/posix"
::set _args=%_args% /I "meganz/mega/db"
set _args=%_args% /I "meganz/mega/win32"
::set _args=%_args% /I "meganz/mega"
set _args=%_args% /I "meganz"

:: need nested include?
set _args=%_args% /I "cryptopp"

 
 set _args=%_args% /DCRYPTOPP_DISABLE_ASM
 set _args=%_args% /DUSE_CRYPTOPP
 :: X:\opensource\github\meganz\sdk\include\mega\db\sqlite.h
 set _args=%_args% /DUSE_SQLITE
 
 
::set _args=%_args% /TC /Zm200 
set _args=%_args% /Zm200 
::set _args=%_args% /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /EHsc  %_sourcefiles% 
:: cl : Command line error D8036 : '/Foobjfolder' not allowed with multiple source files
set _args=%_args% /Fe%_libname%



rem echo cl.exe  %_args%
set _command="cl.exe" %_args%

rem cd
rem dir *.cpp

echo %cd%
echo call %_command%
call %_command%
echo done
rem > compile.log

endlocal
popd
