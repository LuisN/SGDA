Imports System.Text
Imports System.Security.Cryptography
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class SGDA
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
    Public Shared Function json_decode(ByVal input As String)
        Dim decode = JsonConvert.DeserializeObject(input)
        Return decode
    End Function
    Public Shared Function getJson(ByVal input As String, ByVal key As String)
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