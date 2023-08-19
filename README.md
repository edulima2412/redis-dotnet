Primeiramente, inicie uma instância do Redis localmente. Eu indico utilizar Docker para isso, com o comando abaixo.

docker run -d -p 6379:6379 --name redis redis