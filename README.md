# Library

Trabalho Feito por Álvaro Calebe Souza Ferreira na Disciplina Tópicos em Programação III.

# 1 Passo
- Foi feito o desenvolvimento da UML e a criação das entidades para a gestão de reservas de uma biblioteca. As entidades feitas foram: Autor, Categoria, Devolucao, Livro, Reserva e ApplicationUser. Essa última é a responsável por fazer o login e toda a gestão de usuário. Foi feito métodos no Controller de Livro para a inserção de imagens, elas são salvas numa pasta e os nomes são salvos no banco de dados para fazer a sua recuperação e serem exibidas pela view. Foi inserida também as validações solicitadas nos requisitos.

# 2 Passo
- Foi implementado os models e os controllers usando o scaffold. Eu optei por utilizar o code-first para a criação do BD. Também foi feita a migration de todos os relacionamentos e tabelas passando da orietação a objetos para o modelo relacional.

# 3 Passo  
- Foi feito a confirmação por e-mail utilizando o SMTP que é um protocolo que permite o envio de e-mails. Foi implementado todo o controle para que a aplicação não fizesse login sem que antes tivesse o retorno da confirmação que é feita ao clicar no link recebido no e-mai cadastrado.

# 4 Passo 
-Foi criado o IdentityRole "Admin" para que pudesse separar os tipos de usuário. Somente o Admin pode ter acesso aos cruds que controlam os cadastros principais que controlam todos os livros disponíveis e os demais itens do sistema. Para o usuário comum só é permitido reservar um livro após está logado, ele também consegue ter acesso ao histórico de suas reservas e os livros que ele reservou e a data de devolução dos mesmos. 

# 5 Passo 
-Foi feito o refino no front-end usando uma paleta de cores mais agradável e deixando o sistema mais bonito. Foi feito também um filtro na home de livros para que o usuário pudesse escolher os livros por categoria.

# 6 Passo
-Foi feito a correção de alguns bug's e a testagem de todas as funcinalidades do sistema, também foi feito alguns refinos no front como: Troca de fontes e ajuste de botões. 

Por mais que o sistema ficou simples deu para aprender bastante sobre a tecnologia proposta e usar todas as técnicas passadas em sala de aula, foi um desafio bem legal ter pesquisar soluções de algo que não estamos acostumados. Fica meu agradecimento ao professor JOSE ITAMAR MENDES DE SOUZA JUNIOR pela a oportunidade. 

