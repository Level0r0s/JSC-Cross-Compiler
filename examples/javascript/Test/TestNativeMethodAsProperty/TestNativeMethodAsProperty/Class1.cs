using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestNativeMethodAsProperty
{
    [Script(HasNoPrototype = true, ExternalTarget = "google.maps.LatLng")]
    public class LatLng
    {
        // Error	2	'TestNativeMethodAsProperty.LatLng.lng.get' must declare a body because it is not marked abstract or extern. Automatically implemented properties must define both get and set accessors.	Z:\jsc.svn\examples\javascript\Test\TestNativeMethodAsProperty\TestNativeMethodAsProperty\Class1.cs	21	61	TestNativeMethodAsProperty


        // Latitude ranges between -90 and 90 degrees, inclusive

        // TypeError: this.map.getCenter(...).get_lat is not a function
        public double lat { [method: Script(ExternalTarget = "lat")]get; private set; }

        public double lng { [method: Script(ExternalTarget = "lng")]get; private set; }
        // LatLngLiteral 
        //public double lat;

        // Longitude ranges between -180 and 180 degrees, inclusive. 
        //public double lng;
    }

    [Script]
    public class Class1
    {
        public static double Invoke(LatLng e)
        {
            // c = b.lat();
            return e.lat;
        }
    }
}
