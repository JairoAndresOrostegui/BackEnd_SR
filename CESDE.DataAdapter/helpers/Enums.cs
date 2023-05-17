namespace CESDE.DataAdapter.helpers
{
      public class Enums
      {
            //Mensaje de login
            public static string MessageLogin = "Usuario o contraseña incorrecto";
            public static string MessageLoginExitoso = "Autenticación exitosa";
            public static string MessageErrorChangePassword = "Error al actualizar la contraseña";
            public static string MessageChangePassword = "Contraseña actualizada con exito";
            public static string MessageUserUpdatePassword = "Debe actualizar su clave";

            //Mensajes de confirmación
            public static string MessageSave = "Registro guardado con exito";
            public static string MessageUpdate = "Registro actualizado con exito";
            public static string MessageDelete = "Registro eliminado con exito";

            //Mensajes de validación de fecha
            public static string MessageTimeStart = "La fecha de inicio en el sistema aún no se cumple";
            public static string MessageTimeEnd = "Ya finalizo la fecha de ingreso al sistema";

            //Mensaje de listas y referencias
            public static string MessageDoesNotExist = "Este registro no existe";
            public static string MessageNameYesExist = "Ya existe un registro con este nombre";
            public static string MessageNoRecord = "No hay registros";
            public static string MessageReference = "Este registro tiene usuarios asignados";
            public static string MessageExist = "Este registro ya existe";
            public static string MessageErrorData = "Error interno de validación de estado";
            public static string MessageReferenceTable = "Este registro tiene referencias";

            //Mensaje de referencias
            public static string MessageReferenceNameUser = "Esta persona tiene un usuario ya asignado";

            //Estados de datos
            public static string StateDataUpdate = "actualizado";
            public static string StateDataOutdated = "desactualizado";
            public static string StateDataBlocked = "bloqueado";

            //Estados globales y usuario
            public static string StateAsset = "activo";
            public static string StateInactive = "inhactivo";
            public static string StateBlocked = "bloqueado";

            //Estados de clave
            public static string StatePassUpdate = "actualizado";
            public static string StatePassOutdated = "desactualizado";
            public static string StatePassBlocked = "bloqueado";

            //Mensajes de Error
            public static string MessageErrorInsert = "Error: Registro no guardado";
            public static string MessageErrorUpdate = "Error: Registro no actualizar";


            public static int Id_Sede_TipoEspacio = 2;

            public static string jornada1 = "01";
            public static string jornada2 = "02";
            public static string jornada3 = "03";
            public static string jornada4 = "04";
            public static string jornada5 = "05";

      }
}
