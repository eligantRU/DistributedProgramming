bash config.sh
docker run --detach --rm --publish $BACKEND_API_PORT:5000 --name backendapi backendapi:$1
docker run --detach --rm --env BACKEND_HOST=$BACKEND_HOST --link $BACKEND_HOST --publish $FRONTEND_PORT:80 --name frontendclient frontendclient:$1

