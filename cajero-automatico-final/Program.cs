static int BuscarCuenta(string cuenta, string contraseña)
{
    for (int i = 0; i < cuentas.GetLength(0); i++)
    {
        if (cuentas[i, 0] == cuenta && VerificarContraseña(contraseña, cuentas[i, 1]))
        {
            return i;
        }
    }
    return -1;
}