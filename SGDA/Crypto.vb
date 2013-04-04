Imports System.Text
Imports System.Security.Cryptography
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
'Clase Auxiliar
Namespace SGDA
    Public Class Crypto
        'Obtiene el Hash de la cadena de entrada text
        Public Shared Function getHash(ByVal text As String) As String
            Dim password As Byte() = Encoding.UTF8.GetBytes(text)
            Dim md5 As MD5 = md5.Create()
            password = md5.ComputeHash(password)
            Dim sBuilder As New StringBuilder()
            Dim i As Integer
            For i = 0 To password.Length - 1
                sBuilder.Append(password(i).ToString("x2"))
            Next i
            Return sBuilder.ToString()
        End Function
    End Class
End Namespace