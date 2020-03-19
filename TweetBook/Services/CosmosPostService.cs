﻿using Cosmonaut;
using Cosmonaut.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class CosmosPostService : IPostService
    {
        private readonly ICosmosStore<CosmosPostDto> _cosmosStore;

        public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<bool> CreatePost(Post post)
        {
            var cosmosPost = new CosmosPostDto { Id = post.Id.ToString(), Name = post.Name };
            var response = await _cosmosStore.AddAsync(cosmosPost);
            return response.IsSuccess;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetPostById(Guid postId)
        {
            var post = await _cosmosStore.FindAsync(postId.ToString(), postId.ToString());

            return post == null ? null : new Post { Id = Guid.Parse(post.Id), Name = post.Name };
        }

        public async Task<List<Post>> GetPosts()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts.Select(x => new Post { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
        }

        public async Task<bool> UpdatePost(Post postUpdate)
        {
            var cosmosPost = new CosmosPostDto { Id = postUpdate.Id.ToString(), Name = postUpdate.Name };

            var response = await _cosmosStore.UpdateAsync(cosmosPost);
            return response.IsSuccess;
        }
    }
}
