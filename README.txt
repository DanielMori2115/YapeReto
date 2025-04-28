Buen dia, espero que todo este muy bien, estas son algunas instrucciones:

Por favor, dirijase a la ubicacion del proyecto y abra una consola:
------------------------------------------------------------------
root/cmd

Luego inicie el docker-compose.yml con el comando:
--------------------------------------------------
docker-compose up -d

Posteriormente cuando ya se encuentren los servicios corriendo, ejecute el comando:
------------------------------------------------------------------------------------
docker exec yapereto-kafka-1 kafka-topics --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic mi-tema

Finalmente, restaure la base de datos con el script:
----------------------------------------------------
ini-db.sql

Muchas gracias,