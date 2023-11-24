
<h1>Instruções para Executar o Projeto</h1>

<p>Para rodar este projeto, siga as instruções abaixo:</p>

<ol>
<li>Certifique-se de ter o Docker instalado em sua máquina. Caso ainda não tenha, você pode baixá-lo <a href="https://www.docker.com/get-started">aqui</a>.</li>

<li>Execute o seguinte comando no terminal para iniciar o container do SQL Server:</li>
</ol>

<pre><code>docker run -it --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server</code></pre>

<p>Este comando iniciará um container Docker contendo o SQL Server com as configurações necessárias.</p>

<ol start="3">
    <li>Após o SQL Server estar em execução, você pode iniciar o projeto. As migrations serão aplicadas automaticamente durante a inicialização do projeto, graças ao código presente em <code>Program.cs</code>:</li>
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

<p>Este trecho de código garante que o banco de dados seja criado e as migrações sejam aplicadas, eliminando a necessidade de intervenção manual.</p>

<ol start="4">
    <li>O projeto foi desenvolvido utilizando a arquitetura Clean Architecture, proporcionando uma estrutura organizada e modular. Além disso, é importante mencionar que o projeto foi construído utilizando o .NET 5.</li>
</ol>

<p>Agora você está pronto para explorar e trabalhar com o projeto. Em caso de dúvidas ou problemas, consulte a documentação do Docker e do .NET para obter assistência adicional.</p>

<h1>Explicação de algumas classes contidas no projeto</h1>
<b>UnitOfWork</b>
Usado esse pattern para usar uma única transação na request, ou seja, se chegar no final da request e dar tudo certo
a gente só dá commit e salva no banco, se dar alguma exceção, a classe dará dispose e não será salvo nada no banco

<b>Usando o CancellationToken nas request?</b>
Ele oferece um mecanismo para cancelar operações assíncronas.
É frequentemente usado em operações assíncronas para fornecer uma maneira de sinalizar que uma tarefa ou operação deve ser cancelada. Isso é particularmente útil em cenários onde o usuário deseja interromper uma operação em andamento, 
como uma operação de download, processamento de dados, ou uma tarefa em segundo plano.

