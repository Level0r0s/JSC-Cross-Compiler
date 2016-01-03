
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
:: "X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\bin\Debug\staging\project.properties"
:: Error: Target id 'android-10' is not valid. Use 'android.bat list targets' to get the target ids.

:: id: 8 or "Google Inc.:Google APIs:22"
::call "x:\util\android-sdk-windows\tools\android.bat" list targets

::pause
:: exit 0

:: Error: X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging is not a valid project (AndroidManifest.xml not found).
:: http://developer.android.com/tools/projects/projects-cmdline.html
:: 
::call "x:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target "android-22" --name OVROculus360PhotosNDK
::call "x:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target "android-21" --name AndroidBrowserVRNDK
call "x:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target "android-21" --name x360videoNDK



echo ndk-build
:: Android NDK: Your APP_BUILD_SCRIPT points to an unknown file: ./jni/Android.mk
rem call r:\util\android-ndk-r10e\ndk-build.cmd
call x:\util\android-ndk-r10e\ndk-build.cmd

echo ERRORLEVEL: %ERRORLEVEL%

::pause

if  %ERRORLEVEL%==0 exit 0
pause
exit %ERRORLEVEL%

popd

call c:\util\jsc\bin\jsc.exe %TargetFileName% -java
::pause
XCOPY web\java\* staging\src /s /i /Y  

pushd staging

::pause

echo sdk-build

set JAVA_HOME=C:\Program Files (x86)\Java\jdk1.7.0_45
call "C:\util\apache-ant-1.9.2\bin\ant.bat" debug

pushd bin

start /WAIT cmd /C android-install.bat
 
popd
popd


popd

