docker build -t joblogger:$1 -f Dockerfile.joblogger .
docker build -t textrankcalc:$1 -f Dockerfile.textrankcalc .
docker build -t backendapi:$1 -f Dockerfile.backendapi .
docker build -t frontendclient:$1 -f Dockerfile.frontendclient .

mkdir app-$1

cp start.sh app$1
cp stop.sh app$1
cp config.sh app$1
