static void CambiarClave(int cuentaRegistro)
{
    Console.WriteLine("Ingrese su nueva clave:");
    string nuevaClave = Console.ReadLine();
    cuentas[cuentaRegistro, 1] = (nuevaClave); // actualizar la contraseña 
    Console.WriteLine("Clave cambiada correctamente.");
}