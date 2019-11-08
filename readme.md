<p align="center"><img src="https://www.nibo.com.br/logo-nibo.png" width="150" /></p>
<h1 align="center">Nibo Full-Stack Developers Challenge - Level 2</h1>


<p align="center">Quer saber o por que vale a pena trabalhar no Nibo? <a href="https://tech.nibo.com.br">tech.nibo.com.br</a></p>

Olá!

Primeiramente, parabéns por ter avançado ao Desafio para Developers do Nibo! 

## Quem somos
O Nibo nasceu em 2012 com uma missão muito clara: ajudar empresas a gerir suas finanças de forma simples e responsável. Ao longo dos anos, percebemos que a única forma de alcançar esse objetivo seria com a ajuda de uma figura bastante esquecida no imaginário brasileiro - **contador**.

Aqui no Nibo, não enxergamos o contador como o um "mal necessário", mas sim como **cientista da riqueza** - a pessoa com os poderes de ajudar seus clientes a prosperarem e alcançarem o sucesso. 

Traduzimos esse pensamento no nosso manifesto, que nunca deixamos de recitar: 
> Todos os dias, 2000 empresas morrem no Brasil. Empresários precisam de ajuda. Bons contadores são a solução.

Sendo assim, nosso papel é fornecer as ferramentas que aumentem a produtividade e efetividade do contador, de modo que ele tenha tempo para o que realmente importa: ser consultivo e entregar insights valiosos ao seu cliente.

A equipe de Produto e Tecnologia do Nibo é composta por times multifuncionais, com autonomia na tomada de decisão. São as famosas **squads**. Não sabe o que é squad? Assista ao vídeo [Spotify Engineering Culture](https://www.youtube.com/watch?v=hQDblYvY9RY). 

## O desafio
Você deverá criar um simples sistema que atenda à todos os requisitos de negócio e técnicos e nos enviar no prazo estipulado. 

### Persona
Xayah é uma pequena jovem empresária que presta serviços de coaching para grupos de jogadores profissionais de eSports. Ela utiliza Excel para controlar as finanças da empresa.

### Problema
Xayah precisa [concilicar o extrato bancário](https://www.nibo.com.br/blog/como-fazer-conciliacao-bancaria-passo-passo/) da empresa com as entradas e saídas que ela registrou no Excel do último mês. Para isso é utilizado um arquivo do tipo OFX, que contém o registro do banco de todas as entradas e saídas de um período e é fácil de exportar pelo Bankline.

O problema é que Xayah baixou dois arquivos OFX: um que contém transações do dia 01 ao dia 20 e outro que contém transações do dia 15 ao 31. E agora ela não sabe o que fazer para garantir que todas as transações sejam conciliadas corretamente, uma vez que os arquivos OFX possuem transações de um período em comum.

### O que você deverá fazer
Criar um sistema onde a Xayah possa importar dois ou mais arquivos OFX e, no final, poderá ver todas as transações bancárias que ocorreram no período.

Lembre-se que os arquivos OFX poderão conter transações de um mesmo período. Essas transações devem ser exibidas sem duplicidade. Também lembre-se de que é possível que existam transações do mesmo valor em um mesmo dia.

Os arquivos OFX estão na pasta ``\OFX``.

**Requisitos**
- [ ] É necessário persistir as transações finais.
- [ ] Não utilize bibliotecas específicas para a leitura de arquivos do tipo OFX.
- [ ] Você deverá desenvolver uma solução **WEB** em C# .NET utilizando os frameworks, tecnologias e conceitos que julgar melhor.
- [ ] Queremos como resultado uma solução simples, legível e de qualidade. 
- [ ] Código feito e comentado em **inglês**.
- [ ] Não utilize soluções prontas. Nós as conhecemos.
- [ ] Seja criativo. Você decide quais funcionalidades irá incluir além dos requisitos.
- [ ] Não hospede sua aplicação ou parte dela em nenhum lugar. Sua aplicação deverá rodar localmente sem depender de serviços externos.

## Envio da solução
Você deverá criar um fork deste repositório, incluir o seu código fonte na pasta ``SRC``,  preencher o formulário "_about/Profile.md" e enviar para recruta.tech@nibo.com.br o link do seu fork.

Tenha capricho com seu código e com o resultado final. Essa é a sua chance de entrar para o melhor time, na startup que mais cresce no Brasil.

"Domine à si mesmo, e dominará seu inimigo" - Lee Sin

**NIBO - Desenvolvimento de alta performance para geeks inquietos**

Boa sorte :D
