#!/bin/bash
# Пауза, чтобы убедиться, что SQL Server полностью запущен
sleep 20

# Создание базы данных OrleansStorage, если она не существует
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -Q "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'OrleansStorage') BEGIN CREATE DATABASE OrleansStorage END"

# Выполнение скриптов для Orleans в заданной последовательности
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Main.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Clustering.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Persistence.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Reminders.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Clustering-3.7.0.sql

# Запуск SQL Server в foreground режиме после выполнения всех скриптов
exec /opt/mssql/bin/sqlservr
