using android.app;
using android.content;
using android.util;
using ScriptCoreLib;


namespace com.oculus.vrappframework
{
    //==============================================================
    // ConsoleReceiver
    //
    // To send a "console" command to the app:
    // adb shell am -broadcast oculus.console -es cmd <command text>
    // where <command text> is replaced with a quoted string:
    // adb shell am -broadcast oculus.console -es cmd "print QA Event!"
    //==============================================================
    public class ConsoleReceiver : BroadcastReceiver
    {
        // X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360Photos\com\oculus\vrappframework\ConsoleReceiver.java
        public static string TAG = "OvrConsole";

        public static string CONSOLE_INTENT = "oculus.console";
        public static string CONSOLE_STRING_EXTRA = "cmd";

        #region extern "C"

        [Script(IsPInvoke = true)]
        public static void nativeConsoleCommand(long act, string command) { }
        #endregion

        static ConsoleReceiver receiver = new ConsoleReceiver();
        static bool registeredReceiver = false;
        static Activity activity = null;

        public static void startReceiver(Activity act)
        {
            activity = act;
            if (!registeredReceiver)
            {
                Log.d(TAG, "!!#######!! Registering console receiver");

                IntentFilter filter = new IntentFilter();
                filter.addAction(CONSOLE_INTENT);
                act.registerReceiver(receiver, filter);
                registeredReceiver = true;
            }
            else
            {
                Log.d(TAG, "!!!!!!!!!!! Already registered console receiver!");
            }
        }
        public static void stopReceiver(Activity act)
        {
            if (registeredReceiver)
            {
                Log.d(TAG, "Unregistering console receiver");
                act.unregisterReceiver(receiver);
                registeredReceiver = false;
            }
        }

        public override void onReceive(Context arg0, Intent intent)
        {
            Log.d(TAG, "!@#!@ConsoleReceiver action:" + intent);
            if (intent.getAction() == CONSOLE_INTENT)
            {
                // Unity apps will not have a VrActivity, so they can only use console functions that are ok
                // with a NULL appPtr.
                if (activity is VrActivity)
                {
                    nativeConsoleCommand(((VrActivity)activity).appPtr, intent.getStringExtra(CONSOLE_STRING_EXTRA));
                }
                else
                {
                    nativeConsoleCommand(((long)0), intent.getStringExtra(CONSOLE_STRING_EXTRA));
                }
            }
        }
    }
}
