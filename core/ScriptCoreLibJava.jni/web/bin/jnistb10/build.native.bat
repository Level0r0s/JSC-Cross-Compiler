@echo off
pushd
echo + compile native
setlocal

rem https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151002
rem 2011 :) 2015
set _toolkit=x:\Program Files (x86)\Microsoft Visual Studio 14.0\VC
set _init=%_toolkit%\..\Common7\Tools\vsvars32.bat


echo - setting vars
call "%_init%" 
rem >nul

set _targetpath=bin
set _sourcefiles=*.c


set _libname=lib_jnistb10

set _java=X:\Program Files (x86)\Java\jdk1.7.0_79

set _args=/I "%_java%\include"
set _args=%_args% /I "%_java%\include\win32"
set _args=%_args% /TC /Zm200 /D "WIN32" 
rem set _args=%_args% /LD /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /Febin\%_libname%.dll 

rem /link /ASSEMBLYMODULE:bin/foo.netmodule

type dispatch.c > __program.c
type dispatch_x86.c >> __program.c

set _args=%_args% /LD /nologo /EHsc __program.c
set _args=%_args% /Fobin\%_libname%.obj



:: __program.c(30): fatal error C1083: Cannot open include file: 'jni.h': No such file or directory

rem echo cl.exe  %_args%
set _command="%_toolkit%\bin\cl.exe" %_args%

rem cd
rem dir *.cpp

rem echo %_command%
call %_command%
rem > compile.log

erase __program.c

pushd bin

rem call "C:\Program Files\Microsoft Visual Studio 10.0\VC\bin\link.exe" /DLL /LTCG /CLRIMAGETYPE:IJW     /OUT:bar.dll *.obj
  
popd
  
endlocal
popd
 
 echo done