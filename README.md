<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Instru��es para Executar o Projeto</title>
</head>
<body>

    <h1>Instru��es para Executar o Projeto</h1>

    <p>Para rodar este projeto, siga as instru��es abaixo:</p>

    <ol>
        <li>Certifique-se de ter o Docker instalado em sua m�quina. Caso ainda n�o tenha, voc� pode baix�-lo <a href="https://www.docker.com/get-started">aqui</a>.</li>

        <li>Execute o seguinte comando no terminal para iniciar o container do SQL Server:</li>
    </ol>

    <pre><code>docker run -it --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server</code></pre>

    <p>Este comando iniciar� um container Docker contendo o SQL Server com as configura��es necess�rias.</p>

    <ol start="3">
        <li>Ap�s o SQL Server estar em execu��o, voc� pode iniciar o projeto. As migrations ser�o aplicadas automaticamente durante a inicializa��o do projeto, gra�as ao c�digo presente em <code>Program.cs</code>:</li>
    </ol>

    <pre><code>
        private static void CreateDataBaseAndApplyMigrations(IHost host)
        {
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var service = services.GetRequiredService&lt;ApplicationDbContext&gt;();
                    service.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    </code></pre>

    <p>Este trecho de c�digo garante que o banco de dados seja criado e as migra��es sejam aplicadas, eliminando a necessidade de interven��o manual.</p>

    <ol start="4">
        <li>O projeto foi desenvolvido utilizando a arquitetura Clean Architecture, proporcionando uma estrutura organizada e modular. Al�m disso, � importante mencionar que o projeto foi constru�do utilizando o .NET 5.</li>
    </ol>

    <p>Agora voc� est� pronto para explorar e trabalhar com o projeto. Em caso de d�vidas ou problemas, consulte a documenta��o do Docker e do .NET para obter assist�ncia adicional.</p>

</body>
</html>
