## Project overview

TimeTracker is a lightweight employee attendance system using NFC cards.

Employees “tap” their NFC card twice per day:

- Check-in (start work)
- Check-out (end work)

The system tracks:

- attendance history;
- working time statistics;
- schedule rules and exclusions;

## Key endpoints
- POST /card/touch – register NFC card touch
- POST /card/assign – assign card to user
- POST /card/delete – remove card
- POST /work_time/set – set schedule
- POST /work_time/add_exclusion – add schedule exception
- GET /work_time/history_by_user – user history
- GET /work_time/statistics_by_user – user stats

## Docker setup for Windows

1. Setup `PostgreSQL`
	> `docker run -d -p 5432:5432 --name postgres --restart unless-stopped -e POSTGRES_PASSWORD=Qwerty123$% -v d:\time-tracker\postgresql:/var/lib/postgresql/data postgres`

2. Setup `RabbitMq`
    > `docker run -d --name rabbitmq --restart unless-stopped -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin rabbitmq:3-management`


## Docker compose setups

1. Start everything:
    > `docker compose up --build`
2. Only start infrastructure:
    > `docker compose up postgres rabbitmq`
3. Run tests:
    > `docker compose --profile test up --build`