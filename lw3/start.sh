. ./config.sh

docker run --detach --rm --publish $NATS_PORT:4222 --name nats nats
docker run --detach --rm --publish $REDIS_PORT:6379 --name redis redis

docker run --detach --rm --env NATS_HOST=$NATS_HOST --env NATS_PORT=$NATS_PORT --env REDIS_HOST=$REDIS_HOST --env REDIS_PORT=$REDIS_PORT --env NATS_BUS=$NATS_BUS --link $NATS_HOST --link $REDIS_HOST --publish $BACKEND_PORT:5000 --name backendapi backendapi:$1
docker run --detach --rm --env NATS_HOST=$NATS_HOST --env NATS_PORT=$NATS_PORT --env REDIS_HOST=$REDIS_HOST --env REDIS_PORT=$REDIS_PORT --env NATS_BUS=$NATS_BUS --link $NATS_HOST --link $REDIS_HOST --name joblogger joblogger:$1

docker run --detach --rm --env BACKEND_HOST=$BACKEND_HOST --link $BACKEND_HOST --publish $FRONTEND_PORT:80 --name frontendclient frontendclient:$1
