﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog42.Models;

namespace Blog42.DataAccess
{
    public class CommentDAO
    {
        private Blog42Entities entities;

        public CommentDAO() { }

        // Inicializa entity
        private void Init()
        {
            entities = new Blog42Entities();
        }

        /*
         * Método que retorna um comentário de acordo com seu id
         * Caso não possua nenhum comentário com esse id, o retorno é null.
         */
        public Comment GetComment(int id)
        {
            Init(); // Atualiza antes de executar operação
            // Captura os dados do comentário e retorna
            var comment = entities.Comment.Find(id);
            return comment;
        }

        /*
         * Cria um novo registro de comentário
         * Recebe um Comment e retorna um bool informando sucesso
         */
        public bool CreateComment(Comment comment)
        {
            Init(); // Atualiza antes de executar operação
            try
            {
                // Insere momento atual na data/hora de criação
                comment.CreatedAt = DateTime.Now;
                // Inseri e salva as alterações
                entities.Comment.Add(comment);
                entities.SaveChanges();
                // Se ocorrer tudo bem, retorna true
                return true;
            }
            catch (Exception)
            {
                // Caso ocorra algum problema retorna false
                return false;
            }
        }

        /*
         * Deleta um registro de comentário
         * Recebe um int (id do comentário) e retorna um bool informando sucesso
         */
        public bool DeleteComment(int id)
        {
            Init(); // Atualiza antes de executar operação
            try
            {
                // Recebe comentário
                var commentSelected = entities.Comment.Find(id);
                // Remove comentário e salva
                entities.Comment.Remove(commentSelected);
                entities.SaveChanges();
                // Se ocorrer tudo bem, retorna true
                return true;                
            }
            catch (Exception)
            {
                // Caso ocorra algum problema retorna false
                return false;
            }
        }

        /*
         * Seleciona todos os comentários
         * Retorna lista de Comentários
         */
        public List<Comment> SelectAllComments()
        {
            Init(); // Atualiza antes de executar operação
            // Recebe todos os comentários ordenados pelo Id descendente
            var comments = entities.Comment.OrderByDescending(m => m.Id).ToList<Comment>();
            return comments;
        }


        /*
         * Seleciona todos os comentários de um post pelo id do post
         * Recebe int (id da postagem) e retorna lista de comentários
         */
        public List<Comment> SelectCommentsByPost(int postId)
        {
            Init(); // Atualiza antes de executar operação
            // Recebe todos os comentários de um post ordenados pelo id descendente e retorna em lista
            var comments = entities.Comment.Where(m => m.PostId == postId)
                                   .OrderByDescending(m => m.Id)
                                   .ToList<Comment>();
            return comments;
        }

        /*
         * Seleciona todos os comentários de um usuário pelo username do usuário
         * Recebe string (username do usuário) e retorna lista de comentários
         */
        public List<Comment> SelectCommentsByAuthor(string username)
        {
            Init(); // Atualiza antes de executar operação
            // Recebe todos os comentários de um usuário ordenados pelo id descendente e retorna em lista
            var comments = entities.Comment.Where(m => m.Post.User.Username == username)
                                   .OrderByDescending(m => m.Id)
                                   .ToList<Comment>();
            return comments;
        }

    }
}