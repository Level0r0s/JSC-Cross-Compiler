:: damn BOM
:: 20141204
:: after ten years, we still need a bat file for a quick and dirty iteration
:: how do we write one?

@echo off

set TargetFileName=%2
set ConfigurationName=%3



pushd ..\bin\%ConfigurationName%

call c:\util\jsc\bin\jsc.exe %TargetFileName% -c

copy "web\%TargetFileName%.*" ".\staging\jni\"



pushd staging

echo update project
::call "x:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target android-8
call "x:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target android-22

echo ndk-build
::call X:\opensource\android-ndk-r10c\ndk-build.cmd
call X:\util\android-ndk-r10e\ndk-build.cmd



echo ERRORLEVEL: %ERRORLEVEL%
echo.

pause


::pause

echo sdk-build
call "x:\util\apache-ant-1.9.2\bin\ant.bat" debug

pushd bin

rem start /WAIT cmd /C android-install.bat
 
popd
popd


popd

pause