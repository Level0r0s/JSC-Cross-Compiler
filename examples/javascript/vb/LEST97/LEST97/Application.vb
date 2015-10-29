Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Components
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports ScriptCoreLib.JavaScript.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Linq
Imports LEST97
Imports LEST97.Design
Imports LEST97.HTML.Pages

''' <summary>
''' Your client side code running inside a web browser as JavaScript.
''' </summary>
Public NotInheritable Class Application
    Inherits ApplicationWebService
    ''' <summary>
    ''' This is a javascript application.
    ''' </summary>
    ''' <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
    Public Sub New(page As IApp)
        ' it has been a while since last vb
        ' http://www.maaamet.ee/rr/geo-lest/files/geo-lest_function_vba.txt


        Native.body.style.backgroundColor = "cyan"

        ' 6477013.789	525871.672

        'x = 6477013.789
        'y = 525871.672


        x = 6587022.3
        y = 541542.6


        lng = lest_function_vba.lest_geo(x, y, 0)
        lat = lest_function_vba.lest_geo(x, y, 1)


        page.lat.value = lat
        page.lng.value = lng

        page.x.value = x
        page.y.value = y




    End Sub


End Class

'02000020 LEST97.lest_function_vba
'arg[0] is typeof System.Object
'arg[1] is typeof System.Object
'script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Operators.MultiplyObject(System.Object, System.Object)]
' script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Operators.DivideObject(System.Object, System.Object)]
'#Const script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Conversions.ToDouble(System.Object)]
' script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Conversions.ToString(System.Object)]