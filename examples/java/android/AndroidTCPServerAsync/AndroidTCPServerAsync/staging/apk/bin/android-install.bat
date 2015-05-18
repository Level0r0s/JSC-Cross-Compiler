:: 20141204
:: after ten years, we still need a bat file for a quick and dirty iteration
:: how do we write one?

@echo off

:: X:\jsc.svn\examples\java\android\Test\TestChromeAsAsset\TestChromeAsAsset\bin\Debug\staging\apk\bin
:: r:\jsc.svn\examples\java\android\Test\TestChromeAsAsset\TestChromeAsAsset\bin\Debug\staging\apk\bin

:: http://stackoverflow.com/questions/13534935/adb-uninstall-is-failed
:: "x:\util\android-sdk-windows\platform-tools\adb.exe" shell pm uninstall "AndroidTCPServerAsync.Activities-debug.apk"
"x:\util\android-sdk-windows\platform-tools\adb.exe" uninstall "AndroidTCPServerAsync.Activities"
"x:\util\android-sdk-windows\platform-tools\adb.exe" install -r "AndroidTCPServerAsync.Activities-debug.apk"

:: X:\jsc.svn\examples\java\android\AndroidTCPServerAsync\AndroidTCPServerAsync\bin\Debug\staging\apk\bin
:: r:\jsc.svn\examples\java\android\AndroidTCPServerAsync\AndroidTCPServerAsync\bin\Debug\staging\apk\bin
"X:\util\runfromprocess\RunFromProcess-x64.exe" nomsg explorer.exe cmd /K x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "DID" "chromium" "System.Console" "art" "ChildProcessService" "AndroidRuntime" "System.err"

call "x:\util\android-sdk-windows\platform-tools\adb.exe" shell am start -n AndroidTCPServerAsync.Activities/AndroidTCPServerAsync.Activities.ApplicationActivity

::pause


::Failure [DELETE_FAILED_INTERNAL_ERROR]
::Failure [INSTALL_PARSE_FAILED_INCONSISTENT_CERTIFICATES]