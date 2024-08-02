static void OperarCajero(int cuentaRegistro)
{
    int opcion;

    do
    {
        System.Threading.Thread.Sleep(4000);
        Console.Clear();
        MostrarMenu();
        opcion = LeerOpcion();

        switch (opcion)
        {
            case 1:
                RealizarRetiro(cuentaRegistro);
                break;
            case 2:
                MostrarMovimientos(cuentaRegistro);
                break;
            case 3:
                RealizarDeposito(cuentaRegistro);
                break;
            case 4:
                CambiarClave(cuentaRegistro);
                break;
            case 5:
                ConsultarSaldo(cuentaRegistro);
                break;
            case 6:
                RealizarTransferencia(cuentaRegistro);
                break;
            case 7:
                Console.WriteLine("Gracias por utlizar nuestros servios.");
                return;
            default:
                Console.WriteLine("Opción invalida por favor, seleccione una Opción valida.");
                break;
        }
    } while (true);
}