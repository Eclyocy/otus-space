FROM rabbitmq:3.13-management

RUN apt-get update && apt-get install -y curl && apt-get clean

ADD rabbitmq.config /etc/rabbitmq/
ADD definitions.json /etc/rabbitmq/

RUN chown rabbitmq:rabbitmq /etc/rabbitmq/rabbitmq.config /etc/rabbitmq/definitions.json

CMD ["rabbitmq-server"]

EXPOSE 15672
EXPOSE 5672
