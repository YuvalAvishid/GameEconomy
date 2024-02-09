# Game Economy
Consider a gaming economy wherein each user possesses an inventory, referred to as the inventory service. 
The acquisition of items within this system occurs through transactions with the store, denoted as the product service.

### The project was built with:
- **ASP.NET Core** and **C#** for cross-platform server side code
- **Docker** for services containerization
- **Kubernetes** for managing containers
- **MongoDB** for database storage
- **RabbitMQ** and **MassTransit** for message-based asynchronous commuincation
- **gRPC** for message-based synchronous commuincation 


# Architecture
- [Game Economy System Architecture](#game-economy)
- [Product Service Architecture](#product-service-architecture)
- [Product Rest API](#product-rest-api)
- [Inventory Service Architecture](#inventory-service-architecture)
- [Inventory Rest API](#inventory-rest-api)

## Game Economy System Architecture
![game_economey_system_architecture drawio](https://github.com/YuvalAvishid/GameEconomy/assets/104455714/5743566a-0e82-40ac-a535-45781f77b757)
</br>
## Product Service
- ### Product Service Architecture
  ![product_service_architecture drawio](https://github.com/YuvalAvishid/GameEconomy/assets/104455714/5203efb4-564e-4aae-8b6c-e8004caf134b)
- ### Product Rest API
  ![product_rest_api drawio](https://github.com/YuvalAvishid/GameEconomy/assets/104455714/e81118e0-1192-4c52-bf97-bc77c0b1289e)
  </br>
  [Ameer]
  - Consider adding a PATCH endpoint (its like UPDATE but only replaces part of the endpoint object)
## Inventory Service
- ### Inventory Service Architecture
  ![inventory_service_architecture drawio](https://github.com/YuvalAvishid/GameEconomy/assets/104455714/4c25dba9-57e6-4813-8592-c558ab2a19bf)
- ### Inventory Rest API
  ![inventory_rest_api drawio](https://github.com/YuvalAvishid/GameEconomy/assets/104455714/bb79ae54-503e-4c06-8e9e-25d2cc154df9)
