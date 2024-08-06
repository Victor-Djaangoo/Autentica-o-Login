using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

[HttpPost]
[Route("login")]

 async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
{
    // recupera o usuario
    var user = UserRepository.Get(model.Username, model.Password);

    // verifica se o usuario existe
    if (user == null)
        return NotFound(new { message = "Usuário ou Senha Inválidos" });

    //gera token
    var token = TokenService.GenerateToken(user);

    //oculta senha
    user.Password = "";

    //retorna os dados
    return new
    {
        user = user,
        token = token,
    };
}

ActionResult<dynamic> NotFound(object value)
{
    throw new NotImplementedException();
}