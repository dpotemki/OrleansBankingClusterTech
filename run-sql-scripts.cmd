
@echo off


REM Execute SQL scripts
docker exec -it sqlserver_container /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d master -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'OrleansStorage') BEGIN CREATE DATABASE OrleansStorage END"
docker exec -it sqlserver_container /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Main.sql
docker exec -it sqlserver_container /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Clustering.sql
docker exec -it sqlserver_container /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Persistence.sql
docker exec -it sqlserver_container /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Reminders.sql
docker exec -it sqlserver_container /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Akjd3_as77" -d OrleansStorage -i /usr/src/app/SQLServer-Clustering-3.7.0.sql

echo finish executing SQL 
