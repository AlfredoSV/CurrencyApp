🚀 CurrencyApp

Guía para configurar la aplicación.

📌 Requisitos previos

Tener instalado .NET SDK 8

Verifica la versión instalada con:

dotnet --list-sdks
8.0.413 [C:\Program Files\dotnet\sdk]

⚙️ Configuración de la cadena de conexión

En el proyecto CurrencyApp.Web, modifica el valor de Server en el archivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=Alfredo;Database=dbAppCurrency;Integrated Security=True;TrustServerCertificate=True"
}

▶️ Creación de base de datos

Ubícate en la carpeta CurrencyApp\src desde una terminal y ejecuta los siguientes comandos:

🔹 Si borraste las migraciones

dotnet ef migrations add Initial -p .\CurrencyApp.Infrastructure\ -s .\CurrencyApp.Web\
dotnet ef database update Initial -p .\CurrencyApp.Infrastructure\ -s .\CurrencyApp.Web\

🔹 Si usarás la migración Initial existente

dotnet ef database update Initial -p .\CurrencyApp.Infrastructure\ -s .\CurrencyApp.Web\

🔹 Si quieres ejecutar los scripts de la bd directamente

CurrencyApp\src\CurrencyApp.Infrastructure\Scipts\Init.sql

Imagenes:

![cap1](https://github.com/AlfredoSV/CurrencyApp/blob/master/imagesApp/one.png)


![cap2](https://github.com/AlfredoSV/CurrencyApp/blob/master/imagesApp/two.png)


![cap3](https://github.com/AlfredoSV/CurrencyApp/blob/master/imagesApp/three.png)


![cap4](https://github.com/AlfredoSV/CurrencyApp/blob/master/imagesApp/four.png)


![cap5](https://github.com/AlfredoSV/CurrencyApp/blob/master/imagesApp/five.png)
