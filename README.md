# API_Veiculos

O projeto a seguir compõe o **Teste Técnico** do **Processo Seletivo** de uma empresa, cujo o objetivo é a criação de uma API Rest para Registro de Veículos.

## Funcionalidades

Por base, a API deveria conter os seguintes **Endpoints**:

- Criação de um Novo Registro na Base de Dados;
- Atualização do Registro na Base de Dados com base em seu identificador (ID);
- Exclusão de um Registro na Base de Dados com base em seu identificador (ID);
- Busca e Listagem de Todos os Registros na Base de Dados;
- Busca e Listagem de um Registro especifico com base em seu identificador (ID);

## Execução

### Clonagem de repositório

**Via prompt cmd:**

	git clone edusalees/API_Veiculos_TestePratico

**Via interface grafica do Visual Studio:**

<img width="1919" height="1039" alt="image" src="https://github.com/user-attachments/assets/77480ed4-0f71-4f0f-9bc3-65fa4e8ff212" />

### Configurações

- Com o projeto aberto, configurar o projeto de Inicialização:
  
<img width="955" height="850" alt="image" src="https://github.com/user-attachments/assets/182505c5-bdb9-4032-bb52-5de234f5af49" />

- No arquivo "appSettings.json", configurar os seguintes parâmetros:
    - SqlServer_ConnectionString => [string] => String de Conexão com a Base de Dados.
    - UseSqlDatabase => [bool] => Indicar ao sistema se será usada uma Base de Dados externa. [true] => Utiliza Base de Dados externa | [false] => Utiliza Base de Dados In-Memory

### Inicie a execução do projeto

### Teste Via Swagger

- Conceitos Gerais:
  - Ao entrar na pagina do Swagger, iremos ver todos os Endpoints, disponíveis para teste, da aplicação.
    <img width="1919" height="1035" alt="image" src="https://github.com/user-attachments/assets/7afddc8a-a759-405e-973e-787a3cd95cba" />

  - Expandindo o bloco que cada Endpoint, temos a seguinte tela. Ela segue o mesmo padrão para todos os Endpoints.
    <img width="1919" height="1037" alt="image" src="https://github.com/user-attachments/assets/d089a59e-1bb4-4889-b4da-d230fec79ead" />

  - Durantes os testes, será usada a opção TryOut para envio de requisições via Swagger.
 
  - Json para requisição de Criação de um Novo Registro:

        {     
          "id": 0,
          "descVeiculo": "string",
          "marcaVeiculo": 0,
          "modeloVeiculo": "string",
          "opcionaisVeiculo": "string",
          "valorVeiculo": 0
        }
    "id" => [Int64]
    "descVeiculo" => [string] Max 100 Caracteres
    "marcaVeiculo" => [enum] Consultar "MarcaVeiculo" na pasta Enum
    "modeloVeiculo" => [string] Max 30 Caracteres
    "opcionaisVeiculo" => [string]
    "valorVeiculo" => [string]
 
- Teste Endpoint RegisterVehicle => Registro de um novo Veículo na Base de Dados.

  
    







