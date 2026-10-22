using System;
using System.Text;
namespace Supermercado
{
    internal class Program
    {
        
        const int MAX_USUARIOS = 20;
        const int MAX_ARTICULOS = 20;
        const int MAX_ITEMS_VENTA = 20;
        const double IVA = 0.19;

        // DATOS DE USUARIOS
        static string[] usuCedula = new string[MAX_USUARIOS];
        static string[] usuNombre = new string[MAX_USUARIOS];
        static string[] usuTelefono = new string[MAX_USUARIOS];
        static string[] usuDireccion = new string[MAX_USUARIOS];
        static int totalUsuarios = 0;

        // DATOS DE ARTÍCULOS 
        static int[] artId = new int[MAX_ARTICULOS];
        static string[] artNombre = new string[MAX_ARTICULOS];
        static double[] artPrecio = new double[MAX_ARTICULOS];
        static int[] artStock = new int[MAX_ARTICULOS];
        static int totalArticulos = 0;
        static int siguienteIdArticulo = 1;

        // DATOS DE VENTAS
        static int numeroFactura = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int opcion;
            do
            {
                opcion = MenuPrincipal();
                switch (opcion)
                {
                    case 1: MenuUsuarios(); break;
                    case 2: MenuArticulos(); break;
                    case 3: MenuVentas(); break;
                    case 4: Console.WriteLine("¡Hasta pronto!"); break;
                }
            } while (opcion != 4);
        }

  
        static int MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("SUPERMERCADO");
            Console.WriteLine("1. Gestión de usuarios");
            Console.WriteLine("2. Gestión de artículos");
            Console.WriteLine("3. Gestión de ventas");
            Console.WriteLine("4. Salir del programa");
            return LeerEntero("Seleccione una opción: ", 1, 4);
        }

        static void MenuUsuarios()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("GESTIÓN DE USUARIOS");
                Console.WriteLine("1. Ver lista de usuarios");
                Console.WriteLine("2. Nuevo usuario");
                Console.WriteLine("3. Editar información de usuario (buscar por c.c)");
                Console.WriteLine("4. Salir de Gestión de usuarios (Menú principal)");
                opcion = LeerEntero("Seleccione una opción: ", 1, 4);

                switch (opcion)
                {
                    case 1: VerUsuarios(); Pausa(); break;
                    case 2: NuevoUsuario(); Pausa(); break;
                    case 3: EditarUsuario(); Pausa(); break;
                }
            } while (opcion != 4);
        }

        static void VerUsuarios()
        {
            Console.WriteLine("LISTA DE USUARIOS");
            if (totalUsuarios == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }
            Console.WriteLine("{0} {1} {2} {3}",
                "C.C".PadRight(15), "Nombre".PadRight(25), "Teléfono".PadRight(15), "Dirección");
            for (int i = 0; i < totalUsuarios; i++)
            {
                Console.WriteLine("{0} {1} {2} {3}",
                    usuCedula[i].PadRight(15), usuNombre[i].PadRight(25),
                    usuTelefono[i].PadRight(15), usuDireccion[i]);
            }
        }

        static void NuevoUsuario()
        {
            Console.WriteLine(" NUEVO USUARIO");
            if (totalUsuarios >= MAX_USUARIOS)
            {
                Console.WriteLine("No se pueden registrar más usuarios (límite alcanzado).");
                return;
            }

            string cedula = LeerTextoObligatorio("C.C: ");
            if (BuscarUsuario(cedula) != -1)
            {
                Console.WriteLine("Ya existe un usuario con esa cédula.");
                return;
            }

            usuCedula[totalUsuarios] = cedula;
            usuNombre[totalUsuarios] = LeerTextoObligatorio("Nombre: ");
            usuTelefono[totalUsuarios] = LeerTextoObligatorio("Teléfono: ");
            usuDireccion[totalUsuarios] = LeerTextoObligatorio("Dirección: ");
            totalUsuarios++;
            Console.WriteLine("Usuario registrado correctamente.");
        }

        static void EditarUsuario()
        {
            Console.WriteLine(" EDITAR USUARIO");
            string cedula = LeerTextoObligatorio("C.C del usuario a editar: ");
            int pos = BuscarUsuario(cedula);
            if (pos == -1)
            {
                Console.WriteLine("No se encontró un usuario con esa cédula.");
                return;
            }

            Console.WriteLine("(Presione Enter para conservar el valor actual)");
            usuNombre[pos] = LeerTextoOpcional("Nombre [" + usuNombre[pos] + "]: ", usuNombre[pos]);
            usuTelefono[pos] = LeerTextoOpcional("Teléfono [" + usuTelefono[pos] + "]: ", usuTelefono[pos]);
            usuDireccion[pos] = LeerTextoOpcional("Dirección [" + usuDireccion[pos] + "]: ", usuDireccion[pos]);
            Console.WriteLine("Usuario actualizado correctamente.");
        } static int BuscarUsuario(string cedula)
        {
            for (int i = 0; i < totalUsuarios; i++)
            {
                if (usuCedula[i] == cedula) return i;
            }
            return -1;
        }
        static void MenuArticulos()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine(" GESTIÓN DE ARTÍCULOS ");
                Console.WriteLine("1. Ver lista de artículos");
                Console.WriteLine("2. Nuevo artículo");
                Console.WriteLine("3. Editar información del artículo (buscar por id)");
                Console.WriteLine("4. Salir de Gestión de Artículos (Menú Principal)");
                opcion = LeerEntero("Seleccione una opción: ", 1, 4);

                switch (opcion)
                {
                    case 1: VerArticulos(); Pausa(); break;
                    case 2: NuevoArticulo(); Pausa(); break;
                    case 3: EditarArticulo(); Pausa(); break;
                }
            } while (opcion != 4);
        }

        static void VerArticulos()
        {
            Console.WriteLine("LISTA DE ARTÍCULOS ");
            if (totalArticulos == 0)
            {
                Console.WriteLine("No hay artículos registrados.");
                return;
            }
            Console.WriteLine("{0} {1} {2} {3}",
                "ID".PadRight(6), "Nombre".PadRight(25), "Precio".PadRight(14), "Stock");
            for (int i = 0; i < totalArticulos; i++)
            {
                Console.WriteLine("{0} {1} {2} {3}",
                    artId[i].ToString().PadRight(6), artNombre[i].PadRight(25),
                    artPrecio[i].ToString("N2").PadRight(14), artStock[i]);
            }
        }

        static void NuevoArticulo()
        {
            Console.WriteLine("NUEVO ARTÍCULO");
            if (totalArticulos >= MAX_ARTICULOS)
            {
                Console.WriteLine("No se pueden registrar más artículos (límite alcanzado).");
                return;
            }

            artId[totalArticulos] = siguienteIdArticulo;
            artNombre[totalArticulos] = LeerTextoObligatorio("Nombre: ");
            artPrecio[totalArticulos] = LeerDecimal("Precio: ", 0.01);
            artStock[totalArticulos] = LeerEntero("Cantidad en stock: ", 0, 1000000);
            Console.WriteLine("Artículo registrado con ID " + siguienteIdArticulo + ".");
            siguienteIdArticulo++;
            totalArticulos++;
        }

        static void EditarArticulo()
        {
            Console.WriteLine(" EDITAR ARTÍCULO");
            int id = LeerEntero("ID del artículo a editar: ", 1, int.MaxValue);
            int pos = BuscarArticulo(id);
            if (pos == -1)
            {
                Console.WriteLine("No se encontró un artículo con ese ID.");
                return;
            }

            Console.WriteLine("(Presione Enter para conservar el valor actual)");
            artNombre[pos] = LeerTextoOpcional("Nombre [" + artNombre[pos] + "]: ", artNombre[pos]);
            artPrecio[pos] = LeerDecimalOpcional("Precio [" + artPrecio[pos].ToString("N2") + "]: ", artPrecio[pos]);
            artStock[pos] = LeerEnteroOpcional("Stock [" + artStock[pos] + "]: ", artStock[pos]);
            Console.WriteLine("Artículo actualizado correctamente.");
        }

      
        static int BuscarArticulo(int id)
        {
            for (int i = 0; i < totalArticulos; i++)
            {
                if (artId[i] == id) return i;
            }
            return -1;
        }

        static void MenuVentas()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== GESTIÓN DE VENTAS =====");
                Console.WriteLine("1. Registrar venta");
                Console.WriteLine("2. Salir de Gestión de venta (Menú Principal)");
                opcion = LeerEntero("Seleccione una opción: ", 1, 2);

                if (opcion == 1)
                {
                    RegistrarVenta();
                    Pausa();
                }
            } while (opcion != 2);
        }

        static void RegistrarVenta()
        {
            Console.WriteLine("REGISTRAR VENTA");

            if (totalUsuarios == 0 || totalArticulos == 0)
            {
                Console.WriteLine("Debe haber al menos un usuario y un artículo registrados.");
                return;
            }

        
            int posUsuario = SeleccionarUsuario();
            if (posUsuario == -1)
            {
                Console.WriteLine("Venta cancelada.");
                return;
            }

   
            int[] itemPos = new int[MAX_ITEMS_VENTA];
            int[] itemCant = new int[MAX_ITEMS_VENTA];
            int totalItems = SeleccionarArticulos(itemPos, itemCant);

            if (totalItems == 0)
            {
                Console.WriteLine("No se seleccionó ningún artículo. Venta cancelada.");
                return;
            }

           
            GenerarFactura(posUsuario, itemPos, itemCant, totalItems);
        }

       
        static int SeleccionarUsuario()
        {
            VerUsuarios();
            while (true)
            {
                string cedula = LeerTextoObligatorio("C.C del cliente (0 para cancelar): ");
                if (cedula == "0") return -1;

                int pos = BuscarUsuario(cedula);
                if (pos != -1)
                {
                    Console.WriteLine("Cliente seleccionado: " + usuNombre[pos]);
                    return pos;
                }
                Console.WriteLine("No existe un usuario con esa cédula.");
            }
        }

        
        static int SeleccionarArticulos(int[] itemPos, int[] itemCant)
        {
            int total = 0;
            Console.WriteLine();
            VerArticulos();

            while (total < MAX_ITEMS_VENTA)
            {
                int id = LeerEntero("ID del artículo (0 para terminar): ", 0, int.MaxValue);
                if (id == 0) break;

                int pos = BuscarArticulo(id);
                if (pos == -1)
                {
                    Console.WriteLine("No existe un artículo con ese ID.");
                    continue;
                }
                if (ArticuloYaAgregado(itemPos, total, pos))
                {
                    Console.WriteLine("Ese artículo ya está en la venta.");
                    continue;
                }
                if (artStock[pos] == 0)
                {
                    Console.WriteLine("No hay stock de ese artículo.");
                    continue;
                }

                itemPos[total] = pos;
                itemCant[total] = LeerEntero("Cantidad (disponible " + artStock[pos] + "): ", 1, artStock[pos]);
                total++;
                Console.WriteLine("Artículo agregado (" + total + "/" + MAX_ITEMS_VENTA + ").");
            }

            if (total == MAX_ITEMS_VENTA)
                Console.WriteLine("Se alcanzó el máximo de " + MAX_ITEMS_VENTA + " artículos por venta.");

            return total;
        }

        static bool ArticuloYaAgregado(int[] itemPos, int total, int pos)
        {
            for (int i = 0; i < total; i++)
            {
                if (itemPos[i] == pos) return true;
            }
            return false;
        }

        static void GenerarFactura(int posUsuario, int[] itemPos, int[] itemCant, int totalItems)
        {
            double subtotal = 0;

            Console.WriteLine("FACTURA");
            Console.WriteLine("Factura N° " + numeroFactura.ToString("D4"));
            Console.WriteLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Cliente: " + usuNombre[posUsuario] + "  (C.C " + usuCedula[posUsuario] + ")");
            Console.WriteLine("{0} {1} {2} {3}",
                "Artículo".PadRight(22), "Cant.".PadRight(6), "P. Unit.".PadRight(11), "Total");

            for (int i = 0; i < totalItems; i++)
            {
                int p = itemPos[i];
                double totalLinea = artPrecio[p] * itemCant[i];
                subtotal += totalLinea;

                Console.WriteLine("{0} {1} {2} {3}",
                    artNombre[p].PadRight(22), itemCant[i].ToString().PadRight(6),
                    artPrecio[p].ToString("N2").PadRight(11), totalLinea.ToString("N2"));

                artStock[p] -= itemCant[i]; 
            }

            double valorIva = subtotal * IVA;

            Console.WriteLine("Subtotal:".PadRight(41) + subtotal.ToString("N2"));
            Console.WriteLine(("IVA (" + (IVA * 100) + "%):").PadRight(41) + valorIva.ToString("N2"));
            Console.WriteLine("TOTAL A PAGAR:".PadRight(41) + (subtotal + valorIva).ToString("N2"));


            numeroFactura++;
        }

       
        static void Pausa()
        {
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        static int LeerEntero(string mensaje, int min, int max)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                    return valor;
                Console.WriteLine("Valor inválido. Ingrese un número entre " + min + " y " + max + ".");
            }
        }

        static double LeerDecimal(string mensaje, double min)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out valor) && valor >= min)
                    return valor;
                Console.WriteLine("Valor inválido. Ingrese un número mayor o igual a " + min + ".");
            }
        }

        static string LeerTextoObligatorio(string mensaje)
        {
            string texto;
            while (true)
            {
                Console.Write(mensaje);
                texto = Console.ReadLine().Trim();
                if (texto != "") return texto;
                Console.WriteLine("Este campo no puede estar vacío.");
            }
        }

       
        static string LeerTextoOpcional(string mensaje, string actual)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine().Trim();
            return texto == "" ? actual : texto;
        }

        static double LeerDecimalOpcional(string mensaje, double actual)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string texto = Console.ReadLine().Trim();
                if (texto == "") return actual;
                if (double.TryParse(texto, out valor) && valor > 0) return valor;
                Console.WriteLine("Valor inválido.");
            }
        }

        static int LeerEnteroOpcional(string mensaje, int actual)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                string texto = Console.ReadLine().Trim();
                if (texto == "") return actual;
                if (int.TryParse(texto, out valor) && valor >= 0) return valor;
                Console.WriteLine("Valor inválido.");
            }
        }
    }
}