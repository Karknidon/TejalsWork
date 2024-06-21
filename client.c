#include<unistd.h>
#include<stdio.h>
#include<stdlib.h>
#include<sys/socket.h>
#include<sys/types.h>
#include<netinet/in.h>

int client_socket=socket(AF_INET,SOCK_STREAM,0);

struct sockaddr client_address;
client_address.sin_family=AF_INET;
client_address.sin_port=htons(9002);
client_address.sin_addr.s_addr = INADDR_ANY;
connect(client_socket, (struct sockaddr*)&server_address, sizeof(server_address));

char server_response[256];
recv(client_socket, &server_response, sizeof(server_response), 0);
printf("%s", server_response);

close(client_socket);