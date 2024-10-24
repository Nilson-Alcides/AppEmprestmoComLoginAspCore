﻿using AppLayoutAspCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
//using X.PagedList;

namespace AppLayoutAspCore.Repositories.Contracts
{
    public interface ICategoriaRepository
    {
        //CRUD
        void Cadastrar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Excluir(int Id);
        Categoria ObterCategoria(int Id);
        Categoria ObterCategoria(string Slug);
        IEnumerable<Categoria> ObterCategoriasRecursivas(Categoria categoriaPai);
        IEnumerable<Categoria> ObterTodasCategorias();
        IPagedList<Categoria> ObterTodasCategorias(int? pagina);
        IPagedList<Categoria> ObterTodasCategorias(int? pagina, string pesquisa);
    }
}
