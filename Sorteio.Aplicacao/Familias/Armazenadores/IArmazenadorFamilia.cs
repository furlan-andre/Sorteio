using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.Familias.Armazenadores;

public interface IArmazenadorFamilia
{
    Task<Familia> ArmazenarAsync(ArmazenaFamiliaDto armazenaFamiliaDto);
}