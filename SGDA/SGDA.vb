Imports System.Text
Imports System.Security.Cryptography
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
'Clase Auxiliar
Namespace SGDA
    Public Class Json
        Public Shared Function json_decode(ByVal input As String)
            Dim decode = JsonConvert.DeserializeObject(input)
            Return decode
        End Function
        Public Shared Function getJson(ByVal input As String, ByVal key As String) As String
            Dim decode = JObject.Parse(input)
            Dim output = decode(key)
            If IsNothing(output) Then
                Return False
            End If
            Return output
        End Function
        Public Shared Function json_encode(ByVal input As Object)
            Dim encode = JsonConvert.SerializeObject(input)
            Return encode
        End Function
    End Class
End Namespace
