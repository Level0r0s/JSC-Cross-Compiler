

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
::set _sourcefiles=foo/foo.c *.c

::set _sourcefiles=zlib/contrib/blast/*.c
::set _sourcefiles=%_sourcefiles% zlib/contrib/minizip/*.c
::set _sourcefiles=%_sourcefiles% zlib/*.c
::set _sourcefiles=%_sourcefiles% zlib/contrib/puff/*.c
set _sourcefiles=%_sourcefiles% bar/bar.cpp 
set _sourcefiles=%_sourcefiles% Ws2_32.lib
::set _sourcefiles=%_sourcefiles% cryptopp/*.cpp 
set _sourcefiles=%_sourcefiles% meganz/*.cpp 
 
 

set _sourcefiles=%_sourcefiles% foo/foo.c *.c


::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"

:: not to be used?
:: used by zlib\contrib\minizip\ioapi.h
:: X:\opensource\github\meganz\sdk\include\mega\win32\meganet.h
set _args=%_args% /I "zlib"
set _args=%_args% /I "bar"
::set _args=%_args% /I "meganz/mega/posix"
set _args=%_args% /I "meganz/mega/win32"
::set _args=%_args% /I "meganz/mega"
set _args=%_args% /I "meganz"

:: need nested include?
set _args=%_args% /I "cryptopp"

 
 set _args=%_args% /DCRYPTOPP_DISABLE_ASM
 set _args=%_args% /DUSE_CRYPTOPP
 
 
::set _args=%_args% /TC /Zm200 
set _args=%_args% /Zm200 
::set _args=%_args% /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /EHsc  %_sourcefiles% 
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
