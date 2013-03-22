Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Runtime.Serialization
Imports SDGA.SGDA
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class frmLogin
    Friend Session As New ArrayList
    Private Sub BtnEntrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEntrar.Click
        If txtUsername.TextLength = 0 Or txtPassword.TextLength = 0 Then
            MsgBox("Ingrese Usuario y Contraseña", MsgBoxStyle.Exclamation, "Complete todos los campos")
        Else
            EnDi(False)
            'Tomamos el nombre de usuario del campo de texto de formulario
            Dim username As String = txtUsername.Text
            'Creamos una matriz de Bytes
            Dim byteArray As Byte()
            'Instanciamos la clase SGDA 

            'Tomamos la contraseña y la encriptamos con el metodo GetHash de la clase SGDA
            Dim password As String = getHash(txtPassword.Text)
            'Creamos una peticion al servidor 
            Dim request As WebRequest = WebRequest.Create("http://localhost/api/")
            'Establecemos El agente de usuario de la peticion
            CType(request, HttpWebRequest).UserAgent = "SDGA/1.0"

            'Establecemos el metodo de petcion a POST, para enviar datos
            request.Method = "POST"
            'Convertimos la cadena de usuario y contraseña que enviaremos al servidor en una matriz de Bytes en Formato UTF-8
            byteArray = Encoding.UTF8.GetBytes("username=" + username + "&password=" + password)
            'Establecemos ContentLength al mismo tamaño que la matriz de datos
            request.ContentLength = byteArray.Length
            'Establecemos el tipo de la peticion para poder enviar los datos via POST
            request.ContentType = "application/x-www-form-urlencoded"
            'Ejecutamos la peticion y retornamos los datos en un Stream
            'Try
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
            'asigno las variables 
            Dim userjson As String = getJson(responseFromServer, "username")
            Dim passjson As String = getJson(responseFromServer, "password")
            Dim access_token As String = getJson(responseFromServer, "accessToken")
            Dim code As Integer = getJson(responseFromServer, "code")
            'Debug
            Console.WriteLine("User: " & userjson)
            Console.WriteLine("Pass: " & passjson)
            Console.WriteLine("Token: " & access_token)
            Console.WriteLine("Code: " & code)
            'Asigno los datos retornados por el servidor a la variable de sesion
            Session.Add(access_token)
            Session.Add(userjson)
            Session.Add(passjson)
            Session.Add(code)
            If code = 200 Then
                frmPrincipal.Show()
                'Catch es As SystemException
                ''Console.WriteLine(es)
                'lblStatus.Text = es.Message
                'EnDi(True)
                'End Try
            ElseIf code = 404 Then
                lblStatus.Text = "Usuario y/o Contraseña incorrectos"
            End If
        End If
    End Sub
    Private Sub EnDi(ByVal bool As Boolean)
        BtnEntrar.Enabled = bool
        txtUsername.Enabled = bool
        txtPassword.Enabled = bool
    End Sub
    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        'Cadena JSON tipica "{'username':'Luis_N','password':'MewTwo'}"
        'Para poder enviar datos al servidor se necesita crear un objeto anonimo con las variables inicializadas en Nothing (como muestra debajo)
        'Luego podemos agregar los valores a cada propiedad(variable) del objeto anonimo
        'para finalizar convertimos con CType a object el anonimo y finalmente lo pasamos de parametro a json_encode lo que generara una cedena codificada en json
        'ESTA PARTE ES MUY IMPORTANTE O DE LO CONTRARIO NO PODREMOS CONVERTIR A JSON
        Dim Obj = New With {.name = Nothing, .password = Nothing} 'tantas claves como datos necesitemos enviar al servidor
        Obj.Name = "Luis_N"
        Obj.password = "MewTwo"
        'Esto es Debug :D
        Console.WriteLine(json_encode(CType(Obj, Object)))
        Console.WriteLine(getJson(json_encode(CType(Obj, Object)), "name"))
    End Sub
End Class
