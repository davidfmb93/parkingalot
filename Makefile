INSTALL:
	docker-compose build && docker-compose up db -d && ping 127.0.0.1 -n 30 > ttl && docker-compose up api  -d && echo "Go to: http://localhost:7201/swagger"
RUN:
	docker-compose up -d