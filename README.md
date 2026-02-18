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

	- Altere os campos para os valores desejados para criação do Registro e clique em execute para enviar a requisição.
   
		<img width="1474" height="689" alt="image" src="https://github.com/user-attachments/assets/a014fe8f-aa94-4109-8569-426e2d541c45" />

	- Caso os valores informados estejam dentro do aceito pela aplicação a Aplicação retornará a seguinte mensagem de sucesso:

   		<img width="1404" height="180" alt="image" src="https://github.com/user-attachments/assets/f6442979-5835-4bcf-8af3-73796a59e56c" />
		
- Teste Endpoint UpdateRecordVehicle => Atualizar um Registro já existente.

  	- Altere os campos desejados e clique em enviar. **Todos os campos devem ser preenchidos**(Atente-se ao campo "Id", pois ele deve conter o identificador exato do registro que deseja alterar).
  	  
  	  <img width="1433" height="639" alt="image" src="https://github.com/user-attachments/assets/2be46b9e-6614-49c8-a421-c2bebef33dbd" />

  	- Caso os valores informados estejam dentro do aceito pela aplicação a Aplicação retornará a seguinte mensagem de sucesso:
 
  	  <img width="1433" height="300" alt="image" src="https://github.com/user-attachments/assets/acff946b-17dc-407c-ae57-4a32d69b9e99" />
	  
- Teste Endpoint DeleteRecordVehicle => Excluir Registro existente com base no identificador.

	- Informe o Identificador no campo apropriado e clique em Execute:
   
   		<img width="1445" height="514" alt="image" src="https://github.com/user-attachments/assets/315f4e6e-a48d-4d34-85e3-95ba3146c11c" />
		
  	- Informando um identificador válido, será excluido o registro e a aplicação retornará o seguinte:
 
  	  <img width="1398" height="174" alt="image" src="https://github.com/user-attachments/assets/db0d31b9-d0d4-4faa-89ba-15c66fcd51d1" />

	- Execute o Endpoint GetAllVehicles para confirmar que o registro foi excluido.

   		<img width="1418" height="423" alt="image" src="https://github.com/user-attachments/assets/39491684-d31e-4399-9480-1d8549d7a97c" />

- Teste Endpoint GetAllVehicle => Retornar uma lista contendo todos os Registros existentes na base de dados.(Utilizando In-Memory Database, é necessário efetuar pelo menos um registro antes de testar este Endpoint)

  	- Expanda o bloco do Endpoint GetAllVehicles e clique em execute.
  	  
  	  <img width="1450" height="253" alt="image" src="https://github.com/user-attachments/assets/e44f6f98-5677-4d44-a2eb-bded101390a4" />

	- Caso haja algum registro na base de dados, será retornada uma lista contendo todos os Registros existentes.

	  <img width="1432" height="502" alt="image" src="https://github.com/user-attachments/assets/ab54e4ff-799b-47c4-b669-c1d20953f6d9" />

- Teste Endpoint GetVehicleById => Retornar um único registro com base no Identificador.

  	- Expanda o bloco do Endpoint, informe o Identificador e clique em Execute:
 
  	  <img width="1489" height="351" alt="image" src="https://github.com/user-attachments/assets/27e9ee4d-86f1-4ec6-8175-a74e9d4c6881" />

	- Ao informar um identificador, será retornado o Registro correspondente:

   		<img width="1423" height="277" alt="image" src="https://github.com/user-attachments/assets/0bc042ce-d20e-43ed-94b6-ff067f4fb37c" />







	


  


  
    







