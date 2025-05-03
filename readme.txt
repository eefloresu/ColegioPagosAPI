| Método HTTP | Ruta                        | Acción                       |
| ----------- | --------------------------- | ---------------------------- |
| `POST`      | `api/pagos/cargar`          | Crear nuevo pago "Pendiente" |
| `POST`      | `api/pagos/pagar/{id}`      | Cambiar estado a "Pagado"    |
| `GET`       | `api/pagos/{id}`  	    | Consultar datos de un pago   |
| `PUT`       | `api/pagos/{id}` 	    | Actualizar todos los campos  |
| `DELETE`    | `api/pagos/eliminar/{id}`   | Eliminar un pago por su ID   |
