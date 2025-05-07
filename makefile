start:
    echo "Iniciando os containeres..."
    docker-compose up -d
stop:
    echo "Parando os containers..."
    docker-compose down
reestart:
    echo "Reiniciando os containers..."
    docker-compose down
    docker-compose up -d
rebuild:
    echo "Rebuildando containers..."
    docker-compose down
    docker-compose build --no-cache
    docker-compose up -d
    