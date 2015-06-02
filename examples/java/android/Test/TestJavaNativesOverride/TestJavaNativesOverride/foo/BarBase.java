package foo;
import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
public abstract class BarBase // extends Service
{
	//  error: Bar is not abstract and does not override abstract method onBind(Intent) in Service
	//public IBinder onBind(Intent intent) {return null;}
	//public abstract String GetBarString();
}