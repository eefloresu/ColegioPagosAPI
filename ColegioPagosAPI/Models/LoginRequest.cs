/*
* LoginRequest.cs
* Clase que representa la solicitud de inicio de sesión.
* Contiene las propiedades necesarias para autenticar a un usuario.
* Esta clase se utiliza para recibir los datos del cliente al intentar iniciar sesión.
*/

public class LoginRequest
{
    // Nombre de usuario que el cliente envía para autenticarse.
    public string NombreUsuario { get; set; }
    // Contraseña que el cliente envía para autenticarse.
    public string Clave { get; set; }
}