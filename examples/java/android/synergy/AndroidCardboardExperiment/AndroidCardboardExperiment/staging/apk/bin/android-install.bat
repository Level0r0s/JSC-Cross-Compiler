:: 20141204
:: after ten years, we still need a bat file for a quick and dirty iteration
:: how do we write one?

@echo off

:: "X:\jsc.svn\examples\java\android\synergy\AndroidCardboardExperiment\AndroidCardboardExperiment\bin\Debug\staging\apk\bin\android-install.bat"
"x:\util\android-sdk-windows\platform-tools\adb.exe" install -r "AndroidCardboardExperiment.Activities-debug.apk"

:: RemoteApp
:: "x:\util\android-sdk-windows\platform-tools\adb.exe" install -r "r:\jsc.svn\examples\java\android\synergy\AndroidCardboardExperiment\AndroidCardboardExperiment\bin\Debug\staging\apk\bin\AndroidCardboardExperiment.Activities-debug.apk"
:: we could run this in a loop if the length changes, or date changes

"X:\util\runfromprocess\RunFromProcess-x64.exe" nomsg explorer.exe cmd /K x:\util\android-sdk-windows\platform-tools\adb.exe logcat -v threadtime -s "System.Console"

::pause