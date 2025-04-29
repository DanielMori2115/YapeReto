Inicie el docker-compose.yml con el comando:
--------------------------------------------
docker-compose up -d

Luego ejecute en un cmd:
------------------------------------------------------------------------------------
docker exec yapereto-kafka-1 kafka-topics --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic mi-tema

Finalmente, restaure la base de datos con el script:
----------------------------------------------------
ini-db.sql
