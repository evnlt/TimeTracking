## Docker setup for Windows

1. Setup `PostgreSQL`
	> `docker run -d -p 5432:5432 --name postgres --restart unless-stopped -e POSTGRES_PASSWORD=Qwerty123$% -v d:\time-tracker\postgresql:/var/lib/postgresql/data postgres`

2. Setup `RabbitMq`
    > `docker run -d --name rabbitmq --restart unless-stopped -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin rabbitmq:3-management`
