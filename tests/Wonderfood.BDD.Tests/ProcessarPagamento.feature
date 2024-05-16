Funcionalidade: Processar Pagamento
Como um cliente
Eu desejo efetuar um pagamento do meu Pedido
Para que eu possa receber o meu pedido

Cenário: Pagamento efetuado com sucesso na processadora
    Dado que o Cliente possui um Pagamento com o status 'AguardandoRetornoProcessadora'
    Quando o sistema recebe a informação de que o pagamento foi processado com sucesso
    Então o sistema deve atualizar o status do Pagamento para 'PagamentoAprovado'
    E deve comunicar ao Sistema responsável pelos Pedidos que o pagamento foi efetuado com sucesso