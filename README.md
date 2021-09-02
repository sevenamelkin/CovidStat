# Сервис получения ковид статистики по ip страны

Запрос для создания таблицы:

CREATE TABLE ip2location(
	ip_from bigint NOT NULL,
	ip_to bigint NOT NULL,
	country_code character(2) NOT NULL,
	country_name character varying(64) NOT NULL,
	CONSTRAINT ip2location_db1_pkey PRIMARY KEY (ip_from, ip_to));

Файл для импорта таблицы ip адресов (лежит в files):

IP2LOCATION-LITE-DB1.csv

docker compose с необходимыми для запуска сервисами docker-compose.yml лежит в files

Коллекция с запросом к сервису:
https://www.getpostman.com/collections/5475eab0164736d2b806
