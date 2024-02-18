# OrleansBankingClusterTech 🏦

## Русский

### Пример проекта на .NET 8, демонстрирующий создание инфраструктуры кластера с использованием Orleans для реализации распределенных банковских операций. 🚀

#### Начало работы
Этот проект является обучающим примером для понимания принципов работы с Orleans в контексте создания надежной и масштабируемой банковской системы.

#### Предварительные условия
Для запуска проекта необходимы:
- .NET 8
- Docker
- SQL Server (можно развернуть с помощью Docker)

#### Установка
1. **Запуск SQL Server:** 📦  
Используйте `docker-compose.yaml` для запуска экземпляра MS SQL Server:  

```sh
docker-compose up -d

```

2. **Настройка базы данных:** 🛠️  
Создайте и сконфигурируйте базу данных с помощью `run-sql-scripts.cmd` или вручную, следуя официальной документации Orleans. Настройте компоненты для Clustering, Persistence и Reminders.
3. **Запуск кластера:** 🌐  
Запустите силосы (один или несколько), которые объединятся в кластер, с помощью скрипта `run_few_silo_and_one_api.cmd`.

#### Использование
После запуска взаимодействуйте с API через Swagger. Для тестирования кластера:  
`GET /api/TestCluster/SayClusterHi`

#### Банковские операции
Используйте контроллеры Account и Transaction для безопасных банковских операций, таких как переводы и пополнения счетов.

## English

### OrleansBankingClusterTech 🏦

A .NET 8 project showcasing the creation of a cluster infrastructure using Orleans for distributed banking operations. 🚀

#### Getting Started
This project serves as a tutorial example to understand the principles of working with Orleans in the context of creating a reliable and scalable banking system.

#### Prerequisites
Required for the project:
- .NET 8
- Docker
- SQL Server (can be deployed using Docker)

#### Installation
1. **Start SQL Server:** 📦  
Use `docker-compose.yaml` to launch an instance of MS SQL Server:  
```sh
docker-compose up -d

```

2. **Database Setup:** 🛠️  
Create and configure the database using `run-sql-scripts.cmd` or manually by following the official Orleans documentation. Set up components for Clustering, Persistence, and Reminders.
3. **Cluster Launch:** 🌐  
Start the silos (one or more), which will form a cluster, using the `run_few_silo_and_one_api.cmd` script.

#### Usage
After launch, interact with the API via Swagger. To test the cluster:  
`GET /api/TestCluster/SayClusterHi`

#### Banking Operations
Use Account and Transaction controllers for secure banking operations such as transfers and account funding.

