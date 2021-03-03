# EwsChat
A Very simple chat service - no special technology like RabbitMQ, Kakka, etc.

## Server
The server is written in .Net Core 3.1 and the communication with it happens through the http protocol. It is a (Pragmatic) RESTful API.
Unit tests have been written for the most part of the application but some tests are still to be written (and I will) - tests for some Controllers and Integration Tests.

## Client
The client is written using Angular 9 with Bootstrap. Tt is still buggy and needs improvements, but the Idea of a chat can be indentified clearly.


## Testing
One can test the aplicatoin by conling the repository, navigatnig to the EwsChat.Web folder using a terminal and runing ```dotnet run``` or using the Swagger interface here: [EwsChat API  - Swagger UI](https://localhost:5001/swagger/index.html).
Navigate to EwsChat.Data.Tests or EwsChat.Web.Tests and execute the command ```dotnet test``` to run the unit tests.


## Open API definition
The JSON file of the Open API Definition can be found here: [EwsChat - API Definition](https://localhost:5001/swagger/v1/swagger.json) (the application needs to be running).

## Extra
**If you would like to see more of my development style, check this another .Net 5.0 API Here: https://github.com/ewertonews/SimpleOrderManager/tree/master**
(It was another test and again didn't do all the tests on time (they gave me only 2 days to finish), but I really intend to finish the tests and improve it further)
