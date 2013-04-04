Imports System.Net
Imports System.IO
Imports System.Text
Namespace SGDA
    Public Class WebBackend
        Public Shared Function Req(ByVal url As String, ByVal data As String ) As String
            'Creamos una peticion al servidor 
            Dim request As WebRequest = WebRequest.Create("http://localhost/api/")
            'Establecemos El agente de usuario de la peticion
            CType(request, HttpWebRequest).UserAgent = "SDGA/1.0"

            'Establecemos el metodo de petcion a POST, para enviar datos
            request.Method = "POST"
            'Convertimos la cadena de usuario y contraseña que enviaremos al servidor en una matriz de Bytes en Formato UTF-8
            'Creamos una matriz de Bytes
            Dim byteArray As Byte()
            byteArray = Encoding.UTF8.GetBytes(data)
            'Establecemos ContentLength al mismo tamaño que la matriz de datos
            request.ContentLength = byteArray.Length
            'Establecemos el tipo de la peticion para poder enviar los datos via POST
            request.ContentType = "application/x-www-form-urlencoded"
            'Ejecutamos la peticion y retornamos los datos en un Stream
            Try
                Dim dataStream As Stream = request.GetRequestStream()
                'Reescribimos la matriz
                dataStream.Write(byteArray, 0, byteArray.Length)

                'Intanciamos la clases de Respuesta Web y pasamos de parametro la respuesta de la peticion
                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                'Devolvemos la respuesta del servidor como un Flujo
                dataStream = response.GetResponseStream()
                'Iniciamos un lector de Flujo
                Dim reader As New StreamReader(dataStream)
                'Leemos hasta el final el Flujo y lo depositamos en una cadena de texto
                Dim responseFromServer As String = reader.ReadToEnd()
                Return responseFromServer
            Catch we As WebException
                Return ""
            End Try
        End Function
    End Class
End Namespace
