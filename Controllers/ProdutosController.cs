using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;
using CRUD.DTO;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produtos>>> GetProdutos()
        {
            return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoDTO produtoDTO)
        {
            if(!ProdutosExists(id))
            {
                return NotFound();
            }
            else if (GetCategoria(produtoDTO.CategoriaId) == null)
            {
                return NotFound();
            }

            var produto = _context.Produtos.Find(id);
            produto.Produto = produtoDTO.Produto;
            produto.Preco = produtoDTO.Preco;
            produto.Quantidade = produtoDTO.Quantidade;
            produto.Categoria = GetCategoria(produtoDTO.CategoriaId);

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProdutosExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> PostProduto(ProdutoDTO produtoDTO)
        {
            if (GetCategoria(produtoDTO.CategoriaId) == null)
            {
                return NotFound();
            }

            var produto = new Produtos
            {
                Produto = produtoDTO.Produto,
                Preco = produtoDTO.Preco,
                Quantidade = produtoDTO.Quantidade,
                Categoria = GetCategoria(produtoDTO.CategoriaId)
            };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutos", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produtos>> DeleteProdutos(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();

            return produtos;
        }

        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }

        private Categoria GetCategoria(int id)
        {
            return _context.Categoria.Find(id);
        }
    }
}
