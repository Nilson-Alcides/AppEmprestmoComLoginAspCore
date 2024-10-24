﻿using AppLayoutAspCore.Models;

namespace AppLayoutAspCore.Repository.Contrato
{
    public interface IItemRepository
    {
        //CRUD
        IEnumerable<Item> ObterTodosItens();

        void Cadastrar(Item item);

        void Atualizar(Item item);

        Item ObterItens(int Id);

        void Excluir(int Id);
    }
}
