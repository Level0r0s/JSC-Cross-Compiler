Public Module lest_function_vba
    ' http://www.maaamet.ee/rr/geo-lest/files/geo-lest_function_vba.txt

    Function geo_lest(north, east, nr)
        LAT = north * (3.14159265358979 / 180)
        LON = east * (3.14159265358979 / 180)
        a = 6378137
        F = 298.257222100883
        RF = F
        F = 1 / F
        B0 = (57 + 31 / 60 + 3.194148 / 3600) * (3.14159265358979 / 180)
        L0 = 24 * (3.14159265358979 / 180)
        FN = 6375000
        FE = 500000
        B1 = (59 + 20 / 60) * (3.14159265358979 / 180)
        B2 = (58) * (3.14159265358979 / 180)
        xx = north - FN
        yy = east - FE
        f1 = 1 / RF
        er = (2 * f1) - (f1 * f1)
        loce = Sqrt(er)

        t1 = Sqrt(((1 - Sin(B1)) / (1 + Sin(B1))) * (((1 + loce * Sin(B1)) / (1 - loce * Sin(B1))) ^ loce))

        t2 = Sqrt(((1 - Sin(B2)) / (1 + Sin(B2))) * (((1 + loce * Sin(B2)) / (1 - loce * Sin(B2))) ^ loce))
        t0 = Sqrt(((1 - Sin(B0)) / (1 + Sin(B0))) * (((1 + loce * Sin(B0)) / (1 - loce * Sin(B0))) ^ loce))
        t = Sqrt(((1 - Sin(LAT)) / (1 + Sin(LAT))) * (((1 + loce * Sin(LAT)) / (1 - loce * Sin(LAT))) ^ loce))
        m1 = Cos(B1) / ((1 - er * Sin(B1) * Sin(B1)) ^ 0.5)
        m2 = Cos(B2) / ((1 - er * Sin(B2) * Sin(B2)) ^ 0.5)
        n = (Log(m1) - Log(m2)) / (Log(t1) - Log(t2))
        FF = m1 / (n * t1 ^ n)
        p0 = a * FF * (t0 ^ n)
        FII = n * (LON - L0)
        p = a * FF * t ^ n
        n = p0 - (p * Cos(FII)) + FN
        loce = p * Sin(FII) + FE

        If nr = 0 Then geo_lest = n Else geo_lest = loce
    End Function


    Function lest_geo(x, y, nr)
        a = 6378137
        F = 1 / 298.257222100883
        ESQ = F + F - F * F
        B0 = (57 + 31 / 60 + 3.194148 / 3600) / 57.2957795130823
        L0 = 24 / 57.2957795130823
        FN = 6375000
        FE = 500000
        B2 = (59 + 20 / 60) / 57.2957795130823
        B1 = 58 / 57.2957795130823
        xx = x - FN
        yy = y - FE

        'System.Console.WriteLine("B0 " & B0)
        'System.Console.WriteLine("ESQ " & ESQ)

        t0 = Sqrt((1 - Sin(B0)) / (1 + Sin(B0)) * ((1 + Sqrt(ESQ) * Sin(B0)) / (1 - Sqrt(ESQ) * Sin(B0))) ^ Sqrt(ESQ))
        t1 = Sqrt((1 - Sin(B1)) / (1 + Sin(B1)) * ((1 + Sqrt(ESQ) * Sin(B1)) / (1 - Sqrt(ESQ) * Sin(B1))) ^ Sqrt(ESQ))
        t2 = Sqrt((1 - Sin(B2)) / (1 + Sin(B2)) * ((1 + Sqrt(ESQ) * Sin(B2)) / (1 - Sqrt(ESQ) * Sin(B2))) ^ Sqrt(ESQ))
        m1 = Cos(B1) / (1 - ESQ * Sin(B1) * Sin(B1)) ^ 0.5
        m2 = Cos(B2) / (1 - ESQ * Sin(B2) * Sin(B2)) ^ 0.5
        n1 = (Log(m1) - Log(m2)) / (Log(t1) - Log(t2))
        FF = m1 / (n1 * t1 ^ n1)
        p0 = a * FF * t0 ^ n1
        p = (yy * yy + (p0 - xx) * (p0 - xx)) ^ 0.5
        t = (p / (a * FF)) ^ (1 / n1)
        FII = Atan(yy / (p0 - xx))
        LON = FII / n1 + L0
        u = (3.14159265358979 / 2) - (2 * Atan(t))
        LAT = (u + (ESQ / 2 + (5 * ESQ ^ 2 / 24) + (ESQ ^ 3 / 12) + (13 * ESQ ^ 4 / 360)) * Sin(2 * u) + ((7 * ESQ ^ 2 / 48) + (29 * ESQ ^ 3 / 240) + (811 * ESQ ^ 4 / 11520)) * Sin(4 * u) + ((7 * ESQ ^ 3 / 120) + (81 * ESQ ^ 4 / 1120)) * Sin(6 * u) + (4279 * ESQ ^ 4 / 161280) * Sin(8 * u))
        LAT = LAT * 57.2957795130823
        LON = LON * 57.2957795130823

        If nr = 0 Then lest_geo = LAT Else lest_geo = LON
    End Function


    'V:\staging\web\java\LEST97\lest_function_vba.java:59: error: cannot find symbol
    '    object21 = __RuntimeHelpers.GetObjectValue(object5);

    Function hint() As System.Type
        hint = GetType(ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices.__RuntimeHelpers)
    End Function

End Module

' https://en.wikibooks.org/wiki/Visual_Basic_.NET/Visual_Basic_6_to_.NET_Function_Equivalents
' Error	2	'Sqrt' is not declared. Function has moved to the 'System.Math' class and is named 'Sqrtt'.	Z:\jsc.svn\examples\javascript\vb\LEST97\LEST97\Library\lest_function_vba.vb	19	13	LEST97
' script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Operators.ExponentObject(System.Object, System.Object)]
' script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(System.Object, System.Object)]
