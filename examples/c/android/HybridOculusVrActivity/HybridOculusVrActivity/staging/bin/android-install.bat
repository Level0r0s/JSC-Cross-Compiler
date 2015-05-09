:: modify
:: X:\jsc.svn\examples\c\android\HybridOculusVrActivity\HybridOculusVrActivity\staging\bin\android-install.bat

:: 20141204
:: after ten years, we still need a bat file for a quick and dirty iteration
:: how do we write one?

@echo off

"x:\util\android-sdk-windows\platform-tools\adb.exe" install -r "ApplicationActivity-debug.apk"
start cmd /K x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "OVR" "chromium" "System.Console" "VrActivity" "xNativeActivity" "VrApi" "VrLib"


::pause