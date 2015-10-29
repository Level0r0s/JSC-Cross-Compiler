Imports jsc.meta.Commands.Rewrite.RewriteToUltraApplication
Imports System

''' <summary>
''' You can debug your application by hitting F5.
''' </summary>
Public Module Program
    Public Sub Main(args As String())

        ' http://xgis.maaamet.ee/xGIS/XGis?app_id=UU82&user_id=at&bbox=529464.185181536,6581178.67615528,549196.965601648,6593508.79911233&LANG=1
        ' http://stackoverflow.com/questions/14575697/math-pow-with-negative-numbers-and-non-integer-powers



        xlng = 58.432703481019288
        xlat = 24.442902208375234

        ' rewrite broken? Or vba broken?
        xx = LEST97.lest_function_vba.geo_lest(xlng, xlat, 0)
        ' vba vs vbnet
        ' 6477013,789


        ' do we have vba?
        ' excel 2013 has vba?

        xy = LEST97.lest_function_vba.geo_lest(xlng, xlat, 1)


        '


        x = 6587022.3
        y = 541542.6

        'x = 6477013.789
        'y = 525871.672


        lng = lest_function_vba.lest_geo(x, y, 0)
        lat = lest_function_vba.lest_geo(x, y, 1)

        'lng = 58.432703481019288 {Double}
        'lat = 24.442902208375234 {Double}
        RewriteToUltraApplication.AsProgram.Launch(GetType(Application))
    End Sub


End Module

