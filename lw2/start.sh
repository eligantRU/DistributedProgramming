. ./config.sh

docker run --detach --rm --env BACKEND_PORT=$BACKEND_PORT --publish $BACKEND_PORT:5000 --name backendapi backendapi:$1
docker run --detach --rm --env BACKEND_PROTOCOL=$BACKEND_PROTOCOL --env BACKEND_HOST=$BACKEND_HOST --env BACKEND_PORT=$BACKEND_PORT --link $BACKEND_HOST --publish $FRONTEND_PORT:80 --name frontendclient frontendclient:$1

