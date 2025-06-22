| Método HTTP | Ruta                        | Acción                       |
| ----------- | --------------------------- | ---------------------------- |
| `POST`      | `api/pagos/cargar`          | Crear nuevo pago "Pendiente" |
| `POST`      | `api/pagos/pagar/{id}`      | Cambiar estado a "Pagado"    |
| `GET`       | `api/pagos/Consultar/{id}`  | Consultar datos de un pago   |
| `PUT`       | `api/pagos/Editar/{id}`     | Actualizar todos los campos  |
| `DELETE`    | `api/pagos/Eliminar/{id}`   | Eliminar un pago por su ID   |

Gestor Base de Datos: MySQL
* En la carpeta recursos está el script de la base de datos colegiodb.sql
 _________________________________________________________________________________________________
|				                                                                  |
|* Definir la variable de entorno (CMD o PowerShell)						  |
|								                                  |
| setx DefaultConnection "server=localhost;database=colegiodb;user=root;password=mi_clave_segura;"|
|                                                                                                 | 
| setx JWT_SECRET_KEY "tu_clave_secreta"						          |
|												  |
| setx ALLOWED_ORIGINS "http://localhost:4200;https://colegioapi.com;http://localhost:8080"       |                                                           |
|                                                                                                 |
|________________________________________________________________________________________________ |

* Probar CORS en index.html 
